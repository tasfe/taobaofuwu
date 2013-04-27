using System.Data.Entity.Infrastructure;
using RCSoft.Core.Data;

namespace RCSoft.Data
{
    /// <summary>
    /// 实现EntityFrameWork框架接口
    /// </summary>
    public interface IEfDataProvider : IDataProvider
    {
        /// <summary>
        /// 获取连接工厂
        /// </summary>
        /// <returns>连接工厂类</returns>
        IDbConnectionFactory GetConnectionFactory();

        /// <summary>
        /// 初始化连接工厂类
        /// </summary>
        void InitConnectionFactory();

        /// <summary>
        /// 设置数据库初始化容器
        /// </summary>
        void SetDatabaseInitializer();
    }
}
