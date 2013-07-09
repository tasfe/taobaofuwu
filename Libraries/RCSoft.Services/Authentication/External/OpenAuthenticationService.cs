using System.Collections.Generic;
using System.Linq;
using RCSoft.Services.Customers;
using RCSoft.Core.Plugins;
using RCSoft.Core.Domain.Customers;
using RCSoft.Core.Data;
using System;

namespace RCSoft.Services.Authentication.External
{
    public partial class OpenAuthenticationService : IOpenAuthenticationService
    {
        private readonly ICustomerService _customerService;
        private readonly IPluginFinder _pluginFinder;
        private readonly ExternalAuthenticationSettings _externalAuthenticationSettings;
        private readonly IRepository<ExternalAuthenticationRecord> _externalAuthenticationRecordRepository;

        public OpenAuthenticationService(IRepository<ExternalAuthenticationRecord> externalAuthenticationRecordRepository,
           IPluginFinder pluginFinder,
           ExternalAuthenticationSettings externalAuthenticationSettings,
           ICustomerService customerService)
        {
            this._externalAuthenticationRecordRepository = externalAuthenticationRecordRepository;
            this._pluginFinder = pluginFinder;
            this._externalAuthenticationSettings = externalAuthenticationSettings;
            this._customerService = customerService;
        }

        /// <summary>
        /// 加载已经启用的第三方验证方式
        /// </summary>
        /// <returns></returns>
        public virtual IList<IExternalAuthenticationMethod> LoadActiveExternalAuthenticationMethods()
        {
            return LoadAllExternalAuthenticationMethods()
                   .Where(provider => _externalAuthenticationSettings.ActiveAuthenticationMethodSystemNames.Contains(provider.PluginDescriptor.SystemName, StringComparer.InvariantCultureIgnoreCase))
                   .ToList();
        }

        /// <summary>
        /// 根据系统名称加载其它验证方式
        /// </summary>
        /// <param name="systemName"></param>
        /// <returns></returns>
        public virtual IExternalAuthenticationMethod LoadExternalAuthenticationMethodBySystemName(string systemName)
        {
            var descriptor = _pluginFinder.GetPluginDescriptorBySystemName<IExternalAuthenticationMethod>(systemName);
            if (descriptor != null)
                return descriptor.Instance<IExternalAuthenticationMethod>();

            return null;
        }

        /// <summary>
        /// 加载所有第三方验证方式
        /// </summary>
        /// <returns></returns>
        public virtual IList<IExternalAuthenticationMethod> LoadAllExternalAuthenticationMethods()
        {
            return _pluginFinder.GetPlugins<IExternalAuthenticationMethod>().ToList();
        }


        public virtual void AssociateExternalAccountWithUser(Customer customer, OpenAuthenticationParameters parameters)
        {
            if (customer == null)
                throw new ArgumentNullException("客户");

            string email = null;
            if(parameters.UserClaims!=null)
                foreach (var userClaim in parameters.UserClaims.Where(x => x.Contact != null && !String.IsNullOrEmpty(x.Contact.Email)))
                {
                    email = userClaim.Contact.Email;
                    break;
                }

            var externalAuthenticationRecord = new ExternalAuthenticationRecord()
            {
                CustomerId = customer.Id,
                Email = email,
                ExternalIdentifier = parameters.ExternalIdentifier,
                ExternalDisplayIdentifier = parameters.ExternalDisplayIdentifier,
                OAuthToken = parameters.OAuthToken,
                OAuthAccessToken = parameters.OAuthAccessToken,
                ProviderSystemName = parameters.ProviderSystemName,
            };
            _externalAuthenticationRecordRepository.Insert(externalAuthenticationRecord);
        }

        public virtual bool AccountExists(OpenAuthenticationParameters parameters)
        {
            return GetUser(parameters) != null;
        }

        public virtual Customer GetUser(OpenAuthenticationParameters parameters)
        {
            var record = _externalAuthenticationRecordRepository.Table
                .Where(o => o.ExternalIdentifier == parameters.ExternalIdentifier && o.ProviderSystemName == parameters.ProviderSystemName)
                .FirstOrDefault();

            if (record != null)
                return _customerService.GetCustomerById(record.CustomerId);

            return null;
        }

        public virtual IList<ExternalAuthenticationRecord> GetExternalIdentifiersFor(Customer customer)
        {
            if (customer == null)
                throw new ArgumentNullException("customer");

            return customer.ExternalAuthenticationRecords.ToList();
        }

        public virtual void RemoveAssociation(OpenAuthenticationParameters parameters)
        {
            var record = _externalAuthenticationRecordRepository.Table
                .Where(o => o.ExternalIdentifier == parameters.ExternalIdentifier && o.ProviderSystemName == parameters.ProviderSystemName)
                .FirstOrDefault();

            if (record != null)
                _externalAuthenticationRecordRepository.Delete(record);
        }
    }
}
