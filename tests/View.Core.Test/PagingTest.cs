using System;
using System.Collections.Generic;
using System.Linq;
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
            List<int> nums = new() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };
            IQueryable<int> query = nums.AsQueryable();

            IPagedQueryable<int> pageQuery = query.AsPagedQueryable(2, 3);
            IPagedList<int> pagedList = pageQuery.ToPagedList();
            IPagedResult<int> pagedResult = pagedList.ToPagedResult();

            var list = nums.Where(x => x % 2 == 0).ToPagedList(2, 3);

            Assert.True(pagedList.Count == nums.Count);
            Assert.True(pagedList.CurrentCount == 3);
            Assert.True(pagedResult.Data.Count() == 3);
            Assert.True(pagedResult.Data.Count() == pagedResult.PagedInfo.Size);
        }
    }
}
