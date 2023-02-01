namespace ZTO_CLI
{
    internal class Program
    {
        /// <summary>
        /// Elementy menu programu.
        /// </summary>
        public static readonly List<string> menu = new()
        {
            "[c].   Dodać użytkownika.",
            "[r].   Wyświetlić wszystkich użytkowników.",
            "[rs].  Wyświetlić wszystkich użytkowników z informacjami o sucharach.",
            "[u].   Zktualizować dane użytkownika.",
            "[d].   Usunąć użytkownika.",
            "[s].   Wybać użytkownika i przypisać suchara.",
            "[e].   Zakończ"
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
                case "rs":

                        if (Person.ReadJoin() != null)
                        {
                            Console.WriteLine("-------------------------------------------------------");
                            Console.WriteLine("RELACJA UŻYTKOWNIKA I SUCHARA");
                            Console.WriteLine("-------------------------------------------------------");
                            foreach (object item in Person.ReadJoin())
                            {
                                Console.WriteLine("-------------------------------------------------------");
                                Console.WriteLine(item);
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
                        Console.ForegroundColor = ConsoleColor.Red;
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
                    do
                    {
                        Console.WriteLine("Wpisz id użytkownika któremu chcesz przypisać suchara:");
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

                    switch (Person.CheckPerson(Helper.id))
                    {
                        case 0:
                            Console.WriteLine("Brak danych");
                            Thread.Sleep(1500);
                            Start();
                            break;
                        case 1:
                            //Task<Suchar> taskAktualizujSuchar = Helper.PobierzSuchara();
                            //taskAktualizujSuchar.Wait();
                            //Suchar nowySucharDoPodmiany = taskAktualizujSuchar.Result;
                            //nowySucharDoPodmiany.PersonId = Helper.id;
                            Suchar.Update(Helper.id);
                            //Console.WriteLine(Suchar.Update(Helper.id));
                            Thread.Sleep(2000);
                            Start();
                            break;
                        case 2:
                            Task<Suchar> taskSuchar = Helper.PobierzSuchara();
                            taskSuchar.Wait();
                            Suchar nowySuchar = taskSuchar.Result;
                            nowySuchar.PersonId = Helper.id;
                            Console.WriteLine(Suchar.Create(nowySuchar));
                            Thread.Sleep(2000);
                            Start();
                            break;
                        default:
                            Console.WriteLine("Błąd podczas weryfikacji czy użytkownik ma przypisanego suchara.");
                            Thread.Sleep(1500);
                            Start();
                            break;
                    }
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