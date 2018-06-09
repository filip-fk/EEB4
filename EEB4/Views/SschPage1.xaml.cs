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
    public sealed partial class SschPage1 : Page
    {
        public SschPage1()
        {
            this.InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            load_subj();
            Load_Sample_Schedule();
            Load_Days();
            Load_Times();
            //Load_Schedule();
        }

        private void Load_Times()
        {
            TimeSpan timeL = StartTime;
            for (int i = 1; i < left.RowDefinitions.Count() + 1; i++)
            {
                if (i < 4)
                {
                    Get_TextBlock(i, timeL, i - 1, new TimeSpan(0,0,0));
                }
                else if (i > 4)
                {
                    Get_TextBlock((i - 1), timeL, i - 1, new TimeSpan(0,15,0));
                }
            }
        }

        private void Get_TextBlock(int i, TimeSpan timeL, int r, TimeSpan br)
        {
            TimeSpan span1 = TimeSpan.FromMinutes(i * 5 + (i - 1) * 45);
            var ts1 = timeL.Add(span1);
            ts1 = ts1.Add(br);
            TimeSpan span2 = TimeSpan.FromMinutes(i * 50);
            var ts2 = timeL.Add(span2);
            ts2 = ts2.Add(br);
            TextBlock text = new TextBlock { Text = GetHour(ts1) + " - " + GetHour(ts2), HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center };
            Grid.SetRow(text, r);
            left.Children.Add(text);
        }

        private string GetHour(TimeSpan ts)
        {
            string h = ts.Hours.ToString();
            if (h.Length < 2)
            {
                h = "0" + h;
            }
            string m = ts.Minutes.ToString();
            if (m.Length < 2)
            {
                m = "0" + m;
            }

            return h + ":" + m;
        }

        private void Load_Days()
        {
            for (int i = 0; i < up.ColumnDefinitions.Count(); i++)
            {
                switch (i)
                {
                    case 0:
                        SetDayOfWeek("Monday", i);
                        break;
                    case 1:
                        SetDayOfWeek("Tuesday", i);
                        break;
                    case 2:
                        SetDayOfWeek("Wednesday", i);
                        break;
                    case 3:
                        SetDayOfWeek("Thursday", i);
                        break;
                    case 4:
                        SetDayOfWeek("Friday", i);
                        break;
                }
            }
        }

        private void SetDayOfWeek(string d, int i)
        {
            TextBlock text = new TextBlock { Text = d, FontWeight = FontWeights.Bold, HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center };
            Grid.SetColumn(text, i);
            up.Children.Add(text);
        }

        ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
        StorageFolder localFolder = ApplicationData.Current.LocalFolder;

        private List<Subject> Subjects = new List<Subject>();
        private TimeSpan StartTime = new TimeSpan(8, 10, 0);

        private void load_subj()
        {
            string str = "No subjects available";
            //try
            //{
            str = (localSettings.Values["Subjects"].ToString());
            int l_count1 = -1; int l_count2 = -1; int l_count3 = -1; int l_count4 = -1;
            Color c;
            string sName = "";
            string sTeac = "";
            string sLoca = "";
            string sTime = "";
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
                            l_count1++;
                            string s_el2 = s_el.Substring(3);
                            sName = s_el2;
                        }
                    }
                    else if (s_el.First() == '2')
                    {
                        //teac
                        l_count2++;
                        string s_el2 = s_el.Substring(3);
                        sTeac = s_el2;
                    }
                    else if (s_el.First() == '3')
                    {
                        //teac
                        l_count3++;
                        string s_el2 = s_el.Substring(3);
                        sLoca = s_el2;
                        sTime = s_el2;
                        //sLoca = Get_LocD(s_el2, "l");
                        //sTime = Get_LocD(s_el2, "t");
                    }
                    else if (s_el.First() == '4')
                    {
                        //colorif (subj.Contains("@"))
                        {
                            //color
                            l_count4++;
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
                            c = new SolidColorBrush(Color.FromArgb(a, r, g, b)).Color;
                        }
                    }
                }
                Subjects.Add(new Subject { color = c, name = sName, teacher = sTeac, location = sLoca, time = sTime });
            }
        }

        private string Get_Time(string v, string code, TimeSpan br)
        {
            //input ex --> 5|7 = day 5 (FRI); period 7
            string result = "";
            int day = Convert.ToInt32(v.First().ToString());
            int per = Convert.ToInt32(v.Last().ToString());
            TimeSpan l_TS = new TimeSpan(8, 15, 0);
            TimeSpan oneP = new TimeSpan(0, 50, 0);
            TimeSpan brea = new TimeSpan(0, 15, 0);

            for (int i = 0; i < per; i++)
            {
                l_TS = l_TS.Add(oneP);
            }

            if (per > 3)
            {
                l_TS.Add(brea);
            }

            if (code == "d")
            {
                result = day.ToString();
            }
            else if (code == "p")
            {
                result = per.ToString();
            }
            else if (code == "b")
            {
                l_TS = l_TS.Add(br);
                result = GetHour(l_TS) + " - " + GetHour(l_TS.Add(new TimeSpan(0, 45, 0)));
            }

            return result;
        }

        private void Get_LocD(Subject sbj)
        {
            string input = sbj.time;
            if (input.Contains("&&"))
            {
                string[] spl = input.Split(new[] { "&&" }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string str in spl)
                {
                    SplitLD(sbj, str);
                }
            }
            else SplitLD(sbj, input);
        }

        private void SplitLD(Subject input, string con)
        {
            string loc = "";
            string tim = "";
            string[] spl = con.Split(new[] { "++" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string str in spl)
            {
                if (str.Contains("|"))
                {
                    //time (D)
                        tim = str;
                }
                else
                {
                    //locaion (L)
                        loc = str;
                }

                SetClass(input.name, Convert.ToInt32(Get_Time(tim, "p", new TimeSpan(0,0,0))) - 1, Convert.ToInt32(Get_Time(tim, "d", new TimeSpan(0, 0, 0))) - 1, loc);
            }
        }

        private void Load_Sample_Schedule()
        {
            foreach (Subject sbj in Subjects)
            {
                Get_LocD(sbj);
            }

            #region try1
            /*foreach (RowDefinition r in con.RowDefinitions)
            {
                if (r.Height != new GridLength(10, GridUnitType.Pixel))
                {
                    i++;
                    ri++;
                    //not seperator
                    foreach (ColumnDefinition c in con.ColumnDefinitions)
                    {
                        i++;
                        ci++;

                        switch (i)
                        {
                            case 0:
                                //1|1 -> monday first period
                                SetClass(r, c, "MA6", ri, ci);
                                break;
                        }
                    }
                }
            }*/
            #endregion
            #region try2
            /*int i = -1;
            int ri = -1;
            int ci = -1;
            int r = 0;
            string SUBJ = "";

            for (int j = 0; j < con.RowDefinitions.Count(); j++)
            {
                for (int k = 0; k < con.ColumnDefinitions.Count(); k++)
                {
                    ri = j;
                    ci = k;

                    Random random = new Random();
                    r = random.Next(0, 13);
                    switch (r)
                    {
                        case 0:
                            SUBJ = "MA6";
                            break;
                        case 1:
                            SUBJ = "L1-";
                            break;
                        case 2:
                            SUBJ = "L2-";
                            break;
                        case 3:
                            SUBJ = "L3-";
                            break;
                        case 4:
                            SUBJ = "L4-";
                            break;
                        case 5:
                            SUBJ = "PHY";
                            break;
                        case 6:
                            SUBJ = "CHI";
                            break;
                        case 7:
                            SUBJ = "BIO";
                            break;
                        case 8:
                            SUBJ = "MOR";
                            break;
                        case 9:
                            SUBJ = "HIS";
                            break;
                        case 10:
                            SUBJ = "GEO";
                            break;
                        case 11:
                            SUBJ = "EPM";
                            break;
                        case 12:
                            SUBJ = "#FRR";
                            break;
                    }
                    await Task.Delay(30);
                    SetClass(SUBJ, ri, ci);
                }
            }*/
            #endregion
        }

        private void SetClass(string SUBJ, int ri, int ci, string loc)
        {
            if (ri >= 3)
            {
                ri++;
            }

            if (SUBJ == "#FRR")
            {

            }
            else
            {
                Grid local = new Grid { Background = new SolidColorBrush(Subjects.Find(x => x.name.Contains(SUBJ)).color), Margin = new Thickness(5), CornerRadius = new CornerRadius(15) };
                TextBlock text = new TextBlock { Text = (Subjects.Find(x => x.name.Contains(SUBJ)).name), Foreground = new SolidColorBrush(IdealTextColor((Subjects.Find(x => x.name.Contains(SUBJ)).color))), HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center };
                local.Children.Add(text);
                Grid.SetRow(local, ri);
                Grid.SetColumn(local, ci);
                local.Tapped += (Sender, TappedRoutedEventArgs) => { Local_Tapped(Sender, TappedRoutedEventArgs, SUBJ, ri, ci, loc); };
                local.RightTapped += (Sender, RightTappedRoutedEventArgs) => { Local_RightTapped(Sender, RightTappedRoutedEventArgs, SUBJ); };
                con.Children.Add(local);
            }
        }

        private void Local_RightTapped(object sender, RightTappedRoutedEventArgs e, string SUBJ)
        {
            subjN2.Text = (Subjects.Find(x => x.name.Contains(SUBJ)).name);
            Flyout flyout = new Flyout();
            flyout = SubjectFlyout2;
            flyout.ShowAt(sender as FrameworkElement);
        }

        private void Local_Tapped(object sender, TappedRoutedEventArgs e, string SUBJ, int ri, int ci, string loc)
        {
            subjN.Text = (Subjects.Find(x => x.name.Contains(SUBJ)).name);
            teacher.Text = (Subjects.Find(x => x.name.Contains(SUBJ)).teacher);
            room.Text = loc;
            TimeSpan time = new TimeSpan(0, 0, 0);
            if (ri >= 4)
            {
                ri--;
                time = time.Add(new TimeSpan(0, 15, 0));
            }
            timeS.Text = Get_Time((ci).ToString()+"|"+(ri).ToString(), "b", time);
            Flyout flyout = new Flyout();
            flyout = SubjectFlyout;
            flyout.ShowAt(sender as FrameworkElement);
        }

        public Color IdealTextColor(Color bg)
        {
            int nThreshold = 105;
            int bgDelta = Convert.ToInt32((bg.R * 0.299) + (bg.G * 0.587) +
                                          (bg.B * 0.114));

            Color foreColor = (255 - bgDelta < nThreshold) ? Colors.Black : Colors.White;
            return foreColor;
        }

        private void discB_Click(object sender, RoutedEventArgs e)
        {
            //fl-stp-gr-sener
            Popup pp = (Popup)((FlyoutPresenter)((StackPanel)((Grid)((Button)(sender)).Parent).Parent).Parent).Parent;
            tt2.Text = "";
            nt2.Text = "";
            pp.IsOpen = false;
        }
    }
}
