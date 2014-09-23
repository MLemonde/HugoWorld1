using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HugoLand.Model;

namespace HugoLand.Controleur
{
    class ItemController
    {
        
        public void CreateItem(int? _x, int? _y, string nom, string description, decimal poids, int quantite, int reqDexterite, int reqEndurance, int reqForce, int reqIntelligence, int redNiveau, int ValeurArgent, int reqNiveau, decimal? valeurArgent)
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

                context.SaveChanges();
            }
        }
        public void EditItem(int ItemId)
        {
            using (RpgGameEntities context = new RpgGameEntities())
            {
                context.SaveChanges();
            }
        }
        
    }
}
