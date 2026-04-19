using System.Collections.Generic;
using System.Linq;

namespace Recipe.Lib
{
    public static class CollectionExtensions
    {
        public static void SynchronizeByContent<T>(
            this List<T> internalList,
            IEnumerable<T> newList)
            where T : class
        {
            if (newList == null)
                return;

            var newSet = newList.ToHashSet();

            // Удаляем элементы, которых нет в новом списке (сравнение через Equals)
            internalList.RemoveAll(item => !newSet.Contains(item));

            // Добавляем те, которых еще нет во внутреннем списке
            foreach (T newItem in newList)
            {
                if (!internalList.Contains(newItem))
                    internalList.Add(newItem);
            }
        }
    }
}
