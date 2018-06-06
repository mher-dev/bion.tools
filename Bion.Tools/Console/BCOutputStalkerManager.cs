using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bion.Tools.Console
{
    using Console.Exceptions;
    using TSelfType = BCOutputStalkerManager;
    using ConsoleColor = System.ConsoleColor;
    using Console;
    using Tools.Exceptions;

    internal class BCOutputStalkerManager : TextWriter, IBCOutputStalkerManager
    {
        #region ======== Constructor ========
        public BCOutputStalkerManager(TextWriter standardOutput)
        {
            this.ConsoleOutput = new BCOutputWrapper(standardOutput);
        }

        public static TextWriter CreateOutput(Stream outputStream)
        {
            return new StreamWriter(outputStream);
        }
        #endregion [======== Constructor ========]


        #region ######## Propiedades ########
        private BCOutputWrapper ConsoleOutput { get; set; }

        public virtual BCTextFormats TextFormat { get; protected set; }

        #region -------- StalkingOutputs --------

        private List<IBCOutputExtended> _StalkingOutputs;
        public List<IBCOutputExtended> StalkingOutputs
        {
            get { return this._StalkingOutputs ?? (this._StalkingOutputs = new List<IBCOutputExtended>()); }
            set { this._StalkingOutputs = value; }
        }
        #endregion [-------- StalkingOutputs --------]

        public override Encoding Encoding { get { return ConsoleOutput.Encoding; } }

        private int tabDents = 0;
        #endregion [######## Propiedades ########]



        #region ######## Funciones de sobrecarga ########
        #region ======== Funciones y propiedades genericas ========
        public override IFormatProvider FormatProvider { get { return this.ConsoleOutput.FormatProvider; } }
        public override string NewLine
        {
            get { return this.ConsoleOutput.NewLine; }
            set { throw new BException<NotImplementedException>(); }
        }

        public int BUID { get; } = BionTools.GenerateBUID();

        public override void Close()
        {
            string exceptionText = "Error while closing one or more streams. See InnerExceptions for more details.";


            this.GenericAction(c => c.Close, exceptionText);

        }

        public override void Flush()
        {
            string exceptionText = "Error while flushing one or more streams. See InnerExceptions for more details.";

            this.GenericAction(c => c.Close, exceptionText);
        }

        protected override void Dispose(bool disposing)
        {
            string exceptionText = ("Error while flushing one or more streams. See InnerExceptions for more details.");
            this.GenericAction(c => c.Close, exceptionText);
        }
        public override Task FlushAsync()
        {
            return new Task(() =>
            {
                this.Flush();
            });
        }


        #endregion [======== Funciones y propiedades genericas ========]

        //#region ######## TextFormating ########

        //private int _NextPosition(string text, int posiblePosition)
        //{
        //    var textLength = text.Length;
        //    if (text[posiblePosition] == '\r')
        //    {
        //        if ((posiblePosition + 1) >= textLength)
        //            return posiblePosition + 2;
        //        return posiblePosition + 1;
        //    }

        //    return posiblePosition;
        //}
        //public void FormatText(int columnNumber, string text, out string formatedText, BCTextFormats? textFormat = null)
        //{
        //    formatedText = "";

        //    var tab = _GetTabSpaces() ?? "";
        //    var tabTotalLength = tab?.Length ?? 0;

        //    var textLength = text.Length;
        //    int maxLineLength = tabTotalLength;

        //    if (maxLineLength >= columnNumber)
        //        maxLineLength = columnNumber - 1;
        //    else
        //        maxLineLength = columnNumber;

        //    int lineLength = 0;
        //    for (int i = 0; i < textLength; i++)
        //    {
        //        var nextPosition = _NextPosition(text, i);

        //        if (nextPosition != i || lineLength >= maxLineLength)
        //        {
        //            lineLength = tab.Length;
        //            formatedText += "\r\n" + tab;

        //            if (i != nextPosition)
        //            {
        //                i = nextPosition;
        //                continue;
        //            }
        //        }
        //        lineLength++;
        //        formatedText += text[i];

        //    }
        //}
        //#endregion [######## TextFormating ########]


        #region ######## GENERIC ACTIONS ########
        private void GenericSimpleAction(Action<IBCOutputExtended> action, string exceptionText = null)
        {

            exceptionText = exceptionText ?? "Error while writing in one or more streams. See InnerExceptions for more details.";
            BCException genericException = new BCException(exceptionText);

            try
            {

                this.AddDents(this.ConsoleOutput);
                action(this.ConsoleOutput);
            }
            catch (Exception ex)
            {
                genericException.AddExceptions(ex);
            }


            this.StalkingOutputs.ForEach(stalkingOutput =>
            {
                try
                {
                    if (stalkingOutput != null)
                    {
                        this.AddDents(stalkingOutput);
                        action(stalkingOutput);
                    }
                }
                catch (Exception ex)
                {
                    genericException.AddExceptions(ex);
                }
            });

            if (genericException.InnerExceptions.Any())
                throw genericException;

        }


        private void GenericAction(Func<IBCOutputExtended, Action> action, string exceptionText = null)
        {
            GenericSimpleAction(stream =>
            {
                if (stream != null)
                    action(stream)();
            }, exceptionText);
        }

        private void GenericAction<T>(T value, Func<IBCOutputExtended, Action<T>> action, string exceptionText = null)
        {
            GenericSimpleAction(stream =>
            {
                if (stream != null)
                    action(stream)(value);
            }, exceptionText);
        }

        private void GenericAction<T1, T2>(T1 value1, T2 value2, Func<IBCOutputExtended, Action<T1, T2>> action, string exceptionText = null)
        {
            GenericSimpleAction(stream =>
            {
                if (stream != null)
                    action(stream)(value1, value2);
            }, exceptionText);
        }

        private void GenericAction<T1, T2, T3>(T1 value1, T2 value2, T3 value3, Func<IBCOutputExtended, Action<T1, T2, T3>> action, string exceptionText = null)
        {
            GenericSimpleAction(stream =>
            {
                if (stream != null)
                    action(stream)(value1, value2, value3);
            }, exceptionText);
        }

        private void GenericAction<T1, T2, T3, T4>(T1 value1, T2 value2, T3 value3, T4 value4, Func<IBCOutputExtended, Action<T1, T2, T3, T4>> action, string exceptionText = null)
        {
            GenericSimpleAction(stream =>
            {
                if (stream != null)
                    action(stream)(value1, value2, value3, value4);
            }, exceptionText);
        }

        #endregion [######## GENERIC ACTIONS ########]


        #region ======== Write ========




        public override void Write(string value)
        {
            this.GenericAction(value, c => c.Write);
        }

        public override void Write(decimal value)
        {
            this.GenericAction(value, c => c.Write);
        }

        public override void Write(double value)
        {
            this.GenericAction(value, c => c.Write);
        }

        public override void Write(float value)
        {
            this.GenericAction(value, c => c.Write);
        }

        public override void Write(object value)
        {
            this.GenericAction(value, c => c.Write);
        }

        public override void Write(long value)
        {
            this.GenericAction(value, c => c.Write);
        }

        public override void Write(uint value)
        {
            this.GenericAction(value, c => c.Write);
        }

        public override void Write(int value)
        {
            this.GenericAction(value, c => c.Write);
        }

        public override void Write(bool value)
        {
            this.GenericAction(value, c => c.Write);
        }

        public override void Write(ulong value)
        {
            this.GenericAction(value, c => c.Write);
        }

        public override void Write(char[] buffer)
        {
            this.GenericAction(buffer, c => c.Write);
        }

        public override void Write(char value)
        {
            this.GenericAction(value, c => c.Write);
        }

        public override void Write(string format, object arg0)
        {
            this.GenericAction(format, arg0, c => c.Write);
        }

        public override void Write(string format, params object[] arg)
        {
            this.GenericAction(format, arg, c => c.Write);
        }

        public override void Write(string format, object arg0, object arg1)
        {
            this.GenericAction(format, arg0, arg1, c => c.Write);
        }

        public override void Write(char[] buffer, int index, int count)
        {
            this.GenericAction(buffer, index, count, c => c.Write);
        }

        public override void Write(string format, object arg0, object arg1, object arg2)
        {
            this.GenericAction(format, arg0, arg1, arg2, c => c.Write);
        }

        public override Task WriteAsync(char value)
        {
            return new Task(() =>
            {
                this.Write(value);
            });
        }


        public override Task WriteAsync(string value)
        {
            return new Task(() =>
            {
                this.Write(value);
            });
        }

        public override Task WriteAsync(char[] buffer, int index, int count)
        {
            return new Task(() =>
            {
                this.Write(buffer, index, count);
            });
        }

        #endregion [======== Write ========]

        #region ======== Write Line ========
        public override void WriteLine()
        {
            this.GenericAction(c => c.WriteLine);
        }

        public override void WriteLine(object value)
        {
            this.GenericAction(value, c => c.WriteLine);
        }
        public override void WriteLine(string value)
        {
            GenericSimpleAction(stream =>
            {
                if (stream != null)
                {
                    var columnCount = stream.GetColumnCount();
                    if (columnCount.HasValue && stream.GetTextFormat() != BCTextFormats.None)
                    {
                        string output;
                        if (stream.TryFormatText(value, out output, stream.GetTextFormat()))
                            value = output;
                        
                    }

                    stream.WriteLine(value);
                }
            }, null);
        }

        public override void WriteLine(decimal value)
        {
            this.GenericAction(value, c => c.WriteLine);
        }

        public override void WriteLine(int value)
        {
            this.GenericAction(value, c => c.WriteLine);
        }

        public override void WriteLine(float value)
        {
            this.GenericAction(value, c => c.WriteLine);
        }

        public override void WriteLine(char value)
        {
            this.GenericAction(value, c => c.WriteLine);
        }

        public override void WriteLine(double value)
        {
            this.GenericAction(value, c => c.WriteLine);
        }

        public override void WriteLine(bool value)
        {
            this.GenericAction(value, c => c.WriteLine);
        }

        public override void WriteLine(uint value)
        {
            this.GenericAction(value, c => c.WriteLine);
        }

        public override void WriteLine(long value)
        {
            this.GenericAction(value, c => c.WriteLine);
        }

        public override void WriteLine(char[] buffer)
        {
            this.GenericAction(buffer, c => c.WriteLine);
        }

        public override void WriteLine(ulong value)
        {
            this.GenericAction(value, c => c.WriteLine);
        }

        public override void WriteLine(string format, object arg0)
        {
            this.GenericAction(format, arg0, c => c.WriteLine);
        }

        public override void WriteLine(string format, params object[] arg)
        {
            this.GenericAction(format, arg, c => c.WriteLine);

        }

        public override void WriteLine(string format, object arg0, object arg1)
        {
            this.GenericAction(format, arg0, arg1, c => c.WriteLine);
        }
        public override void WriteLine(char[] buffer, int index, int count)
        {
            this.GenericAction(buffer, index, count, c => c.WriteLine);

        }

        public override void WriteLine(string format, object arg0, object arg1, object arg2)
        {
            this.GenericAction(format, arg0, arg1, arg2, c => c.WriteLine);

        }

        public override Task WriteLineAsync()
        {
            return new Task(() =>
            {
                this.WriteLine();
            });
        }

        public override Task WriteLineAsync(char value)
        {
            return new Task(() =>
            {
                this.WriteLine(value);
            });
        }

        public override Task WriteLineAsync(string value)
        {
            return new Task(() =>
            {
                this.WriteLine(value);
            });
        }

        public override Task WriteLineAsync(char[] buffer, int index, int count)
        {
            return new Task(() =>
            {
                this.WriteLine(buffer, index, count);
            });
        }
        #endregion [======== Write Line ========]
        #endregion [######## Funciones de sobrecarga ########]


        #region ######## Metodos ########


        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="text"></param>
        public void WriteText(IBCText text)
        {
            throw new BENotImplementedException();
        }


        #region ######## CONSOLE MANAGMENT ########
        public void SetTabSpacing(int dents)
        {
            this.ConsoleOutput.SetTabSpacing(dents);
            this.StalkingOutputs.ForEach(output =>
            {
                output?.SetTabSpacing(dents);
            });
        }

        public int GetTabSpacing()
        {
            return this.ConsoleOutput.GetTabSpacing();
        }


        #region -------- AddDents --------
        private void AddDents(IBCOutputExtended output)
        {
            if (output != null)
                this._AddDents(output, c => c.Write);
        }

        //private void AddDents(TextWriter output)
        //{
        //    if (output != null)
        //        this._AddDents(output, c => c.Write);
        //}

        private void _AddDents<T>(T stream, Func<T, Action<string>> writer)
            where T : IBCOutputExtended
        {
            var dents = _GetTabSpaces(stream.GetTabSpacing());
            if (dents != null)
                writer(stream)?.Invoke(dents);
        }

        private string _GetTabSpaces(int tabDents)
        {
            string dents = null;
            for (int i = 0; i < tabDents; i++)
                dents += BConsole.TabSpacing;

            return dents;
        }
        #endregion [-------- AddDents --------]

        //private void _SetConsoleTextColor(IBCColorExtended color)
        //{
        //    if (color.HasColor)
        //        System.Console.ForegroundColor = color.ToConsoleColor().Value;
        //    else
        //    {
        //        var backgroundColor = System.Console.BackgroundColor;
        //        System.Console.ResetColor();
        //        System.Console.BackgroundColor = backgroundColor;
        //    }
        //}

        //private void _SetBackgroundTextColor(IBCColorExtended color)
        //{
        //    if (color.HasColor)
        //        System.Console.BackgroundColor = color.ToConsoleColor().Value;
        //    else
        //    {
        //        var oldColor = System.Console.ForegroundColor;
        //        System.Console.ResetColor();
        //        System.Console.ForegroundColor = oldColor;
        //    }
        //}

        #endregion [######## Metodos ########]
        #endregion [######## CONSOLE MANAGMENT ########]

        #region ======== SET/GET Color ========
        public void SetTextColor(IBCColorExtended color)
        {
            //this._SetConsoleTextColor(color);
            this.ConsoleOutput.SetTextColor(color);
            this.StalkingOutputs.ForEach(output =>
            {
                output?.SetTextColor(color);
            });
        }

        public void SetBackgroundColor(IBCColorExtended color)
        {
            this.ConsoleOutput.SetBackgroundColor(color);
            this.StalkingOutputs.ForEach(output =>
            {
                output?.SetBackgroundColor(color);
            });
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
            return this.ConsoleOutput.GetRowCount();
            //return null;
        }

        public int? GetColumnCount()
        {
            return this.ConsoleOutput.GetColumnCount();

        }

        public void SetTextFormat(BCTextFormats format)
        {
            string exceptionText = "Error while asignating text format to one or more streams";
            this.ConsoleOutput.SetTextFormat(format);
            this.StalkingOutputs.ForEach(output =>
            {
                output?.SetTextFormat(format);
            });
            //this.GenericAction(format, c => c.SetT, exceptionText);
        }

        public BCTextFormats GetTextFormat()
        {
            return this.ConsoleOutput.GetTextFormat();
        }

        public bool TryFormatText(string text, out string formatedText, BCTextFormats? textFormat = null, object parameter = null)
        {
            return this.ConsoleOutput.TryFormatText(text, out formatedText, textFormat, parameter);
        }

        #endregion [======== SET/GET Color ========]






    }
}
