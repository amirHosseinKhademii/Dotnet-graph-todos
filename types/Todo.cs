
namespace hot_demo.types;

public record Todo
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; init; }

    public string Title { get; set; }

    public string? Body { get; set; }

    public bool IsCompleted { get; set; }

    public DateTime CreatedDate { get; init; }

    [GraphQLIgnore]
    public string? Author { get; init; }

    public async Task<User> GetUser([Service] Service service, [Parent] Todo todo) => await service.GetUserAsync(todo.Author);

}
