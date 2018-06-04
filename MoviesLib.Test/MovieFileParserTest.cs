using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoviesLib.Entities;

namespace MoviesLib.Test
{
    [TestClass]
    public class MovieFileParserTest
    {
        [TestMethod]
        [TestProperty("MoviesLib", "MovieFilePaser")]
        [Description("Test sur le type de retour de la méthode, doit être de type MovieInformation.")]
        public void TestSurLeRetourneDuType_When_RetourEgalMovieInformation_Then_True()
        {
            #region ARRANGE

            string titre = "paddington-2-french-bluray-1080p-2018";

            #endregion

            #region ACT

            MovieFileParser parser = new MovieFileParser();
            var resultParser = parser.GetInformation(titre, String.Empty, 0, TypeVideo.Movie);

            #endregion

            #region ASSERT

            Assert.IsTrue(resultParser.GetType().Name == typeof(MovieInformation).Name);

            #endregion
        }

        #region Tests sur la Qualité

        [TestMethod]
        [TestProperty("MoviesLib", "MovieFileParser")]
        [Description("Il faut extraire la résolution du titre.")]
        public void ExtraireQualiteDuTitre_When_ResolutionEst1080p_Then_1080p()
        {
            #region ARRANGE

            string titre = "paddington-2-french-bluray-1080p-2018";

            #endregion

            #region ACT

            MovieFileParser parser = new MovieFileParser();
            var resultParser = parser.GetInformation(titre, String.Empty, 0, TypeVideo.Movie);

            #endregion

            #region ASSERT

            Assert.AreEqual("1080p", resultParser.Resolution);

            #endregion
        }

        #endregion

        [TestMethod]
        [TestProperty("MoviesLib", "MovieFileParser")]
        [Description("Il faut extraire la qualité du titre.")]
        public void ExtraireLangueDuTitre_When_French()
        {
            #region ARRANGE

            string titre = "paddington-2-french-bluray-1080p-2018";

            #endregion

            #region ACT

            MovieFileParser parser = new MovieFileParser("french", "TRUEFRENCH");
            var resultParser = parser.GetInformation(titre, String.Empty, 0, TypeVideo.Movie);

            #endregion

            #region ASSERT

            Assert.AreEqual("1080p", resultParser.Resolution);
            Assert.AreEqual("FRENCH", resultParser.Langage);

            #endregion
        }

        [TestMethod]
        [TestProperty("MoviesLib", "MovieFileParser")]
        [Description("")]
        public void ExtraireDeToutesLesInformations()
        {
            #region ARRANGE

            string titreSeries = "An.American.Haunting.2005.TRUEFRENCH.DVDRip.XviD.Mp3-Tetine";

            #endregion

            #region ACT

            MovieFileParser parser = new MovieFileParser("VOSTFR", "french", "TRUEFRENCH");
            var resultParser = parser.GetInformation(titreSeries, String.Empty, 0, TypeVideo.Movie);

            #endregion

            #region ASSERT

            Assert.AreEqual("Inconnu", resultParser.Resolution);
            Assert.AreEqual("DVDRip", resultParser.Qualite);
            Assert.AreEqual("2005", resultParser.Annee);
            Assert.AreEqual("TRUEFRENCH", resultParser.Langage);
            Assert.AreEqual("AN AMERICAN HAUNTING", resultParser.Titre);

            #endregion
        }

        [TestMethod]
        [TestProperty("MoviesLib", "MovieFileParser")]
        [Description("")]
        public void ExtraireDeToutesLesInfosDuTitre()
        {
            #region ARRANGE

            string titreSeries = "Black Christmas 2006 TRUEFRENCH DVDRIP Xvid MP3";

            #endregion

            #region ACT

            MovieFileParser parser = new MovieFileParser("VOSTFR", "french", "TRUEFRENCH");
            var resultParser = parser.GetInformation(titreSeries, String.Empty, 0, TypeVideo.Movie);

            #endregion

            #region ASSERT

            Assert.AreEqual("Inconnu", resultParser.Resolution);
            Assert.AreEqual("DVDRIP", resultParser.Qualite);
            Assert.AreEqual("2006", resultParser.Annee);
            Assert.AreEqual("TRUEFRENCH", resultParser.Langage);
            Assert.AreEqual("BLACK CHRISTMAS", resultParser.Titre);

            #endregion
        }

