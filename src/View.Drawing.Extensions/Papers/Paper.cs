namespace View.Drawing.Extensions.Papers
{
    /// <summary>
    /// 表示纸张的参数
    /// </summary>
    public struct Paper
    {
        /// <summary>
        /// 表示 <see langword="A0"/> 纸张的实际尺寸。
        /// </summary>
        public static PaperSize A0 { get; } = new PaperSize(841, 1189);
        /// <summary>
        /// 表示 <see langword="A1"/> 纸张的实际尺寸。
        /// </summary>
        public static PaperSize A1 { get; } = new PaperSize(594, 841);
        /// <summary>
        /// 表示 <see langword="A2"/> 纸张的实际尺寸。
        /// </summary>
        public static PaperSize A2 { get; } = new PaperSize(420, 594);
        /// <summary>
        /// 表示 <see langword="A3"/> 纸张的实际尺寸。
        /// </summary>
        public static PaperSize A3 { get; } = new PaperSize(294, 420);
        /// <summary>
        /// 表示 <see langword="A4"/> 纸张的实际尺寸。
        /// </summary>
        public static PaperSize A4 { get; } = new PaperSize(210, 297);
        /// <summary>
        /// 表示 <see langword="A5"/> 纸张的实际尺寸。
        /// </summary>
        public static PaperSize A5 { get; } = new PaperSize(148, 210);
        /// <summary>
        /// 表示 <see langword="A6"/> 纸张的实际尺寸。
        /// </summary>
        public static PaperSize A6 { get; } = new PaperSize(105, 148);
        /// <summary>
        /// 表示 <see langword="A7"/> 纸张的实际尺寸。
        /// </summary>
        public static PaperSize A7 { get; } = new PaperSize(74, 105);
        /// <summary>
        /// 表示 <see langword="A8"/> 纸张的实际尺寸。
        /// </summary>
        public static PaperSize A8 { get; } = new PaperSize(52, 74);
        /// <summary>
        /// 表示 <see langword="A9"/> 纸张的实际尺寸。
        /// </summary>
        public static PaperSize A9 { get; } = new PaperSize(37, 52);
        /// <summary>
        /// 表示 <see langword="A10"/> 纸张的实际尺寸。
        /// </summary>
        public static PaperSize A10 { get; } = new PaperSize(26, 37);

        /// <summary>
        /// 表示 <see langword="B0"/> 纸张的实际尺寸。
        /// </summary>
        public static PaperSize B0 { get; } = new PaperSize(1000, 1414);
        /// <summary>
        /// 表示 <see langword="B1"/> 纸张的实际尺寸。
        /// </summary>
        public static PaperSize B1 { get; } = new PaperSize(707, 1000);
        /// <summary>
        /// 表示 <see langword="B2"/> 纸张的实际尺寸。
        /// </summary>
        public static PaperSize B2 { get; } = new PaperSize(500, 707);
        /// <summary>
        /// 表示 <see langword="B3"/> 纸张的实际尺寸。
        /// </summary>
        public static PaperSize B3 { get; } = new PaperSize(353, 500);
        /// <summary>
        /// 表示 <see langword="B4"/> 纸张的实际尺寸。
        /// </summary>
        public static PaperSize B4 { get; } = new PaperSize(250, 353);
        /// <summary>
        /// 表示 <see langword="B5"/> 纸张的实际尺寸。
        /// </summary>
        public static PaperSize B5 { get; } = new PaperSize(176, 250);
        /// <summary>
        /// 表示 <see langword="B6"/> 纸张的实际尺寸。
        /// </summary>
        public static PaperSize B6 { get; } = new PaperSize(125, 176);
        /// <summary>
        /// 表示 <see langword="B7"/> 纸张的实际尺寸。
        /// </summary>
        public static PaperSize B7 { get; } = new PaperSize(88, 125);
        /// <summary>
        /// 表示 <see langword="B8"/> 纸张的实际尺寸。
        /// </summary>
        public static PaperSize B8 { get; } = new PaperSize(62, 88);
        /// <summary>
        /// 表示 <see langword="B9"/> 纸张的实际尺寸。
        /// </summary>
        public static PaperSize B9 { get; } = new PaperSize(44, 62);
        /// <summary>
        /// 表示 <see langword="B10"/> 纸张的实际尺寸。
        /// </summary>
        public static PaperSize B10 { get; } = new PaperSize(31, 44);

        /// <summary>
        /// 表示 <see langword="C0"/> 纸张的实际尺寸。
        /// </summary>
        public static PaperSize C0 { get; } = new PaperSize(917, 1297);
        /// <summary>
        /// 表示 <see langword="C1"/> 纸张的实际尺寸。
        /// </summary>
        public static PaperSize C1 { get; } = new PaperSize(648, 917);
        /// <summary>
        /// 表示 <see langword="C2"/> 纸张的实际尺寸。
        /// </summary>
        public static PaperSize C2 { get; } = new PaperSize(458, 648);
        /// <summary>
        /// 表示 <see langword="C3"/> 纸张的实际尺寸。
        /// </summary>
        public static PaperSize C3 { get; } = new PaperSize(324, 458);
        /// <summary>
        /// 表示 <see langword="C4"/> 纸张的实际尺寸。
        /// </summary>
        public static PaperSize C4 { get; } = new PaperSize(229, 324);
        /// <summary>
        /// 表示 <see langword="C5"/> 纸张的实际尺寸。
        /// </summary>
        public static PaperSize C5 { get; } = new PaperSize(162, 229);
        /// <summary>
        /// 表示 <see langword="C6"/> 纸张的实际尺寸。
        /// </summary>
        public static PaperSize C6 { get; } = new PaperSize(114, 162);
        /// <summary>
        /// 表示 <see langword="C7"/> 纸张的实际尺寸。
        /// </summary>
        public static PaperSize C7 { get; } = new PaperSize(81, 114);
        /// <summary>
        /// 表示 <see langword="C8"/> 纸张的实际尺寸。
        /// </summary>
        public static PaperSize C8 { get; } = new PaperSize(57, 81);
        /// <summary>
        /// 表示 <see langword="DL"/> 纸张的实际尺寸。
        /// </summary>
        public static PaperSize DL { get; } = new PaperSize(110, 220);
        /// <summary>
        /// 表示 <see langword="C7/6"/> 纸张的实际尺寸。
        /// </summary>
        public static PaperSize C7_6 { get; } = new PaperSize(81, 162);
    }
}
