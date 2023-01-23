using System.ComponentModel.DataAnnotations.Schema;
using Ardalis.GuardClauses;
using Merchant.Domain.Base;
using Merchant.Domain.Entities.OrderAttributes;
using Merchant.Domain.Entities.ProductAttributes;
using Merchant.Domain.Entities.ReferenceNumber;

namespace Merchant.Domain.Entities;

public class Product : BaseEntity<Guid>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal PurchasePrice { get; set; }
    public decimal SellingPrice { get; set; }
    public string? Barcode { get; set; }

    public int UnitId { get; set; }
    [ForeignKey(nameof(UnitId))]
    public Unit Unit { get; set; }
    public int GroupId { get; set; }
    [ForeignKey(nameof(GroupId))]
    public Group Group { get; set; }
    public int CategoryId { get; set; }
    [ForeignKey(nameof(CategoryId))] 
    public Category Category { get; set; }
    public int BrandId { get; set; }
    [ForeignKey(nameof(BrandId))]
    public Brand Brand { get; set; }

    public ICollection<ReferenceNumberValue> ReferenceNumberValues { get; set; }
}