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
        Entities db = new Entities();

        public void CreateClass(string sNomClasse, string sDescription, float fStatPoidsStr, float fStatPoidsDex,
                                float fStatPoidsInt, float fStatPoidsStam, int iMondeId)
        {
            bool existanceDuMonde = false; // Vérifier que le monde qu'on où on créer la classe existe au minimum...
            foreach (var item in db.Mondes)
            {
                if (item.Id == iMondeId)
                    existanceDuMonde = true;
            }

            if (existanceDuMonde)
            {
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
                db.SaveChanges();
            }
        }

        public void DeleteClass(int iClassID)
        {
            Classe myClasse = db.Classes.Find(iClassID);
            if (myClasse == null)
                return;
            else
                db.Classes.Remove(myClasse);
            db.SaveChanges();
        }

        public void EditClassFromWorld(int iClasseID, string sNomClasse, string sDescription, float fStatPoidsStr, 
                                        float fStatPoidsDex, float fStatPoidsInt, float fStatPoidsStam, int iMondeID)
        {
            Classe myClasse = db.Classes.Find(iClasseID);
            if (myClasse == null)
                return;
            else
            {
                foreach (var item in db.Classes)
                {
                    if(item.MondeId == iMondeID)
                    {
                        myClasse.Description = sNomClasse;
                        myClasse.StatPoidsStr = fStatPoidsStr;
                        myClasse.StatPoidsDex = fStatPoidsDex;
                        myClasse.StatPoidsInt = fStatPoidsInt;
                        myClasse.StatPoidsStam = fStatPoidsStam;
                    }
                }
            }
            db.SaveChanges();
        }

        public List<Classe> GetListMap(int mondeID)
        {
            List<Classe> listOfClasses = new List<Classe>();

            foreach (var item in db.Classes)
            {
                if (item.MondeId == mondeID)
                    listOfClasses.Add(item);
            }
            return listOfClasses;
        }

        // Recevoir toutes les classes pour un monde donné (clé)
        

        public string FindClasseOfHero(int iHeroID, int iMondeID)
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

                return ClassOfHero.ToString();
            }
            else
            {
                return "Le héro ou le monde demander n'existe pas. GG";
            }
        }
    }
}
