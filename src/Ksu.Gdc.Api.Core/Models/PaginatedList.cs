using System;
using System.Collections.Generic;

namespace Ksu.Gdc.Api.Core.Models
{
    public class PaginatedList
    {
        public int Total { get; protected set; }

        public static bool IsValid(int pageNumber, int pageSize)
        {
            if (pageNumber == 0)
            {
                if (pageSize != 0)
                {
                    throw new ArgumentException();
                }
                return false;
            }
            else
            {
                if (pageNumber < 0 || pageSize < 0)
                {
                    throw new ArgumentException();
                }
                return true;
            }
        }

        protected List<T> Paginate<T>(List<T> list, int pageNumber, int pageSize)
        {
            if (list.Count == 0)
            {
                return new List<T>();
            }
            var index = (pageNumber - 1) * pageSize;
            index = Math.Min(index, list.Count);
            var count = Math.Min(pageSize, list.Count - index);
            return list.GetRange(index, count);
        }
    }

    public class PaginatedList<T> : PaginatedList
    {
        public List<T> Value { get; protected set; }

        public PaginatedList(List<T> list, int pageNumber, int pageSize)
        {
            Total = list.Count;
            List<T> newList = Paginate(list, pageNumber, pageSize);
            Value = newList;
        }
    }
}
