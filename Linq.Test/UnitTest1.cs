using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Linq.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            double x = 4;
            var seq1 = Enumerable.Range(1, 100);
            seq1.Take(10).Sum().Should().Be(55);

            seq1.Average().Should().Be(50.5);
            seq1.Select(a => new Random(a).NextDouble())
                .Min().Should().BeApproximately(0.011129266, 0.00001);

            seq1.Take(10).Aggregate(0, (total, elt) => total + elt).Should().Be(55);
            Enumerable.Range(1, 4).Aggregate(1, (total, elt) => total * elt).Should().Be(24);

            seq1.Select(a => new Random(a).NextDouble())
                .Aggregate(Double.MaxValue, (min, elt) => elt < min ? elt : min)
                .Should().BeApproximately(0.011129266, 0.00001);

            new List<String>(){"Apple", "Bananas", "Orange"}
                .Aggregate("", (agg, elt) => agg + elt +" ")
                .Should().Be("Apple Bananas Orange ");

            String.Join(" ", new List<String>() { "Apple", "Bananas", "Orange" })
                
                .Should().Be("Apple Bananas Orange");

            seq1.GroupBy(a => a % 2 == 0).First(grp => grp.Key == false)
                .Sum().Should().Be(2500);
        }

        [TestMethod]
        public void GroupByChar()
        {
            // canonical form uppercase. ibm IBM KISS
            var s = "AbraCadabra";
            var grps = s.GroupBy(c => Char.ToUpper(c));
            grps.Count().Should().Be(5);
            var aGroup = grps.First(g => g.Key == 'A');
            aGroup.Key.Should().Be('A');
            aGroup.Count().Should().Be(5);
            aGroup.Skip(3).First().Should().Be('a');
            aGroup.First().Should().Be('A');
        }


        [TestMethod]
        public void GroupByWord()
        {
            var s = "The fox ate the bird which ate the orange";
            var words = s.Split(' ', '.', ',', '\t');
            words.Length.Should().Be(9);
            var grps = words.GroupBy(w => w.ToUpper())
                .OrderByDescending(g => g.Count()).ToList();
            grps.Count().Should().Be(6);
            grps.First().Key.Should().Be("THE");
            grps.Last().Key.Should().Be("ORANGE");

            var top2 = grps.Take(2).ToList();
            top2.Should().Contain(g => g.Key == "THE");
            top2.Should().Contain(g => g.Key == "ATE");

            var top2Words = top2.Select(g => g.Key);
            var top3Words = top2.Select(g => g.Key);

            top2Words.Intersect(top3Words).Count().Should().Be(2);



            //var aGroup = grps.First(g => g.Key == 'A');
            //aGroup.Key.Should().Be('A');
            //aGroup.Count().Should().Be(5);
            //aGroup.Skip(3).First().Should().Be('a');
            //aGroup.First().Should().Be('A');
        }
    }
}