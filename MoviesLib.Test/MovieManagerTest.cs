using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoviesLib.Entities;

namespace MoviesLib.Test
{
    [TestClass]
    public class MovieManagerTest
    {

        private static string _pathFilmsTest;

        #region Initializer

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            // Il faut créer un répertoire de film et créer
            // des fichiers de "films" avec des bonnes et mauvaises
            // extension.

            DirectoryInfo directoryInfo = new DirectoryInfo(AppContext.BaseDirectory);

            DirectoryInfo filmRepertoire = directoryInfo.CreateSubdirectory("films");
            File.Create(filmRepertoire.FullName + @"\filmAvi.avi");
            File.Create(filmRepertoire.FullName + @"\filmMkv.mkv");
            File.Create(filmRepertoire.FullName + @"\filmMp4.mp4");
            File.Create(filmRepertoire.FullName + @"\fichierWeb.html");
            File.Create(filmRepertoire.FullName + @"\readme.txt");

            DirectoryInfo sousFilmRepertoire = filmRepertoire.CreateSubdirectory("AutreRepertoire");
            File.Create(sousFilmRepertoire.FullName + @"\phantom-thread-french-webrip-1080p-2018.avi");


            _pathFilmsTest = filmRepertoire.FullName;
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(_pathFilmsTest);
            directoryInfo.Delete(true);
        }

        #endregion




        [TestMethod]
        [TestProperty("MovieLib", "MovieManager")]
        [Description("Récupération des fichiers vidéos d'un répertoire.")]
        public void TestSurLaRecuperationDesFichiersVideos_When_LeRepertoireEtLesFichiersExistent_Then_DonneLaListe()
        {
            #region ARRANGE

            string nomFilm = "phantom-thread-french-webrip-1080p-2018";

            #endregion

            #region ACT

            MovieManager manager = new MovieManager("TRUEFRENCH", "FRENCH", "FR");
            IEnumerable<MovieInformation> result = manager.GetMoviesInformations(_pathFilmsTest, TypeVideo.Movie);

            #endregion

            #region ASSERT

            Assert.AreEqual(result.Count(), 4, "Il doit y avoir 4 films dans la liste");

            var listeTemp = result.ToList();
            MovieInformation movieTest = listeTemp[3];
            Assert.AreEqual(movieTest.Annee, "2018");
            Assert.AreEqual(movieTest.Langage, "FRENCH");
            Assert.AreEqual(movieTest.Qualite, "webrip");
            Assert.AreEqual(movieTest.Resolution, "1080p");
            Assert.AreEqual(movieTest.Titre, "PHANTOM THREAD");


            #endregion
        }

    }
}
