using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RoomOccupancy.API.Middleware
{
    public class ErrorResponce
    {
        public ErrorResponce()
        {
            Errors = new List<string>();
            Message = string.Empty;
        }
        public string Message { get; set; }

        public List<string> Errors { get; set; }
    }

}
