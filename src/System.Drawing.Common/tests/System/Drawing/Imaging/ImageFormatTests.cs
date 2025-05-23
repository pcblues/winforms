﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.Drawing.Imaging.Tests;

public class ImageFormatTests
{
    private static ImageFormat BmpImageFormat { get; } = new(new Guid("b96b3cab-0728-11d3-9d7b-0000f81ef32e"));
    private static ImageFormat EmfImageFormat { get; } = new(new Guid("b96b3cac-0728-11d3-9d7b-0000f81ef32e"));
    private static ImageFormat ExifImageFormat { get; } = new(new Guid("b96b3cb2-0728-11d3-9d7b-0000f81ef32e"));
    private static ImageFormat GifImageFormat { get; } = new(new Guid("b96b3cb0-0728-11d3-9d7b-0000f81ef32e"));
    private static ImageFormat TiffImageFormat { get; } = new(new Guid("b96b3cb1-0728-11d3-9d7b-0000f81ef32e"));
    private static ImageFormat PngImageFormat { get; } = new(new Guid("b96b3caf-0728-11d3-9d7b-0000f81ef32e"));
    private static ImageFormat MemoryBmpImageFormat { get; } = new(new Guid("b96b3caa-0728-11d3-9d7b-0000f81ef32e"));
    private static ImageFormat IconImageFormat { get; } = new(new Guid("b96b3cb5-0728-11d3-9d7b-0000f81ef32e"));
    private static ImageFormat JpegImageFormat { get; } = new(new Guid("b96b3cae-0728-11d3-9d7b-0000f81ef32e"));
    private static ImageFormat WmfImageFormat { get; } = new(new Guid("b96b3cad-0728-11d3-9d7b-0000f81ef32e"));
    private static ImageFormat HeifImageFormat { get; } = new(new Guid("{b96b3cb6-0728-11d3-9d7b-0000f81ef32e}"));
    private static ImageFormat WebpImageFormat { get; } = new(new Guid("{b96b3cb7-0728-11d3-9d7b-0000f81ef32e}"));
    private static ImageFormat CustomImageFormat { get; } = new(new Guid("48749428-316f-496a-ab30-c819a92b3137"));

    public static IEnumerable<object[]> ImageFormatGuidTestData
    {
        get
        {
            yield return new object[] { BmpImageFormat.Guid, ImageFormat.Bmp };
            yield return new object[] { EmfImageFormat.Guid, ImageFormat.Emf };
            yield return new object[] { ExifImageFormat.Guid, ImageFormat.Exif };
            yield return new object[] { GifImageFormat.Guid, ImageFormat.Gif };
            yield return new object[] { TiffImageFormat.Guid, ImageFormat.Tiff };
            yield return new object[] { PngImageFormat.Guid, ImageFormat.Png };
            yield return new object[] { MemoryBmpImageFormat.Guid, ImageFormat.MemoryBmp };
            yield return new object[] { IconImageFormat.Guid, ImageFormat.Icon };
            yield return new object[] { JpegImageFormat.Guid, ImageFormat.Jpeg };
            yield return new object[] { WmfImageFormat.Guid, ImageFormat.Wmf };
#if NET
#pragma warning disable CA1416 // Validate platform compatibility
            yield return new object[] { HeifImageFormat.Guid, ImageFormat.Heif };
            yield return new object[] { WebpImageFormat.Guid, ImageFormat.Webp };
#pragma warning restore CA1416
#endif
            yield return new object[] { new Guid("48749428-316f-496a-ab30-c819a92b3137"), CustomImageFormat };
        }
    }

    public static IEnumerable<object[]> ImageFormatToStringTestData
    {
        get
        {
            yield return new object[] { "Bmp", ImageFormat.Bmp };
            yield return new object[] { "Emf", ImageFormat.Emf };
            yield return new object[] { "Exif", ImageFormat.Exif };
            yield return new object[] { "Gif", ImageFormat.Gif };
            yield return new object[] { "Tiff", ImageFormat.Tiff };
            yield return new object[] { "Png", ImageFormat.Png };
            yield return new object[] { "MemoryBMP", ImageFormat.MemoryBmp };
            yield return new object[] { "Icon", ImageFormat.Icon };
            yield return new object[] { "Jpeg", ImageFormat.Jpeg };
            yield return new object[] { "Wmf", ImageFormat.Wmf };
#if NET
#pragma warning disable CA1416 // Validate platform compatibility
            yield return new object[] { "Heif", ImageFormat.Heif };
            yield return new object[] { "Webp", ImageFormat.Webp };
#pragma warning restore CA1416
#endif
            yield return new object[] { "[ImageFormat: 48749428-316f-496a-ab30-c819a92b3137]", CustomImageFormat };
        }
    }

    public static IEnumerable<object[]> ImageFromFileToStringTestData
    {
        get
        {
            yield return new object[] { Path.Join("bitmaps", "nature24bits.gif"), "Gif" };
            yield return new object[] { Path.Join("bitmaps", "nature24bits.jpg"), "Jpeg" };
            yield return new object[] { Path.Join("bitmaps", "VisualPng.ico"), "Icon" };
            yield return new object[] { Path.Join("bitmaps", "almogaver32bits.tif"), "Tiff" };
        }
    }

    public static IEnumerable<object[]> ImageFormatEqualsTestData
    {
        get
        {
            yield return new object[] { new ImageFormat(new Guid("48749428-316f-496a-ab30-c819a92b3137")), new ImageFormat(new Guid("48749428-316f-496a-ab30-c819a92b3137")), true };
            yield return new object[] { new ImageFormat(new Guid("48749428-316f-496a-ab30-c819a92b3137")), new ImageFormat(new Guid("b96b3cad-0728-11d3-9d7b-0000f81ef32e")), false };
            yield return new object[] { new ImageFormat(new Guid("48749428-316f-496a-ab30-c819a92b3137")), null, false };
            yield return new object[] { new ImageFormat(new Guid("48749428-316f-496a-ab30-c819a92b3137")), new(), false };
        }
    }

    [Theory]
    [MemberData(nameof(ImageFormatGuidTestData))]
    public void Guid_ReturnsExpected(Guid expectedGuid, ImageFormat imageFormat)
    {
        Assert.Equal(expectedGuid, imageFormat.Guid);
    }

    [Theory]
    [MemberData(nameof(ImageFormatToStringTestData))]
    public void ToString_ReturnsExpected(string expected, ImageFormat imageFormat)
    {
        Assert.Equal(expected, imageFormat.ToString());
    }

    [Theory]
    [MemberData(nameof(ImageFromFileToStringTestData))]
    public void Image_RawFormat_ToString(string path, string expected)
    {
        var img = Image.FromFile(path);
        Assert.Equal(expected, img.RawFormat.ToString());
    }

    [Theory]
    [MemberData(nameof(ImageFormatEqualsTestData))]
    public void Equals_Object_ReturnsExpected(ImageFormat imageFormat, object obj, bool result)
    {
        Assert.Equal(result, imageFormat.Equals(obj));
    }

    [Fact]
    public void GetHashCode_Success()
    {
        Guid guid = Guid.NewGuid();
        Assert.Equal(guid.GetHashCode(), new ImageFormat(guid).GetHashCode());
    }
}
