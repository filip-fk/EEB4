using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace EEB4
{
    /// <summary>
    /// first use experience
    /// shown only once
    /// </summary>
    public sealed partial class FirstPage1 : Page
    {
        public FirstPage1()
        {
            this.InitializeComponent();

            //call for add content
            populate_con();
        }

        //adds content
        //sign in interface
        private void populate_con()
        {
            var seti = new UISettings();
            var accent = seti.GetColorValue(UIColorType.Accent);

            //new itemPane
            Grid main1 = new Grid();
            ScrollViewer scrollViewer = new ScrollViewer();
            StackPanel stack1 = new StackPanel { HorizontalAlignment=HorizontalAlignment.Stretch};
            TextBlock text1 = new TextBlock { TextWrapping = TextWrapping.WrapWholeWords, Text = "Welcome to the new school app. Here you can find everything you need to know. The app provides" +
                " a united experience combining both SMS and Office 356 school platforms.\nPlease sign in to your account to start" };
            Button button1 = new Button { Content = "Sign in", Background = new SolidColorBrush(accent), Foreground=new SolidColorBrush(Colors.White), HorizontalAlignment=HorizontalAlignment.Right, Margin=new Thickness(0,60,0,0) };
            button1.Click += Button1_Click;

            stack1.Children.Add(text1);
            stack1.Children.Add(button1);
            scrollViewer.Content = stack1;
            main1.Children.Add(scrollViewer);
            con.Children.Add(new ItemPane(400, 600, "Welcome to the EEB4 app", HorizontalAlignment.Center, main1, "", ""));
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            //start sihn in process
        }
    }
}
