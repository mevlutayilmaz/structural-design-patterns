
TV tv = new();
SoundSystem soundSystem = new();
DVDPlayer dvdPlayer = new();

HomeTheaterFacade homeTheater = new(tv, soundSystem, dvdPlayer);

homeTheater.WatchMovie(new Movie() { Name = "Inception", Director = "Christopher Nolan" });

homeTheater.EndMovie();

public class Movie
{
    public string Name { get; set; }
    public string Director { get; set; }
}

#region Facade
public class TV
{
    public void TurnOn()
        => Console.WriteLine("TV turned on.");
    public void TurnOff()
        => Console.WriteLine("TV turned off.");
}

public class SoundSystem
{
    public void TurnOn()
        => Console.WriteLine("Sound system turned on.");
    public void SetVolume(int level)
        => Console.WriteLine($"Volume level is set to {level}.");
    public void TurnOff()
        => Console.WriteLine("Sound system turned off.");
}

public class DVDPlayer
{
    public void TurnOn()
        => Console.WriteLine("DVD Player turned on.");
    public void PlayMovie(Movie movie)
        => Console.WriteLine($"Playing movie '{movie.Name}'.");
    public void TurnOff()
        => Console.WriteLine("DVD Player turned off.");
}
#endregion

#region Subsystem
public class HomeTheaterFacade
{
    protected TV _tv;
    protected SoundSystem _soundSystem;
    protected DVDPlayer _dvdPlayer;

    public HomeTheaterFacade(TV tv, SoundSystem soundSystem, DVDPlayer dvdPlayer)
    {
        _tv = tv;
        _soundSystem = soundSystem;
        _dvdPlayer = dvdPlayer;
    }

    public void WatchMovie(Movie movie)
    {
        Console.WriteLine("\n🎬 Turning on Movie Mode...");
        _tv.TurnOn();
        _soundSystem.TurnOn();
        _soundSystem.SetVolume(8);
        _dvdPlayer.TurnOn();
        _dvdPlayer.PlayMovie(movie);
    }

    public void EndMovie()
    {
        Console.WriteLine("\n📴 Turning off Movie Mode...");
        _dvdPlayer.TurnOff();
        _soundSystem.TurnOff();
        _tv.TurnOff();
    }
}
#endregion