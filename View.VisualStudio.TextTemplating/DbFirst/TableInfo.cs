using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace View.VisualStudio.TextTemplating.DbFirst
{
    /// <summary>
    /// 表信息
    /// </summary>
    public class TableInfo
    {
        /// <summary>
        /// 命名空间
        /// </summary>
        public string NameSpaces { get; set; }
        /// <summary>
        /// 表名
        /// </summary>
        public string TableName { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public List<ColumnInfo> Columns { get; set; } = new List<ColumnInfo>();
    }
}
