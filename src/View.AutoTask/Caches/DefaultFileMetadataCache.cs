using System;
using System.IO;
using View.AutoTask.Core;

namespace View.AutoTask.Caches
{
    /// <summary>
    /// 默认的文件缓存方案
    /// </summary>
    public class DefaultFileMetadataCache : IMetadataCache
    {
        protected virtual string FileName => Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
            Default.Options.CacheType == Options.CacheType.Text ? "view_autotask.vatt" : "view_autotask.vatb");

        public void Write(byte[] metadata)
        {
            File.WriteAllBytes(FileName, metadata);
        }

        public byte[] Read()
        {
            if (File.Exists(FileName))
            {
                return File.ReadAllBytes(FileName);
            }
            else
            { return new byte[0]; }
        }

        public void Clear()
        {
            if (File.Exists(FileName))
            { File.Delete(FileName); }
        }
    }
}
