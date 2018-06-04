using System;
using System.Collections.Generic;
using System.Text;

namespace MoviesLib.Entities
{
	/// <summary>
	/// Class contenant les informations d'une série.
	/// </summary>
    public class ShowInformation : VideoInformation, IEquatable<ShowInformation>
	{
	    #region Properties

		/// <summary>
		/// Numéro de la saison.
		/// </summary>
	    public short Saison { get; set; }

		/// <summary>
		/// Numéro de l'épisode.
		/// </summary>
	    public short Episode { get; set; }

		#endregion

		public bool Equals(ShowInformation other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;

			return string.Equals(Titre, other.Titre)
				&& Saison == other.Saison 
				&& Episode == other.Episode;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;

			if (obj.GetType() != this.GetType()) return false;
			return Equals((ShowInformation) obj);
		}

		public override int GetHashCode()
		{
			return (Titre != null ? Titre.GetHashCode() : 0) + Saison.GetHashCode() + Episode.GetHashCode();
		}
	}
}
