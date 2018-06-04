using System;
using System.Collections.Generic;
using System.Text;

namespace MoviesLib.Entities
{
    /// <summary>
    /// Classe contenant toutes les informations sur un film.
    /// </summary>
    public class MovieInformation : VideoInformation, IEquatable<MovieInformation>
    {
        #region Properties
        
        /// <summary>
        /// Détermine le type de vidéo.
        /// </summary>
        public TypeVideo TypeVideo { get; set; }

        #endregion

        #region Implement IEquatable

        public bool Equals(MovieInformation other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return string.Equals(Titre, other.Titre);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;

            if (obj.GetType() != this.GetType()) return false;
            return Equals((MovieInformation)obj);
        }

        public override int GetHashCode()
        {
            return (Titre != null ? Titre.GetHashCode() : 0);
        }

        #endregion
    }

    public enum TypeVideo
    {
        DessinAnime,
        Movie
    }
}
