using Ardalis.GuardClauses;
using Merchant.Domain.Base;

namespace Merchant.Domain.Entities.ProductAttributes;

public class Group : BaseEntity<int>
{
    public string Name { get; set; }
}