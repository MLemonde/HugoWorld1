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
    
    public partial class ObjetMonde
    {
        public int Id { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public string Description { get; set; }
        public int TypeObjet { get; set; }
        public int MondeId { get; set; }
    
        public virtual Monde Monde { get; set; }
    }
}
