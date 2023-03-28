using TodoApi.Application;
using TodoApi.Domain;

namespace TodoApi.Test
{
    public class TodoApplicationMock : ITodoService
    {
        private IList<TodoItemDTO> todoItems;

        public TodoApplicationMock()
        {
            todoItems = new List<TodoItemDTO>();
            InitializeMockRepository();
        }
        public ICollection<TodoItemDTO> TodoItems => todoItems;

        public async Task DeleteTodoItem(long id)
        {
            var todoItem = todoItems[Convert.ToInt32(id)];
            todoItems.Remove(todoItem);
        }

        public async Task<ICollection<TodoItemDTO>> GetTodoItems()
        {
            return todoItems;
        }

        public async Task<TodoItemDTO> PostTodoItem(TodoItemDTO todoItemDTO)
        {
            todoItems.Add(todoItemDTO);
            return todoItemDTO;
        }

        public async Task PutTodoItem(long id, TodoItemDTO todoItemDTO)
        {
            todoItems[Convert.ToInt32(id)] = todoItemDTO;
        }

        private void InitializeMockRepository()
        {
            for (long index = 1; index < 10; index++)
            {
                todoItems.Add(new()
                {
                    Id = index,
                    Name = $"Test {index}",
                    IsComplete = default
                });
            }
        }
    }
}