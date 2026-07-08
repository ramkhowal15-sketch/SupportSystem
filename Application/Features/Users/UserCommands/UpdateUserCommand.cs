using Application.Commons.Mappings;
using Application.Interfaces.Repositorys;
using Domain.Entites;
using MediatR;
using Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Users.UserCommands;

public class UpdateUserCommand : IRequest<Result<string>> , ICreateMapFrom<User>
{
    public int Id { get; set; }
    public CreateUserCommand CreateUser { get; set; }
    public UpdateUserCommand(int id, CreateUserCommand command)
    {
        Id = id;
        CreateUser = command;
    }
}
internal class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateUserCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<string>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Repository<User,int>().GetByIdAsync(request.Id);

        user.FirstName = request.CreateUser.FirstName;
        user.LastName = request.CreateUser.LastName;
        user.Email = request.CreateUser.Email;
        user.Password = request.CreateUser.Password;
        user.PhoneNumber = request.CreateUser.PhoneNumber;

        await _unitOfWork.Repository<User,int>().PutAsync(request.Id, user);
        await _unitOfWork.Save(cancellationToken);

        return Result<string>.Success("User Updated successfully");

    }
}

