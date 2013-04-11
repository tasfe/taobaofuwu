using System;
using System.Collections.Generic;
using System.Collections;

namespace RCSoft.Core
{
    public interface IPersistentCollection<T> : ICollection<T>, ICollection
            where T : class
    {
        Action<ICollection<T>> AfterAdd { get; set; }
        Action<ICollection<T>> AfterRemove { get; set; }
        Func<ICollection<T>, T, bool> BeforeAdd { get; set; }
        Func<ICollection<T>, T, bool> BeforeRemove { get; set; }
    }
}
