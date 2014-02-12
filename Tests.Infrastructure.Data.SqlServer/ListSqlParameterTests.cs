using System.Collections.Generic;
using Infrastructure.Data.SqlServer;
using Moq;
using NUnit.Framework;

namespace Tests.Infrastructure.Data.SqlServer
{
    public class ListSqlParameterTests
    {
        [Test]
        public void ParameterName_IsSetOnConstruction()
        {
            const string paramName = "a";
            var idList = new List<int>();

            var sut = new ListSqlParameter(paramName, idList);
            var result = sut.ParameterName;

            Assert.AreEqual(paramName, result);
        }

        [Test]
        public void ParameterNameList_WithOneParam_HasOneParamName()
        {
            var idList = new List<int> { 1 };
            const string expected = "@param0";

            var sut = new ListSqlParameter(It.IsAny<string>(), idList);
            var result = sut.ParameterNameList;

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ParameterNameList_WithTwoParam_HasTwoParamNames()
        {
            var idList = new List<int> { 1, 2 };
            const string expected = "@param0,@param1";

            var sut = new ListSqlParameter(It.IsAny<string>(), idList);
            var result = sut.ParameterNameList;

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ParameterNameList_WithThreeParam_HasThreeParamNames()
        {
            var idList = new List<int> { 1, 2, 3 };
            const string expected = "@param0,@param1,@param2";

            var sut = new ListSqlParameter(It.IsAny<string>(), idList);
            var result = sut.ParameterNameList;

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ParameterList_WithOneParam_HasOneParamWithCorrectValues()
        {
            const int expectedLength = 1;
            const string expectedParamName = "@param0";
            var idList = new List<int> { 1 };

            var sut = new ListSqlParameter(It.IsAny<string>(), idList);
            var result = sut.ParameterList;

            Assert.AreEqual(expectedLength, result.Count);
            Assert.AreEqual(expectedParamName, result[0].ParameterName);
        }

        [Test]
        public void ParameterList_WithTwoParams_HasTwoParams()
        {
            var idList = new List<int> { 1, 2 };
            const int expected = 2;

            var sut = new ListSqlParameter(It.IsAny<string>(), idList);
            var result = sut.ParameterList.Count;

            Assert.AreEqual(expected, result);
        }
    }
}
