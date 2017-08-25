using ChatDemo.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ChatDemo.Helpers
{
    internal static class AppSecurity
    {

        public static User CurrentUser { get; private set; }
        public static string ContactNumber { get; set; }
        const string TokenKey = "CHAT_DEMO_TOKEN";

        public static string Token { get; set; }

        public static bool IsAuthenticated
        {
            get { return !string.IsNullOrWhiteSpace(ContactNumber); }
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
                ContactNumber = CurrentUser.ContactNo;
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

        public static void Register(string firstName,string lastName,string userName,string Number,string password)
        {
            var user = new User
            {
                firstName = firstName,
                LastName=lastName,
                UserName=userName,
                ContactNo = Number,
                Password=password,
                CreatedOnUtc = DateTime.UtcNow,
                UpdatedOnUtc = DateTime.UtcNow,
            };
            ContactNumber = user.ContactNo;
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
