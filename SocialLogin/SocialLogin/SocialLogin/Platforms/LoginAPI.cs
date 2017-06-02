using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Auth;
using Xamarin.Forms;
using Newtonsoft.Json;
#if __ANDROID__
using Android.Content;
using SocialLogin.Droid.Platforms;
#endif
namespace SocialLogin.Platforms
{
    class XamariansSocialSdk
    {
        const string TwitterServiceId = "TwitterService";
        const string LinkedInServiceId = "LinkedInService";
        const string UserInfoUrl= "https://www.googleapis.com/oauth2/v2/userinfo";

        #region Twitter Settings
        const string TwitterRequestTokenUrl = "https://api.twitter.com/oauth/request_token";
        const string TwitterAccessTokenUrl = "https://api.twitter.com/oauth/access_token";
        const string TwitterAuthUrl = "https://api.twitter.com/oauth/authorize";
        #endregion

        #region LinkedIn  Settings

        const string LinkedinAuthUrl = "https://www.linkedin.com/uas/oauth2/authorization";
        const string LinkedinRedirectURL = "https://www.linkedin.com";
        const string LinkedinAccessTokenURL = "https://www.linkedin.com/uas/oauth2/accessToken";
        const string LinkedinScope = "r_basicprofile r_emailaddress w_share";
        const string LinkedinProfileURL = "https://api.linkedin.com/v1/people/~:(id,firstName,lastName,headline,picture-url,summary,educations,three-current-positions,honors-awards,site-standard-profile-request,location,api-standard-profile-request,phone-numbers)";

        #endregion
        public AccountStore GetAccountStore()
        {

#if __ANDROID__
            return AccountStore.Create(Forms.Context);
#endif
#if __IOS__
            return AccountStore.Create();
#endif
        }

        private void DisplayUI(WebAuthenticator auth)
        {
#if __ANDROID__
            Forms.Context.StartActivity(auth.GetUI(Forms.Context));
#endif
#if __IOS__
			SocialLogin.iOS.AppDelegate.GetController().PresentViewController(auth.GetUI(), true, null);
#endif
        }

        public static void FacebookLoginAsync(Action<FBLoginResult> callback)
        {
#if __ANDROID__
            FacebookLoginActivity.OnLoginCompleted(callback);
            var fbIntent = new Intent(Xamarin.Forms.Forms.Context, typeof(FacebookLoginActivity));
            fbIntent.PutExtra("Permissions", "email");
            Xamarin.Forms.Forms.Context.StartActivity(fbIntent);
#endif


        }


        public static void GoogleLoginAsync(Action<GoogleLoginResult> callback)
        {
#if __ANDROID__
            GoogleLoginActivity.OnLoginCompleted(callback);
            var fbIntent = new Intent(Forms.Context, typeof(GoogleLoginActivity));
            Xamarin.Forms.Forms.Context.StartActivity(fbIntent);
#endif

            //#if __IOS__
            //            var loginResult = DependencyService.Get<IGoogleLogin>(DependencyFetchTarget.GlobalInstance);
            //            if (loginResult == null)
            //                return;

            //            var result = await loginResult.GoogleLogin();
            //            if (result == null)
            //                return;

            //            callback.Invoke(result);
            //#endif
        }

        public Task<GoogleOuthLoginResult> GoogleLoginAsync()
        {
            var accStore = GetAccountStore();
            var accounts = accStore.FindAccountsForService(LinkedInServiceId);
            if (accounts != null && accounts.Any())
            {
                var result = CreateGooogleLoginResult(accounts.FirstOrDefault());
                return Task.FromResult(result);
            }

            var tcs = new TaskCompletionSource<GoogleOuthLoginResult>();
            var auth = new OAuth2Authenticator(
                            clientId: App.GoogleClientId,
                            clientSecret: App.GoogleClientSecret,
                            scope: "profile email",
                            authorizeUrl: new Uri("https://accounts.google.com/o/oauth2/auth"),
                            redirectUrl: new Uri("http://www.google.com"),
                            accessTokenUrl: new Uri("https://accounts.google.com/o/oauth2/token")
                        );

            // If authorization succeeds or is canceled, .Completed will be fired.
            auth.AllowCancel = true;
            auth.Completed += async (sender, e) =>
            {
                if (e.IsAuthenticated)
                {
                   // UserInfoUrl = https://www.googleapis.com/oauth2/v2/userinfo
                    var request = new OAuth2Request("GET", new Uri(UserInfoUrl), null, e.Account);
                    var response = await request.GetResponseAsync();
                    if (response != null)
                    {
                        string userJson = response.GetResponseText();
                       //var result= JsonConvert.DeserializeObject<User>(userJson);
                    }

                    //accStore.Save(e.Account, LinkedInServiceId);
                    //var result = CreateGooogleLoginResult(e.Account);
                    //tcs.TrySetResult(result);
                }
                else
                {
                    var result = new GoogleOuthLoginResult
                    {
                        IsAuthenticated = false,
                        ErrorMessage = "Authentication Failed.",
                    };
                    tcs.TrySetResult(result);
                }
            };

            var token = auth.RequestAccessTokenAsync("");
            DisplayUI(auth);
            return tcs.Task;
        }

        private GoogleOuthLoginResult CreateGooogleLoginResult(Account account)
        {
            return new GoogleOuthLoginResult
            {
                IsAuthenticated = true,
                IsCancelled = false,
                AccountName = "LinkedIn",
                Token = account.Properties["access_token"],
                UserName = account.Username,
            };
        }

