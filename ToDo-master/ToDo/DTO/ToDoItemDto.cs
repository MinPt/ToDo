namespace ToDo.DTO
{
    public class ToDoItemDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public bool IsCompleted { get; set; }

        public int ToDoListId { get; set; }
    }
}