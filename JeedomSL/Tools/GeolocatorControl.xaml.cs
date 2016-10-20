using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using JeedomSL.Pluggin;
using System.Globalization;
using Microsoft.Phone.Maps.Services;
using System.Device.Location;
using Microsoft.Phone.Maps.Controls;
using System.Windows.Shapes;
using System.Windows.Media;

namespace JeedomSL.Tools
{
    public partial class GeolocatorControl : UserControl
    {
        List<GeoCoordinate> MyCoordinates = new List<GeoCoordinate>();

        public GeolocatorControl()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (Tag != null)
            {
                Cmd[] cmds = (Tag as Eqlogic).cmds;
                Cmd cmd = cmds.GetCmdByConfigModeName("travelDistance");
                if (cmd != null) LblTrajet.Text = cmd.ToString("");
                cmd = cmds.GetCmdByConfigModeName("travelTime");
                if (cmd != null) LblTemp.Text = cmd.ToString("");
                cmd = cmds.GetCmdByConfigModeName("distance");
                if (cmd != null) LblDistance.Text = cmd.ToString("");

                cmd = cmds.GetCmdByConfigModeName("dynamic");
                if (cmd != null)
                {
                    string name = cmd.name;
                    string coord = cmd.ToString("");
                    string[] tmp = coord.Split(',');
                    MyCoordinates.Add(new GeoCoordinate(double.Parse(tmp[0], CultureInfo.InvariantCulture), double.Parse(tmp[1], CultureInfo.InvariantCulture)));
                    cmd = cmds.GetCmdByConfigModeName("fixe");
                    if (cmd != null)
                    {
                        LblTitle.Text = cmd.name + "=>" + name;
                        coord = cmd.ToString("");
                        tmp = coord.Split(',');
                        MyCoordinates.Add(new GeoCoordinate(double.Parse(tmp[0], CultureInfo.InvariantCulture), double.Parse(tmp[1], CultureInfo.InvariantCulture)));
                        RouteQuery MyQuery = new RouteQuery();
                        MyQuery.TravelMode = TravelMode.Driving;
                        MyQuery.Waypoints = MyCoordinates;
                        MyQuery.QueryCompleted += MyQuery_QueryCompleted;
                        MyQuery.QueryAsync();
                    }

                       MapControl.ZoomLevel = 16;
                    MapControl.Center = MyCoordinates[0];
                }
            }
        }

        private void MyQuery_QueryCompleted(object sender, QueryCompletedEventArgs<Route> e)
        {
            if (e.Error == null)
            {
                Route MyRoute = e.Result;
                MapRoute MyMapRoute = new MapRoute(MyRoute);
                MyMapRoute.Color = (Colors.Blue);
                MapControl.AddRoute(MyMapRoute);
                #region Draw source location ellipse
                Ellipse myCircle = new Ellipse();
                myCircle.Fill = new SolidColorBrush(Colors.Blue);
                myCircle.Height = 20;
                myCircle.Width = 20;
                myCircle.Opacity = 50;
                MapOverlay myLocationOverlay = new MapOverlay();
                myLocationOverlay.Content = myCircle;
                myLocationOverlay.PositionOrigin = new Point(0.5, 0.5);
                myLocationOverlay.GeoCoordinate = MyCoordinates[0];
            MapLayer MylocationLayer = new MapLayer();
            MylocationLayer.Add(myLocationOverlay);
            MapControl.Layers.Add(MylocationLayer);
            #endregion
            #region Draw target location ellipse
            Ellipse CarCircle = new Ellipse();
            CarCircle.Fill = new SolidColorBrush(Colors.Red);
            CarCircle.Height = 20;
            CarCircle.Width = 20;
            CarCircle.Opacity = 50;
            MapOverlay CarLocationOverlay = new MapOverlay();
            CarLocationOverlay.Content = CarCircle;
            CarLocationOverlay.PositionOrigin = new Point(0.5, 0.5);
            CarLocationOverlay.GeoCoordinate = MyCoordinates[1];
                MapLayer CarlocationLayer = new MapLayer();
                CarlocationLayer.Add(CarLocationOverlay);
                MapControl.Layers.Add(CarlocationLayer);
                #endregion
               
            }
        }
    }
}
