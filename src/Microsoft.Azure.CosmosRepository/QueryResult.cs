﻿// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Microsoft.Azure.CosmosRepository;

/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
public class QueryResult<T> : IQueryResult<T>
    where T : IItem
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="items"></param>
    /// <param name="charge"></param>
    public QueryResult(IReadOnlyList<T> items, double charge)
    {
        Items = items;
        Charge = charge;
    }
    /// <summary>
    /// 
    /// </summary>
    public IReadOnlyList<T> Items { get; }
    /// <summary>
    /// 
    /// </summary>
    public double Charge { get; }
}
