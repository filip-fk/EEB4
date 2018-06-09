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
using Windows.UI.Composition;
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
    /// first use experience
    /// shown only once
    /// </summary>
    public sealed partial class FirstPage1 : Page
    {
        public FirstPage1()
        {
            this.InitializeComponent();

            //call for add content
            add1(false);

            timer.Interval = new TimeSpan(0, 0, 0, 0, 250);
            timer.Tick += Timer_Tick;
        }

        public string type;
        public string username;
        public string name;
        public string password;
        public string date;
        DispatcherTimer timer = new DispatcherTimer();

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string _entityId = e.Parameter as string;

            if (_entityId == "SignInClicked")
            {
                add1(true);
            }
        }

        //adds content
        //sign in interface Stage1
        //welcome
        private void add1(bool execute)
        {
            var seti = new UISettings();
            var accent = seti.GetColorValue(UIColorType.Accent);

            //add margin
            Grid con01 = new Grid { Margin = new Thickness(10, -30, 10, 10) };

            //new itemPane
            Grid main1 = new Grid();
            ScrollViewer scrollViewer = new ScrollViewer { Padding = new Thickness(0, 0, 15, 0), VerticalScrollBarVisibility = ScrollBarVisibility.Hidden };
            StackPanel stack1 = new StackPanel { HorizontalAlignment = HorizontalAlignment.Stretch };
            TextBlock text1 = new TextBlock
            {
                TextWrapping = TextWrapping.WrapWholeWords, Foreground = new SolidColorBrush(Color.FromArgb(255, 118, 113, 113)),
                Text = "Welcome to the new school app. Here you can find everything you need to know. The app provides" +
                " a united experience combining both SMS and Office 356 school platforms.\nPlease sign in to your account to start"
            };
            Button button1 = new Button { Content = "Start", Width = 80, Background = new SolidColorBrush(accent), Foreground = new SolidColorBrush(Colors.White), HorizontalAlignment = HorizontalAlignment.Right, Margin = new Thickness(0, 60, 0, 0) };
            button1.Click += Button1_Click;

            if (execute == true)
            {
                Button1_Click(button1, new RoutedEventArgs());
            }

            stack1.Children.Add(text1);
            stack1.Children.Add(button1);
            scrollViewer.Content = stack1;
            main1.Children.Add(scrollViewer);
            con01.Children.Add(new ItemPane(400, 600, "Welcome to the EEB4 app", HorizontalAlignment.Center, main1, "", ""));

            con.Children.Add(con01);
        }

        //adds content
        //sign in interface Stage2
        //privacy notice
        private void add2(bool execute)
        {
            var seti = new UISettings();
            var accent = seti.GetColorValue(UIColorType.Accent);

            //add margin
            Grid con01 = new Grid { Margin = new Thickness(10) };

            //new itemPane
            Grid main1 = new Grid();
            ScrollViewer scrollViewer = new ScrollViewer { Padding = new Thickness(0, 0, 15, 0) };
            StackPanel stack1 = new StackPanel { HorizontalAlignment = HorizontalAlignment.Stretch };
            TextBlock text1 = new TextBlock
            {
                TextWrapping = TextWrapping.WrapWholeWords,
                FontWeight = FontWeights.Bold,
                Foreground = new SolidColorBrush(Color.FromArgb(255, 118, 113, 113)),
                Text = "Privacy notice"
            };

            TextBlock text2 = new TextBlock
            {
                TextWrapping = TextWrapping.WrapWholeWords,
                Margin = new Thickness(5,20,0,0),
                Foreground = new SolidColorBrush(Color.FromArgb(255, 59, 56, 56)),
                Text = "None of your information will ever be viewed by/shared with anyone unless you explicitly provide permission to do so."+
"\nNo emails will be sent without your acknowledgment."+
"\nNo events / appointments will be added to / removed from your calendar."
            };

            StackPanel stack2 = new StackPanel { Orientation = Orientation.Horizontal, HorizontalAlignment = HorizontalAlignment.Right, Margin = new Thickness(0, 60, 0, 0) };
            Button button2 = new Button { Content = "OK", Width = 80, Background = new SolidColorBrush(accent), Foreground = new SolidColorBrush(Colors.White), Margin = new Thickness(10, 0, 0, 0) };
            button2.Click += Button2_Click;
            Button button2b = new Button { Content = "Bcak", Width = 80, Background = new SolidColorBrush(Color.FromArgb(255,228,228,228)), Foreground = new SolidColorBrush(Colors.Black)};
            button2b.Click += Button2b_Click;
            
            stack2.Children.Add(button2b);
            stack2.Children.Add(button2);

            stack1.Children.Add(text1);
            stack1.Children.Add(text2);
            stack1.Children.Add(stack2);
            scrollViewer.Content = stack1;
            main1.Children.Add(scrollViewer);
            con01.Children.Add(new ItemPane(400, 600, "Welcome to the EEB4 app", HorizontalAlignment.Center, main1, "", ""));

            con.Children.Add(con01);
        }

        //adds content
        //sign in interface Stage3
        //username
        private void add3(bool execute)
        {
            var seti = new UISettings();
            var accent = seti.GetColorValue(UIColorType.Accent);

            //add margin
            Grid con01 = new Grid { Margin = new Thickness(10) };

            //new itemPane
            Grid main1 = new Grid();
            ScrollViewer scrollViewer = new ScrollViewer { Padding = new Thickness(0, 0, 15, 0) };
            StackPanel stack1 = new StackPanel { HorizontalAlignment = HorizontalAlignment.Stretch };
            ComboBox box = new ComboBox
            {
                Margin = new Thickness(5, 10, 0, 0),
                PlaceholderText = "I'm a -"
            };
            ComboBoxItem boxItem2 = new ComboBoxItem { Content = "Student" };
            box.Items.Add(boxItem2);
            ComboBoxItem boxItem3 = new ComboBoxItem { Content = "Teacher" };
            box.Items.Add(boxItem3);
            ComboBoxItem boxItem4 = new ComboBoxItem { Content = "Parent" };
            box.Items.Add(boxItem4);
            ComboBoxItem boxItem5 = new ComboBoxItem { Content = "Driver" };
            box.Items.Add(boxItem5);
            TextBlock text1_2 = new TextBlock
            {
                TextWrapping = TextWrapping.WrapWholeWords,
                FontWeight = FontWeights.Bold,
                Margin = new Thickness(0, 20, 0, 0),
                Foreground = new SolidColorBrush(Color.FromArgb(255, 118, 113, 113)),
                Text = "Name"
            };
            TextBox textb1_2 = new TextBox
            {
                Margin = new Thickness(5, 10, 0, 0),
                Foreground = new SolidColorBrush(Color.FromArgb(255, 118, 113, 113)),
                PlaceholderText = "Your name"
            };
            TextBlock text1 = new TextBlock
            {
                TextWrapping = TextWrapping.WrapWholeWords,
                FontWeight = FontWeights.Bold,
                Margin = new Thickness(0, 20, 0, 0),
                Foreground = new SolidColorBrush(Color.FromArgb(255, 118, 113, 113)),
                Text = "Username/e-mail"
            };
            TextBox textb1 = new TextBox
            {
                Margin = new Thickness(5, 10, 0, 0),
                Foreground = new SolidColorBrush(Color.FromArgb(255, 118, 113, 113)),
                PlaceholderText = "Username"
            };
            TextBlock text2 = new TextBlock
            {
                TextWrapping = TextWrapping.WrapWholeWords,
                Margin = new Thickness(0, 20, 0, 0),
                FontWeight = FontWeights.Bold,
                Foreground = new SolidColorBrush(Color.FromArgb(255, 118, 113, 113)),
                Text = "Password"
            };
            PasswordBox textb2 = new PasswordBox
            {
                Margin = new Thickness(5, 10, 0, 0),
                Foreground = new SolidColorBrush(Color.FromArgb(255, 118, 113, 113)),
                PlaceholderText = "Password"
            };
            StackPanel stack2 = new StackPanel { Orientation = Orientation.Horizontal, HorizontalAlignment = HorizontalAlignment.Right, Margin = new Thickness(0, 60, 0, 0) };
            Button button3 = new Button { Content = "OK", Width = 80, Background = new SolidColorBrush(accent), Foreground = new SolidColorBrush(Colors.White), Margin = new Thickness(10, 0, 0, 0) };
            button3.Click += Button3_Click;
            Button button3b = new Button { Content = "Bcak", Width = 80, Background = new SolidColorBrush(Color.FromArgb(255, 228, 228, 228)), Foreground = new SolidColorBrush(Colors.Black)};
            button3b.Click += Button3b_Click;
            
            stack2.Children.Add(button3b);
            stack2.Children.Add(button3);

            stack1.Children.Add(box);
            stack1.Children.Add(text1_2);
            stack1.Children.Add(textb1_2);
            stack1.Children.Add(text1);
            stack1.Children.Add(textb1);
            stack1.Children.Add(text2);
            stack1.Children.Add(textb2);
            stack1.Children.Add(stack2);
            scrollViewer.Content = stack1;
            main1.Children.Add(scrollViewer);
            con01.Children.Add(new ItemPane(400, 600, "Welcome to the EEB4 app", HorizontalAlignment.Center, main1, "", ""));

            con.Children.Add(con01);
        }

        //adds content
        //sign in interface Stage4
        //o365 options
        private void add4(bool execute)
        {
            var seti = new UISettings();
            var accent = seti.GetColorValue(UIColorType.Accent);

            //add margin
            Grid con01 = new Grid { Margin = new Thickness(10) };

            //new itemPane
            Grid main1 = new Grid();
            ScrollViewer scrollViewer = new ScrollViewer { Padding = new Thickness(0, 0, 15, 0) };
            StackPanel stack1 = new StackPanel { HorizontalAlignment = HorizontalAlignment.Stretch };
            TextBlock text1 = new TextBlock
            {
                TextWrapping = TextWrapping.WrapWholeWords,
                FontWeight = FontWeights.Bold,
                Foreground = new SolidColorBrush(Color.FromArgb(255, 118, 113, 113)),
                Text = "Chose what to sync – Office 365"
            };
            CheckBox check1 = new CheckBox { Content = "Mail", Foreground = new SolidColorBrush(Color.FromArgb(255, 118, 113, 113)), IsChecked = true, Margin=new Thickness(5,10,0,0) };
            CheckBox check2 = new CheckBox { Content = "Calendar", Foreground = new SolidColorBrush(Color.FromArgb(255, 118, 113, 113)), IsChecked = true, Margin = new Thickness(5, 5, 0, 0) };
            CheckBox check3 = new CheckBox { Content = "Contacts", Foreground = new SolidColorBrush(Color.FromArgb(255, 118, 113, 113)), IsChecked = true, Margin = new Thickness(5, 5, 0, 0) };
            CheckBox check4 = new CheckBox { Content = "Documents", Foreground = new SolidColorBrush(Color.FromArgb(255, 118, 113, 113)), IsChecked = true, Margin = new Thickness(5, 5, 0, 0) };
            CheckBox check5 = new CheckBox { Content = "Assignments", Foreground = new SolidColorBrush(Color.FromArgb(255, 118, 113, 113)), IsChecked = true, Margin = new Thickness(5, 5, 0, 0) };

            StackPanel stack2 = new StackPanel { Orientation = Orientation.Horizontal, HorizontalAlignment = HorizontalAlignment.Right, Margin = new Thickness(0, 60, 0, 0) };
            Button button4 = new Button { Content = "Next", Width = 80, Background = new SolidColorBrush(accent), Foreground = new SolidColorBrush(Colors.White), Margin = new Thickness(10, 0, 0, 0) };
            button4.Click += Button4_Click;
            Button button4b = new Button { Content = "Bcak", Width = 80, Background = new SolidColorBrush(Color.FromArgb(255, 228, 228, 228)), Foreground = new SolidColorBrush(Colors.Black) };
            button4b.Click += Button4b_Click;
            
            stack2.Children.Add(button4b);
            stack2.Children.Add(button4);

            stack1.Children.Add(text1);
            stack1.Children.Add(check1);
            stack1.Children.Add(check2);
            stack1.Children.Add(check3);
            stack1.Children.Add(check4);
            stack1.Children.Add(check5);
            stack1.Children.Add(stack2);
            scrollViewer.Content = stack1;
            main1.Children.Add(scrollViewer);
            con01.Children.Add(new ItemPane(400, 600, "Welcome to the EEB4 app", HorizontalAlignment.Center, main1, "", ""));

            con.Children.Add(con01);
        }

        //adds content
        //sign in interface Stage5
        //sms options
        private void add5(bool execute)
        {
            var seti = new UISettings();
            var accent = seti.GetColorValue(UIColorType.Accent);

            //add margin
            Grid con01 = new Grid { Margin = new Thickness(10) };

            //new itemPane
            Grid main1 = new Grid();
            ScrollViewer scrollViewer = new ScrollViewer { Padding = new Thickness(0, 0, 15, 0) };
            StackPanel stack1 = new StackPanel { HorizontalAlignment = HorizontalAlignment.Stretch };
            TextBlock text1 = new TextBlock
            {
                TextWrapping = TextWrapping.WrapWholeWords,
                FontWeight = FontWeights.Bold,
                Foreground = new SolidColorBrush(Color.FromArgb(255, 118, 113, 113)),
                Text = "Chose permissions – SMS"
            };
            CheckBox check1 = new CheckBox { Content = "Grades", Foreground = new SolidColorBrush(Color.FromArgb(255, 118, 113, 113)), IsChecked = true, Margin = new Thickness(5, 10, 0, 0) };
            CheckBox check2 = new CheckBox { Content = "Mail", Foreground = new SolidColorBrush(Color.FromArgb(255, 118, 113, 113)), IsChecked = true, Margin = new Thickness(5, 5, 0, 0) };
            CheckBox check3 = new CheckBox { Content = "Schedule", Foreground = new SolidColorBrush(Color.FromArgb(255, 118, 113, 113)), IsChecked = true, Margin = new Thickness(5, 5, 0, 0) };
            CheckBox check4 = new CheckBox { Content = "Absences", Foreground = new SolidColorBrush(Color.FromArgb(255, 118, 113, 113)), IsChecked = true, Margin = new Thickness(5, 5, 0, 0) };
            CheckBox check5 = new CheckBox { Content = "Files", Foreground = new SolidColorBrush(Color.FromArgb(255, 118, 113, 113)), IsChecked = true, Margin = new Thickness(5, 5, 0, 0) };

            StackPanel stack2 = new StackPanel { Orientation = Orientation.Horizontal, HorizontalAlignment = HorizontalAlignment.Right, Margin = new Thickness(0, 60, 0, 0) };
            Button button5 = new Button { Content = "Finish", Width=80, Background = new SolidColorBrush(accent), Foreground = new SolidColorBrush(Colors.White), Margin = new Thickness(10, 0, 0, 0) };
            button5.Click += Button5_Click;
            Button button5b = new Button { Content = "Bcak", Width=80, Background = new SolidColorBrush(Color.FromArgb(255, 228, 228, 228)), Foreground = new SolidColorBrush(Colors.Black)};
            button5b.Click += Button5b_Click;
            
            stack2.Children.Add(button5b);
            stack2.Children.Add(button5);

            stack1.Children.Add(text1);
            stack1.Children.Add(check1);
            stack1.Children.Add(check2);
            stack1.Children.Add(check3);
            stack1.Children.Add(check4);
            stack1.Children.Add(check5);
            stack1.Children.Add(stack2);
            scrollViewer.Content = stack1;
            main1.Children.Add(scrollViewer);
            con01.Children.Add(new ItemPane(400, 600, "Welcome to the EEB4 app", HorizontalAlignment.Center, main1, "", ""));

            con.Children.Add(con01);
        }

        #region forward
        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            //start sign in process
            slider_l.Value = 2;
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            slider_l.Value = 3;
        }

        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            Button lolcal = (Button)sender;
            StackPanel stack1 = (lolcal.Parent as StackPanel).Parent as StackPanel;
            bool can_edit = true;
            bool go = false;

            ComboBox localc = stack1.Children.ElementAt(0) as ComboBox;
            string typel = ((ComboBoxItem)localc.Items[localc.SelectedIndex] as ComboBoxItem).Content.ToString();
            type = typel;

            string namel = (stack1.Children.ElementAt(2) as TextBox).Text;
            name = namel;

            string usernamel = (stack1.Children.ElementAt(4) as TextBox).Text;
            username = usernamel;

            string passwordl = (stack1.Children.ElementAt(6) as PasswordBox).Password;
            password = passwordl;

            if (type != "")
            {
                if (can_edit)
                {
                    go = true;
                }
            }
            else
            {
                localc.PlaceholderForeground = new SolidColorBrush(Colors.Red);
                can_edit = false;
                go = false;
            }

            if (name != "")
            {
                if (can_edit)
                {
                    go = true;
                }
            }
            else
            {
                (stack1.Children.ElementAt(2) as TextBox).PlaceholderForeground = new SolidColorBrush(Colors.Red);
                can_edit = false;
                go = false;
            }

            if (username != "")
            {
                if (can_edit)
                {
                    go = true;
                }
            }
            else
            {
                (stack1.Children.ElementAt(4) as TextBox).PlaceholderForeground = new SolidColorBrush(Colors.Red);
                can_edit = false;
                go = false;
            }

            if (password != "")
            {
                if (can_edit)
                {
                    go = true;
                }
            }
            else
            {
                (stack1.Children.ElementAt(6) as PasswordBox).PlaceholderText = "Password is required";
                can_edit = false;
                go = false;
            }

            if (go == true)
            {
                slider_l.Value = 4;
            }
        }

        private void Button4_Click(object sender, RoutedEventArgs e)
        {

            slider_l.Value = 5;
        }

        //user is signed in
        private void Button5_Click(object sender, RoutedEventArgs e)
        {
            //get date
            string datel = DateTime.Now.Day.ToString()+"/"+ DateTime.Now.Month.ToString() + "/"+ DateTime.Now.Year.ToString();
            date = datel;

            string userInfo = type + ":!#123#!:" + username + ":!#123#!:" + password + ":!#123#!:" + name + ":!#123#!:" + date;
            
            //write to user
            localSettings.Values["User"] = userInfo;
            localSettings.Values["IsSignedIn"] = true.ToString();

            Frame.Navigate(typeof(HomePage1));

            //set up subjects
            Set_Subj();
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
            string result= "";

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
        #endregion

        #region back
        private void Button2b_Click(object sender, RoutedEventArgs e)
        {
            slider_l.Value = 1;
        }
        private void Button3b_Click(object sender, RoutedEventArgs e)
        {
            slider_l.Value = 2;
        }
        private void Button4b_Click(object sender, RoutedEventArgs e)
        {
            slider_l.Value = 3;
        }
        private void Button5b_Click(object sender, RoutedEventArgs e)
        {
            slider_l.Value = 4;
        }
        #endregion

        ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;

        //handle slider change
        private void Slider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            var seti = new UISettings();
            var accent = seti.GetColorValue(UIColorType.Accent);

            if (slider_l.Value == 0) slider_l.Value = 1;

            switch (slider_l.Value)
            {
                case 1:
                    stage1.Background = new SolidColorBrush(accent); stage1.BorderBrush = new SolidColorBrush(accent); //con.Children.Clear(); add1(true);

                    scrv.UpdateLayout();
                    scrv.ChangeView(0.0f, 0f, 1.0f);

                    break;
                case 2:
                    stage2.Background = new SolidColorBrush(accent); stage2.BorderBrush = new SolidColorBrush(accent);
                    if (con.Children.Count() == 1)
                    {
                        add2(false);
                        scrv.UpdateLayout();
                        scrv.ChangeView(0.0f, double.MaxValue, 1.0f);
                    }
                    else
                    {

                        scrv.UpdateLayout();
                        scrv.ChangeView(0.0f, 420f, 1.0f);
                    }
                    break;
                case 3:
                    stage3.Background = new SolidColorBrush(accent); stage3.BorderBrush = new SolidColorBrush(accent);
                    if (con.Children.Count() == 2)
                    {
                        add3(false);
                        scrv.UpdateLayout();
                        scrv.ChangeView(0.0f, double.MaxValue, 1.0f);
                    }
                    else
                    {
                        scrv.UpdateLayout();
                        scrv.ChangeView(0.0f, 840f, 1.0f);
                    }
                    break;
                case 4:
                    stage4.Background = new SolidColorBrush(accent); stage4.BorderBrush = new SolidColorBrush(accent);
                    if (con.Children.Count() == 3)
                    {
                        add4(false);
                        scrv.UpdateLayout();
                        scrv.ChangeView(0.0f, double.MaxValue, 1.0f);
                    }
                    else
                    {
                        scrv.UpdateLayout();
                        scrv.ChangeView(0.0f, 1260, 1.0f);
                    }
                    break;
                case 5:
                    stage5.Background = new SolidColorBrush(accent); stage5.BorderBrush = new SolidColorBrush(accent);
                    if (con.Children.Count() == 4)
                    {
                        add5(false);
                        slider_l.Background = new SolidColorBrush(accent);
                        slider_l.Value = 5;
                        scrv.UpdateLayout();
                        scrv.ChangeView(0.0f, double.MaxValue, 1.0f);
                    }
                    else
                    {
                        scrv.UpdateLayout();
                        scrv.ChangeView(0.0f, double.MaxValue, 1.0f);
                    }
                    break;
            }
        }

        private void scrv_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            if (timer.IsEnabled) timer.Stop();
            timer.Start();
        }

        //handles scrollviewr stoed scrolling
        private void Timer_Tick(object sender, object e)
        {
            timer.Stop();
            double maxV = scrv.ScrollableHeight;
            double current_loc = scrv.VerticalOffset;
            double new_loc = 0;

            if ((current_loc != 0) || (current_loc != 420) || (current_loc != 840) || (current_loc != 1260) || (current_loc != maxV))
            {
                Task.Delay(20);
                //put into scopes
                if (current_loc < 230)
                {
                    //stage1
                    new_loc = 0;
                    slider_l.Value = 1;
                }
                else if ((current_loc > 231) && (current_loc < 630))
                {
                    //stage2
                    new_loc = 420;
                    slider_l.Value = 2;
                }
                else if ((current_loc > 631) && (current_loc < 1050))
                {
                    //stage3
                    new_loc = 840;
                    slider_l.Value = 3;
                }
                else if ((current_loc > 1051) && (current_loc < 1470))
                {
                    //stage4
                    new_loc = 1260;
                    slider_l.Value = 4;
                }
                else if (current_loc > 1471)
                {
                    //stage5
                    new_loc = maxV;
                    slider_l.Value = 5;
                }
                scrv.ChangeView(0.0f, new_loc, 1.0f);
            }
        }
    }
    /*    public static class ScrollViewerBinding
    {
        #region VerticalOffset attached property

        /// <summary>
        /// Gets the vertical offset value
        /// </summary>
        public static double GetVerticalOffset(DependencyObject depObj)
        {
            return (double)depObj.GetValue(VerticalOffsetProperty);
        }

        /// <summary>
        /// Sets the vertical offset value
        /// </summary>
        public static void SetVerticalOffset(DependencyObject depObj, double value)
        {
            depObj.SetValue(VerticalOffsetProperty, value);
        }

        /// <summary>
        /// VerticalOffset attached property
        /// </summary>
        public static readonly DependencyProperty VerticalOffsetProperty =
            DependencyProperty.RegisterAttached("VerticalOffset", typeof(double),
            typeof(ScrollViewerBinding),
          new PropertyMetadata(0.0, OnVerticalOffsetPropertyChanged));

        #endregion

        /// <summary>
        /// An attached property which holds a reference to the vertical scrollbar which
        /// is extracted from the visual tree of a ScrollViewer
        /// </summary>
        private static readonly DependencyProperty VerticalScrollBarProperty =
            DependencyProperty.RegisterAttached("VerticalScrollBar", typeof(ScrollBar),
            typeof(ScrollViewerBinding), new PropertyMetadata(null));

        /// <summary>
        /// Invoked when the VerticalOffset attached property changes
        /// </summary>
        private static void OnVerticalOffsetPropertyChanged(DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            ScrollViewer sv = d as ScrollViewer;
            if (sv != null)
            {
                // check whether we have a reference to the vertical scrollbar
                if (sv.GetValue(VerticalScrollBarProperty) == null)
                {
                    // if not, handle LayoutUpdated, which will be invoked after the
                    // template is applied and extract the scrollbar
                    sv.LayoutUpdated += (s, ev) =>
                    {
                        if (sv.GetValue(VerticalScrollBarProperty) == null)
                        {
                            GetScrollBarsForScrollViewer(sv);
                        }
                    };
                }
                else
                {
                    // update the scrollviewer offset
                    sv.ChangeView(0.0f,(double)e.NewValue,1.0f);
                }
            }
        }

        private static void GetScrollBarsForScrollViewer(ScrollViewer scrollViewer)
        {
            Slider slider = new Slider { Orientation = Orientation.Vertical };
            if (slider != null)
            {
                // save a reference to this scrollbar on the attached property
                //scrollViewer.SetValue(VerticalScrollBarProperty, scroll);

                // scroll the scrollviewer
                scrollViewer.ChangeView(0.0f,GetVerticalOffset(scrollViewer),1.0f);


                // handle the changed event to update the exposed VerticalOffset
                slider.ValueChanged += (s, e) =>
                {
                    SetVerticalOffset(scrollViewer, e.NewValue);
                };
            }
        }

        /// <summary>
        /// Searches the descendants of the given element, looking for a scrollbar
        /// with the given orientation.
        /// </summary>
        private static ScrollBar GetScrollBar(FrameworkElement fe, Orientation orientation)
        {
            ScrollBar scrollBar = new ScrollBar();
            for (var i = 0; i < VisualTreeHelper.GetChildrenCount(fe); i++)
            {
                var child = VisualTreeHelper.GetChild(fe, i);

                if (child.GetType() == typeof(ScrollBar))
                {
                    scrollBar= (ScrollBar)child;                    
                }
                //else return null;
            }
            //return fe.l().OfType<ScrollBar>().Where(s => s.Orientation == orientation).SingleOrDefault();
            return scrollBar;
        }
    }*/
}
