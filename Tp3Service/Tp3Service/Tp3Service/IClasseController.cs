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
    public interface IClasseController
    {
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
        [OperationContract]
        void CreateClass(string sNomClasse, string sDescription, float fStatPoidsStr, float fStatPoidsDex, float fStatPoidsInt, float fStatPoidsStam, int iMondeId);

        /// <summary>
        /// Auteur: Marc-André Landry
        /// </summary>
        /// <param name="iClassID"></param>
        [OperationContract]
        void DeleteClass(int iClassID);

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
        [OperationContract]
        void EditClassFromWorld(int iClasseID, string sNomClasse, string sDescription, float fStatPoidsStr, float fStatPoidsDex, float fStatPoidsInt, float fStatPoidsStam, int iMondeID);

        /// <summary>
        /// Auteur: Mathew Lemonde
        /// Retourne liste de classe
        /// </summary>
        /// <param name="mondeID"></param>
        /// <returns></returns>
        [OperationContract]
        List<Classe> GetListClasses(int mondeID);

        [OperationContract]
        string GetClassDescription(string sClassName);


        /// <summary>
        /// Auteur: MAL
        /// Description: Recevoir le ID de la classe qui vient d'être sélectionnée
        /// </summary>
        /// <param name="sUsername">Nom de la classe</param>
        /// <returns></returns>
        [OperationContract]
        int? GetClassID(string sClassName);

        /// <summary>
        /// Auteur : Marc-André Landry
        /// Trouver la classe d'un héro
        /// </summary>
        /// <param name="iHeroID">Id du héro</param>
        /// <param name="iMondeID">Id du Monde</param>
        /// <returns></returns>
        [OperationContract]
        Classe FindClasseOfHero(int iHeroID, int iMondeID);
    }
}
