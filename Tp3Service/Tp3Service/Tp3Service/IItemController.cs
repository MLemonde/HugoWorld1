using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Tp3Service
{
    [ServiceContract]
    interface IItemController
    {
        /// <summary>
        /// Auteur Francis Lussier
        /// </summary>
        [OperationContract]
        void CreateItem(int mondeId, int x, int y, string nom, string description, decimal poids, int quantite, int reqDexterite, int reqEndurance, int reqForce, int reqIntelligence, int reqNiveau, decimal valeurArgent);

        /// <summary>
        /// Auteur Francis Lussier
        /// </summary>
        [OperationContract]
        void DeleteItem(int itemId);

        /// <summary>
        /// Auteur Francis Lussier
        /// </summary>
        [OperationContract]
        void EditItem(int itemId, int quantite);
    }
}
