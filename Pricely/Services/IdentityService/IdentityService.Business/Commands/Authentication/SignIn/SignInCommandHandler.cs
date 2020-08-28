using Common.Exceptions;
using IdentityService.Business.Interfaces;
using IdentityService.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace IdentityService.Business.Commands.Authentication.SignIn
{
    public class SignInCommandHandler : IRequestHandler<SignInCommand, SignInResponse>
    {
        private readonly SignInManager<Company> _signInManager;
        private readonly UserManager<Company> _userManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public SignInCommandHandler(SignInManager<Company> signInManager, UserManager<Company> userManager, IJwtTokenGenerator jwtTokenGenerator)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<SignInResponse> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(request.Email);
                if (user == null)
                    throw new NotFoundException(request.Email);

                var signInResult = await _signInManager.CheckPasswordSignInAsync(user, request.Password, true);
                if (!signInResult.Succeeded)
                    throw new UnauthorizedAccessException("Wrong username or password");

                var identity = await GetIdentityClaims(user);

                var token = _jwtTokenGenerator.GenerateToken(user, identity, request.RememberMe);

                return new SignInResponse(token);
            }
            catch (Exception e)
            {
                throw new UnauthorizedAccessException(e.Message, e.InnerException);
            }
        }

        internal async Task<ClaimsIdentity> GetIdentityClaims(Company user)
        {
            var identity = new ClaimsIdentity();

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Name),
            };

            if (!string.IsNullOrWhiteSpace(user.LogoUrl))
            {
                claims.Add(new Claim(nameof(Company.LogoUrl), user.LogoUrl));
            }

            if (!string.IsNullOrWhiteSpace(user.PhoneNumber))
            {
                claims.Add(new Claim(ClaimTypes.HomePhone, user.PhoneNumber));
            }

            if (user.Address != null)
            {
                claims.Add(new Claim(ClaimTypes.StreetAddress, user.Address?.GetAddress()));
            }

            identity.AddClaims(claims);

            return identity;
        }
    }
}