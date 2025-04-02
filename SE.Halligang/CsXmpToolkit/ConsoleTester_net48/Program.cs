using SE.Halligang.CsXmpToolkit;
using System;

namespace  My
{
    public record FileAndFormat(string FilePath, FileFormat FileFormat)
    {
        public string FilePath { get; } = FilePath;
        public FileFormat FileFormat { get; } = FileFormat;
    }

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

            xmp.SerializeToBuffer(out var bla, SerializeFlags.EncodeUTF8, 0);

            Console.WriteLine(bla);
        }
    }
}

