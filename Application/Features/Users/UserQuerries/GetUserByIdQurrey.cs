using Application.Dtos.UserDto.Users;
using Application.Interfaces.Repositorys;
using Domain.Entites;
using MediatR;
using Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Users.UserQuerries;

public class GetUserByIdQurrey : IRequest<Result<GetUserDTO>>
{
    public int Id { get; set; }
    public GetUserByIdQurrey(int id)
    {
        Id = id;
    }
}

internal class GetUserByIdQureyHandler : IRequestHandler<GetUserByIdQurrey, Result<GetUserDTO>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetUserByIdQureyHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<GetUserDTO>> Handle(GetUserByIdQurrey request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Repository<User,int>().GetByIdAsync(request.Id);

        if (user == null)
        {
            return Result<GetUserDTO>.BadRequest("User Not Found");
        }

        var result = new GetUserDTO
        {
           Id = request.Id,
           FirstName = user.FirstName,
           LastName = user.LastName,
           Email = user.Email,
           Password = user.Password,
           PhoneNumber = user.PhoneNumber,
           IsActive = user.IsActive,
           CreateDate = user.CreateDate,
           CreatedBy = user.CreatedBy,
           UpdateDate = user.UpdateDate,
           UpdatedBy = user.UpdatedBy,
        };

        return Result<GetUserDTO>.Success(result, "User..............");
    }
}


