﻿namespace Merchant.Domain.Interfaces;

public interface IEntity
{
    public DateTime CreateDate { get; set; }
    public DateTime UpdateDate { get; set; }
    public bool IsDeleted { get; set; }
}