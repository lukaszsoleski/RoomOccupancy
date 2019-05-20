using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace RoomOccupancy.Domain.Entities.Campus
{
    public class Disponent : IEntity
    {
        public int Id { get; set; }
        /// <summary>
        /// Optional name if other disponent. 
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Id of the related room disponent. It may be faculty, user or other. 
        /// </summary>
        public int? RelatedEntityId { get; set; }
        public DisponentEntityType DisponentType { get; set; }
        
        public Type GetDisponentType()
        {
            Type type = null; 
            switch(DisponentType)
            {
                case DisponentEntityType.Faculty: type = typeof(Faculty);
                    break;
                //TODO: Add lecturer
            }
            return type;
        }
    }
    public enum DisponentEntityType
    {
        Faculty = 1,
        Lecturer,
        Other
    }
}
