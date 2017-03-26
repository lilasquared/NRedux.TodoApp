using System;

namespace NRedux.TodoApp.Models {
    public class Todo {
        public Int32 Id { get; set; }
        public String Message { get; set; }
        public Boolean Completed { get; set; }

        public Todo() { }

        public Todo(Todo old, Boolean completed) {
            Id = old.Id;
            Message = old.Message;
            Completed = completed;
        }
    }
}