//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Tp3Service
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [DataContract(IsReference = true)]
    public partial class Classe
    {
        public Classe()
        {

            this.Heroes = new HashSet<Hero>();
        }
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string NomClasse { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public float StatPoidsStr { get; set; }
        [DataMember]
        public float StatPoidsDex { get; set; }
        [DataMember]
        public float StatPoidsInt { get; set; }
        [DataMember]
        public float StatPoidsStam { get; set; }
        [DataMember]
        public int MondeId { get; set; }

        [DataMember]
        public virtual Monde Monde { get; set; }
        [DataMember]
        public virtual ICollection<Hero> Heroes { get; set; }
    }
}
