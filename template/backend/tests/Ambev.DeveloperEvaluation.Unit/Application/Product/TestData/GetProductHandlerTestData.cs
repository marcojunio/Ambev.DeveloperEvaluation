using Ambev.DeveloperEvaluation.Application.Product.GetProduct;

namespace Ambev.DeveloperEvaluation.Unit.Application.Product.TestData;

public static class GetProductHandlerTestData
{
    public static GetProductQuery GenerateValidCommand()
    {
        return new GetProductQuery(Guid.NewGuid());
    }
}