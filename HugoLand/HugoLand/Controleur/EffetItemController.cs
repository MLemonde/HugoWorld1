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
        HugoWorldEntities context = new HugoWorldEntities();

        /// <summary>
        /// Auteur Francis
        /// </summary>
        public void CreateEffetItem(int itemId, int typeEffet, int valeurEffet)
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
        public void DeleteEffetItem(int effetItemId)
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
        public void EditEffetItem(int effetItemId, int typeEffet, int valeurEffet)
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
