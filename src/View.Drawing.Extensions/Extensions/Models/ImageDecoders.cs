using System;
using System.Collections.Generic;
using System.Text;

namespace View.Drawing.Extensions.Models
{
    /// <summary>
    /// 解码器名称
    /// </summary>
    public enum ImageDecoders
    {
        /// <summary>
        /// <see langword="BMP"/> 格式
        /// <para>*.BMP;*.DIB;*.RLE</para>
        /// </summary>
        BMP = 1,
        /// <summary>
        /// <see langword="JPEG"/> 格式
        /// <para>*.JPG;*.JPEG;*.JPE;*.JFIF</para>
        /// </summary>
        JPEG = 2,
        /// <summary>
        /// <see langword="GIF"/> 格式
        /// <para>*.GIF</para>
        /// </summary>
        GIF = 3,
        /// <summary>
        /// <see langword="EMF"/> 格式
        /// <para>*.EMF</para>
        /// </summary>
        EMF = 4,
        /// <summary>
        /// <see langword="WMF"/> 格式
        /// <para>*.WMF</para>
        /// </summary>
        WMF = 5,
        /// <summary>
        /// <see langword="TIFF"/> 格式
        /// <para>*.TIF;*.TIFF</para>
        /// </summary>
        TIFF = 6,
        /// <summary>
        /// <see langword="PNG"/> 格式
        /// <para>*.PNG</para>
        /// </summary>
        PNG = 7,
        /// <summary>
        /// <see langword="ICO"/> 格式
        /// <para>*.ICO</para>
        /// </summary>
        ICO = 8,
    }
}
