using System.IO;
using View.AutoTask.Core;

namespace View.AutoTask
{
    /// <summary>
    /// 缓存方式
    /// </summary>
    public interface IMetadataCache
    {
        /// <summary>
        /// 将当前任务元数据写入缓存
        /// </summary>
        /// <param name="metadata">任务元数据</param>
        void Write(byte[] metadata);

        /// <summary>
        /// 从缓存中读取任务元数据
        /// </summary>
        /// <returns>任务元数据</returns>
        public byte[] Read();

        /// <summary>
        /// 清空当前任务元数据
        /// </summary>
        public void Clear();

        public static IMetadataCache DefaultMemonyCache => new MemonyMetadataCache();
        public static IMetadataCache DefaultFileCache => new FileMetadataCache();
    }
}
