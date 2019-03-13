using Code.Utils.Collections;
using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace Code.Utils.Test
{
    public class CollectionsTest
    {
        [Fact]
        public void Find_Existing_Item()
        {
            ICollection<string> arr = new[] { "A", "B", "C" };

            arr.FindIndex(item => item == "A").Should().Be(0);
        }

        [Fact]
        public void Return_Minus_One_On_Not_Found()
        {
            IReadOnlyList<string> arr = new[] { "A", "B", "C" };

            arr.FindIndex(item => item == "D").Should().Be(-1);
        }

        [Fact]
        public void Find_Existing_Item_With_Idx()
        {
            ICollection<string> arr = new[] { "A", "B", "C" };

            arr.FindIndex((item, idx) => {
                if (item == "B")
                {
                    idx.Should().Be(1);
                    return true;
                }
                return false;
            }).Should().Be(1);
        }

        [Fact]
        public void Return_Minus_One_On_Not_Found_With_Idx()
        {
            ICollection<string> arr = new[] { "A", "B", "C" };

            arr.FindIndex((item, idx) => item == "D").Should().Be(-1);
        }
    }
}