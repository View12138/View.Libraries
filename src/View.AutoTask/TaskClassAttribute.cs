using System;

namespace View.AutoTask
{
    /// <summary>
    /// 包含任务的类。
    /// <para>所有任务均以类为组织方式。</para>
    /// <para>任务以无参无返回值的公共静态方法为单元。</para>
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class TaskClassAttribute : Attribute
    {
        public TaskClassAttribute()
        {

        }
    }
}
