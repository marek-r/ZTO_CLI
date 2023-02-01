using System.ComponentModel.DataAnnotations;

namespace ZTO_CLI
{
    /// <summary>
    /// Klasa Suchar odpowiadająca tabeli oraz właściwości automatyczne.
    /// </summary>
    /// <example>
    /// Przykład odpowiedzi z API.
    /// {
    //   "categories":[      
    //   ],
    //   "created_at":"2020-01-05 13:42:29.569033",
    //   "icon_url":"https://assets.chucknorris.host/img/avatar/chuck-norris.png",
    //   "id":"qJ9lipf0RFeWLTNGNbwCBg",
    //   "updated_at":"2020-01-05 13:42:29.569033",
    //   "url":"https://api.chucknorris.io/jokes/qJ9lipf0RFeWLTNGNbwCBg",
    //   "value":"The jokes are slacking, pick up the paste guys, or Chuck Norris will Virtually Roundhouse your asses!!"
    //}
    /// </example>
    public class Suchar
    {
        /// <summary>
        /// Konstruktor.
        /// Parametry zgodnie z API
        /// </summary>
        /// <param name="personId"></param>
        /// <param name="created_at"></param>
        /// <param name="icon_url"></param>
        /// <param name="id"></param>
        /// <param name="updated_at"></param>
        /// <param name="url"></param>
        /// <param name="value"></param>
        public Suchar(int personId, string created_at, string icon_url, string id, string updated_at, string url, string value)
        {
            PersonId = personId;

            this.created_at = created_at;
            this.icon_url = icon_url;
            this.id = id;
            this.updated_at = updated_at;
            this.url = url;
            this.value = value;
        }
        [Key]
        public int SucharId { get; set; }
        public int PersonId { get; set; }

        //public List<object> categories { get; set; }
        public string created_at { get; set; }
        public string icon_url { get; set; }
        public string id { get; set; }
        public string updated_at { get; set; }
        public string url { get; set; }
        public string value { get; set; }

        /// <summary>
        /// Dodaje suchara do bazy
        /// </summary>
        /// <param name="suchar">Obiekt klasy suchar</param>
        /// <returns>Status zakończonej operacji</returns>
        public static string Create(Suchar suchar)
        {
            try
            {
                Console.WriteLine("Czekaj...");
                using (DataContext context = new DataContext())
                {
                    if (suchar.id != null)
                    {
                        context.Suchary.Add(suchar);
                        context.SaveChanges();
                        return "Suchar został dodany i powiązany z uytkownikiem.";
                    }
                    else
                    {
                        return "Brak id suchara";
                    }
                }
            }
            catch (Exception error)
            {
                return (error.Message.ToString());
            }
        }

        /// <summary>
        /// Aktualizuje suchara w oparciu o id Użytkownika (PersonId)
        /// </summary>
        /// <param name="id">Id (PersonId) użytkownika w tabeli Suchary</param>
        public static void Update(int id)
        {
            using (DataContext context = new DataContext())
            {
                Suchar suchar = context.Suchary.Where(p => p.PersonId == id).FirstOrDefault();
                try
                {
                    Console.WriteLine("Czekaj...");

                    if (suchar != null)
                    {
                        Task<Suchar> taskSuchar = Helper.PobierzSuchara();
                        taskSuchar.Wait();
                        Suchar nowySuchar = taskSuchar.Result;
                        suchar.created_at = nowySuchar.created_at;
                        suchar.icon_url = nowySuchar.icon_url;
                        suchar.id = nowySuchar.id;
                        suchar.updated_at = nowySuchar.updated_at;
                        suchar.url = nowySuchar.url;
                        suchar.value = nowySuchar.value;
                        Console.WriteLine("Zaktualizowano suchara.");
                        context.SaveChanges();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Błąd podczas aktualizacji suchara. Suchar=null.");
                        Console.ResetColor();
                        Thread.Sleep(1500);
                    }
                }
                catch (Exception error)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(error.Message.ToString());
                    Console.ResetColor();
                    Thread.Sleep(5000);
                }
            }

        }

    }
}