        [TestMethod]
        [TestProperty("MoviesLib", "MovieFileParser")]
        [Description("")]
        public void ExtraireDeToutesLesInfosDuTitre_When_TorrentIsTorrent9()
        {
            #region ARRANGE

            string titreSeries = "[ Torrent9.red ] Maze.Runner.The.Death.Cure.2018.MULTi.TRUEFRENCH.1080p.WEB.H264-SiGeRiS.mkv";

            #endregion

            #region ACT

            MovieFileParser parser = new MovieFileParser("VOSTFR", "french", "TRUEFRENCH");
            var resultParser = parser.GetInformation(titreSeries, String.Empty, 0, TypeVideo.Movie);

            #endregion

            #region ASSERT

            Assert.AreEqual("1080p", resultParser.Resolution);
            Assert.AreEqual("Inconnu", resultParser.Qualite);
            Assert.AreEqual("2018", resultParser.Annee);
            Assert.AreEqual("TRUEFRENCH", resultParser.Langage);
            Assert.AreEqual("MAZE RUNNER THE DEATH CURE", resultParser.Titre);

            #endregion
        }

        [TestMethod]
        [TestProperty("MoviesLib", "MovieFileParser")]
        [Description("")]
        public void ExtraireDeToutesLesInfosDuTitre_When_TitreContientParenthese()
        {
            #region ARRANGE

            string titreSeries = "American Made (2017) VFF-ENG AC3 BluRay 1080p x264.GHT.mkv";

            #endregion

            #region ACT

            MovieFileParser parser = new MovieFileParser("VOSTFR", "french", "TRUEFRENCH");
            var resultParser = parser.GetInformation(titreSeries, String.Empty, 0, TypeVideo.Movie);

            #endregion

            #region ASSERT

            Assert.AreEqual("1080p", resultParser.Resolution);
            Assert.AreEqual("BluRay", resultParser.Qualite);
            Assert.AreEqual("2017", resultParser.Annee);
            Assert.AreEqual("Inconnu", resultParser.Langage);
            Assert.AreEqual("AMERICAN MADE", resultParser.Titre);

            #endregion
        }

        [TestMethod]
        [TestProperty("MoviesLib", "MovieFileParser")]
        [Description("")]
        public void ExtraireDeToutesLesInfosDuTitre_When_TitreContientChiffreRomain()
        {
            #region ARRANGE

            string titreSeries = "Cars Ii 1080p x264.GHT.mkv";

            #endregion

            #region ACT

            MovieFileParser parser = new MovieFileParser("VOSTFR", "french", "TRUEFRENCH");
            var resultParser = parser.GetInformation(titreSeries, String.Empty, 0, TypeVideo.DessinAnime);

            #endregion

            #region ASSERT

            Assert.AreEqual("1080p", resultParser.Resolution);
            Assert.AreEqual("Inconnu", resultParser.Qualite);
            Assert.AreEqual("Inconnu", resultParser.Annee);
            Assert.AreEqual("Inconnu", resultParser.Langage);
            Assert.AreEqual("CARS 2", resultParser.Titre);

            #endregion
        }

        [TestMethod]
        [TestProperty("MoviesLib", "MovieFileParser")]
        [Description("")]
        public void ExtraireDeToutesLesInfosDuTitre_When_TitreNeContientPasChiffreRomain()
        {
            #region ARRANGE

            string titreSeries = "Bienvenue à Suburbicon FRENCH HDlight 1080p 2018.mkv";

            #endregion

            #region ACT

            MovieFileParser parser = new MovieFileParser("VOSTFR", "FRENCH", "TRUEFRENCH");
            var resultParser = parser.GetInformation(titreSeries, String.Empty, 0, TypeVideo.DessinAnime);

            #endregion

            #region ASSERT

            Assert.AreEqual("1080p", resultParser.Resolution);
            Assert.AreEqual("HDlight", resultParser.Qualite);
            Assert.AreEqual("2018", resultParser.Annee);
            Assert.AreEqual("FRENCH", resultParser.Langage);
            Assert.AreEqual("BIENVENUE À SUBURBICON", resultParser.Titre);

            #endregion
        }

	    [TestMethod]
	    [TestProperty("MoviesLib", "MovieFileParser")]
	    [Description("")]
	    public void ExtraireDeToutesLesInfosDuTitre_When_ContientUneAnneeDansLeTitre()
	    {
		    #region ARRANGE

		    string titreSeries = "Blade.Runner.2049.2017.Multi.1080p.Bluray.Remux.AVC-Santec29.mkv";

		    #endregion

		    #region ACT

		    MovieFileParser parser = new MovieFileParser("VOSTFR", "FRENCH", "TRUEFRENCH");
		    var resultParser = parser.GetInformation(titreSeries, String.Empty, 0, TypeVideo.Movie);

		    #endregion

		    #region ASSERT

		    Assert.AreEqual("1080p", resultParser.Resolution);
		    Assert.AreEqual("Bluray", resultParser.Qualite);
		    Assert.AreEqual("2017", resultParser.Annee);
		    Assert.AreEqual("BLADE RUNNER 2049", resultParser.Titre);

		    #endregion
	    }

	}
}
