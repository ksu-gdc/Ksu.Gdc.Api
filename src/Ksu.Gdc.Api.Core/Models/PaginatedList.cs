using System;
using System.Collections.Generic;

using Ksu.Gdc.Api.Core.Exceptions;

namespace Ksu.Gdc.Api.Core.Models
{
    public class PaginatedList
    {
        public int Total { get; protected set; }

        public static bool IsValid(int pageNumber, int pageSize)
        {
            if (pageNumber < 0 || pageSize < 0)
            {
                throw new PaginationException("The 'pageNumber' and 'pageSize' query parameters must be greater than 0.");
            }
            if (pageNumber == 0)
            {
                if (pageSize == 0)
                {
                    return false;
                }
                else
                {
                    throw new PaginationException("The 'pageNumber' and 'pageSize' query parameters are both required if one is given.");
                }
            }
            else if (pageSize == 0)
            {
                throw new PaginationException("The 'pageNumber' and 'pageSize' query parameters are both required if one is given.");
            }
            return true;
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
