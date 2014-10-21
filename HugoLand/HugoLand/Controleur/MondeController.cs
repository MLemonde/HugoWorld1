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
        RpgGameEntities db = new RpgGameEntities();

        /// <summary>
        /// Auteur: Marc-André Landry
        /// Create a new world
        /// </summary>
        /// <param name="iLimiteX">Limit of the world (x)</param>
        /// <param name="iLimiteY">Limit of the world (y)</param>
        /// <param name="sDescription">A small description of your new world!</param>
        public void CreateMonde(string iLimiteX, string iLimiteY, string sDescription)
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
        /// Auteur: Marc-André Landry
        /// Edit the world you want.
        /// </summary>
        /// <param name="iID">The ID of the world you wanna change</param>
        /// <param name="sDescription">The new description of the world you wanna change</param>
        /// <param name="iLimiteX">The new limit of the world (x)</param>
        /// <param name="iLimiteY">The new limit of the world (y)</param>
        public void EditMonde(int iID, string sDescription, string iLimiteX, string iLimiteY)
        {
            var Monde = db.Mondes.FirstOrNull(c => c.Id == iID);
            if (Monde == null)
                return;
            
                Monde.Description = sDescription;
                Monde.LimiteX = iLimiteX;
                Monde.LimiteY = iLimiteY;
            
            db.SaveChanges();
        }

        /// <summary>
        /// Auteur: Marc-André Landry        /// 
        /// This method is used if you wanna change only the description of the world
        /// </summary>
        /// <param name="iID">The ID of the world you wanna change</param>
        /// <param name="sDescription">The new description of the world</param>
        public void EditMonde(int iID, string sDescription)
        {
            var monde = db.Mondes.FirstOrNull(c => c.Id == iID);
            if (monde == null)
                return;
            
                monde.Description = sDescription;

            db.SaveChanges();
        }

        /// <summary>
        /// Auteur: Marc-André Landry        /// 
        /// This method is used if you want to change only the positions of the world
        /// </summary>
        /// <param name="iID">The ID of the world you wanna change</param>
        /// <param name="iLimiteX">The new limit of the world (x)</param>
        /// <param name="iLimiteY">The new limit of the world (y)</param>
        public void EditMonde(int iID, string iLimiteX, string iLimiteY)
        {
            var monde = db.Mondes.FirstOrNull(c => c.Id == iID);
            if (monde == null)
                return;
            
            
                monde.LimiteX = iLimiteX;
                monde.LimiteY = iLimiteY;
            
            db.SaveChanges();
        }

        /// <summary>
        /// Auteur: Marc-André Landry
        /// 
        /// Use this methode if you ever wanna delete the world
        /// </summary>
        /// <param name="iID">The ID of the world you wanna destroy</param>
        public void DeleteMonde(int iID)
        {
            var monde = db.Mondes.FirstOrNull(c => c.Id == iID);
            if (monde == null)
                return;
            
            db.Mondes.Remove(monde);
            db.SaveChanges();
        }

        public List<Monde> GetListMonde()
        {
            return db.Mondes.ToList(); 
        }

    }
}
