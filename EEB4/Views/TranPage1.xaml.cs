using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Services.Maps;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
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
    public sealed partial class TranPage1 : Page
    {
        public TranPage1()
        {
            this.InitializeComponent();
            request_loc();
        }

        public async void request_loc()
        {
            #region bad
            /*var accessStatus = await Geolocator.RequestAccessAsync();
            var _geolocator = new Geolocator();
            switch (accessStatus)
            {
                case GeolocationAccessStatus.Allowed:
                    // Create Geolocator and define perodic-based tracking (2 second interval).
                    _geolocator = new Geolocator { ReportInterval = 1 };

                    // Subscribe to the PositionChanged event to get location updates.
                    _geolocator.PositionChanged += OnPositionChanged;

                    break;

                case GeolocationAccessStatus.Denied:
                    //Denied
                    break;

                case GeolocationAccessStatus.Unspecified:
                    //Unspecified
                    break;
            }*/
            #endregion

            var _geolocator = new Geolocator();

            // Create Geolocator and define perodic-based tracking (2 second interval).
            _geolocator = new Geolocator { ReportInterval = 1 };

            // Subscribe to the PositionChanged event to get location updates.
            _geolocator.PositionChanged += OnPositionChanged;

            Geoposition pos = await _geolocator.GetGeopositionAsync();
            UpdateLocationData(pos);
        }

        async private void OnPositionChanged(Geolocator sender, PositionChangedEventArgs e)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
            {
                //get location

                Geolocator geolocator = new Geolocator { DesiredAccuracyInMeters = 1 };

                // Carry out the operation.
                Geoposition pos = await geolocator.GetGeopositionAsync();
                UpdateLocationData(pos);
            });
        }

        double lat = 0;
        double log = 0;

        public void UpdateLocationData(Geoposition pos)
        {
            lat = pos.Coordinate.Point.Position.Latitude;
            log = pos.Coordinate.Point.Position.Longitude;

            pin_onMap(pos);

            BasicGeoposition point1 = new BasicGeoposition() { Latitude = 50.8849, Longitude = 4.35303 };
            BasicGeoposition point2 = new BasicGeoposition() { Latitude = lat, Longitude = log };
            BasicGeoposition point3 = new BasicGeoposition() { Latitude = 50.81024, Longitude = 4.419431 };

            showRoute(point1, point2, point3);
        }

        private void pin_onMap(Geoposition pos)
        {
            MapControl1.Children.Clear();
            var seti = new UISettings();
            var accent = seti.GetColorValue(UIColorType.Accent);

            BasicGeoposition myLocation = new BasicGeoposition { Latitude = pos.Coordinate.Point.Position.Latitude, Longitude = pos.Coordinate.Point.Position.Longitude };
            Geopoint _myLocation = new Geopoint(myLocation);

            Grid border = new Grid
            {
                CornerRadius = new CornerRadius(90),
                Height = 20,
                Width = 20,
                Background = new SolidColorBrush(Colors.White),
                BorderBrush = new SolidColorBrush(accent),
                BorderThickness = new Thickness(5),
            };

            border.PointerEntered += Border_PointerEntered;
            border.PointerExited += Border_PointerExited;

            // Add XAML to the map.
            MapControl1.Children.Add(border);
            MapControl.SetLocation(border, _myLocation);
            MapControl.SetNormalizedAnchorPoint(border, new Windows.Foundation.Point(0.5, 0.5));

            f++;
            if (f == 1)
            {
                MapControl1.Center = pos.Coordinate.Point;
                MapControl1.ZoomLevel = 17;
            }
        }

        private int f = 0;

        private async void showRoute(BasicGeoposition pos1, BasicGeoposition pos2, BasicGeoposition pos3)
        {
            var seti = new UISettings();
            var accent = seti.GetColorValue(UIColorType.Accent);
            var accent1 = seti.GetColorValue(UIColorType.AccentLight2);

            Geolocator locator = new Geolocator();
            locator.DesiredAccuracyInMeters = 1;

            var path = new List<EnhancedWaypoint>();

            path.Add(new EnhancedWaypoint(new Geopoint(pos1), WaypointKind.Stop));
            path.Add(new EnhancedWaypoint(new Geopoint(pos2), WaypointKind.Stop));
            //path.Add(new EnhancedWaypoint(new Geopoint(pos3), WaypointKind.Stop));

            MapRouteFinderResult routeResult = await MapRouteFinder.GetDrivingRouteFromEnhancedWaypointsAsync(path);

            if (routeResult.Status == MapRouteFinderStatus.Success)
            {
                MapRouteView viewOfRoute = new MapRouteView(routeResult.Route);
                viewOfRoute.RouteColor = accent1;
                viewOfRoute.OutlineColor = Colors.Transparent;


                MapControl1.Routes.Add(viewOfRoute);

                Geopoint c_loc = new Geopoint(pos2);
                MapControl1.Center = c_loc;
                /*
                await MapControl1.TrySetViewBoundsAsync(
                      routeResult.Route.BoundingBox,
                      null, MapAnimationKind.Linear);*/
            }

            var path2 = new List<EnhancedWaypoint>();

            path2.Add(new EnhancedWaypoint(new Geopoint(pos2), WaypointKind.Stop));
            path2.Add(new EnhancedWaypoint(new Geopoint(pos3), WaypointKind.Stop));

            MapRouteFinderResult routeResult2 = await MapRouteFinder.GetDrivingRouteFromEnhancedWaypointsAsync(path2);

            if (routeResult2.Status == MapRouteFinderStatus.Success)
            {
                int time;
                time = Convert.ToInt32(routeResult2.Route.EstimatedDuration.TotalMinutes);
                add_pane(time);

                MapRouteView viewOfRoute = new MapRouteView(routeResult2.Route);
                viewOfRoute.RouteColor = accent;
                viewOfRoute.OutlineColor = Colors.Transparent;

                MapControl1.Routes.Add(viewOfRoute);
            }

        }

        private void Border_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            Grid grid = (Grid)sender;

            grid.Width = 25;
            grid.Height = 25;
            grid.BorderThickness = new Thickness(3);

            Flyout fl = new Flyout { };
            TextBlock text_f = new TextBlock { Text = "Bus " + busNum.ToString() };
            fl.Content = text_f;
            FlyoutBase.SetAttachedFlyout(grid, fl);
            FlyoutBase.ShowAttachedFlyout(grid);
        }

        private void Border_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            Grid grid = (Grid)sender;

            grid.Width = 20;
            grid.Height = 20;
            grid.BorderThickness = new Thickness(5);
        }

        private void add_pane(double time)
        {
            content_container.Children.Clear();

            Grid grid1 = new Grid();
            TextBlock text1 = new TextBlock { Text = "Bus " + busNum.ToString() + " will arrive in " + time.ToString() + " minutes", TextWrapping = TextWrapping.WrapWholeWords };
            grid1.Children.Add(text1);

            content_container.Children.Add(new ItemPane(170, 300, "Bus " + busNum.ToString() + " is on time", HorizontalAlignment.Left, grid1, "", ""));
        }

        private const int busNum = 244;
    }
}