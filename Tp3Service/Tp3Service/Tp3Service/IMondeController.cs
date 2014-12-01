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
    public interface IMondeController
    {
        /// <summary>
        /// Auteur: Marc-André Landry
        /// Create a new world
        /// </summary>
        /// <param name="iLimiteX">Limit of the world (x)</param>
        /// <param name="iLimiteY">Limit of the world (y)</param>
        /// <param name="sDescription">A small description of your new world!</param>
        [OperationContract]
        void CreateMonde(string iLimiteX, string iLimiteY, string sDescription);

        /// <summary>
        /// Auteur: Marc-André Landry
        /// Edit the world you want.
        /// </summary>
        /// <param name="iID">The ID of the world you wanna change</param>
        /// <param name="sDescription">The new description of the world you wanna change</param>
        /// <param name="iLimiteX">The new limit of the world (x)</param>
        /// <param name="iLimiteY">The new limit of the world (y)</param>
        [OperationContract]
        void EditMonde1(int iID, string sDescription, string iLimiteX, string iLimiteY);

        /// <summary>
        /// Auteur: Marc-André Landry        /// 
        /// This method is used if you wanna change only the description of the world
        /// </summary>
        /// <param name="iID">The ID of the world you wanna change</param>
        /// <param name="sDescription">The new description of the world</param>
        [OperationContract]
        void EditMonde2(int iID, string sDescription);

        /// <summary>
        /// Auteur: Marc-André Landry        /// 
        /// This method is used if you want to change only the positions of the world
        /// </summary>
        /// <param name="iID">The ID of the world you wanna change</param>
        /// <param name="iLimiteX">The new limit of the world (x)</param>
        /// <param name="iLimiteY">The new limit of the world (y)</param>
        [OperationContract]
        void EditMonde3(int iID, string iLimiteX, string iLimiteY);

        /// <summary>
        /// Auteur: Marc-André Landry
        /// 
        /// Use this methode if you ever wanna delete the world
        /// </summary>
        /// <param name="iID">The ID of the world you wanna destroy</param>
        [OperationContract]
        void DeleteMonde(int iID);

        /// <summary>
        /// Auteur : Marc-André L.
        /// Descrption : Use this method to get the current world ID
        /// </summary>
        /// <param name="Description">the description of the world</param>
        [OperationContract]
        int? GetWorldID(string sDescription);

        /// <summary>
        /// Retourne la liste des mondes du context!
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<Monde> GetListMonde();

    }
}
