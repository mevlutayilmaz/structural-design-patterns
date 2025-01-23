using System.Text;
using System.Text.Json;
using System.Xml;
using System.Xml.Serialization;

var exporters = new Dictionary<ExportTypes, IProductsExporter>
{
    { ExportTypes.Json, new JsonProductsExporter() },
    { ExportTypes.Xml, new XmlProductsExporter() },
    { ExportTypes.Csv, new CsvProductsExporterAdapter(new CsvHelper(';')) }
};

Console.WriteLine("Please select one:");

Console.WriteLine(
    exporters
        .Select((e, i) => $"{i + 1}. {e.Key}")
        .Aggregate((x, y) => x + Environment.NewLine + y));

var key = Console.ReadLine()!;

var hasKey = exporters.TryGetValue(Enum.Parse<ExportTypes>(key), out var productExporter);

if (!hasKey) return;

var products = new List<Product>
{
    new()
    {
        Title = "MacBook Pro 14 inch",
        Description = "It's now even more capable, thanks to the new M3 chip."
    },
    new()
    {
        Title = "MacBook Pro 16 inch",
        Description = "It's now even more capable, thanks to the new M3 chip."
    }
};

var data = productExporter.Export(products);

Console.WriteLine(data);


public class Product
{
    public string Title { get; set; }
    public string Description { get; set; }
}

#region Target
public interface IProductsExporter
{
    string Export(List<Product> products);
}
#endregion

public class XmlProductsExporter : IProductsExporter
{
    public string Export(List<Product> products)
    {
        var xmlSerializer = new XmlSerializer(products.GetType());

        using var stringWriter = new StringWriter();
        using var xmlWriter = new XmlTextWriter(stringWriter) { Formatting = Formatting.Indented };
        xmlSerializer.Serialize(xmlWriter, products);

        return stringWriter.ToString();
    }
}

public class JsonProductsExporter : IProductsExporter
{
    public string Export(List<Product> products)
    {
        return JsonSerializer.Serialize(products);
    }
}

#region Adaptee
public class CsvHelper
{
    private readonly char _delimiter;

    public CsvHelper(char delimiter)
    {
        _delimiter = delimiter;
    }

    public string GenerateCsv(List<object> rows)
    {
        if (rows == null) return string.Empty;

        var objectType = rows.FirstOrDefault()!.GetType();
        var headers = objectType.GetProperties().Select(x => x.Name).ToList();

        var stringBuilder = new StringBuilder();
        stringBuilder.AppendLine(string.Join(_delimiter, headers));

        foreach (var row in rows)
        {
            var values = objectType.GetProperties().Select(prop => prop.GetValue(row)).ToList();
            stringBuilder.AppendLine(string.Join(_delimiter, values));
        }

        return stringBuilder.ToString();
    }
}
#endregion

#region Adapter
public class CsvProductsExporterAdapter : IProductsExporter
{
    private readonly CsvHelper _csvHelper;

    public CsvProductsExporterAdapter(CsvHelper csvHelper)
    {
        _csvHelper = csvHelper;
    }

    public string Export(List<Product> data)
    {
        return _csvHelper.GenerateCsv(data.Cast<object>().ToList());
    }
}
#endregion

public enum ExportTypes
{
    Json = 1,
    Xml = 2,
    Csv = 3
}