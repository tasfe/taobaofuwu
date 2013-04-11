using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCSoft.Core.Domain.Localization
{
    public partial class LocaleStringResource : BaseEntity
    {
        public virtual int LanguageId { get; set; }

        public virtual string ResourceName { get; set; }

        public virtual string ResourceValue { get; set; }

        public virtual Language Language { get; set; }
    }
}
