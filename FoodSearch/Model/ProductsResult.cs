namespace FoodSearch.Model;

public class ProductsResult
{
    public int Count { get; set; }
    public int Page { get; set; }
    public int PageCount { get; set; }
    public int PageSize { get; set; }

    public List<Product> Products { get; set; }
}
