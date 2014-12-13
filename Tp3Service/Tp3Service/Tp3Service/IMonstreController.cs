using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Tp3Service
{
    [ServiceContract]
    public interface IMonstreController
    {
        /// <summary>
        /// Auteur: Mathew Lemonde
        /// 
        /// Creation d'un Monstre avec valeur aléatoires
        /// </summary>
        /// <param name="Mondeid">Id du monde</param>
        [OperationContract]
        void CreateMonster(int Mondeid, int x, int y, int HP, string sname);

        /// <summary>
        /// Auteur: Mathew Lemonde
        /// 
        /// Suppression d'un monstre
        /// </summary>
        /// <param name="monsterid">id du Monstre a delete</param>
        /// <param name="Mapid">Id du monde</param>
        [OperationContract]
        void DeleteMonster(int monsterid);

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
        [OperationContract]
        void EditMonster(int MonsterID, string sNom, int iLvl, int ix, int iy, int iPv, int iDmgMin, int iDmgMax);

        [OperationContract]
        void KillMonster(int mondeId, int x, int y);
    }
}
