using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCSoft.Core.Domain.Configuration
{
    public partial class Setting : BaseEntity
    {
        public Setting() { }

        public Setting(string name, string value)
        {
            this.Name = name;
            this.Value = value;
        }
        /// <summary>
        /// 名称
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public virtual string Value { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
