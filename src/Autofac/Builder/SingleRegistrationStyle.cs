﻿// Copyright (c) Autofac Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Autofac.Core;
using Autofac.Util;

namespace Autofac.Builder;

/// <summary>
/// Registration style for individual components.
/// </summary>
public class SingleRegistrationStyle
{
    /// <summary>
    /// Gets or sets the ID used for the registration.
    /// </summary>
    public Guid Id { get; set; } = FastGuid.NewGuid();

    /// <summary>
    /// Gets the handlers to notify of the component registration event.
    /// </summary>
    public ICollection<EventHandler<ComponentRegisteredEventArgs>> RegisteredHandlers { get; } = new List<EventHandler<ComponentRegisteredEventArgs>>();

    /// <summary>
    /// Gets or sets a value indicating whether default registrations should be preserved.
    /// By default, new registrations override existing registrations as defaults.
    /// If set to true, new registrations will not change existing defaults.
    /// </summary>
    public bool PreserveDefaults { get; set; }

    /// <summary>
    /// Gets or sets the component upon which this registration is based.
    /// </summary>
    public IComponentRegistration? Target { get; set; }
}
