import { BuildingComponent } from './building/building.component';
import { CampusComponent } from './campus/campus.component';
import { Routes } from '@angular/router';
import { RoomsComponent } from './campus/rooms/rooms.component';

export const appRoutes: Routes = [
  {
    path: 'campus', component: CampusComponent,
  },
  {
    path: 'building/:id', component: BuildingComponent
  },
  {
    path: 'campus/rooms', component: RoomsComponent
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
