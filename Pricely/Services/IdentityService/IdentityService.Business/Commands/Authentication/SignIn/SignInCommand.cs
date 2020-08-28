﻿using MediatR;

namespace IdentityService.Business.Commands.Authentication.SignIn
{
    public class SignInCommand : IRequest<SignInResponse>
    {
        public SignInCommand(string email, string password, bool rememberMe)
        {
            Email = email;
            Password = password;
            RememberMe = rememberMe;
        }

        public string Email { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
