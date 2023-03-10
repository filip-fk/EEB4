using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace EEB4
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// the mainPage displays a welcome screen or first view for signed in users
    /// </summary>
    public sealed partial class MainPage : Page
    {
        DispatcherTimer Timer = new DispatcherTimer();
        
        public MainPage()
        {
            this.InitializeComponent();

            //close pane
            navVS.IsPaneOpen = false;

            //check sihn in
            check_sign_in();

            //start date/time
            DataContext = this;
            Timer.Tick += Timer_Tick;
            Timer.Interval = new TimeSpan(0, 0, 1);
            Timer.Start();

            //fill screen desktop
            ApplicationViewTitleBar formattableTitleBar = ApplicationView.GetForCurrentView().TitleBar;
            formattableTitleBar.ButtonBackgroundColor = Colors.Transparent;
            formattableTitleBar.ButtonForegroundColor = Colors.Black;
            formattableTitleBar.BackgroundColor = Colors.Transparent;
            CoreApplicationViewTitleBar coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            coreTitleBar.ExtendViewIntoTitleBar = true;

            //close pane
            navVS.IsPaneOpen = false;
        }

        private void check_sign_in()
        {
            if (check_signIn() == true)
            {
                Task.Delay(500);
                Hi.Text += " " + name+ "!";
                Home.IsSelected = true;
                contentFrame.Navigate(typeof(HomePage1));
            }
            else
            {
                Hi.Text += " " + "User!";

                foreach (NavigationViewItem item in navVS.MenuItems)
                {
                    if(item != navVS.SettingsItem)
                    {
                        if (item.Name != "Home")
                        {
                            item.Visibility = Visibility.Collapsed;
                        }

                        else
                        {
                            //it is home item
                            item.IsEnabled = false;
                        }
                    }
                }
                contentFrame.Navigate(typeof(FirstPage1));
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                try
                {
                    if (Convert.ToInt32(e.Parameter) == 10)
                    {
                        InitializeComponent();
                        this.check_sign_in();
                    }
                }
                catch { }
            }
        }

        public string type;
        public string username;
        public string name;
        public string password;
        public string date;

        //check whether user signed in
        private Boolean check_signIn()
        {
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;

            //write to signedin
            //localSettings.Values["IsSignedIn"] = false.ToString();

            string signIn = localSettings.Values["IsSignedIn"].ToString();
            if (signIn == true.ToString())
            {
                //get user info
                int j = 0;
                string userInfo = localSettings.Values["User"].ToString();
                string[] userInfoSplit = userInfo.Split(new string[] { ":!#123#!:" }, StringSplitOptions.None);
                foreach (string userInfoValue in userInfoSplit)
                {
                    j++;

                    if (j == 1)
                    {
                        //type
                        type = userInfoValue;
                    }
                    if (j == 2)
                    {
                        //username
                        username = userInfoValue;
                    }
                    if (j == 3)
                    {
                        //password
                        password = userInfoValue;
                    }
                    if (j == 4)
                    {
                        //name
                        name = userInfoValue;
                    }
                    if (j == 5)
                    {
                        //date
                        date = userInfoValue;
                    }
                }
                return true;
            }
            else return false;
        }

        //handle selection changed in navigation view
        //redirect to pages
        private void navV_slc(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            //settings
            if (args.IsSettingsSelected)
            {
                contentFrame.Navigate(typeof(SettingsPage1));
            }

            //all other
            else
            {
                var selectedItem = (NavigationViewItem)args.SelectedItem;
                string pageName = "EEB4." + ((string)selectedItem.Name) + "Page1";
                Type pageType = Type.GetType(pageName);
                contentFrame.Navigate(pageType);
            }
        }

        //pane opened is not supported on builds before 17095
        private void NavigationView_PaneOpened(NavigationView sender, object args)
        {/*
            Title.Margin = new Thickness(160, 14, 0, 0);
            Greeting.Margin = new Thickness(160, 43, 0, 0);*/
        }

        //pane closed is not supported on builds before 17095
        private void NavigationView_PaneClosed(NavigationView sender, object args)
        {/*
            Title.Margin = new Thickness(0, 14, 0, 0);
            Greeting.Margin = new Thickness(0, 43, 0, 0);
            //p_pic.Margin = new Thickness(15, 0, 20, 55);
            //p_pic.HorizontalAlignment = HorizontalAlignment.Left;*/
        }

        //set greeting
        private void Timer_Tick(object sender, object e)
        {
            Date.Text = DateTime.Today.DayOfWeek + " " + DateTime.Today.Day + "/" + DateTime.Today.Month + "/" + DateTime.Today.Year + 
                " | " + DateTime.Now.ToString("h:mm");
        }
    }
}
