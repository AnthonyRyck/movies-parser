using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using MoviesLib.Entities;

namespace MoviesLib
{
    public class ShowManager
    {
        #region Properties

        /// <summary>
        /// Contient les noms des extensions possibles pour les films et séries.
        /// </summary>
        private IEnumerable<string> _fileExtensionCollection = new List<string>()
        {
            ".avi", ".mkv", ".mp4"
        };

        private ShowFileParser _showFileParser;

        #endregion

        #region Constructeur

        public ShowManager(params string[] languesChoisis)
        {
            _showFileParser = new ShowFileParser(languesChoisis);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Récupère les informations sur les séries du chemin donnée.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public IEnumerable<ShowInformation> GetShowsInformation(string path)
        {
            List<ShowInformation> retourInformations = new List<ShowInformation>();
            FileInfo[] videoFiles = GetAllVideoFiles(path);

            foreach (var file in videoFiles)
            {
                string fileName = RemoveExtension(file);
                ShowInformation information = _showFileParser.GetShow(fileName, file.Name, file.Length);
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
