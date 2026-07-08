using Application.Commons.Mappings;
using Application.Dtos.UserDto.Commons;
using Domain.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos.UserDto.Users;

public class GetUserDTO : BaseDto,IMapFrom<User>
{
   
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public long PhoneNumber { get; set; }
}
