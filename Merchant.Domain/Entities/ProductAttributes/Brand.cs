using Ardalis.GuardClauses;
using Merchant.Domain.Base;

namespace Merchant.Domain.Entities.ProductAttributes;

public class Brand : BaseEntity<int>
{
    public string Name { get; set; }
}