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
                }
                else
                {
                    UsedSave(user);
                }
            } 
            else
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

            FirstAnimalEvent(user);

            FirstMineralEvent(user);

            FirstAdventurerEvent(user);

            SecondAnimalEvent(user);

            SecondMineralEvent(user);

            SecondAdventurerEvent(user);

            ThirdMineralEvent(user);

            SecondPlantEvent(user);

            ThirdPlantEvent(user);

            ThirdAdventurerEvent(user);

            ThirdAnimalEvent(user);

            FinalEvent(user);

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
                    FirstMineralEvent(user);
                    break;
                case 4:
                    FirstAdventurerEvent(user);
                    break;
                case 5:
                    SecondAnimalEvent(user);
                    break;
                case 6:
                   SecondMineralEvent(user);
                    break;
                case 7:
                    SecondAdventurerEvent(user);
                    break;
                case 8:
                    ThirdMineralEvent(user);
                    break;
                case 9:
                    SecondPlantEvent(user);
                    break;
                case 10:
                   ThirdPlantEvent(user);
                    break;
                case 11:
                    ThirdAdventurerEvent (user);
                    break;
                case 12:
                    ThirdAnimalEvent(user);
                    break;
                case 13:
                    FinalEvent(user);
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
            
            user.RestorePoints();

            user.savePoint = 1;
            AskToSave(user);

        }

        static void SecondPlantEvent(UserCore user)
        {
            Console.WriteLine("Save Point: " + user.savePoint);
            Console.WriteLine($"{Environment.NewLine}Congrats! You leveled up.");
            LevelUp(user);
            user.PrintCurrentStats();
            user.RestorePoints();
            user.savePoint = 9;
            AskToSave(user);
        }

        static void ThirdPlantEvent(UserCore user)
        {
            Console.WriteLine("Save Point: " + user.savePoint);
            Console.WriteLine($"{Environment.NewLine}Congrats! You leveled up.");
            LevelUp(user);
            user.PrintCurrentStats();
            user.RestorePoints();
            user.savePoint = 10;
            AskToSave(user);
        }

        static void FirstAnimalEvent(UserCore user)
        {
            Console.WriteLine("Save Point: " + user.savePoint);
            Console.WriteLine($"{Environment.NewLine}While you were investigating your {user.coreFirstPlant}, it looks like an animal wandered into your space.");
            Console.WriteLine("At first, you can't tell what it is. The shape of the creature seems fuzzy until you focus on it. You don't have eyes, after all!");
            Console.Write($"{Environment.NewLine}Your first animal is... a {user.coreFirstAnimal}! Hopefully it doesn't bite.");

            Console.Write($"The {user.coreFirstAnimal} seems quite small and frail. It wanders slowly and carefully to another {user.coreFirstPlant} like it expects ");
            Console.Write("to be attacked at any second. It takes a wary bite of the plant. What do you do?");
            Console.Write($"{Environment.NewLine}1 - Save the {user.coreFirstPlant}?");
            Console.Write($"{Environment.NewLine}2 - Stop the {user.coreFirstAnimal}?");
            Console.Write($"{Environment.NewLine}3 - Leave them both alone?");

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
                    Console.WriteLine($"{Environment.NewLine}You use your MP to protect your {user.coreFirstPlant}, manifesting a small {user.coreColor} barrier around it. The {user.coreFirstAnimal} is startled and panics.");
                    Console.WriteLine($"{Environment.NewLine}You calm down the {user.coreFirstAnimal} by singing a lullaby. You gain your first animal companion, the {user.coreFirstAnimal}.");
                    Console.WriteLine($"{Environment.NewLine}You gain 20 exp. You use 1 MP. You gain 1 DEF.");
                    user.coreDEF++;
                    user.coreMP--;
                    break;
                case 2:
                    Console.WriteLine($"{Environment.NewLine}You attack the {user.coreFirstAnimal} with a magical burst of energy. The {user.coreFirstAnimal} is weakened and shakes in fear.");
                    Console.WriteLine($"{Environment.NewLine}You calm down the {user.coreFirstAnimal} by distracting it with pretty sparkling lights. You gain your first animal companion, the {user.coreFirstAnimal}.");
                    Console.WriteLine($"{Environment.NewLine}You gain 20 exp. You use 1 MP. You gain 1 ATK.");
                    user.coreMP--;
                    user.coreATK++;
                    break;
                case 3:
                    Console.WriteLine($"{Environment.NewLine}The {user.coreFirstAnimal} continues eating the {user.coreFirstPlant}. With each bite, the {user.coreFirstAnimal} looks like it's getting stronger.");
                    Console.Write($" It looks like {user.coreColor} sparkling mist is being emitted from the plant! How strange. You gain your first animal companion, the {user.coreFirstAnimal}.");
                    Console.WriteLine($"{Environment.NewLine}You gain 20 exp. You gain 5 MP.");
                    user.coreMP = user.coreMP + 5;
                    break;
                default:
                    Console.WriteLine($"{Environment.NewLine}SSoommeetthhiinngg  ssttrraannggee  hhaappppeenneedd!!  The {user.coreFirstPlant} explodes into a {user.coreColor} smoke.  The {user.coreFirstAnimal} is startled and panics.");
                    Console.WriteLine($"{Environment.NewLine}You gain 20 exp. You lose 1 SPD.");
                    user.coreSPD--;
                    break;
            }


            Console.WriteLine($"{Environment.NewLine}Congrats! You leveled up.");
            LevelUp(user);
            user.PrintCurrentStats();
            user.RestorePoints();
            user.savePoint = 2;
            AskToSave(user);
        }

        static void SecondAnimalEvent(UserCore user)
        {
            Console.WriteLine("Save Point: " + user.savePoint);
            Console.WriteLine($"{Environment.NewLine}Congrats! You leveled up.");
            LevelUp(user);
            user.PrintCurrentStats();
            user.RestorePoints();
            user.savePoint = 5;
            AskToSave(user);
        }

        static void ThirdAnimalEvent(UserCore user)
        {
            Console.WriteLine("Save Point: " + user.savePoint);
            Console.WriteLine($"{Environment.NewLine}Congrats! You leveled up.");
            LevelUp(user);
            user.PrintCurrentStats();
            user.RestorePoints();
            user.savePoint = 12;
            AskToSave(user);
        }

        static void FirstMineralEvent(UserCore user)
        {
            Console.WriteLine("Save Point: " + user.savePoint);
            Console.Write($"{Environment.NewLine}Whew, animals sure are scary. You hope that your new {user.coreFirstAnimal} is happy in its new home.");
            Console.Write($"{Environment.NewLine}Meanwhile, you notice your observation area has grown. There are a couple more plants scattered around.");
            Console.Write($"{Environment.NewLine}Not only that, but there are a bunch of rocks and other minerals you can feel at the {user.coreLandscape}.");
            Console.Write($"{Environment.NewLine}Wow... You focus on your surroundings. Your first mineral is... a {user.coreFirstMineral}! That really rocks.");

            Console.Write($"{Environment.NewLine}What should you do with your new {user.coreFirstMineral}?");
            Console.Write($"{Environment.NewLine}1 - Eat it?");
            Console.Write($"{Environment.NewLine}2 - Mine it?");
            Console.Write($"{Environment.NewLine}3 - Try to grow more?");

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
                    Console.WriteLine($"{Environment.NewLine}Mmm, tasty! You somehow manage to eat the {user.coreFirstMineral}, despite not having a mouth.");
                    Console.WriteLine($"{Environment.NewLine}In geology, you lick science! That's how it works.");
                    Console.WriteLine($"{Environment.NewLine}You gain 30 exp. You gain 5 HP. You gain 1 DEF.");
                    user.coreHP = user.coreHP + 5;
                    user.coreDEF++;
                    break;
                case 2:
                    Console.WriteLine($"{Environment.NewLine}You manage to mine some of the {user.coreFirstMineral}, and it drops to the ground.");
                    Console.WriteLine($"{Environment.NewLine}Now what? You're not sure what to do with it. Your {user.coreFirstAnimal} comes over and sits on it.");
                    Console.WriteLine($"{Environment.NewLine}You gain 30 exp. You gain 1 SPD.");
                    user.coreSPD++;
                    break;
                case 3:
                    Console.WriteLine($"{Environment.NewLine}You try to grow more {user.coreFirstMineral} by focusing intently, and it works!");
                    Console.Write($"Several more {user.coreFirstMineral} veins spawn in your immediate vacinity.");
                    Console.WriteLine($"{Environment.NewLine}You gain 30 exp. You gain 5 MP.");
                    user.coreMP = user.coreMP + 5;
                    break;
                default:
                    Console.WriteLine($"{Environment.NewLine}SSoommeetthhiinngg  ssttrraannggee  hhaappppeenneedd!!");
                    Console.WriteLine($"{Environment.NewLine}You gain 30 exp. You lose 1 SPD.");
                    user.coreSPD--;
                    break;
            }

            Console.WriteLine($"{Environment.NewLine}Congrats! You leveled up.");
            LevelUp(user);
            user.PrintCurrentStats();
            user.RestorePoints();
            user.savePoint = 3;
            AskToSave(user);
        }

        static void SecondMineralEvent(UserCore user)
        {
            Console.WriteLine("Save Point: " + user.savePoint);
            Console.WriteLine($"{Environment.NewLine}Congrats! You leveled up.");
            LevelUp(user);
            user.PrintCurrentStats();
            user.RestorePoints();
            user.savePoint = 6;
            AskToSave(user);
        }

        static void ThirdMineralEvent(UserCore user)
        {
            Console.WriteLine("Save Point: " + user.savePoint);
            Console.WriteLine($"{Environment.NewLine}Congrats! You leveled up.");
            LevelUp(user);
            user.PrintCurrentStats();
            user.RestorePoints();
            user.savePoint = 8;
            AskToSave(user);
        }

        static void FirstAdventurerEvent(UserCore user)
        {
            Console.WriteLine("Save Point: " + user.savePoint);
            Console.WriteLine($"{Environment.NewLine}Congrats! You leveled up.");
            LevelUp(user);
            user.PrintCurrentStats();
            user.RestorePoints();
            user.savePoint = 4;
            AskToSave(user);
        }

        static void SecondAdventurerEvent(UserCore user)
        {
            Console.WriteLine("Save Point: " + user.savePoint);
            Console.WriteLine($"{Environment.NewLine}Congrats! You leveled up.");
            LevelUp(user);
            user.PrintCurrentStats();
            user.RestorePoints();
            user.savePoint = 7;
            AskToSave(user);
        }

        static void ThirdAdventurerEvent(UserCore user)
        {
            Console.WriteLine("Save Point: " + user.savePoint);
            Console.WriteLine($"{Environment.NewLine}Congrats! You leveled up.");
            LevelUp(user);
            user.PrintCurrentStats();
            user.RestorePoints();
            user.savePoint = 11;
            AskToSave(user);
            
        }

        static void FinalEvent(UserCore user)
        {
            Console.WriteLine("Save Point: " + user.savePoint);
            user.savePoint = 13;
            Console.WriteLine("After everything, you're finally a thriving dungeon... You continue living out your days eventfully.");
            Console.WriteLine("Here are your final stats: ");
            user.PrintCurrentStats();
        }

        static void AskToSave(UserCore user)
        {
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

        static void SaveFile(UserCore user)
        {
            Console.WriteLine("Saving your data to json format...");

            string fileName = "userdata.json";
            string jsonString = JsonSerializer.Serialize(user);
            File.WriteAllText(fileName, jsonString);
        }

        static void LoadFile(UserCore user)
        {
          
            Console.WriteLine("Loading save data...");

            string fileName = "userdata.json";
            string jsonString = File.ReadAllText(fileName);
            user = JsonSerializer.Deserialize<UserCore>(jsonString);

            Console.WriteLine($"Welcome back, {user.userName}! Your file was loaded successfully!");
        }
    }
}
