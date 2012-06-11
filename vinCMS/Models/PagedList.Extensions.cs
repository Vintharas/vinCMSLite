using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;


namespace vinCMS.Models
{
    public static class ModelExtensions
    {

        public static int GetNextPageIndex<T>(this IPagedList<T> pagedList)
        {
            return pagedList.PageNumber + 1;
        }

        public static int GetPreviousPageIndex<T>(this IPagedList<T> pagedList)
        {
            return pagedList.PageNumber > 0 ? pagedList.PageNumber - 1 : 0;
        }


    }
}