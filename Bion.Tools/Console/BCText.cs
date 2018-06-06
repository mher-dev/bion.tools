using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bion.Tools.Console
{
    /// <summary>
    /// Bion's Console Text
    /// </summary>
    [Serializable]
    public class BCText : IBUID, IBCText
    {
        internal BCText() { }

        /// <summary>
        /// Crea texto de consola
        /// </summary>
        /// <param name="text">Color del texto. En caso de NULL se utiliza la de por defecto</param>
        /// <param name="isNewLine">Indica que este elemento tiene que ser tramitado solo por los stalkers, omitiendo la salida por Console</param>
        /// <param name="textColor">Color del texto. En caso de NULL se utiliza la de por defecto</param>
        /// <param name="backgroundColor">Color de fondo del texto. En caso de NULL se utiliza la de por defecto</param>
        /// <param name="isOnlyForStalkers">Indica que este elemento tiene que ser tramitado solo por los stalkers, omitiendo la salida por Console</param>
        public BCText(
            string text
            , bool isNewLine
            , ConsoleColor? textColor = null
            , ConsoleColor? backgroundColor = null
            , bool isOnlyForStalkers = false
            )
        {
            this.Text = text;
            this.IsNewLine = isNewLine;
            this.IsOnlyForStalkers = this.IsOnlyForStalkers;
            this.BackgroundColor = backgroundColor;
            this.TextColor = this.TextColor;
        }


        #region -------- Flags --------

        /// <summary>
        /// Indica si el texto se tiene que mostrar en una nueva linea
        /// </summary>
        public virtual bool IsNewLine { get; internal set; }

        /// <summary>
        /// Indica que este elemento tiene que ser tramitado solo por los stalkers, omitiendo la salida por Console
        /// </summary>
        public virtual bool IsOnlyForStalkers { get; internal set; }
        #endregion [-------- Flags --------]


        #region -------- Design --------
        /// <summary>
        /// Color de fondo del texto. En caso de NULL se utiliza la de por defecto
        /// </summary>
        public virtual ConsoleColor? BackgroundColor { get; internal set; }

        /// <summary>
        /// Color del texto. En caso de NULL se utiliza la de por defecto
        /// </summary>
        public virtual ConsoleColor? TextColor { get; internal set; }
        #endregion [-------- Design --------]

        #region -------- Text --------
        /// <summary>
        /// Texto que se tiene que visualizar
        /// </summary>
        public virtual string Text { get; internal set; }

        public int BUID { get; private set; } = BionTools.GenerateBUID();
        #endregion [-------- Text --------]

        #region -------- Functions --------
        public override string ToString()
        {
            return this.Text;
        }
        #endregion [-------- Functions --------]


    }
}
