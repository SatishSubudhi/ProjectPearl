using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PearlFramework.Utilities
{
    class ColorUtilities
    {
        public static string Convert_RGBA_to_Hex(string color)
        {
            Console.WriteLine("btn bg color in convfunc" + color);
            string[] hexValue = color.Replace("rgba(", "").Replace(")", "").Split(',');

            Console.WriteLine("0 " + hexValue[0]);
            Console.WriteLine("1 " + hexValue[1]);
            Console.WriteLine("2 " + hexValue[2]);
            Console.WriteLine("3 " + hexValue[3]);

            int hexValue1 = Int16.Parse(hexValue[0]);
            hexValue[1] = hexValue[1].Trim();
            int hexValue2 = Int16.Parse(hexValue[1]);
            hexValue[2] = hexValue[2].Trim();
            int hexValue3 = Int16.Parse(hexValue[2]);

            string actualColor = string.Format("#{0:X2}{1:X2}{2:X2}", hexValue1, hexValue2, hexValue3);
            Console.WriteLine("btn bg color in convfunc" + actualColor);
            return actualColor;
        }
    }
}
