using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.ServiceModel;

namespace Tp3Service
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class ServiceHugoWorld : IClasseController, IMondeController, ICompteJoueurController, IHeroController, IMonstreController, IEffetItemController, IInventaireHeroController, IItemController, IObjectMondeController
    {
        private HugoWorldEntities context = new HugoWorldEntities();
        private System.Timers.Timer timer;
        
        public ServiceHugoWorld()
        {
            try
            {
                context.Database.Connection.Open();
                if (context.Database.Connection.State == ConnectionState.Open)
                {
                    Console.WriteLine(@"INFO: ConnectionString: " + context.Database.Connection.ConnectionString
                        + "\n DataBase: " + context.Database.Connection.Database
                        + "\n DataSource: " + context.Database.Connection.DataSource
                        + "\n ServerVersion: " + context.Database.Connection.ServerVersion
                        + "\n TimeOut: " + context.Database.Connection.ConnectionTimeout);
                    context.Database.Connection.Close();
                }
                else
                    throw new FaultException("Connection error...");

                //lock (context.Heroes)
                //{
                //    foreach (Hero hero in context.Heroes)
                //        hero.Connected = false;
                //    context.SaveChanges();
                //}

                timer = new System.Timers.Timer(15000);
                timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);

                timer.Start();
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        private void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
              
                using (HugoWorldEntities db = new HugoWorldEntities())
                {

                    List<Hero> heroes = db.Heroes.Where(h => h.Connected).ToList();
                
                    foreach (Hero hero in heroes)
                    {
                        if (hero.LastActivity.Value + new TimeSpan(00, 05, 00) < DateTime.Now)
                            hero.Connected = false;
                    }
                    
                        db.SaveChanges();
                }
                    
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        #region Classe
        /// <summary>
        /// Auteur : Marc-André Landry
        /// </summary>
        /// <param name="sNomClasse"></param>
        /// <param name="sDescription"></param>
        /// <param name="fStatPoidsStr">Facteur bonus/malus Strength</param>
        /// <param name="fStatPoidsDex">Facteur bonus/malus Dex</param>
        /// <param name="fStatPoidsInt">Facteur bonus/malus Intel</param>
        /// <param name="fStatPoidsStam">Facteur bonus/malus Stamina</param>
        /// <param name="iMondeId">Monde dans lequel la classe est créer</param>
        void IClasseController.CreateClass(string sNomClasse, string sDescription, float fStatPoidsStr, float fStatPoidsDex, float fStatPoidsInt, float fStatPoidsStam, int iMondeId)
        {
            var Monde = context.Mondes.FirstOrNull(c => c.Id == iMondeId);
            if (Monde == null)
                return;
            
            Classe myClass = new Classe()
            {
                NomClasse = sNomClasse,
                Description = sDescription,
                StatPoidsStr = fStatPoidsStr,
                StatPoidsDex = fStatPoidsDex,
                StatPoidsInt = fStatPoidsInt,
                StatPoidsStam = fStatPoidsStam,
                MondeId = iMondeId
            };
            context.Classes.Add(myClass);
            Monde.Classes.Add(myClass);
            context.SaveChanges();
        }

        /// <summary>
        /// Auteur: Marc-André Landry
        /// permet de supprimer une classe.
        /// </summary>
        /// <param name="iClassID"></param>
        void IClasseController.DeleteClass(int iClassID)
        {
            Classe myClasse = context.Classes.FirstOrNull(c => c.Id == iClassID);
            if (myClasse == null)
                return;

            myClasse.Monde.Classes.Remove(myClasse);
            context.Classes.Remove(myClasse);
            context.SaveChanges();
        }

        /// <summary>
        /// Auteur : Marc-André Landry
        /// </summary>
        /// <param name="iClasseID">Id de la classe</param>
        /// <param name="sNomClasse">Nom de la classe</param>
        /// <param name="sDescription">Description de la classe</param>
        /// <param name="fStatPoidsStr">Facteur bonus/malus Strength</param>
        /// <param name="fStatPoidsDex">Facteur bonus/malus Dex</param>
        /// <param name="fStatPoidsInt">Facteur bonus/malus Intel</param>
        /// <param name="fStatPoidsStam">Facteur bonus/malus Stamina</param>
        /// <param name="iMondeID">Id du monde</param>
        void IClasseController.EditClassFromWorld(int iClasseID, string sNomClasse, string sDescription, float fStatPoidsStr, float fStatPoidsDex, float fStatPoidsInt, float fStatPoidsStam, int iMondeID)
        {
            Classe myClasse = context.Classes.FirstOrNull(c => c.Id == iClasseID);
            if (myClasse == null)
                return;
            if (myClasse.MondeId != iMondeID)
                return;
            myClasse.NomClasse = sNomClasse;
            myClasse.Description = sDescription;
            myClasse.StatPoidsStr = fStatPoidsStr;
            myClasse.StatPoidsDex = fStatPoidsDex;
            myClasse.StatPoidsInt = fStatPoidsInt;
            myClasse.StatPoidsStam = fStatPoidsStam;
                    
            context.SaveChanges();
        }

        /// <summary>
        /// Auteur: Mathew Lemonde
        /// Retourne liste de classe
        /// </summary>
        /// <param name="mondeID"></param>
        /// <returns></returns>
        List<Classe> IClasseController.GetListClasses(int mondeID)
        {
            lock (context.Mondes)
            {
                var Monde = context.Mondes.FirstOrNull(c => c.Id == mondeID);
                if (Monde == null)
                    return null;

                return Monde.Classes.ToList();       
            }     
        }

        /// <summary>
        /// Auteur : MA
        /// Retourne la description de la classe sélectionnée
        /// </summary>
        /// <param name="sClassName">la classe sélectionnée</param>
        /// <returns>la description de la classe sélectionnée</returns>
        string IClasseController.GetClassDescription(string sClassName)
        {
            var Classe = context.Classes.FirstOrNull(c => c.NomClasse == sClassName);

            return Classe.Description;
        }

        /// <summary>
        /// Auteur: MAL
        /// Description: Recevoir le ID de la classe qui vient d'être sélectionnée
        /// </summary>
        /// <param name="sUsername">Nom de la classe</param>
        /// <returns></returns>
        int? IClasseController.GetClassID(string sClassName)
        {
            var Classe = context.Classes.FirstOrNull(c => c.NomClasse == sClassName);
            if (Classe != null)
                return Classe.Id;

            else
                return null;
        }

        /// <summary>
        /// Auteur : Marc-André Landry
        /// Trouver la classe d'un héro
        /// </summary>
        /// <param name="iHeroID">Id du héro</param>
        /// <param name="iMondeID">Id du Monde</param>
        /// <returns></returns>
        Classe IClasseController.FindClasseOfHero(int iHeroID, int iMondeID)
        {
            return context.Heroes.Include("Classe").FirstOrNull(h => h.Id == iHeroID && h.MondeId == iMondeID).Classe;
        }
        #endregion

        #region Monde
        /// <summary>
        /// Auteur: Marc-André Landry
        /// Create a new world
        /// </summary>
        /// <param name="iLimiteX">Limit of the world (x)</param>
        /// <param name="iLimiteY">Limit of the world (y)</param>
        /// <param name="sDescription">A small description of your new world!</param>
        void IMondeController.CreateMonde(string iLimiteX, string iLimiteY, string sDescription)
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

        /// <summary>
        /// Auteur: Marc-André Landry
        /// Edit the world you want.
        /// </summary>
        /// <param name="iID">The ID of the world you wanna change</param>
        /// <param name="sDescription">The new description of the world you wanna change</param>
        /// <param name="iLimiteX">The new limit of the world (x)</param>
        /// <param name="iLimiteY">The new limit of the world (y)</param>
        void IMondeController.EditMonde1(int iID, string sDescription, string iLimiteX, string iLimiteY)
        {
            var monde = context.Mondes.FirstOrNull(c => c.Id == iID);
            if (monde == null && iLimiteX.Length > 10 && iLimiteY.Length > 10)
                return;

            monde.Description = sDescription;
            monde.LimiteX = iLimiteX;
            monde.LimiteY = iLimiteY;

            context.SaveChanges();
        }

        /// <summary>
        /// Auteur: Marc-André Landry        /// 
        /// This method is used if you wanna change only the description of the world
        /// </summary>
        /// <param name="iID">The ID of the world you wanna change</param>
        /// <param name="sDescription">The new description of the world</param>
        void IMondeController.EditMonde2(int iID, string sDescription)
        {
            var monde = context.Mondes.FirstOrNull(c => c.Id == iID);
            if (monde == null)
                return;

            monde.Description = sDescription;

            context.SaveChanges();
        }

        /// <summary>
        /// Auteur: Marc-André Landry        /// 
        /// This method is used if you want to change only the positions of the world
        /// </summary>
        /// <param name="iID">The ID of the world you wanna change</param>
        /// <param name="iLimiteX">The new limit of the world (x)</param>
        /// <param name="iLimiteY">The new limit of the world (y)</param>
        void IMondeController.EditMonde3(int iID, string iLimiteX, string iLimiteY)
        {
            var monde = context.Mondes.FirstOrNull(c => c.Id == iID);
            if (monde == null)
                return;


            monde.LimiteX = iLimiteX;
            monde.LimiteY = iLimiteY;

            context.SaveChanges();
        }

        /// <summary>
        /// Auteur: Marc-André Landry
        /// 
        /// Use this methode if you ever wanna delete the world
        /// </summary>
        /// <param name="iID">The ID of the world you wanna destroy</param>
        void IMondeController.DeleteMonde(int iID)
        {
            var monde = context.Mondes.FirstOrNull(c => c.Id == iID);
            if (monde == null)
                return;

            List<Classe> lstClass = monde.Classes.ToList();

            foreach (var item in lstClass)
            {
                List<Hero> lsthero = item.Heroes.ToList();
                foreach (var hero in lsthero)
                    context.Heroes.Remove(hero);
                context.Classes.Remove(item);
            }

            List<Item> lstItem = monde.Items.ToList();
            foreach (var item in lstItem)
            {
                List<EffetItem> lsteffet = item.EffetItems.ToList();
                foreach (var effet in lsteffet)
                    context.EffetItems.Remove(effet);

                context.Items.Remove(item);
            }

            List<Monstre> lstmonstre = monde.Monstres.ToList();
            foreach (var item in lstmonstre)
                context.Monstres.Remove(item);

            List<ObjetMonde> lstob = monde.ObjetMondes.ToList();
            foreach (var item in lstob)
                context.ObjetMondes.Remove(item);
            context.Mondes.Remove(monde);
            context.SaveChanges();
        }

        /// <summary>
        /// Auteur : MAL
        /// Description : retourne le ID du monde sélectionnée
        /// </summary>
        /// <param name="sDescription"></param>
        /// <returns></returns>
        int? IMondeController.GetWorldID(string sDescription)
        {
            var Monde = context.Mondes.FirstOrNull(c => c.Description == sDescription);
            if (Monde != null)
                return Monde.Id;

            else
                return null;
        }

        List<Monde> IMondeController.GetListMonde()
        {
            //context.Dispose();
            //context = new HugoWorldEntities();
                return context.Mondes
                    .Include("Classes")
                    .Include("Heroes")
                    .Include("Items")
                    .Include("Monstres")
                    .Include("ObjetMondes")
                    .ToList(); 
            
        }
        #endregion

        #region CompteJoueur
        /// <summary>
        /// Auteur: Mathew Lemonde
        /// Description : Creer un nouveau compte
        /// </summary>
        /// <param name="sUsername">Nom d'Utilisateur</param>
        /// <param name="sPass">Mot de passe</param>
        /// <param name="sEmail">Adresse e-mail</param>
        /// <param name="sFname">Prénom</param>
        /// <param name="sLname">Nom de Famille</param>
        /// <param name="iType"> Type de compte (int)  0=Util, 1=Admin</param>
        /// <returns>false si deja pris</returns>
        bool ICompteJoueurController.CreatePlayer(string sUsername, string sPass, string sEmail, string sFname, string sLname, int iType)
        {
            var account = context.CompteJoueurs.FirstOrNull(c => c.NomUtilisateur == sUsername);
            if (account != null)
                return false;

            string sPassword = PasswordHash.CreateHash(sPass);

            CompteJoueur Account = new CompteJoueur()
            {
                Courriel = sEmail,
                NomUtilisateur = sUsername,
                TypeUtilisateur = iType,
                Nom = sLname,
                Prenom = sFname,
                Password = sPassword
            };
            context.CompteJoueurs.Add(Account);
            context.SaveChanges();
            return true;
        }

        /// <summary>
        /// Auteur:Mathew Lemonde
        /// Description : Supprimer un compte
        /// </summary>
        /// <param name="sUsername">Nom d'utilisateur</param>
        void ICompteJoueurController.DeletePlayer(string sUsername)
        {
            var Account = context.CompteJoueurs.FirstOrNull(c => c.NomUtilisateur == sUsername);
            if (Account == null)
                return;

            context.CompteJoueurs.Remove(Account);
            context.SaveChanges();

        }

        /// <summary>
        /// Auteur: Mathew Lemonde
        /// Description: Modifier un compte a partir du Username
        /// </summary>
        /// <param name="sUsername">Nom d'utilisateur</param>
        /// <param name="sEmail">Adresse email</param>
        /// <param name="sFname">Prénom</param>
        /// <param name="sLname">Nom de Famille</param>
        /// <param name="iType">Type de compte (int) 0=Util, 1=Admin</param>
        void ICompteJoueurController.EditPlayer1(string sUsername, string sEmail, string sFname, string sLname, int iType)
        {
            var Account = context.CompteJoueurs.FirstOrNull(c => c.NomUtilisateur == sUsername);
            if (Account == null)
                return;

            Account.Courriel = sEmail;
            Account.Prenom = sFname;
            Account.Nom = sLname;
            Account.TypeUtilisateur = iType;

            context.SaveChanges();
        }

        /// <summary>
        /// Auteur: Mathew Lemonde
        /// Description: Modifier un compte a partir du ID
        /// </summary>
        /// <param name="id">id du Compte</param>
        /// <param name="sEmail">Adresse email</param>
        /// <param name="sFname">Prénom</param>
        /// <param name="sLname">Nom de Famille</param>
        /// <param name="iType">Type de compte (int) 0=Util, 1=Admin</param>
        void ICompteJoueurController.EditPlayer2(int id, string sEmail, string sFname, string sLname, int iType)
        {
            var Account = context.CompteJoueurs.FirstOrNull(c => c.Id == id);
            if (Account == null)
                return;

            Account.Courriel = sEmail;
            Account.Prenom = sFname;
            Account.Nom = sLname;
            Account.TypeUtilisateur = iType;

            context.SaveChanges();
        }

        /// <summary>
        /// Auteur: Mathew Lemonde
        /// Description: Valider le login
        /// </summary>
        /// <param name="sUsername">Nom d'utilisateur</param>
        /// <param name="sPassword">Mot de passe</param>
        /// <returns></returns>
        bool ICompteJoueurController.ValidatePlayer(string sUsername, string sPassword)
        {
            var Account = context.CompteJoueurs.FirstOrNull(c => c.NomUtilisateur == sUsername);
            if (Account == null)
                return false;

            if (PasswordHash.ValidatePassword(sPassword, Account.Password))
                return true;

            return false;
        }

        /// <summary>
        /// Auteur : MAL
        /// Description : get ID of the User
        /// </summary>
        /// <param name="sUsername"></param>
        /// <param name="sPassword"></param>
        /// <returns></returns>
        int? ICompteJoueurController.GetUserID(string sUsername)
        {
            var Account = context.CompteJoueurs.FirstOrNull(c => c.NomUtilisateur == sUsername);
            if (Account != null)
                return Account.Id;

            else
                return null;
        }

        /// <summary>
        /// Auteur: Mathew Lemonde
        /// Description: Valider le login d'un admin
        /// </summary>
        /// <param name="sUsername">Nom d'utilisateur</param>
        /// <param name="sPassword">Mot de passe</param>
        /// <returns></returns>
        bool ICompteJoueurController.ValidateAdmin(string sUsername, string sPassword)
        {
            var Account = context.CompteJoueurs.FirstOrNull(c => c.NomUtilisateur == sUsername);
            if (Account == null)
                return false;

            if (Account.TypeUtilisateur != 1)
                return false;

            if (PasswordHash.ValidatePassword(sPassword, Account.Password))
                return true;

            return false;
        }

         bool ICompteJoueurController.ValidateAdmin2(int userId)
        {
            CompteJoueur Account = context.CompteJoueurs.FirstOrNull(c => c.Id == userId);

                return Account != null && Account.TypeUtilisateur == 1;
        }
        #endregion

        #region Hero
        /// <summary>
        /// Auteur Francis Lussier
        /// permet de crée un héro
        /// </summary>
        void IHeroController.CreateHero(int MondeID, int compteId, int classeId, int X, int Y, int niveau, int dex, int str, int stamina, int Int, long experience, decimal argent, string name)
        {
            var Monde = context.Mondes.FirstOrNull(c => c.Id == MondeID);
            if (Monde == null)
                return;

            Classe classe = context.Classes.FirstOrNull(c => c.Id == classeId);
            CompteJoueur compte = context.CompteJoueurs.FirstOrNull(c => c.Id == compteId);
            if (classe == null || compte == null)
                return;

            while (true)
            {
                try
                {


                    using (HugoWorldEntities db = new HugoWorldEntities())
                    {
                        Hero hero = new Hero()
                        {
                            //Id = id,
                            MondeId = MondeID,
                            Argent = argent,
                            ClasseId = classeId,
                            CompteJoueurId = compteId,
                            Experience = experience,
                            Niveau = niveau,
                            StatBaseDex = dex,
                            StatBaseInt = Int,
                            StatBaseStam = stamina,
                            StatBaseStr = str,
                            Connected = false,
                            Name = name,
                            x = X,
                            y = Y,
                            LastActivity = DateTime.Now,
                            PV = stamina*10
                        };

                        db.Heroes.Add(hero);
                        db.SaveChanges();
                        break;
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        /// <summary>
        /// Auteur Francis Lussier
        /// permet de modifier un héro
        /// </summary>
        void IHeroController.EditHero(int HeroId,int niveau, int dex, int str, int stamina, int Int, long experience, decimal argent,int pv)
        {
            Hero hero = context.Heroes.FirstOrNull(h => h.Id == HeroId);
            
            if (hero != null)
            {
                try
                {


                    hero.Niveau = niveau;
                    hero.StatBaseDex = dex;
                    hero.StatBaseStr = str;
                    hero.StatBaseStam = stamina;
                    hero.StatBaseInt = Int;
                    hero.Experience = experience;
                    hero.Argent = argent;
                    hero.LastActivity = DateTime.Now;
                    hero.Connected = true;
                    hero.PV = pv;
                    context.SaveChanges();
                }
                catch(Exception ex)
                {
                    var objContext = ((IObjectContextAdapter)context).ObjectContext;
                    objContext.Refresh(RefreshMode.StoreWins, hero);
                }
            }
        }

        /// <summary>
        /// Auteur Francis Lussier
        /// permet de supprimer un héro
        /// </summary>
        void IHeroController.DeleteHero(int HeroId)
        {
            Hero hero = context.Heroes.FirstOrNull(h => h.Id == HeroId);
            if (hero != null)
            {
                hero.Monde.Heroes.Remove(hero);
                hero.CompteJoueur.Heroes.Remove(hero);
                hero.Classe.Heroes.Remove(hero);
                context.Heroes.Remove(hero);

                context.SaveChanges();
            }
        }

        /// <summary>
        /// Auteur Francis Lussier
        /// permet d'Aller chercher la liste des héros du compte en parametre.
        /// </summary>
        List<Hero> IHeroController.GetListHero(int compteId)
        {
            lock (context.Heroes)
            {
                CompteJoueur compte = context.CompteJoueurs.Include("Heroes").FirstOrNull(c => c.Id == compteId);
                if (compte != null)
                    return compte.Heroes.ToList();
                else
                    return new List<Hero>();
            }
            
        }

        List<Hero> IHeroController.GetListHeroNearHero(int heroId)
        {
            lock (context.Heroes)
            {
                Hero hero = context.Heroes.Include("Classe").FirstOrNull(h => h.Id == heroId);
                if (hero == null)
                    return new List<Hero>();

                List<Hero> lstheo = new List<Hero>();
                //List<Hero> lstheo = context.Heroes.Where(
                //    h => h.MondeId == hero.MondeId &&
                //    (int)(h.x % 8) == (int)(hero.x % 8) &&
                //    (int)(h.y % 8) == (int)(hero.y % 8) 
                //    && h.Connected == true).ToList();

                foreach(var her in context.Heroes)
                {
                    int x1 = (her.x) / 8;
                    int x2 = (hero.x) / 8;
                    int y1 = (her.y) / 8;
                    int y2 =  (hero.y) / 8;
                    
                    if (x1==x2)
                        if (y1 == y2)
                            if (her.Connected == true)
                                if(her.MondeId == hero.MondeId)
                                lstheo.Add(her);
                }

                return lstheo;
            }
        }

        void IHeroController.ConnectHero(int heroId)
        {
            using (HugoWorldEntities db = new HugoWorldEntities())
            {
                CompteJoueur compte = db.CompteJoueurs.Include("Heroes").FirstOrNull(c => c.Heroes.Any(h => h.Id == heroId));
                if (compte == null)
                    throw new FaultException("Error: the account wasn't found in the database");

                Hero hero = db.Heroes.FirstOrNull(h => h.Id == heroId);
                if (hero == null)
                    throw new FaultException("Error: the hero wasn't found in the database");
                else if (compte.Heroes.Any(h => h.Connected))
                    throw new FaultException("Error: there's already a connected hero on that account");
                else
                {
                    hero.Connected = true;
                    hero.LastActivity = DateTime.Now;
                    db.SaveChanges();
                }
            }
            
        }

        void IHeroController.DeconnectHero(int heroId)
        {
            lock (context.Heroes)
            {
                try
                {
                    Hero hero = context.Heroes.FirstOrNull(h => h.Id == heroId);
                    if (hero == null)
                        throw new FaultException("Error: the hero wasn't found in the database");
                    else
                    {
                        hero.Connected = false;
                        context.SaveChanges();
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        void IHeroController.SetHeroPos(int HeroId, int x, int y,string area)
        {
            lock (context.Heroes)
            {
                Hero hero = context.Heroes.FirstOrNull(h => h.Id == HeroId);

                if (hero != null)
                {
                    string[] lststr = area.Split(',');

                    hero.x = int.Parse(lststr[0]) * 8 + x;
                    hero.y = int.Parse(lststr[1]) * 8 + y;

                    context.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Auteur Francis
        /// retourne une liste de objects (ObjetMonde, Monstre, Item, Héro) qui se trouve dans le rayon de 200 par 200 du héro.
        /// </summary>
        /// <param name="HeroId"></param>
        Elements IHeroController.GetElementsArroundHero(int HeroId)
        {
            var elements = new Elements(); 
            Hero hero = context.Heroes.FirstOrNull(h => h.Id == HeroId);
            if (hero != null)
            {
                elements.lstmonstre.AddRange(context.Monstres.Where(m => m.x >= hero.x - 100 && m.x <= hero.x + 100 && m.y >= hero.y - 100 && m.y <= hero.y + 100));
                elements.lstobj.AddRange(context.ObjetMondes.Where(m => m.x >= hero.x - 100 && m.x <= hero.x + 100 && m.y >= hero.y - 100 && m.y <= hero.y + 100));
                elements.lstitem.AddRange(context.Items.Where(m => m.x >= hero.x - 100 && m.x <= hero.x + 100 && m.y >= hero.y - 100 && m.y <= hero.y + 100));
                elements.lstHero.AddRange(context.Heroes.Where(m => m.x >= hero.x - 100 && m.x <= hero.x + 100 && m.y >= hero.y - 100 && m.y <= hero.y + 100));
            }
            return elements;
        }
        #endregion

        #region Monstre
        /// <summary>
        /// Auteur: Mathew Lemonde
        /// 
        /// Creation d'un Monstre avec valeur aléatoires
        /// </summary>
        /// <param name="Mondeid">Id du monde</param>
        void IMonstreController.CreateMonster(int Mondeid, int x, int y, int HP, string sname)
        {
            var Monde = context.Mondes.FirstOrNull(c => c.Id == Mondeid);
            if (Monde == null)
                return;
            Random _rand = new Random();
            int DmgMin = _rand.Next(0, 100);
            Monstre monster = new Monstre()
            {
                MondeId = Monde.Id,
                Nom = sname,
                //TODO
                Niveau = _rand.Next(0, 101),
                x = x,
                y = y,
                StatPV = HP,

                //DMG SELON LE MONSTRE TODO
                StatDmgMax = _rand.Next(DmgMin, 400)
            };
            context.Monstres.Add(monster);
            Monde.Monstres.Add(monster);
            context.SaveChanges();
        }

        /// <summary>
        /// Auteur: Mathew Lemonde
        /// 
        /// Suppression d'un monstre
        /// </summary>
        /// <param name="monsterid">id du Monstre a delete</param>
        /// <param name="Mapid">Id du monde</param>
        void IMonstreController.DeleteMonster(int monsterid)
        {
            var monster = context.Monstres.FirstOrNull(c => c.Id == monsterid);
            if (monster == null)
                return;



            monster.Monde.Monstres.Remove(monster);
            context.Monstres.Remove(monster);
            context.SaveChanges();
        }

        /// <summary>
        /// Auteur: Mathew Lemonde
        /// Modification d'un monstre
        /// </summary>
        /// <param name="MonsterID">Id du monstre</param>
        /// <param name="sNom">Nom de Monstre</param>
        /// <param name="iLvl">Level du monstre</param>
        /// <param name="ix">Position en x du monstre</param>
        /// <param name="iy">Position en y du monstre</param>
        /// <param name="iPv">Point de vie du monstre</param>
        /// <param name="iDmgMin">Damage minimum du monstre</param>
        /// <param name="iDmgMax">Damage maximum du monstre</param>
        void IMonstreController.EditMonster(int MonsterID, string sNom, int iLvl, int ix, int iy, int iPv, int iDmgMin, int iDmgMax)
        {
            var monster = context.Monstres.FirstOrNull(c => c.Id == MonsterID);
            if (monster == null)
                return;
            else
            {
                monster.Nom = sNom;
                monster.Niveau = iLvl;
                monster.x = ix;
                monster.y = iy;
                monster.StatPV = iPv;
                monster.StatDmgMin = iDmgMin;
                monster.StatDmgMax = iDmgMax;
            }
            context.SaveChanges();
        }
        #endregion

        #region EffetItem
        /// <summary>
        /// Auteur Francis
        /// </summary>
        void IEffetItemController.CreateEffetItem(int itemId, int typeEffet, int valeurEffet)
        {
            var item = context.Items.FirstOrNull(c => c.Id == itemId);
            if (item == null)
                return;

            EffetItem effet = new EffetItem()
            {
                ItemId = itemId,
                TypeEffet = typeEffet,
                ValeurEffet = valeurEffet,
            };
            context.EffetItems.Add(effet);
            item.EffetItems.Add(effet);
            context.SaveChanges();
        }

        /// <summary>
        /// Auteur Francis
        /// </summary>
        void IEffetItemController.DeleteEffetItem(int effetItemId)
        {
            EffetItem effetItem = context.EffetItems.FirstOrNull(i => i.Id == effetItemId);
            if (effetItem == null)
                return;
            effetItem.Item.EffetItems.Remove(effetItem);
            context.EffetItems.Remove(effetItem);
            context.SaveChanges();
        }

        /// <summary>
        /// Auteur Francis
        /// </summary>
        void IEffetItemController.EditEffetItem(int effetItemId, int typeEffet, int valeurEffet)
        {
            EffetItem effetItem = context.EffetItems.FirstOrNull(i => i.Id == effetItemId);
            if (effetItem == null)
                return;

            effetItem.TypeEffet = typeEffet;
            effetItem.ValeurEffet = valeurEffet;

            context.SaveChanges();
        }
        #endregion
        
        #region InventaireHero
        /// <summary>
        /// Auteur : Mathew Lemonde
        /// </summary>
        /// <param name="heroid"></param>
        /// <param name="itemid"></param>
        /// <returns>False si inventaire plein </returns>
        bool IInventaireHeroController.AddItemToHero(int heroid, int itemid)
        {

            var hero = context.Heroes.FirstOrNull(c => c.Id == heroid);
            if (hero == null)
                return false;

            var Item = context.Items.FirstOrNull(c => c.Id == itemid);
            if (Item == null)
                return false;

            decimal LimiteInventaire = (hero.StatBaseStr * (decimal)hero.Classe.StatPoidsStr) * 10;
            decimal TotalPoids = 0;
            decimal placeDispo = 0;
            foreach (var item in hero.Items)
            {
                TotalPoids += item.Poids;
            }
            placeDispo = LimiteInventaire - TotalPoids;

            if (Item.Poids > placeDispo)
                return false;

            Item.x = 0;
            Item.y = 0;
            hero.Items.Add(Item);
            context.SaveChanges();
            return true;




        }

        /// <summary>
        /// Auteur: Mathew Lemonde
        /// Oter un item de l'inventaire
        /// </summary>
        /// <param name="heroid"></param>
        /// <param name="itemid"></param>
        void IInventaireHeroController.DeleteItemFromHero(int heroid, int itemid)
        {
            var hero = context.Heroes.FirstOrNull(c => c.Id == heroid);
            if (hero == null)
                return;

            var Item = context.Items.FirstOrNull(c => c.Id == itemid);
            if (Item == null)
                return;

            hero.Items.Remove(Item);
            Item.x = hero.x;
            Item.y = hero.y;
            context.SaveChanges();
        }
        #endregion

        #region Item
        /// <summary>
        /// Auteur Francis Lussier
        /// permet de crée un item
        /// </summary>
        void IItemController.CreateItem(int mondeId, int _x, int _y, string nom, string description, decimal poids, int quantite, int reqDexterite, int reqEndurance, int reqForce, int reqIntelligence, int reqNiveau, decimal valeurArgent)
        {
            context.Items.Add(new Item()
            {
                Description = description,
                MondeId = mondeId,
                Nom = nom,
                Poids = poids,
                Quantite = quantite,
                ReqDexterite = reqDexterite,
                ReqEndurance = reqEndurance,
                ReqForce = reqForce,
                ReqIntelligence = reqIntelligence,
                ReqNiveau = reqNiveau,
                ValeurArgent = valeurArgent,
                x = _x,
                y = _y
            });
            context.SaveChanges();
        }

        /// <summary>
        /// Auteur Francis Lussier
        /// permet de supprimer un item
        /// </summary>
        void IItemController.DeleteItem(int itemId)
        {
            Item item = context.Items.FirstOrNull(i => i.Id == itemId);
            if (item != null)
            {
                context.Items.Remove(item);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Auteur Francis Lussier
        /// permet de modifier un item
        /// </summary>
        void IItemController.EditItem(int itemId, int quantite)
        {
            Item item = context.Items.FirstOrNull(i => i.Id == itemId);
            if (item != null)
            {
                item.Quantite = quantite;
                context.SaveChanges();
            }
        }
        #endregion

        #region ObjectMonde
        /// <summary>
        /// Auteur : Marc-André Landry
        /// Create an object (ex: scenery, rock, water, etc) in the world
        /// </summary>
        /// <param name="iX">Where should your object be put m8? (x)</param>
        /// <param name="iY">Where should your object be put m8? (y)</param>
        /// <param name="sDescription">Gimme a small description of your object mate</param>
        /// <param name="iTypeObjet">What's the type of your object (WARNING : integers)</param>
        /// <param name="iMondeId">The ID of your world</param>
        void IObjectMondeController.CreateObjectMonde(int iX, int iY, string sDescription, int iTypeObjet, int iMondeId)
        {
            var Monde = context.Mondes.FirstOrNull(c => c.Id == iMondeId);
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
            context.ObjetMondes.Add(objMonde);
            Monde.ObjetMondes.Add(objMonde);
            context.SaveChanges();
        }

        /// <summary>
        /// Auteur : Marc-André Landry
        /// 
        /// Delete an object from the world by its ID
        /// </summary>
        /// <param name="iID">The ID of the object you want to destroy</param>
        /// <param name="mondeid">Id du monde</param>
        void IObjectMondeController.DeleteObjectMonde(int objectMondeId, int mondeid)
        {
            Monde monde = context.Mondes.FirstOrNull(c => c.Id == mondeid);
            ObjetMonde objMonde = context.ObjetMondes.FirstOrNull(o => o.Id == objectMondeId);
            if (objMonde == null || monde == null)
                return;

            context.ObjetMondes.Remove(objMonde);
            monde.ObjetMondes.Remove(objMonde);
            context.SaveChanges();
        }

        /// <summary>
        /// Auteur : Marc-André Landry
        /// Modifi la description d'un object monde avec son ID
        /// </summary>
        /// <param name="iID">Id de l'objet</param>
        /// <param name="sDescription">Nouvelle description</param>
        void IObjectMondeController.EditObjectMondeDescription(int objectMondeId, string sDescription)
        {
            ObjetMonde objMonde = context.ObjetMondes.Find(objectMondeId);
            if (objMonde == null)
                return;
            else
                objMonde.Description = sDescription;

            context.SaveChanges();
        }
        #endregion




       //// private void Refreshcontext()
       // {
       //     var objContext = ((IObjectContextAdapter)context).ObjectContext;
       //     var refreshableObjects = (from entry in objContext.ObjectStateManager.GetObjectStateEntries(
       //                                         EntityState.Added
       //                                        | EntityState.Deleted
       //                                        | EntityState.Modified
       //                                        | EntityState.Unchanged)
       //                               where entry.EntityKey != null
       //                               select entry.Entity);

       //     objContext.Refresh(RefreshMode.StoreWins, refreshableObjects);
       // }



        bool IHeroController.PickupItem(int Heroid, int x, int y)
        {
            lock (context.Items)
            {
                Item item = context.Items.FirstOrNull(i => i.x == x && i.y == y);
                Hero hero = context.Heroes.FirstOrNull(h => h.Id == Heroid);                
                while (true)
                {
                    try
                    {
                        if (hero == null)
                            return false;

                        if (item != null)
                        {
                            var original = item.RowVersion;
                            item.x = 0;
                            item.y = 0;
                            hero.Items.Add(item);
                            context.SaveChanges();
                            var newro = item.RowVersion;
                            if (original != newro)
                            {
                               // Refreshcontext();
                                return true;
                            }
                        }
                        else
                            return false;
                    }
                    catch (Exception ex)
                    {
                        var objContext = ((IObjectContextAdapter)context).ObjectContext;
                        objContext.Refresh(RefreshMode.StoreWins, item);
                    }
                }
            }
            
        }
    }
}
