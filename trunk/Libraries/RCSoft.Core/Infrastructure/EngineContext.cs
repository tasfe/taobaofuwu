using System;
using System.Configuration;
using System.Runtime.CompilerServices;
using System.Diagnostics;
using RCSoft.Core.Configuration;

namespace RCSoft.Core.Infrastructure
{
    public class EngineContext
    {
        #region 初始化方法
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static IEngine Initialize(bool forceRecrate)
        {
            if (Singleton<IEngine>.Instance == null || forceRecrate)
            {
                var config = ConfigurationManager.GetSection("RCSoftConfig") as RCSoftConfig;
                Debug.WriteLine("构建引擎：" + DateTime.Now);
                Singleton<IEngine>.Instance = CreateEngineInstance(config);
                Debug.WriteLine("初始化引擎：" + DateTime.Now);
                Singleton<IEngine>.Instance.Initialize(config);
            }
            return Singleton<IEngine>.Instance;
        }
        /// <summary>Sets the static engine instance to the supplied engine. Use this method to supply your own engine implementation.</summary>
        /// <param name="engine">The engine to use.</param>
        /// <remarks>Only use this method if you know what you're doing.</remarks>
        public static void Replance(IEngine engine)
        {
            Singleton<IEngine>.Instance = engine;
        }
        /// <summary>
        /// Creates a factory instance and adds a http application injecting facility.
        /// </summary>
        /// <returns>A new factory</returns>
        public static IEngine CreateEngineInstance(RCSoftConfig config)
        {
            if (config != null && !string.IsNullOrEmpty(config.EngineType))
            {
                var engineType = Type.GetType(config.EngineType);
                if (engineType == null)
                    throw new ConfigurationErrorsException("没有发现类型'" + engineType + "'.请检查/configuration/nop/engine[@engineType]位置或者检查字符拼写错误.");
                if (!typeof(IEngine).IsAssignableFrom(engineType))
                    throw new ConfigurationErrorsException("类型'" + engineType + "'没有实现'RCSoft.Core.Infrastructure.IEngine',不能配置在/configuration/nop/engine[@engineType]位置.");
                return Activator.CreateInstance(engineType) as IEngine;
            }

            return new RCSoftEngine();
        }
        #endregion

        public static IEngine Current
        {
            get
            {
                if (Singleton<IEngine>.Instance == null)
                    Initialize(false);
                return Singleton<IEngine>.Instance;
            }
        }
    }
}
