using System.Collections.Generic;
using System.Security.Policy;
using Xunit;
using Xunit.Abstractions;

namespace GenericsShallowDive {
    public class GenericAreaObjectCollection<T> where T : IAreaObject {
        public Dictionary<int, Dictionary<int, List<T>>> map =
            new Dictionary<int, Dictionary<int, List<T>>>();

        public void Add(AreaId id, T obj) {
            int x = id.X;
            int y = id.Y;
            if (!map.ContainsKey(x)) {
                map.Add(x, new Dictionary<int, List<T>>());
            }
            if (!map[x].ContainsKey(y)) {
                map[x].Add(y, new List<T>());
            }
            map[x][y].Add(obj);
        }
    }

    public class EnhancedAreaObject : IAreaObject {
        public string AreaId { get; set; }
        public string SomeCoolAttribute { get; set; }
    }

    public class GenericAreaObjectCollectionTests {
        private readonly ITestOutputHelper _output;
        public GenericAreaObjectCollectionTests(ITestOutputHelper output) {
            _output = output;
        }

        [Fact]
        public void GenericAreaObjectCollection_Add_areaObject() {
            var areaObjectCollection = new GenericAreaObjectCollection<AreaObject>();
            areaObjectCollection.Add(
                new AreaId() { X = 1, Y = 1 },
                new AreaObject() { AreaId = "My Area" });

            _output.WriteLine(areaObjectCollection.ToJson());
            /*
              "map": {
                "1": {
                  "1": [
                    {
                      "AreaId": "My Area"
                    }
                  ]
                }
            */
        }
        [Fact]
        public void GenericAreaObjectCollection_Add_enhancedAreaObject() {
            var areaObjectCollection = new GenericAreaObjectCollection<EnhancedAreaObject>();
            areaObjectCollection.Add(
                new AreaId() { X = 1, Y = 1 },
                new EnhancedAreaObject() {
                    AreaId = "My Area",
                    SomeCoolAttribute = "I'm a better AreaObject"
                });

            _output.WriteLine(areaObjectCollection.ToJson());
            /*
              "map": {
                "1": {
                  "1": [
                    {
                      "AreaId": "My Area",
                      "SomeCoolAttribute": "I'm a better AreaObject"
                    }
                  ]
                }
            */
        }
    }
}