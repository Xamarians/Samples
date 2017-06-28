using System;
using System.Collections.Generic;
using System.Text;
using CompressedVideoDemo.Interface;
using Xamarin.Forms;

namespace CompressedVideoDemo.DS
{
   public static class DependencyServices
    {
       public static IMobileFeature mobileFeature
        {
            get
            {
                return DependencyService.Get<IMobileFeature>(DependencyFetchTarget.GlobalInstance);
            }
        }

    }
}
