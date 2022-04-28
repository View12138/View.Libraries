using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using View.AutoTask.Core;

namespace View.AutoTask.Caches
{
    /// <summary>
    /// 默认的内存缓存方案
    /// <para>等于没有使用缓存</para>
    /// </summary>
    public sealed class DefaultMemonyMetadataCache : IMetadataCache
    {
        private static byte[] _metadata;

        public void Write(byte[] metadata)
        {
            _metadata = metadata;
        }

        public byte[] Read()
        {
            return _metadata ?? new byte[0];
        }

        public void Clear()
        {
            _metadata = null;
        }
    }
}
