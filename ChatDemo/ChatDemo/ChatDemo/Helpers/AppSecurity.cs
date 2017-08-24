using ChatDemo.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatDemo.Helpers
{
    internal static class AppSecurity
    {

        public static User CurrentUser { get; private set; }
        public static string userName { get; set; }
        const string TokenKey = "CHAT_DEMO_TOKEN";

        public static string Token { get; set; }

        public static bool IsAuthenticated
        {
            get { return !string.IsNullOrWhiteSpace(userName); }
        }

        static AppSecurity()
        {
            CurrentUser = Data.Repository.FindOne<User>(x => true);
            if(CurrentUser== null)
            {
                return;
            }
            else
            {
                userName = CurrentUser.UserName;
            }
            //if (App.Current.Properties.ContainsKey(TokenKey))
            //{
            //    Token = Convert.ToString(App.Current.Properties[TokenKey]);
            //}
            //else
            //{
            //    Token = null;
            //}
        }

        public static void Login(string name,string username)
        {
           
            var user = new User
            {
                Name = name,
                UserName = username,
                CreatedOnUtc = DateTime.UtcNow,
                UpdatedOnUtc = DateTime.UtcNow,
            };
            userName = user.UserName;
            CurrentUser = user;
            Data.Repository.SaveOrUpdate(user);

            //if (App.Current.Properties.ContainsKey(TokenKey))
            //{
            //    App.Current.Properties[TokenKey] = token;
            //}
            //else
            //{
            //    App.Current.Properties.Add(TokenKey, token);
            //}
            //Token = token;
        }

        public static void Logout()
        {
            if (CurrentUser != null)
            {
                Data.Repository.Delete(CurrentUser);
                CurrentUser = null;
            }
            //if (App.Current.Properties.ContainsKey(TokenKey))
            //{
            //    App.Current.Properties.Remove(TokenKey);
            //}
            //Token = null;
        }

    }
}
