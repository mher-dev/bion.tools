using BionCore.Tools.Console;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BionCore.Tools
{
    public static class BConsole
    {

        [ThreadStatic]
        private static BCOutputStalkerManager _OutputStalker;
        internal static BCOutputStalkerManager OutputStalker
        {
            get
            {
                if (_OutputStalker == null)
                {
                    if (System.Console.Out is BCOutputStalkerManager)
                    {
                        _OutputStalker = (BCOutputStalkerManager)System.Console.Out;
                    }
                    else
                    {
                        _OutputStalker = new BCOutputStalkerManager(System.Console.Out);
                    }
                }

                return _OutputStalker;



            }
        }

        public static void BeginStalking()
        {
            System.Console.SetOut(OutputStalker);
        }
        #region ######## Metodos TextWriter ########
        public static Encoding Encoding
        {
            get
            {
                return OutputStalker.Encoding;
            }
        }

        public static IFormatProvider FormatProvider
        {
            get
            {
                return OutputStalker.FormatProvider;
            }
        }

        public static string NewLine
        {
            get
            {
                return OutputStalker.NewLine;
            }

            set
            {
                OutputStalker.NewLine = value;
            }
        }

        private static string _defaultTabSpacing { get; } = "    ";

        [ThreadStatic]
        private static string _threadTabSpacing;
        private static bool _hasThreadTabSpacing;

        public static string TabSpacing
        {
            get
            {
                if (_hasThreadTabSpacing)
                    return _threadTabSpacing;
                return _defaultTabSpacing;
            }
            set
            {
                _hasThreadTabSpacing = true;
                _threadTabSpacing = _defaultTabSpacing;
            }
        }

        public static List<IBCOutputExtended> StalkingOutputs
        {
            get
            {
                return OutputStalker.StalkingOutputs;
            }

            set
            {
                OutputStalker.StalkingOutputs = value;
            }
        }

        public static void Close()
        {
            OutputStalker.Close();
        }

        public static void Flush()
        {
            OutputStalker.Flush();
        }

        public static Task FlushAsync()
        {
            return OutputStalker.FlushAsync();
        }

        public static void Write(bool value)
        {
            OutputStalker.Write(value);
        }

        public static void Write(char value)
        {
            OutputStalker.Write(value);
        }

        public static void Write(ulong value)
        {
            OutputStalker.Write(value);
        }

        public static void Write(char[] buffer)
        {
            OutputStalker.Write(buffer);
        }

        public static void Write(float value)
        {
            OutputStalker.Write(value);
        }

        public static void Write(int value)
        {
            OutputStalker.Write(value);
        }

        public static void Write(decimal value)
        {
            OutputStalker.Write(value);
        }

        public static void Write(double value)
        {
            OutputStalker.Write(value);
        }

        public static void Write(long value)
        {
            OutputStalker.Write(value);
        }

        public static void Write(uint value)
        {
            OutputStalker.Write(value);
        }

        public static void Write(string value)
        {
            OutputStalker.Write(value);
        }

        public static void Write(object value)
        {
            OutputStalker.Write(value);
        }

        public static void Write(string format, object arg0)
        {
            OutputStalker.Write(format, arg0);
        }

        public static void SetFormat(BCTextFormats format)
        {
            OutputStalker.SetTextFormat(format);
        }

        public static void Write(string format, params object[] arg)
        {
            OutputStalker.Write(format, arg);
        }

        public static void Write(char[] buffer, int index, int count)
        {
            OutputStalker.Write(buffer, index, count);
        }

        public static void Write(string format, object arg0, object arg1)
        {
            OutputStalker.Write(format, arg0, arg1);
        }

        public static void Write(string format, object arg0, object arg1, object arg2)
        {
            OutputStalker.Write(format, arg0, arg1, arg2);
        }

        public static Task WriteAsync(char value)
        {
            return OutputStalker.WriteAsync(value);
        }

        public static Task WriteAsync(string value)
        {
            return OutputStalker.WriteAsync(value);
        }

        public static Task WriteAsync(char[] buffer, int index, int count)
        {
            return OutputStalker.WriteAsync(buffer, index, count);
        }

        public static void WriteLine()
        {
            OutputStalker.WriteLine();
        }

        public static void WriteLine(bool value)
        {
            OutputStalker.WriteLine(value);
        }

        public static void WriteLine(uint value)
        {
            OutputStalker.WriteLine(value);
        }

        public static void WriteLine(long value)
        {
            OutputStalker.WriteLine(value);
        }

        public static void WriteLine(object value)
        {
            OutputStalker.WriteLine(value);
        }

        public static void WriteLine(char[] buffer)
        {
            OutputStalker.WriteLine(buffer);
        }

        public static void WriteLine(char value)
        {
            OutputStalker.WriteLine(value);
        }

        public static void WriteLine(decimal value)
        {
            OutputStalker.WriteLine(value);
        }

        public static void WriteLine(int value)
        {
            OutputStalker.WriteLine(value);
        }

        public static void WriteLine(ulong value)
        {
            OutputStalker.WriteLine(value);
        }

        public static void WriteLine(float value)
        {
            OutputStalker.WriteLine(value);
        }

        public static void WriteLine(double value)
        {
            OutputStalker.WriteLine(value);
        }

        public static void WriteLine(string value)
        {
            OutputStalker.WriteLine(value);
        }

        public static void WriteLine(string format, object arg0)
        {
            OutputStalker.WriteLine(format, arg0);
        }

        public static void WriteLine(string format, params object[] arg)
        {
            OutputStalker.WriteLine(format, arg);
        }

        public static void WriteLine(char[] buffer, int index, int count)
        {
            OutputStalker.WriteLine(buffer, index, count);
        }

        public static void WriteLine(string format, object arg0, object arg1)
        {
            OutputStalker.WriteLine(format, arg0, arg1);
        }

        public static void WriteLine(string format, object arg0, object arg1, object arg2)
        {
            OutputStalker.WriteLine(format, arg0, arg1, arg2);
        }

        public static Task WriteLineAsync()
        {
            return OutputStalker.WriteLineAsync();
        }

        public static Task WriteLineAsync(char value)
        {
            return OutputStalker.WriteLineAsync(value);
        }

        public static Task WriteLineAsync(string value)
        {
            return OutputStalker.WriteLineAsync(value);
        }

        public static Task WriteLineAsync(char[] buffer, int index, int count)
        {
            return OutputStalker.WriteLineAsync(buffer, index, count);
        }
        #endregion [######## Metodos TextWriter ########]

        #region -------- SET COLOR --------
        public static void SetTextColor(IBCColorExtended color)
        {
            OutputStalker.SetTextColor(color);
        }

        public static void SetTextColor(ConsoleColor color)
        {
            OutputStalker.SetTextColor(new BCColorExtended(color));
        }

        public static void SetBackgroundColor(IBCColorExtended color)
        {
            OutputStalker.SetBackgroundColor(color);
        }

        public static void SetBackgroundColor(ConsoleColor color)
        {
            OutputStalker.SetBackgroundColor(new BCColorExtended(color));
        }
        #endregion [-------- SET COLOR --------]

        #region -------- RESET COLOR --------
        public static void ResetTextColor()
        {
            OutputStalker.SetTextColor(new BCColorExtended());
        }

        public static void ResetBackgroundColor()
        {
            OutputStalker.SetBackgroundColor(new BCColorExtended());
        }
        #endregion [-------- RESET COLOR --------]

        #region -------- TAB SPACING --------
        public static void AddTabSpacing()
        {
            OutputStalker.SetTabSpacing(OutputStalker.GetTabSpacing() + 1);
        }
        public static void RemoveTabSpacing()
        {
            OutputStalker.SetTabSpacing(OutputStalker.GetTabSpacing() - 1);
        }

        #endregion [-------- TAB SPACING --------]


        #region -------- COLOR AMBIT --------
        public static IBCColorAmbit TextColorAmbit(IBCColorExtended color)
        {
            return new BCTextColorAmbit(OutputStalker, color);
        }
        public static IBCColorAmbit TextColorAmbit(ConsoleColor color)
        {
            return new BCTextColorAmbit(OutputStalker, new BCColorExtended(color));
        }

        public static IBCColorAmbit BackgroundColorAmbit(IBCColorExtended color)
        {
            return new BCBackgroundColorAmbit(OutputStalker, color);
        }
        public static IBCColorAmbit BackgroundColorAmbit(ConsoleColor color)
        {
            return new BCBackgroundColorAmbit(OutputStalker, new BCColorExtended(color));
        }
        #endregion [-------- COLOR AMBIT --------]

    }
}
