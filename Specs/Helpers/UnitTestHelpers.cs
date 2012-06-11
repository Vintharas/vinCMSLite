using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit;
using NUnit.Framework;
using DomainRepos.Abstracts;
using Moq;
using Domain.Entities;


namespace Specs.Helpers
{
    public static partial class UnitTestHelpers
    {
        public static void ShouldEqual<T>(this T actualValue, T expectedValue)
        {
            Assert.AreEqual(expectedValue, actualValue);
        }

    }


}
