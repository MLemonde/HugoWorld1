﻿using HugoLand.Model;
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
            Console.WriteLine("Création de mondes...");
            //mondeControleur.CreateMonde("100", "100", "monde1");
            //mondeControleur.CreateMonde("100", "100", "monde2");
            //mondeControleur.CreateMonde("100", "100", "monde3");
            //mondeControleur.CreateMonde("100", "100", "monde4");
            //mondeControleur.CreateMonde("100", "100", "monde5");
            //mondeControleur.CreateMonde("100", "100", "monde6");
            
            List<Monde> _lstmondes = mondeControleur.GetListMonde();

            Console.WriteLine("\nvoici la liste des mondes");
            foreach (Monde monde in _lstmondes)
            {
                Console.WriteLine(monde.Description);
            }

            Console.WriteLine("\nmodification du premier monde");
            mondeControleur.EditMonde(_lstmondes.First().Id, "Description","98", "99");
            Console.WriteLine(_lstmondes.First().Description);

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

            int mondeId = _lstmondes.First().Id;

            #region
            if (!compteJoueurController.CreatePlayer("Joueur01", "PASSWORD", "email@email.com", "Mathew", "Lemonde", 0))
                Console.WriteLine();
            if (!compteJoueurController.CreatePlayer("Joueur02", "PASSWORD", "email2@email.com", "Mathew2", "Lemonde2", 0))
                Console.WriteLine();
            compteJoueurController.EditPlayer("Joueur01", "newEmail@hotmail.com", "Francis", "Lussier", 1);
            compteJoueurController.DeletePlayer("Joueur01");
            #endregion

            #region CLASSE
            classeController.CreateClass("Paladin", "Guerriers nobles?", 10, 10, 10, 10, mondeId);
            classeController.CreateClass("Noob", "Guerrier noob?", 0, 0, 0, 0, mondeId);
            List<Classe> lstClass = classeController.GetListClasses(mondeId);

           

            classeController.EditClassFromWorld(lstClass.First().Id, "newClassName", "newClassDescription", 20, 20, 20, 20, mondeId);
            lstClass = classeController.GetListClasses(mondeId);

            classeController.DeleteClass(lstClass.First().Id);
            #endregion
            lstClass = classeController.GetListClasses(mondeId);

            int classID = lstClass.First().Id;
            int compteId = context.CompteJoueurs.First().Id;

            //#region
            //heroController.CreateHero(_lstmondes.First().Id,compteId, classID, 0, 0, 20, 20, 20, 20, 20, 200, 300);
            //heroController.CreateHero(_lstmondes.First().Id,compteId, classID, 0, 0, 30, 30, 30, 30, 30, 400, 300);
            
            //Classe test = classeController.FindClasseOfHero(0, mondeId);
            //Console.WriteLine(test.NomClasse);
            //#endregion

           


            #region
            Console.WriteLine("\najout d'un nouveau object dans le premier monde");
            objetMondeController.CreateObjectMonde(0, 0, "Object01", 0, mondeId);

            Console.WriteLine("\nModification d'un objectMonde...");
            objetMondeController.EditObjectMondeDescription(_lstmondes.First().ObjetMondes.First().Id, "ObjDescriptionModifiee");

            Console.WriteLine("\nsupression de cet object dans le premier monde");
            objetMondeController.DeleteObjectMonde(_lstmondes.First().ObjetMondes.First().Id, mondeId);
            #endregion

            #region
            monstreController.CreateMonster(mondeId);
            monstreController.EditMonster(_lstmondes.First().Monstres.First().Id, "Patate", 10, 10, 10, 10, 10, 10);
            monstreController.DeleteMonster(_lstmondes.First().Monstres.First().Id);
            #endregion




            

            #region
            Console.WriteLine("Création d'items...");
            itemController.CreateItem(mondeId, 0, 0, "item01", "itemDesc", 1, 1, 1, 1, 1, 1, 1, 1);
            itemController.CreateItem(mondeId, 0, 0, "item02", "itemDesc", 100, 100, 100, 100, 100, 100, 100, 200);

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
            effetItemController.CreateEffetItem(itemId, 0, 0);
            effetItemController.CreateEffetItem(itemId, 0, 1);
            effetItemController.CreateEffetItem(itemId, 0, 2);

            int effetItemId = context.EffetItems.First().Id;

            effetItemController.EditEffetItem(effetItemId, 1, 1);
            effetItemController.DeleteEffetItem(effetItemId);
            #endregion

            Console.ReadKey();
        }
    }
}
