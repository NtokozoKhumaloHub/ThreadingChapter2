using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ThreadingChapter2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void myButton_Click(object sender, RoutedEventArgs e)
        {
            Task.Run(() =>
            {
                Debug.WriteLine($"Thread Number: {Thread.CurrentThread.ManagedThreadId}");
                HttpClient webClient = new HttpClient();
                string HtmlLink = webClient.GetStringAsync("http://www.Google.com").Result;
                myButton.Dispatcher.Invoke(() =>
                {
                    myButton.Content = "Done";
                });

            });
        }

        private async void myButton_Click2(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine($"Thread Number: {Thread.CurrentThread.ManagedThreadId} before await");
            await Task.Run( async () =>
            {
                Debug.WriteLine($"Thread Number: {Thread.CurrentThread.ManagedThreadId} during await");
                HttpClient webClient = new HttpClient();
                string HtmlLink = webClient.GetStringAsync("http://www.Google.com").Result;
                
            });

            Debug.WriteLine($"Thread Number: {Thread.CurrentThread.ManagedThreadId} after await");

            myButton.Dispatcher.Invoke(() =>
            {
                myButton.Content = "Done Downloading";
            });
        }
    }
}
