using Ambev.DeveloperEvaluation.Application.Users.DeleteUser;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Mappings.Users;

public class DeleteUserRequestProfile : Profile
{
    public DeleteUserRequestProfile()
    {
        CreateMap<DeleteUserRequestProfile, DeleteUserCommand>();
    }
}