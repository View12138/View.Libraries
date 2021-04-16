using System;
using System.Drawing;
using System.Drawing.Imaging;
using View.Drawing.Extensions.Models;
using Xunit;
using Xunit.Abstractions;

namespace View.Drawing.Extensions.Test
{

    public class ImageHandleTest
    {
        protected readonly ITestOutputHelper Output;

        public ImageHandleTest(ITestOutputHelper tempOutput) => Output = tempOutput;

        [Fact]
        public void ImageEncodersTest()
        {
            foreach (var imageCodec in ImageCodecInfo.GetImageEncoders())
            {
                Output.WriteLine($"{imageCodec.FormatDescription} : {imageCodec.FilenameExtension} | {imageCodec.DllName} | {imageCodec.CodecName}");
            }
        }

        [Fact]
        public void ImageDecoderTest()
        {
            foreach (var imageCodec in ImageCodecInfo.GetImageDecoders())
            {
                Output.WriteLine($"{imageCodec.FormatDescription} : {imageCodec.FilenameExtension} | {imageCodec.DllName} | {imageCodec.CodecName}");
            }
        }

        [Fact]
        public void SizeToSizeTest()
        {
            bool hasError = false;
            Size newSize = new Size(50, 50);
            for (int width = 1; width <= 100; width++)
            {
                for (int height = 1; height <= 100; height++)
                {
                    Size oldSize = new Size(width, height);
                    var destSizeIn = ImageHandle.SizeToSize(oldSize, newSize, SizeMode.ToMax);
                    var destSizeOut = ImageHandle.SizeToSize(oldSize, newSize, SizeMode.ToMin);
                    try
                    {
                        Assert.True(destSizeIn.Width <= newSize.Width);
                        Assert.True(destSizeIn.Height <= newSize.Height);
                        Assert.True(destSizeOut.Width >= newSize.Width);
                        Assert.True(destSizeOut.Height >= newSize.Height);
                    }
                    catch (Xunit.Sdk.TrueException)
                    {
                        hasError = true;
                        string res = $"old:[{oldSize.Width},{oldSize.Height}] - IN:[{destSizeIn.Width},{destSizeIn.Height}] | OUT:[{destSizeOut.Width},{destSizeOut.Height}]";
                        Output.WriteLine(res);
                    }

                }
            }
            if (hasError)
            { Assert.Throws<Xunit.Sdk.TrueException>(() => { }); }
        }

        [Fact]
        public void IsIndexedPixelFormatTest()
        {
            bool hasError = false;
            var values = Enum.GetValues(typeof(PixelFormat));
            foreach (PixelFormat value in values)
            {
                var res = ImageHandle.IsErrorPixelFormat(value);
                try
                {
                    try
                    {
                        using (Bitmap bitmap = new Bitmap(1, 1, value))
                        {
                            using (Graphics g = Graphics.FromImage(bitmap))
                            { g.DrawImage(bitmap, 0, 0); }
                        }
                        Assert.False(res);
                    }
                    catch (Exception)
                    {
                        Assert.True(res);
                    }

                }
                catch (Xunit.Sdk.TrueException)
                {
                    hasError = true;
                    Output.WriteLine($"TruePixelFormat:[{value}]");
                }
                catch (Xunit.Sdk.FalseException)
                {
                    hasError = true;
                    Output.WriteLine($"FalsePixelFormat:[{value}]");
                }
            }
            if (hasError)
            { Assert.Throws<Xunit.Sdk.TrueException>(() => { }); }
        }
    }
}
