using NRedux.TodoApp.Reducers;

namespace NRedux.TodoApp.Actions {
    public class SetVisibilityFilterAction {
        public VisibilityFilter Filter { get; set; }
    }
}