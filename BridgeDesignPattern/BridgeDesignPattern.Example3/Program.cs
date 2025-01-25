
Music music = new() { Name = "Zalim", Singer = "Sezen Aksu" };

Device computer = new Computer(new Speaker(), new Spotify());
computer.PlayMusic(music);

computer = new Computer(new HeadPhone(), new Spotify());
computer.PlayMusic(music);

Device phone = new Phone(new Speaker(), new Fizy());
phone.PlayMusic(music);

phone = new Phone(new HeadPhone(), new Fizy());
phone.PlayMusic(music);

public class Music
{
    public string Name { get; set; }
    public string Singer { get; set; }
}

#region Abstraction
public abstract class Device
{
    protected IOutputDevice _outputDevice;
    protected IMusicPlayer _musicPlayer;

    public Device(IOutputDevice outputDevice, IMusicPlayer musicPlayer)
    {
        _outputDevice = outputDevice;
        _musicPlayer = musicPlayer;
    }

    public virtual void PlayMusic(Music music)
    {
        _musicPlayer.PlayMusic(music);
        _outputDevice.MakeSound();
    }
}
#endregion

#region Refined Abstraction
public class Computer : Device
{
    public Computer(IOutputDevice outputDevice, IMusicPlayer musicPlayer) : base(outputDevice, musicPlayer)
    {
    }

    public override void PlayMusic(Music music)
    {
        Console.WriteLine($"The {nameof(Computer)} device is being used.");
        base.PlayMusic(music);
    }
}

public class Phone : Device
{
    public Phone(IOutputDevice outputDevice, IMusicPlayer musicPlayer) : base(outputDevice, musicPlayer)
    {
    }

    public override void PlayMusic(Music music)
    {
        Console.WriteLine($"The {nameof(Phone)} device is being used.");
        base.PlayMusic(music);
    }
}
#endregion

#region Implementor
public interface IOutputDevice
{
    void MakeSound();
}

public interface IMusicPlayer
{
    void PlayMusic(Music music);
}
#endregion

#region Concrete Implementor
public class Spotify : IMusicPlayer
{
    public void PlayMusic(Music music)
        => Console.WriteLine($"{music.Singer} - {music.Name} is playing on {nameof(Spotify)}.");
}

public class Fizy : IMusicPlayer
{
    public void PlayMusic(Music music)
        => Console.WriteLine($"{music.Singer} - {music.Name} is playing on {nameof(Fizy)}.");
}

public class Speaker : IOutputDevice
{
    public void MakeSound()
        => Console.WriteLine($"Sound comes out of {nameof(Speaker)}.\n");
}

public class HeadPhone : IOutputDevice
{
    public void MakeSound()
        => Console.WriteLine($"Sound comes out of {nameof(HeadPhone)}.\n");
}
#endregion