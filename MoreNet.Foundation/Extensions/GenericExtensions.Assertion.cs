﻿#pragma warning disable CA1050 // Declare types in namespaces
using System;
using System.Collections;

/// <summary>
/// Extension methods for all generic types.
/// </summary>
/// <remarks>
/// Remove namespace on purpose.
/// </remarks>
public static partial class GenericExtensions
{
    /// <summary>
    /// Assert argument should not null.
    /// </summary>
    /// <typeparam name="T">Type of argument.</typeparam>
    /// <param name="arg">Argument.</param>
    /// <param name="argName">Argument name.</param>
    /// <remarks>
    /// Should not be null for reference type.
    /// Should not be empty for <see cref="IEnumerable"/>.
    /// </remarks>
    public static void ShouldNotNull<T>(this T arg, string argName)
        where T : class
    {
        if (arg == null)
        {
            throw new ArgumentNullException(argName);
        }
    }

    /// <summary>
    /// Assert argument should not empty.
    /// </summary>
    /// <typeparam name="T">Type of argument.</typeparam>
    /// <param name="arg">Argument.</param>
    /// <param name="argName">Argument name.</param>
    /// <remarks>
    /// Should not be null for reference type.
    /// Should not be empty for <see cref="IEnumerable"/>.
    /// </remarks>
    public static void ShouldNotEmpty<T>(this T arg, string argName)
        where T : class
    {
        if (arg == null)
        {
            throw new ArgumentNullException(argName);
        }

        var argumentEmptyException = new ArgumentException("Value should not be empty", argName);
        if (arg is string s && s.Length == 0)
        {
            throw argumentEmptyException;
        }

        if (arg is IEnumerable enumerableTarget)
        {
            bool any = false;
            foreach (var item in enumerableTarget)
            {
                any = true;
                break;
            }

            if (!any)
            {
                throw argumentEmptyException;
            }
        }
    }
}

#pragma warning restore CA1050 // Declare types in namespaces