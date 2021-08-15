using System;
using Xunit;
using FluentAssertions;
using System.Text.RegularExpressions;

namespace ParseStringKata
{
    public class UnitTest1
    {
        

        [Theory]
        [InlineData("1+1")]
        public void TestParserSum1(string input)
        {
            var sut = new StringParser();

            int result = sut.Parse(input);

            result.Should().Be(2);
        }

        [Theory]
        [InlineData("1+5")]
        public void TestParserSum2(string input)
        {
            var sut = new StringParser();

            int result = sut.Parse(input);

            result.Should().Be(6);
        }

        [Theory]
        [InlineData("abc")]
        [InlineData("123")]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("1+")]
        [InlineData("1+2+2")]
        public void TestParser_invali_input(string input)
        {
            var sut = new StringParser();

            Action act = () => sut.Parse(input);

            act.Should().Throw<ArgumentException>();
        }
    }

    internal class StringParser
    {
        public StringParser()
        {
        }

        internal int Parse(string input)
        {
            var alphaCount = Regex.Matches(input, @"[a-zA-Z]").Count;

            var isNumeric = int.TryParse(input, out _);
            
            var nums = input.Split('+');
            foreach (var num in nums)
            {
                if (!int.TryParse(num, out _))
                {
                    throw new ArgumentException();
                }
            }

            if (alphaCount > 0 || isNumeric || string.IsNullOrEmpty(input) || nums.Length != 2)
            {
                throw new ArgumentException();
            }
        
            return int.Parse(nums[0]) + int.Parse(nums[1]);
        }
    }
}
