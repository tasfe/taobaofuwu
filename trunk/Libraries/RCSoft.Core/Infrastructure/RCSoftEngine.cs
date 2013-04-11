using System;
using RCSoft.Core.Infrastructure.DependencyManagement;
using System.Configuration;
using RCSoft.Core.Configuration;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using RCSoft.Core.Data;

namespace RCSoft.Core.Infrastructure
{
    public class RCSoftEngine:IEngine
    {
        #region 字段

        private ContainerManager _containerManager;

        #endregion

        #region 构造函数
        public RCSoftEngine()
            : this(EventBroker.Instance, new ContainerConfigurer())
        { }

        public RCSoftEngine(EventBroker broker, ContainerConfigurer configurer)
        {
            var config = ConfigurationManager.GetSection("RCSoftConfig") as RCSoftConfig;
            InitializeContainer(configurer, broker, config);
        }

        #endregion

        #region 工具
        private void RunStartupTasks()
        {
            var typeFinder = _containerManager.Resolve<ITypeFinder>();
            var startUpTaskTypes = typeFinder.FindClassesOfType<IStartupTask>();
            var startUpTasks = new List<IStartupTask>();
            foreach (var startUpTaskType in startUpTaskTypes)
                startUpTasks.Add((IStartupTask)Activator.CreateInstance(startUpTaskType));
            //排序
            startUpTasks = startUpTasks.AsQueryable().OrderBy(st => st.Order).ToList();
            foreach (var startUpTask in startUpTasks)
                startUpTask.Execute();
        }

        private void InitializeContainer(ContainerConfigurer configurer, EventBroker broker, RCSoftConfig config)
        {
            var builder = new ContainerBuilder();
            _containerManager = new ContainerManager(builder.Build());
            configurer.Configure(this, _containerManager, broker, config);
        }
        #endregion

        #region 方法
        public void Initialize(RCSoftConfig config)
        {
            bool databaseInstalled = DataSettingsHelper.DatabaseIsInstalled();
            if (databaseInstalled)
            {
                RunStartupTasks();
            }
        }

        public T Resolve<T>() where T : class
        {
            return ContainerManager.Resolve<T>();
        }

        public object Resolve(Type type)
        {
            return ContainerManager.Resolve(type);
        }

        public T[] ResolveAll<T>()
        {
            return ContainerManager.ResolveAll<T>();
        }
        #endregion

        #region 属性
        public ContainerManager ContainerManager
        {
            get { return _containerManager; }
        }
        #endregion
    }
}
