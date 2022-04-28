using View.AutoTask.Caches;
using View.AutoTask.Core;

namespace View.AutoTask.Options
{
    /// <summary>
    /// 任务选项
    /// </summary>
    public class TaskOptions
    {
        /// <summary>
        /// 获取或设置缓存。
        /// <list type="bullet">
        /// <item>默认使用内置的文件缓存</item>
        /// <item>使用内存缓存的效果与不使用缓存相同</item>
        /// </list>
        /// </summary>
        public IMetadataCache Cache { get; set; } = new DefaultFileMetadataCache();

        /// <summary>
        /// 获取或设置缓存写入的格式。
        /// <para>如果产生缓存后重新设置格式，请手动清空缓存</para>
        /// </summary>
        public CacheType CacheType { get; set; } = CacheType.Binary;

        /// <summary>
        /// 获取或设置扫描自动任务的范围
        /// <para>默认扫描范围: <see cref="TaskScope.Assembly"/></para>
        /// </summary>
        public TaskScope TaskScope { get; set; } = TaskScope.Assembly;
    }
}
