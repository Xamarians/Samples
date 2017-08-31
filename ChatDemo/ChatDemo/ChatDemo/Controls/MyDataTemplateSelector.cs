using ChatDemo.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ChatDemo.Controls
{
    class MyDataTemplateSelector:DataTemplateSelector
    {
        public MyDataTemplateSelector()
        {
            this.incomingDataTemplate = new DataTemplate(typeof(IncomingViewCell));
            this.outgoingDataTemplate = new DataTemplate(typeof(OutGoingViewCell));
            
        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var messageVm = item as Models.UserMessage;
            if (messageVm == null)
                return null;
            return messageVm.IsIncoming ? this.incomingDataTemplate : this.outgoingDataTemplate;
        }

        private readonly DataTemplate incomingDataTemplate;
        private readonly DataTemplate outgoingDataTemplate;
    }
}
