using Merchant.Domain.Base;

namespace Merchant.Domain.Entities;

public class Payment : BaseEntity<Guid>
{
    public int Status { get; set; }
    public decimal Amount { get; set; }
    public int Type { get; set; }
    public Guid ProviderId { get; set; }
}