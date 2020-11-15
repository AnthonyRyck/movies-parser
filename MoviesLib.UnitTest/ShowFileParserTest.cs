using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoviesLib;
using MoviesLib.Entities;

namespace MoviesLib.UnitTest
{
    [TestClass]
    public class ShowFileParserTest
    {
        [TestMethod]
        [TestProperty("MoviesLib", "ShowFileParser")]
        [Description("Test sur le type de retour de la méthode, doit être de type ShowInformation.")]
        public void TestSurLeRetourneDuType_When_RetourEgalShowInformation_Then_True()
        {
            #region ARRANGE

            string titreSeries = "Frontier.S01E01.FASTSUB.VOSTFR.HDTV.x264-otm";

            #endregion

            #region ACT

            ShowFileParser parser = new ShowFileParser();
            var resultParser = parser.GetShow(titreSeries, "fakeName", 0);

            #endregion

            #region ASSERT

            Assert.IsTrue(resultParser.GetType().Name == typeof(ShowInformation).Name);

            #endregion
        }

        #region Saison

        [TestMethod]
        [TestProperty("MoviesLib", "ShowFileParser")]
        [Description("Il faut que la méthode extrait la saison du titre.")]
        public void DoitExtraireLaSaisonDuTitreDuTorrent_When_SaisonEstMajuscule()
        {
            #region ARRANGE

            string titreSeries = "Frontier.S01E01.FASTSUB.VOSTFR.HDTV.x264-otm";

            #endregion

            #region ACT

            ShowFileParser parser = new ShowFileParser();
            var resultParser = parser.GetShow(titreSeries, "fakeName", 0);

            #endregion

            #region ASSERT

            Assert.AreEqual(01, resultParser.Saison);

            #endregion
        }

        [TestMethod]
        [TestProperty("MoviesLib", "ShowFileParser")]
        [Description("Il faut que la méthode extrait la saison du titre.")]
        public void DoitExtraireLaSaisonDuTitreDuTorrent_When_SaisonEstMinuscule()
        {
            #region ARRANGE

            string titreSeries = "[VOSTFR] Frontier.2016.s01.720p.NF.WEBRip.DD5.1.x264-RTN";

            #endregion

            #region ACT

            ShowFileParser parser = new ShowFileParser();
            var resultParser = parser.GetShow(titreSeries, "fakeName", 0);

            #endregion

            #region ASSERT

            Assert.AreEqual(01, resultParser.Saison);

            #endregion
        }

        #endregion

        #region Episode

        [TestMethod]
        [TestProperty("MoviesLib", "ShowFileParser")]
        [Description("Il faut extraire le numéro de l'épisode du titre.")]
        public void ExtraireNumeroEpisodeDuTitre_When_EpisodeEstMinuscule()
        {
            #region ARRANGE

            string titreSeries = "[VOSTFR] Frontier.2016.s22e14.720p.NF.WEBRip.DD5.1.x264-RTN";

            #endregion

            #region ACT

            ShowFileParser parser = new ShowFileParser();
	        var resultParser = parser.GetShow(titreSeries, "fakeName", 0);

            #endregion

            #region ASSERT

            Assert.AreEqual(22, resultParser.Saison);
            Assert.AreEqual(14, resultParser.Episode);

            #endregion
        }

        [TestMethod]
        [TestProperty("MoviesLib", "ShowFileParser")]
        [Description("Il faut extraire le numéro de l'épisode du titre.")]
        public void ExtraireNumeroEpisodeDuTitre_When_EpisodeEstMajuscule()
        {
            #region ARRANGE

            string titreSeries = "[VOSTFR] Frontier.2016.s01.E14.720p.NF.WEBRip.DD5.1.x264-RTN";

            #endregion

            #region ACT

            ShowFileParser parser = new ShowFileParser();
            var resultParser = parser.GetShow(titreSeries, "fakeName", 0);

            #endregion

            #region ASSERT

            Assert.AreEqual(14, resultParser.Episode);
            Assert.AreEqual(1, resultParser.Saison);

            #endregion
        }

