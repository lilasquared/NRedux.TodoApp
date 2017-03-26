using NRedux.TodoApp.Models;
using NRedux.TodoApp.Reducers;
using System;

namespace NRedux.TodoApp {
    public class AppState {
        public Boolean InvalidAction { get; set; }
        public VisibilityFilter VisibilityFilter { get; set; }
        public Todo[] Todos { get; set; }

        public AppState() {
            VisibilityFilter = VisibilityFilter.All;
            Todos = new Todo[0];
        }
    }
}