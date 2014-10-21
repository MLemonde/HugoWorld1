using HugoLand.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HugoLand.Controller;
using HugoLand.Controleur;

namespace HugoLand
{
    class Program
    {
        /// <summary>
        /// Plan de test des fonctions
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            RpgGameEntities context = new RpgGameEntities();
            MondeController mondeControleur = new MondeController(context);
            CompteJoueurController compteJoueurController = new CompteJoueurController(context);
            ClasseController classeController = new ClasseController(context);
            ObjetMondeController objetMondeController = new ObjetMondeController(context);
            MonstreController monstreController = new MonstreController(context);
            ItemController itemController = new ItemController(context);
            EffetItemController effetItemController = new EffetItemController(context);
            HeroController heroController = new HeroController(context);
            InventaireHeroController InventaireController = new InventaireHeroController(context);

           



            #region MONDE            
            mondeControleur.CreateMonde("100", "100", "mondetest");
            List<Monde> _lstmondes = mondeControleur.GetListMonde();


            int mondeId = _lstmondes.First().Id;
            Console.WriteLine("\nvoici la liste des mondes");
            foreach (Monde monde in _lstmondes)
            {
                Console.WriteLine(monde.Description);
            }

            Console.WriteLine("\nmodification du premier monde");
            mondeControleur.EditMonde(_lstmondes.First().Id, "Description","98", "99");
            Console.WriteLine(context.Mondes.First().Description);

            Console.WriteLine("\nsupression du premier monde");
            mondeControleur.DeleteMonde(_lstmondes.First().Id);

            Console.WriteLine("\nrevoici la listes de tous les mondes");
            _lstmondes = mondeControleur.GetListMonde();
            foreach (Monde monde in _lstmondes)
            {
                Console.WriteLine(monde.Description);
            }

            Console.WriteLine("\n\n");
            
            #endregion


            #region COMPTE
            if (!compteJoueurController.CreatePlayer("Joueur01", "PASSWORD", "email@email.com", "Mathew", "Lemonde", 0))
                Console.WriteLine("Deja existant");
            if (!compteJoueurController.CreatePlayer("Joueur02", "PASSWORD", "email2@email.com", "Mathew2", "Lemonde2", 0))
                Console.WriteLine("Deja existant");
            compteJoueurController.EditPlayer("Joueur01", "newEmail@hotmail.com", "Francis", "Lussier", 1);
            Console.WriteLine(context.CompteJoueurs.First().Courriel);
            compteJoueurController.DeletePlayer("Joueur01");
            Console.WriteLine(context.CompteJoueurs.First().Courriel);
            #endregion

            #region CLASSE
            classeController.CreateClass("Paladin", "Guerriers nobles?", 10, 10, 10, 10, context.Mondes.First().Id);
            classeController.CreateClass("Noob", "Guerrier noob?", 0, 0, 0, 0, context.Mondes.First().Id);
            List<Classe> lstClass = classeController.GetListClasses(context.Mondes.First().Id);



            classeController.EditClassFromWorld(lstClass.First().Id, "newClassName", "newClassDescription", 20, 20, 20, 20, context.Mondes.First().Id);
            Console.WriteLine(context.Classes.First().Description);
            classeController.DeleteClass(lstClass.First().Id);
            Console.WriteLine(context.Classes.First().Description);

            #endregion
            lstClass = classeController.GetListClasses(context.Mondes.First().Id);

            int classID = lstClass.First().Id;
            int compteId = context.CompteJoueurs.First().Id;


            //TODO
            //#region
            //heroController.CreateHero(_lstmondes.First().Id,compteId, classID, 0, 0, 20, 20, 20, 20, 20, 200, 300);
            //heroController.CreateHero(_lstmondes.First().Id,compteId, classID, 0, 0, 30, 30, 30, 30, 30, 400, 300);

            //Classe test = classeController.FindClasseOfHero(0, context.Mondes.First().Id);
            //Console.WriteLine(test.NomClasse);
            //#endregion

           


            #region OBJETMONDE
            Console.WriteLine("\najout d'un nouveau object dans le premier monde");
            objetMondeController.CreateObjectMonde(0, 0, "Object01", 0, context.Mondes.First().Id);
            objetMondeController.CreateObjectMonde(0, 0, "Object02", 0, context.Mondes.First().Id);

            Console.WriteLine(context.Mondes.First().ObjetMondes.First().Description);
            Console.WriteLine("\nModification d'un objectMonde...");
            objetMondeController.EditObjectMondeDescription(context.Mondes.First().ObjetMondes.First().Id, "ObjDescriptionModifiee");
            Console.WriteLine(context.Mondes.First().ObjetMondes.First().Description);

            Console.WriteLine("\nsupression de cet object dans le premier monde");
            objetMondeController.DeleteObjectMonde(_lstmondes.First().ObjetMondes.First().Id, context.Mondes.First().Id);
            Console.WriteLine(context.Mondes.First().ObjetMondes.First().Description);

            #endregion

            #region MONSTRE
            monstreController.CreateMonster(context.Mondes.First().Id);
            monstreController.CreateMonster(context.Mondes.First().Id);

            Console.WriteLine(context.Mondes.First().Monstres.First().Niveau);
            monstreController.EditMonster(_lstmondes.First().Monstres.First().Id, "Patate", 10, 10, 10, 10, 10, 10);
            Console.WriteLine(context.Mondes.First().Monstres.First().Niveau);

            monstreController.DeleteMonster(_lstmondes.First().Monstres.First().Id);
            Console.WriteLine(context.Mondes.First().Monstres.First().Niveau);

            #endregion




            

            #region
            Console.WriteLine("Création d'items...");
            itemController.CreateItem(context.Mondes.First().Id, 0, 0, "item01", "itemDesc", 1, 1, 1, 1, 1, 1, 1, 1);
            itemController.CreateItem(context.Mondes.First().Id, 0, 0, "item02", "itemDesc", 100, 100, 100, 100, 100, 100, 100, 200);

            int itemId = context.Items.First().Id;

            //LOOKUP
            foreach (Item item in context.Items)
                Console.WriteLine(item.Quantite.ToString());

            Console.WriteLine("Modification de l'item nommé \"item01\"");
            itemController.EditItem(itemId, 2);

            //LOOKUP
            foreach (Item item in context.Items)
                Console.WriteLine(item.Quantite.ToString());

            Console.WriteLine("Modification de l'item nommé \"item02\"");

            itemController.DeleteItem(itemId);

            //LOOKUP
            foreach (Item item in context.Items)
            {
                Console.WriteLine(item.Quantite);
            }
            Console.WriteLine("\n\n");
            #endregion



            #region
            effetItemController.CreateEffetItem(context.Items.First().Id, 0, 0);
            effetItemController.CreateEffetItem(context.Items.First().Id, 0, 12);
            effetItemController.CreateEffetItem(context.Items.First().Id, 0, 2);
            Console.WriteLine(context.Items.First().EffetItems.First().ValeurEffet);

            int effetItemId = context.EffetItems.First().Id;

            effetItemController.EditEffetItem(effetItemId, 1, 1);
            Console.WriteLine(context.Items.First().EffetItems.First().ValeurEffet);
            effetItemController.DeleteEffetItem(effetItemId);
            Console.WriteLine(context.Items.First().EffetItems.First().ValeurEffet);

            #endregion

            Console.ReadKey();
        }
    }
}
