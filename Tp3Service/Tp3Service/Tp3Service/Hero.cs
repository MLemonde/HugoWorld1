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
    using System.Runtime.Serialization;

    [DataContract(IsReference = true)]  
    public partial class Hero
    {
        public Hero()
        {
            this.Items = new HashSet<Item>();
        }

        [DataMember]
        public int CompteJoueurId { get; set; }
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int Niveau { get; set; }
        [DataMember]
        public long Experience { get; set; }
        [DataMember]
        public int x { get; set; }
        [DataMember]
        public int y { get; set; }
        [DataMember]
        public decimal Argent { get; set; }
        [DataMember]
        public int StatBaseStr { get; set; }
        [DataMember]
        public int StatBaseDex { get; set; }
        [DataMember]
        public int StatBaseInt { get; set; }
        [DataMember]
        public int StatBaseStam { get; set; }
        [DataMember]
        public int MondeId { get; set; }
        [DataMember]
        public int ClasseId { get; set; }

        [DataMember]
        public virtual Classe Classe { get; set; }
        [DataMember]
        public virtual CompteJoueur CompteJoueur { get; set; }
        [DataMember]
        public virtual Monde Monde { get; set; }
        [DataMember]
        public virtual ICollection<Item> Items { get; set; }
    }
}
