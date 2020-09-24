using System;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using Dapper.Contrib.Extensions;
using Newtonsoft.Json;
using View.Core.Models;
using View.Drawing.Extensions;
using View.Drawing.Extensions.Models;
using View.Data.Sql.Extensions;
using View.Core.Attributes;

namespace View.Console.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var db = BuildDB();

            db.CreateTable<Employee>();


            System.Console.WriteLine("OK");
            System.Console.ReadKey();
        }
        public static IDbConnection BuildDB()
        {
            string dbName = "client.db";

            return new SQLiteConnection($@"Data Source={dbName};Version={3};");
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



    [Table("userbook_user")]
    public class Employee : Entity
    {
        [JsonProperty("id")]
        [ExplicitKey]
        [Length(19)]
        public long Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("phone")]
        public string Phone { get; set; }
        [JsonProperty("sex")]
        [Default(SexType.Male)]
        public SexType Sex { get; set; }
        [JsonProperty("nick")]
        public string NickName { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("password")]
        public string Password { get; set; }
        [JsonProperty("passwordsalt")]
        public string PasswordSalt { get; set; }
        [JsonProperty("departmentid")]
        public long DepartmentId { get; set; }
        [JsonProperty("role")]
        public int Role { get; set; }
        [JsonProperty("birthday")]
        public DateTime Birthday { get; set; }
        [JsonProperty("positionid")]
        public long PositionId { get; set; }
        [JsonProperty("areaid")]
        public long AreaId { get; set; }
        [JsonProperty("createdate")]
        public DateTime CreateTime { get; set; }
        [JsonProperty("isdeleted")]
        public bool IsDeleted { get; set; }
        [JsonProperty("isdisabled")]
        public bool IsDisabled { get; set; }
        [JsonProperty("state")]
        public UserStatus State { get; set; }
    }



    /// <summary>
    /// 性别
    /// </summary>
    public enum SexType : int
    {
        /// <summary>女性</summary>
        [Description("女")] Female = 0,
        /// <summary>男性</summary>
        [Description("男")] Male = 1,
    }
    /// <summary>
    /// 任务类别
    /// </summary>
    public enum TaskType
    {
        /// <summary>未知</summary>
        [Description("未知")] Unknown = -1,
        /// <summary>全过程</summary>
        [Description("全过程")] Process,
        /// <summary>招标代理</summary>
        [Description("招标代理")] InvTender,
        /// <summary>业务投标</summary>
        [Description("业务投标")] SubTender,
        /// <summary>通用</summary>
        [Description("通用")] Normal,
    }

    /// <summary>
    /// 就职状态
    /// </summary>
    public enum UserStatus
    {
        /// <summary>离职</summary>
        [Description("离职")] LeaveWork,
        /// <summary>在职</summary>
        [Description("在职")] InWork,
        /// <summary>停职</summary>
        [Description("停职")] StopWork,
    }
    /// <summary>
    /// 账户类型
    /// </summary>
    public enum AccountType
    {
        /// <summary>未知</summary>
        [Description("未知")] Unknown,
        /// <summary>项目负责人</summary>
        [Description("项目负责人")] ProjectAdmin,
        /// <summary>任务负责人</summary>
        [Description("任务负责人")] TaskAdmin,
        /// <summary>业务人员</summary>
        [Description("业务人员")] Agent,
    }
}
