using Microsoft.AspNetCore.Mvc;
using UserManager.DTOs;
using UserManager.Models;

namespace UserManager.Controllers;

[ApiController]
[Route("categories")]
public class CategoriesController : Controller
{
    #region Actions

    [HttpGet("get-paginated/{page:int}/{itemsPerPage:int}")]
    public ActionResult<GetPaginatedCategoriesDto> Paginate(int page = 0, int itemsPerPage = 5)
    {
        var paginatedCategories = new GetPaginatedCategoriesDto
        {
            Categories = DbMock.categories.Skip((page/* - 1*/) * itemsPerPage).Take(itemsPerPage)
        };
        return Ok(paginatedCategories);
    }

    [HttpGet("get-page-number/{itemsPerPage:int}")]
    public ActionResult<int> GetPageNumber(int itemsPerPage = 5)
    {
        return Ok(Math.Ceiling((float)DbMock.categories.Count/itemsPerPage));
    }

    [HttpGet("get-by-id/{id:int}")]
    public ActionResult<Category> GetById(int id)
    {
        var category = DbMock.categories.FirstOrDefault(x => x.Id == id);
        if (category == null) return NotFound();
        return Ok(category);
    }

    [HttpPost("add-new/{name}")]
    public ActionResult AddNew(string name)
    {
        DbMock.categories.Add(new Category()
        {
            Name = name,
            Id = DbMock.categories.Max((x => x.Id)) + 1
        });
        return Accepted();
    }

    [HttpPut("update/{id:int}/{name}")]
    public ActionResult Update(int id, string name)
    {
        var category = DbMock.categories.FirstOrDefault(x => x.Id == id);
        if (category == null) return NotFound();
        category.Name = name;
        return Ok();
    }

    [HttpDelete("delete/{id:int}")]
    public ActionResult Delete(int id)
    {
        var category = DbMock.categories.FirstOrDefault(x => x.Id == id);
        if (category == null) return NotFound();
        DbMock.categories.Remove(category);
        return Ok();
    }

    [HttpPost("filter/{page:int}/{itemsPerPage:int}")]
    public ActionResult<List<Category>> GetFiltered(string categoryFilter, int page = 1, int itemsPerPage = 5)
    {
        IEnumerable<Category> query = new List<Category>(DbMock.categories);
        return Ok(
            query
                .Where(x => x.Name.Contains(categoryFilter))
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
        );
    }

    #endregion
}