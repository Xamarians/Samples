using ChatDemo.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChatDemo.Services
{

    class AccountManager
    {

        readonly WebService Service;
        public AccountManager()
        {
            Service = new WebService();
        }

        #region webservice request    
        public Task<Result<LoginResult>> GetTokenAsync(string userName, string Password)
        {
            return Service.PostAsync<LoginResult>("token", new Dictionary<string, string>
            {
                {"UserName",userName },
                {"Password",Password },
                {"grant_type","password" }
            });
        }

        public Task<Result<string>> RegisterUserAsync(string firstName,string lastName,string userName,string phone, string Password)
        {
            return Service.PostAsync<string>("users", new Dictionary<string, string>
            {
                {"FirstName",firstName},
                {"LastName",lastName },
                {"UserName",userName },
                {"Password",Password },
                {"Phone",phone }
            });
        }
    
        public Task<Result<User>> GetMeAsync()
        {
            return Service.GetAsync<User>("users/me", null);
        }

        public Task<Result<List<User>>> GetUserAsync()
        {
            return Service.GetAsync<List<User>>("users", null);
        }

        public Task<Result<string>> RegisterGcmTokenAsync(string token)
        {
            return Service.PostAsync<string>("users/userToken", new Dictionary<string, string>
            {
                {"Token",token }
            });
        }

        public Task<Result<string>> SendMessageAsync(string Title,string message,int userId)
        {
            return Service.PostAsync<string>("users/"+userId+"/send", new Dictionary<string, string>
            {
                {"Title",Title },
                {"Message",message }
            });
        }

        #endregion
    }
}