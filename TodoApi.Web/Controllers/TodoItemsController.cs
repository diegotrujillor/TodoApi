using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Application;

namespace TodoApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TodoItemsController : ControllerBase
{
    private readonly TodoService _todoService;

    public TodoItemsController(TodoService todoService)
    {
        _todoService = todoService;
    }

    // GET: api/TodoItems
    [HttpGet]
    public async Task<ActionResult> GetTodoItems()
    {
        return await _todoService.GetTodoItems();
    }

    // GET: api/TodoItems/5
    [HttpGet("{id}")]
    public async Task<ActionResult> GetTodoItem(long id)
    {
        var todoItem = await _todoService.TodoItems.FindAsync(id);

        if (todoItem == null)
        {
            return NotFound();
        }

        return todoItem;
    }
    

    // POST: api/TodoItems
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult> PostTodoItem(dynamic todoItemDTO)
    {
        var todoItem = await _todoService.PostTodoItem(todoItemDTO);

        return CreatedAtAction(nameof(GetTodoItem), new { id = todoItem.Id }, todoItem);
    }

    // PUT: api/TodoItems/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutTodoItem(long id, dynamic todoItemDTO)
    {
        if (id != todoItemDTO?.Id)
        {
            return BadRequest();
        }

        try
        {
            await _todoService.PutTodoItem(id, todoItemDTO);
        }
        catch (DbUpdateConcurrencyException) //when (!TodoItemExists(id))
        {
            return NotFound();
        }

        return NoContent();
    }

    // DELETE: api/TodoItems/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTodoItem(long id)
    {
        await _todoService.DeleteTodoItem(id);
        return Ok();
    }

    // private bool TodoItemExists(long id)
    // {
    //     return _todoService.TodoItems.Any(e => e.Id == id);
    // }
}