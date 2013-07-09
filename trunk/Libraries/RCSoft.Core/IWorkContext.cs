using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCSoft.Core.Domain.Customers;

namespace RCSoft.Core
{
    /// <summary>
    /// 运行环境
    /// </summary>
    public interface IWorkContext
    {
        /// <summary>
        /// 设置获取当前用户
        /// </summary>
        Customer CurrentCustomer { get; set; }
    }
}
