using System;
using System.Collections.Generic;
using System.Text;
using ListViewPaginationDemo.Interface;
namespace ListViewPaginationDemo.Model
{
  public class UserData:IPageItem
    {
        public int PageItemId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
       public string Image { get; set; }
    }
}
