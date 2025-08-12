using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnWellUniversity.Application.Models.Common
{
    public class ApiError
    {
        public string Message { get; set; }
        public string? Details { get; set; }

        public ApiError(string message, string? details = null)
        {
            Message = message;
            Details = details;
        }
    }

}
