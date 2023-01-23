using Ardalis.GuardClauses;
using Merchant.Domain.Base;

namespace Merchant.Domain.Entities.ProductAttributes;

public class Unit : BaseEntity<int>
{
    public string Name { get; set; }
    public string ShortName { get; set; }
}