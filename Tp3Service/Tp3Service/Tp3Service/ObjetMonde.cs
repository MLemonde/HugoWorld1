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
    public partial class ObjetMonde
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int x { get; set; }
        [DataMember]
        public int y { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public int TypeObjet { get; set; }
        [DataMember]
        public int MondeId { get; set; }

        [DataMember]
        public virtual Monde Monde { get; set; }
    }
}
