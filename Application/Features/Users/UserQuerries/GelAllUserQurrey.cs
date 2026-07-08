using Application.Dtos.UserDto.Users;
using Application.Interfaces.Repositorys;
using Domain.Entites;
using MediatR;
using Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Users.UserQuerries;

public class GelAllUserQurrey : IRequest<Result<List<GetUserDTO>>>
{
    internal class GelAllUserQurreyhandler : IRequestHandler<GelAllUserQurrey, Result<List<GetUserDTO>>>
    {
        private readonly IUnitOfWork _unitof;

        public GelAllUserQurreyhandler(IUnitOfWork unitof)
        {
            _unitof = unitof;
        }

        public async Task<Result<List<GetUserDTO>>> Handle(GelAllUserQurrey request, CancellationToken cancellationToken)
        {
            var Users = await _unitof.Repository<User,int>().GetAll();

            List<GetUserDTO> result = Users.Select(x => new GetUserDTO
            {
               FirstName = x.FirstName,
               LastName = x.LastName,
               Email = x.Email,
               Password = x.Password,
               PhoneNumber = x.PhoneNumber,
               IsActive = x.IsActive,
               CreateDate = x.CreateDate,
               CreatedBy = x.CreatedBy,
               UpdateDate = x.UpdateDate,
               UpdatedBy = x.UpdatedBy,
            }).ToList();

            return Result<List<GetUserDTO>>.Success(result, "....User");
        }
    }
}

