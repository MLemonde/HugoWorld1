using HugoLand.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HugoLand.Controller
{
    class MonstreController
    {
        /// <summary>
        /// Auteur: Mathew Lemonde
        /// Description : Controlleur pour l'entity monstre
        /// </summary>
        RpgGameEntities context = new RpgGameEntities();

        /// <summary>
        /// Auteur: Mathew Lemonde
        /// 
        /// Creation d'un Monstre avec valeur aléatoires
        /// </summary>
        /// <param name="Mondeid">Id du monde</param>
        public void CreateMonster(int Mondeid)
        {
            var Monde = context.Mondes.FirstOrNull(c => c.Id == Mondeid);
            if (Monde == null)
                return;
            Random _rand = new Random();
            int DmgMin = _rand.Next(0, 100);
            Monstre monster = new Monstre()
            {
                MondeId = Monde.Id,
                Nom = "Monster",
                Niveau = _rand.Next(0,101),
                x= _rand.Next(0,int.Parse(Monde.LimiteX)),
                y = _rand.Next(0, int.Parse(Monde.LimiteY)),
                StatPV = _rand.Next(0,400),
                StatDmgMax = _rand.Next(DmgMin,400)
            };
            context.Monstres.Add(monster);
            Monde.Monstres.Add(monster);
            context.SaveChanges();
        }

        /// <summary>
        /// Auteur: Mathew Lemonde
        /// 
        /// Suppression d'un monstre
        /// </summary>
        /// <param name="monster"></param>
        /// <param name="Mapid">Id du monde</param>
        public void DeleteMonster(Monstre monster,int Mapid)
        {
            var Monde = context.Mondes.FirstOrNull(c => c.Id == Mapid);
            if (Monde == null)
                return;

            context.Monstres.Remove(monster);
            Monde.Monstres.Remove(monster);
            context.SaveChanges();
        }

        /// <summary>
        /// Auteur: Mathew Lemonde
        /// Modification d'un monstre
        /// </summary>
        /// <param name="MonsterID"></param>
        /// <param name="sNom"></param>
        /// <param name="iLvl"></param>
        /// <param name="ix"></param>
        /// <param name="iy"></param>
        /// <param name="iPv"></param>
        /// <param name="iDmgMin"></param>
        /// <param name="iDmgMax"></param>
        public void EditMonster(int MonsterID,string sNom,int iLvl,int ix,int iy, int iPv,int iDmgMin,int iDmgMax)
        {
            Monstre monster = context.Monstres.Find(MonsterID);
            if (monster == null)
                return;
            else
            {
                monster.Nom = sNom;
                monster.Niveau = iLvl;
                monster.x = ix;
                monster.y = iy;
                monster.StatPV = iPv;
                monster.StatDmgMin = iDmgMin;
                monster.StatDmgMax = iDmgMax;
            }
            context.SaveChanges();
        }

    }
}
