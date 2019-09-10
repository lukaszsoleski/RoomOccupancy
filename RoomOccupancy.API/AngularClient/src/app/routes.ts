import { ProfileComponent } from './users/profile/profile.component';
import { ScheduleComponent } from './room/schedule/schedule.component';
import { BuildingComponent } from './building/building.component';
import { CampusComponent } from './campus/campus.component';
import { Routes } from '@angular/router';
import { RoomsComponent } from './campus/rooms/rooms.component';
import { RoomComponent } from './room/room.component';
import { RegistrationFormComponent } from './users/registration-form/registration-form.component';
import { LoginComponent } from './users/login/login.component';
import { AuthGuard } from './common/guard/auth.guard';
import { ReservationsComponent } from './users/reservations/reservations.component';
import { FindRoomComponent } from './room/find-room/find-room.component';

export const appRoutes: Routes = [
  {
    path: 'campus', component: CampusComponent,
  },
  {
    path: 'building/:id', component: BuildingComponent
  },
  {
    path: 'room/:id', component: RoomComponent
  },
  {
    path: 'campus/rooms', component: RoomsComponent
  },
  {
    path: 'find-room', component: FindRoomComponent
  },
  {
    path: 'register', component: RegistrationFormComponent
  },
  {
    path: 'login', component: LoginComponent
  },
  {
    path: 'profile', component: ProfileComponent, canActivate: [AuthGuard]
  },
  {
    path: 'user/reservations', component: ReservationsComponent, canActivate: [AuthGuard]
  },
  {
    path: '',
    redirectTo: '/campus',
    pathMatch: 'full'
  },
  {
    path: '**', // catch all routes that do not match the pattern
    redirectTo: '/campus',
    pathMatch: 'full'
  },
];
