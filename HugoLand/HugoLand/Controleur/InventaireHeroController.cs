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
        HugoWorldEntities context = new HugoWorldEntities();

        
        /// <summary>
        /// Auteur : Mathew Lemonde
        /// </summary>
        /// <param name="heroid"></param>
        /// <param name="itemid"></param>
        /// <returns>False si inventaire plein </returns>
        public bool AddItemToHero(int heroid, int itemid)
        {

            var hero = context.Heroes.FirstOrNull(c => c.Id == heroid);
            if (hero == null)
                return false;

            var Item = context.Items.FirstOrNull(c => c.Id == itemid);
            if (Item == null)
                return false;

            decimal LimiteInventaire = (hero.StatBaseStr * (decimal)hero.Classe.StatPoidsStr) * 10;
            decimal TotalPoids = 0;
            decimal placeDispo = 0;
            foreach (var item in hero.Items)
            {
                TotalPoids += item.Poids;
            }
            placeDispo = LimiteInventaire - TotalPoids;

            if (Item.Poids > placeDispo)
                return false;

            Item.x = 0;
            Item.y = 0;
            hero.Items.Add(Item);
            context.SaveChanges();
            return true;
                  
                      
                
            
        }

        /// <summary>
        /// Auteur: Mathew Lemonde
        /// Oter un item de l'inventaire
        /// </summary>
        /// <param name="heroid"></param>
        /// <param name="itemid"></param>
        public void DeleteItemFromHero(int heroid, int itemid)
        {
            var hero = context.Heroes.FirstOrNull(c => c.Id == heroid);
            if (hero == null)
                return;

            var Item = context.Items.FirstOrNull(c => c.Id == itemid);
            if (Item == null)
                return;

            hero.Items.Remove(Item);
            Item.x = hero.x;
            Item.y = hero.y;
            context.SaveChanges();
        }


    }
}
