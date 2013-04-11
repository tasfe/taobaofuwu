using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCSoft.Core.Domain.Localization
{
    public partial class Language : BaseEntity
    {
        private ICollection<LocaleStringResource> _localeStringResources;

        public virtual string Name { get; set; }

        public virtual string LanguageCulture { get; set; }

        public virtual string UniqueSeoCode { get; set; }

        public virtual string FlagImageFileName { get; set; }

        public virtual bool Published { get; set; }

        public virtual int DisplayOrder { get; set; }

        public virtual ICollection<LocaleStringResource> LocaleStringResoures
        {
            get { return _localeStringResources ?? (_localeStringResources = new List<LocaleStringResource>()); }
            protected set { _localeStringResources = value; }
        }
    }
}
