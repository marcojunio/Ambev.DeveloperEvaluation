namespace Ambev.DeveloperEvaluation.Common.Cache;

public class CacheKeys
{
    private const string SaleKeyPrefix = "Sale:";
    public const string AllSalesKey = "AllSales:";


    private const string CustomerKeyPrefix = "Customer:";
    private const string CompanyKeyPrefix = "Company:";
    private const string ProductKeyPrefix = "Product:";

    public static string GetSaleKey(Guid saleId) => $"{SaleKeyPrefix}{saleId}";
    public static string GetCustomerKey(Guid customer) => $"{CustomerKeyPrefix}{customer}";
    public static string GetProductKey(Guid productId) => $"{ProductKeyPrefix}{productId}";
    public static string GetCompanyKey(Guid customerId) => $"{CompanyKeyPrefix}{customerId}";

    public static string GetAllSalesPrefix(Guid userId) =>
        $"{AllSalesKey}User:{userId}";
    public static string GetAllSalesKey(Guid userId, int pageNumber, int pageSize, string order) =>
        $"{AllSalesKey}User:{userId}:Page:{pageNumber}:PageSize:{pageSize}:Order:{order}";
}