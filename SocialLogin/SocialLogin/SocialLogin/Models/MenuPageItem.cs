using System;

namespace SocialLogin.Models
{
    class MenuPageItem
    {
        public string Title { get; set; }
        public string IconSource { get; set; }
        public Type TargetType { get; internal set; }
    }
}
