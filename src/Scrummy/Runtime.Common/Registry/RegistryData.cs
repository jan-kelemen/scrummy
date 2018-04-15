using System;
using System.Collections.Generic;
using System.Text;

namespace Scrummy.Runtime.Common.Registry
{
    public class RegistryData
    {
        private readonly IDictionary<Type, object> _objects = new Dictionary<Type, object>();

        public T Get<T>() where T : class => _objects[typeof(T)] as T;

        public void Register<T>(T o) where T : class => _objects[typeof(T)] = o;
    }
}
