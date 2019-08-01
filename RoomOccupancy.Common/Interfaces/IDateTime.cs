using System;
using System.Collections.Generic;
using System.Text;

namespace RoomOccupancy.Common
{
    public interface IDateTime
    {
        DateTime Now { get; }
    }
}
