using SE.Halligang.CsXmpToolkit;

namespace ConsoleTester;

public record FileAndFormat(string FilePath, FileFormat FileFormat);

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");

        XmpFiles.Initialize();
        XmpCore.Initialize();

        var xmp = new XmpCore();

        var jpgFile = new FileAndFormat(@"C:\Users\juraj.ahel\source\repos\DataArchivalHelper\test_files\BlackSquare.jpg", FileFormat.Jpeg);
        var pngFile = new FileAndFormat(@"C:\Users\juraj.ahel\source\repos\DataArchivalHelper\test_files\BlackSquare.png", FileFormat.Png);

        var file = jpgFile;

        var xmpFile = new XmpFiles(file.FilePath, file.FileFormat, OpenFlags.OpenForRead);
        xmpFile.GetXmp(xmp);

        xmp.SerializeToBuffer(out var buffer, SerializeFlags.EncodeUTF8, 2);
        
        Console.WriteLine(buffer);
    }
}