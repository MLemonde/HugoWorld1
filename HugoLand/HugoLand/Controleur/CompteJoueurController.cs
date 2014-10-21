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
        HugoWorldEntities context;
        public CompteJoueurController(HugoWorldEntities contextt)
        {
            context = contextt;
        }

        /// <summary>
        /// Auteur: Mathew Lemonde
        /// Description : Creer un nouveau compte
        /// </summary>
        /// <param name="sUsername">Nom d'Utilisateur</param>
        /// <param name="sPass">Mot de passe</param>
        /// <param name="sEmail">Adresse e-mail</param>
        /// <param name="sFname">Prénom</param>
        /// <param name="sLname">Nom de Famille</param>
        /// <param name="iType"> Type de compte (int)  0=Util, 1=Admin</param>
        public bool CreatePlayer(string sUsername,string sPass,string sEmail,string sFname,string sLname,int iType)
        {
            var account = context.CompteJoueurs.FirstOrNull(c => c.NomUtilisateur == sUsername);
            if (account != null)
                return false;

            string sPassword = PasswordHash.CreateHash(sPass);

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
            return true;
        }
        /// <summary>
        /// Auteur:Mathew Lemonde
        /// Description : Supprimer un compte
        /// </summary>
        /// <param name="sUsername">Nom d'utilisateur</param>
        public void DeletePlayer(string sUsername)
        {
            var Account = context.CompteJoueurs.FirstOrNull(c => c.NomUtilisateur == sUsername);
            if (Account == null)
                return;

                context.CompteJoueurs.Remove(Account);
                context.SaveChanges();
            
        }

        /// <summary>
        /// Auteur: Mathew Lemonde
        /// Description: Modifier un compte a partir du Username
        /// </summary>
        /// <param name="sUsername">Nom d'utilisateur</param>
        /// <param name="sEmail">Adresse email</param>
        /// <param name="sFname">Prénom</param>
        /// <param name="sLname">Nom de Famille</param>
        /// <param name="iType">Type de compte (int) 0=Util, 1=Admin</param>
        public void EditPlayer(string sUsername,string sEmail,string sFname,string sLname,int iType)
        {
            var Account = context.CompteJoueurs.FirstOrNull(c => c.NomUtilisateur == sUsername);
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
        /// Description: Modifier un compte a partir du ID
        /// </summary>
        /// <param name="id">id du Compte</param>
        /// <param name="sEmail">Adresse email</param>
        /// <param name="sFname">Prénom</param>
        /// <param name="sLname">Nom de Famille</param>
        /// <param name="iType">Type de compte (int) 0=Util, 1=Admin</param>
        public void EditPlayer(int id, string sEmail, string sFname, string sLname, int iType)
        {
            var Account = context.CompteJoueurs.FirstOrNull(c => c.Id == id);
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
        /// <param name="sUsername">Nom d'utilisateur</param>
        /// <param name="sPassword">Mot de passe</param>
        /// <returns></returns>
        public bool ValidatePlayer(string sUsername,string sPassword)
        {
            var Account = context.CompteJoueurs.FirstOrNull(c => c.NomUtilisateur == sUsername);
            if (Account == null)
                return false;
            
            if (PasswordHash.ValidatePassword(sPassword,Account.Password))
            return true;
            
            return false;
        }
    }
}
