using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;

namespace View.AutoTask.Core
{
    /// <summary>
    /// 任务元数据
    /// </summary>
    internal class Metadata : IEquatable<Metadata>
    {
        private static readonly string split = ".";

        public Metadata(MethodInfo methodInfo) : this(methodInfo.Module.MetadataToken, methodInfo.MetadataToken) { }
        public Metadata(MethodInfo methodInfo, long excuteTime) : this(methodInfo.Module.MetadataToken, methodInfo.MetadataToken, excuteTime) { }

        public Metadata(int moduleToken, int methodToken) : this(moduleToken, methodToken, 0) { }
        public Metadata(int moduleToken, int methodToken, long excuteTime)
        {
            ModuleToken = moduleToken;
            MethodToken = methodToken;
            ExcuteTime = excuteTime;
        }

        public int ModuleToken { get; set; }
        public int MethodToken { get; set; }
        public long ExcuteTime { get; set; }

        public string GetString()
        {
            return $"{ModuleToken:X4}{split}{MethodToken:X4}{split}{ExcuteTime:X8}";
        }

        public byte[] GetByte()
        {
            List<byte> buffers = new List<byte>();
            buffers.AddRange(BitConverter.GetBytes(ModuleToken));
            buffers.AddRange(BitConverter.GetBytes(MethodToken));
            buffers.AddRange(BitConverter.GetBytes(ExcuteTime));
            return buffers.ToArray();
        }

        public static Metadata Parse(byte[] buffer)
        {
            if (buffer.Length != 16)
            { throw new ArgumentOutOfRangeException(nameof(buffer), "不是正确的数据"); }
            return new Metadata(
                BitConverter.ToInt32(buffer, 0),
                BitConverter.ToInt32(buffer, 4),
                BitConverter.ToInt64(buffer, 8));
        }

        public static Metadata Parse(string parseString)
        {
            var datas = parseString.Split(split);
            if (datas.Length == 3)
            {
                return new Metadata(Convert.ToInt32(datas[0], 16), Convert.ToInt32(datas[1], 16), Convert.ToInt64(datas[2], 16));
            }
            throw new ArgumentOutOfRangeException(nameof(parseString), "不是正确的格式化字符串");
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Metadata);
        }

        public bool Equals(Metadata other)
        {
            return other != null &&
                   ModuleToken == other.ModuleToken &&
                   MethodToken == other.MethodToken;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ModuleToken, MethodToken);
        }
    }
}
