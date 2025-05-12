using System;

namespace practice
{
    class Projector
    {
        public void TurnOn() => Console.WriteLine("Projector is turned on");
        public void Running() => Console.WriteLine("Projector is running");
        public void TurnOff() => Console.WriteLine("Projector is turned off");
    }

    class DVDPlayer
    {
        public void TurnOn() => Console.WriteLine("DVD Player is turned on");
        public void Running() => Console.WriteLine("DVD Player is running");
        public void TurnOff() => Console.WriteLine("DVD Player is turned off");
    }

    class MovieOn
    {
        public void TurnOn() => Console.WriteLine("Movie is on");
        public void Running() => Console.WriteLine("Movie is running");
        public void TurnOff() => Console.WriteLine("Movie is turned off");
    }

    class HomeTheaterFacade
    {
        private Projector _projector;
        private DVDPlayer _dvdPlayer;
        private MovieOn _movie;

        public HomeTheaterFacade(Projector projector, DVDPlayer dvdPlayer, MovieOn movie)
        {
            _projector = projector;
            _dvdPlayer = dvdPlayer;
            _movie = movie;
        }

        public void WatchMovie(string movie)
        {
            Console.WriteLine($"Watching movie: {movie}");
            _projector.TurnOn();
            _dvdPlayer.TurnOn();
            _movie.TurnOn();
        }

        public void EndMovie()
        {
            Console.WriteLine("Ending movie...");
            _projector.TurnOff();
            _dvdPlayer.TurnOff();
            _movie.TurnOff();
        }
    }

    class Program
    {
        static void Main()
        {
            DVDPlayer dvd = new DVDPlayer();
            Projector projector = new Projector();
            MovieOn movie = new MovieOn();

            HomeTheaterFacade homeTheater = new HomeTheaterFacade(projector, dvd, movie);

            homeTheater.WatchMovie("Avengers");
            homeTheater.EndMovie();
        }
    }
}
