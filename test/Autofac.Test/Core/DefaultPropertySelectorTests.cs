﻿// Copyright (c) Autofac Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Reflection;
using Autofac.Core;

namespace Autofac.Test.Core;

public class DefaultPropertySelectorTests
{
    [InlineData(true, "PrivatePropertyWithSet", false)]
    [InlineData(false, "PrivatePropertyWithSet", false)]
    [InlineData(true, "PublicPropertyNoDefault", true)]
    [InlineData(false, "PublicPropertyNoDefault", true)]
    [InlineData(true, "PublicPropertyWithDefault", false)]
    [InlineData(false, "PublicPropertyWithDefault", true)]
    [InlineData(true, "PublicPropertyNoGet", true)]
    [InlineData(true, "PublicPropertyNoSet", false)]
    [InlineData(true, "PublicPropertyThrowsOnGet", false)]
    [InlineData(false, "PublicPropertyThrowsOnGet", true)]
#if NET7_0_OR_GREATER
    [InlineData(false, "PublicRequiredProperty", false)]
#endif
    [Theory]
    public void DefaultTests(bool preserveSetValue, string propertyName, bool expected)
    {
        var finder = new DefaultPropertySelector(preserveSetValue);

        var instance = new HasProperties
        {
#if NET7_0_OR_GREATER
            PublicRequiredProperty = new(),
#endif
        };
        var property = instance.GetType().GetProperty(propertyName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

        Assert.Equal(expected, finder.InjectProperty(property, instance));
    }

    private class HasProperties
    {
        public Test PublicPropertyNoDefault { get; set; }

        public Test PublicPropertyNoGet
        {
            set { }
        }

        public Test PublicPropertyNoSet { get; }

        public Test PublicPropertyThrowsOnGet
        {
            get
            {
                // Issue #799: InjectUnsetProperties blows up if the getter throws an exception.
                throw new InvalidOperationException();
            }

            set
            {
            }
        }

#if NET7_0_OR_GREATER
        required public Test PublicRequiredProperty { get; set; }
#endif

        public Test PublicPropertyWithDefault { get; set; } = new Test();

        private Test PrivatePropertyWithDefault { get; set; } = new Test();

        private Test PrivatePropertyWithSet { get; set; }
    }

    private class Test
    {
    }
}
