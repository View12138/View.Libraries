using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace View.VisualStudio.TextTemplating.DbFirst
{
    /* 获取字段信息的 SQL 语句：
     * select column_name as `Name`,column_type as `SqlType`,column_key as `KeyState`,privileges as `Privileges`,
     *        is_nullable as IsNullable,column_default as `Default`,column_comment as `Comment`,extra as `Extra` 
     * from information_schema.columns 
     * where table_schema='view.b2r' and table_name='drugs' 
     * order by ordinal_position asc;
     */

    /// <summary>
    /// 字段信息。
    /// </summary>
    public class ColumnInfo
    {
        /// <summary>
        /// 字段名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 字段类型
        /// </summary>
        public string SqlType { get; set; }
        /// <summary>
        /// 键状态
        /// <para>主键: PRI</para>
        /// </summary>
        public string KeyState { get; set; }
        /// <summary>
        /// 字段权限
        /// <para>select,insert,update,references</para>
        /// </summary>
        public string Privileges { get; set; }
        /// <summary>
        /// 是否可为空
        /// <para>YES / NO</para>
        /// </summary>
        public string IsNullable { get; set; }
        /// <summary>
        /// 默认值
        /// </summary>
        public string Default { get; set; }
        /// <summary>
        /// 注释
        /// </summary>
        public string Comment { get; set; }
        /// <summary>
        /// 附加信息
        /// <para>自增：auto_increment</para>
        /// </summary>
        public string Extra { get; set; }

        public override string ToString() => $"Column: Name={Name}, SqlType={SqlType}, KeyState={KeyState}, Privileges={Privileges}, IsNullable={IsNullable}, Default={Default}, Comment={Comment}, Extra={Extra}";
    }
}
