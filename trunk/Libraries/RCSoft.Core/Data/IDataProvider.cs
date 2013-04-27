
using System.Data.Common;
namespace RCSoft.Core.Data
{
    /// <summary>
    /// 数据库接口
    /// </summary>
    public interface IDataProvider
    {
        /// <summary>
        /// 初始化数据库
        /// </summary>
        void InitDatabase();

        /// <summary>
        /// 设置数据库是否支持存储过程
        /// </summary>
        bool StoredPeoceduredSupported { get; }

        /// <summary>
        /// 为存储过程获取参数
        /// </summary>
        /// <returns></returns>
        DbParameter GetParameter();
    }
}
