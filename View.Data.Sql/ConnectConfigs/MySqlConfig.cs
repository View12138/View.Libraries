using System;
using View.Core.Extensions;
using View.Data.Sql.Interfaces;

namespace View.Data.Sql.ConnectConfigs
{
    /// <summary>
    /// <see langword="MySql"/> 数据库配置
    /// </summary>
    public class MySqlConfig : IDbConnectConfig
    {
        /// <summary>
        /// 数据库地址以及端口。
        /// </summary>
        public Uri Server { get; set; }
        /// <summary>
        /// 数据库用户名。
        /// </summary>
        public string User { get; set; }
        /// <summary>
        /// 数据库密码。
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 数据库名。
        /// </summary>
        public string Database { get; set; }

        /// <summary>
        /// 初始化数据库配置
        /// </summary>
        /// <param name="database">数据库名</param>
        public MySqlConfig(string database) : this(database, string.Empty, string.Empty, null) { }
        /// <summary>
        /// 初始化数据库配置
        /// </summary>
        /// <param name="database">数据库名</param>
        /// <param name="user">数据库用户名</param>
        public MySqlConfig(string database, string user) : this(database, user, string.Empty, null) { }
        /// <summary>
        /// 初始化数据库配置
        /// </summary>
        /// <param name="database">数据库名</param>
        /// <param name="user">数据库用户名</param>
        /// <param name="password">数据库密码</param>
        public MySqlConfig(string database, string user, string password) : this(database, user, password, null) { }
        /// <summary>
        /// 初始化数据库配置
        /// </summary>
        /// <param name="database">数据库名</param>
        /// <param name="user">数据库用户名</param>
        /// <param name="server">数据库地址以及端口</param>
        public MySqlConfig(string database, string user, Uri server) : this(database, user, string.Empty, server) { }
        /// <summary>
        /// 初始化数据库配置
        /// </summary>
        /// <param name="database">数据库名</param>
        /// <param name="user">数据库用户名</param>
        /// <param name="password">数据库密码</param>
        /// <param name="server">数据库地址以及端口</param>
        public MySqlConfig(string database, string user, string password, Uri server)
        {
            if (string.IsNullOrEmpty(database)) { throw new ArgumentException("message", nameof(database)); }

            Server = server ?? new Uri("https://127.0.0.1:3306");
            User = user.IsNullOrWhiteSpace() ? "root" : user;
            Password = password;
            Database = database;
        }

        /// <summary>
        /// 返回 <see langword="MySql"/> 数据库连接字段。
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string _server = $"server={Server.Host};";
            string _port = $"port={Server.Port};";
            string _user = $"user={User};";
            string _password = Password.IsNullOrWhiteSpace() ? "" : $"password={Password};";
            string _database = $"database={Database};";
            return $"{_server}{_port}{_user}{_password}{_database}";
        }
    }
}
