using System.Collections;
using Microsoft.AspNetCore.Mvc;
using UserManager.DTOs;
using UserManager.Models;
using System.Linq.Expressions;
using System.Linq;

namespace UserManager.Controllers;

[ApiController]
[Route("users")]
public class UsersController : Controller
{
    #region Actions

    [HttpGet("get-paginated/{page:int}/{itemsPerPage:int}")]
    public ActionResult<GetPaginatedUsersDto> Paginate(int page = 0, int itemsPerPage = 5)
    {
        var users = DbMock.Users.Skip((page/* - 1*/) * itemsPerPage).Take(itemsPerPage).Select(x => new UserDto
        {
            Id = x.Id,
            FirstName = x.FirstName,
            LastName = x.LastName,
            PersonalNumber = x.PersonalNumber,
            Email = x.Email,
            DateOfBirth = x.DateOfBirth,
            Category = DbMock.Categories.FirstOrDefault(cat => cat.Id == x.Id),
            Status = DbMock.Statuses.FirstOrDefault(sta => sta.Id == x.Id)
        }).ToList();

        var getPaginatedUsers = new GetPaginatedUsersDto
        {
            Users = users,
        };
        return Ok(getPaginatedUsers);
    }

    [HttpGet("get-by-id/{id:int}")]
    public ActionResult<List<User>> GetById(int id)
    {
        var user = DbMock.Users.FirstOrDefault(x => x.Id == id);
        if (user == null) return NotFound();
        return Ok(new UserDto()
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            PersonalNumber = user.PersonalNumber,
            Email = user.Email,
            DateOfBirth = user.DateOfBirth,
            Category = DbMock.Categories.FirstOrDefault(cat => cat.Id == user.Id),
            Status = DbMock.Statuses.FirstOrDefault(sta => sta.Id == user.Id)
        });
    }

    [HttpPost("add-new")]
    public ActionResult AddNew(User user)
    {
        DbMock.Users.Add(user);
        return Accepted();
    }

    [HttpPut("update")]
    public ActionResult Update(User user)
    {
        var foundUser = DbMock.Users.FirstOrDefault(x => x.Id == user.Id);
        if (foundUser == null) return NotFound();
        foundUser.Email = user.Email;
        foundUser.FirstName = user.FirstName;
        foundUser.LastName = user.LastName;
        foundUser.PersonalNumber = user.PersonalNumber;
        foundUser.CategoryId = user.CategoryId;
        foundUser.StatusId = user.StatusId;
        foundUser.DateOfBirth = user.DateOfBirth;
        return Ok();
    }

    [HttpDelete("delete/{id:int}")]
    public ActionResult Delete(int id)
    {
        var user = DbMock.Users.FirstOrDefault(x => x.Id == id);
        if (user == null) return NotFound();
        DbMock.Users.Remove(user);
        return Ok();
    }

    [HttpPost("filter/{page:int}/{itemsPerPage:int}")]
    public ActionResult<List<UserDto>> GetFiltered(UsersFilterOptions filterOptions, int page = 1, int itemsPerPage = 5)
    {
        IEnumerable<User> query = DbMock.Users;

        if (filterOptions.HasFirstNameFilter) query = query.Where(x => x.FirstName.Contains(filterOptions.FirstNameFilter));
        if (filterOptions.HasLastNameFilter) query = query.Where(x => x.LastName.ToUpper().Contains(filterOptions.LastNameFilter.ToUpper()));
        if (filterOptions.HasEmailFilter) query = query.Where(x => x.Email.ToUpper().Contains(filterOptions.EmailFilter.ToUpper()));
        if (filterOptions.HasCategoryIdFilter) query = query.Where(x => x.CategoryId == filterOptions.CategoryIdFilter);
        if (filterOptions.HasStatusIdFilter) query = query.Where(x => x.StatusId == filterOptions.StatusIdFilter);
        if (filterOptions.HasPersonalNumberFilter) query = query.Where(x => x.PersonalNumber == filterOptions.PersonalNumberFilter);
        if (filterOptions.HasDateOfBirthEndFilter) query = query.Where(x => x.DateOfBirth < filterOptions.DateOfBirthEnd);
        if (filterOptions.HasDateOfBirthStartFilter) query = query.Where(x => x.DateOfBirth > filterOptions.DateOfBirthStart);

        var users = query.Skip((page - 1) * itemsPerPage).Take(itemsPerPage).Select(x => new UserDto
        {
            Id = x.Id,
            FirstName = x.FirstName,
            LastName = x.LastName,
            PersonalNumber = x.PersonalNumber,
            Email = x.Email,
            DateOfBirth = x.DateOfBirth,
            Category = DbMock.Categories.FirstOrDefault(cat => cat.Id == x.Id),
            Status = DbMock.Statuses.FirstOrDefault(sta => sta.Id == x.Id)
        });

        var result = new GetPaginatedUsersDto()
        {
            Users = users
        };

        return Ok(result);
    }

    #endregion
}