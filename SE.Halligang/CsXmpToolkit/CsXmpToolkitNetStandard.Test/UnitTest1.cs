using System.Runtime.ExceptionServices;
using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualBasic;
using SE.Halligang.CsXmpToolkit;
using OpenFlags = SE.Halligang.CsXmpToolkit.OpenFlags;

namespace CsXmpToolkitNetStandard.Test;

public record FileAndFormat(string FilePath, FileFormat FileFormat);

public class UnitTest1
{

    [Fact]
    public void TestConvFromDate()
    {
        XmpCore.Initialize();

        XmpUtils.ConvertToDate("2024-07-12T23:44:04.962", out var date);
    }

    [Fact]
    public void TestFromDateTimeAbs()
    {
        XmpCore.Initialize();

        var dt = new DateTime(2023, 10, 1, 12, 0, 0, DateTimeKind.Utc);
        XmpUtils.ConvertFromDate(dt, out var strValue);
        Assert.Equal("2023-10-01T12:00:00Z", strValue);
    }

    [Fact]
    public void TestFromDateTimeZone()
    {
        XmpCore.Initialize();

        var dt = new DateTimeOffset(2023, 10, 1, 12, 0, 0, new TimeSpan(0,1,0,0));
        XmpUtils.ConvertFromDate(dt, out var strValue);
        Assert.Equal("2023-10-01T12:00:00+01:00", strValue);
    }


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