using TodoApi.Domain;
using Microsoft.EntityFrameworkCore;

namespace TodoApi.Application;

public class TodoService : ITodoService
{
    private readonly TodoContext _context;

    public TodoService(TodoContext context)
    {
        _context = context;
    }

    public ICollection<TodoItemDTO> TodoItems
    {
        get
        {
            return _context.TodoItems.Select(x => ItemToDTO(x))
                                     .ToList();
        }
    }

    public async Task<ICollection<TodoItemDTO>> GetTodoItems()
    {
        return await _context.TodoItems.Select(x => ItemToDTO(x))
                                       .ToListAsync();
    }

    public async Task<TodoItemDTO> PostTodoItem(TodoItemDTO todoItemDTO)
    {
        var todoItem = new TodoItem
        {
            IsComplete = todoItemDTO.IsComplete,
            Name = todoItemDTO.Name
        };

        _context.TodoItems.Add(todoItem);
        
        await _context.SaveChangesAsync();

        return _context.ItemToDTO(todoItem);
    }

    public async Task PutTodoItem(long id, TodoItemDTO todoItemDTO)
    {
        var todoItem = await _context.TodoItems.FindAsync(id);

        if (todoItem is null)
        {
            throw new KeyNotFoundException();
        }

        todoItem.Name = todoItemDTO.Name;
        todoItem.IsComplete = todoItemDTO.IsComplete;

        await _context.SaveChangesAsync();
    }
    
    public async Task DeleteTodoItem(long id)
    {
        var todoItem = await _context.TodoItems.FindAsync(id);
        
        if (todoItem is null)
        {
            throw new KeyNotFoundException();
        }

        _context.TodoItems.Remove(todoItem);
        await _context.SaveChangesAsync();
    }

    private TodoItemDTO ItemToDTO(TodoItem item)
    {
        return _context.ItemToDTO(item);
    }
}