        #endregion

        #region Resolution

        [TestMethod]
        [TestProperty("MoviesLib", "ShowFileParser")]
        [Description("Il faut etraire la résolution du titre.")]
        public void ExtraireResolutionDuTitre_When_ResolutionEst720p_Then_720p()
        {
            #region ARRANGE

            string titreSeries = "[VOSTFR] Frontier.2016.s01.E14.720p.NF.WEBRip.DD5.1.x264-RTN";

            #endregion

            #region ACT

            ShowFileParser parser = new ShowFileParser();
            var resultParser = parser.GetShow(titreSeries, "fakeName", 0);

            #endregion

            #region ASSERT

            Assert.AreEqual(14, resultParser.Episode);
            Assert.AreEqual(1, resultParser.Saison);
            Assert.AreEqual("720p", resultParser.Resolution);

            #endregion
        }

        [TestMethod]
        [TestProperty("MoviesLib", "ShowFileParser")]
        [Description("Il faut etraire la résolution du titre.")]
        public void ExtraireResolutionDuTitre_When_ResolutionEst1080p_Then_1080p()
        {
            #region ARRANGE

            string titreSeries = "[VOSTFR] Frontier.2016.s01.E14.1080p.NF.WEBRip.DD5.1.x264-RTN";

            #endregion

            #region ACT

            ShowFileParser parser = new ShowFileParser();
            var resultParser = parser.GetShow(titreSeries, "fakeName", 0);

            #endregion

            #region ASSERT

            Assert.AreEqual(14, resultParser.Episode);
            Assert.AreEqual(1, resultParser.Saison);
            Assert.AreEqual("1080p", resultParser.Resolution);

            #endregion
        }

        [TestMethod]
        [TestProperty("MoviesLib", "ShowFileParser")]
        [Description("Il faut etraire la résolution du titre.")]
        public void ExtraireResolutionDuTitre_When_ResolutionEstInconnu_Then_Inconnu()
        {
            #region ARRANGE

            string titreSeries = "[VOSTFR] Frontier.2016.s01.E14.NF.WEBRip.DD5.1.x264-RTN";

            #endregion

            #region ACT

            ShowFileParser parser = new ShowFileParser();
            var resultParser = parser.GetShow(titreSeries, "fakeName", 0);

            #endregion

            #region ASSERT

            Assert.AreEqual(14, resultParser.Episode);
            Assert.AreEqual(1, resultParser.Saison);
            Assert.AreEqual("Inconnu", resultParser.Resolution);

            #endregion
        }

        #endregion

        #region Qualite

        [TestMethod]
        [TestProperty("MoviesLib", "ShowFileParser")]
        [Description("Il faut extraire la qualité du titre.")]
        public void ExtraireQualiteDuTitre_When_QualiteEstWEBRip_Then_WEBRip()
        {
            #region ARRANGE

            string titreSeries = "[VOSTFR] Frontier.2016.s01.E14.720p.NF.WEBRip.DD5.1.x264-RTN";

            #endregion

            #region ACT

            ShowFileParser parser = new ShowFileParser();
            var resultParser = parser.GetShow(titreSeries, "fakeName", 0);

            #endregion

            #region ASSERT

            Assert.AreEqual(14, resultParser.Episode);
            Assert.AreEqual(1, resultParser.Saison);
            Assert.AreEqual("720p", resultParser.Resolution);
            Assert.AreEqual("WEBRip", resultParser.Qualite);

            #endregion
        }

        [TestMethod]
        [TestProperty("MoviesLib", "ShowFileParser")]
        [Description("Il faut extraire la qualité du titre.")]
        public void ExtraireQualiteDuTitre_When_QualiteEstWEBDL_Then_WEBDL()
        {
            #region ARRANGE

            string titreSeries = "Gotham.S03E19.SUBFRENCH.720p.DD5.1.x264-ARK";

            #endregion

            #region ACT

            ShowFileParser parser = new ShowFileParser();
            var resultParser = parser.GetShow(titreSeries, "fakeName", 0);

            #endregion

            #region ASSERT

            Assert.AreEqual(19, resultParser.Episode);
            Assert.AreEqual(3, resultParser.Saison);
            Assert.AreEqual("720p", resultParser.Resolution);
            Assert.AreEqual("Inconnu", resultParser.Qualite);

            #endregion
        }

