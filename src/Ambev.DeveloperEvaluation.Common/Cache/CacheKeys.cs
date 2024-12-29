namespace Ambev.DeveloperEvaluation.Common.Cache;

public class CacheKeys
{
    private const string SaleKeyPrefix = "Sale:";
    private const string AllSalesKey = "AllSales:";

    public static string GetSaleKey(Guid saleId) => $"{SaleKeyPrefix}{saleId}";

    public static string GetAllSalesPrefix(Guid userId) =>
        $"{AllSalesKey}User:{userId}";
    public static string GetAllSalesKey(Guid userId, int pageNumber, int pageSize, string order) =>
        $"{AllSalesKey}User:{userId}:Page:{pageNumber}:PageSize:{pageSize}:Order:{order}";
}