# MoreNet Foundation

Foundation functions for general-purpose use and other `MoreNet.*` projects.

## Introduction

This library provides common foundation features for general use and shared MoreNet projects.

## Usage

### Pagination and order

Implement `IPageable` for pagination, `IOrderable<T>` for a single order condition, and `ISequentialOrderable<T>` for a sequence of order conditions. These conventions help keep request models consistent while leaving query composition in the application layer.

`QueryableExtensions.SequentialOrderBy(...)` is designed to support both styles. The first call starts an `OrderBy` or `OrderByDescending`, and each later call continues with `ThenBy` or `ThenByDescending`. This makes it suitable for both simple ordering and an arbitrary number of sequential order conditions.

In practice:

- Use `IOrderable<T>` when the caller only needs one ordering choice.
- Use `ISequentialOrderable<T>` when the caller needs multiple ordering choices in order.
- Apply both through the same `SequentialOrderBy(...)` extension method.

```csharp
public enum ProductOrderBy
{
    Name = 1,
    NameDescending = 2,
    Price = 4,
    PriceDescending = 8,
}

public sealed class ProductQuery : IOrderable<ProductOrderBy>
{
    public ProductOrderBy OrderBy { get; set; }
}

public sealed class ProductAdvancedQuery : ISequentialOrderable<ProductOrderBy>
{
    public IList<ProductOrderBy> SequentialOrderBy { get; set; } = new List<ProductOrderBy>();
}

query = query.SequentialOrderBy(request.OrderBy, item => item.Name, request.OrderBy == ProductOrderBy.NameDescending);

foreach (var orderBy in advancedRequest.SequentialOrderBy)
{
    switch (orderBy)
    {
        case ProductOrderBy.Name:
        case ProductOrderBy.NameDescending:
            query = query.SequentialOrderBy(orderBy, item => item.Name, orderBy == ProductOrderBy.NameDescending);
            break;
        case ProductOrderBy.Price:
        case ProductOrderBy.PriceDescending:
            query = query.SequentialOrderBy(orderBy, item => item.Price, orderBy == ProductOrderBy.PriceDescending);
            break;
    }
}
```

### `TextElementString`

Provides text element functions by using the built-in `TextElementEnumerator`.

### `FluentUriBuilder`

A fluent API-style wrapper for `UriBuilder`.

### Extension methods

Enhance existing types with extension methods, all of them under namespace **MoreNet.Foundation.Extensions**.

## Documentation

See the [API documentation](https://ronsun.github.io/MoreNet.Foundation/api) for the full API reference.
