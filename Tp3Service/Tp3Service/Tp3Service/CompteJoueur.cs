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
    public partial class CompteJoueur
    {
        public CompteJoueur()
        {
            this.Heroes = new HashSet<Hero>();
        }
    [DataMember]
        public int Id { get; set; }
    [DataMember]
    public string NomUtilisateur { get; set; }
    [DataMember]
    public string Password { get; set; }
    [DataMember]
    public string Courriel { get; set; }
    [DataMember]
    public string Prenom { get; set; }
    [DataMember]
    public string Nom { get; set; }
    [DataMember]
    public int TypeUtilisateur { get; set; }

    [DataMember]
    public virtual ICollection<Hero> Heroes { get; set; }
    }
}
