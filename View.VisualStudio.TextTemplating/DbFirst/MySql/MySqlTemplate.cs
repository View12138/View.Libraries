using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using View.Data.Sql;

namespace View.VisualStudio.TextTemplating.DbFirst.MySql
{
    /// <summary>
    /// MySql 实体模板。
    /// </summary>
    public class MySqlTemplate : TextTemplate
    {
        /// <summary>
        /// 获取或设置 Mysql 表
        /// </summary>
        public TableInfo Table { get; set; }

        /// <summary>
        /// 初始化一个表实体模板。
        /// </summary>
        /// <param name="table">表</param>
        public MySqlTemplate(TableInfo table) { Table = table; }

        /// <summary>
        /// 将表转换为实体类。
        /// </summary>
        public override void Transform(bool haveEnum = false)
        {
            string[] _classNames = Regex.Replace(Table.TableName, "[ \\[ \\] \\^ \\-_*×――(^)$%~!@#$…&%￥—+=<>《》!！??？:：•`·、。，；,.;\"‘’“”-]", " ")
                .Split(' ');
            string className = "";
            foreach (var _className in _classNames)
            { className += Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(_className); }
            if (className.EndsWith("s"))
            { className = className.Remove(className.Length - 1); }
            if (Table.NameSpaces.ToLower().EndsWith(".entity") ||
                Table.NameSpaces.ToLower().EndsWith(".entities"))
            { Table.NameSpaces = Table.NameSpaces.Substring(0, Table.NameSpaces.LastIndexOf(".")); }

            WriteLine($"using System;");
            WriteLine($"using Dapper.Contrib.Extensions;");
            WriteLine($"using View.Data.Sql.Models;");
            if (haveEnum)
            { WriteLine($"using {Table.NameSpaces}.Core;"); }
            WriteLine();
            WriteLine($"// 此文件由系统自动生成，请勿修改。");
            WriteLine();
            WriteLine($"namespace {Table.NameSpaces}.Entity");
            WriteLine($"{{");
            WriteLine($"    [Table(\"{Table.TableName}\")]");
            WriteLine($"    public class {className} : IEntity");
            WriteLine($"    {{");

            foreach (ColumnInfo col in Table.Columns)
            {
                base.WriteComment(col.Comment);

                if (col.Comment.Replace(" ", "").Contains("{enum") && TypeConverter.SqlTypeToType(col.SqlType) == typeof(short))
                {
                    try
                    {
                        string @enum = col.Comment.Substring(col.Comment.IndexOf("{") + 1, col.Comment.IndexOf("}") - col.Comment.IndexOf("{") - 1);
                        string[] enums = @enum.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        WriteLine($"        public {enums[1]} {col.Name} " + "{ get; set; }");
                        continue;
                    }
                    catch { }
                }

                if (col.KeyState == "PRI")
                { WriteLine($"        [Key] public {TypeConverter.SqlTypeToType(col.SqlType).Name} {col.Name} " + "{ get; set; }"); }
                else
                { WriteLine($"        public {TypeConverter.SqlTypeToType(col.SqlType).Name} {col.Name} " + "{ get; set; }"); }
            }
            WriteLine($"    }}");
            WriteLine($"}}");
        }
    }

    /// <summary>
    /// 枚举类型模板
    /// </summary>
    public class EnumTemplate : TextTemplate
    {
        private readonly string _enumName = string.Empty;
        private string _nameSpace;

        /// <summary>
        /// 初始化一个枚举模板。
        /// </summary>
        /// <param name="enumName">枚举名</param>
        /// <param name="nameSpace">命名空间</param>
        public EnumTemplate(string enumName, string nameSpace)
        {
            if (string.IsNullOrWhiteSpace(enumName))
            { throw new ArgumentException("message", nameof(enumName)); }

            if (string.IsNullOrWhiteSpace(nameSpace))
            { throw new ArgumentException("message", nameof(nameSpace)); }

            _enumName = enumName;
            _nameSpace = nameSpace;
        }

        /// <summary>
        /// 枚举转换器
        /// </summary>
        /// <param name="haveEnum"></param>
        public override void Transform(bool haveEnum = false)
        {
            if (_nameSpace.ToLower().EndsWith(".entity") ||
                _nameSpace.ToLower().EndsWith(".entities"))
            { _nameSpace = _nameSpace.Substring(0, _nameSpace.LastIndexOf(".")); }

            WriteLine($"using System;");
            WriteLine($"using System.ComponentModel;");
            WriteLine();
            WriteLine($"// 请自行定制此枚举类型。枚举文件一旦生成便不会再被自动修改。");
            WriteLine();
            WriteLine($"namespace {_nameSpace}.Core");
            WriteLine($"{{");
            WriteLine($"    public enum {_enumName} : short");
            WriteLine($"    {{");
            WriteLine($"        /// <summary>");
            WriteLine($"        /// 未定义的");
            WriteLine($"        /// </summary>");
            WriteLine($"        [Description(\"未定义的\")]");
            WriteLine($"        Unworked = 0,");
            WriteLine($"    }}");
            WriteLine($"}}");
            WriteLine();
        }
    }
}
