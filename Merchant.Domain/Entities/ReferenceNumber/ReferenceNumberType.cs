using Merchant.Domain.Base;

namespace Merchant.Domain.Entities.ReferenceNumber;

public class ReferenceNumberType : BaseEntity<int>
{
    public string Name { get; set; }
    public int ParentType { get; set; }
}