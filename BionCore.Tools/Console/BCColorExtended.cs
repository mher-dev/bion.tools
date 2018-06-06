using BionCore.Tools.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BionCore.Tools.Console
{
    public class BCColorExtended : IBCColorExtended
    {
        public BCColorExtended() { }
        public BCColorExtended(ConsoleColor? color) { this.Color = color; }

        #region ######## Propiedades ########
        public virtual ConsoleColor? Color { get; set; }

        public string BUID { get; } = BionTools.GenerateBUID();

        public virtual string Name { get { return this.Color.HasValue ? this.Color.Value.ToString() : null; } }
        public virtual bool HasColor { get { return this.Color.HasValue; } }
        #endregion [######## Propiedades ########]


        #region ######## Metodos ########

        public ConsoleColor? ToConsoleColor()
        {
            return this.Color;
        }

        public string ToRGB()
        {
            if (this.Color.HasValue)
                return ToRGB(this.Color.Value);
            return null;
        }
        public static string ToRGB(ConsoleColor color)
        {
            string result = "";

            switch (color)
            {
                case ConsoleColor.Black: result = "000000"; break;
                case ConsoleColor.DarkBlue: result = "00008B"; break;
                case ConsoleColor.DarkGreen: result = "006400"; break;
                case ConsoleColor.DarkCyan: result = "008B8B"; break;
                case ConsoleColor.DarkRed: result = "8B0000"; break;
                case ConsoleColor.DarkMagenta: result = "8B008B"; break;
                case ConsoleColor.DarkYellow: result = "000000"; break;
                case ConsoleColor.Gray: result = "808080"; break;
                case ConsoleColor.DarkGray: result = "A9A9A9"; break;
                case ConsoleColor.Blue: result = "0000FF"; break;
                case ConsoleColor.Green: result = "008000"; break;
                case ConsoleColor.Cyan: result = "00FFFF"; break;
                case ConsoleColor.Red: result = "FF0000"; break;
                case ConsoleColor.Magenta: result = "FF00FF"; break;
                case ConsoleColor.Yellow: result = "FFFF00"; break;
                case ConsoleColor.White: result = "FFFFFF"; break;

                default: throw new BENotImplementedException("There is no valid conversion for `" + color + "´ to RGB");
            }

            return result;

        }

        #endregion [######## Metodos ########]

        #region ######## Operadores ########
        public static implicit operator ConsoleColor?(BCColorExtended color)
        {
            return color?.ToConsoleColor();
        }

        public static implicit operator BCColorExtended(ConsoleColor? color)
        {
            return new BCColorExtended() { Color = color };
        }
        #endregion [######## Operadores ########]

    }
}
