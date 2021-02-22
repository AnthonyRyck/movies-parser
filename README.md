# Movies-Parser 
[![NuGet](https://img.shields.io/nuget/vpre/Ryck.MoviesLib.svg)](https://www.nuget.org/packages/Ryck.MoviesLib)
[![Downloads](https://img.shields.io/nuget/dt/Ryck.MoviesLib.svg)](https://www.nuget.org/packages/Moq)

A library that allows you to do the parsing of video file name to extract the information such as quality, language, resolution, title.

**Updated to .Net 5**

## Nuget

Install from Nuget using the command: **Install-Package Ryck.MoviesLib**
View more about that here: https://www.nuget.org/packages/Ryck.MoviesLib/

## Usage

```csharp
string fileTitle = "paddington-2-french-bluray-1080p-2018";

// Choose your language to extract.
MovieFileParser parser = new MovieFileParser("french", "TRUEFRENCH");
MovieInformation resultParser = parser.GetInformation(titre, String.Empty, 0, TypeVideo.Movie);
```

The result :
```csharp
"1080p" == resultParser.Resolution;
"bluray" == resultParser.Qualite;
"2018" == resultParser.Annee;
"french" == resultParser.Langage;
"PADDINGTON 2" == resultParser.Titre;
```

Example with a show :

```csharp
string titreSeries = "Crazy.Ex-Girlfriend.S02E06.French.720p.DD5.x264-otm";

ShowFileParser parser = new ShowFileParser("VOSTFR", "french");
var resultParser = parser.GetShow(titreSeries, "fakeName", 0);

// Result
resultParser.Saison == 2;
resultParser.Episode == 6;
resultParser.Resolution == "720p";
resultParser.Qualite == "Inconnu";
resultParser.Langage == "FRENCH";
resultParser.Titre == "Crazy Ex Girlfriend";
```
