using HugoLand.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HugoLand.Controleur
{
    class MondeController
    {
        RpgGameEntities context = new RpgGameEntities();

        #region MONDE
        public void CreateMap(string iLimiteX, string iLimiteY, string sDescription) //TODO CHECKER PARAMS : DONE??
        {
            Monde monde = new Monde()
            {
                LimiteX = iLimiteX,
                LimiteY = iLimiteY,
                Description = sDescription
            };
            context.Mondes.Add(monde);
            context.SaveChanges();
        }
        public void EditMap(int iID, string sDescription, string iLimiteX, string iLimiteY) //TODO LIMITES
        {
            //Map map = context.Maps.Find(id);
            Monde monde = context.Mondes.Find(iID);
            if (monde == null)
                return;
            else
            {
                //map.Description = sDesc;
                monde.Description = sDescription;
                monde.LimiteX = iLimiteX;
                monde.LimiteY = iLimiteY;
            }
            context.SaveChanges();
        }
        public void EditMap(int iID, string sDescription)
        {
            Monde monde = context.Mondes.Find(iID);
            if (monde == null)
                return;
            else
            {
                monde.Description = sDescription;
            }
            context.SaveChanges();
        }

        public void DeleteMap(Monde monde)
        {
            context.Mondes.Remove(monde);
            context.SaveChanges();
        }

        public List<Monde> GetListMap()
        {
            return context.Mondes.ToList();
        }
        #endregion
    }
}
