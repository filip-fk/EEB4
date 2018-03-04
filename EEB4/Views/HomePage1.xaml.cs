using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
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
    public sealed partial class HomePage1 : Page
    {
        public HomePage1()
        {
            this.InitializeComponent();
            load_items();
        }

        private void load_items()
        {
            int j = 4;
            for (int i = 0; i < j; i++)
            {
                Grid local = new Grid { Height = 280, Width = 480 };

                Grid con = new Grid { };
                TextBlock textBlock1 = new TextBlock { Text = "BUS 328 has a delay of over 25 minutes due to bad weather conditions", Foreground = new SolidColorBrush(Colors.Gray), TextWrapping = TextWrapping.WrapWholeWords };
                con.Children.Add(textBlock1);
                local.Children.Add(new ItemPane(250, 450, "BUS 328 DELAYED 25 MINUTES", HorizontalAlignment.Left, con, "Info from Transport", "Track on map"));

                VariableSizedWrapGrid.SetRowSpan(local, 3);
                VariableSizedWrapGrid.SetColumnSpan(local, 5);

                Grid local2 = new Grid { Height = 580, Width = 380 };

                Grid con2 = new Grid { };
                TextBlock textBlock2 = new TextBlock { Text = "BUS 328 has a delay of over 25 minutes due to bad weather conditions", Foreground = new SolidColorBrush(Colors.Gray), TextWrapping = TextWrapping.WrapWholeWords };
                con2.Children.Add(textBlock2);
                local2.Children.Add(new ItemPane(550, 350, "BUS 328 DELAYED 25 MINUTES", HorizontalAlignment.Left, con2, "Info from Transport", "Track on map"));

                VariableSizedWrapGrid.SetRowSpan(local2, 6);
                VariableSizedWrapGrid.SetColumnSpan(local2, 4);

                main10.Children.Add(local); main10.Children.Add(local2);
            }
        }
    }
}
