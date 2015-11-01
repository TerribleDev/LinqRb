using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace LinqRb.UnitTests
{
    public class EnumerableTest
    {
        [Fact]
        public void TestRejectSuccessful()
        {
            var range = Enumerable.Range(1, 5);
            var rejected = range.Reject(a => a == 3);
            foreach (var number in rejected)
            {
                Assert.NotEqual(number, 3);
            }
            Assert.True(rejected.Count() == 4);
        }

        [Fact]
        public void TestAssoc()
        {
            var outer = new List<List<string>>();
            outer.Add(new List<string>() { "books", "school", "apple", "playtime" });
            outer.Add(new List<string>() { "star", "wars", "vader" });
            var assc = outer.AssocFirstOrDefault("wars");
            Assert.True(assc.Count() == 3);
            var assc2 = outer.AssocFirstOrDefault("poop");
            Assert.True(assc2 == null);
        }

        [Fact]
        public void TestCycle()
        {
            var p = new List<string> { "a", "b" };
            var count = 0;
            p.Cycle(a => count++, 2);
            Assert.True(count == 4);
        }

        [Fact]
        public void TestForEach()
        {
            var counter = 0;
            Enumerable.Range(1, 4).ForEach(a => counter++);
            Assert.Equal(counter, 4);
        }

        [Fact]
        public void TestForEachIndex()
        {
            var counter = 0;
            Enumerable.Range(1, 4).ForEach((a, index) =>
            {
                Assert.Equal(counter, index);
                counter++;
            });
            Assert.Equal(counter, 4);
        }

        [Fact]
        public void TestDistinct()
        {
            var items = new List<Tuple<string, string>>();
            items.Add(Tuple.Create("awesom", "1"));
            items.Add(Tuple.Create("awesom", "7"));
            items.Add(Tuple.Create("awesom", "6"));
            items.Add(Tuple.Create("awzomes", "2"));
            items.Add(Tuple.Create("pwned", "3"));
            items.Add(Tuple.Create("kewlbz", "4"));
            var result = items.Distinct(a => a.Item1);
            Assert.True(result.Count() == 4);
        }

        [Fact]
        public void TestChunks()
        {
            var t = new List<string>();
            var data = Enumerable.Range(1, 20).Select((a) => "ts").Chunk(4).ToList();
            Assert.Equal(data.Count, 5);
            foreach (var childData in data)
            {
                Assert.Equal(childData.Count(), 4);
            }
        }

        [Fact]
        public void TestOddSizeChunks()
        {
            var t = new List<string>();
            var data = Enumerable.Range(1, 22).Select((a) => "ts").Chunk(4).ToList();
            Assert.Equal(data.Count, 6);
            Assert.True(data.First(a => a.Count() < 4).Count() == 2);
        }
    }
}