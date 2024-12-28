using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Companies.DeleteCompany;

public record DeleteCompanyCommand : IRequest<DeleteCompanyResult>
{
    public Guid Id { get; }
    
    public DeleteCompanyCommand(Guid id)
    {
        Id = id;
    }

    public DeleteCompanyCommand()
    {
        
    }
}
