using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HugoLand.Model;

namespace HugoLand.Controller
{
    class ItemController
    {
        RpgGameEntities context = new RpgGameEntities();

        public void CreateItem(int mondeId, int _x, int _y, string nom, string description, decimal poids, int quantite, int reqDexterite, int reqEndurance, int reqForce, int reqIntelligence, int reqNiveau, int ValeurArgent, decimal valeurArgent)
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
        /// 
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="mondeId"></param>
        /// <param name="_x"></param>
        /// <param name="_y"></param>
        /// <param name="nom"></param>
        /// <param name="description"></param>
        /// <param name="poids"></param>
        /// <param name="quantite"></param>
        /// <param name="reqDexterite"></param>
        /// <param name="reqEndurance"></param>
        /// <param name="reqForce"></param>
        /// <param name="reqIntelligence"></param>
        /// <param name="reqNiveau"></param>
        /// <param name="valeurArgent"></param>
        public void EditItem(int itemId, int mondeId, int _x, int _y, string nom, string description, decimal poids, int quantite, int reqDexterite, int reqEndurance, int reqForce, int reqIntelligence, int reqNiveau, decimal valeurArgent)
        {
            Item item = context.Items.FirstOrNull(i => i.Id == itemId);
            if (item != null)
            {
                item.Description = description;
                item.MondeId = mondeId;
                item.Nom = nom;
                item.Poids = poids;
                item.Quantite = quantite;
                item.ReqDexterite = reqDexterite;
                item.ReqEndurance = reqEndurance;
                item.ReqForce = reqForce;
                item.ReqIntelligence = reqIntelligence;
                item.ReqNiveau = reqNiveau;
                item.ValeurArgent = valeurArgent;
                item.x = _x;
                item.y = _y;

                context.SaveChanges();
            }
        }
        
    }
}
