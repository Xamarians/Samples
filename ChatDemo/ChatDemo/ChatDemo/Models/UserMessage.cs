namespace ChatDemo.Models
{
    class UserMessage : BaseEntity
    {
        public int ToUserId { get; set; }
        public string ToUserName { get; set; }
        public string FromUserName { get; set; }
        public string Content { get; set; }
        public bool IsIncoming { get;  set; }
    }
}
