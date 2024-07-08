﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Hexagonal.Common.Pagging
{
    [DataContract]
    public record PageResponse<T>
    {
        [DataMember]
        public int CurrentPage { get; init; }

        [DataMember]
        public int PageSize { get; init; }

        [DataMember]
        public int TotalPages
        {
            get
            {
                if (TotalItems == 0 || PageSize == 0)
                    return 0;

                return (int)Math.Ceiling((double)TotalItems / PageSize);
            }
        }

        [DataMember]
        public int TotalItems { get; init; }

        [DataMember]
        public IEnumerable<T> Items { get; init; }

        public PageResponse<T2> ConvertItems<T2>(Func<T, T2> converter)
        {
            return new PageResponse<T2>
            {
                CurrentPage = CurrentPage,
                PageSize = PageSize,
                TotalItems = TotalItems,
                Items = Items.Select(converter)
            };
        }
    }
}
