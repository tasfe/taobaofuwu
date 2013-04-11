using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace RCSoft.Core
{
    [Serializable]
    public class RCSoftException : Exception
    {
        /// <summary>
        /// 实例化一个新的异常类
        /// </summary>
        public RCSoftException()
        { }
        /// <summary>
        /// 实例化一个带错误消息的异常类
        /// </summary>
        /// <param name="message">异常消息</param>
        public RCSoftException(string message)
            : base(message)
        { }
        /// <summary>
        /// 实例化一个带指定格式的错误异常类
        /// </summary>
        /// <param name="messageFormat">异常消失格式</param>
        /// <param name="args">异常错误参数</param>
        public RCSoftException(string messageFormat, params object[] args)
            : base(string.Format(messageFormat, args))
        { }
        /// <summary>
        /// 实例化一个序列化的错误类
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected RCSoftException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }
        /// <summary>
        /// 实例化一个特殊错误并转向内部错误的异常类
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public RCSoftException(string message, Exception innerException)
            : base(message, innerException)
        { }

    }
}
