using Mapsui.Styles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Element.Classes
{
   public class MapsUIView: Xamarin.Forms.View
    {
        public Mapsui.Map NativeMap { get; set; }

        public MapsUIView()
        {
            NativeMap = new Mapsui.Map();
            NativeMap.BackColor = Color.Black;
        }
    }
}
