using Ambev.DeveloperEvaluation.Common.Infraestructure;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

public interface ISaleRepository : IRepository<Sale>, ISearchData<Sale>
{
}