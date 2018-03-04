using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    /// </summary>
    public sealed partial class MainPage : Page
    {
        DispatcherTimer Timer = new DispatcherTimer();
        
        public MainPage()
        {
            this.InitializeComponent();

            Home.IsSelected = true;

            DataContext = this;
            Timer.Tick += Timer_Tick;
            Timer.Interval = new TimeSpan(0, 0, 1);
            Timer.Start();

            ApplicationViewTitleBar formattableTitleBar = ApplicationView.GetForCurrentView().TitleBar;
            formattableTitleBar.ButtonBackgroundColor = Colors.Transparent;
            formattableTitleBar.ButtonForegroundColor = Colors.Black;
            formattableTitleBar.BackgroundColor = Colors.Transparent;
            CoreApplicationViewTitleBar coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            coreTitleBar.ExtendViewIntoTitleBar = true;
        }

        private void navV_slc(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.IsSettingsSelected)
            {
                contentFrame.Navigate(typeof(SettingsPage1));
            }

            else
            {
                var selectedItem = (NavigationViewItem)args.SelectedItem;
                string pageName = "EEB4." + ((string)selectedItem.Name) + "Page1";
                Type pageType = Type.GetType(pageName);
                contentFrame.Navigate(pageType);
            }
        }

        private void NavigationView_PaneOpened(NavigationView sender, object args)
        {
            Headings.Visibility = Visibility.Visible;
            Title.Margin = new Thickness(160, 14, 0, 0);
            Greeting.Margin = new Thickness(160, 43, 0, 0);
            News.Margin = new Thickness(0, 40, 0, 0);
            Mail.Margin = new Thickness(0, 40, 0, 0);
            //p_pic.Margin = new Thickness(0, 0, 20, 16);
            //p_pic.HorizontalAlignment = HorizontalAlignment.Right;
        }

        private void NavigationView_PaneClosed(NavigationView sender, object args)
        {
            Headings.Visibility = Visibility.Collapsed;
            Title.Margin = new Thickness(0, 14, 0, 0);
            Greeting.Margin = new Thickness(0, 43, 0, 0);
            News.Margin = new Thickness(0, 0, 0, 0);
            Mail.Margin = new Thickness(0, 0, 0, 0);
            //p_pic.Margin = new Thickness(15, 0, 20, 55);
            //p_pic.HorizontalAlignment = HorizontalAlignment.Left;
        }

        private void Timer_Tick(object sender, object e)
        {
            Date.Text = DateTime.Today.DayOfWeek + " " + DateTime.Today.Day + "/" + DateTime.Today.Month + "/" + DateTime.Today.Year + " | " + DateTime.Now.ToString("h:mm");
        }
    }
}