        [TestMethod]
        [TestProperty("MoviesLib", "ShowFileParser")]
        [Description("Il faut extraire la qualité du titre.")]
        public void ExtraireQualiteDuTitre_When_QualiteEstHDTV_Then_HDTV()
        {
            #region ARRANGE

            string titreSeries = "BrainDead S01E11 FRENCH 1080p HDTV x264-SH0W";

            #endregion

            #region ACT

            ShowFileParser parser = new ShowFileParser();
            var resultParser = parser.GetShow(titreSeries, "fakeName", 0);

            #endregion

            #region ASSERT

            Assert.AreEqual(11, resultParser.Episode);
            Assert.AreEqual(1, resultParser.Saison);
            Assert.AreEqual("1080p", resultParser.Resolution);
            Assert.AreEqual("HDTV", resultParser.Qualite);

            #endregion
        }

        [TestMethod]
        [TestProperty("MoviesLib", "ShowFileParser")]
        [Description("Il faut extraire la qualité du titre.")]
        public void ExtraireQualiteDuTitre_When_QualiteEstDVDRip_Then_DVDRip()
        {
            #region ARRANGE

            string titreSeries = "Miranda S02 e04 FRENCH LD DVDRip XviD MiND";

            #endregion

            #region ACT

            ShowFileParser parser = new ShowFileParser();
            var resultParser = parser.GetShow(titreSeries, "fakeName", 0);

            #endregion

            #region ASSERT

            Assert.AreEqual(4, resultParser.Episode);
            Assert.AreEqual(2, resultParser.Saison);
            Assert.AreEqual("Inconnu", resultParser.Resolution);
            Assert.AreEqual("DVDRip", resultParser.Qualite);

            #endregion
        }

        [TestMethod]
        [TestProperty("MoviesLib", "ShowFileParser")]
        [Description("Il faut extraire la qualité du titre.")]
        public void ExtraireQualiteDuTitre_When_QualiteEstBluray_Then_Bluray()
        {
            #region ARRANGE

            string titreSeries = "Prison Break S03E56 Integrale Bluray 1080p Multi HDMA AC3";

            #endregion

            #region ACT

            ShowFileParser parser = new ShowFileParser();
            var resultParser = parser.GetShow(titreSeries, "fakeName", 0);

            #endregion

            #region ASSERT

            Assert.AreEqual(56, resultParser.Episode);
            Assert.AreEqual(3, resultParser.Saison);
            Assert.AreEqual("1080p", resultParser.Resolution);
            Assert.AreEqual("Bluray", resultParser.Qualite);

            #endregion
        }

        [TestMethod]
        [TestProperty("MoviesLib", "ShowFileParser")]
        [Description("Il faut extraire la qualité du titre.")]
        public void ExtraireQualiteDuTitre_When_QualiteEstInconnu_Then_Inconnu()
        {
            #region ARRANGE

            string titreSeries = "The Expanse S02 E15 720p h264/AVC AAC VOSTFR Fast";

            #endregion

            #region ACT

            ShowFileParser parser = new ShowFileParser();
            var resultParser = parser.GetShow(titreSeries, "fakeName", 0);

            #endregion

            #region ASSERT

            Assert.AreEqual(15, resultParser.Episode);
            Assert.AreEqual(2, resultParser.Saison);
            Assert.AreEqual("720p", resultParser.Resolution);
            Assert.AreEqual("Inconnu", resultParser.Qualite);

            #endregion
        }


