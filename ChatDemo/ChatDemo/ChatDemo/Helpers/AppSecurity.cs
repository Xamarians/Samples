using ChatDemo.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ChatDemo.Helpers
{
    internal static class AppSecurity
    {
        const string TokenKey = "CHAT_DEMO_TOKEN";

        static User _currentUser = null;
        public static User CurrentUser
        {
            get
            {
                if (_currentUser == null)
                    _currentUser = Data.Repository.FindOne<User>(x => true);
                return _currentUser;
            }
        }

        public static string Token { get; private set; }
        public static DateTime ExpiryTime { get; private set; }

        public static bool IsAuthenticated
        {
            get
            {
                return !string.IsNullOrWhiteSpace(Token)
                    && ExpiryTime > DateTime.Now;
            }
        }

        static AppSecurity()
        {
            Initialize();
        }

        private static void Initialize()
        {
            if (App.Current.Properties.ContainsKey(TokenKey))
            {
                var json = Convert.ToString(App.Current.Properties[TokenKey]);
                var logingObj = JsonConvert.DeserializeObject<LoginObject>(json);
                if (logingObj.ExpiryTime > DateTime.Now)
                {
                    ExpiryTime = logingObj.ExpiryTime;
                    Token = logingObj.Token;
                    return;
                }
            }
            ExpiryTime = DateTime.Now.AddDays(-1);
            Token = null;
            CurrentUser = null;
        }

        public static void Login(string token, int expirySeconds)
        {
            var expDate = DateTime.Now.AddSeconds(expirySeconds - 4);
            var loginObj = new LoginObject { Token = token, ExpiryTime = expDate };
            var json = JsonConvert.SerializeObject(loginObj);
            if (App.Current.Properties.ContainsKey(TokenKey))
            {
                App.Current.Properties[TokenKey] = json;
            }
            else
            {
                App.Current.Properties.Add(TokenKey, json);
            }
        }


        public static void Logout()
        {
            if (App.Current.Properties.ContainsKey(TokenKey))
            {
                App.Current.Properties[TokenKey] = null;
            }
            Data.Repository.ClearDatabasse();
            Token = null;
            CurrentUser = null;
        }

        class LoginObject
        {
            public string Token { get; set; }
            public DateTime ExpiryTime { get; set; }
        }

    }
}
