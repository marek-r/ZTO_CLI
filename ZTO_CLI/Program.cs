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
        /// Pozycja kursora.
        /// </summary>
        public static (int l, int t) Pk { get; set; }

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
            Helper.FolderBazy();
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
                    do
                    {
                        Console.WriteLine("Wpisz hasło:");
                        Helper.Password = Console.ReadLine();
                        if (Helper.Password.Length > 4)
                        {
                            break;
                        }
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Wpisz hasło minimum cztery znaki:");
                        Console.ResetColor();
                    } while (true);
                    Krypto krypto = new();
                    Person person = new(username, krypto.GenerujMD5(Helper.Password), 1);
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
                            Pk = Console.GetCursorPosition();
                            Console.SetCursorPosition(Pk.l, Pk.t);
                            for (int i = 0; i < Console.WindowWidth; i++)
                            {
                                Console.Write("-");
                            }
                            Console.Write("Id: " + item.Id);
                            Console.Write(" Username: " + item.Username);
                            Console.Write(" Password: " + item.Password);
                            Console.WriteLine(" Enabled: " + item.Enabled);
                        }
                        Console.WriteLine(Environment.NewLine);
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
                        Pk = Console.GetCursorPosition();
                        Console.SetCursorPosition(Pk.l, Pk.t);

                        for (int i = 0; i < Console.WindowWidth; i++)
                        {
                            Console.Write("-");
                        }
                        Console.WriteLine("RELACJA UŻYTKOWNIKA I SUCHARA");
                        foreach (var item in Person.ReadJoin())
                        {
                            Pk = Console.GetCursorPosition();
                            Console.SetCursorPosition(Pk.l, Pk.t);
                            for (int i = 0; i < Console.WindowWidth; i++)
                            {
                                Console.Write("-");
                            }
                            Console.WriteLine("Id: " + item.PersonId);
                            Console.WriteLine("User: " + item.Username);
                            Console.WriteLine("Enabled: " + item.Enabled);
                            Console.WriteLine("SucharId: " + item.Id);
                            Console.WriteLine("CreatedAt: " + item.created_at);
                            Console.WriteLine("UpdatedAt: " + item.updated_at);
                            Console.WriteLine("IconUrl: " + item.icon_url);
                            Console.WriteLine("Value: " + item.value);
                        }
                        Console.WriteLine(Environment.NewLine);
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
                            Suchar.Update(Helper.id);
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