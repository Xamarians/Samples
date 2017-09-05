using Xamarin.Forms;

namespace ChatDemo.Models
{
    class UserMessage : BaseEntity
    {
        /// <summary>
        /// Message sender user id
        /// </summary>
        public int SenderId { get; set; }
        public string SenderName { get; set; }
        public int ReceiverId { get; set; }
        public string ReceiverName { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public string Image { get; set; }
        public bool isSend { get; set; }
        public bool IsIncoming { get; set; }

    }
}
