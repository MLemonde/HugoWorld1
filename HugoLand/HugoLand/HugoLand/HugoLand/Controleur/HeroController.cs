using HugoLand.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HugoLand.Controleur
{
    class HeroController
    {
        RpgGameEntities context = new RpgGameEntities();

        public void CreateHero(CompteJoueur compte, int X, int Y, int niveau, int dex, int str, int stamina, int Int, long experience, decimal argent)
        {
            Hero hero = new Hero()
            {
                Argent = argent,
                //Classe = classe,
                CompteJoueur = compte,
                Experience = experience,
                Niveau = niveau,
                StatBaseDex = dex,
                StatBaseInt = Int,
                StatBaseStam = stamina,
                StatBaseStr = str,
                x = X,
                y = Y
            };

            context.Heroes.Add(hero);
            context.SaveChanges();
        }

        public void EditHero(int ID, int X, int Y, int niveau, int dex, int str, int stamina, int Int, long experience, decimal argent)
        {
            Hero hero = context.Heroes.FirstOrNull(h => h.Id == ID);
            if (hero != null)
            {
                hero.x = X;
                hero.y = Y;
                hero.Niveau = niveau;
                hero.StatBaseDex = dex;
                hero.StatBaseStr = str;
                hero.StatBaseStam = stamina;
                hero.StatBaseInt = Int;
                hero.Experience = experience;
                hero.Argent = argent;

                context.SaveChanges();
            }
        }

        public void DeleteHero(int ID)
        {
            Hero hero = context.Heroes.FirstOrNull(h => h.Id == ID);
            if (hero != null)
            {
                hero.Monde.Heroes.Remove(hero);
                hero.CompteJoueur.Heroes.Remove(hero);
                hero.Classe.Heroes.Remove(hero);
                context.Heroes.Remove(hero);

                context.SaveChanges();
            }
        }

        public List<Hero> GetListHero(CompteJoueur compte)
        {
            return compte.Heroes.ToList();
        }
    }
}
