using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.Text.Json;

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

        /// <summary>
        /// Właściwość auto. Hasło do zahaszowania
        /// </summary>
        public static string? Password { get; set; }

        /// <summary>
        /// Właściwość auto. Ścieżka do pliku bazy.
        /// </summary>
        public static string? PlikBazy { get; set; }

        /// <summary>
        /// Scieżka do katalogu roboczego aplikacji
        /// </summary>
        public static string? BazaPath { get; set; }

        /// <summary>
        /// Nazwa folderu w którym znajduje się baza.
        /// </summary>
        public static string? FolderDb { get; set; }

        /// <summary>
        /// Właściwość auto. relacja użytkownik-suchar
        /// </summary>
        public static dynamic? UserSuchar { get; set; }

        /// <summary>
        /// Pole klient Http
        /// </summary>
        public static readonly HttpClient client = new HttpClient();

        /// <summary>
        /// Zadanie pobierania sucharów.
        /// </summary>
        /// <returns>Obiekt klasy suchar</returns>
        public static async Task<Suchar?> PobierzSuchara()
        {
            try
            {
                using HttpResponseMessage response = await client.GetAsync("https://api.chucknorris.io/jokes/random");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                Suchar suchar = JsonSerializer.Deserialize<Suchar>(responseBody);

                return suchar;

            }
            catch (HttpRequestException error)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(error.Message.ToString());
                Console.ResetColor();
                Thread.Sleep(5000);
                throw;
            }
        }

        /// <summary>
        /// Tworzy katalog roboczy dla pliku bazy danych, który trzeba tam umieścić przed uruchomienim programu.
        /// </summary>
        public static void FolderBazy()
        {
            try
            {
                BazaPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
                FolderDb = System.IO.Path.GetDirectoryName(BazaPath);
                FolderDb += "\\_Baza";
                PlikBazy = Path.Combine(FolderDb, "Baza.db");
                DirectoryInfo di = Directory.CreateDirectory(FolderDb);
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
