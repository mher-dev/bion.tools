using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BionCore.Tools.Console;
using BionCore.Tools.Exceptions;

namespace BionCore.Tools.Console
{
    internal class BCOutputWrapper : IBCOutputExtended
    {
        internal BCOutputWrapper(TextWriter consoleOutput)
        {
            this.ConsoleOutput = consoleOutput;
        }

        private TextWriter ConsoleOutput { get; set; }

        public Encoding Encoding { get { return ConsoleOutput.Encoding; } }

        public IFormatProvider FormatProvider { get { return ConsoleOutput.FormatProvider; } }

        public string NewLine
        {
            get { return ConsoleOutput.NewLine; }
            set { ConsoleOutput.NewLine = value; }
        }

        internal enum FormatOptions { WithOutEndline, WithEndLine }

        public string BUID { get; } = BionTools.GenerateBUID();

        public BCTextFormats TextFormat { get; protected set; }

        public int TabDents { get; protected set; }

        #region ######## ITextWriter ########
        public void Close()
        {
            ConsoleOutput.Close();
        }

        public void Dispose()
        {
            ConsoleOutput.Dispose();
        }

        public void Flush()
        {
            ConsoleOutput.Flush();
        }

        public Task FlushAsync()
        {
            return ConsoleOutput.FlushAsync();
        }

        public void Write(decimal value)
        {
            ConsoleOutput.Write(value);
        }

        public void Write(bool value)
        {
            ConsoleOutput.Write(value);
        }

        public void Write(char value)
        {
            ConsoleOutput.Write(value);
        }

        public void Write(int value)
        {
            ConsoleOutput.Write(value);
        }

        public void Write(uint value)
        {
            ConsoleOutput.Write(value);
        }

        public void Write(char[] buffer)
        {
            ConsoleOutput.Write(buffer);
        }

        public void Write(double value)
        {
            ConsoleOutput.Write(value);
        }

        public void Write(float value)
        {
            ConsoleOutput.Write(value);
        }

        public void Write(ulong value)
        {
            ConsoleOutput.Write(value);
        }

        public void Write(long value)
        {
            ConsoleOutput.Write(value);
        }

        public void Write(string value)
        {
            ConsoleOutput.Write(value);
        }

        public void Write(object value)
        {
            ConsoleOutput.Write(value);
        }

        public void Write(string format, object arg0)
        {
            ConsoleOutput.Write(format, arg0);
        }

        public void Write(string format, params object[] arg)
        {
            ConsoleOutput.Write(format, arg);
        }

        public void Write(char[] buffer, int index, int count)
        {
            ConsoleOutput.Write(buffer, index, count);
        }

        public void Write(string format, object arg0, object arg1)
        {
            ConsoleOutput.Write(format, arg0, arg1);
        }

        public void Write(string format, object arg0, object arg1, object arg2)
        {
            ConsoleOutput.Write(format, arg0, arg1, arg2);
        }

        public Task WriteAsync(char value)
        {
            return ConsoleOutput.WriteAsync(value);
        }

        public Task WriteAsync(string value)
        {
            return ConsoleOutput.WriteAsync(value);
        }

        public Task WriteAsync(char[] buffer, int index, int count)
        {
            return ConsoleOutput.WriteAsync(buffer, index, count);
        }

        public void WriteLine()
        {
            ConsoleOutput.WriteLine();
        }

        public void WriteLine(char value)
        {
            ConsoleOutput.WriteLine(value);
        }

        public void WriteLine(bool value)
        {
            ConsoleOutput.WriteLine(value);
        }

        public void WriteLine(uint value)
        {
            ConsoleOutput.WriteLine(value);
        }

        public void WriteLine(long value)
        {
            ConsoleOutput.WriteLine(value);
        }

        public void WriteLine(object value)
        {
            ConsoleOutput.WriteLine(value);
        }

        public void WriteLine(char[] buffer)
        {
            ConsoleOutput.WriteLine(buffer);
        }

        public void WriteLine(decimal value)
        {
            ConsoleOutput.WriteLine(value);
        }

        public void WriteLine(float value)
        {
            ConsoleOutput.WriteLine(value);
        }

        public void WriteLine(ulong value)
        {
            ConsoleOutput.WriteLine(value);
        }

        public void WriteLine(int value)
        {
            ConsoleOutput.WriteLine(value);
        }

        public void WriteLine(double value)
        {
            ConsoleOutput.WriteLine(value);
        }

        public void WriteLine(string value)
        {
            ConsoleOutput.WriteLine(value);
        }

        public void WriteLine(string format, object arg0)
        {
            ConsoleOutput.WriteLine(format, arg0);
        }

        public void WriteLine(string format, params object[] arg)
        {
            ConsoleOutput.WriteLine(format, arg);
        }

        public void WriteLine(char[] buffer, int index, int count)
        {
            ConsoleOutput.WriteLine(buffer, index, count);
        }

