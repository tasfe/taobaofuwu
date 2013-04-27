using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.Data.Common;

namespace RCSoft.Data
{
    public abstract class BaseEfDataProvider : IEfDataProvider
    {
        /// <summary>
        /// 获取连接工厂
        /// </summary>
        /// <returns></returns>
        public abstract IDbConnectionFactory GetConnectionFactory();

        /// <summary>
        /// 初始化连接工厂
        /// </summary>
        public void InitConnectionFactory()
        {
            Database.DefaultConnectionFactory = GetConnectionFactory();
        }

        /// <summary>
        /// 设置初始化数据库容器
        /// </summary>
        public abstract void SetDatabaseInitializer();

        /// <summary>
        /// 初始化数据库
        /// </summary>
        public virtual void InitDatabase()
        {
            InitConnectionFactory();
            SetDatabaseInitializer();
        }

        /// <summary>
        /// 设置是否支持存储过程
        /// </summary>
        public abstract bool StoredPeoceduredSupported { get; }

        /// <summary>
        /// 为存储过程获取参数
        /// </summary>
        /// <returns></returns>
        public abstract DbParameter GetParameter();
    }
}
