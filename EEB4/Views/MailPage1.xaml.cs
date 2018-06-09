using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class MailPage1 : Page
    {
        private Mail dMail = new Mail();
        public Mail DMail { get { return this.dMail; } }

        public MailPage1()
        {
            this.InitializeComponent();

            LoadMail();
        }

        private List<Mail> Emails { get; set; } = new List<Mail>();

        private void LoadMail()
        {
            LoadMailSample();
        }

        private void LoadMailSample()
        {
            int j = 100;
            for (int i = 0; i < j; i++)
            {
                Emails.Add(new Mail
                {
                    Sender = "John SMITH",
                    Subject = "Project example " + i.ToString(),
                    FContent = GenText(800),
                    SContent = "Lorem ipsum dolor sit amet, consectetur...",
                    DateStamp = "21/03",
                    IsNotRead = true,
                    IsAttached = false,
                    IsFlagged = false,
                    ID = i * 17 + 3
                });
            }

            mailList.ItemsSource = Emails;
        }

        private string GenText(int char_num)
        {
            string text = "";
            int r = 0;
            for(int i = 0; i < char_num; i++)
            {
                Random random = new Random();
                r = random.Next(0,10);

                switch (r)
                {
                    case 0:
                        text += "a";
                        break;
                    case 1:
                        text += "b";
                        break;
                    case 2:
                        text += "c";
                        break;
                    case 3:
                        text += "d";
                        break;
                    case 4:
                        text += "e";
                        break;
                    case 5:
                        text += "f";
                        break;
                    case 6:
                        text += " ";
                        break;
                    case 7:
                        text += ".";
                        break;
                    case 8:
                        text += "o";
                        break;
                    case 9:
                        text += "i";
                        break;
                }
            }
            return text;
        }

        private void Delete_Invoked(SwipeItem sender, SwipeItemInvokedEventArgs args)
        {
            Mail m = (args.SwipeControl.DataContext as Mail);
            int i = 0;
            foreach (Mail mail in mailList.Items)
            {
                if (i == 0)
                {
                    if (mail.ID.ToString() == m.ID.ToString())
                    {
                        m = mail;
                        i = 1;
                    }
                }
                else { }
            }
            Emails.Remove(m);
            mailList.ItemsSource = null;
            mailList.ItemsSource = Emails;
        }

        private void Read_Invoked(SwipeItem sender, SwipeItemInvokedEventArgs args)
        {
            Mail m = (args.SwipeControl.DataContext as Mail);
            int i = 0;
            int j = 0;
            int k = 0;
            foreach (Mail mail in mailList.Items)
            {
                j++;
                if (i == 0)
                {
                    if (mail.ID.ToString() == m.ID.ToString())
                    {
                        m = mail;
                        k = j - 1;
                        i = 1;
                    }
                }
                else { }
            }
            Emails.Remove(m);
            m.IsNotRead = !m.IsNotRead;
            Emails.Insert(k, m);
            mailList.ItemsSource = null;
            mailList.ItemsSource = Emails;
        }

        private void Flag_Invoked(SwipeItem sender, SwipeItemInvokedEventArgs args)
        {
            Mail m = (args.SwipeControl.DataContext as Mail);
            int i = 0;
            int j = 0;
            int k = 0;
            foreach (Mail mail in mailList.Items)
            {
                j++;
                if (i == 0)
                {
                    if (mail.ID.ToString() == m.ID.ToString())
                    {
                        m = mail;
                        k = j - 1;
                        i = 1;
                    }
                }
                else { }
            }
            Emails.Remove(m);
            m.IsFlagged = !m.IsFlagged;
            Emails.Insert(k, m);
            mailList.ItemsSource = null;
            mailList.ItemsSource = Emails;
        }

        private void DelButton_Click(object sender, RoutedEventArgs e)
        {
            int i = 0;
            int j = -1;
            Mail m = new Mail();
            string buttonID = ((Button)sender).Tag.ToString();

            foreach (Mail mail in mailList.Items)
            {
                if (i == 0)
                {
                    if (mail.ID.ToString() == buttonID)
                    {
                        m = mail;
                        i = 1;
                    }
                }
                else { }
            }

            Emails.Remove(m);
            mailList.ItemsSource = null;
            mailList.ItemsSource = Emails;
        }

        private void mailList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Type type = mailList.SelectedItem.GetType();
                Mail m = new Mail();
                if (type == typeof(Mail))
                {
                    m = mailList.SelectedItem as Mail;
                    title.Visibility = Visibility.Visible;
                    subject.Visibility = Visibility.Visible;
                    dstamp.Visibility = Visibility.Visible;

                    if (m.IsAttached)
                    {
                        att.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        att.Visibility = Visibility.Collapsed;
                    }
                    if (m.IsFlagged)
                    {
                        flg.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        flg.Visibility = Visibility.Collapsed;
                    }
                    del.Visibility = Visibility.Visible;
                    del.Tag = m.ID;

                    title.Text = m.Sender;
                    subject.Text = m.Subject;
                    dstamp.Text = m.DateStamp;
                    cont.Text = m.FContent;
                    pic.DisplayName = m.Sender;

                    if (m.IsNotRead)
                    {
                        ((Mail)mailList.SelectedItem).IsNotRead = false;
                    }
                }
                else
                {
                    //error
                }
            }
            catch { }
        }
    }//st gr gr co st gr

    public class Mail
    {
        public Mail()
        {

        }

        public string Sender { get; set; }
        public string Subject { get; set; }
        public string FContent { get; set; }
        public string SContent { get; set; }
        public string DateStamp { get; set; }
        public bool IsNotRead { get; set; }
        public bool IsAttached { get; set; }
        public bool IsFlagged { get; set; }
        public bool CanReply { get; set; }
        public double ID { get; set; }
    }
}
