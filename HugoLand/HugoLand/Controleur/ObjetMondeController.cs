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


        public ObjetMondeController(RpgGameEntities context)
        {
            db = context;
        }
        /// <summary>
        /// Auteur : Marc-André Landry
        /// Create an object (ex: scenery, rock, water, etc) in the world
        /// </summary>
        /// <param name="iX">Where should your object be put m8? (x)</param>
        /// <param name="iY">Where should your object be put m8? (y)</param>
        /// <param name="sDescription">Gimme a small description of your object mate</param>
        /// <param name="iTypeObjet">What's the type of your object (WARNING : integers)</param>
        /// <param name="iMondeId">The ID of your world</param>
        public void CreateObjectMonde(int iX, int iY, string sDescription, int iTypeObjet, int iMondeId)
        {
            var Monde = db.Mondes.FirstOrNull(c => c.Id == iMondeId);
            if (Monde == null)
                return;
            ObjetMonde objMonde = new ObjetMonde()
            {
                x = iX,
                y = iY,
                Description = sDescription,
                TypeObjet = iTypeObjet,
                MondeId = iMondeId
            };
            db.ObjetMondes.Add(objMonde);
            Monde.ObjetMondes.Add(objMonde);
            db.SaveChanges();
        }

        /// <summary>
        /// Auteur : Marc-André Landry
        /// 
        /// Delete an object from the world by its ID
        /// </summary>
        /// <param name="iID">The ID of the object you want to destroy</param>
        /// <param name="mondeid">Id du monde</param>
        public void DeleteObjectMonde(int objectMondeId,int mondeid)
        {
            Monde monde = db.Mondes.FirstOrNull(c => c.Id == mondeid);
            ObjetMonde objMonde = db.ObjetMondes.FirstOrNull(o => o.Id == objectMondeId);
            if (objMonde == null || monde == null)
                return;
            
            db.ObjetMondes.Remove(objMonde);
            monde.ObjetMondes.Remove(objMonde);
            db.SaveChanges();
        }

        

        /// <summary>
        /// Auteur : Marc-André Landry
        /// Modifi la description d'un object monde avec son ID
        /// </summary>
        /// <param name="iID">Id de l'objet</param>
        /// <param name="sDescription">Nouvelle description</param>
        public void EditObjectMondeDescription(int objectMondeId, string sDescription)
        {
            ObjetMonde objMonde = db.ObjetMondes.Find(objectMondeId);
            if (objMonde == null)
                return;
            else
                objMonde.Description = sDescription;

            db.SaveChanges();
        }
    }
}
