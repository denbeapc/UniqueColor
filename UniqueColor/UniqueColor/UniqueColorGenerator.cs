using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace UniqueColor
{
    public class UniqueColorGenerator
    {
        const int MaxColors = 7;
        private int NumberOfColors { get; set; }

        public UniqueColorGenerator(int numberOfColors)
        {
            NumberOfColors = numberOfColors;
        }

        public List<string> GetColors()
        {
            var Colors = new List<string>(NumberOfColors);
            var HSVs = new List<float[]>(NumberOfColors);

            for (int i = 0; i < MaxColors; i++)
            {
                HSVs.Add(new float[] { (float)(i * 61.8033988749895),
                                       (float)50,
                                       (float)Math.Sqrt(100 - ((i * 61.8033988749895) % 50)) });
            }

            int[] RGB = new int[3];
            foreach (var HSV in HSVs)
            {
                if (Array.IndexOf(HSVs.ToArray(), HSV) >= NumberOfColors)
                    break;

                RGBFromHSV(HSV, out RGB);
                var hexStr = GetHexString(Xamarin.Forms.Color.FromRgb(RGB[0], RGB[1], RGB[2]));
                Colors.Add(hexStr);
            }

            double alphaVal = 0.50;
            double tintVal = 1;
            int tintCount = 1;
            // tintCount 0, 1 makes tintVal 1
            // tintCount 2, 3 makes tintVal 0.66
            // reset tintCount to 0 when tintCount is 4
            int count = 0;
            while (Colors.Count < NumberOfColors)
            {
                if (count >= MaxColors)
                {
                    count = 0;
                    alphaVal -= 0.50;
                    tintCount++;
                }
                if (alphaVal < 0.50)
                {
                    alphaVal = 1;
                }

                if (tintCount == 2)
                {
                    tintVal = 0.80;
                }
                else if (tintCount == 3)
                {
                    tintVal = 1;
                }
                else if (tintCount == 4)
                {
                    tintCount = 0;
                }

                var tempColor = Color.FromHex(Colors[count]);
                var newColor = Color.FromRgba(tempColor.R * tintVal, tempColor.G * tintVal, tempColor.B * tintVal, alphaVal);
                var newHex = GetHexString(newColor);

                Colors.Add(newHex);
                count++;
            }

            return Colors;
        }

        private string GetHexString(Color color)
        {
            var red = (int)(color.R * 255);
            var green = (int)(color.G * 255);
            var blue = (int)(color.B * 255);
            var alpha = (int)(color.A * 255);
            var hex = $"#{alpha:X2}{red:X2}{green:X2}{blue:X2}";

            return hex;
        }

        private void RGBFromHSV(float[] HSV, out int[] RGB)
        {
            // Init HSV float values
            // If H is greater than 360 degrees, clamp it to 360
            float H = (HSV[0] > 360) ? 360 : HSV[0], S = HSV[1], V = HSV[2];

            // Init RGB int values
            int R = 0, G = 0, B = 0;

            // Find the Chroma
            float chroma = V * S;

            float hPrime = H / 60;
            var X = chroma * (1 - Math.Abs((H % 2) - 1));

            if (hPrime >= 0 && hPrime <= 1)
            {
                R = (int)chroma;
                G = (int)X;
                B = 0;
            }
            else if (hPrime > 1 && hPrime <= 2)
            {
                R = (int)X;
                G = (int)chroma;
                B = 0;
            }
            else if (hPrime > 2 && hPrime <= 3)
            {
                R = 0;
                G = (int)chroma;
                B = (int)X;
            }
            else if (hPrime > 3 && hPrime <= 4)
            {
                R = 0;
                G = (int)X;
                B = (int)chroma;
            }
            else if (hPrime > 4 && hPrime <= 5)
            {
                R = (int)X;
                G = 0;
                B = (int)chroma;
            }
            else if (hPrime > 5 && hPrime <= 6)
            {
                R = (int)chroma;
                G = 0;
                B = (int)X;
            }

            var m = V - chroma;

            var rPrime = int.Parse((Math.Abs((int)((R + m) * 255))).ToString().Substring(0, 3));
            var gPrime = int.Parse((Math.Abs((int)((G + m) * 255))).ToString().Substring(0, 3));
            var bPrime = int.Parse((Math.Abs((int)((B + m) * 255))).ToString().Substring(0, 3));

            // Set RGB array 
            RGB = new int[] { rPrime, gPrime, bPrime };
        }
    }
}
