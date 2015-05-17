using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace Sman
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class CollectPage : Page
    {
        public CollectPage()
        {
            this.InitializeComponent();
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private void music_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void picture_Click(object sender, RoutedEventArgs e)
        {
            WriteableBitmap writeAbleBitmap = new WriteableBitmap(200, 200);
            FileOpenPicker picker = new FileOpenPicker();
            picker.FileTypeFilter.Add(".png");
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".jpeg");
            picker.FileTypeFilter.Add(".bmp");
            picker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;

            StorageFile file = await picker.PickSingleFileAsync();

            if (file != null)
            {
                CollectInfo collectInfo = new CollectInfo("picture", file);
                this.Frame.Navigate(typeof(CollectReasonPage), collectInfo);

            }
        }

        private void video_Click(object sender, RoutedEventArgs e)
        {

        }

        private void book_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
