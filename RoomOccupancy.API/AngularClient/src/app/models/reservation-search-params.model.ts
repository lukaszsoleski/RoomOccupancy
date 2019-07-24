import {RoomType} from './room-type.model';
import { Building } from './building.model';
import { Faculty } from './faculty.model';
import { Equipment } from './equipment.model';
export class ReservationSearchParams {
    building: Building;
    date: Date;
    faculty: Faculty;
    roomType: RoomType;
    equipment: Equipment;
}
