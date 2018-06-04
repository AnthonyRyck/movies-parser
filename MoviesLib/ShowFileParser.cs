using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using MoviesLib.Entities;

namespace MoviesLib
{
	/// <summary>
	/// Class permettant de faire l'extraction des informations du titre
	/// d'une série (nom d'un fichier).
	/// </summary>
	public class ShowFileParser
	{
		#region Properties

		private const string SAISON_PATTERN = "[Ss][0-9]{1,2}";
		private const string EPISODE_PATTERN = "[Ee][0-9]{1,2}";
	    private const string RESOLUTION_PATTERN = "[0-9]{3,4}p";
	    private const string QUALITY_PATTERN = @"(\.)([HP]DTV|HDCAM|B[rR]Rip|TS|WEB-DL|H[dD]Rip|DVDRip|DVDRiP|DVDRIP|CamRip|W[EB]B[rR]ip|[Bb]lu[Rr]ay|DvDScr|hdtv|[Hh][Dd][Ll]ight)(\.)";


        private string _languesPattern;

		#endregion

		#region Constructeur

		/// <summary>
		/// Constructeur du Parser pour les noms des Torrents.
		/// </summary>
		/// <param name="langues">Liste des langues que l'utilisateur veut récupérer.</param>
		public ShowFileParser(params string[] langues)
		{
			_languesPattern = BuildPattern(langues);
		}		

		#endregion

		#region Public Methods

		/// <summary>
		/// Méthode permettant de récupérer les informations d'un fichier pour une série.
		/// </summary>
		/// <param name="title"></param>
		/// <param name="fileName"></param>
		/// <param name="sizeByte"></param>
		/// <returns></returns>
		public ShowInformation GetShow(string title, string fileName, long sizeByte)
	    {
		    ShowInformation result = new ShowInformation();
	        result.FileName = fileName;
	        result.Size = sizeByte;

            // Ajout de "." à la place des espaces.
	        title = title.Replace(' ', '.')
	                     .Replace('-', '.')
	                     .Replace('(', '.')
	                     .Replace(')', '.');

			if(Regex.IsMatch(title, SAISON_PATTERN))
				result.Saison = GetSeasonOrEpisode(ref title, SAISON_PATTERN);

		    if (Regex.IsMatch(title, EPISODE_PATTERN))
			    result.Episode = GetSeasonOrEpisode(ref title, EPISODE_PATTERN);

			result.Resolution = GetResolution(ref title, RESOLUTION_PATTERN);
			result.Qualite = GetQuality(title, QUALITY_PATTERN);
			result.Langage = GetLanguage(title);

		    result.Titre = GetTitle(title);

			return result;  
	    }
		
	    #endregion

	    #region Private Methods Show

		/// <summary>
		/// Récupère la saison ou l'épisode. Dépends du pattern passé en paramètre.
		/// </summary>
		/// <param name="title"></param>
		/// <param name="pattern"></param>
		/// <returns></returns>
	    private short GetSeasonOrEpisode(ref string title, string pattern)
	    {
			// Récupération de l'information.
		    var matchResult = Regex.Match(title, pattern);

			// Suppression du resultat trouvé du titre.
		    title = RemoveResultRegex(matchResult.Value, title);

			// Récupération du numéro de l'épisode/Saison.
			// [^0-9] : Tout ce qui ne correspond pas à un nombre.
			var result = Regex.Replace(matchResult.Value, @"[^0-9]", string.Empty);
		    return Convert.ToInt16(result);
	    }

		/// <summary>
		/// Récupère la qualité du Torrent.
		/// </summary>
		/// <param name="title">Titre du Torrent.</param>
		/// <param name="pattern">Pattern du Regex.</param>
		/// <returns></returns>
	    private string GetQuality(string title, string pattern)
	    {
		    var matchResult = Regex.Match(title, pattern);
            RemoveResultRegex(matchResult.Value, title);

			return string.IsNullOrEmpty(matchResult.Value)
			    ? "Inconnu"
			    : matchResult.Value.Replace(".", String.Empty);
	    }

	    /// <summary>
	    /// Récupère la résoluion de la vidéo.
	    /// </summary>
	    /// <param name="title"></param>
	    /// <param name="pattern"></param>
	    /// <returns></returns>
	    private string GetResolution(ref string title, string pattern)
	    {
			Match matchResult = Regex.Match(title, pattern);
		    title = RemoveResultRegex(matchResult.Value, title);

			return string.IsNullOrEmpty(matchResult.Value)
			    ? "Inconnu"
			    : matchResult.Value;
	    }

		/// <summary>
		/// Supprime du titre ce qui est trouvé dans le Regex.
		/// </summary>
		/// <param name="resultatRegex"></param>
		/// <param name="titre"></param>
		/// <returns></returns>
	    private string RemoveResultRegex(string resultatRegex, string titre)
		{
			return !string.IsNullOrEmpty(resultatRegex) 
			? titre.Replace(resultatRegex, string.Empty) 
			: titre;
		}

		/// <summary>
		/// Extrait le titre de la serie/film.
		/// </summary>
		/// <param name="title"></param>
		/// <returns></returns>
	    private string GetTitle(string title)
		{
			int indexDoublePoint = title.IndexOf("..");

			if (indexDoublePoint > 0)
			{
				title = title.Substring(0, indexDoublePoint);
			}
			string retourTitle = title.Replace('.', ' ');

			// J'enlève tous caractères qui n'est pas alphabétique.
			// \W tous les caractères qui ne sont pas alphanumérique
			// ^\s exception sur les espaces.
			string result = Regex.Replace(retourTitle, @"\W^\s", string.Empty);

			return result;
		}

		/// <summary>
		/// Récupère le language de la vidéo. La langue cherché est donnée en 
		/// paramètre dans le constructeur.
		/// </summary>
		/// <returns></returns>
		private string GetLanguage(string titleTorrent)
		{
			string titleTempMajuscule = titleTorrent.ToUpper();
			Match matchResult = Regex.Match(titleTempMajuscule, _languesPattern);

			return string.IsNullOrEmpty(matchResult.Value)
				? "Inconnu"
				: matchResult.Value.Replace(".", string.Empty);
		}

		/// <summary>
		/// Construit un pattern pour un Regex.
		/// </summary>
		/// <param name="langues"></param>
		/// <returns></returns>
		private string BuildPattern(string[] langues)
		{
			// Il faut que le pattern commence par '.'
			string result = @"(\.)(";

			for (int i = 0; i < langues.Length; i++)
			{
				if(i == langues.Length - 1)
					result += ( langues[i].ToUpper());
				else
					result += (langues[i].ToUpper() + "|"); // '|' signe 'ou' pour le pattern.
			}

			// et finisse par un '.'
			result += @")(\.)";

			return result;
		}

		#endregion

	}
}