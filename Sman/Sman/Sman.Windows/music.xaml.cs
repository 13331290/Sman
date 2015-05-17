using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Popups;
using Windows.ApplicationModel.DataTransfer;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.Graphics.Imaging;
using Windows.UI.Xaml.Media.Imaging;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace Sman
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class music : Page
    {
        private int music_url_num = 4;
        private string[] music_url = new String[100];
        private string[] music_title = new String[100];
        private IRandomAccessStream[] musci_stream = new IRandomAccessStream[100];
        public music()
        {
            this.InitializeComponent();

            music_url[0] = "Assets/music/李荣浩 - 笑忘书(Live).mp3";
            music_url[1] = "Assets/music/陈奕迅 - 无条件.mp3";
            music_url[2] = "Assets/music/张学友 - 你最珍贵.mp3";
            music_url[3] = "Assets/music/John Legend - All Of Me.mp3";

            music_title[0] = "笑忘书 - 李荣浩";
            music_title[1] = "陈奕迅 - 无条件";
            music_title[2] = "你最珍贵 - 张学友";
            music_title[3] = "All Of Me - John Legend";

            for (int i = 0; i < 4; i++)
            {
                music_list.Items.Add(music_title[i]);
            }
            RegisterForShare();
        }

        private void RegisterForShare()
        {
            DataTransferManager dataTransferManager = DataTransferManager.GetForCurrentView();
            dataTransferManager.DataRequested += new TypedEventHandler<DataTransferManager,
                DataRequestedEventArgs>(this.ShareStorageItemsHandler);
        }

        private async void ShareStorageItemsHandler(DataTransferManager sender,
            DataRequestedEventArgs e)
        {
            DataRequest request = e.Request;
            request.Data.Properties.Title = "Music";
            request.Data.Properties.Description = "Keep it";

            DataRequestDeferral deferral = request.GetDeferral();

            try
            {
                string current_music = music_element.Source.ToString();
                for (var i = 0; i < music_url_num; i++)
                {
                    if (current_music.Contains(music_url[i]))
                    {
                        StorageFile logoFile =
                            await Package.Current.InstalledLocation.GetFileAsync(music_url[i].Replace("/", "\\"));
                        List<IStorageItem> storageItems = new List<IStorageItem>();
                        storageItems.Add(logoFile);
                        request.Data.SetStorageItems(storageItems);
                        break;
                    }
                }
            }
            finally
            {
                deferral.Complete();
            }
        }

        private void stop_Click(object sender, RoutedEventArgs e)
        {
            music_element.Stop();
            reset_pause();
        }

        private void play_pause_Click(object sender, RoutedEventArgs e)
        {
            if ((string)play_pause.Label == "Play")
            {
                reset_play();
            }
            else
            {
                reset_pause();
            }
        }

        private void share_Click(object sender, RoutedEventArgs e)
        {
            DataTransferManager.ShowShareUI();
        }

        private void page2_Click(object sender, RoutedEventArgs e)
        {
            ///Frame.Navigate(typeof(BlankPage1));
        }

        private void next_Click(object sender, RoutedEventArgs e)
        {
            music_list.SelectedIndex = (music_list.SelectedIndex + 1) % music_url_num;
            /*
            string current_music = music_element.Source.ToString();
            for (var i = 0; i < music_url_num; i++)
            {
                if (current_music.Contains(music_url[i]))
                {
                    music_element.Source = new Uri("ms-appx:///" + music_url[(i + 1) % music_url_num]);
                    music_list.SelectedIndex = (i + 1) % music_url_num;
                    ///music_element.PosterSource = new BitmapImage(new Uri("ms-appx:///" + music_poster[(i + 1) % music_url_num]));
                    reset_play();
                    break;
                }
            }
            */
        }

        private void previous_Click(object sender, RoutedEventArgs e)
        {
            music_list.SelectedIndex = (music_list.SelectedIndex - 1 + music_url_num) % music_url_num;

            /*
            string current_music = music_element.Source.ToString();
            for (var i = 0; i < music_url_num; i++)
            {
                if (current_music.Contains(music_url[i]))
                {
                    music_element.Source = new Uri("ms-appx:///" + music_url[(i - 1 + music_url_num) % music_url_num]);
                    music_list.SelectedIndex = (i - 1 + music_url_num) % music_url_num;
                    ///music_element.PosterSource = new BitmapImage(new Uri("ms-appx:///" + music_poster[(i - 1 + music_url_num) % music_url_num]));
                    reset_play();
                    break;
                }
            }
            */
        }

        private void reset_play()
        {
            play_pause.Icon = new SymbolIcon(Symbol.Pause);
            play_pause.Label = "Pause";
            music_element.Play();
        }

        private void reset_pause()
        {
            play_pause.Icon = new SymbolIcon(Symbol.Play);
            play_pause.Label = "Play";
            music_element.Pause();
        }

        private void MenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
            MenuFlyoutItem selectedItem = sender as MenuFlyoutItem;

            if (selectedItem != null)
            {
                if (selectedItem.Tag.ToString() == "100")
                {
                    music_element.Volume = 1;
                }
                else if (selectedItem.Tag.ToString() == "50")
                {
                    music_element.Volume = 0.5;
                }
                else if (selectedItem.Tag.ToString() == "0")
                {
                    music_element.Volume = 0;
                }
            }
        }

        private void repeat_Click(object sender, RoutedEventArgs e)
        {
            if (music_element.IsLooping)
            {
                music_element.IsLooping = false;
            }
            else
            {
                music_element.IsLooping = true;
            }
        }

        private void music_list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = music_list.SelectedIndex;
            if (index < 4)
            {
                music_element.Source = new Uri("ms-appx:///" + music_url[music_list.SelectedIndex]);
            }
            else
            {
                string type = music_url[index].Substring(music_url[index].IndexOf("."));
                music_element.SetSource(musci_stream[index], type);
            }
            reset_play();
        }

        private async void Open_Click(object sender, RoutedEventArgs e)
        {
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.SuggestedStartLocation = PickerLocationId.MusicLibrary;
            openPicker.FileTypeFilter.Add(".mp3");
            openPicker.FileTypeFilter.Add(".mp4");
            openPicker.FileTypeFilter.Add(".mov");

            StorageFile file = await openPicker.PickSingleFileAsync();
            if (file != null)
            {
                musci_stream[music_url_num] = await file.OpenAsync(FileAccessMode.Read);
                ///IRandomAccessStream stream = await file.OpenAsync(FileAccessMode.Read);
                music_element.SetSource(musci_stream[music_url_num], ".mp3");

                music_url[music_url_num] = "Assets/music/" + file.Name;
                string new_music_title = file.Name.Substring(0, file.Name.IndexOf("."));
                music_list.Items.Add(new_music_title);
                music_list.SelectedIndex = music_url_num;
                music_title[music_url_num] = new_music_title;
                music_url_num++;
            }
            /*
            FileSavePicker savePicker = new FileSavePicker();
            savePicker.SuggestedStartLocation = PickerLocationId.MusicLibrary;
            savePicker.FileTypeChoices.Add("mp3", new List<string>() { ".mp3" });
            savePicker.FileTypeChoices.Add("mp4", new List<string>() { ".mp4" });
            savePicker.FileTypeChoices.Add("mov", new List<string>() { ".mov" });

            StorageFile file2 = await savePicker.PickSaveFileAsync();
            music_url[music_url_num] = "Assets/music/" + file2.Name;
            string new_music_title = file2.Name.Substring(0, file2.Name.IndexOf("."));
            music_list.Items.Add(new_music_title);
            music_list.SelectedIndex = music_url_num;
            music_title[music_url_num++] = new_music_title;
            if (file2 != null)
            {
                CachedFileManager.DeferUpdates(file2);
                await FileIO.WriteTextAsync(file2, file2.Name);
                await CachedFileManager.CompleteUpdatesAsync(file2);
                music_element.Source = new Uri("ms-appx:///" + music_url[music_url_num - 1]);
            }
            */
        }

    }
}