        public void WriteLine(string format, object arg0, object arg1)
        {
            ConsoleOutput.WriteLine(format, arg0, arg1);
        }

        public void WriteLine(string format, object arg0, object arg1, object arg2)
        {
            ConsoleOutput.WriteLine(format, arg0, arg1, arg2);
        }

        public Task WriteLineAsync()
        {
            return ConsoleOutput.WriteLineAsync();
        }

        public Task WriteLineAsync(char value)
        {
            return ConsoleOutput.WriteLineAsync(value);
        }

        public Task WriteLineAsync(string value)
        {
            return ConsoleOutput.WriteLineAsync(value);
        }

        public Task WriteLineAsync(char[] buffer, int index, int count)
        {
            return ConsoleOutput.WriteLineAsync(buffer, index, count);
        }
        #endregion [######## ITextWriter ########]


        private void _SetConsoleTextColor(IBCColorExtended color)
        {
            if (color.HasColor)
                System.Console.ForegroundColor = color.ToConsoleColor().Value;
            else
            {
                var backgroundColor = System.Console.BackgroundColor;
                System.Console.ResetColor();
                System.Console.BackgroundColor = backgroundColor;
            }
        }

        private void _SetBackgroundTextColor(IBCColorExtended color)
        {
            if (color.HasColor)
                System.Console.BackgroundColor = color.ToConsoleColor().Value;
            else
            {
                var oldColor = System.Console.ForegroundColor;
                System.Console.ResetColor();
                System.Console.ForegroundColor = oldColor;
            }
        }

        public void WriteText(IBCText text)
        {
            throw new BENotImplementedException();
        }

        public void SetTextColor(IBCColorExtended color)
        {
            this._SetConsoleTextColor(color);
        }

        public void SetBackgroundColor(IBCColorExtended color)
        {
            this._SetBackgroundTextColor(color);
        }




        public void SetTabSpacing(int dents)
        {
            this.TabDents = dents;
        }

        public int GetTabSpacing()
        {
            return this.TabDents;
        }

        public BCColorExtended GetTextColor()
        {
            return System.Console.ForegroundColor;
        }

        public BCColorExtended GetBackgroundColor()
        {
            return System.Console.BackgroundColor;
        }

        public int? GetRowCount()
        {
            try
            {
                if (System.Console.IsOutputRedirected)
                    return null;
                return System.Console.WindowHeight;
            }
            catch
            {
                return null;
            }
        }

        public int? GetColumnCount()
        {
            try
            {
                if (System.Console.IsOutputRedirected)
                    return null;
                return System.Console.WindowWidth;
            }
            catch
            {
                return null;
            }
        }

        public void SetTextFormat(BCTextFormats format)
        {
            this.TextFormat = format;
        }

        public BCTextFormats GetTextFormat()
        {
            return this.TextFormat;
        }

        #region ######## TextFormating ########

        private int _NextPosition(string text, int posiblePosition)
        {
            var textLength = text.Length;
            if (text[posiblePosition] == '\r')
            {
                if ((posiblePosition + 2) >= textLength)
                    return posiblePosition + 1;
                return posiblePosition + 2;
            }

            return posiblePosition;
        }

        private string _GetTabSpaces()
        {
            string dents = null;
            for (int i = 0; i < this.TabDents; i++)
                dents += BConsole.TabSpacing;

            return dents;
        }

        public bool TryFormatText(string text, out string formatedText, BCTextFormats? textFormat = null, object parameter = null)
        {
            formatedText = null;
            int? columnNumberNul = this.GetColumnCount();
            if (!columnNumberNul.HasValue)
                return false;

            bool withEndLine = (parameter is FormatOptions) ? ((FormatOptions)parameter) == FormatOptions.WithEndLine : false;

            formatedText = "";
            int columnNumber = columnNumberNul.Value;

            var tab = _GetTabSpaces() ?? "";
            var tabTotalLength = tab?.Length ?? 0;

            var textLength = text.Length;
            int maxLineLength = tabTotalLength;

            if (maxLineLength >= columnNumber)
                maxLineLength = columnNumber - 1;
            else
                maxLineLength = columnNumber - 0;

            int lineLength = tab.Length;
            for (int i = 0; i < textLength; i++)
            {
                var nextPosition = _NextPosition(text, i);

                if (nextPosition != i || lineLength >= maxLineLength)
                {
                    lineLength = tab.Length;

                    if (!(nextPosition != i && lineLength >= maxLineLength))
                        formatedText += withEndLine ? (Environment.NewLine + tab) : tab;
                    else
                    {

                    }

                    if (i != nextPosition)
                    {
                        i = nextPosition;
                        continue;
                    }
                }
                lineLength++;
                formatedText += text[i];

            }

            return true;
        }
        #endregion [######## TextFormating ########]
    }
}
