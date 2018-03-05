using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Text;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace EEB4
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SettingsPage1 : Page
    {
        public SettingsPage1()
        {
            this.InitializeComponent();

            var seti = new UISettings();
            var accent = seti.GetColorValue(UIColorType.Accent);

            if (check_signIn() == true)
            {
                //show user info
                TextBlock text = new TextBlock { Text = "Username", FontWeight=FontWeights.Bold, FontSize=20, Margin=new Thickness(0,10,0,10) };
                user_info.Children.Add(text);

            }

            else
            {
                TextBlock text = new TextBlock { Text = "No user has signed in\nPlease sign in with your Office 365 (or SMS account)", FontWeight=FontWeights.Bold, FontSize=20, Margin=new Thickness(0,10,0,10) };
                user_info.Children.Add(text);

                Button button1 = new Button { Content = "Sign in", Background = new SolidColorBrush(accent), Foreground = new SolidColorBrush(Colors.White), HorizontalAlignment = HorizontalAlignment.Right, Margin = new Thickness(0, 60, 0, 0) };
                button1.Click += Button1_Click;
                user_info.Children.Add(button1);
            }
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(FirstPage1), "SignInClicked");
        }

        private Boolean check_signIn()
        {
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;

            //write to signedin
            //localSettings.Values["IsSignedIn"] = false.ToString();

            string signIn = localSettings.Values["IsSignedIn"].ToString();
            if (signIn == true.ToString())
            { return true; }
            else return false;
        }
    }
}
