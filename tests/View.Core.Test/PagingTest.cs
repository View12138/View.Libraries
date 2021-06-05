using System;
using System.Collections.Generic;
using System.Linq;
using View.Core;
using View.Core.Extensions;
using View.Core.Models.Paging;
using View.Core.Models.Results;
using Xunit;

namespace View.Core.Test
{
    public class PagingTest
    {
        [Fact]
        public void Test()
        {
            List<int> nums = new() { 1, 3, 5, 7, 9, 11, 13, 15, 17, 19, 21 };
            IQueryable<int> query = nums.AsQueryable();
            IPagedQueryable<int> pageQuery = query.AsPagedQueryable(new PagedQueryInfo(2, 3));
            IPagedList<int> pagedList = pageQuery.ToPagedList();
            IPagedResult<int> pagedResult = pagedList.ToPagedResult();

            Assert.True(pagedList.Count == nums.Count);
            Assert.True(pagedList.CurrentCount == 3);
            Assert.True(pagedResult.Data.Count() == 3);
            Assert.True(pagedResult.Data.Count() == pagedResult.PagedInfo.Size);
        }
    }
}
