namespace TodoApi.Application;

public interface ITodoService
{
    public Task<dynamic> GetTodoItems();
    public Task<dynamic> PostTodoItem(dynamic todoItemDTO);
    public Task PutTodoItem(long id, dynamic todoItemDTO);
    public Task DeleteTodoItem(long id);
}