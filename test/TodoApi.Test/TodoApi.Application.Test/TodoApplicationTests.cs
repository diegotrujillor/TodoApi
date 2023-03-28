using TodoApi.Application;
using TodoApi.Domain;

namespace TodoApi.Test;

public class TodoApplicationTests
{
    [Fact]
    public async void Add_TodoItem_ShouldReturnValidItem()
    {
        // Arrange
        ITodoService service = new TodoApplicationMock();

        // Act
        var actual = await service.PostTodoItem(new()
        {
            Id = new Random().Next(),
            Name = $"Test 1",
            IsComplete = true
        });

        // Assert
        Assert.Equal(typeof(TodoItemDTO), actual.GetType());
    }

    [Fact]
    public async void Update_TodoItem_ShouldPassValidation()
    {
        // Arrange
        ITodoService service = new TodoApplicationMock();
        var existing = service.TodoItems.FirstOrDefault();

        // Act
        await service.PutTodoItem(existing.Id, new()
        {
            Name = $"Test updated 1",
            IsComplete = true
        });

        // Assert
        Assert.False(existing.Id == 0);
    }

    [Fact]
    public async void Delete_TodoItem_ShouldValidateListLessOneItem()
    {
        // Arrange
        ITodoService service = new TodoApplicationMock();
        var existing = service.TodoItems.FirstOrDefault();

        // Act
        await service.DeleteTodoItem(existing.Id);

        // Assert
        Assert.True(service.TodoItems.Count == 8);
    }

    [Fact]
    public async void Read_TodoItemList_ShouldReturnNotNull()
    {
        // Arrange
        ITodoService service = new TodoApplicationMock();

        // Act
        var actual = await service.GetTodoItems();

        // Assert
        Assert.True(actual is not null);
    }
}