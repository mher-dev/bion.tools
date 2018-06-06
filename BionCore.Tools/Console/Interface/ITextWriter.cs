using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace BionCore.Tools
{
    public interface ITextWriter: IDisposable
    {
        Encoding Encoding { get; }
        IFormatProvider FormatProvider { get; }
        string NewLine { get; set; }


        void Close();
        void Flush();
        Task FlushAsync();
        void Write(object value);
        void Write(string value);
        void Write(long value);
        void Write(uint value);
        void Write(ulong value);
        void Write(char[] buffer);
        void Write(double value);
        void Write(float value);
        void Write(int value);
        void Write(decimal value);
        void Write(char value);
        void Write(bool value);
        void Write(string format, params object[] arg);
        void Write(string format, object arg0);
        void Write(string format, object arg0, object arg1);
        void Write(char[] buffer, int index, int count);
        void Write(string format, object arg0, object arg1, object arg2);
        Task WriteAsync(string value);
        Task WriteAsync(char value);
        Task WriteAsync(char[] buffer, int index, int count);
        void WriteLine();
        void WriteLine(string value);
        void WriteLine(double value);
        void WriteLine(float value);
        void WriteLine(int value);
        void WriteLine(ulong value);
        void WriteLine(uint value);
        void WriteLine(long value);
        void WriteLine(object value);
        void WriteLine(char[] buffer);
        void WriteLine(decimal value);
        void WriteLine(char value);
        void WriteLine(bool value);
        void WriteLine(string format, params object[] arg);
        void WriteLine(string format, object arg0);
        void WriteLine(string format, object arg0, object arg1);
        void WriteLine(char[] buffer, int index, int count);
        void WriteLine(string format, object arg0, object arg1, object arg2);
        Task WriteLineAsync();
        Task WriteLineAsync(string value);
        Task WriteLineAsync(char value);
        Task WriteLineAsync(char[] buffer, int index, int count);
    }
}