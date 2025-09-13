using System;
public class ProductResponse
{

    public Guid Id { get; set; }
    public string? Name { get; set; }

    public decimal Price { get; set; }

    public List<ProductReviewResponse>? Reviews { get; set; }

    private ProductResponse() { }

    public static ProductResponse FromModel(Product? product)
    {
        if (product is null)
            throw new ArgumentNullException(nameof(product), "can not create a product for  null");
        
        return new ProductResponse{
            Id = product.Id,
            Name = product.Name, 
            Price  = product.Price
        };

    }

    }