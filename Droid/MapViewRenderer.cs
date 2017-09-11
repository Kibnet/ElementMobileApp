using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using Element.Classes;
using Element.Droid;

[assembly: ExportRenderer (typeof(MapsUIView), typeof(MapViewRenderer))]
namespace Element.Droid
{
    
    public class MapViewRenderer:ViewRenderer<MapsUIView,Mapsui.UI.Android.MapControl>
    {
        MapsUIView mapViewControl;
        Mapsui.UI.Android.MapControl mapNativeControl;

        protected override void OnElementChanged(ElementChangedEventArgs<MapsUIView> e)
        {
            base.OnElementChanged(e);

            if (mapViewControl == null && e.NewElement != null)
                mapViewControl = e.NewElement;

            if (mapNativeControl == null && mapViewControl != null)
            {
                mapNativeControl = new Mapsui.UI.Android.MapControl(Context,null);
                mapNativeControl.Map = mapViewControl.NativeMap;

                SetNativeControl(mapNativeControl);
            }

        }


    }
}