//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Tp3Service
{
    using System;
    using System.Collections.Generic;
    
    public partial class Item
    {
        public Item()
        {
            this.EffetItems = new HashSet<EffetItem>();
            this.Heroes = new HashSet<Hero>();
        }
    
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Description { get; set; }
        public Nullable<int> x { get; set; }
        public Nullable<int> y { get; set; }
        public int Niveau { get; set; }
        public Nullable<decimal> ValeurArgent { get; set; }
        public decimal Poids { get; set; }
        public int ReqNiveau { get; set; }
        public int ReqForce { get; set; }
        public int ReqDexterite { get; set; }
        public int ReqIntelligence { get; set; }
        public int ReqEndurance { get; set; }
        public int MondeId { get; set; }
        public int Quantite { get; set; }
    
        public virtual ICollection<EffetItem> EffetItems { get; set; }
        public virtual Monde Monde { get; set; }
        public virtual ICollection<Hero> Heroes { get; set; }
    }
}
