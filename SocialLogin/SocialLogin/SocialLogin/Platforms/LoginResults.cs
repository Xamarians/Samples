

namespace SocialLogin.Platforms
{
    public class FBLoginResult
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string ApplicationId { get; set; }
        public string JsonData { get; set; }
        public string AccessToken { get; set; }
        public FBStatus Status { get; set; }
        public string Message { get; set; }
    }

    public class GoogleLoginResult
    {
        public bool IsSuccess { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string ApplicationId { get; set; }
        public string JsonData { get; set; }
        public string AccessToken { get; set; }
        public string Message { get; set; }
    }

    public class FBUserProfile
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
    }

    public enum FBStatus
    {
        Error = 0, Success = 1, Cancelled = 2
    }


    public class AuthResult
    {
        public bool IsCancelled { get; set; }
        public bool IsAuthenticated { get; set; }
        public string Token { get; set; }
        public string TokenSecret { get; set; }
        public string ErrorMessage { get; set; }
        public string AccountName { get; set; }
    }

    public class TwitterLoginResult : AuthResult
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
    }

    public class LinkedInLoginResult : AuthResult
    {
        public string UserName { get; set; }
    }

    public class GoogleOuthLoginResult : AuthResult
    {
        public string UserName { get; set; }
    }

	public class FacebookLoginResult : AuthResult
	{
	}
}
