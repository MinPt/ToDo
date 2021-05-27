namespace ToDo.Data.Entities
{
    public class ToDoItem
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public bool IsCompleted { get; set; }

        public int ToDoListId { get; set; }
        public ToDoList ToDoList { get; set; }
    }
}