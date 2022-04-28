using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using View.AutoTask.Options;

namespace View.AutoTask.Core
{
    internal static class Default
    {
        private static readonly string split = Environment.NewLine;

        public static TaskOptions Options { get; set; } = new TaskOptions();

        public static void WriteMetadatas(this IMetadataCache cache, HashSet<Metadata> metadatas)
        {
            if (Options.CacheType == CacheType.Text)
            {
                string stringMetadatas = string.Join(split, metadatas.Select(x => x.GetString()));
                if (string.IsNullOrWhiteSpace(stringMetadatas))
                { return; }
                cache.Write(Encoding.UTF8.GetBytes(stringMetadatas));
            }
            else if (Options.CacheType == CacheType.Binary)
            {
                List<byte> bytes = new List<byte>();
                foreach (Metadata metadata in metadatas)
                {
                    bytes.AddRange(metadata.GetByte());
                }
                cache.Write(bytes.ToArray());
            }
        }

        public static HashSet<Metadata> ReadMetadatas(this IMetadataCache cache)
        {
            HashSet<Metadata> metadatas = new HashSet<Metadata>();
            if (Options.CacheType == CacheType.Text)
            {
                byte[] byteMetadatas = cache.Read();
                if (byteMetadatas != null && byteMetadatas.Length != 0)
                {
                    var stringMetadatas = Encoding.UTF8.GetString(byteMetadatas).Split(new string[] { split }, StringSplitOptions.RemoveEmptyEntries);
                    if (stringMetadatas != null && stringMetadatas.Length != 0)
                    {
                        foreach (var stringMetadata in stringMetadatas)
                        {
                            metadatas.Add(Metadata.Parse(stringMetadata));
                        }
                    }
                }
            }
            else if (Options.CacheType == CacheType.Binary)
            {
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP2_1_OR_GREATER
                var byteMetadatas = cache.Read().AsSpan();
                for (int i = 0; i < byteMetadatas.Length; i += 16)
                {
                    List<byte> bytes = new List<byte>();
                    bytes.AddRange(byteMetadatas.Slice(i, 4).ToArray());
                    bytes.AddRange(byteMetadatas.Slice(i + 4, 4).ToArray());
                    bytes.AddRange(byteMetadatas.Slice(i + 8, 8).ToArray());
                    metadatas.Add(Metadata.Parse(bytes.ToArray()));
                }
#else
                var byteMetadatas = cache.Read();
                for (int i = 0; i < byteMetadatas.Length; i += 16)
                {
                    List<byte> bytes = new List<byte>();
                    for (int j = 0; j < 16; j++)
                    {
                        bytes.Add(byteMetadatas[j]);
                    }
                    metadatas.Add(Metadata.Parse(bytes.ToArray()));
                }
#endif
            }
            return metadatas;
        }

        public static void ClrarMetadatas(this IMetadataCache cache) => cache.Clear();

    }
}
