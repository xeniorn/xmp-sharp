using System.Runtime.ExceptionServices;
using Microsoft.VisualBasic;
using SE.Halligang.CsXmpToolkit;

namespace CsXmpToolkitNetStandard.Test;

public record FileAndFormat(string FilePath, FileFormat FileFormat);

public class UnitTest1
{
    [Fact]
    public void TestJpg()
    {
        XmpFiles.Initialize();
        XmpCore.Initialize();

        var xmp = new XmpCore();

        var jpgFile = new FileAndFormat(@"C:\Users\juraj.ahel\source\repos\DataArchivalHelper\test_files\BlackSquare.jpg", FileFormat.Jpeg);
        var pngFile = new FileAndFormat(@"C:\Users\juraj.ahel\source\repos\DataArchivalHelper\test_files\BlackSquare.png", FileFormat.Png);
        
        var file = jpgFile;

        var xmpFile = new XmpFiles(file.FilePath, file.FileFormat, OpenFlags.OpenForRead);

        xmpFile.GetXmp(xmp);
        xmp.SerializeToBuffer(out var text, SerializeFlags.EncodeUTF8, 2);
        Assert.True(text.Length > 0);

    }

    [Fact]
    public void TestPng()
    {
        XmpFiles.Initialize();
        XmpCore.Initialize();

        var xmp = new XmpCore();

        var jpgFile = new FileAndFormat(@"C:\Users\juraj.ahel\source\repos\DataArchivalHelper\test_files\BlackSquare.jpg", FileFormat.Jpeg);
        var pngFile = new FileAndFormat(@"C:\Users\juraj.ahel\source\repos\DataArchivalHelper\test_files\BlackSquare.png", FileFormat.Png);

        var file = pngFile;

        var xmpFile = new XmpFiles(file.FilePath, file.FileFormat, OpenFlags.OpenForRead);
        xmpFile.GetXmp(xmp);
        xmp.SerializeToBuffer(out var text, SerializeFlags.EncodeUTF8, 2);

        Assert.True(text.Length > 0);
    }
}