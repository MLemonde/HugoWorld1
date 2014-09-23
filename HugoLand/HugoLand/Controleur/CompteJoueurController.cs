using HugoLand.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HugoLand.Controleur
{
    class CompteJoueurController
    {
        RpgGameEntities context = new RpgGameEntities();


        public void CreatePlayer(string sUsername,string sPass,string sEmail,string sFname,string sLname,int iType)
        {          
            //TODO VRAI HASH
            string sPassword = "soada11" + sPass + "neoi333`";
            CompteJoueur Account = new CompteJoueur()
            {
                Courriel = sEmail,
                NomUtilisateur = sUsername,
                TypeUtilisateur = iType,
                Nom = sLname,
                Prenom = sFname,
                Password = sPassword
            };
            context.CompteJoueurs.Add(Account);
            context.SaveChanges();

        }
        public void DeletePlayer(int id)
        {
            CompteJoueur Account = context.CompteJoueurs.Find(id);
            context.CompteJoueurs.Remove(Account);
            context.SaveChanges();
        }

        public void EditPlayer(int id,string sEmail,string sFname,string sLname,int iType)
        {
            CompteJoueur Account = context.CompteJoueurs.Find(id);
            if (Account == null)
                return;

            Account.Courriel = sEmail;
            Account.Prenom = sFname;
            Account.Nom = sLname;
            Account.TypeUtilisateur = iType;

            context.SaveChanges();
        }

        public void ValidatePlayer(string sUsername,string sPassword)
        {
            
        }
    }
}
