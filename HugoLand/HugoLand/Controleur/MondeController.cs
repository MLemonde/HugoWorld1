using HugoLand.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HugoLand.Controller
{
    class MondeController
    {
        Entities db = new Entities();

        #region MONDE

        /// <summary>
        /// Create a new world
        /// </summary>
        /// <param name="iLimiteX">Limit of the world (x)</param>
        /// <param name="iLimiteY">Limit of the world (y)</param>
        /// <param name="sDescription">A small description of your new world!</param>
        public void CreateMap(string iLimiteX, string iLimiteY, string sDescription)
        {
            Monde monde = new Monde()
            {
                LimiteX = iLimiteX,
                LimiteY = iLimiteY,
                Description = sDescription
            };
            db.Mondes.Add(monde);
            db.SaveChanges();
        }

        /// <summary>
        /// Edit the world you want.
        /// </summary>
        /// <param name="iID">The ID of the world you wanna change</param>
        /// <param name="sDescription">The new description of the world you wanna change</param>
        /// <param name="iLimiteX">The new limit of the world (x)</param>
        /// <param name="iLimiteY">The new limit of the world (y)</param>
        public void EditMap(int iID, string sDescription, string iLimiteX, string iLimiteY)
        {
            Monde monde = db.Mondes.Find(iID);
            if (monde == null)
                return;
            else
            {
                monde.Description = sDescription;
                monde.LimiteX = iLimiteX;
                monde.LimiteY = iLimiteY;
            }
            db.SaveChanges();
        }

        /// <summary>
        /// This method is used if you wanna change only the description of the world
        /// </summary>
        /// <param name="iID">The ID of the world you wanna change</param>
        /// <param name="sDescription">The new description of the world</param>
        public void EditMap(int iID, string sDescription)
        {
            Monde monde = db.Mondes.Find(iID);
            if (monde == null)
                return;
            else
                monde.Description = sDescription;

            db.SaveChanges();
        }

        /// <summary>
        /// This method is used if you want to change only the positions of the world
        /// </summary>
        /// <param name="iID">The ID of the world you wanna change</param>
        /// <param name="iLimiteX">The new limit of the world (x)</param>
        /// <param name="iLimiteY">The new limit of the world (y)</param>
        public void EditMap(int iID, string iLimiteX, string iLimiteY)
        {
            Monde monde = db.Mondes.Find(iID);
            if (monde == null)
                return;
            else
            {
                monde.LimiteX = iLimiteX;
                monde.LimiteY = iLimiteY;
            }
            db.SaveChanges();
        }

        /// <summary>
        /// Use this methode if you ever wanna delete the world
        /// </summary>
        /// <param name="iID">The ID of the world you wanna destroy</param>
        public void DeleteMap(int iID)
        {
            Monde monde = db.Mondes.Find(iID);
            if (monde == null)
                return;
            else
                db.Mondes.Remove(monde);
            db.SaveChanges();
        }

        public List<Monde> GetListMap()
        {
            return db.Mondes.ToList(); 
        }

        #endregion
    }
}
