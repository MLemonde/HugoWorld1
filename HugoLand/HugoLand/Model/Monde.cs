//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HugoLand.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Monde
    {
        public Monde()
        {
            this.Classes = new HashSet<Classe>();
            this.Heroes = new HashSet<Hero>();
            this.Items = new HashSet<Item>();
            this.Monstres = new HashSet<Monstre>();
            this.ObjetMondes = new HashSet<ObjetMonde>();
        }
    
        public int Id { get; set; }
        public string Description { get; set; }
        public string LimiteX { get; set; }
        public string LimiteY { get; set; }
    
        public virtual ICollection<Classe> Classes { get; set; }
        public virtual ICollection<Hero> Heroes { get; set; }
        public virtual ICollection<Item> Items { get; set; }
        public virtual ICollection<Monstre> Monstres { get; set; }
        public virtual ICollection<ObjetMonde> ObjetMondes { get; set; }
    }
}
