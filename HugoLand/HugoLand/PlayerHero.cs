//------------------------------------------------------------------------------
// <auto-generated>
//    Ce code a été généré à partir d'un modèle.
//
//    Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//    Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HugoLand
{
    using System;
    using System.Collections.Generic;
    
    public partial class PlayerHero
    {
        public PlayerHero()
        {
            this.PlayerInventories = new HashSet<PlayerInventory>();
        }
    
        public int UserAccountID { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public int HeroID { get; set; }
        public long Experience { get; set; }
        public int Money { get; set; }
        public int StatBaseStr { get; set; }
        public int StatBaseDex { get; set; }
        public int StatBaseInt { get; set; }
        public int StatBaseSTAM { get; set; }
    
        public virtual Hero Hero { get; set; }
        public virtual ICollection<PlayerInventory> PlayerInventories { get; set; }
        public virtual UserAccount UserAccount { get; set; }
    }
}
