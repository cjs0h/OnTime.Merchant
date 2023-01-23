using System.ComponentModel.DataAnnotations.Schema;
using Merchant.Domain.Base;

namespace Merchant.Domain.Entities.ReferenceNumber;

public class ReferenceNumberValue : BaseEntity<int>
{
    public int TypeId { get; set; }
    [ForeignKey(nameof(TypeId))]
    public ReferenceNumberType ReferenceNumberType { get; set; }
    public string Number { get; set; }
    public string Description { get; set; }
    public string OwnerId { get; set; }
}