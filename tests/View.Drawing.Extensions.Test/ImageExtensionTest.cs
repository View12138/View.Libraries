using System.Drawing;
using System.Drawing.Imaging;
using Xunit;
using Xunit.Abstractions;

namespace View.Drawing.Extensions.Test
{
    public class ImageExtensionTest
    {
        protected readonly ITestOutputHelper Output;
        public ImageExtensionTest(ITestOutputHelper tempOutput) => Output = tempOutput;

        // test image path
        private readonly string path24 = @"View.Logo.24.jpg";
        private readonly string path32 = @"View.Logo.32.png";

        [Fact]
        public void ChangeSizeTest()
        {
            Image image24 = Image.FromFile(path24);
            try
            {
                Size minSize = new Size(64, 60);
                var newMinImg = image24.ChangeSize(minSize, new ChangeSizeConfiguration() { Reserve = true, SizeMode = Models.SizeMode.ToOut });
                Output.WriteLine(newMinImg.Size.ToString());
                Assert.True(newMinImg.Size.Width >= minSize.Width && newMinImg.Height >= minSize.Height);

                Size maxSize = new Size(256, 250);
                var newMaxImg = image24.ChangeSize(maxSize, new ChangeSizeConfiguration() { Reserve = true, SizeMode = Models.SizeMode.ToIn });
                Output.WriteLine(newMaxImg.Size.ToString());
                Assert.True(newMaxImg.Size.Width <= maxSize.Width && newMaxImg.Height <= maxSize.Height);
            }
            finally
            {
                image24.Dispose();
            }
        }

        [Fact]
        public void ChangePixelFormatTest()
        {
            bool hasError = false;
            Image image24 = Image.FromFile(path24);
            Image image32 = Image.FromFile(path32);
            try
            {
                foreach (PixelFormat pixel in System.Enum.GetValues(typeof(PixelFormat)))
                {
                    if (!ImageHandle.IsErrorPixelFormat(pixel))
                    {
                        using (var temp = image24.ChangePixelFormat(pixel, new ChangeSizeConfiguration() { Reserve = true }))
                        {
                            try
                            {
                                Assert.True(temp.PixelFormat == pixel);
                            }
                            catch (Xunit.Sdk.TrueException)
                            {
                                hasError = true;
                                Output.WriteLine($"Change Error Pixel: {pixel}");
                            }
                        }
                        using (var temp = image32.ChangePixelFormat(pixel, new ChangeSizeConfiguration() { Reserve = true }))
                        {
                            try
                            {
                                Assert.True(temp.PixelFormat == pixel);
                            }
                            catch (Xunit.Sdk.TrueException)
                            {
                                hasError = true;
                                Output.WriteLine($"Change Error Pixel: {pixel}");
                            }
                        }
                    }
                }
                if (hasError)
                { Assert.Throws<Xunit.Sdk.TrueException>(() => { }); }
            }
            finally
            {
                image24.Dispose();
                image32.Dispose();
            }
        }
    }
}
