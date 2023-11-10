using OrderAutomationsProject.Data.Domain;

namespace OrderAutomationsProject.Data.Helpers;

public static class ProductHelper
{
    public static List<Product> GetProductsWithDividend(List<Product> productList, decimal divident)
    {
        foreach (var product in productList)
        {
            product.Price += (product.Price * divident / 100);
        }
        return productList;
    }
}
