using Application.Interfaces.Repositorys;
using Domain.Entites;
using MediatR;
using Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Users.UserCommands;

public class DeleteUserCommand : IRequest<Result<string>>
{
    public int Id { get; set; }
    public DeleteUserCommand(int id)
    {
        Id = id;
    }

}
internal class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Result<string>>
{
    private readonly IUnitOfWork _unitofwork;

    public DeleteUserCommandHandler(IUnitOfWork unitofwork)
    {
        _unitofwork = unitofwork;
    }
    public async Task<Result<string>> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
    {
        await _unitofwork.Repository<User,int>().DeleteAsync(command.Id);
        await _unitofwork.Save(cancellationToken);

        return Result<string>.Success("User Deleted Successfully");

    }

}


