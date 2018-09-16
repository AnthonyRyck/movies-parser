using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace MoviesLib
{
	public abstract class VideoParser
	{
		#region Protected Properties

		protected const string INCONNU = "Inconnu";
		private string _languesPattern;

		#endregion

		#region Protected Methods

		/// <summary>
		/// Méthode permettant d'enlever les informations inutiles.
		/// </summary>
		/// <param name="title"></param>
		/// <returns></returns>
		protected string RemoveExtraInformation(string title)
		{
			string tempTitle = title;

			if (tempTitle.Contains("[") && tempTitle.Contains("]"))
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

		protected string ReplaceCharacters(string title)
		{
			return title.Replace(' ', '.')
				.Replace('-', '.')
				.Replace('(', '.')
				.Replace(')', '.');
		}


		#endregion
		
	}
}
