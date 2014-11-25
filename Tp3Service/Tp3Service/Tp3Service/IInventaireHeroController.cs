using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Tp3Service
{
    [ServiceContract]
    public interface IInventaireHeroController
    {
        /// <summary>
        /// Auteur : Mathew Lemonde
        /// </summary>
        /// <param name="heroid"></param>
        /// <param name="itemid"></param>
        /// <returns>False si inventaire plein </returns>
        [OperationContract]
        bool AddItemToHero(int heroid, int itemid);

        /// <summary>
        /// Auteur: Mathew Lemonde
        /// Oter un item de l'inventaire
        /// </summary>
        /// <param name="heroid"></param>
        /// <param name="itemid"></param>
        [OperationContract]
        void DeleteItemFromHero(int heroid, int itemid);
    }
}
