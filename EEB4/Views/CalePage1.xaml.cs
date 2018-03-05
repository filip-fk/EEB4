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
    public sealed partial class CalePage1 : Page
    {
        public CalePage1()
        {
            this.InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var appointment = new Windows.ApplicationModel.Appointments.Appointment();

            appointment.StartTime = DateTime.Now + TimeSpan.FromDays(1);
            appointment.Duration = TimeSpan.FromHours(1);
            appointment.Location = "Meeting location";
            appointment.Subject = "Meeting subject";
            appointment.Details = "Meeting description";
            appointment.Reminder = TimeSpan.FromMinutes(15); // Remind me 15 minutes prior


            // ShowAddAppointmentAsync returns an appointment id if the appointment given was added to the user' s calendar.
            // This value should be stored in app data and roamed so that the appointment can be replaced or removed in the future.
            // An empty string return value indicates that the user canceled the operation before the appointment was added.
            String appointmentId =
                await Windows.ApplicationModel.Appointments.AppointmentManager.ShowEditNewAppointmentAsync(appointment);
        }
    }
}
