using HugoLand.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HugoLand.Controller;

namespace HugoLand
{
    class Program
    {
        static void Main(string[] args)
        {
            MondeController mondeControleur = new MondeController();

            Console.WriteLine("Création d'un monde...");
            mondeControleur.CreateMonde("100", "100", "monde1");
            mondeControleur.CreateMonde("100", "100", "monde2");
            mondeControleur.CreateMonde("100", "100", "monde3");
            mondeControleur.CreateMonde("100", "100", "monde4");
            mondeControleur.CreateMonde("100", "100", "monde5");
            mondeControleur.CreateMonde("100", "100", "monde6");
            
            List<Monde> _lstmondes = mondeControleur.GetListMonde();

            Console.WriteLine("voici la liste des mondes");
            foreach (Monde monde in _lstmondes)
            {
                Console.WriteLine(monde.Description);
            }

            Console.WriteLine("\nmodification du premier monde");
            mondeControleur.EditMonde(_lstmondes.First().Id, "99","98", "Description");
            Console.WriteLine(_lstmondes.First().Description);

            Console.WriteLine("\nsupression du premier monde");
            mondeControleur.DeleteMonde(_lstmondes.First().Id);

            Console.WriteLine("\nrevoici la listes de tous les mondes");
            foreach (Monde monde in _lstmondes)
            {
                Console.WriteLine(monde.Description);
            }

            Console.WriteLine("\n\n");


            
            ObjetMondeController objetMondeController = new ObjetMondeController();
            Console.WriteLine("\najout d'un nouveau object dans le premier monde");
            objetMondeController.CreateObjectMonde(0, 0, "Object01", 0, _lstmondes.First().Id);

            Console.WriteLine("\nModification d'un objectMonde...");
            objetMondeController.EditObjectMondeDescription(_lstmondes.First().ObjetMondes.First().Id, "ObjDescriptionModifiee");

            Console.WriteLine("\nsupression de cet object dans le premier monde");
            objetMondeController.DeleteObjectMonde(_lstmondes.First().ObjetMondes.First().Id);


            MonstreController monstreController = new MonstreController();
            monstreController.CreateMonster(_lstmondes.First());
            monstreController.EditMonster(_lstmondes.First().Monstres.First().Id, "PAtate", 10, 10, 10, 10, 10, 10);
            monstreController.DeleteMonster(_lstmondes.First().Monstres.First(), _lstmondes.First());


            CompteJoueurController compteJoueurController = new CompteJoueurController();
            compteJoueurController.CreatePlayer("Joueur01", "PASSWORD", "email@email.com", "Mathew", "Lemonde", 0);
            //compteJoueurController.EditPlayer("Joueur01", "newEmail@hotmail.com", "Francis", "Lussier", 1);
            //compteJoueurController.DeletePlayer("Joueur01");


            ItemController itemController = new ItemController();
            itemController.CreateItem(0, 0, "item01", "itemDesc", 1, 1, 1, 1, 1, 1, 1, 1, 1);
            //itemController.EditItem("item01", 0, 0, "itemDescription", 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2);


            

            Console.ReadKey();
        }
    }
}
