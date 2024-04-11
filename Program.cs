using System.IO.Compression;
using SharpCompress.Archives;
using SharpCompress.Archives.Rar;
using SharpCompress.Writers;


internal class Program
{
    private static void Main(string[] args)
    {
        if (args.Length != 1)
        {
            Console.WriteLine("Usage: <program> <archive>");
            return;
        }
        var fileName = args[0];
        Console.WriteLine("Extracting " + fileName);
        var memStram = new MemoryStream();
        var dirId = "./" + Guid.NewGuid().ToString();
        using (var archive = RarArchive.Open(fileName))
        {
            archive.ExtractToDirectory(dirId);
        }

        using (var arch2 = SharpCompress.Archives.Zip.ZipArchive.Create())
        {
            arch2.AddAllFromDirectory(dirId);
            arch2.SaveTo(fileName + ".epub", new WriterOptions(SharpCompress.Common.CompressionType.Deflate));
        }
        Directory.Delete(dirId, true);
    }
}