using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using MoviesLib.Entities;

namespace MoviesLib
{
    /// <summary>
    /// Classe de gestion pour les fichiers.
    /// </summary>
    public class MovieManager
    {
        #region Properties

        /// <summary>
        /// Contient les noms des extensions possibles pour les films et séries.
        /// </summary>
        private IEnumerable<string> _fileExtensionCollection = new List<string>()
        {
            ".avi", ".mkv", ".mp4"
        };

        private MovieFileParser _movieFileParser;

        #endregion

        #region Constructeur

        /// <summary>
        /// 
        /// </summary>
        /// <param name="languesChoisis"></param>
        public MovieManager(params string[] languesChoisis)
        {
            _movieFileParser = new MovieFileParser(languesChoisis);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Récupère les informations sur les vidéos du chemin donnée.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="type">Donne le type de vidéo.</param>
        /// <returns></returns>
        public IEnumerable<MovieInformation> GetMoviesInformations(string path, TypeVideo type)
        {
            List<MovieInformation> retourInformations = new List<MovieInformation>();
            FileInfo[] videoFiles = GetAllVideoFiles(path);

            foreach (var file in videoFiles)
            {
                string fileName = RemoveExtension(file);
                MovieInformation information = _movieFileParser.GetInformation(fileName, file.Name, file.Length, type);
                information.PathFile = file.FullName;

                retourInformations.Add(information);
            }

            return retourInformations;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Récupère les "FileInfo" de tous les fichiers contenu dans le
        /// chemin donné en paramètre.
        /// Un filtre est appliqué pour ne prendre que des fichiers vidéos
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private FileInfo[] GetAllVideoFiles(string path)
        {
            DirectoryInfo directory = new DirectoryInfo(path);
            FileInfo[] allFiles = directory.GetFiles("*", SearchOption.AllDirectories);

            return allFiles.Where(x => _fileExtensionCollection.Contains(x.Extension)).ToArray();
        }

        /// <summary>
        /// Enlève l'extension du fichier.
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private string RemoveExtension(FileInfo file)
        {
            return file.Name.Replace(file.Extension, string.Empty);
        }

        #endregion
    }
}