# Movies-Parser 
[![NuGet](https://img.shields.io/nuget/v/Ryck.MoviesLib.svg)](https://www.nuget.org/packages/Ryck.MoviesLib)

A library that allows you to do the parsing of video file name to extract the information such as quality, language, resolution, title.

## Nuget

Install from Nuget using the command: **Install-Package Ryck.MoviesLib**
View more about that here: https://www.nuget.org/packages/Ryck.MoviesLib/

## Usage

  string fileTitle = "paddington-2-french-bluray-1080p-2018";
  
  // Choose your language to extract.
  MovieFileParser parser = new MovieFileParser("french", "TRUEFRENCH");
  MovieInformation resultParser = parser.GetInformation(titre, String.Empty, 0, TypeVideo.Movie);

The result : 
  "1080p" = resultParser.Resolution
  "bluray" = resultParser.Qualite
  "2018" = resultParser.Annee
  "french" = resultParser.Langage
  "PADDINGTON 2" = resultParser.Titre