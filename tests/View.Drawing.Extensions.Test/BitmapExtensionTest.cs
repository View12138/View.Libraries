using System.Drawing;
using Xunit;
using Xunit.Abstractions;

namespace View.Drawing.Extensions.Test
{
    public class BitmapExtensionTest
    {
        protected readonly ITestOutputHelper Output;

        public BitmapExtensionTest(ITestOutputHelper tempOutput) => Output = tempOutput;

        [Fact]
        public void To32BGRByteArrayTest()
        {
            string path = $@"C:\Users\yangw\OneDrive\Project\View.local.Packages\Icons\View.Logo.png";
            var img = Image.FromFile(path);
            //bmp.To32BGRByteArray(out _, out _, out _);
        }
    }
}
