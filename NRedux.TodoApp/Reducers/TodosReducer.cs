using NRedux.TodoApp.Actions;
using NRedux.TodoApp.Models;
using System;
using System.Linq;

namespace NRedux.TodoApp.Reducers {

    public partial class Reducer {
        private static Int32 NextId = 1;

        public static Todo[] Todos(Todo[] state, Object action) {
            if (action is AddTodoAction) {
                var newState = state.ToList();
                newState.Add(Todo(null, action));
                return newState.ToArray();
            }
            if (action is ToggleTodoAction) {
                return state.Select(t => Todo(t, action)).ToArray();
            }
            return state;
        }

        private static Todo Todo(Todo state, Object action) {
            if (state == null) state = new Todo();

            if (action is AddTodoAction) {
                var castAction = (AddTodoAction)action;
                return new Todo {
                    Id = NextId++,
                    Message = castAction.Message
                };
            }

            if (action is ToggleTodoAction) {
                var castAction = (ToggleTodoAction)action;
                if (state.Id != castAction.TodoId) {
                    return state;
                }

                return new Todo(state, !state.Completed);
            }
            return state;
        }
    }
}