using System.ComponentModel.DataAnnotations.Schema;
using Merchant.Domain.Base;

namespace Merchant.Domain.Entities.OrderAttributes;

public class OrderItem : BaseEntity<Guid>
{
    public Guid ProductId { get; set; }
    [ForeignKey(nameof(ProductId))] 
    public Product Product { get; set; }
    public int ProductQuantity { get; set; }
    public decimal ProductPrice { get; set; }
}