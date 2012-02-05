﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Contendio.Sql
{
    public static class ArrayUtils
    {
        public static IList<TEntity> MoveItemBefore<TEntity>(IList<TEntity> items, int moveIndex, int moveBeforeIndex)
        {
            var newList = new List<TEntity>(items);
            var item = newList[moveIndex];
            newList.RemoveAt(moveIndex);

            if (moveBeforeIndex > moveIndex) moveBeforeIndex--;

            newList.Insert(moveBeforeIndex, item);
            return newList;
        }
    }
}
