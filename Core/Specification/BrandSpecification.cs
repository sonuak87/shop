using System;
using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specification;

public class BrandSpecification : BaseSpecification<Product, string>
{
    public BrandSpecification()
    {
        AddSelect(x => x.Brand);
        ApplyDistinct();
    }
}
