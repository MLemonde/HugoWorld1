using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HugoWorldServiceRef;
namespace HugoWorld
{
    public static class Data
    {
        public static int UserId { get; set; }
        public static int CurrentHeroId { get; set; }
        public static int WorldId { get; set; }
        public static int ClassId { get; set; }
        public static string HeroName { get; set; }
        public static int Attack { get; set; }
        public static int Def { get; set; }
        public static int Potion { get; set; }
        public static int Lvl { get; set; }
        public static int Exp { get; set; }
        public static int vie { get; set; }
        public static int  Argent { get; set; }
        public static int Dex { get; set; }
        public static int Str { get; set; }
        public static int Stam { get; set; }
        public static int Intel { get; set; }

        public static ClasseControllerClient ClassController = new ClasseControllerClient();
        public static CompteJoueurControllerClient CompteJoueurController = new CompteJoueurControllerClient();
        public static EffetItemControllerClient EffetItemController = new EffetItemControllerClient();
        public static HeroControllerClient HeroController = new HeroControllerClient();
        public static InventaireHeroControllerClient InventaireHeroController = new InventaireHeroControllerClient();
        public static ItemControllerClient ItemController = new ItemControllerClient();
        public static MondeControllerClient MondeController = new MondeControllerClient();
        public static MonstreControllerClient MonstreController = new MonstreControllerClient();
        public static ObjectMondeControllerClient ObjectMondeController = new ObjectMondeControllerClient();
    }
}
