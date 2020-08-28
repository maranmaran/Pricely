namespace IdentityService.Business.Commands.Authentication.SignIn
{
    public class SignInResponse
    {
        public SignInResponse(string token)
        {
            Token = token;
        }

        public string Token { get; set; }
    }
}