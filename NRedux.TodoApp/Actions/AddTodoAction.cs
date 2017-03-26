using System;

namespace NRedux.TodoApp.Actions {
    public class AddTodoAction {
        public String Message { get; set; }
        public AddTodoAction(String message) {
            Message = message;
        }
    }
}