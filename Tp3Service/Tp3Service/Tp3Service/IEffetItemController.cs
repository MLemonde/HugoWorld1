using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Tp3Service
{
    [ServiceContract]
    public interface IEffetItemController
    {
        /// <summary>
        /// Auteur Francis
        /// </summary>
        [OperationContract]
        void CreateEffetItem(int itemId, int typeEffet, int valeurEffet);

        /// <summary>
        /// Auteur Francis
        /// </summary>
        [OperationContract]
        void DeleteEffetItem(int effetItemId);

        /// <summary>
        /// Auteur Francis
        /// </summary>
        [OperationContract]
        void EditEffetItem(int effetItemId, int typeEffet, int valeurEffet);
    }
}
