using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Microsoft.VisualStudio.TextTemplating;
using MySql.Data.MySqlClient;
using View.Data.Sql;
using View.Data.Sql.ConnectConfigs;

namespace View.VisualStudio.TextTemplating.DbFirst.MySql
{
    public class MySqlEntityBuilder : IEntityBuilder
    {
        private readonly ITextTemplatingEngineHost _host;
        private readonly string _namespaces;
        private IDbConnection _db;
        private List<TableInfo> Tables = new List<TableInfo>();
        private List<MySqlTemplate> Templates = new List<MySqlTemplate>();


        public MySqlEntityBuilder(ITextTemplatingEngineHost host, string namespaces, string database, string user, string password, Uri uri = null)
        {
            this._host = host;
            this._namespaces = namespaces;
            _db = new MySqlConnection(new MySqlConfig(database, user, password, uri).ToString());
        }
        public void Build()
        {
            string baseDir = Path.GetDirectoryName(_host.TemplateFile);
            string coreDir = Path.Combine(baseDir, "Core"); // 核心文件夹，包括各种扩展的实体
            string entityDir = Path.Combine(baseDir, "Entity"); // t4 模板生成的实体文件夹
            string viewDir = Path.Combine(baseDir, "View"); // 视图文件夹
            string dtoDir = Path.Combine(baseDir, "Dto"); // 数据传输实体文件夹

            try { Directory.Delete(entityDir, true); } catch { }
            if (!Directory.Exists(coreDir)) { Directory.CreateDirectory(coreDir); }
            if (!Directory.Exists(entityDir)) { Directory.CreateDirectory(entityDir); }
            if (!Directory.Exists(viewDir)) { Directory.CreateDirectory(viewDir); }
            if (!Directory.Exists(dtoDir)) { Directory.CreateDirectory(dtoDir); }

            Tables = GetTables(_db);
            for (int i = 0; i < Tables.Count; i++)
            {
                var columns = GetTablesColumns(_db, Tables[i]);
                for (int j = 0; j < columns.Count; j++)
                {
                    if (columns[j].SqlType.Contains("("))
                    { columns[j].SqlType = columns[j].SqlType.Substring(0, columns[j].SqlType.IndexOf("(")); }
                }
                Tables[i].Columns = columns;
            }
            Templates = GetEntityTemplates(Tables);
            foreach (var template in Templates)
            {

                List<string> enumNames = new List<string>();
                template.Table.Columns.ForEach((col) =>
                {
                    if (col.Comment.Replace(" ", "").Contains("{enum") && TypeConverter.SqlTypeToType(col.SqlType) == typeof(short))
                    {
                        try
                        {
                            string @enum = col.Comment.Substring(col.Comment.IndexOf("{") + 1, col.Comment.IndexOf("}") - col.Comment.IndexOf("{") - 1);
                            string[] enums = @enum.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            enumNames.Add(enums[1]);
                        }
                        catch { }
                    }
                });

                template.Transform(enumNames.Any());

                foreach (var enumName in enumNames)
                {
                    EnumTemplate enumTemplate = new EnumTemplate(enumName, template.Table.NameSpaces);
                    enumTemplate.Transform();
                    string enumFileName = Path.Combine(coreDir, $"{enumName}.cs");
                    if (!File.Exists(enumFileName))
                    { File.WriteAllText(enumFileName, enumTemplate.ToString()); }
                }
                string[] _filenames = Regex.Replace(template.Table.TableName, "[ \\[ \\] \\^ \\-_*×――(^)$%~!@#$…&%￥—+=<>《》!！??？:：•`·、。，；,.;\"‘’“”-]", " ")
                    .Split(' ');


                string fileName = "";
                foreach (var _filename in _filenames)
                { fileName += Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(_filename); }
                if (fileName.EndsWith("s"))
                { fileName = fileName.Remove(fileName.Length - 1); }
                File.WriteAllText(Path.Combine(entityDir, $"{fileName}.cs"), template.ToString());
            }
        }

        private List<TableInfo> GetTables(IDbConnection db, List<string> filterTableNames = null)
        {
            var filter = filterTableNames ?? new List<string>();

            string dbName = db.Database;
            string sql = $"show full tables from `{dbName}` where Table_type = 'BASE TABLE'";

            if (filter.Any())
            { sql = $"show full tables from `{dbName}` where Table_type = 'BASE TABLE' and Tables_in_{dbName} in ('{string.Join("','", filter)}')"; }

            List<string> _tables = db.Query<string>(sql).AsList();

            List<TableInfo> tables = new List<TableInfo>();
            db.Query<string>(sql).AsList().ForEach((_t) => { tables.Add(new TableInfo() { TableName = _t, NameSpaces = _namespaces }); });
            return tables;
        }

        private List<ColumnInfo> GetTablesColumns(IDbConnection db, TableInfo tables)
        {
            var sql = $@"select column_name as `Name`,column_type as `SqlType`,column_key as `KeyState`,privileges as `Privileges`,
is_nullable as IsNullable,column_default as `Default`,column_comment as `Comment`,extra as `Extra` from information_schema.columns 
where table_schema='{db.Database}' and table_name='{tables.TableName}' order by ordinal_position asc;";

            return db.Query<ColumnInfo>(sql).ToList();
        }

        private List<MySqlTemplate> GetEntityTemplates(List<TableInfo> tables)
        {
            List<MySqlTemplate> templates = new List<MySqlTemplate>();
            foreach (TableInfo table in tables)
            {
                templates.Add(new MySqlTemplate(table));
            }
            return templates;
        }
    }

    public static class MySqlEntityBuilderExtension
    {
        public static MySqlEntityBuilder BuildEntityFromMySql(this TextTransformation transformation, ITextTemplatingEngineHost host,
            string namespaces, string database, string user, string password, string uri)
        {
            return new MySqlEntityBuilder(host, namespaces, database, user, password, new Uri(uri));
        }
    }
}
