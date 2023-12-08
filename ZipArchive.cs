using System.IO.Compression;
using IronZip;

namespace zip_unzip;

public class ZipArchive
{
    const string file1 = ".\\image1.png";
    const string file2 = ".\\image2.png";
    const string file3 = ".\\image3.png";

    static void AddEntry(string filePath, System.IO.Compression.ZipArchive zipArchive)
    {
        var entryName = Path.GetFileName(filePath);
        zipArchive.CreateEntryFromFile(filePath, entryName);
    }

    public static void TestWithSystemIoCompression()
    {
        Console.WriteLine("-----------Zip - Unzip-----------");

        var zipFile = "systemIoCompression.zip";
        using (System.IO.Compression.ZipArchive archive = ZipFile.Open(zipFile, ZipArchiveMode.Create))
        {
            AddEntry(file1, archive);
            AddEntry(file2, archive);
        }

        var dirToExtract = "extract";
        if (Directory.Exists(dirToExtract) == false)
        {
            Directory.CreateDirectory(dirToExtract);
        }

        ZipFile.ExtractToDirectory(zipFile, dirToExtract);
    }

    public static void TestWithIronZip()
    {
        // setup
        var archivePath = "ironZip.zip";
        var archivePlusPath = "ironZipPlus.zip";
        var extractionPath = "IronZipFiles";

        if (File.Exists(archivePath))
        {
            File.Delete(archivePath);
        }

        // zip file
        using (var archive = new IronArchive(archivePath, 9))
        {
            // Add files to the zip
            archive.AddArchiveEntry(file1);
            archive.AddArchiveEntry(file2);
        }

        // extract
        if (Directory.Exists(extractionPath) == false)
        {
            Directory.CreateDirectory(extractionPath);
        }

        IronArchive.ExtractArchiveToDirectory(archivePath, extractionPath);

        //add to existing archive
        using (var archive = IronArchive.FromFile(archivePath, archivePlusPath))
        {
            // Add files
            archive.AddArchiveEntry(file3);
        }
    }
}