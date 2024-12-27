using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Companies.GetCompany;

public record GetCompanyCommand : IRequest<GetCompanyResult>
{
    public Guid Id { get; }
    
    public GetCompanyCommand(Guid id)
    {
        Id = id;
    }
}
