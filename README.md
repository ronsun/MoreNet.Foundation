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

### Instructions

Use `IInstructable<TInstruction>` when one API needs to serve many similar cases. Without this pattern, some callers may use one large API to get only a small part of the result, which wastes work. Creating a different API for each case can reduce that waste, but it also creates many similar endpoints that are hard to maintain. This pattern keeps common data and filter fields on the root model, and moves optional behavior switches into a nested `InstructionData`. These flags let a caller skip unneeded work, hide unneeded data, or ask for extra handling only when needed. If `Instruction` is `null`, the request uses the normal behavior. Keep instruction properties as simple boolean flags.

Design `TInstruction` very carefully. It should stay small, and each flag should have one clear purpose. Too many flags, or flags that depend on each other, can turn the implementation into many branches that are hard to read, test, and maintain. If the flags start to describe different workflows, it is often better to split the request or the API.

```csharp
public sealed class ProductSummaryRequest : IInstructable<ProductSummaryRequest.InstructionData>
{
    public IList<long> ProductIds { get; set; }

    public InstructionData Instruction { get; set; }

    public sealed class InstructionData
    {
        public bool IgnoreInventory { get; set; }

        public bool IncludeDrafts { get; set; }
    }
}

// Read the optional instruction in the API.
// When Instruction is null, use the normal behavior.
if (request.Instruction?.IgnoreInventory != true)
{
    query = query.Include(item => item.Inventory);
}

if (request.Instruction?.IncludeDrafts != true)
{
    query = query.Where(item => item.IsDraft == false);
}
```

### API response

Use `ApiResponse<T>` when many APIs need the same response shape. Without a shared wrapper, common response fields and format often become inconsistent between endpoints. `ApiResponse<T>` keeps the common part in one place, while `T` handles the changing payload. Use `ApiResponse` as a shortcut when the operation returns no data. Manage status values in one constants class to keep them consistent between APIs.

```csharp
public static class Statuses
{
    public const string Success = "Success";

    public const string BadParameters = "BadParameters";
}

public ApiResponse<ProductSummary> GetProduct(long id)
{
    if (id <= 0)
    {
        return new ApiResponse(Statuses.BadParameters);
    }

    return new ApiResponse<ProductSummary>(Statuses.Success, summary);
}

public ApiResponse DeleteProduct(long id)
{
    return new ApiResponse(Statuses.Success);
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
