using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Security.Cryptography;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;
using WinRTXamlToolkit.Controls.DataVisualization.Charting;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace EEB4
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SproPage1 : Page
    {
        public SproPage1()
        {
            this.InitializeComponent();
        }

        private List<Subject> Subjects = new List<Subject>();
        private List<GradesList> ListsG = new List<GradesList>();
        private List<PointssList> ListsP = new List<PointssList>();
        private List<LinesList> ListsL = new List<LinesList>();
        private const int year = 2017;
        
        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            load_months();
            load_subj(); //load_lists(); executed trough load_subj();
            await Task.Delay(100);
            load_sample_grade();
        }

        ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
        StorageFolder localFolder = ApplicationData.Current.LocalFolder;

        private void load_subj()
        {
            string str = "No subjects available";
            //try
            //{
            str = (localSettings.Values["Subjects"].ToString());
            int l_count1 = -1; int l_count2 = -1;
            Color c;
            string sName = "";
            string[] toSPL = str.Split(new[] { ":!#123#!:" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string subj in toSPL)
            {
                string[] l_str = subj.Split(new[] { "^$" }, StringSplitOptions.RemoveEmptyEntries);
                foreach(string s_el in l_str)
                {
                    if (s_el.First() == '1')
                    {
                        //name
                        if (!s_el.Contains("@"))
                        {
                            //text
                            l_count2++;
                            string s_el2 = s_el.Substring(3);
                            sName = s_el2;
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
                            c = new SolidColorBrush(Color.FromArgb(a, r, g, b)).Color;
                        }
                    }
                }
                Subjects.Add(new Subject { color = c, name = sName });
            }

            load_lists();
        }

        private async void load_lists()
        {
            foreach(Subject s in Subjects)
            {
                ListsG.Add(new GradesList { name = s.name, Grades = new List<Grade>() });
                ListsP.Add(new PointssList { name = s.name, Points = new PointCollection() });
                ListsL.Add(new LinesList { name = s.name, Line = new Polyline() });

                subjrcts_con.Items.Add(s.name);
            }

            await Task.Delay(1000);
        }

        private void load_months()
        {
            m1.Text += year + 1.ToString();
            m2.Text += year + 1.ToString();
            m3.Text += year + 1.ToString();
            m4.Text += year + 1.ToString();
            m5.Text += year + 1.ToString();
            m6.Text += year + 1.ToString();
            m7.Text += year + 1.ToString();
            m9.Text += year.ToString();
            m10.Text += year.ToString();
            m11.Text += year.ToString();
            m12.Text += year.ToString();
        }

        public void load_sample_grade()
        {
            #region try1
            /*
            int j = 100;
            for(int i = 1; i < j; i++)
            {
                Grades_math.Add(new Grade { name = "Math B-Test", subj = "S4MA6DEA", note="8", date = i.ToString() + "/03/2018" });
                Grades_l1.Add(new Grade { name = "L1 B-Test", subj = "S4L1-DEA", note ="10", date = i.ToString() + "/03/2018" });
                Grades_l2.Add(new Grade { name = "L2 B-Test", subj = "S4L2-ENB", note = "7", date = i.ToString() + "/03/2018" });
                Grades_l3.Add(new Grade { name = "L3 B-Test", subj = "S4L3-FRB", note = "6", date = i.ToString() + "/03/2018" });
                Grades_l4.Add(new Grade { name = "L4 B-Test", subj = "S4L4-ESB", note = "5", date = i.ToString() + "/03/2018" });
            }

            line_con.SeriesDefinitions.Add(new SeriesDefinition { IndependentValuePath = "date", DependentValuePath = "note", Title = "Math", ItemsSource = Grades_math });
            line_con.SeriesDefinitions.Add(new SeriesDefinition { IndependentValuePath = "date", DependentValuePath = "note", Title = "L1", ItemsSource = Grades_l1 });
            line_con.SeriesDefinitions.Add(new SeriesDefinition { IndependentValuePath = "date", DependentValuePath = "note", Title = "L2", ItemsSource = Grades_l2 });
            line_con.SeriesDefinitions.Add(new SeriesDefinition { IndependentValuePath = "date", DependentValuePath = "note", Title = "L3", ItemsSource = Grades_l3 });
            line_con.SeriesDefinitions.Add(new SeriesDefinition { IndependentValuePath = "date", DependentValuePath = "note", Title = "L4", ItemsSource = Grades_l4 });
            //
            //          Line.ItemsSource = Grades_math;
            //          Line2.ItemsSource = Grades_l1;
            Grades_add(Grades_math);
            Grades_add(Grades_l1);
            Grades_add(Grades_l2);
            Grades_add(Grades_l3);
            Grades_add(Grades_l4);
            //Line.LegendItems.Clear();

            Task.Delay(1000);
            update();
            //(Line.Series[0] as LineSeries).DependentRangeAxis = new LineAreaBaseSeries<DataPoint>*/
            #endregion
            
            string SUBJL = "";

            int j = 22;
            for (int i = 1; i < j; i++)
            {
                SUBJL = "MA6";
                SetGrade("Math B-test", (ListsG.Find(x => x.name.Contains(SUBJL))).name, SUBJL, RandomNumber(0, 0, 1).ToString(), new DateTime(2018,6,i));

                SUBJL = "L1-";
                SetGrade("L1 B-test", (ListsG.Find(x => x.name.Contains(SUBJL))).name, SUBJL, RandomNumber(0, 0, 1).ToString(), new DateTime(2018, 6, i));

                SUBJL = "L2-";
                SetGrade("L2 B-test", (ListsG.Find(x => x.name.Contains(SUBJL))).name, SUBJL, RandomNumber(0, 0, 1).ToString(), new DateTime(2018, 6, i));

                SUBJL = "L3-";
                SetGrade("L3 B-test", (ListsG.Find(x => x.name.Contains(SUBJL))).name, SUBJL, RandomNumber(0, 0, 1).ToString(), new DateTime(2018, 6, i));

                SUBJL = "L4-";
                SetGrade("L4 B-test", (ListsG.Find(x => x.name.Contains(SUBJL))).name, SUBJL, RandomNumber(0, 0, 1).ToString(), new DateTime(2018, 6, i));
            }

            foreach (GradesList gl in ListsG)
            {
                Grades_add(gl.Grades);
            }

            foreach(LinesList ll in ListsL)
            {
                ll.Line.Stroke = new SolidColorBrush(Subjects.Find(x => x.name.Contains(ll.name)).color);
                ll.Line.StrokeThickness = 3;
                ll.Line.Points = (ListsP.Find(x => x.name.Contains(ll.name))).Points;

                Canvas.SetTop(ll.Line, 0);
                Canvas.SetLeft(ll.Line, 0);
                cnv.Children.Add(ll.Line);
            }
        }

        private void SetGrade(string name_, string subj_, string SUBJ_, string note_, DateTime date_)
        {
            //subj_ = actual
            //SUBJ_ = short uniform
            (ListsG.Find(x => x.name.Contains(SUBJ_))).Grades.Add(new Grade { name = name_, subj = subj_, note = note_, date = date_.ToString() });
            Get_Point(subj_, SUBJ_, Convert.ToInt32(note_), date_);
        }
        
        private void Get_Point(string subjL, string SUBJ, int note, DateTime dateL)
        {
            //sunj detirmines color
            //note detirmines y-
            //date detirmines x-

            Point point1 = new Point { X = dateL.Day * 40, Y = ((-1 * note + 100) * 4.2) - 0 };
            ((ListsP.Find(x => x.name.Contains(SUBJ))).Points).Add(point1);
        }

        private void Grades_add(List<Grade> ts)
        {
            foreach(Grade g in ts)
            {
                grade_list.Items.Add(g);
            }
        }

        private int RandomNumber(int min, int max, double q_code)
        {
            if (q_code == 1)
            {
                Random random = new Random();
                return random.Next(40, 100);
            }
            else
            {
                Random random = new Random();
                return random.Next(min, max);
            }
        }

        private void update()
        {
            Task.Delay(10000);
            int j = 31;
            for (int i = 1; i < j; i++)
            {
                grade_list.UpdateLayout();
                main_con.UpdateLayout();
            }

            Task.Delay(6000);
            grade_list.UpdateLayout();
        }
    }

    class Grade
    {
        public Grade()
        {

        }

        public string name { get; set; }
        public string subj { get; set; }
        public string note { get; set; }
        public string date { get; set; }
    }

    class Subject
    {
        public Subject()
        {

        }

        public string name { get; set; }
        public string teacher { get; set; }
        public string location { get; set; }
        public string time { get; set; }
        public string note { get; set; }
        public Color color { get; set; }
    }

    class GradesList
    {
        public GradesList()
        {

        }

        public string name { get; set; }
        public List<Grade> Grades { get; set; }
    }

    class PointssList
    {
        public PointssList()
        {

        }

        public string name { get; set; }
        public PointCollection Points { get; set; }
    }

    class LinesList
    {
        public LinesList()
        {

        }

        public string name { get; set; }
        public Polyline Line { get; set; }
    }
}
