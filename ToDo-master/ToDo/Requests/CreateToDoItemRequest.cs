namespace ToDo.Requests
{
    public class CreateToDoItemRequest
    {
        public string Text { get; set; }
        public bool IsCompleted { get; set; }
        public int ToDoListId { get; set; }
    }
}