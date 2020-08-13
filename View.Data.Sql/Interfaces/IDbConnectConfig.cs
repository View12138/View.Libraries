using System;
using System.Collections.Generic;
using System.Text;

namespace View.Data.Sql.Interfaces
{
    /// <summary>
    /// 数据库连接配置
    /// </summary>
    interface IDbConnectConfig
    {
        /// <summary>
        /// 数据库地址以及端口。
        /// </summary>
        Uri Server { get; set; }
        /// <summary>
        /// 数据库用户名。
        /// </summary>
        string User { get; set; }
        /// <summary>
        /// 数据库密码。
        /// </summary>
        string Password { get; set; }
        /// <summary>
        /// 数据库名。
        /// </summary>
        string Database { get; set; }
        /// <summary>
        /// 数据库连接字符串。
        /// </summary>
        /// <returns></returns>
        string ToString();
    }
}
