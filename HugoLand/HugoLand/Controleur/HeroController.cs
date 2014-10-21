﻿using HugoLand.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HugoLand.Controller
{
    class HeroController
    {
        HugoWorldEntities context;

        public HeroController(HugoWorldEntities db)
        {
            context = db;
        }
        /// <summary>
        /// Auteur Francis Lussier
        /// </summary>
        public void CreateHero(int MondeID,int compteId, int classeId, int X, int Y, int niveau, int dex, int str, int stamina, int Int, long experience, decimal argent)
        {
            var Monde = context.Mondes.FirstOrNull(c => c.Id == MondeID);
            if (Monde == null)
                return;

            Classe classe = context.Classes.FirstOrNull(c => c.Id == classeId);
            CompteJoueur compte = context.CompteJoueurs.FirstOrNull(c => c.Id == compteId);
            if (classe == null || compte == null)
                return;
            
            Hero hero = new Hero()
            {
                MondeId = MondeID,
                Argent = argent,
                ClasseId = classeId,
                CompteJoueurId = compteId,
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

        /// <summary>
        /// Auteur Francis Lussier
        /// </summary>
        public void EditHero(int HeroId, int classeId, int X, int Y, int niveau, int dex, int str, int stamina, int Int, long experience, decimal argent)
        {
            Hero hero = context.Heroes.FirstOrNull(h => h.Id == HeroId);
            Classe classe = context.Classes.FirstOrNull(c => c.Id == classeId);
            if (classe != null && hero != null)
            {
                hero.ClasseId = classeId;
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

        /// <summary>
        /// Auteur Francis Lussier
        /// </summary>
        public void DeleteHero(int HeroId)
        {
            Hero hero = context.Heroes.FirstOrNull(h => h.Id == HeroId);
            if (hero != null)
            {
                hero.Monde.Heroes.Remove(hero);
                hero.CompteJoueur.Heroes.Remove(hero);
                hero.Classe.Heroes.Remove(hero);
                context.Heroes.Remove(hero);

                context.SaveChanges();
            }
        }

        /// <summary>
        /// Auteur Francis Lussier
        /// </summary>
        public List<Hero> GetListHero(int compteId)
        {
            CompteJoueur compte = context.CompteJoueurs.FirstOrNull(c => c.Id == compteId);
            if (compte != null)
                return compte.Heroes.ToList();
            else
                return new List<Hero>();
        }
    }
}
