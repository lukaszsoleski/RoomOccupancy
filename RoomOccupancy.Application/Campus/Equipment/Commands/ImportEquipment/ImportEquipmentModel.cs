using System;
using System.Collections.Generic;
using System.Text;

namespace RoomOccupancy.Application.Campus.Equipment.Commands.ImportEquipment
{
    public class ImportEquipmentModel
    {
        public int Floor { get; set; }
        public int BuildingNo { get; set; }
        public string RoomNo { get; set; }

        public string Equipment { get; set; }
        public int Count { get; set; }
    }
}
