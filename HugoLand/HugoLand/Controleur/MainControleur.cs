using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HugoLand.Model;

namespace HugoLand.Controleur
{
    class MainControleur
    {
        rpggameEntities context = new rpggameEntities();

        #region MAP
        public void CreateMap(string sName,string sDescription,int iLevel,int iReqLevel) //TODO CHECKER PARAMS
        {
            Map map = new Map()
            {
                Name = sName,
                Description = sDescription,
                Level = iLevel,
                ReqLevel = iReqLevel
           
            };
            context.Maps.Add(map);
            context.SaveChanges();
        }
        public void EditMap(int id,string sDesc) //TODO LIMITES
        {
            Map map = context.Maps.Find(id);
            if (map == null)
                return;
            else
            {
                map.Description = sDesc;                
            }
            context.SaveChanges();
        }
        public void DeleteMap(Map map)
        {
            context.Maps.Remove(map);
            context.SaveChanges();
        }

        public List<Map> GetListMap()
        {
            return context.Maps.ToList();
        }
        #endregion

    }
}
