﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Extensions.TextRelated;
using Core.Mathematics;
using Core.Net.Impl;

namespace Core.Text.Formatter
{
    public class BingMapsLinkFormatter : ITextFormatter<IGeoLocation>
    {
        public string PinName { get; set; } = null;

        public void Write(IGeoLocation value, TextWriter writer)
        {
            var lat            = value.Latitude.ToString(CultureInfo.InvariantCulture);
            var lon            = value.Longitude.ToString(CultureInfo.InvariantCulture);
            
            writer.Write($"https://bing.com/maps/default.aspx?cp={lat}~{lon}&lvl=16&dir=0&sty=a&sp=point.{lat}_{lon}");
            if (!string.IsNullOrWhiteSpace(PinName))
            {
                var urlEncodedName = new DefaultUrlEncoder().Encode(PinName);
                writer.Write($"_{urlEncodedName}");
            }
        }
    }
}
