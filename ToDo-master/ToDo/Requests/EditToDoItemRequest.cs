﻿namespace ToDo.Requests
{
    public class EditToDoItemRequest
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public bool IsCompleted { get; set; }
    }
}