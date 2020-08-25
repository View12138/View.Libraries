using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using View.Drawing.Extensions;
using View.Drawing.Extensions.Models;

namespace View.Console.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Bitmap bitmap = (Bitmap)Image.FromFile("C:/Users/yangw/OneDrive/图片/Camera Roll/IMG_20190131_141623.jpg");
            Image image = Image.FromFile("C:/Users/yangw/OneDrive/图片/Camera Roll/IMG_20190131_141623.jpg");
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            image.InverseColor().Save("123.jpg");
            stopwatch.Stop();
            System.Console.WriteLine(stopwatch.Elapsed.Milliseconds + " ms");

            HH.AA1.ToDescription();

            System.Console.ReadKey();
        }

        public enum HH
        {
            [Description("asdf")] AA1,
            [Description("aa2")] AA2,
            [Description("aa3")] AA3,
        }
    }

    public static class e
    {
        /// <summary>
        /// 获取枚举类型的描述。
        /// </summary>
        /// <param name="enum"></param>
        /// <returns>枚举描述信息</returns>
        public static string ToDescription(this Enum @enum)
        {
            Type type = @enum.GetType();
            MemberInfo[] memInfo = type.GetMember(@enum.ToString());
            if (null != memInfo && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (null != attrs && attrs.Length > 0)
                    return ((DescriptionAttribute)attrs[0]).Description;
            }
            return "";
        }
    }
}
