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
    public interface IHeroController
    {
        /// <summary>
        /// Auteur Francis Lussier
        /// </summary>
        [OperationContract]
        void CreateHero(int MondeID, int compteId, int classeId, int X, int Y, int niveau, int dex, int str, int stamina, int Int, long experience, decimal argent,string name);

        /// <summary>
        /// Auteur Francis Lussier
        /// </summary>
        [OperationContract]
        void EditHero(int HeroId, int classeId, int X, int Y, int niveau, int dex, int str, int stamina, int Int, long experience, decimal argent);

        [OperationContract]
        void SetHeroPos(int HeroId, int x, int y, string area);

        [OperationContract]
        void ConnectHero(int heroid);
        /// <summary>
        /// Auteur Francis Lussier
        /// </summary>
        [OperationContract]
        void DeleteHero(int HeroId);

        /// <summary>
        /// Auteur Francis Lussier
        /// </summary>
        [OperationContract]
        List<Hero> GetListHero(int compteId);


        [OperationContract]
        void DeconnectHero(int heroId);

        /// <summary>
        /// Auteur Francis Lussier
        /// </summary>
        [OperationContract]
        List<Hero> GetListHeroNearHero(int heroId);

        /// <summary>
        /// Auteur Francis
        /// retourne une liste de objects (ObjetMonde, Monstre, Item, Héro) qui se trouve dans le rayon de 200 par 200 du héro.
        /// </summary>
        /// <param name="HeroId"></param>
        [OperationContract]
        Elements GetElementsArroundHero(int HeroId);

        
    }
    [DataContract]
    public class Elements
    {
        public List<Monstre> lstmonstre;
        public List<Item> lstitem;
        public List<ObjetMonde> lstobj;
        public List<Hero> lstHero;

    }
}
