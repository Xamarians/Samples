using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ChatDemo.Controls
{
    class ExtendedListView : ListView
    {
        public int PageCount = 10;
        //int pageIndex = 1;
        //int startIndex = 1;
        //public event EventHandler<PageEventArgs> PageChanged;
        public bool EnablePagination
        {
            get { return (bool)GetValue(EnablePaginationProperty); }
            set { SetValue(EnablePaginationProperty, value); }
        }
        public static BindableProperty EnablePaginationProperty = BindableProperty.Create("EnablePagination", typeof(bool), typeof(ExtendedListView), false, propertyChanged: (obj, oval, nval) =>
        {
            (obj as ExtendedListView).OnEnablePaginationPropertyChanged();
        });

        public ExtendedListView()
        {
            PageCount = 10;
            Refreshing += ExtendedListView_Refreshing;

        }

        private void ExtendedListView_Refreshing(object sender, EventArgs e)
        {
            IsRefreshing = false;
        }

        private void OnEnablePaginationPropertyChanged()
        {
            ItemAppearing -= OnAppearing;
            ItemAppearing += OnAppearing;
        }
        private void OnAppearing(object sender, ItemVisibilityEventArgs e)
        {
            //if (PageCount <= 0)
            //    return;
            //var _iPageItem = e.Item as Interface.IPageItem;
            //if (_iPageItem == null)
            //    return;
            //if (_iPageItem.PageItemId == 0)
            //{
            //    _iPageItem.PageItemId = startIndex++;
            //    if (_iPageItem.PageItemId == (PageCount * (pageIndex)) - 2)
            //    {
            //        PageChanged?.Invoke(this, new PageEventArgs(pageIndex));
            //        pageIndex++;
            //    }
            //}
        }
    }

    public class PageEventArgs : EventArgs
    {
        public int PageIndex { get; set; }

        public PageEventArgs(int pageIndex)
        {
            PageIndex = pageIndex;
        }
    }
}

