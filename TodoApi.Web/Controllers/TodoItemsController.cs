using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Application;
using TodoApi.Domain;

namespace TodoApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TodoItemsController : ControllerBase
{
    private readonly ITodoService _todoService;

    public TodoItemsController(ITodoService todoService)
    {
        _todoService = todoService;
    }

    // GET: api/TodoItems
    [HttpGet]
    public async Task<ICollection<TodoItemDTO>> GetTodoItems()
    {
        return await _todoService.GetTodoItems();
    }

    // GET: api/TodoItems/5
    [HttpGet("{id}")]
    public TodoItemDTO GetTodoItem(long id)
    {
        var todoItem = _todoService.TodoItems.Where(x => x.Id == id)
                                             .FirstOrDefault();
        return todoItem;
    }
    

    // POST: api/TodoItems
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult> PostTodoItem(TodoItemDTO todoItemDTO)
    {
        var todoItem = await _todoService.PostTodoItem(todoItemDTO);
        return CreatedAtAction(nameof(GetTodoItem), new { id = todoItem.Id }, todoItem);
    }

    // PUT: api/TodoItems/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutTodoItem(long id, TodoItemDTO todoItemDTO)
    {
        if (id != todoItemDTO.Id)
        {
            return BadRequest();
        }

        try
        {
            await _todoService.PutTodoItem(id, todoItemDTO);
        }
        catch (DbUpdateConcurrencyException) when (!TodoItemExists(id))
        {
            return NotFound();
        }

        return Ok();
    }

    // DELETE: api/TodoItems/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTodoItem(long id)
    {
        await _todoService.DeleteTodoItem(id);
        return Ok();
    }

    private bool TodoItemExists(long id)
    {
        return _todoService.TodoItems.Any(e => e.Id == id);
    }
}