        [TestMethod]
        [TestProperty("MoviesLib", "ShowFileParser")]
        [Description("Il faut extraire la qualité du titre.")]
        public void ExtraireQualiteDuTitre_When_QualiteEstHDLight_Then_HDLight()
        {
            #region ARRANGE

            string titreSeries = "Twin.Peaks.S1.E02.MULTi.720p.HDLight.x265-TRUNKD";

            #endregion

            #region ACT

            ShowFileParser parser = new ShowFileParser();
            var resultParser = parser.GetShow(titreSeries, "fakeName", 0);

            #endregion

            #region ASSERT

            Assert.AreEqual(2, resultParser.Episode);
            Assert.AreEqual(1, resultParser.Saison);
            Assert.AreEqual("720p", resultParser.Resolution);
            Assert.AreEqual("HDLight", resultParser.Qualite);

            #endregion
        }

        #endregion

        #region Titre

        [TestMethod]
        [TestProperty("MoviesLib", "ShowFileParser")]
        [Description("Il faut extraire la qualité du titre.")]
        public void ExtraireNomDeSerieDuTitre()
        {
            #region ARRANGE

            string titreSeries = "Twin.Peaks.S1.E02.MULTi.720p.HDLight.x265-TRUNKD";

            #endregion

            #region ACT

            ShowFileParser parser = new ShowFileParser();
            var resultParser = parser.GetShow(titreSeries, "fakeName", 0);

            #endregion

            #region ASSERT

            Assert.AreEqual(2, resultParser.Episode);
            Assert.AreEqual(1, resultParser.Saison);
            Assert.AreEqual("720p", resultParser.Resolution);
            Assert.AreEqual("HDLight", resultParser.Qualite);
            Assert.AreEqual("Twin Peaks", resultParser.Titre);

            #endregion
        }

        [TestMethod]
        [TestProperty("MoviesLib", "ShowFileParser")]
        [Description("Il faut extraire la qualité du titre.")]
        public void ExtraireNomDeSerieDuTitre_OuLeTitreEstAuMilieu()
        {
            #region ARRANGE

            string titreSeries = "12.Monkeys.S03E05.Causality.FASTSUB.VOSTFR.1080p.AAC.2.0.x264.GuS2SuG";

            #endregion

            #region ACT

            ShowFileParser parser = new ShowFileParser();
            var resultParser = parser.GetShow(titreSeries, "fakeName", 0);

            #endregion

            #region ASSERT

            Assert.AreEqual(5, resultParser.Episode);
            Assert.AreEqual(3, resultParser.Saison);
            Assert.AreEqual("1080p", resultParser.Resolution);
            Assert.AreEqual("Inconnu", resultParser.Qualite);
            Assert.AreEqual("12 Monkeys", resultParser.Titre);

            #endregion
        }

        #endregion

        #region Langues

        [TestMethod]
        [TestProperty("MoviesLib", "ShowFileParser")]
        [Description("Extraire la langue du Torrent - FRENCH.")]
        public void ExtraireLangueDuTitre_When_French_Then_French()
        {
            #region ARRANGE

            string titreSeries = "Miranda S02 e04 FRENCH LD DVDRip XviD MiND";

            #endregion

            #region ACT

            ShowFileParser parser = new ShowFileParser("FRENCH");
            var resultParser = parser.GetShow(titreSeries, "fakeName", 0);

            #endregion

            #region ASSERT

            Assert.AreEqual(4, resultParser.Episode);
            Assert.AreEqual(2, resultParser.Saison);
            Assert.AreEqual("Inconnu", resultParser.Resolution);
            Assert.AreEqual("DVDRip", resultParser.Qualite);
            Assert.AreEqual("FRENCH", resultParser.Langage);
            Assert.AreEqual("Miranda", resultParser.Titre);

            #endregion
        }

