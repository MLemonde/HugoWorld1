using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Tp3Service
{
    [ServiceContract]
    public interface ICompteJoueurController
    {
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
        /// <returns>false si deja pris</returns>
        [OperationContract]
        bool CreatePlayer(string sUsername, string sPass, string sEmail, string sFname, string sLname, int iType);

        /// <summary>
        /// Auteur:Mathew Lemonde
        /// Description : Supprimer un compte
        /// </summary>
        /// <param name="sUsername">Nom d'utilisateur</param>
        [OperationContract]
        void DeletePlayer(string sUsername);

        /// <summary>
        /// Auteur: Mathew Lemonde
        /// Description: Modifier un compte a partir du Username
        /// </summary>
        /// <param name="sUsername">Nom d'utilisateur</param>
        /// <param name="sEmail">Adresse email</param>
        /// <param name="sFname">Prénom</param>
        /// <param name="sLname">Nom de Famille</param>
        /// <param name="iType">Type de compte (int) 0=Util, 1=Admin</param>
        [OperationContract]
        void EditPlayer1(string sUsername, string sEmail, string sFname, string sLname, int iType);

        /// <summary>
        /// Auteur: Mathew Lemonde
        /// Description: Modifier un compte a partir du ID
        /// </summary>
        /// <param name="id">id du Compte</param>
        /// <param name="sEmail">Adresse email</param>
        /// <param name="sFname">Prénom</param>
        /// <param name="sLname">Nom de Famille</param>
        /// <param name="iType">Type de compte (int) 0=Util, 1=Admin</param>
        [OperationContract]
        void EditPlayer2(int id, string sEmail, string sFname, string sLname, int iType);

        /// <summary>
        /// Auteur: Mathew Lemonde
        /// Description: Valider le login
        /// </summary>
        /// <param name="sUsername">Nom d'utilisateur</param>
        /// <param name="sPassword">Mot de passe</param>
        /// <returns></returns>
        [OperationContract]
        bool ValidatePlayer(string sUsername, string sPassword);

        /// <summary>
        /// Auteur: Mathew Lemonde
        /// Description: Valider le login d'un admin
        /// </summary>
        /// <param name="sUsername">Nom d'utilisateur</param>
        /// <param name="sPassword">Mot de passe</param>
        /// <returns></returns>
        [OperationContract]
        bool ValidateAdmin(string sUsername, string sPassword);
    }
}
