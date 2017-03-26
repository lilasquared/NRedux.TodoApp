using System;
using System.Collections.Generic;

namespace NRedux.TodoApp {
    public static class IEnumerableExtensions {
        public static void ForEach<T>(this IEnumerable<T> array, Action<T> action) {
            foreach (var item in array) {
                action(item);
            }
        }
    }
}