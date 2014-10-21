using HugoLand.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HugoLand.Controller
{
    /// <summary>
    /// Auteur: Mathew Lemonde
    /// Description : Controlleur de Compte joueur
    /// </summary>
    class CompteJoueurController
    {
        RpgGameEntities context = new RpgGameEntities();


        /// <summary>
        /// Auteur: Mathew Lemonde
        /// Description : Creer un nouveau compte
        /// </summary>
        /// <param name="sUsername"></param>
        /// <param name="sPass"></param>
        /// <param name="sEmail"></param>
        /// <param name="sFname"></param>
        /// <param name="sLname"></param>
        /// <param name="iType"></param>
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
        /// <summary>
        /// Auteur:Mathew Lemonde
        /// Description : Supprimer un compte
        /// </summary>
        /// <param name="id"></param>
        public void DeletePlayer(int id)
        {
            CompteJoueur Account = context.CompteJoueurs.Find(id);
            context.CompteJoueurs.Remove(Account);
            context.SaveChanges();
        }

        /// <summary>
        /// Auteur: Mathew Lemonde
        /// Description: Modifier un compte
        /// </summary>
        /// <param name="id"></param>
        /// <param name="sEmail"></param>
        /// <param name="sFname"></param>
        /// <param name="sLname"></param>
        /// <param name="iType"></param>
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

        /// <summary>
        /// Auteur: Mathew Lemonde
        /// Description: Valider le login
        /// </summary>
        /// <param name="sUsername"></param>
        /// <param name="sPassword"></param>
        /// <returns></returns>
        public bool ValidatePlayer(string sUsername,string sPassword)
        {
            var Account = context.CompteJoueurs.Include("NomUtilisateur").Where(c => c.NomUtilisateur == sUsername).ToList();
            foreach (var uti in Account)
            {
                if (uti.Password == "soada11" + sPassword + "neoi333`")
                    return true;
            }
            return false;
        }
    }
}
