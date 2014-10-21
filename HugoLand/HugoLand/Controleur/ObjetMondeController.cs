using HugoLand.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HugoLand.Controller
{
    class ObjetMondeController
    {
        RpgGameEntities db = new RpgGameEntities();

        /// <summary>
        /// Create an object (ex: scenery, rock, water, etc) in the world
        /// </summary>
        /// <param name="iX">Where should your object be put m8? (x)</param>
        /// <param name="iY">Where should your object be put m8? (y)</param>
        /// <param name="sDescription">Gimme a small description of your object mate</param>
        /// <param name="iTypeObjet">What's the type of your object (WARNING : integers)</param>
        /// <param name="iMondeId">The ID of your world</param>
        public void CreateObjectMonde(int iX, int iY, string sDescription, int iTypeObjet, int iMondeId)
        {
            ObjetMonde objMonde = new ObjetMonde()
            {
                x = iX,
                y = iY,
                Description = sDescription,
                TypeObjet = iTypeObjet,
                MondeId = iMondeId
            };
            db.ObjetMondes.Add(objMonde);
            db.SaveChanges();
        }

        /// <summary>
        /// Delete an object from the world by its ID
        /// </summary>
        /// <param name="iID">The ID of the object you want to destroy</param>
        public void DeleteObjectMonde(int iID)
        {
            ObjetMonde objMonde = db.ObjetMondes.Find(iID);
            if (objMonde == null)
                return;
            else
                db.ObjetMondes.Remove(objMonde);

            db.SaveChanges();
        }

        /// <summary>
        /// Delete an object from the world by its type
        /// </summary>
        /// <param name="iTypeObject">The type of the objects</param>
        //public void DeleteObjectMonde(int iTypeObject)
        //{
        //    var objetMondeID = (from p in db.ObjetMondes
        //                        where p.TypeObjet.Equals(iTypeObject)
        //                        select p.MondeId);

        //    ObjetMonde objMonde = db.ObjetMondes.Find(objetMondeID);

        //    if (objMonde == null)
        //        return;
        //    else
        //        db.ObjetMondes.Remove(objMonde);

        //    db.SaveChanges();
        //}

        public void EditObjectMondeDescription(int iID, string sDescription)
        {
            ObjetMonde objMonde = db.ObjetMondes.Find(iID);
            if (objMonde == null)
                return;
            else
                objMonde.Description = sDescription;

            db.SaveChanges();
        }
    }
}
