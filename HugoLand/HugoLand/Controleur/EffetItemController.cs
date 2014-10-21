using HugoLand.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HugoLand.Controller
{
    class EffetItemController
    {
        /// <summary>
        /// Auteur Francis
        /// </summary>
        public void CreateEffetItem(int itemId, int typeEffet, int valeurEffet)
        {
            using (RpgGameEntities context = new RpgGameEntities())
            {
                context.EffetItems.Add(new EffetItem()
                {
                    ItemId = itemId,
                    TypeEffet = typeEffet,
                    ValeurEffet = valeurEffet,
                });
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Auteur Francis
        /// </summary>
        public void DeleteEffetItem(int effetItemId)
        {
            using (RpgGameEntities context = new RpgGameEntities())
            {
                EffetItem effetItem = context.EffetItems.FirstOrNull(i => i.Id == effetItemId);
                if (effetItem == null)
                    return;
                effetItem.Item.EffetItems.Remove(effetItem);
                context.EffetItems.Remove(effetItem);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Auteur Francis
        /// </summary>
        public void EditEffetItem(int effetItemId, int typeEffet, int valeurEffet)
        {
            using (RpgGameEntities context = new RpgGameEntities())
            {
                EffetItem effetItem = context.EffetItems.FirstOrNull(i => i.Id == effetItemId);
                if (effetItem == null)
                    return;
                effetItem.TypeEffet = typeEffet;
                effetItem.ValeurEffet = valeurEffet;

                context.SaveChanges();
            }
        }
    }
}
