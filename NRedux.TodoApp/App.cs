using NRedux.TodoApp.Actions;
using NRedux.TodoApp.Models;
using NRedux.TodoApp.Reducers;
using System;
using System.Linq;

namespace NRedux.TodoApp {
    class App {
        private static IStore<AppState> _store;
        static void Main(string[] args) {
            var rootReducer = Redux.CombineReducers<AppState, Reducer>();
            _store = Redux.CreateStore(rootReducer, new AppState());

            var stop = false;

            Console.OutputEncoding = System.Text.Encoding.Unicode;
            while(!stop) {
                Render();
                stop = ReadActions();
            }
        }

        static void Render() {
            RenderHeader();
            RenderTodos();
            RenderActions();
            RenderFilterArray();
            RenderInvalid();
        }

        static void RenderHeader() {
            Console.Clear();
            Console.WriteLine(" __________________________ ");
            Console.WriteLine("|                          |");
            Console.WriteLine("|  NRedux Todos Exammple:  |");
            Console.WriteLine("|__________________________|");
        }

        static void RenderTodos() {
            var todos = _store.GetState().Todos;
            var filter = _store.GetState().VisibilityFilter;
            switch (filter) {
                case VisibilityFilter.All:
                    todos.ForEach(RenderTodo);
                    break;
                case VisibilityFilter.Completed:
                    todos.Where(t => t.Completed).ForEach(RenderTodo);
                    break;
                case VisibilityFilter.Active:
                    todos.Where(t => !t.Completed).ForEach(RenderTodo);
                    break;
                default:
                    todos.ForEach(RenderTodo);
                    break;
            }
        }

        static void RenderTodo(Todo todo) {
            var completedSymbol = todo.Completed ? "X" : String.Empty;
            Console.WriteLine($"{todo.Id}. {todo.Message} {completedSymbol}");
        }

        static void RenderFilterArray() {
            Console.WriteLine();
            var filter = _store.GetState().VisibilityFilter;
            (Enum.GetValues(typeof(VisibilityFilter)) as VisibilityFilter[]).ForEach(value => {
                RenderFilter(filter, value);
            });
            Console.WriteLine();
            Console.WriteLine();
        }

        static void RenderFilter(VisibilityFilter selected, VisibilityFilter filter) {
            if (selected == filter) {
                Console.Write($"({filter})  ");
            }
            else {
                Console.Write($"{filter}  ");
            }
        }

        static void RenderActions() {
            Console.WriteLine();
            Console.WriteLine("(add <message>) Add Todo");
            Console.WriteLine("(toggle <id>) Toggle Todo");
            Console.WriteLine("(show <all, completed, active>) Change Filter");
            Console.WriteLine("(q) Quit");
        }

        static void RenderInvalid() {
            if (_store.GetState().InvalidAction) {
                Console.WriteLine();
                Console.WriteLine("Invalid action.  Please try again!");
                Console.WriteLine();
            }
        }

        static Boolean ReadActions() {
            var action = Console.ReadLine();
            return DoAction(action);
        }

        static Boolean DoAction(String action) {
            var split = action.Split(' ');
            String message;
            switch (split.First()) {
                case "add":
                    message = String.Join(" ", split.Skip(1));
                    _store.Dispatch(new AddTodoAction(message));
                    break;
                case "toggle":
                    if (split.Length < 2 || !Int32.TryParse(split[1], out var todoId)) {
                        _store.Dispatch(new InvalidAction());
                        break;
                    }
                    _store.Dispatch(new ToggleTodoAction { TodoId = todoId });
                    break;
                case "show":
                    if (split.Length < 2 || !Enum.TryParse<VisibilityFilter>(split[1], out var newFilter)) {
                        _store.Dispatch(new InvalidAction());
                        break;
                    }
                    _store.Dispatch(new SetVisibilityFilterAction { Filter = newFilter });
                    break;
                case "q":
                    return true;
                default:
                    _store.Dispatch(new InvalidAction());
                    break;
            }
            return false;
        }
    }
}