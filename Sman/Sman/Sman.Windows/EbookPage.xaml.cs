using System;
using System.Diagnostics;
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

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace Sman
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class EbookPage : Page
    {
        public EbookPage()
        {
            this.InitializeComponent();
            List<Book> bookList = new List<Book>();
            for (int i = 0; i < 10; i++) {
                bookList.Add(new Book("Assets/book.jpg", "性的起源"));
            }
            this.MyBook.ItemsSource = bookList;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }
    }


    public class Book
    {
        public string src { get; set; }
        public string name { get; set; }

        public Book(string p1, string p2)
        {
            this.src = p1;
            this.name = p2;
        }
    }
}
