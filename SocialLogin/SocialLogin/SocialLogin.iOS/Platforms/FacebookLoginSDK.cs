//using System;
//using Xamarin.Forms;
//using SocialLogin.Platforms;
//using Facebook.LoginKit;
//using Facebook.CoreKit;
//using Foundation;

//[assembly: Dependency(typeof(SocialLogin.iOS.Social.FacebookLoginSDK))]
//namespace SocialLogin.iOS.Social
//{
//	class FacebookLoginSDK : XamariansSocialSdk.IFacebookLoginSdk
//	{
//		public event EventHandler<FacebookLoginResult> Completed;
//		static LoginManager _Instance;
//		public static LoginManager FbLoginManager
//		{
//			get
//			{
//				if (_Instance == null)
//					_Instance = new LoginManager();
//				return _Instance;
//			}
//		}

//		public FacebookLoginSDK()
//		{
//			FbLoginManager.Init();
//		}

//		private AccessToken GetCurrentAccessToken()
//		{
//			AccessToken accessToken = AccessToken.CurrentAccessToken;
//			if (accessToken == null || accessToken.ExpirationDate.Compare(NSDate.Now) == NSComparisonResult.Ascending)
//			{
//				return null;
//			}
//			return accessToken;
//		}

//		void XamariansSocialSdk.IFacebookLoginSdk.LogInWithReadPermissions(string[] permissions)
//		{
//			var token = GetCurrentAccessToken();
//			if (token != null)
//			{
//				LoginCompleted(new LoginManagerLoginResult(token, false, null, null), null);
//			}
//			else
//			{
//				FbLoginManager.LogInWithReadPermissions(permissions, AppDelegate.GetController(), LoginCompleted);
//			}
//		}

//		void XamariansSocialSdk.IFacebookLoginSdk.LogInWithPublishPermissions(string[] permissions)
//		{
//			var token = GetCurrentAccessToken();
//			if (token != null)
//			{
//				LoginCompleted(new LoginManagerLoginResult(token, false, null, null), null);
//			}
//			else
//			{
//				FbLoginManager.LogInWithPublishPermissions(permissions, AppDelegate.GetController(), LoginCompleted);
//			}
//		}

//		public void LogInPublishPermissions(string[] permissions)
//		{
//			FbLoginManager.LogInWithPublishPermissions(permissions, AppDelegate.GetController(), LoginCompleted);
//		}

//		public void LoginCompleted(LoginManagerLoginResult result, Foundation.NSError error)
//		{
//			if (result == null)
//			{
//				FbLoginManager.LogOut();
//				Completed?.Invoke(this, new FacebookLoginResult() { AccountName = "Facebook", IsCancelled = true, ErrorMessage = "Cancelled by user." });
//			}
//			else if (result.IsCancelled)
//			{
//				Completed?.Invoke(this, new FacebookLoginResult() { AccountName = "Facebook", IsCancelled = true, ErrorMessage = "Cancelled by user." });
//			}
//			else if (error != null)
//			{
//				Completed?.Invoke(this, new FacebookLoginResult() { AccountName = "Facebook", IsAuthenticated = false, ErrorMessage = error.Description });
//			}
//			else
//			{
//				var declPermissions = result.DeclinedPermissions;
//				if (declPermissions != null && declPermissions.Count > 0)
//				{
//					FbLoginManager.LogOut();
//					Completed?.Invoke(this, new FacebookLoginResult()
//					{
//						AccountName = "Facebook",
//						IsAuthenticated = false,
//						ErrorMessage = "Permission declined for " + string.Join(",", declPermissions)
//					});
//				}
//				else
//				{
//					Completed?.Invoke(this, new FacebookLoginResult()
//					{
//						AccountName = "Facebook",
//						IsAuthenticated = true,
//						Token = result?.Token?.TokenString
//					});
//				}
//			}
//		}

//	}
//}