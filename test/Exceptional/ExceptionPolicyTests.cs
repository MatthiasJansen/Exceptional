#region headers

// Copyright (c) 2017 Matthias Jansen
// See the LICENSE file in the project root for more information.

#endregion

#region imports

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ExceptionManager.Policies;
using NUnit.Framework;

#endregion

namespace ExceptionManagerTests
{
    [TestFixture]
    public class ExceptionPolicyTests
    {
        [Test]
        public void Assert_ThatCorrectTypeGuidsAreAvailable()
        {
            var policy = new ExceptionPolicy<NotFiniteNumberException, NotSupportedException>(null, null);

            Assert.That(policy.Handles, Is.EqualTo(typeof(NotFiniteNumberException)));
            Assert.That(policy.Returns, Is.EqualTo(typeof(NotSupportedException)));
        }
    }

    [TestFixture]
    public class ExceptionPolicyGroupTests
    {
        [Test]
        public void Handles_ShouldBeTheTypeOfTheDstException()
        {
            var epg = new ExceptionPolicyGroup<ArgumentNullException, ArgumentOutOfRangeException>(
                new ReadOnlyDictionary<string, ExceptionPolicy<ArgumentNullException, ArgumentOutOfRangeException>>(
                    new Dictionary<string, ExceptionPolicy<ArgumentNullException, ArgumentOutOfRangeException>>()));

            Assert.That(() => epg.Returns, Is.EqualTo(typeof(ArgumentOutOfRangeException)));
        }

        [Test]
        public void Handles_ShouldBeTheTypeOfTheSrcException()
        {
            var epg = new ExceptionPolicyGroup<ArgumentNullException, ArgumentOutOfRangeException>(
                new ReadOnlyDictionary<string, ExceptionPolicy<ArgumentNullException, ArgumentOutOfRangeException>>(
                    new Dictionary<string, ExceptionPolicy<ArgumentNullException, ArgumentOutOfRangeException>>()));

            Assert.That(() => epg.Handles, Is.EqualTo(typeof(ArgumentNullException)));
        }

        [Test]
        public void T1()
        {
            var epg = new ExceptionPolicyGroup<ArgumentNullException, ArgumentOutOfRangeException>(
                new ReadOnlyDictionary<string, ExceptionPolicy<ArgumentNullException, ArgumentOutOfRangeException>>(
                    new Dictionary<string, ExceptionPolicy<ArgumentNullException, ArgumentOutOfRangeException>>()));

            var ctx = epg.PolicyByContextOrDefault(null);
        }
    }
}