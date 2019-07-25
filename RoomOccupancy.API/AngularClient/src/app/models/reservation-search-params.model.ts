import {RoomType} from './room-type.model';
import { Building } from './building.model';
import { Faculty } from './faculty.model';
import { Equipment } from './equipment.model';
export class ReservationSearchParams {
    public building: Building;
    public isAnyBuilding: boolean;
    public date: Date;
    public faculty: Faculty;
    public roomType: RoomType;
    public equipment: Equipment;
    public seats: number;
}
