using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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
                Task.Delay(500);
                //show user info
                TextBlock name_ = new TextBlock { Text = "Name: " + name, FontWeight = FontWeights.Bold, FontSize = 20, Margin = new Thickness(-20, 10, 0, 0) };
                user_info.Children.Add(name_);
                TextBlock username_ = new TextBlock { Text = "Username: " + username, FontWeight = FontWeights.Bold, FontSize = 20, Margin = new Thickness(-20, 5, 0, 0) };
                user_info.Children.Add(username_);
                TextBlock pass_ = new TextBlock { Text = "Password: " + RePass(password), FontWeight = FontWeights.Bold, FontSize = 23, Margin = new Thickness(-20, 5, 0, 0) };
                user_info.Children.Add(pass_);
                TextBlock date_ = new TextBlock { Text = "Date added: " + date, FontWeight = FontWeights.Bold, FontSize = 20, Margin = new Thickness(-20, 5, 0, 0) };
                user_info.Children.Add(date_);
                Button button1_1 = new Button { Content = "Edit subjects", HorizontalAlignment = HorizontalAlignment.Left, Margin = new Thickness(-20, 60, 0, 0) };
                button1_1.Click += Button1_1_Click;
                user_info.Children.Add(button1_1);
                Button button2 = new Button { Content = "Sign out", Background = new SolidColorBrush(accent), Foreground = new SolidColorBrush(Colors.White), HorizontalAlignment = HorizontalAlignment.Right, Margin = new Thickness(0, -28, 0, 0) };
                button2.Click += Button2_Click;
                user_info.Children.Add(button2);
            }

            else
            {
                TextBlock text = new TextBlock { Text = "No user has signed in\nPlease sign in with your Office 365 (or SMS account)", FontWeight = FontWeights.Bold, FontSize = 20, Margin = new Thickness(0, 10, 0, 10) };
                user_info.Children.Add(text);

                Button button1 = new Button { Content = "Sign in", Background = new SolidColorBrush(accent), Foreground = new SolidColorBrush(Colors.White), HorizontalAlignment = HorizontalAlignment.Right, Margin = new Thickness(0, 60, 0, 0) };
                button1.Click += Button1_Click;
                user_info.Children.Add(button1);
            }
        }

        private string RePass(string password)
        {
            string result = "";
            foreach(char str in password)
            {
                result += "•";
            }
            return result;
        }

        private string newSubj;

        private void Button1_1_Click(object sender, RoutedEventArgs e)
        {
            //edit subjects
            Grid grid = new Grid { Height = 500, Width = 700, HorizontalAlignment = HorizontalAlignment.Center, Padding = new Thickness(10) };
            ScrollViewer scr = new ScrollViewer { Margin = new Thickness(0, 0, 0, 55) };
            Grid grid2 = new Grid { Margin = new Thickness(0, 0, 15, 0) };
            string str = "No subjects available";
            //try
            //{
            str = (localSettings.Values["Subjects"].ToString());
            newSubj = str;
            int l_count1 = -1; int l_count2 = -1;
            string[] toSPL = str.Split(new[] { ":!#123#!:" }, StringSplitOptions.RemoveEmptyEntries);
            
            foreach (string subj in toSPL)
            {
                string[] l_str = subj.Split(new[] { "^$" }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string s_el in l_str)
                {
                    if (s_el.First() == '1')
                    {
                        //name
                        if (!s_el.Contains("@"))
                        {
                            //text
                            l_count2++;
                            string s_el2 = s_el.Substring(3);

                            TextBlock text = new TextBlock { Text = s_el2, VerticalAlignment = VerticalAlignment.Center };
                            RowDefinition rowDefinition = new RowDefinition();
                            rowDefinition.Height = new GridLength(40);
                            grid2.RowDefinitions.Add(rowDefinition);
                            Grid.SetRow(text, l_count2);
                            grid2.Children.Add(text);
                        }
                    }
                    else if (s_el.First() == '4')
                    {
                        //colorif (subj.Contains("@"))
                        {
                            //color
                            l_count1++;
                            string s_el2 = s_el.Substring(3);
                            byte a = 255;
                            byte r = 255;
                            byte g = 255;
                            byte b = 255;

                            string[] toSPL2 = s_el2.Split('@', StringSplitOptions.RemoveEmptyEntries);
                            foreach (string byt in toSPL2)
                            {
                                if (byt.Contains("r"))
                                {
                                    r = Convert.ToByte(byt.Replace("r", string.Empty));
                                }
                                else if (byt.Contains("g"))
                                {
                                    g = Convert.ToByte(byt.Replace("g", string.Empty));
                                }
                                else if (byt.Contains("b"))
                                {
                                    b = Convert.ToByte(byt.Replace("b", string.Empty));
                                }
                            }

                            Grid mini = new Grid { Background = new SolidColorBrush(Color.FromArgb(a, r, g, b)), Height = 32, Width = 100, HorizontalAlignment = HorizontalAlignment.Right, VerticalAlignment = VerticalAlignment.Center };
                            mini.Tapped += Mini_Tapped;
                            RowDefinition rowDefinition = new RowDefinition();
                            rowDefinition.Height = new GridLength(40);
                            //grid2.RowDefinitions.Add(rowDefinition);
                            Grid.SetRow(mini, l_count1);
                            grid2.Children.Add(mini);
                        }
                    }
                }
            }
            //}
            //catch{ }

            var seti = new UISettings();
            var accent = seti.GetColorValue(UIColorType.Accent);

            Button button = new Button { Content = "Reload server default", VerticalAlignment = VerticalAlignment.Bottom };
            button.Click += Button_Click;
            Button button10 = new Button { Content = "Report a problem", VerticalAlignment = VerticalAlignment.Bottom, HorizontalAlignment = HorizontalAlignment.Left, Margin = new Thickness(180, 0, 0, 0) };
            button10.Click += Button10_Click;
            Button button11 = new Button { Content = "Close", VerticalAlignment = VerticalAlignment.Bottom, Background = new SolidColorBrush(accent), Foreground = new SolidColorBrush(Colors.White), HorizontalAlignment = HorizontalAlignment.Right };
            button11.Click += Button11_Click;

            scr.Content = grid2;
            grid.Children.Add(scr);
            grid.Children.Add(button);
            grid.Children.Add(button10);
            grid.Children.Add(button11);

            user_info.Children.Add(new ItemPane(600, 800, "Subjects", HorizontalAlignment.Center, grid, "", ""));
        }

        private void Button11_Click(object sender, RoutedEventArgs e)
        {
            //close item
            user_info.Children.RemoveAt(user_info.Children.Count() - 1);
        }

        private void Mini_Tapped(object sender, TappedRoutedEventArgs e)
        {
            //show color picker
            Grid mini = (Grid)(sender);
            Grid parent = (Grid)((ScrollViewer)((Grid)((mini).Parent)).Parent).Parent;
            ColorPicker cp = new ColorPicker { IsColorSliderVisible = true, IsColorChannelTextInputVisible = false, IsHexInputVisible = false, IsAlphaEnabled = false, IsAlphaSliderVisible = false, IsAlphaTextInputVisible = false };
            //cp.ColorChanged += new TypedEventHandler<ColorPicker, ColorChangedEventArgs>(cp,Cp_ColorChanged(cp,new ColorChangedEventArgs(),5)));
            Button btn = new Button { VerticalAlignment = VerticalAlignment.Bottom, HorizontalAlignment = HorizontalAlignment.Center, Content = "Done", Margin = new Thickness(0, 0, 0, 55), Width = cp.Width };
            btn.Click += (Sender, EventArgs) => { Cp_ColorChanged(Sender, EventArgs, mini, cp); };
            //cp.ColorChanged += (Sender, EventArgs) => { Cp_ColorChanged(Sender, EventArgs,(Grid)(sender)); };


            //check if another mini is active
            /*foreach (object g in ((Grid)(mini.Parent)).Children)
            {
                if (g.GetType() == typeof(Grid))
                {
                    if ((g as Grid).BorderThickness != new Thickness(0))
                    {
                        //a colorPicker is open
                        Control control = new ColorPicker();
                        if (parent.Children.Contains(control))
                        {
                            parent.Children.RemoveAt(3);
                        }
                    }
                }
            }
            int intTotalChildren = (parent).Children.Count - 1;
            for (int intCounter = intTotalChildren; intCounter > 0; intCounter--)
            {
                if (((Grid)(mini.Parent)).Children[intCounter].GetType() == typeof(ColorPicker))
                {
                    ColorPicker clrP = (ColorPicker)(parent).Children[intCounter];
                    (parent).Children.Remove(clrP);
                }

                if (((Grid)(mini.Parent)).Children[intCounter].GetType() == typeof(Button))
                {
                    Button btn2 = (Button)(parent).Children[intCounter];
                    if (btn2.Content == "Done")
                    {
                        (parent).Children.Remove(btn2);
                    }
                }
            }*/

            parent.Children.Add(cp);
            parent.Children.Add(btn);
            mini.BorderBrush = new SolidColorBrush(Colors.Gray); mini.BorderThickness = new Thickness(2);
        }

        private async void Cp_ColorChanged(object sender, RoutedEventArgs args, Grid i, ColorPicker cp)
        {
            //change color
            Grid parent = (Grid)((cp).Parent);
            //Grid mini = (Grid)((Grid)((ScrollViewer)(parent.Children.ElementAt(0))).Content).Children.ElementAt(1);

            SolidColorBrush br = i.Background as SolidColorBrush;
            Color c = br.Color;

            //change color code
            string str = newSubj;
            int j = 0;
            if (str.Contains($"{"@r" + c.R + "@g" + c.G + "@b" + c.B}"))
            {
                string[] toSPL = str.Split(new[] { ":!#123#!:" }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string subj in toSPL)
                {
                    if (subj.Contains("@"))
                    {
                        if (subj.Contains($"{"@r" + cp.Color.R + "@g" + cp.Color.G + "@b" + cp.Color.B}"))
                        {
                            //should return 0 if no matches
                            j++;
                        }
                    }
                }
                if (j > 0)
                {
                    //two subjects with identical color code
                    ContentDialog noWifiDialog = new ContentDialog
                    {
                        Title = "Error",
                        Content = "Two or more subjects with identical color.\n" +
                        "Please choose a different color.",
                        CloseButtonText = "Ok"
                    };
                    ContentDialogResult result = await noWifiDialog.ShowAsync();
                }
                else
                {
                    //continue
                    localSettings.Values["Subjects"] = str.Replace($"{"@r" + c.R + "@g" + c.G + "@b" + c.B}", $"{"@r" + cp.Color.R + "@g" + cp.Color.G + "@b" + cp.Color.B}");
                    newSubj = (localSettings.Values["Subjects"]).ToString();
                    i.Background = new SolidColorBrush(cp.Color);
                }
            }
            /*int l_count1 = 0;
    int rule = 0;
    string[] toSPL = str.Split(new[] { ":!#123#!:" }, StringSplitOptions.RemoveEmptyEntries);
    foreach (string subj in toSPL)
    {
        if (subj.Contains("@"))
        {
            //color
            byte a = 255;
            byte r = 255;
            byte g = 255;
            byte b = 255;

            string[] toSPL2 = subj.Split('@', StringSplitOptions.RemoveEmptyEntries);
            foreach (string byt in toSPL2)
            {
                if (byt.Contains("r"))
                {
                    string l = (byt.Replace("r", string.Empty));
                    if(l==c.R.ToString())
                    {
                        l_count1++;
                        rule++;
                    }
                }
                else if (byt.Contains("g"))
                {
                    string l = (byt.Replace("g", string.Empty));
                    if (l == c.G.ToString())
                    {
                        rule++;
                    }
                }
                else if (byt.Contains("b"))
                {
                    string l = (byt.Replace("b", string.Empty));
                    if (l == c.B.ToString())
                    {
                        rule++;
                    }
                }
            }
        }*/


            parent.Children.Remove(cp);
            parent.Children.Remove((Button)sender);
            i.BorderThickness = new Thickness(0);
        }

        private void Button10_Click(object sender, RoutedEventArgs e)
        {
            //report a problem
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //reload default values for subjects

            //sample reload
            Set_Subj();

            //replace pop-up item
            int child = user_info.Children.Count();
            user_info.Children.RemoveAt(child - 1);
            Button1_1_Click(sender as Button, new RoutedEventArgs());
        }

        private void Set_Subj()
        {
            //sample
            string final = "";
            int j = 13 + 1; //number of subj + 1 #FRR
            for (int i = 0; i < j + 1; i++)
            {
                final += Set_SN(i);
            }
            localSettings.Values["Subjects"] = final;

            #region try1
            /*"S4BIODEA" + "^$1$^" + ":!#123#!:" + "@r19@g57@b119" + ":!#123#!:" + 
                "S4CHIDEA" + ":!#123#!:" + "@r119@g19@b19" + ":!#123#!:" + 
                "S4EPM--C" + ":!#123#!:" + "@r0@g0@b0" + ":!#123#!:" + 
                "S4GEOEND" + ":!#123#!:" + "@r214@g153@b23" + ":!#123#!:" + 
                "S4HISEND" + ":!#123#!:" + "@r182@g214@b23" + ":!#123#!:" + 
                "S4ICT--A" + ":!#123#!:" + "@r17@g160@b60" + ":!#123#!:" + 
                "S4L1-DEA" + ":!#123#!:" + "@r6@g214@b203" + ":!#123#!:" + 
                "S4L2-ENB" + ":!#123#!:" + "@r49@g152@b216" + ":!#123#!:" + 
                "S4L3-FRB" + ":!#123#!:" + "@r94@g49@b216" + ":!#123#!:" + 
                "S4L4-ESB" + ":!#123#!:" + "@r158@g18@b226" + ":!#123#!:" + 
                "S4MA6DEA" + ":!#123#!:" + "@r226@g18@b164" + ":!#123#!:" + 
                "S4MORENC" + ":!#123#!:" + "@r255@g242@b0" + ":!#123#!:" + 
                "S4PHYDEA" + ":!#123#!:" + "@r6@g114@b12";*/
            #endregion
            //for real
            //foreach & encapsulate upon retreival
        }

        private string Set_SN(int i)
        {
            string result = "";

            switch (i)
            {
                case 0: //bio
                    result = Set_InS(Set_Name("S4", "bio", "dea"), "Stephanie SCHRÖDER", "1|8++B109&&5|2++B001", Set_ColorS(19, 57, 119));
                    break;
                case 1: //chi
                    result = Set_InS(Set_Name("S4", "chi", "dea"), "Ulrike SEMMLER", "4|2++B209&&4|9++B209", Set_ColorS(119, 19, 19));
                    break;
                case 2: //emp
                    result = Set_InS(Set_Name("S4", "epm", "--c"), "Heike SCHÖNAU", "3|5++GYM4&&5|4++GYM3", Set_ColorS(0, 0, 0));
                    break;
                case 3: //geo
                    result = Set_InS(Set_Name("S4", "geo", "end"), "Joan FARRELL", "2|4++A207&&5|6++A207", Set_ColorS(214, 153, 23));
                    break;
                case 4: //his
                    result = Set_InS(Set_Name("S4", "his", "end"), "April CAPILI", "4|1++W118&&5|8++W119", Set_ColorS(182, 214, 23));
                    break;
                case 5: //ict
                    result = Set_InS(Set_Name("S4", "ict", "--a"), "Jose Virgilio FRAGOSO", "1|1++W124&&1|2++W124", Set_ColorS(17, 160, 60));
                    break;
                case 6: //l1
                    result = Set_InS(Set_Name("S4", "l1", "-dea"), "Marco ZINSER", "1|5++W405&&2|1++W405&&4|3++W405&&4|4++W405", Set_ColorS(6, 21, 203));
                    break;
                case 7: //l2
                    result = Set_InS(Set_Name("S4", "l2", "-enb"), "Anthony HUDDERS", "2|7++W208&&4|6++W208&&5|7++W208", Set_ColorS(49, 152, 216));
                    break;
                case 8: //l3
                    result = Set_InS(Set_Name("S4", "l3", "-frb"), "Véronique HENRY", "2|2++A104&&4|5++A104&&5|3++A105", Set_ColorS(94, 49, 216));
                    break;
                case 9: //l4
                    result = Set_InS(Set_Name("S4", "l4", "-esb"), "Angela QUIROS COLUBI", "1|7++W319&&2|3++W319&&2|8++W319&&3|1++W319", Set_ColorS(15, 18, 226));
                    break;
                case 10: //ma6
                    result = Set_InS(Set_Name("S4", "ma6", "dea"), "Carsten HENKEL", "1|4++B201&&2|5++B201&&3|4++B201&&4|8++B201&&5|1++B201&&5|9++B201", Set_ColorS(226, 18, 164));
                    break;
                case 11: //mor
                    result = Set_InS(Set_Name("S4", "mor", "enc"), "Alexander CORNFORD", "1|9++W213", Set_ColorS(255, 24, 0));
                    break;
                case 12: //phy
                    result = Set_InS(Set_Name("S4", "phy", "dea"), "Carsten HENKEL", "1|3++B11&&3|2++B11", Set_ColorS(6, 114, 12));
                    break;
                case 13: //#FRR
                    result = Set_InS(Set_Name("", "#FRR", ""), "", "1|6++&&2|6++&&2|9++&&3|3++&&3|6++&&3|7++&&3|8++&&3|9++&&4|7++&&5|5++", Set_ColorS(0, 0, 0));
                    break;
            }

            return result;
        }

        private string Set_Name(string c_g, string c_n, string c_s)
        {
            string result = c_g + c_n.ToUpper() + c_s.ToUpper();
            return result;
        }

        private string Set_ColorS(int r, int g, int b)
        {
            string result = "@r" + r.ToString() + "@g" + g.ToString() + "@b" + b.ToString();
            return result;
        }

        private string Set_InS(string nam, string tea, string ld, string col)
        {
            string split_1 = "^$1$^"; //name  ***//split on ^$; check for 1$^ ... 2$^ 
            string split_2 = "^$2$^"; //teacher
            string split_3 = "^$3$^"; //location + time
            string split_4 = "^$4$^"; //color
            string split_5 = "^$5$^"; //note
            string split_m = ":!#123#!:";
            string result = split_1 + nam + split_2 + tea + split_3 + ld + split_4 + col + split_5 + split_m;
            return result;
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            //sign out
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            
            localSettings.Values["User"] = "";
            localSettings.Values["IsSignedIn"] = false.ToString();

            Frame.Navigate(typeof(FirstPage1));
        }

        public string type;
        public string username;
        public string name;
        public string password;
        public string date;

        ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
        StorageFolder localFolder = ApplicationData.Current.LocalFolder;

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
    }
}
