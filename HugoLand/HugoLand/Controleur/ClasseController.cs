using HugoLand.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HugoLand.Controller
{
    class ClasseController
    {
        RpgGameEntities db = new RpgGameEntities();
        /// <summary>
        /// Auteur : Marc-André Landry
        /// </summary>
        /// <param name="sNomClasse"></param>
        /// <param name="sDescription"></param>
        /// <param name="fStatPoidsStr">Facteur bonus/malus Strength</param>
        /// <param name="fStatPoidsDex">Facteur bonus/malus Dex</param>
        /// <param name="fStatPoidsInt">Facteur bonus/malus Intel</param>
        /// <param name="fStatPoidsStam">Facteur bonus/malus Stamina</param>
        /// <param name="iMondeId">Monde dans lequel la classe est créer</param>
        public void CreateClass(string sNomClasse, string sDescription, float fStatPoidsStr, float fStatPoidsDex,
                                float fStatPoidsInt, float fStatPoidsStam, int iMondeId)
        {
            var Monde = db.Mondes.FirstOrNull(c => c.Id == iMondeId);
            if (Monde == null)
                return;

            

            
                Classe myClass = new Classe()
                {
                    NomClasse = sNomClasse,
                    Description = sDescription,
                    StatPoidsStr = fStatPoidsStr,
                    StatPoidsDex = fStatPoidsDex,
                    StatPoidsInt = fStatPoidsInt,
                    StatPoidsStam = fStatPoidsStam,
                    MondeId = iMondeId
                };
                db.Classes.Add(myClass);
                Monde.Classes.Add(myClass);
                db.SaveChanges();
            
        }

        /// <summary>
        /// Auteur: Marc-André Landry
        /// </summary>
        /// <param name="iClassID"></param>
        public void DeleteClass(int iClassID)
        {
            Classe myClasse = db.Classes.Find(iClassID);
            if (myClasse == null)
                return;

            myClasse.Monde.Classes.Remove(myClasse);
            db.Classes.Remove(myClasse);
            db.SaveChanges();
        }

        /// <summary>
        /// Auteur : Marc-André Landry
        /// </summary>
        /// <param name="iClasseID">Id de la classe</param>
        /// <param name="sNomClasse">Nom de la classe</param>
        /// <param name="sDescription">Description de la classe</param>
        /// <param name="fStatPoidsStr">Facteur bonus/malus Strength</param>
        /// <param name="fStatPoidsDex">Facteur bonus/malus Dex</param>
        /// <param name="fStatPoidsInt">Facteur bonus/malus Intel</param>
        /// <param name="fStatPoidsStam">Facteur bonus/malus Stamina</param>
        /// <param name="iMondeID">Id du monde</param>
        public void EditClassFromWorld(int iClasseID, string sNomClasse, string sDescription, float fStatPoidsStr, 
                                        float fStatPoidsDex, float fStatPoidsInt, float fStatPoidsStam, int iMondeID)
        {
            Classe myClasse = db.Classes.Find(iClasseID);
            if (myClasse == null)
                return;

            if (myClasse.MondeId != iMondeID)
                return;

                
             myClasse.Description = sNomClasse;
             myClasse.StatPoidsStr = fStatPoidsStr;
             myClasse.StatPoidsDex = fStatPoidsDex;
             myClasse.StatPoidsInt = fStatPoidsInt;
             myClasse.StatPoidsStam = fStatPoidsStam;
                    
                
            
            db.SaveChanges();
        }

        /// <summary>
        /// Auteur: Mathew Lemonde
        /// Retourne liste de classe
        /// </summary>
        /// <param name="mondeID"></param>
        /// <returns></returns>
        public List<Classe> GetListClasses(int mondeID)
        {
            var Monde = db.Mondes.FirstOrNull(c => c.Id == mondeID);
            if (Monde == null)
                return null;

            return Monde.Classes.ToList();            
        }
        
        /// <summary>
        /// Auteur : Marc-André Landry
        /// Trouver la classe d'un héro
        /// </summary>
        /// <param name="iHeroID">Id du héro</param>
        /// <param name="iMondeID">Id du Monde</param>
        /// <returns></returns>
        public Classe FindClasseOfHero(int iHeroID, int iMondeID)
        {
            bool WorldExistance = false;
            bool HeroExistance = false;

            foreach (var item in db.Mondes)
            {
                if (item.Id == iMondeID)
                    WorldExistance = true;
            }
            foreach (var item in db.Heroes)
            {
                if (item.Id == iHeroID)
                    HeroExistance = true;
            }

            if(WorldExistance && HeroExistance)
            {
                var ClassOfHero = from p in db.Heroes
                           where p.MondeId.Equals(iMondeID) &&
                           p.Id == iHeroID
                           select p.Classe;

                return ClassOfHero.First();
            }
            else
            {
                return null;
            }
        }
    }
}
