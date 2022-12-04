using Microsoft.AspNetCore.Mvc;
using UserManager.Models;

namespace UserManager.Controllers;
[ApiController]
[Route("statuses")]
public class StatusesController : Controller
{
    #region Actions

    [HttpGet("get-paginated/{page:int}/{itemsPerPage:int}")]
    public ActionResult<List<Status>> Paginate(int page = 0, int itemsPerPage = 5)
    {
        return Ok(DbMock.statuses.Skip((page/*-1*/) * itemsPerPage).Take(itemsPerPage));
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

    #endregion

}