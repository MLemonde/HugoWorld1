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
        
        public void CreateItem(int _x, int _y, string nom, string description, decimal poids, int quantite, int reqDexterite, int reqEndurance, int reqForce, int reqIntelligence, int reqNiveau, int ValeurArgent, decimal valeurArgent)
        {
            using (RpgGameEntities context = new RpgGameEntities())
            {
                context.Items.Add(new Item()
                    {
                        Description = description,
                        //EffetItems,
                        //Heroes,
                        //Id,
                        //Monde,
                        //MondeId,
                        //Niveau,
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
        }

        public void DeleteItem(int id)
        {
            using (RpgGameEntities context = new RpgGameEntities())
            {
                context.Items.FirstOrNull(i => i.Id == id);
                context.SaveChanges();
            }
        }
        public void DeleteItem(string nom)
        {
            using (RpgGameEntities context = new RpgGameEntities())
            {
                Item item = context.Items.FirstOrNull(i => i.Nom == nom);
                if (item != null)
                {
                    context.Items.Remove(item);

                    while (item.Heroes.Count != 0)
                    {
                        item.Heroes.First().Items.Remove(item);
                    }
                    item.Monde.Items.Remove(item);
                }
                context.SaveChanges();
            }
        }

        public void EditItem(int ItemId, int _x, int _y, string nom, string description, decimal poids, int quantite, int reqDexterite, int reqEndurance, int reqForce, int reqIntelligence, int reqNiveau, int ValeurArgent, decimal valeurArgent)
        {
            using (RpgGameEntities context = new RpgGameEntities())
            {
                context.SaveChanges();
            }
        }

        public void EditItem(string nom, int _x, int _y, string description, decimal poids, int quantite, int reqDexterite, int reqEndurance, int reqForce, int reqIntelligence, int reqNiveau, int ValeurArgent, decimal valeurArgent)
        {
            using (RpgGameEntities context = new RpgGameEntities())
            {
                context.SaveChanges();
            }
        }
        
    }
}
