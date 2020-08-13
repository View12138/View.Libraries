using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlTypes;

namespace View.VisualStudio.TextTemplating
{
    /// <summary>
    /// 文本模板
    /// </summary>
    public abstract class TextTemplate
    {
        StringBuilder sb = new StringBuilder();

        /// <summary>
        /// 向模板中写入一行文本。
        /// </summary>
        /// <param name="str"></param>
        public void WriteLine(string str) => sb.Append($"{str}{Environment.NewLine}");

        /// <summary>
        /// 向模板中写入空行。
        /// </summary>
        public void WriteLine() => sb.Append($"{Environment.NewLine}");

        /// <summary>
        /// 代码转换。
        /// <para>在此方法内部实现代码转换的具体步骤。</para>
        /// </summary>
        public abstract void Transform(bool haveEnum = false);

        /// <summary>
        /// 写入文档注释
        /// </summary>
        /// <param name="comment">注释文本</param>
        protected void WriteComment(string comment)
        {
            if (!string.IsNullOrEmpty(comment))
            {
                string[] _comments = new string[2];
                if (comment.Contains("<para>")) { comment = comment.Replace("<para>", "\n"); }
                if (comment.Contains("</para>")) { comment = comment.Replace("</para>", ""); }
                if (comment.Contains("enum")) { comment = comment.Replace("enum", ""); }

                if (comment.Contains("\n")) { _comments = comment.Split2('\n'); }
                else if (comment.Contains("。")) { _comments = comment.Split2('。'); }
                else if (comment.Contains(".")) { _comments = comment.Split2('.'); }
                else if (comment.Contains(" ")) { _comments = comment.Split2(' '); }
                else { _comments[0] = comment; _comments[1] = string.Empty; }

                WriteLine($"        /// <summary>");
                WriteLine($"        /// {_comments[0]}");
                if (!string.IsNullOrWhiteSpace(_comments[1]))
                { WriteLine($"        /// <para>{_comments[1]}</para>"); }
                WriteLine($"        /// </summary>");
            }
        }

        /// <summary>
        /// 将此模板内容转换为模板
        /// </summary>
        /// <returns></returns>
        public override string ToString() => sb.ToString();
    }
}
