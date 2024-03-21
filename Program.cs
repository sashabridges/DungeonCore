using System.Xml.Linq;
using System.Xml.Serialization;

namespace DungeonCore
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, new Core!");

            Console.WriteLine("What is your name?");
            var name = Console.ReadLine();
            var currentDate = DateTime.Now;
            Console.WriteLine($"{Environment.NewLine}Hello, DungeonCore {name}, on {currentDate:d} at {currentDate:t}!");

            Console.WriteLine("Would you like to play DungeonCore?");
            var question = Console.ReadLine();

            if (question == "" || question == null)
            {
                question = "NOT YES";
            }

            if (question.ToLower() == "yes")
            {
                UserCore user = new UserCore();
                user.userName = name;

                String[] coreColors = new String[] { "black", "dark blue", "dark green", "dark cyan", "dark red", "dark magenta", "dark yellow", "gray", "dark gray", "blue", "green", "cyan", "red", "magenta", "yellow", "white" };
                String[] coreShape = new String[] { "oval", "hexagon", "trillion", "square", "heart", "octagon", "princess", "cabochon", "round", "kite", "pear" };
                String[] coreLandscape = new String[] { "plains", "caves", "volcanic", "iceland", "beach", "lakeside", "mountainside", "ravine", "abandoned building", "graveyard" };
                String[] plants = new String[] { "moss", "bush", "sapling", "cactus", "fern", "vine", "flower", "mushroom" };

                Console.WriteLine("Good! Let's get started...");
                Console.WriteLine("Generating Core color...");
                
                var rand = new Random();
                var randNum = rand.Next(0, 15);
                Console.Write($"{Environment.NewLine}Press any key to continue...");
                Console.ReadKey(true);
                user.coreColor = coreColors[randNum];

                Console.WriteLine($"Nice! You have a {user.coreColor} color Core!");
                ConsoleColor[] colors = (ConsoleColor[])ConsoleColor.GetValues(typeof(ConsoleColor));
                Console.BackgroundColor = colors[randNum];
                Console.WriteLine("Generating Core shape...");

                randNum = rand.Next(0, 10);
                Console.Write($"{Environment.NewLine}Press any key to continue...");
                Console.ReadKey(true);
                user.coreShape = coreShape[randNum];
                
                Console.WriteLine($"Nice! You have a {user.coreShape} shape Core!");
                Console.WriteLine($"That makes you a {user.coreColor} {user.coreShape} Core!");
                Console.WriteLine("Generating your landscape...");
                
                Console.Write($"{Environment.NewLine}Press any key to continue...");
                Console.ReadKey(true);        
                randNum = rand.Next(0, 9);
                user.coreLandscape = coreLandscape[randNum];

                Console.WriteLine($"Nice! You have spawned in a {user.coreLandscape} landscape! How cozy.");
                Console.WriteLine("Slowly, your presence expands. You can't tell how long it's been. But you feel something nearby.");
                Console.WriteLine("Is it a plant...? You investigate it further.");
                
                randNum = rand.Next(0, 7);
                user.coreFirstPlant = plants[randNum];
                Console.WriteLine($"Woah... it looks like a {user.coreFirstPlant}. What should we do with it? Choose a number: ");
                Console.Write($"{Environment.NewLine}1 - Eat it?");
                Console.Write($"{Environment.NewLine}2 - Touch it?");
                Console.Write($"{Environment.NewLine}3 - Smell it?");
                var option = Console.ReadLine();
                int opt = 0;
                randNum = rand.Next(1, 100);

                if (!Int32.TryParse( option, out randNum))
                {
                    Console.WriteLine($"Oops! You didn't enter a correct value and accidentally get confused!");
                    option = randNum.ToString();
                }

                opt = Convert.ToInt32(option);

                switch(opt)
                {
                    case 1:
                        Console.WriteLine($"{Environment.NewLine}The {user.coreFirstPlant} dissolves into a {user.coreColor} sparkling mist. You gain 10 exp.");
                        break;
                    case 2: 
                        Console.WriteLine($"{Environment.NewLine}The {user.coreFirstPlant} wilts and then melts into a {user.coreColor} ooze. You gain 10 exp.");
                        break;
                    case 3:
                        Console.WriteLine($"{Environment.NewLine}The {user.coreFirstPlant} emits a nice, pleasant smell. You gain 10 exp.");
                        break;
                    default:
                        Console.WriteLine($"{Environment.NewLine}SSoommeetthhiinngg  ssttrraannggee  hhaappppeenneedd!!  The {user.coreFirstPlant} explodes into a {user.coreColor} smoke. You gain 10 exp.");
                        break;
                }

                Console.WriteLine($"{Environment.NewLine}Congrats! You leveled up.");
                Console.WriteLine($"{Environment.NewLine}HP + 5");
                Console.WriteLine($"{Environment.NewLine}MP + 5");
                Console.WriteLine($"{Environment.NewLine}DEF + 2");
                Console.WriteLine($"{Environment.NewLine}ATK + 2");
                Console.WriteLine($"{Environment.NewLine}SPD + 1");

                LevelUp(user);
                

            } else
            {
                Console.WriteLine("Not ready? See you later!");
                GoodBye(name, currentDate);
            }

            GoodBye(name, currentDate);
            
        }

        static void LevelUp(UserCore user)
        {
            user.coreLevel++;
            user.coreMaxHP = user.coreMaxHP + 5;
            user.coreMaxMP = user.coreMaxMP + 5;
            user.coreDEF = user.coreDEF + 2;
            user.coreATK = user.coreATK + 2;
            user.coreSPD = user.coreSPD + 1;
        }

        static void GoodBye(String name, DateTime currentDate)
        {
            currentDate = DateTime.Now;
            Console.WriteLine($"{Environment.NewLine}Goodbye, DungeonCore {name}, on {currentDate:d} at {currentDate:t}!");
            Console.Write($"{Environment.NewLine}Press any key to exit...");
            Console.ReadKey(true);
        }
    }
}
