using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTO_CLI
{
    /// <summary>
    /// Helpery powiązane z klasami.
    /// </summary>
    public static class Helper
    {
        /// <summary>
        /// Pole id po przekonwertowaniu ze stringa.
        /// </summary>
        public static int id;

        /// <summary>
        /// Właściwość.
        /// String wprowadzany podczas aktualizacji danych użytkownika.
        /// Później jest konwertowany na int.
        /// </summary>
        public static string? Index { get; set; }

        /// <summary>
        /// Pole status po przekonwertowaniu ze stringa.
        /// </summary>
        public static int status;

        /// <summary>
        /// Deklarowany ststus użytkownika.
        /// </summary>
        public static string? Status { get; set; }
    }
}
