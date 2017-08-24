using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ChatDemo.Services
{
    interface IWebServices
    {
        Task<Result<T>> GetAsync<T>(string action, Dictionary<string, string> parameters);

        Task<Result<T>> PostAsync<T>(string action, Dictionary<string, string> parameters);

        Task<Result<T>> PutAsync<T>(string action, Dictionary<string, string> parameters);

        Task<Result<T>> DeleteAsync<T>(string action, Dictionary<string, string> parameters);

        Task<Result<T>> UploadFileAsync<T>(string action, string filePath);

        Task<Result<T>> UploadStatusOnLinkedIn<T>(string action, string text);

    }
}