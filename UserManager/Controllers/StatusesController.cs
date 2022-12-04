﻿using Microsoft.AspNetCore.Mvc;
using UserManager.DTOs;
using UserManager.Models;

namespace UserManager.Controllers;
[ApiController]
[Route("statuses")]
public class StatusesController : Controller
{
    #region Actions

    [HttpGet("get-paginated/{page:int}/{itemsPerPage:int}")]
    public ActionResult<GetPaginatedStatusesDto> Paginate(int page = 0, int itemsPerPage = 5)
    {
        var result = new GetPaginatedStatusesDto()
        {
            Statuses = DbMock.statuses.Skip((page /*-1*/) * itemsPerPage).Take(itemsPerPage)
        };
        return Ok(result);
    }

    [HttpGet("get-page-number/{itemsPerPage:int}")]
    public ActionResult<int> GetPageNumber(int itemsPerPage = 5)
    {
        return Ok(Math.Ceiling((float)DbMock.statuses.Count / itemsPerPage));
    }

    [HttpGet("get-by-id/{id:int}")]
    public ActionResult<Status> GetById(int id)
    {
        var status = DbMock.statuses.FirstOrDefault(x => x.Id == id);
        if (status == null) return NotFound();
        return Ok(status);
    }

    [HttpPost("add-new/{name}")]
    public ActionResult AddNew(string name)
    {
        DbMock.statuses.Add(new Status
        {
            Name = name,
            Id = DbMock.statuses.Max((x => x.Id)) + 1
        });
        return Accepted();
    }

    [HttpPut("update/{id:int}/{name}")]
    public ActionResult Update(int id, string name)
    {
        var status = DbMock.statuses.FirstOrDefault(x => x.Id == id);
        if (status == null) return NotFound();
        status.Name = name;
        return Ok();
    }

    [HttpDelete("delete/{id:int}")]
    public ActionResult Delete(int id)
    {
        var status = DbMock.statuses.FirstOrDefault(x => x.Id == id);
        if (status == null) return NotFound();
        DbMock.statuses.Remove(status);
        return Ok();
    }

    [HttpPost("filter/{page:int}/{itemsPerPage:int}")]
    public ActionResult<List<Status>> GetFiltered(string statusFilter, int page = 1, int itemsPerPage = 5)
    {
        IEnumerable<Status> query = new List<Status>(DbMock.statuses);
        return Ok(
            query
                .Where(x => x.Name.Contains(statusFilter))
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
        );
    }
    #endregion
}