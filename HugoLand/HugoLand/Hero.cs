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
    
    public partial class Hero
    {
        public Hero()
        {
            this.PlayerHeroes = new HashSet<PlayerHero>();
            this.Spells = new HashSet<Spell>();
        }
    
        public int Id { get; set; }
        public string ClassName { get; set; }
        public string Description { get; set; }
        public float StatWeigthStr { get; set; }
        public float StatWeigthDex { get; set; }
        public float StatWeigthInt { get; set; }
        public float StatWeigthStam { get; set; }
    
        public virtual ICollection<PlayerHero> PlayerHeroes { get; set; }
        public virtual ICollection<Spell> Spells { get; set; }
    }
}
