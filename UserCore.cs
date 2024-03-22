using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCore
{
    internal class UserCore
    {
        public String userName { get; set; }
        public String coreColor {get; set;}
        public String coreShape { get; set;}
        public String coreLandscape { get; set;}
        public String coreFirstPlant { get; set;}
        public String coreFirstAnimal { get; set;}
        public String coreFirstMineral { get; set;}
        public int coreLevel { get; set;}
        public int coreHP { get; set;}
        public int coreMaxHP { get; set;}
        public int coreMP { get; set;}
        public int coreMaxMP { get; set;}
        public int coreATK { get; set;}
        public int coreDEF { get; set;}
        public int coreSPD { get; set;}

        public UserCore() {
            coreLevel = 1;
            coreHP = 1; coreMaxHP = 1;
            coreMP = 1; coreMaxMP = 1;
            coreATK = 1; coreDEF = 1;
            coreSPD = 1;
        }

        public void PrintCurrentStats()
        {
            Console.WriteLine($"{Environment.NewLine}These are your current stats...");
            Console.WriteLine($"{Environment.NewLine}Level: {this.coreLevel}");
            Console.WriteLine($"{Environment.NewLine}HP: {this.coreHP}/{this.coreMaxHP}");
            Console.WriteLine($"{Environment.NewLine}MP: {this.coreMP}/{this.coreMaxMP}");
            Console.WriteLine($"{Environment.NewLine}DEF: {this.coreDEF}");
            Console.WriteLine($"{Environment.NewLine}ATK: {this.coreATK}");
            Console.WriteLine($"{Environment.NewLine}SPD: {this.coreSPD}");
        }
    }
}
