using System;

namespace NRedux.TodoApp.Reducers {
    public partial class Reducer {
        public static Boolean InvalidAction(Boolean state, Object action) {
            if (action is Actions.InvalidAction) {
                return true;
            }
            return false;
        }
    }
}