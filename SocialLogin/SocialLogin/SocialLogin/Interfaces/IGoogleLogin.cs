using SocialLogin.Platforms;
using System.Threading.Tasks;

namespace SocialLogin.Interfaces
{
    interface IGoogleLogin
    {
        Task<GoogleLoginResult> GoogleLogin();

    }
}
