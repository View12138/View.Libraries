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
                if (!System.IO.Directory.Exists(@"Out"))
                { System.IO.Directory.CreateDirectory(@"Out"); }

                Size minSizeA = new Size(64, 40);
                BuildImage(image24, minSizeA, nameof(minSizeA));

                Size minSizeB = new Size(40, 64);
                BuildImage(image24, minSizeB, nameof(minSizeB));

                Size maxSizeA = new Size(256, 210);
                BuildImage(image24, maxSizeA, nameof(maxSizeA));

                Size maxSizeB = new Size(210, 256);
                BuildImage(image24, maxSizeB, nameof(maxSizeB));
            }
            finally
            {
                image24.Dispose();
            }
        }

        private void BuildImage(Image image24, Size minSizeA, string groupName)
        {
            var newMinImgToMin = image24.ChangeSize(minSizeA, new Configurations.ChangeSizeConfiguration() { SizeMode = Models.SizeMode.ToMin });
            Output.WriteLine(newMinImgToMin.Size.ToString());
            newMinImgToMin.Save($@"Out\View.Logo.24.{groupName}.ToMin.jpg");
            Assert.True(newMinImgToMin.Size.Width >= minSizeA.Width && newMinImgToMin.Height >= minSizeA.Height);

            var newMinImgToMax = image24.ChangeSize(minSizeA, new Configurations.ChangeSizeConfiguration() { SizeMode = Models.SizeMode.ToMax });
            Output.WriteLine(newMinImgToMax.Size.ToString());
            newMinImgToMax.Save($@"Out\View.Logo.24.{groupName}.ToMax.jpg");
            Assert.True(newMinImgToMax.Size.Width <= minSizeA.Width && newMinImgToMax.Height <= minSizeA.Height);

            var newMinImgStretch = image24.ChangeSize(minSizeA, new Configurations.ChangeSizeConfiguration() { SizeMode = Models.SizeMode.Stretch });
            Output.WriteLine(newMinImgStretch.Size.ToString());
            newMinImgStretch.Save($@"Out\View.Logo.24.{groupName}.Stretch.jpg");
            Assert.True(newMinImgStretch.Size.Width == minSizeA.Width && newMinImgStretch.Height == minSizeA.Height);

            var newMinImgCut1 = image24.ChangeSize(minSizeA, new Configurations.ChangeSizeConfiguration() { SizeMode = Models.SizeMode.Cut, Location = new Point(0, 0) });
            Output.WriteLine(newMinImgCut1.Size.ToString());
            newMinImgCut1.Save($@"Out\View.Logo.24.{groupName}.Cut1.jpg");
            Assert.True(newMinImgCut1.Size.Width == minSizeA.Width && newMinImgCut1.Height == minSizeA.Height);
            var newMinImgCut2 = image24.ChangeSize(minSizeA, new Configurations.ChangeSizeConfiguration() { SizeMode = Models.SizeMode.Cut, Location = new Point(15, 25), ClipInBox = true });
            Output.WriteLine(newMinImgCut2.Size.ToString());
            newMinImgCut2.Save($@"Out\View.Logo.24.{groupName}.Cut2.jpg");
            Assert.True(newMinImgCut2.Size.Width == minSizeA.Width && newMinImgCut2.Height == minSizeA.Height);
            var newMinImgCut3 = image24.ChangeSize(minSizeA, new Configurations.ChangeSizeConfiguration() { SizeMode = Models.SizeMode.Cut, Location = new Point(15, 25), ClipInBox = false });
            Output.WriteLine(newMinImgCut3.Size.ToString());
            newMinImgCut3.Save($@"Out\View.Logo.24.{groupName}.Cut3.jpg");
            Assert.True(newMinImgCut3.Size.Width == minSizeA.Width && newMinImgCut3.Height == minSizeA.Height);
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
                        using (var temp = image24.ChangePixelFormat(pixel))
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
                        using (var temp = image32.ChangePixelFormat(pixel))
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
