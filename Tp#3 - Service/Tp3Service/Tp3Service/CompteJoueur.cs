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
    
    public partial class CompteJoueur
    {
        public CompteJoueur()
        {
            this.Heroes = new HashSet<Hero>();
        }
    
        public int Id { get; set; }
        public string NomUtilisateur { get; set; }
        public string Password { get; set; }
        public string Courriel { get; set; }
        public string Prenom { get; set; }
        public string Nom { get; set; }
        public int TypeUtilisateur { get; set; }
    
        public virtual ICollection<Hero> Heroes { get; set; }
    }
}
