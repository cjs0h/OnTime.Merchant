using System.ComponentModel.DataAnnotations;
using Merchant.Domain.Interfaces;

namespace Merchant.Domain.Base;

public abstract class BaseEntity<TId> : IEntity where TId : struct
{
    [Key] public TId Id { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime UpdateDate { get; set; }
    public bool IsDeleted { get; set; }
}