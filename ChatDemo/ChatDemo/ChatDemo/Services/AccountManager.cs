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

        public Task<Result<string>> GetUserID(string token,string userName,string Name)
        {
            return Service.PostAsync<string>("user", new Dictionary<string, string>
            {
                {"Name",Name },
                {"UserName",userName },
                {"Token",token }
            });
        }

        public Task<Result<List<string>>> GetUserList()
        {
            return Service.GetAsync<List<string>>("user", new Dictionary<string, string> { });
        }

        public Task<Result<string>> SendMessage(string message,string FromUserName,string ToUserName)
        {
            return Service.PostAsync<string>("push", new Dictionary<string, string>
            {
                 {"FromUserName",FromUserName },
                {"ToUserName",ToUserName },
                {"Message",message }
            });
        }

        #endregion
    }
}