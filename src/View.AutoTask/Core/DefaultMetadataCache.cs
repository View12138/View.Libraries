using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace View.AutoTask.Core
{
    internal class MemonyMetadataCache : IMetadataCache
    {
        private static byte[] _metadata;

        public void Write(byte[] metadata)
        {
            _metadata = metadata;
        }

        public byte[] Read()
        {
            return _metadata ?? Array.Empty<byte>();
        }

        public void Clear()
        {
            _metadata = null;
        }
    }

    internal class FileMetadataCache : IMetadataCache
    {
        private readonly static string fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "view.autotask.atmd");

        public void Write(byte[] metadata)
        {
            File.WriteAllBytes(fileName, metadata);
        }

        public byte[] Read()
        {
            if (File.Exists(fileName))
            {
                return File.ReadAllBytes(fileName);
            }
            else
            { return Array.Empty<byte>(); }
        }

        public void Clear()
        {
            if (File.Exists(fileName))
            { File.Delete(fileName); }
        }
    }
}
