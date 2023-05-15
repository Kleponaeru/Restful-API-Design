using BookStoreApi.Models;
using BookStoreApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http;

namespace BookStoreApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class BooksController : ControllerBase
{
    private readonly BooksService _booksService;

    public BooksController(BooksService booksService) =>
        _booksService = booksService;
    /// <summary>
    /// Get all BooksItem data.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <response code="400">If the item is null</response>
    /// <response code="401">If the user is not authorized to access the resource</response>
    /// <response code="404">If the resource could not be found</response>
    /// <response code="500">If there was an internal server error</response>
    ///<remarks>
    /// Sample request:
    ///
    ///     GET /BookStore
    ///     {
    ///        "id": 1,
    ///        "name": "Item #1",
    ///        "isComplete": true
    ///     }
    ///
    /// </remarks>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<List<Book>> Get() =>
        await _booksService.GetAsync();

    /// <summary>
    /// Get specific BooksItem.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <response code="400">If the item is null</response>
    /// <response code="401">If the user is not authorized to access the resource</response>
    /// <response code="404">If the resource could not be found</response>
    /// <response code="500">If there was an internal server error</response>
     /// <remarks>
    /// Sample request:
    ///
    ///     GET /BookStore
    ///     {
    ///        "id": "string
    ///     }
    ///
    /// </remarks>
    [HttpGet("{id:length(24)}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Book>> Get(string id)
    {
        var book = await _booksService.GetAsync(id);

        if (book is null)
        {
            return NotFound();
        }

        return book;
    }
    /// <summary>
    /// Creates a BooksItem.
    /// </summary>
    /// <param name="item"></param>
    /// <returns>A newly created BooksItem</returns>
    /// <remarks>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /BookStore
    ///     {
    ///        "id": "string,
    ///        "BookName": "Item #1",
    ///        "Price": 0,
    ///        "Category": "Cateory #1",
    ///        "Author" : "Author #1";
    ///     }
    ///
    /// </remarks>
    /// </remarks>
    /// <response code="201">Returns the newly created item</response>
    /// <response code="400">If the item is null</response>
    /// <response code="401">If the user is not authorized to access the resource</response>
    /// <response code="404">If the resource could not be found</response>
    /// <response code="500">If there was an internal server error</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Post(Book newBook)
    {
        try
        {
            if (newBook == null)
            {
                return BadRequest();
            }

            await _booksService.CreateAsync(newBook);

            return CreatedAtAction(nameof(Get), new { id = newBook.Id }, newBook);
        }

        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
            "Error retrieving data from the database");
        }
    }

    /// <summary>
    /// Update specific BooksItem.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <response code="400">If the item is null</response>
    /// <response code="401">If the user is not authorized to access the resource</response>
    /// <response code="404">If the resource could not be found</response>
    /// <response code="500">If there was an internal server error</response>
    /// <remarks>
    /// Sample request:
    ///
    ///     PUT /BookStore
    ///     {
    ///        "id": "string,
    ///        "BookName": "Item #1",
    ///        "Price": 0,
    ///        "Category": "Cateory #1",
    ///        "Author" : "Author #1";
    ///     }
    ///
    /// </remarks>
    [HttpPut("{id:length(24)}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update(string id, Book updatedBook)
    {
        var book = await _booksService.GetAsync(id);

        if (book is null)
        {
            return NotFound();
        }

        updatedBook.Id = book.Id;

        await _booksService.UpdateAsync(id, updatedBook);

        return NoContent();
    }
    /// <summary>
    /// Deletes a specific BooksItem.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <response code="400">If the item is null</response>
    /// <response code="401">If the user is not authorized to access the resource</response>
    /// <response code="404">If the resource could not be found</response>
    /// <response code="500">If there was an internal server error</response>
     /// <remarks>
    /// Sample request:
    ///
    ///     DELETE /BookStore
    ///     {
    ///        "id": "string,
    ///        "BookName": "Item #1",
    ///        "Price": 0,
    ///        "Category": "Cateory #1",
    ///        "Author" : "Author #1";
    ///     }
    ///
    /// </remarks>
    [HttpDelete("{id:length(24)}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(string id)
    {
        var book = await _booksService.GetAsync(id);

        if (book is null)
        {
            return NotFound();
        }

        await _booksService.RemoveAsync(id);

        return NoContent();
    }
}