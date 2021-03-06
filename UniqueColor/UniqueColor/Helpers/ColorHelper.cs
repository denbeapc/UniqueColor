﻿using System;
using Xamarin.Forms;

namespace UniqueColor.Helpers
{
    public class ColorHelper
    {
        public string GetHexString(Color color)
        {
            var red = (int)(color.R * 255);
            var green = (int)(color.G * 255);
            var blue = (int)(color.B * 255);
            var hex = $"#{red:X2}{green:X2}{blue:X2}";

            return hex;
        }
    }
}
