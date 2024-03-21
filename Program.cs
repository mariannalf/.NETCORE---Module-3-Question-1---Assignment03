using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.Json;
using System.Xml.Serialization;

partial class Program
{
    static void Main(string[] args)
    {
       List<Category> categories = QueryCategories();
        List<Product> products = QueryProducts();

        string jsonSerialized = SerializeToJson(categories, products);
        string xmlSerialized = SerializeToXml(categories, products);
        string binarySerialized = SerializeToBinary(categories, products);

        int jsonSize = Encoding.UTF8.GetByteCount(jsonSerialized);
        int xmlSize = Encoding.UTF8.GetByteCount(xmlSerialized);
        int binarySize = binarySerialized.Length; 

        Console.WriteLine("Serialization Size (bytes):");
        Console.WriteLine($"JSON: {jsonSize} bytes");
        Console.WriteLine($"XML: {xmlSize} bytes");
        Console.WriteLine($"Binary: {binarySize} bytes");

        List<(string, int)> rankings = new List<(string, int)> {
            ("JSON", jsonSize),
            ("XML", xmlSize),
            ("Binary", binarySize)
        };
        rankings.Sort((x, y) => x.Item2.CompareTo(y.Item2));

        Console.WriteLine("\nRanking (from smallest to largest):");
        for (int i = 0; i < rankings.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {rankings[i].Item1}");
        }
    }

    static List<Category> QueryCategories()
    {
        return new List<Category>(); 
    }

    static List<Product> QueryProducts()
    {
        return new List<Product>(); 
    }

    static string SerializeToJson(List<Category> categories, List<Product> products)
    {
        return JsonSerializer.Serialize(new { Categories = categories, Products = products });
    }

    static string SerializeToXml(List<Category> categories, List<Product> products)
    {
        var serializer = new XmlSerializer(typeof(List<Category>));
        using (var writer = new StringWriter())
        {
            serializer.Serialize(writer, categories);
            return writer.ToString();
        }
    }

    static string SerializeToBinary(List<Category> categories, List<Product> products)
    {
        using (var stream = new MemoryStream())
        {
#pragma warning disable SYSLIB0011
            var formatter = new BinaryFormatter(); 
            formatter.Serialize(stream, categories);
            formatter.Serialize(stream, products);
            return Convert.ToBase64String(stream.ToArray());
#pragma warning restore SYSLIB0011
        }
        // SECURITY RISK - Binary Serialization is no longer a method considered safe and has being added to this
        // project just to fullfil the assingment requirements.
        // https://learn.microsoft.com/en-us/dotnet/core/compatibility/serialization/8.0/binaryformatter-disabled

    }
}

 