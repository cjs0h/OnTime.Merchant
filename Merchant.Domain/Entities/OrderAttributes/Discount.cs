using Merchant.Domain.Base;

namespace Merchant.Domain.Entities.OrderAttributes;

public class Discount : BaseEntity<Guid>
{
    public int Type { get; set; }
    public long Value { get; set; }
}