        [TestMethod]
        [TestProperty("MoviesLib", "ShowFileParser")]
        [Description("Extrait la langue du titre - VOSTFR.")]
        public void ExtraireLangueDuTitre_When_VoSousTitreFrench_Then_VOSTFR()
        {
            #region ARRANGE

            string titreSeries = "Frontier.S01E01.FASTSUB.VOSTFR.HDTV.x264-otm";

            #endregion

            #region ACT

            ShowFileParser parser = new ShowFileParser("VOSTFR");
            var resultParser = parser.GetShow(titreSeries, "fakeName", 0);

            #endregion

            #region ASSERT

            Assert.AreEqual(01, resultParser.Saison);
            Assert.AreEqual(1, resultParser.Episode);
            Assert.AreEqual("Inconnu", resultParser.Resolution);
            Assert.AreEqual("HDTV", resultParser.Qualite);
            Assert.AreEqual("VOSTFR", resultParser.Langage);
            Assert.AreEqual("Frontier", resultParser.Titre);

            #endregion
        }



        [TestMethod]
        [TestProperty("MoviesLib", "ShowFileParser")]
        [Description("Extrait la langue du titre avec plusieurs langues choisit- VOSTFR, FRENCH.")]
        public void ExtraireLangueDuTitre_When_VoSousTitreFrench_And_FrenchIsSelected_Then_FRENCH()
        {
            #region ARRANGE

            string titreSeries = "Crazy.Ex-Girlfriend.S02E06.FRENCH.720p.DD5.x264-otm";

            #endregion

            #region ACT

            ShowFileParser parser = new ShowFileParser("VOSTFR", "FRENCH");
            var resultParser = parser.GetShow(titreSeries, "fakeName", 0);

            #endregion

            #region ASSERT

            Assert.AreEqual(2, resultParser.Saison);
            Assert.AreEqual(6, resultParser.Episode);
            Assert.AreEqual("720p", resultParser.Resolution);
            Assert.AreEqual("Inconnu", resultParser.Qualite);
            Assert.AreEqual("FRENCH", resultParser.Langage);
            Assert.AreEqual("Crazy Ex Girlfriend", resultParser.Titre);

            #endregion
        }

        [TestMethod]
        [TestProperty("MoviesLib", "ShowFileParser")]
        [Description("Extrait la langue du titre avec plusieurs langues choisit - VOSTFR, FRENCH. Et en " +
                    "il y a des minuscules et Majuscule.")]
        public void ExtraireLangueDuTitre_When_LangueContientMinuscule_And_Majuscule_Then_FRENCH()
        {
            #region ARRANGE

            string titreSeries = "Crazy.Ex-Girlfriend.S02E06.French.720p.DD5.x264-otm";

            #endregion

            #region ACT

            ShowFileParser parser = new ShowFileParser("VOSTFR", "french");
            var resultParser = parser.GetShow(titreSeries, "fakeName", 0);

            #endregion

            #region ASSERT

            Assert.AreEqual(2, resultParser.Saison);
            Assert.AreEqual(6, resultParser.Episode);
            Assert.AreEqual("720p", resultParser.Resolution);
            Assert.AreEqual("Inconnu", resultParser.Qualite);
            Assert.AreEqual("FRENCH", resultParser.Langage);
            Assert.AreEqual("Crazy Ex Girlfriend", resultParser.Titre);

            #endregion
        }

		#endregion

		#region Test sur des échecs particuliers

		[TestMethod]
		[TestProperty("MoviesLib", "ShowFileParser")]
		[Description("Extraire la langue du Torrent - FRENCH.")]
		public void TestSurSerie_When_ContientDesCrochetsDansLeTitre_Then_EnleverLesCrochets()
		{
			#region ARRANGE

			string titreSeries = "[ Torrent9.red ] Greys.Anatomy.S14E12.FRENCH.HDTVXviD-ZT.avi";

			#endregion

			#region ACT

			ShowFileParser parser = new ShowFileParser("FRENCH");
			var resultParser = parser.GetShow(titreSeries, "fakeName", 0);

			#endregion

			#region ASSERT

			Assert.AreEqual(12, resultParser.Episode);
			Assert.AreEqual(14, resultParser.Saison);
			Assert.AreEqual("Inconnu", resultParser.Resolution);
			Assert.AreEqual("Inconnu", resultParser.Qualite);
			Assert.AreEqual("FRENCH", resultParser.Langage);
			Assert.AreEqual("Greys Anatomy", resultParser.Titre);

			#endregion
		}

		#endregion
	}
}
