using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace View.Core.Models
{
    /// <summary>
    /// 实体类 接口
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// 将实体转换为 基于字节数组的 <see cref="HttpContent"/> 。
        /// </summary>
        /// <returns></returns>
        HttpContent ToByteArrayContent();
        /// <summary>
        /// 将实体转换为 使用 <see langword="application/x-www-form-urlencoded MIME"/>  类型编码的名称/值元组的 <see cref="HttpContent"/> 。
        /// </summary>
        /// <returns></returns>
        HttpContent ToFormUrlEncodedContent();
        /// <summary>
        /// 将实体转换为 使用多部分 <see langword="/*"/> 内容类型的 <see cref="HttpContent"/> 。
        /// </summary>
        /// <returns></returns>
        HttpContent ToMultipartContent();
        /// <summary>
        /// 将实体转换为 使用 <see langword="multipart/form-data MIME"/>  类型进行编码的 <see cref="HttpContent"/> 。
        /// </summary>
        /// <returns></returns>
        HttpContent ToMultipartFormDataContent();
        /// <summary>
        /// 将实体转换为 基于流的 <see cref="HttpContent"/> 。
        /// </summary>
        /// <returns></returns>
        HttpContent ToStreamContent();
        /// <summary>
        /// 将实体转换为 基于字符串的 <see cref="HttpContent"/> 。
        /// </summary>
        /// <returns></returns>
        HttpContent ToStringContent();

        /// <summary>
        /// 将实体转换为 查询字符串。
        /// </summary>
        /// <returns></returns>
        string ToQuery();
    }

    /// <summary>
    /// 所有实体类的基类。
    /// </summary>
    public abstract class Entity : IEntity
    {
        /// <summary>
        /// 将实体转换为 基于字节数组的 <see cref="HttpContent"/> 。
        /// </summary>
        /// <returns></returns>
        public HttpContent ToByteArrayContent() => throw new NotImplementedException();

        /// <summary>
        /// 将实体转换为 使用 <see langword="application/x-www-form-urlencoded MIME"/>  类型编码的名称/值元组的 <see cref="HttpContent"/> 。
        /// </summary>
        /// <returns></returns>
        public HttpContent ToFormUrlEncodedContent()
        {
            Type type = GetType();
            var propertys = type.GetProperties();
            Dictionary<string, string> keyValues = new Dictionary<string, string>();
            propertys.ToList().ForEach(property => keyValues.Add(property.Name, (property.GetValue(this) ?? string.Empty).ToString()));
            return new FormUrlEncodedContent(keyValues);
        }

        /// <summary>
        /// 将实体转换为 使用多部分 <see langword="/*"/> 内容类型的 <see cref="HttpContent"/> 。
        /// </summary>
        /// <returns></returns>
        public HttpContent ToMultipartContent() => throw new NotImplementedException();

        /// <summary>
        /// 将实体转换为 使用 <see langword="multipart/form-data MIME"/>  类型进行编码的 <see cref="HttpContent"/> 。
        /// </summary>
        /// <returns></returns>
        public HttpContent ToMultipartFormDataContent() => throw new NotImplementedException();

        /// <summary>
        /// 将实体转换为 基于流的 <see cref="HttpContent"/> 。
        /// </summary>
        /// <returns></returns>
        public HttpContent ToStreamContent() => throw new NotImplementedException();

        /// <summary>
        /// 将实体转换为 基于字符串的 <see cref="HttpContent"/> 。
        /// </summary>
        /// <returns></returns>
        public HttpContent ToStringContent() => throw new NotImplementedException();


        /// <summary>
        /// 将实体转换为 查询字符串。
        /// </summary>
        /// <returns></returns>
        public string ToQuery()
        {
            throw new NotImplementedException();
        }
    }
}
