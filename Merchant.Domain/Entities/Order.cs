using System.ComponentModel.DataAnnotations.Schema;
using Merchant.Domain.Base;
using Merchant.Domain.Entities.OrderAttributes;

namespace Merchant.Domain.Entities;

public class Order : BaseEntity<Guid>
{
    public Guid CustomerId { get; set; }
    public ICollection<OrderItem> Items { get; set; }
    public Guid PaymentId { get; set; }
    [ForeignKey(nameof(PaymentId))]
    public Payment Payment { get; set; }
    public string ShipmentCode { get; set; }
    public Guid DiscountId { get; set; }
    [ForeignKey(nameof(DiscountId))] 
    public Discount Discount { get; set; }
    public int Status { get; set; }
}