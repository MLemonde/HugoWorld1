using HugoLand.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HugoLand.Controleur
{
    class InventaireHeroController
    {
        RpgGameEntities context = new RpgGameEntities();

        public void AddItemToHero(Hero hero, Item item)
        {
            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < 10; y++)
                {
                    if (hero.Items.FirstOrNull(i => i.x == x && i.y == y) == null)
                    {
                        hero.Items.Add(item);
                        return;
                    }
                }
            }
        }

        public void DeleteItemFromHero(Hero hero, Item item)
        {

        }


    }
}
