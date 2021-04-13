using System;
using System.Collections.Generic;
using System.Text;

namespace View.Core.Models
{
    /// <summary>
    /// 分页标记
    /// </summary>
    public class Pagination
    {
        /// <summary>
        /// 总页码数
        /// </summary>
        protected int _count = 0;
        private int size = 10;
        private int index = 1;

        /// <summary>
        /// 初始化一个页码标记
        /// </summary>
        /// <param name="index"></param>
        /// <param name="size"></param>
        public Pagination(int index = 1, int size = 10)
        {
            Index = index;
            Size = size;
        }

        /// <summary>
        /// 当前需要的页码
        /// <para>默认 1</para>
        /// <para><see cref="Index"/>&gt;0</para>
        /// </summary>
        public int Index { get => index; set => index = value <= 0 ? 1 : value; }
        /// <summary>
        /// 总页数
        /// </summary>
        public int Count => _count;

        /// <summary>
        /// 每一页的数据量
        /// <para>默认 10</para>
        /// <para><see cref="Index"/>&gt;=1</para>
        /// </summary>
        public int Size { get => size; set => size = value <= 0 ? 1 : value; }

        /// <summary>
        /// 使用结果数据和数据量创建一个分页结果
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data">结果</param>
        /// <param name="count">数据总量，不是页码数</param>
        /// <returns></returns>
        public PaginationResult<T> GetResult<T>(T data, int count)
        {
            return new PaginationResult<T>(data, Index, Size, (count - 1) / Size + 1);
        }

    }

    /// <summary>
    /// 包含结果的分页标记
    /// </summary>
    public class PaginationResult<T> : Pagination
    {
        /// <summary>
        /// 初始化一个包含结果的页码标记
        /// </summary>
        /// <param name="data">结果数据</param>
        /// <param name="index">当前页</param>
        /// <param name="Size"></param>
        /// <param name="count"></param>
        public PaginationResult(T data, int index, int Size, int count) : base(index, Size)
        {
            base._count = count;
            Data = data;
        }

        /// <summary>
        /// 结果数据
        /// </summary>
        public T Data { get; }
    }
}
