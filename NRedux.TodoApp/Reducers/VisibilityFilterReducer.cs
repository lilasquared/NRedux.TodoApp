using NRedux.TodoApp.Actions;
using System;

namespace NRedux.TodoApp.Reducers {

    public enum VisibilityFilter {
        All,
        Completed,
        Active
    }

    public partial class Reducer {
        public static VisibilityFilter VisibilityFilter(VisibilityFilter state, Object action) {
            if (action is SetVisibilityFilterAction) {
                var castAction = (SetVisibilityFilterAction)action;
                return castAction.Filter;
            }
            return state;
        }
    }
}