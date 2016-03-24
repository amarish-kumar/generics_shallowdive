using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericsShallowDive
{
    public sealed class DataStoreSingletonIndexer {
        private static readonly Lazy<DataStoreSingletonIndexer> lazy =
            new Lazy<DataStoreSingletonIndexer>(() => new DataStoreSingletonIndexer());
        private readonly ConcurrentDictionary<string, object> _properties;
        public static DataStoreSingletonIndexer Instance { get { return lazy.Value; } }
        private DataStoreSingletonIndexer() {
            _properties = new ConcurrentDictionary<string, object>();
        }
        private void SetProperty<T>(string name, T value) {
            _properties.TryAdd(name, value);
        }
        private dynamic GetProperty(string name) {
            dynamic value;
            _properties.TryGetValue(name, out value);
            return value;
        }
        public int GetCount() {
            return _properties.Count;
        }
        public dynamic this[string index] {
            get { return GetProperty(index); }
            set { SetProperty(index, value); }
        }
    }
}
