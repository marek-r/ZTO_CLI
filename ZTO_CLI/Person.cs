using System.ComponentModel.DataAnnotations;

namespace ZTO_CLI
{

    /// <summary>
    /// Klasa Person odpowiadająca tabeli Person oraz właściwości automatyczne kolumnom.
    /// </summary>
    public class Person
    {
        [Key]
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public int? Enabled { get; set; }


        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="username">Nazwa użytkownika</param>
        /// <param name="password">Hasło</param>
        /// <param name="enabled">Status</param>
        public Person(string? username, string? password, int? enabled)
        {
            Username = username;
            Password = password;
            Enabled = enabled;
        }

        /// <summary>
        /// Dodaje użytkownika.
        /// </summary>
        /// <param name="person">Obiekt klasy Person</param>
        /// <returns>Informację o stanie wykonanej operacji.</returns>
        public static string Create(Person person)
        {
            try
            {
                Console.WriteLine("Czekaj...");
                using (DataContext context = new DataContext())
                {
                    if (person.Username != null && person.Password != null && person.Enabled != null)
                    {
                        if (context.Persons.Where(p => p.Username == person.Username)
                                .FirstOrDefault() == null)
                        {
                            context.Persons.Add(person);
                            context.SaveChanges();
                            return "Użytkownik został dodany. Konto aktywne.";
                        }
                        else
                        {
                            return "Użytkownik o takiej nazwie istnieje";
                        }

                    }

                    return "Nie określono danych użytkownika.";

                }
            }
            catch (Exception error)
            {
                return (error.Message.ToString());
            }
        }

        /// <summary>
        /// Pobiera wszystkie rekordy
        /// </summary>
        /// <returns>Listę obiektów klay Person</returns>
        public static List<Person> Read()
        {
            try
            {
                Console.WriteLine("Czekaj...");
                using (DataContext context = new DataContext())
                {
                    return context.Persons.ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        /// <summary>
        /// Aktualizuje wybrany rekord
        /// </summary>
        /// <param name="id">Id użytkownika</param>
        public static void Update(int id)
        {
            using (DataContext context = new DataContext())
            {
                try
                {
                    Console.WriteLine("Czekaj...");
                    Person person = context.Persons.Find(id);

                    if (person != null)
                    {
                        Console.WriteLine("Nowe hasło:");
                        string password = Console.ReadLine();
                        if (password != "")
                        {
                            person.Password = password;
                        }
                        else
                        {
                            Console.WriteLine("Hasło pozostanie nie zmienione.");
                        }
                        do
                        {
                            Console.WriteLine("Status:");
                            Helper.Status = Console.ReadLine();
                            if (int.TryParse(Helper.Status, out Helper.status) && Helper.status >= 0 && Helper.status <= 1)
                            {
                                break;
                            }
                            Console.WriteLine(Environment.NewLine);
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Wpisz cyfrę. 1 - status aktywny 0 - status nieaktywny.");
                            Console.ResetColor();
                        } while (true);
                        person.Enabled = Helper.status;
                        context.SaveChanges();

                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Brak danych.");
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

        /// <summary>
        /// Usuwa dany rekord.
        /// </summary>
        /// <param name="id">Id użytkownika</param>
        public static void Delete(int id)
        {
            using (DataContext context = new DataContext())
            {
                try
                {
                    Console.WriteLine("Czekaj...");
                    Person person = context.Persons.Find(id);

                    if (person != null)
                    {
                        Console.WriteLine("Usuwanie...");
                        context.Remove(person);
                        context.SaveChanges();

                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Brak danych.");
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

        /// <summary>
        /// Sprawdza czy użytkownik o podanym id istnieje oraz czy ma przypisanego suchara.
        /// </summary>
        /// <param name="id">Id użytkownika</param>
        /// <returns>0-brak użytkownika;1-użytkownik ma suchara (aktualizacja);2-użytkownik nie ma suchara (dodanie)</returns>
        public static int CheckPerson(int id)
        {
            using (DataContext context = new DataContext())
                try
                {
                    Console.WriteLine("Czekaj...");
                    Person person = context.Persons.Find(id);

                    if (person != null)
                    {
                        if (context.Suchary.Where(p => p.PersonId == person.Id)
                                            .FirstOrDefault() != null)
                        {
                            return 1;
                        }
                        else
                        {
                            return 2;
                        }
                    }
                    else
                    {
                        return 0;
                    }
                }
                catch (Exception error)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(error.Message.ToString());
                    Console.ResetColor();
                    Thread.Sleep(5000);
                    return 0;
                }


        }

        /// <summary>
        /// Relacja użytkownika do suchara
        /// </summary>
        public static dynamic ReadJoin()
        {
            try
            {
                Console.WriteLine("Czekaj...");
                using (DataContext context = new DataContext())

                {
                    return Helper.userSuchar = (from pe in context.Persons
                                                join su in context.Suchary on pe.Id equals su.PersonId
                                                where pe.Id == su.PersonId
                                                select new
                                                {
                                                    personId = pe.Id,
                                                    personUsername = pe.Username,
                                                    personPassword = pe.Password,
                                                    personEnabled = pe.Enabled,
                                                    sucharId = su.id,
                                                    sucharPersonid = su.PersonId,
                                                    sucharCreatedAt = su.created_at,
                                                    sucharIconUrl = su.icon_url,
                                                    sucharValue = su.value
                                                }).ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }

        }

    }
}

