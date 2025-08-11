using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnWellUniversity.Application.Common.Paginations
{
    public record DynamicQuery
    {

        /// <summary>
        /// Set filters value from client as example "Department:IT;Status:Active"
        /// </summary>
        public string? Filter { get; set; }

        /// <summary>
        /// Sort by a specific field, e.g., "Name", "CreatedDate"
        /// </summary>
        public string? SortBy { get; set; }

        /// <summary>
        /// Gets or sets the sort direction for the operation. Default is "asc" for ascending order. User can set it to "desc" for descending order.
        /// </summary>
        /// <remarks>Ensure that the value is either "asc" or "desc" to avoid unexpected behavior.</remarks>
        public string? Direction { get; set; } = "asc";

        /// <summary>
        /// Gets or sets the current page number for pagination.
        /// </summary>
        public int PageNumber { get; set; } = 1;


        /// <summary>
        /// Gets or sets the number of items to be displayed per page. Default is 25.
        /// </summary>
        public int PageSize { get; set; } = 25;
    }
}
