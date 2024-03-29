﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Contracts.V1.Responses
{
    public class PageResponse<T>
    {
        public IEnumerable<T> Data { get; set; }
        public string NextPage { get; set; }
        public string PreviousPage { get; set; }
        public PageResponse(IEnumerable<T> data)
        {
            Data = data;
        }
    }
}
