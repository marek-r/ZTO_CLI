using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;


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



    }
}

