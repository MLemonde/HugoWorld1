using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Tp3Service
{
    [ServiceContract]
    public interface IObjectMondeController
    {
        /// <summary>
        /// Auteur : Marc-André Landry
        /// Create an object (ex: scenery, rock, water, etc) in the world
        /// </summary>
        /// <param name="iX">Where should your object be put m8? (x)</param>
        /// <param name="iY">Where should your object be put m8? (y)</param>
        /// <param name="sDescription">Gimme a small description of your object mate</param>
        /// <param name="iTypeObjet">What's the type of your object (WARNING : integers)</param>
        /// <param name="iMondeId">The ID of your world</param>
        [OperationContract]
        void CreateObjectMonde(int iX, int iY, string sDescription, int iTypeObjet, int iMondeId);

        /// <summary>
        /// Auteur : Marc-André Landry
        /// 
        /// Delete an object from the world by its ID
        /// </summary>
        /// <param name="iID">The ID of the object you want to destroy</param>
        /// <param name="mondeid">Id du monde</param>
        [OperationContract]
        void DeleteObjectMonde(int objectMondeId, int mondeid);

        /// <summary>
        /// Auteur : Marc-André Landry
        /// Modifi la description d'un object monde avec son ID
        /// </summary>
        /// <param name="iID">Id de l'objet</param>
        /// <param name="sDescription">Nouvelle description</param>
        [OperationContract]
        void EditObjectMondeDescription(int objectMondeId, string sDescription);
    }
}
