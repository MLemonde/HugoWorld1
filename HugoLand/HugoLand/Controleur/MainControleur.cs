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
        public void CreateMap()
        {
            Map map = new Map()
            {

            };
            context.Maps.Add(map);
            context.SaveChanges();
        }
        public void EditMap(int id)
        {
            Map map = context.Maps.Find(id);

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
