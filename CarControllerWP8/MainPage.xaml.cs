using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using CarControllerWP8.Resources;

using System.IO;

using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Networking.Proximity;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;
using System.Text;
using Windows.UI.Popups;
using System.Windows.Input;


namespace CarControllerWP8
{
    public partial class MainPage : PhoneApplicationPage
    {
        StreamSocket BTSock = new StreamSocket();
        // Конструктор
        public MainPage()
        {
            InitializeComponent();

            // Пример кода для локализации ApplicationBar
            //BuildLocalizedApplicationBar();
        }
        private IBuffer GetBufferFromByteArray(byte[] package)
        {
            using (DataWriter dw = new DataWriter())
            {
                dw.WriteBytes(package);
                return dw.DetachBuffer();
            }
        }
        private async void Connect(object sender, RoutedEventArgs e)
        {
            string deviceName = "HC-06";
            PeerFinder.AlternateIdentities["Bluetooth:Paired"] = ""; // Grab Paired Devices
            var PF = await PeerFinder.FindAllPeersAsync(); // Store Paired Devices
 //           BTSock = new StreamSocket();

            for (int i = 0; i <= PF.Count; i++)
            {
                if (PF[i].DisplayName == deviceName)
                {
                    await BTSock.ConnectAsync(PF[i].HostName, "1");
                    break;
                }
            }
            var datab = GetBufferFromByteArray(Encoding.UTF8.GetBytes("5")); // Create Buffer/Packet for Sending
           
                await BTSock.OutputStream.WriteAsync(datab); // Send
        }

        //public async void CommandSend(string command)
        //{


        //    var datab = GetBufferFromByteArray(Encoding.UTF8.GetBytes(command)); \
        //    var i = BTSock.Information;
        //    var i1 = BTSock.OutputStream;
        //    var i2 = BTSock.Control;
        //    var i3 = BTSock.InputStream;

        //    bool x = await BTSock.OutputStream.FlushAsync();
        //    try
        //    {
        //        await BTSock.OutputStream.WriteAsync(datab); // Send
        //    }
        //    catch (Exception ex)
        //    {
        //        //            MessageBox.Show("Exception on sending : {0}", "Error", MessageBoxButton.OK, ex);
        //     //   MessageDialog msgbox = new MessageDialog("Exception on sending : " + ex);
        //        Console.Write("Exception on sending : " + ex);
        //    }
        //}

 //       private async void Up_Click(object sender, RoutedEventArgs e)
 //       {
 //           string command = "8";
 ////           MainPage O = new MainPage();
 ////           O.CommandSend(command);
 //           var datab = GetBufferFromByteArray(Encoding.UTF8.GetBytes(command));
 //           await BTSock.OutputStream.WriteAsync(datab);
 //       }

        //private async void Down_Click(object sender, RoutedEventArgs e)
        //{
        //    string command = "2";
        //    //MainPage O = new MainPage();
        //    //O.CommandSend(command);
        //    var datab = GetBufferFromByteArray(Encoding.UTF8.GetBytes(command));
        //    await BTSock.OutputStream.WriteAsync(datab);
        //}

        //private async void Left_Click(object sender, RoutedEventArgs e)
        //{
        //    string command = "4";
        ////    MainPage O = new MainPage();
        ////    O.CommandSend(command);
        //    var datab = GetBufferFromByteArray(Encoding.UTF8.GetBytes(command));
        //    await BTSock.OutputStream.WriteAsync(datab);
        //}

        //private async void Right_Click(object sender, RoutedEventArgs e)
        //{
        //    string command = "6";
        //    //MainPage O = new MainPage();
        //    //O.CommandSend(command);
        //    var datab = GetBufferFromByteArray(Encoding.UTF8.GetBytes(command));
        //    await BTSock.OutputStream.WriteAsync(datab);
        //}


        private async void GetData(object sender, RoutedEventArgs e)
        {
            DataReader dataReader = new DataReader(BTSock.InputStream);
            await dataReader.LoadAsync(4);// get the size of message
            uint messageLen =   (uint)dataReader.ReadInt16();
            await dataReader.LoadAsync(messageLen);//send the message
            String Message = dataReader.ReadString(messageLen);
            D.Text = Message.ToString();
        }


        private async void StopSignal(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            string command = "0";
            //MainPage O = new MainPage();
            //O.CommandSend(command);
            var datab = GetBufferFromByteArray(Encoding.UTF8.GetBytes(command));
            await BTSock.OutputStream.WriteAsync(datab);
        }



        private async void Down_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            string command = "2";
            //           MainPage O = new MainPage();
            //           O.CommandSend(command);
            var datab = GetBufferFromByteArray(Encoding.UTF8.GetBytes(command));
            await BTSock.OutputStream.WriteAsync(datab);
        }

        private async void Left_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            string command = "6";
            //    MainPage O = new MainPage();
            //    O.CommandSend(command);
            var datab = GetBufferFromByteArray(Encoding.UTF8.GetBytes(command));
            await BTSock.OutputStream.WriteAsync(datab);
        }

        private async void Right_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            string command = "4";
            //    MainPage O = new MainPage();
            //    O.CommandSend(command);
            var datab = GetBufferFromByteArray(Encoding.UTF8.GetBytes(command));
            await BTSock.OutputStream.WriteAsync(datab);
        }

        private async void Up_Click(object sender, MouseButtonEventArgs e)
        {
            string command = "8";
            //           MainPage O = new MainPage();
            //           O.CommandSend(command);
            var datab = GetBufferFromByteArray(Encoding.UTF8.GetBytes(command));
            await BTSock.OutputStream.WriteAsync(datab);
        }
        // Пример кода для построения локализованной панели ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Установка в качестве ApplicationBar страницы нового экземпляра ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Создание новой кнопки и установка текстового значения равным локализованной строке из AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Создание нового пункта меню с локализованной строкой из AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}