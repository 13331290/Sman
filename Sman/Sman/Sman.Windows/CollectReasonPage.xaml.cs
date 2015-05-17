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
    public sealed partial class CollectReasonPage : Page
    {

        private CollectInfo collectInfo;
        public CollectReasonPage()
        {
            this.InitializeComponent();
            
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            //base.OnNavigatedTo(e);
            collectInfo = (CollectInfo)e.Parameter;
            StorageFile file = collectInfo.getFile();
            filename.Text = file.Name;
            if (collectInfo.getType().Equals("picture"))
            {
                WriteableBitmap writeAbleBitmap = new WriteableBitmap(200, 200);
                IRandomAccessStream stream = await file.OpenAsync(FileAccessMode.Read);
                await writeAbleBitmap.SetSourceAsync(stream);
                image.Source = writeAbleBitmap;
            }
            else if (collectInfo.getType().Equals("music"))
            {
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.UriSource = new Uri("ms-appx://Assets/audio.png");
                image.Source = bitmapImage;
            }
            else if (collectInfo.getType().Equals("video"))
            {
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.UriSource = new Uri("ms-appx://Assets/audio.png");
                image.Source = bitmapImage;
            }
            else
            {
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.UriSource = new Uri("ms-appx://Assets/pdf.png");
                image.Source = bitmapImage;
            }
        }

        private void ensure_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
