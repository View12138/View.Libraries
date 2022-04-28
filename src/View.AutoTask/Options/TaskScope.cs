namespace View.AutoTask.Options
{
    /// <summary>
    /// 扫描自动任务方式的范围选项
    /// </summary>
    public enum TaskScope
    {
        /// <summary>
        /// 自动任务方法必须在 <see cref="TaskAssemblyAttribute"/> 修饰的程序集中且在 <see cref="TaskClassAttribute"/> 修饰的类中且被 <see cref="TaskMethodAttribute"/> 修饰。
        /// </summary>
        Assembly,
        /// <summary>
        /// 自动任务方法必须在 <see cref="TaskClassAttribute"/> 修饰的类中并且 <see cref="TaskMethodAttribute"/> 修饰。
        /// </summary>
        Class,
        /// <summary>
        /// 所有 <see cref="TaskMethodAttribute"/> 修饰的方法都是自动任务
        /// </summary>
        Method,
    }
}
