import { CampusComponent } from './campus/campus.component';
import { Routes } from '@angular/router';

export const appRoutes: Routes = [
  {
    path: '/campus', component: CampusComponent,
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
