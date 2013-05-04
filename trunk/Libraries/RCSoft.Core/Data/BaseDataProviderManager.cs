using System;

namespace RCSoft.Core.Data
{
    public abstract class BaseDataProviderManager
    {
        protected BaseDataProviderManager(DataSettings settings)
        {
            if (settings == null)
                throw new ArgumentNullException("设置");
            this.Settings = settings;
        }
        protected DataSettings Settings { get; private set; }

        public abstract IDataProvider LoadDataProvider();
    }
}
