using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCSoft.Core.Data;
using RCSoft.Core;

namespace RCSoft.Data
{
    public partial class EfDataProviderManager:BaseDataProviderManager
    {
        public EfDataProviderManager(DataSettings settings)
            : base(settings)
        { }

        public override IDataProvider LoadDataProvider()
        {
            var providerName = Settings.DataProvider;
            if (String.IsNullOrWhiteSpace(providerName))
                throw new RCSoftException("数据设置没有提供名称");
            switch (providerName.ToLowerInvariant())
            {
                case "sqlserver":
                    return new SqlServerDataProvider();
                default:
                    throw new RCSoftException(string.Format("不提供名为{0}的数据库", providerName));
            }
        }
    }
}
