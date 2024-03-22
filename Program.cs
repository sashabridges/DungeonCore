using System.Diagnostics;
using System.Text.Json.Serialization;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using System.IO;
using System;
using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.VisualBasic.FileIO;

namespace DungeonCore
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var currentDate = DateTime.Now;
            UserCore user = new UserCore();
            var name = "";
            
            Console.WriteLine("Hello, new Core!");

            Console.WriteLine("Do you want to use your previous data? Yes or no?");
            var saveData = Console.ReadLine();

            if (saveData != null || saveData == "" )
            {
                saveData = "no";
            }

            if (saveData.ToLower() == "no")
            {
                Console.WriteLine("What is your name?");
                name = Console.ReadLine();
                user.userName = name;

                Console.WriteLine($"{Environment.NewLine}Hello, DungeonCore {name}, on {currentDate:d} at {currentDate:t}!");
            }

            if (saveData == "yes")
            {
                LoadFile(user);
            }

            Console.WriteLine($"{Environment.NewLine}Hello, DungeonCore {user.userName}, on {currentDate:d} at {currentDate:t}!");

            var playing = AskToPlay();

            if (playing = true)
            {
                Console.WriteLine("Good! Let's get started...");
                
                if (saveData == "no")
                {
                    NewSave(user);
                } else
                {
                    UsedSave(user);
                }
               
               Console.Write($"{Environment.NewLine}Your first mineral is... a {user.coreFirstMineral}! That really rocks.");
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

        static bool AskToPlay()
        {
            Console.WriteLine("Would you like to play DungeonCore?");
            var question = Console.ReadLine();

            if (question == "" || question == null)
            {
                return false;
            }

            if (question.ToLower() == "yes")
            {
                return true;
            }

            return false;
        }

        static void GoodBye(String name, DateTime currentDate)
        {
            currentDate = DateTime.Now;
            Console.WriteLine($"{Environment.NewLine}Goodbye, DungeonCore {name}, on {currentDate:d} at {currentDate:t}!");
            Console.Write($"{Environment.NewLine}Press any key to exit...");
            Console.ReadKey(true);
        }

        static void GenerateFeatures(UserCore user)
        {
            String[] coreShape = new String[] { "oval", "hexagon", "trillion", "square", "heart", "octagon", "princess", "cabochon", "round", "kite", "pear" };
            String[] coreLandscape = new String[] { "plains", "caves", "volcanic", "iceland", "beach", "lakeside", "mountainside", "ravine", "abandoned building", "graveyard", "desert", "oasis", "pyramid" };
            String[] plants = new String[] { "moss", "bush", "sapling", "cactus", "fern", "vine", "flower", "mushroom" };
            String[] minerals = new String[] { "quartz", "potassium feldspar", "plagioclase feldspar", "micas", "amphiboles", "olivine", "calcite", "talc", "fluorite", "coal" };
            String[] animals = new String[] { "hedgehog", "mouse", "rabbit", "rat", "bat", "seagull", "robin", "kiwi", "turtle", "deer", "skunk", "boar", "clownfish", "beetle", "lizard", "frog", "crab", "pufferfish", "goldfish", "koi", "monkey", "cat", "dog", "ant", "anteater", "jellyfish", "owl", "bee", "wolf", "mole", "chicken", "sheep", "duck", "squirrel", "groundhog", "turkey", "pig", "bear", "quail", "pigeon" };

            var rand = new Random();
            var randNum = 0;

            randNum = rand.Next(0, coreShape.Length - 1); // core shape
            user.coreShape = coreShape[randNum];

            randNum = rand.Next(0, coreLandscape.Length - 1); // starting landscape
            user.coreLandscape = coreLandscape[randNum];

            randNum = rand.Next(0, plants.Length - 1); // common plants
            user.coreFirstPlant = plants[randNum];

            randNum = rand.Next(0, minerals.Length - 1); // common minerals
            user.coreFirstMineral = minerals[randNum];

            randNum = rand.Next(0, animals.Length - 1); // common animals
            user.coreFirstAnimal = animals[randNum];
        }

        static void NewSave(UserCore user)
        {
            String[] coreColors = new String[] { "black", "dark blue", "dark green", "dark cyan", "dark red", "dark magenta", "dark yellow", "gray", "dark gray", "blue", "green", "cyan", "red", "magenta", "yellow", "white" };
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

            GenerateFeatures(user);

            Console.Write($"{Environment.NewLine}Press any key to continue...");
            Console.ReadKey(true);
            Console.Write($"{Environment.NewLine}");

            Console.WriteLine($"Nice! You have a {user.coreShape} shape Core!");
            Console.WriteLine($"That makes you a {user.coreColor} {user.coreShape} Core!");
            Console.WriteLine("Generating your landscape...");

            Console.Write($"{Environment.NewLine}Press any key to continue...");
            Console.ReadKey(true);
            Console.Write($"{Environment.NewLine}");

            Console.WriteLine($"Nice! You have spawned in a {user.coreLandscape} landscape! How cozy.");
            Console.WriteLine("Slowly, your presence expands. You can't tell how long it's been. But you feel something nearby.");

            FirstPlantEvent(user);

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

            var option = Console.ReadLine();

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

            SaveFile(user);
        }

        static void UsedSave(UserCore user)
        {
            String[] coreColors = new String[] { "black", "dark blue", "dark green", "dark cyan", "dark red", "dark magenta", "dark yellow", "gray", "dark gray", "blue", "green", "cyan", "red", "magenta", "yellow", "white" };
            ConsoleColor[] colors = (ConsoleColor[])ConsoleColor.GetValues(typeof(ConsoleColor));

            for (int i = 0; i < coreColors.Length; i++)
            {
                if (coreColors[i] == user.coreColor)
                {
                    Console.BackgroundColor = colors[i];
                    if (i == 11 || i == 14 || i == 15)
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                    }
                }
            }

            switch (user.savePoint)
            {
                case 1:
                    FirstPlantEvent(user);
                    break;
                case 2:
                    FirstAnimalEvent(user);
                    break;
                case 3:
                    Console.WriteLine("You rest for a while. You gain 1 HP. You feel refreshed.");
                    user.coreHP++;
                    break;
                case 4:
                    Console.WriteLine("You rest for a while. You gain 1 HP. You feel refreshed.");
                    user.coreHP++;
                    break;
                case 5:
                    Console.WriteLine("You rest for a while. You gain 1 HP. You feel refreshed.");
                    user.coreHP++;
                    break;
                case 6:
                    Console.WriteLine("You rest for a while. You gain 1 HP. You feel refreshed.");
                    user.coreHP++;
                    break;
                case 7:
                    Console.WriteLine("You rest for a while. You gain 1 HP. You feel refreshed.");
                    user.coreHP++;
                    break;
                case 8:
                    Console.WriteLine("You rest for a while. You gain 1 HP. You feel refreshed.");
                    user.coreHP++;
                    break;
                case 9:
                    Console.WriteLine("You rest for a while. You gain 1 HP. You feel refreshed.");
                    user.coreHP++;
                    break;
                case 10:
                    Console.WriteLine("You rest for a while. You gain 1 HP. You feel refreshed.");
                    user.coreHP++;
                    break;
                case 11:
                    Console.WriteLine("You rest for a while. You gain 1 HP. You feel refreshed.");
                    user.coreHP++;
                    break;
                default:
                    Console.WriteLine("You decide to do neither. You hope you don't regret this.");
                    break;
            }

        }

        static void FirstPlantEvent(UserCore user)
        {
            Console.WriteLine("Is it a plant...? You investigate it further.");
            Console.WriteLine($"Woah... it looks like a {user.coreFirstPlant}. What should we do with it? Choose a number: ");
            Console.Write($"{Environment.NewLine}1 - Eat it?");
            Console.Write($"{Environment.NewLine}2 - Touch it?");
            Console.Write($"{Environment.NewLine}3 - Smell it?");
            var option = Console.ReadLine();
            int opt = 0;
            var rand = new Random();
            var randNum = 0;
            randNum = rand.Next(1, 100);

            if (!Int32.TryParse(option, out randNum))
            {
                Console.WriteLine($"Oops! You didn't enter a correct value and accidentally get confused!");
                option = randNum.ToString();
            }

            opt = Convert.ToInt32(option);

            switch (opt)
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

            user.savePoint = 1;
            Console.Write($"{Environment.NewLine}Do you want to save the game? Yes or no?");
            var saving = Console.ReadLine();

            if (saving != null || saving == "")
            {
                saving = "no";
            }

            if (saving.ToLower() == "yes")
            {
                SaveFile(user);
            }

        }

        static void SecondPlantEvent(UserCore user)
        {
            user.savePoint = 9;
            Console.Write($"{Environment.NewLine}Do you want to save the game? Yes or no?");
            var saving = Console.ReadLine();

            if (saving != null || saving == "")
            {
                saving = "no";
            }

            if (saving.ToLower() == "yes")
            {
                SaveFile(user);
            }
        }

        static void ThirdPlantEvent(UserCore user)
        {
            user.savePoint = 10;
            Console.Write($"{Environment.NewLine}Do you want to save the game? Yes or no?");
            var saving = Console.ReadLine();

            if (saving != null || saving == "")
            {
                saving = "no";
            }

            if (saving.ToLower() == "yes")
            {
                SaveFile(user);
            }
        }

        static void FirstAnimalEvent(UserCore user)
        {
            user.savePoint = 2;
            Console.Write($"{Environment.NewLine}Do you want to save the game? Yes or no?");
            var saving = Console.ReadLine();

            if (saving != null || saving == "")
            {
                saving = "no";
            }

            if (saving.ToLower() == "yes")
            {
                SaveFile(user);
            }
        }

        static void SecondAnimalEvent(UserCore user)
        {
            user.savePoint = 5;
            Console.Write($"{Environment.NewLine}Do you want to save the game? Yes or no?");
            var saving = Console.ReadLine();

            if (saving != null || saving == "")
            {
                saving = "no";
            }

            if (saving.ToLower() == "yes")
            {
                SaveFile(user);
            }
        }

        static void ThirdAnimalEvent(UserCore user)
        {
            user.savePoint = 12;
            Console.Write($"{Environment.NewLine}Do you want to save the game? Yes or no?");
            var saving = Console.ReadLine();

            if (saving != null || saving == "")
            {
                saving = "no";
            }

            if (saving.ToLower() == "yes")
            {
                SaveFile(user);
            }
        }

        static void FirstMineralEvent(UserCore user)
        {
            user.savePoint = 3;
            Console.Write($"{Environment.NewLine}Do you want to save the game? Yes or no?");
            var saving = Console.ReadLine();

            if (saving != null || saving == "")
            {
                saving = "no";
            }

            if (saving.ToLower() == "yes")
            {
                SaveFile(user);
            }
        }

        static void SecondMineralEvent(UserCore user)
        {
            user.savePoint = 6;
            Console.Write($"{Environment.NewLine}Do you want to save the game? Yes or no?");
            var saving = Console.ReadLine();

            if (saving != null || saving == "")
            {
                saving = "no";
            }

            if (saving.ToLower() == "yes")
            {
                SaveFile(user);
            }
        }

        static void ThirdMineralEvent(UserCore user)
        {
            user.savePoint = 8;
            Console.Write($"{Environment.NewLine}Do you want to save the game? Yes or no?");
            var saving = Console.ReadLine();

            if (saving != null || saving == "")
            {
                saving = "no";
            }

            if (saving.ToLower() == "yes")
            {
                SaveFile(user);
            }
        }

        static void FirstAdventurerEvent(UserCore user)
        {
            user.savePoint = 4;
            Console.Write($"{Environment.NewLine}Do you want to save the game? Yes or no?");
            var saving = Console.ReadLine();

            if (saving != null || saving == "")
            {
                saving = "no";
            }

            if (saving.ToLower() == "yes")
            {
                SaveFile(user);
            }
        }

        static void FinalEvent(UserCore user)
        {
            user.savePoint = 13;
            Console.WriteLine("After everything, you're finally a thriving dungeon... You continue living out your days eventfully.");
        }

        static void SaveFile(UserCore user)
        {
            Console.WriteLine("Saving your data to json format...");

            string fileName = "name.json";
            string jsonString = JsonSerializer.Serialize(user);
            File.WriteAllText(fileName, jsonString);
        }

        static void LoadFile(UserCore user)
        {
          
            Console.WriteLine("Loading save data...");

            string fileName = "name.json";
            string jsonString = File.ReadAllText(fileName);
            user = JsonSerializer.Deserialize<UserCore>(jsonString);

            Console.WriteLine($"Welcome back, {user.userName}! Your file was loaded successfully!");
        }
    }
}
