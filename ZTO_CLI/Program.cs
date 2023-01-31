using System.ComponentModel;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ZTO_CLI
{
    internal class Program
    {
        /// <summary>
        /// Elementy menu programu.
        /// </summary>
        public static readonly List<string> menu = new()
        {
            "[c].  Dodać użytkownika.",
            "[r].  Wyświetlić wszystkich użytkowników.",
            "[u].  Zktualizować dane użytkownika.",
            "[d].  Usunąć użytkownika.",
            //"[s].  S.",
            "[e].  Zakończ"
        };       

        /// <summary>
        /// Lista użytkowników
        /// </summary>
        public static List<Person>? Persons { get; private set; }

        /// <summary>
        /// Punkt wejściowy programu.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.Title = "ZTO | Marek";
            Start();
        }

        /// <summary>
        /// Inicjalizuje menu.
        /// </summary>
        public static void Start()
        {
            Console.Clear();
            Console.WriteLine("Co chcesz zrobić ?");
            foreach (var item in menu)
            {
                Console.WriteLine(item);
            }
            switch (Console.ReadLine())
            {
                case "c":
                    Console.WriteLine("Wpisz nazwę użytkownika:");
                    var username = Console.ReadLine();
                    Console.WriteLine("Wpisz hasło:");
                    var password = Console.ReadLine();
                    Person person = new(username, password, 1);
                    Console.WriteLine(Person.Create(person));
                    Thread.Sleep(2000);
                    Start();
                    break;

                case "r":
                    Persons = Person.Read();
                    if (Persons.Count > 0)
                    {
                        foreach (var item in Persons)
                        {
                            Console.WriteLine("-------------------------------------------------------");
                            Console.Write("Id: " + item.Id);
                            Console.Write(" Username: " + item.Username);
                            Console.Write(" Password: " + item.Password);
                            Console.WriteLine(" Enabled: " + item.Enabled);
                            Console.WriteLine("-------------------------------------------------------");
                        }
                        Console.WriteLine("Naciśnij ENTER");
                        Console.ReadLine();
                        Start();
                    }
                    else
                    {
                        Console.WriteLine("Brak danych");
                        Thread.Sleep(1500);
                        Start();
                    }
                    break;
                case "u":                    
                    do
                    {                       
                        Console.WriteLine("Wpisz id użytkownika:");
                        Helper.Index = Console.ReadLine();
                        if (int.TryParse(Helper.Index, out Helper.id))
                        {                            
                            break;
                        }
                        Console.WriteLine(Environment.NewLine);
                        Console.ForegroundColor= ConsoleColor.Red;
                        Console.WriteLine("Wpisz cyfrę lub liczbę.");
                        Console.ResetColor();
                    } while (true);                    
                    Person.Update(Helper.id);
                    Thread.Sleep(2000);
                    Start();
                    break;
                case "d":
                    do
                    {
                        Console.WriteLine("Wpisz id użytkownika:");
                        Helper.Index = Console.ReadLine();
                        if (int.TryParse(Helper.Index, out Helper.id))
                        {
                            break;
                        }
                        Console.WriteLine(Environment.NewLine);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Wpisz cyfrę lub liczbę.");
                        Console.ResetColor();
                    } while (true);
                    Person.Delete(Helper.id);
                    Thread.Sleep(2000);
                    Start();
                    break;
                case "s":
                    //Task task = Get();
                    //task.Wait();

                    break;
                case "e":
                    Console.Clear();
                    Environment.Exit(0);
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Brak takiej opcji- wybierz ponownie.");
                    Console.ResetColor();
                    Thread.Sleep(1500);
                    Start();
                    break;
            }
        }


    }
}