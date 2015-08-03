using System;
using System.Collections.Generic;
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

namespace WPFApp
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

        private async void  Button_Click(object sender, RoutedEventArgs e)
        {
            var str0 = "Main thread :" + Thread.CurrentThread.ManagedThreadId + @"\n";
           var str1=  await MyTask1();
           var str2 = await MyTask2();
           var str3 = LongRunningTask("Task3",10000);
            TextBox1.Text = str0+ str1 + str2 + str3;
        }

        private async Task<string> MyTask1()
        {
            return await Task.Factory.StartNew<string>(()=>LongRunningTask("Task1", 10000));
        }

        private async Task<string> MyTask2()
        {
            return await Task.Factory.StartNew<string>(() => LongRunningTask("Task2",10000));
        }

        

        private string LongRunningTask(string name,int delay)
        {

            var message = "Task: " + name + @"\n"; 
            message    += "StartTime: "+ DateTime.Now.ToLongTimeString()+@"\n" ;
            Task.Delay(delay).Wait();
            message += " End Time: " + DateTime.Now.ToLongTimeString() + @"\n";
            message += " Thread: " + Thread.CurrentThread.ManagedThreadId+ @"\n"; 
            return message;
        }

        //the same could happen for api controller which context is as different as this one UI thread
        private async void DeadLock_Click(object sender, RoutedEventArgs e)
        {
            var jsonTask = GetJsonAsync(new Uri("http://www.google.com"));
            // this is blocking ui
            TextBox1.Text = jsonTask.Result;
            
            /**
            // this is right way
            var jsonString = await GetJsonAsync(new Uri("http://www.google.com"));
            TextBox1.Text = jsonString;
            **/
        }

        // This needs to come back on calling thread 
        public static async Task<string> GetJsonAsync(Uri uri)
        {
            using (var client = new HttpClient())
            {
               return  await client.GetStringAsync(uri);
              
            }
        }
    }
}
