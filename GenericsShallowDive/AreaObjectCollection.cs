using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

namespace GenericsShallowDive {
    public class AreaObjectCollection {
        public Dictionary<int, Dictionary<int, List<AreaObject>>> map =
            new Dictionary<int, Dictionary<int, List<AreaObject>>>();
     
        public void Add(AreaId id, AreaObject obj) {
            int x = id.X;
            int y = id.Y;
            if (!map.ContainsKey(x)) {
                map.Add(x, new Dictionary<int, List<AreaObject>>());
            }
            if (!map[x].ContainsKey(y)) {
                map[x].Add(y, new List<AreaObject>());
            }
            map[x][y].Add(obj);
        }
    }

    public class AreaId : IAreaId {
        public int X { get; set; }
        public int Y { get; set; }
    }
    public interface IAreaId {
        int X { get; set; }
        int Y { get; set; }
    }
    public class AreaObject : IAreaObject {
        public string AreaId { get; set; }
    }
    public interface IAreaObject {
        string AreaId { get; set; }
    }

    public class AreaObjectCollectionTests {
        private readonly ITestOutputHelper _output;
        public AreaObjectCollectionTests(ITestOutputHelper output) {
            _output = output;
        }
        [Fact]
        public void AreaObjectCollection_Add_method() {
            var areaObjectCollection = new AreaObjectCollection();
            areaObjectCollection.Add(
                new AreaId() { X = 1, Y = 1 }, 
                new AreaObject() { AreaId = "My Area"});

            _output.WriteLine(areaObjectCollection.ToJson());
        }
    }

    public static class JsonHelpers {
        public static string ToJson(this object obj) {
            return JsonConvert.SerializeObject(obj, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }
    }
}
