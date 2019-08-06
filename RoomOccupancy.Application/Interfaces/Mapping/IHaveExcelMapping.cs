using Ganss.Excel;
using System;
using System.Collections.Generic;
using System.Text;

namespace RoomOccupancy.Application.Interfaces
{
                
    public interface IHaveExcelMapping
    {
        void SetExcelMapping(ExcelMapper excelMapper);
    }
}
