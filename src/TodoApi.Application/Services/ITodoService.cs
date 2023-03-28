namespace TodoApi.Application;

using TodoApi.Domain;

public interface ITodoService
{
    ICollection<TodoItemDTO> TodoItems { get; }
    Task<ICollection<TodoItemDTO>> GetTodoItems();
    Task<TodoItemDTO> PostTodoItem(TodoItemDTO todoItemDTO);
    Task PutTodoItem(long id, TodoItemDTO todoItemDTO);
    Task DeleteTodoItem(long id);
}