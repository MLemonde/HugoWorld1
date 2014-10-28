using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HugoLand.Model;

namespace HugoLand.Controller
{
    public class ItemController
    {
        HugoWorldEntities context;

        public ItemController(HugoWorldEntities contextt)
        {
            context = contextt;
        }
        /// <summary>
        /// Auteur Francis Lussier
        /// </summary>
        public void CreateItem(int mondeId, int _x, int _y, string nom, string description, decimal poids, int quantite, int reqDexterite, int reqEndurance, int reqForce, int reqIntelligence, int reqNiveau, decimal valeurArgent)
        {
            context.Items.Add(new Item()
                {
                    Description = description,
                    MondeId = mondeId,
                    Nom = nom,
                    Poids = poids,
                    Quantite = quantite,
                    ReqDexterite = reqDexterite,
                    ReqEndurance = reqEndurance,
                    ReqForce = reqForce,
                    ReqIntelligence = reqIntelligence,
                    ReqNiveau = reqNiveau,
                    ValeurArgent = valeurArgent,
                    x = _x,
                    y = _y
                });
            context.SaveChanges();
        }

        /// <summary>
        /// Auteur Francis Lussier
        /// </summary>
        public void DeleteItem(int itemId)
        {
            Item item = context.Items.FirstOrNull(i => i.Id == itemId);
            if (item != null)
            {
                context.Items.Remove(item);
                context.SaveChanges();
            }
        }
        
        /// <summary>
        /// Auteur Francis Lussier
        /// </summary>
        public void EditItem(int itemId, int quantite)
        {
            Item item = context.Items.FirstOrNull(i => i.Id == itemId);
            if (item != null)
            {
                item.Quantite = quantite;                
                context.SaveChanges();
            }
        }
    }
}
