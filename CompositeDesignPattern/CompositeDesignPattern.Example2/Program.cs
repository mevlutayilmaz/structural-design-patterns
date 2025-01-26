File file1 = new() { Name = "file1.txt" , Size = 1024};
File file2 = new() { Name = "file2.jpg" , Size = 2048};
File file3 = new() { Name = "file3.docx" , Size = 512};

Folder folder1 = new() { Name = "Folder1" };
folder1.Add(file1);

Folder folder2 = new() { Name = "Folder2" };
folder2.Add(file1);
folder2.Add(file2);
folder2.Add(folder1);

Folder folder3 = new() { Name = "Folder3" };
folder3.Add(file1);
folder3.Add(file2);
folder3.Add(file3);
folder3.Add(folder1);
folder3.Add(folder2);

folder1.Display(0);
Console.WriteLine();
folder2.Display(0);
Console.WriteLine();
folder3.Display(0);


#region Component
public interface IFileSystemComponent
{
    string Name { get; set;  }
    long GetSize();
    void Display(int depth);
}
#endregion

#region Leaf
public class File : IFileSystemComponent
{
    public string Name { get; set; }
    public long Size { get; set; }

    public void Display(int depth)
        => Console.WriteLine($"{new string(' ', depth)}- File: {Name}, Size: {Size} byte");

    public long GetSize() => Size;
}
#endregion

#region Composite
public class Folder : IFileSystemComponent
{
    public string Name { get; set; }
    protected List<IFileSystemComponent> _children = new();

    public void Add(IFileSystemComponent component) => _children.Add(component);
    public void Remove(IFileSystemComponent component) => _children.Remove(component);

    public void Display(int depth)
    {
        Console.WriteLine($"{new string(' ', depth)}- Folder: {Name}");
        foreach(IFileSystemComponent component in _children)
            component.Display(depth + 2);
    }

    public long GetSize()
    {
        long totalSize = 0;
        foreach (IFileSystemComponent component in _children)
            totalSize += component.GetSize();

        return totalSize;
    }
}
#endregion