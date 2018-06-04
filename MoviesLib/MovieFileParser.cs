using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using MoviesLib.Entities;

namespace MoviesLib
{
    /// <summary>
    /// Class permettant de faire l'extraction des informations du titre
    /// d'un film (nom de fichier).
    /// </summary>
    public class MovieFileParser
    {
        #region Properties

        private const string RESOLUTION_PATTERN = "[0-9]{3,4}p";
        private const string QUALITY_PATTERN = @"([HP]DTV|HDCAM|B[rR]Rip|TS|WEB-DL|H[dD]Rip|DVDRip|DVDRiP|DVDRIP|CamRip|webrip|W[EB]B[rR]ip|[Bb]lu[Rr]ay|DvDScr|hdtv|[Hh][Dd][Ll]ight)";
        private const string INCONNU = "Inconnu";
        private const string CHIFFRE_ROMAIN_PATTERN = @"(\sI(\s|$)|\sII(\s|$)|\sIII(\s|$)|\sIV(\s|$)|\sV(\s|$)|\sVI(\s|$)|\sVII(\s|$)|\sVIII(\s|$)|\sIX(\s|$)|\sX(\s|$))";

        private string _languesPattern;
        private IEnumerable<ChiffreRomain> _chiffreRomainsCollection = new List<ChiffreRomain>()
        {
            ChiffreRomain.I,
            ChiffreRomain.II,
            ChiffreRomain.III,
            ChiffreRomain.IV,
            ChiffreRomain.V,
            ChiffreRomain.VI,
            ChiffreRomain.VII,
            ChiffreRomain.VIII,
            ChiffreRomain.IX,
            ChiffreRomain.X
        };

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur du Parser pour les noms des Torrents.
        /// </summary>
        /// <param name="langues">Liste des langues que l'utilisateur veut récupérer.</param>
        public MovieFileParser(params string[] langues)
        {
            _languesPattern = BuildPattern(langues);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Permet de récupérer les informations sur un film par rapport
        /// à son titre.
        /// </summary>
        /// <param name="title">Nom du fichier sans l'extension</param>
        /// <param name="fileName">Nom du fichier avec son extension.</param>
        /// <param name="sizeByte">Taille en Bytes du fichier.</param>
        /// <param name="type">Donne le type de vidéo.</param>
        /// <returns></returns>
        public MovieInformation GetInformation(string title, string fileName, long sizeByte, TypeVideo type)
        {
            MovieInformation result = new MovieInformation();
            result.FileName = fileName;
            result.Size = sizeByte;
            result.TypeVideo = type;

            title = RemoveExtraInformation(title);

            // Ajout de "." à la place des espaces.
            title = title.Replace(' ', '.')
                         .Replace('-','.')
                         .Replace('(','.')
                         .Replace(')', '.');

            result.Resolution = GetResolution(ref title, RESOLUTION_PATTERN);
            result.Qualite = GetQuality(ref title, QUALITY_PATTERN);
            result.Annee = GetAnneeProduction(ref title);
            result.Langage = GetLanguage(ref title);
            result.Titre = GetTitle(title);

            return result;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Extrait du titre les chiffres romains, et remplace en chiffre.
        /// </summary>
        /// <param name="titre"></param>
        /// <returns></returns>
        private string ExtractRomanNumber(string titre)
        {
            string tempTitre = titre;
            Match matchResult = Regex.Match(tempTitre, CHIFFRE_ROMAIN_PATTERN);

            if (!string.IsNullOrEmpty(matchResult.Value))
            {
                string temp = matchResult.Value.Replace(" ", string.Empty);

                foreach (ChiffreRomain chiffreRomain in _chiffreRomainsCollection)
                {
                    if (temp == chiffreRomain.ToString("f"))
                    {
                        tempTitre = tempTitre.Replace(matchResult.Value, " " + chiffreRomain.ToString("d"));
                    }
                }
            }

            return tempTitre;
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
                ? INCONNU
                : matchResult.Value;
        }

        /// <summary>
        /// Récupère la qualité.
        /// </summary>
        /// <param name="title">Titre du film.</param>
        /// <param name="pattern">Pattern du Regex.</param>
        /// <returns></returns>
        private string GetQuality(ref string title, string pattern)
        {
            var matchResult = Regex.Match(title, pattern);
            title = RemoveResultRegex(matchResult.Value, title);

            return string.IsNullOrEmpty(matchResult.Value)
                ? INCONNU
                : matchResult.Value.Replace(".", String.Empty);
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
            string result;

            if (indexDoublePoint > -1)
            {
                string tempTitle = title.Substring(0, indexDoublePoint);
                string retourTitle = tempTitle.Replace('.', ' ');

                // J'enlève tous caractères qui n'est pas alphabétique.
                // \W tous les caractères qui ne sont pas alphanumérique
                // ^\s exception sur les espaces.
                result = Regex.Replace(retourTitle, @"\W^\s", string.Empty);
            }
            else
            {
                result = title;
            }

            result = ExtractRomanNumber(result);

            return result;
        }

		/// <summary>
		/// Extrait l'année de production d'un film.
		/// DOIT PASSER APRES l'extraction de la résolution.
		/// </summary>
		/// <param name="title"></param>
		/// <returns></returns>
		private string GetAnneeProduction(ref string title)
		{
			const string patternAnnee = "[0-9]{4}";
			Match matchResult = Regex.Match(title, patternAnnee, RegexOptions.RightToLeft);

			title = RemoveResultRegex(matchResult.Value, title);

			return string.IsNullOrEmpty(matchResult.Value)
				? INCONNU
				: matchResult.Value;
		}

		/// <summary>
		/// Récupère le language de la vidéo. La langue cherché est donnée en 
		/// paramètre dans le constructeur.
		/// </summary>
		/// <returns></returns>
		private string GetLanguage(ref string title)
        {
            string titleTempMajuscule = title.ToUpper();
            Match matchResult = Regex.Match(titleTempMajuscule, _languesPattern);

            title = RemoveResultRegex(matchResult.Value, titleTempMajuscule);

            return string.IsNullOrEmpty(matchResult.Value)
                ? INCONNU
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
            string result = @"(";

            for (int i = 0; i < langues.Length; i++)
            {
                if (i == langues.Length - 1)
                    result += langues[i].ToUpper();
                else
                    result += langues[i].ToUpper() + "|"; // '|' signe 'ou' pour le pattern.
            }

            // et finisse par un '.'
            result += @")";

            return result;
        }

        /// <summary>
        /// Méthode permettant d'enlever les informations inutiles.
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        private string RemoveExtraInformation(string title)
        {
            string tempTitle = title;

            if(tempTitle.Contains("[") && tempTitle.Contains("]"))
            {
                int indexStart = tempTitle.IndexOf('[');
                int indexStop = tempTitle.IndexOf(']');
                int count = indexStop - indexStart + 1;

                tempTitle = tempTitle.Remove(indexStart, count);
            }

            if (tempTitle[0] == '.' || tempTitle[0] == ' ')
                tempTitle = tempTitle.Remove(0, 1);

            return tempTitle;
        }

        #endregion
    }
}