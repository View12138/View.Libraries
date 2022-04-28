using System;

namespace View.AutoTask
{
    /// <summary>
    /// 包含任务的程序集
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly, Inherited = false, AllowMultiple = false)]
    public sealed class TaskAssemblyAttribute : Attribute
    {
        public TaskAssemblyAttribute()
        {

        }
    }
}