        public Task<LinkedInLoginResult> LinkedInLoginAsync()
        {
            var accStore = GetAccountStore();
            var accounts = accStore.FindAccountsForService(LinkedInServiceId);
            if (accounts != null && accounts.Any())
            {
                var result = CreateLinkedInResult(accounts.FirstOrDefault());
                return Task.FromResult(result);
            }

            var tcs = new TaskCompletionSource<LinkedInLoginResult>();
            var auth = new OAuth2Authenticator(
                            clientId: App.LinkedinClientId,
                            clientSecret: App.LinkedinClientSecret,
                            scope: "r_basicprofile r_emailaddress rw_company_admin w_share",
                            authorizeUrl: new Uri("https://www.linkedin.com/uas/oauth2/authorization"),
                            redirectUrl: new Uri("https://www.linkedin.com/"),
                            accessTokenUrl: new Uri("https://www.linkedin.com/uas/oauth2/accessToken")
                        );

            // If authorization succeeds or is canceled, .Completed will be fired.
            auth.AllowCancel = true;
            auth.Completed += (sender, e) =>
            {
#if __IOS__
                SocialLogin.iOS.AppDelegate.GetController().DismissViewController(true, null);
#endif
				if (e.IsAuthenticated)
                {
                    accStore.Save(e.Account, LinkedInServiceId);
                    var result = CreateLinkedInResult(e.Account);
                    tcs.TrySetResult(result);
                }
                else
                {
                    var result = new LinkedInLoginResult
                    {
                        IsAuthenticated = false,
                        ErrorMessage = "Authentication Failed.",
                    };
                    tcs.TrySetResult(result);
                }
            };

            var token = auth.RequestAccessTokenAsync("");
            DisplayUI(auth);
            return tcs.Task;
        }

        private LinkedInLoginResult CreateLinkedInResult(Account account)
        {
            return new LinkedInLoginResult
            {
                IsAuthenticated = true,
                IsCancelled = false,
                AccountName = "LinkedIn",
                Token = account.Properties["access_token"],
                UserName = account.Username,
            };
        }

        public Task<TwitterLoginResult> TwitterLoginAsync()
        {
            var accStore = GetAccountStore();
            var accounts = accStore.FindAccountsForService(TwitterServiceId);
            if (accounts != null && accounts.Any())
            {
                var result = CreateTwitterResult(accounts.FirstOrDefault());
                return Task.FromResult(result);
            }
            var tcs = new TaskCompletionSource<TwitterLoginResult>();

            var auth = new OAuth1Authenticator(consumerKey: App.TwitterConsumerKey, consumerSecret: App.TwitterConsumerSecret,
               requestTokenUrl: new Uri(TwitterRequestTokenUrl), authorizeUrl: new Uri(TwitterAuthUrl),
               accessTokenUrl: new Uri(TwitterAccessTokenUrl),
               callbackUrl: new Uri(App.TwitterCallbackUrl));
            auth.AllowCancel = true;
            auth.Completed += (sender, e) =>
            {
#if __IOS__
                SocialLogin.iOS.AppDelegate.GetController().DismissViewController(true, null);
#endif
                if (e.IsAuthenticated)
                {
                    try
                    {
                        accStore.Save(e.Account, TwitterServiceId);
                        var result = CreateTwitterResult(e.Account);
                        tcs.TrySetResult(result);
                    }
                    catch (Exception ex)
                    {
                        var result = new TwitterLoginResult
                        {
                            IsAuthenticated = false,
                            ErrorMessage = "Authentication Failed.",
                        };
                        tcs.TrySetResult(result);
                    }
                }
                else
                {
                    var result = new TwitterLoginResult
                    {
                        IsAuthenticated = false,
                        ErrorMessage = "Authentication Failed.",
                    };
                    tcs.TrySetResult(result);
                }
            };

            auth.Error += (s, e) =>
            {
                var exc = e.Exception;
            };

            DisplayUI(auth);
            return tcs.Task;
        }

        private TwitterLoginResult CreateTwitterResult(Account account)
        {
            return new TwitterLoginResult
            {
                IsAuthenticated = true,
                IsCancelled = false,
                AccountName = "Twitter",
                Token = account.Properties["oauth_token"],
                TokenSecret = account.Properties["oauth_token_secret"],
                UserId = account.Properties["user_id"],
                UserName = account.Properties["screen_name"]
            };
        }

		public Task<FacebookLoginResult> FacebookLoginWithPublishPermissionAsync()
		{
			var fbLoginSdk = DependencyService.Get<IFacebookLoginSdk>(DependencyFetchTarget.GlobalInstance);
			if (fbLoginSdk == null)
				return Task.FromResult<FacebookLoginResult>(null);

			var tcs = new TaskCompletionSource<FacebookLoginResult>();
			fbLoginSdk.Completed += (s, e) =>
			{
				tcs.TrySetResult(e);
			};
			fbLoginSdk.LogInWithPublishPermissions(new string[] { "publish_actions" });
			return tcs.Task;
		}

		public interface IFacebookLoginSdk
		{
			event EventHandler<FacebookLoginResult> Completed;
			void LogInWithReadPermissions(string[] permissions);
			void LogInWithPublishPermissions(string[] permissions);
		}

	}
}
