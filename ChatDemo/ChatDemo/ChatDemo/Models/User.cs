using ChatDemo.Data;
using System;
using SQLite;
using Newtonsoft.Json;

namespace ChatDemo.Models
{
    public class BaseEntity : IEntity
    {
        [JsonIgnore]
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public DateTime CreatedOn
        {
            get; set;
        } = DateTime.Now;

        public DateTime UpdatedOn
        {
            get; set;
        } = DateTime.Now;

        public BaseEntity()
        {
            CreatedOn = DateTime.UtcNow;
            UpdatedOn = DateTime.UtcNow;
        }
    }

    public class User : BaseEntity
    {
        [JsonProperty("Id")]
        public int UserId { get; set; }
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }

        public string GetFullName()
        {
            return $"{FirstName} {LastName}".Trim();
        }
    }
}
