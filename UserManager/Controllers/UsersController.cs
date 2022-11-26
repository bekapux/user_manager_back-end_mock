using Microsoft.AspNetCore.Mvc;
using UserManager.DTOs;
using UserManager.Models;

namespace UserManager.Controllers;

[ApiController]
[Route("users")]
public class UsersController : Controller
{
    #region Actions

    [HttpGet("get-paginated/{page:int}/{itemsPerPage:int}")]
    public ActionResult<List<User>> Paginate(int page = 1, int itemsPerPage = 5)
    {
        return Ok(DbMock.users.Skip((page - 1) * itemsPerPage).Take(itemsPerPage).Select(x => new UserDto
        {
            Id = x.Id,
            FirstName = x.FirstName,
            LastName = x.LastName,
            PersonalNumber = x.PersonalNumber,
            Email = x.Email,
            DateOfBirth = x.DateOfBirth,
            Category = DbMock.categories.FirstOrDefault(cat => cat.Id == x.Id),
            Status = DbMock.statuses.FirstOrDefault(sta => sta.Id == x.Id)
        }));
    }

    [HttpGet("get-by-id/{id:int}")]
    public ActionResult<List<User>> GetById(int id)
    {
        var user = DbMock.users.FirstOrDefault(x => x.Id == id);
        if (user == null) return NotFound();
        return Ok(new UserDto()
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            PersonalNumber = user.PersonalNumber,
            Email = user.Email,
            DateOfBirth = user.DateOfBirth,
            Category = DbMock.categories.FirstOrDefault(cat => cat.Id == user.Id),
            Status = DbMock.statuses.FirstOrDefault(sta => sta.Id == user.Id)
        });
    }

    [HttpPost("add-new")]
    public ActionResult AddNew(User user)
    {
        DbMock.users.Add(user);
        return Accepted();
    }

    [HttpPut("update")]
    public ActionResult Update(User user)
    {
        var foundUser = DbMock.users.FirstOrDefault(x => x.Id == user.Id);
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
        var user = DbMock.users.FirstOrDefault(x => x.Id == id);
        if (user == null) return NotFound();
        DbMock.users.Remove(user);
        return Ok();
    }

    [HttpPost("filter/{page:int}/{itemsPerPage}")]
    public ActionResult<List<User>> GetFiltered(UsersFilterOptions filterOptions, int page = 1, int itemsPerPage = 5)
    {
        IEnumerable<User> query = DbMock.users;
        if (filterOptions.HasFirstNameFilter) query = query.Where(x => x.FirstName.Contains(filterOptions.FirstNameFilter));
        if (filterOptions.HasLastNameFilter) query = query.Where(x => x.LastName.Contains(filterOptions.LastNameFilter));
        if (filterOptions.HasEmailFilter) query = query.Where(x => x.Email.Contains(filterOptions.EmailFilter));
        if (filterOptions.HasCategoryIdFilter) query = query.Where(x => x.CategoryId == filterOptions.CategoryIdFilter);
        if (filterOptions.HasStatusIdFilter) query = query.Where(x => x.StatusId == filterOptions.StatusIdFilter);
        if (filterOptions.HasPersonalNumberFilter) query = query.Where(x => x.PersonalNumber == filterOptions.PersonalNumberFilter);
        if (filterOptions.HasDateOfBirthEndFilter) query = query.Where(x => x.DateOfBirth < filterOptions.DateOfBirthEnd);
        if (filterOptions.HasDateOfBirthStartFilter) query = query.Where(x => x.DateOfBirth > filterOptions.DateOfBirthStart);
        return Ok(query.Skip((page - 1) * itemsPerPage).Take(itemsPerPage).Select(x => new UserDto
        {
            Id = x.Id,
            FirstName = x.FirstName,
            LastName = x.LastName,
            PersonalNumber = x.PersonalNumber,
            Email = x.Email,
            DateOfBirth = x.DateOfBirth,
            Category = DbMock.categories.FirstOrDefault(cat => cat.Id == x.Id),
            Status = DbMock.statuses.FirstOrDefault(sta => sta.Id == x.Id)
        }));
    }

    #endregion
}