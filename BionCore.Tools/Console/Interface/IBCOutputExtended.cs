using BionCore.Tools.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BionCore.Tools
{
    public interface IBCOutputExtended : ITextWriter, IBUID
    {
        void WriteText(IBCText text);
        void SetTextColor(IBCColorExtended color);
        void SetBackgroundColor(IBCColorExtended color);

        void SetTabSpacing(int dents);
        int GetTabSpacing();
        BCColorExtended GetTextColor();
        BCColorExtended GetBackgroundColor();

        int? GetRowCount();
        int? GetColumnCount();

        void SetTextFormat(BCTextFormats format);
        BCTextFormats GetTextFormat();

        bool TryFormatText(string text, out string formatedText, BCTextFormats? textFormat = null, object parameter = null);
    }
}
