using System.Diagnostics;
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
                String[] coreLandscape = new String[] { "plains", "caves", "volcanic", "iceland", "beach", "lakeside", "mountainside", "ravine", "abandoned building", "graveyard", "desert", "oasis", "pyramid" };
                String[] plants = new String[] { "moss", "bush", "sapling", "cactus", "fern", "vine", "flower", "mushroom" };
                String[] minerals = new String[] {"quartz", "potassium feldspar", "plagioclase feldspar", "micas", "amphiboles", "olivine", "calcite", "talc", "fluorite", "coal" };
                String[] animals = new String[] {"hedgehog", "mouse", "rabbit", "rat", "bat", "seagull", "robin", "kiwi", "turtle", "deer", "skunk", "boar", "clownfish", "beetle", "lizard", "frog", "crab", "pufferfish", "goldfish", "koi", "monkey", "cat", "dog", "ant", "anteater", "jellyfish", "owl", "bee", "wolf", "mole", "chicken", "sheep", "duck", "squirrel", "groundhog", "turkey", "pig", "bear", "quail", "pigeon" };

                Console.WriteLine("Good! Let's get started...");
                Console.WriteLine("Generating Core color...");
                
                var rand = new Random();
                var randNum = rand.Next(0, coreColors.Length - 1); // core color
                Console.Write($"{Environment.NewLine}Press any key to continue...");
                Console.ReadKey(true);
                user.coreColor = coreColors[randNum];
                Console.Write($"{Environment.NewLine}");

                Console.WriteLine($"Nice! You have a {user.coreColor} color Core!");
                ConsoleColor[] colors = (ConsoleColor[])ConsoleColor.GetValues(typeof(ConsoleColor));
                if (randNum == 11 || randNum == 14 || randNum == 15) // let's let the user actually read the text if they get cyan, yellow, or white background
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                Console.BackgroundColor = colors[randNum];
                Console.WriteLine("Generating Core shape...");

                randNum = rand.Next(0, coreShape.Length - 1); // core shape
                Console.Write($"{Environment.NewLine}Press any key to continue...");
                Console.ReadKey(true);
                user.coreShape = coreShape[randNum];
                Console.Write($"{Environment.NewLine}");

                Console.WriteLine($"Nice! You have a {user.coreShape} shape Core!");
                Console.WriteLine($"That makes you a {user.coreColor} {user.coreShape} Core!");
                Console.WriteLine("Generating your landscape...");
                
                Console.Write($"{Environment.NewLine}Press any key to continue...");
                Console.ReadKey(true);        
                randNum = rand.Next(0, coreLandscape.Length - 1); // starting landscape
                user.coreLandscape = coreLandscape[randNum];
                Console.Write($"{Environment.NewLine}");

                Console.WriteLine($"Nice! You have spawned in a {user.coreLandscape} landscape! How cozy.");
                Console.WriteLine("Slowly, your presence expands. You can't tell how long it's been. But you feel something nearby.");
                Console.WriteLine("Is it a plant...? You investigate it further.");
                
                randNum = rand.Next(0, plants.Length - 1); // common plants
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
                        Console.WriteLine($"{Environment.NewLine}The {user.coreFirstPlant} dissolves into a {user.coreColor} sparkling mist. You gain 10 exp. You gain 1 HP.");
                        user.coreMaxHP++;
                        user.coreHP++;
                        break;
                    case 2: 
                        Console.WriteLine($"{Environment.NewLine}The {user.coreFirstPlant} wilts and then melts into a {user.coreColor} ooze. You gain 10 exp. You gain 1 ATK.");
                        user.coreATK++;
                        break;
                    case 3:
                        Console.WriteLine($"{Environment.NewLine}The {user.coreFirstPlant} emits a nice, pleasant smell. You gain 10 exp. You gain 1 MP.");
                        user.coreMaxMP++;
                        user.coreMP++;
                        break;
                    default:
                        Console.WriteLine($"{Environment.NewLine}SSoommeetthhiinngg  ssttrraannggee  hhaappppeenneedd!!  The {user.coreFirstPlant} explodes into a {user.coreColor} smoke. You gain 10 exp. You lose 1 SPD.");
                        user.coreSPD--;
                        break;
                }

                Console.Write($"{Environment.NewLine}Press any key to continue...");
                Console.ReadKey(true);
                Console.Write($"{Environment.NewLine}");

                Console.WriteLine($"{Environment.NewLine}Congrats! You leveled up.");
                Console.WriteLine($"{Environment.NewLine}HP + 5");
                Console.WriteLine($"{Environment.NewLine}MP + 5");
                Console.WriteLine($"{Environment.NewLine}DEF + 2");
                Console.WriteLine($"{Environment.NewLine}ATK + 2");
                Console.WriteLine($"{Environment.NewLine}SPD + 1");

                LevelUp(user);
                Console.Write($"{Environment.NewLine}Press any key to continue...");
                Console.ReadKey(true);
                Console.Write($"{Environment.NewLine}");
                user.PrintCurrentStats();

                Console.Write($"{Environment.NewLine}Press any key to continue...");
                Console.ReadKey(true);
                Console.Write($"{Environment.NewLine}");

                Console.Write($"{Environment.NewLine}You have become a bit stronger. But your Hit Points (HP) and Mana Points (MP) are not at their full potential.");
                Console.Write($"{Environment.NewLine}You can rest to gain HP or meditate to gain MP.");
                Console.Write($"{Environment.NewLine}Will you (r)est or (m)editate?");

                option = Console.ReadLine();

                switch (option.ToLower())
                {
                    case "m":
                        Console.WriteLine("You meditate for a while. You gain 1 MP. You feel relaxed.");
                        user.coreMP++;
                        break;
                    case "r":
                        Console.WriteLine("You rest for a while. You gain 1 HP. You feel refreshed.");
                        user.coreHP++;
                        break;
                    default:
                        Console.WriteLine("You decide to do neither. You hope you don't regret this.");
                        break;
                }
               

                randNum = rand.Next(0, minerals.Length - 1); // common minerals
                user.coreFirstMineral = minerals[randNum];
                Console.Write($"{Environment.NewLine}Your first mineral is... a {user.coreFirstMineral}! That really rocks.");

                randNum = rand.Next(0, animals.Length - 1); // common animals
                user.coreFirstAnimal = animals[randNum];
                Console.Write($"{Environment.NewLine}Your first animal is... a {user.coreFirstAnimal}! Hopefully it doesn't bite.");


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
