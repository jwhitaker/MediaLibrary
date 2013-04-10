using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TheWhitakers.MediaLibrary.Models
{
    public class MediaLibraryContextDbInitializer : DropCreateDatabaseIfModelChanges<MediaLibraryContext>
    {
        protected override void Seed(MediaLibraryContext context)
        {
            var genreAction = context.Genres.Add(new Genre() { Name = "Action" });	
            var genreAdventure = context.Genres.Add(new Genre() { Name = "Adventure" });	
            var genreAnimation = context.Genres.Add(new Genre() { Name = "Animation" });	
            var genreBiography = context.Genres.Add(new Genre() { Name = "Biography" });
            var genreComedy = context.Genres.Add(new Genre() { Name = "Comedy" });	
            var genreCrime = context.Genres.Add(new Genre() { Name = "Crime" });	
            var genreDocumentry = context.Genres.Add(new Genre() { Name = "Documentary" });	
            var genreDrama = context.Genres.Add(new Genre() { Name = "Drama" });
            var genreFamily = context.Genres.Add(new Genre() { Name = "Family" });	
            var genreFantasy = context.Genres.Add(new Genre() { Name = "Fantasy" });	
            var genreFilmNoir = context.Genres.Add(new Genre() { Name = "Film-Noir" });	
            var genreGameShow = context.Genres.Add(new Genre() { Name = "Game-Show" });
            var genreHistory = context.Genres.Add(new Genre() { Name = "History" });	
            var genreHorror = context.Genres.Add(new Genre() { Name = "Horror" });	
            var genreMusic = context.Genres.Add(new Genre() { Name = "Music" });	
            var genreMusical = context.Genres.Add(new Genre() { Name = "Musical" });
            var genreMystery = context.Genres.Add(new Genre() { Name = "Mystery" });	
            var genreNews = context.Genres.Add(new Genre() { Name = "News" });	
            var genreRealityTV = context.Genres.Add(new Genre() { Name = "Reality-TV" });	
            var genreRomance = context.Genres.Add(new Genre() { Name = "Romance" });
            var genreSciFi = context.Genres.Add(new Genre() { Name = "Sci-Fi" });	
            var genreSport = context.Genres.Add(new Genre() { Name = "Sport" });	
            var genreTalkShow = context.Genres.Add(new Genre() { Name = "Talk-Show" });	
            var genreThriller = context.Genres.Add(new Genre() { Name = "Thriller" });
            var genreWar = context.Genres.Add(new Genre() { Name = "War" });	
            var genreWestern = context.Genres.Add(new Genre() { Name = "Western" });

            context.SaveChanges();


            context.Movies.Add(new Movie() { Title = "The Untouchables", YearReleased = "1987", Genres = new List<Genre> { genreCrime, genreDrama, genreHistory } });
            context.Movies.Add(new Movie() { Title = "The Shawshank Redemption", YearReleased = "1994", Genres = new List<Genre> { genreCrime, genreDrama } });
            context.Movies.Add(new Movie() { Title = "The Godfather", YearReleased = "1972", Genres = new List<Genre> { genreCrime, genreDrama } });
            context.Movies.Add(new Movie() { Title = "The Godfather: Part II", YearReleased = "1974", Genres = new List<Genre> { genreCrime, genreDrama } });
            context.Movies.Add(new Movie() { Title = "Pulp Fiction", YearReleased = "1994", Genres = new List<Genre> { genreCrime, genreThriller } });
            context.Movies.Add(new Movie() { Title = "The Dark Knight", YearReleased = "2008", Genres = new List<Genre> { genreAction, genreCrime, genreDrama } });
            context.Movies.Add(new Movie() { Title = "Schindler's List", YearReleased = "1993", Genres = new List<Genre> { genreBiography, genreDrama, genreHistory } });

            context.SaveChanges();
        }
    }
}