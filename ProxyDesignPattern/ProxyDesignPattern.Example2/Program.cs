
IImage image = new ProxyImage("example.jpg");

Console.WriteLine("Image is not loaded yet.");

image.Display();
Console.WriteLine("\nImage loaded.");

image.Display();

#region Subject
public interface IImage
{
    void Display();
}
#endregion

#region Real Subject
public class RealImage : IImage
{
    private readonly string _fileName;

    public RealImage(string fileName)
    {
        _fileName = fileName;
        LoadImageFromDisk();
    }

    private void LoadImageFromDisk()
    {
        Console.WriteLine($"Loading image: {_fileName} from disk...");
    }

    public void Display()
    {
        Console.WriteLine($"Displaying image: {_fileName}");
    }
}
#endregion

#region Proxy
public class ProxyImage : IImage
{
    private string _fileName;
    private RealImage _realImage;

    public ProxyImage(string fileName)
    {
        _fileName = fileName;
    }

    public void Display()
    {
        if (_realImage == null)
        {
            _realImage = new RealImage(_fileName);
        }

        _realImage.Display();
    }
}
#endregion