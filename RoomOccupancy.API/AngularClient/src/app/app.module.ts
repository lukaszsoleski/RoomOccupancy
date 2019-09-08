import { AuthGuard } from './common/guard/auth.guard';
import { HttpErrorInterceptor } from './common/interceptors/httperror-interceptor';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppComponent } from './app.component';
import { CampusComponent } from './campus/campus.component';
import { RouterModule } from '@angular/router';
import { appRoutes } from './routes';
import { BuildingComponent } from './building/building.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { DatePipe } from '@angular/common';
import {NgxMaterialTimepickerModule} from 'ngx-material-timepicker';
import { NgxSpinnerModule } from 'ngx-spinner';
// date picker formats
import {MAT_MOMENT_DATE_FORMATS, MomentDateAdapter} from '@angular/material-moment-adapter';
// Angular Material Components
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {MatCheckboxModule, MAT_DATE_LOCALE, DateAdapter, MAT_DATE_FORMATS} from '@angular/material';
import {MatButtonModule} from '@angular/material';
import {MatInputModule} from '@angular/material/input';
import {MatAutocompleteModule} from '@angular/material/autocomplete';
import {MatDatepickerModule} from '@angular/material/datepicker';
import {  MatNativeDateModule } from '@angular/material';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatRadioModule} from '@angular/material/radio';
import {MatSelectModule} from '@angular/material/select';
import {MatSliderModule} from '@angular/material/slider';
import {MatSlideToggleModule} from '@angular/material/slide-toggle';
import {MatMenuModule} from '@angular/material/menu';
import {MatSidenavModule} from '@angular/material/sidenav';
import {MatToolbarModule} from '@angular/material/toolbar';
import {MatListModule} from '@angular/material/list';
import {MatGridListModule} from '@angular/material/grid-list';
import {MatCardModule} from '@angular/material/card';
import {MatStepperModule} from '@angular/material/stepper';
import {MatTabsModule} from '@angular/material/tabs';
import {MatExpansionModule} from '@angular/material/expansion';
import {MatButtonToggleModule} from '@angular/material/button-toggle';
import {MatChipsModule} from '@angular/material/chips';
import {MatIconModule} from '@angular/material/icon';
import {MatProgressSpinnerModule} from '@angular/material/progress-spinner';
import {MatProgressBarModule} from '@angular/material/progress-bar';
import {MatDialogModule} from '@angular/material/dialog';
import {MatTooltipModule} from '@angular/material/tooltip';
import {MatSnackBarModule} from '@angular/material/snack-bar';
import {MatTableModule} from '@angular/material/table';
import {MatSortModule} from '@angular/material/sort';
import {MatPaginatorModule} from '@angular/material/paginator';
import { RoomComponent } from './room/room.component';
import { RoomLookupComponent } from './room/components/room-lookup/room-lookup.component';
import { MainNavComponent } from './main-nav/main-nav.component';
import { LayoutModule } from '@angular/cdk/layout';
import { ToastrModule } from 'ngx-toastr';
import { RoomsComponent } from './campus/rooms/rooms.component';
import { ScheduleComponent } from './room/schedule/schedule.component';
import {DaysOfWeekPipe} from './common/pipes/DaysOfWeekPipe';
import { ReservationComponent } from './room/reservation/reservation.component';
import {NgbModule} from '@ng-bootstrap/ng-bootstrap';
import { RegistrationFormComponent } from './users/registration-form/registration-form.component';
import { LoginComponent } from './users/login/login.component';
import { TokenInterceptor } from './common/interceptors/token.interceptor';
import { ProfileComponent } from './users/profile/profile.component';
import { CustomDateAdapter } from './common/adapters/locale-date.adapter';
import { EquipmentComponent } from './room/components/equipment/equipment.component';
import { ReservationsComponent } from './users/reservations/reservations.component';
// TODO: add separate file for imports
@NgModule({
  declarations: [
    AppComponent,
    CampusComponent,
    BuildingComponent,
    RoomComponent,
    RoomLookupComponent,
    MainNavComponent,
    RoomsComponent,
    ScheduleComponent,
    DaysOfWeekPipe,
    ReservationComponent,
    RegistrationFormComponent,
    LoginComponent,
    ProfileComponent,
    EquipmentComponent,
    ReservationsComponent,
  ],
  imports: [
    CommonModule,
    BrowserModule,
    HttpClientModule,
    RouterModule.forRoot(appRoutes),
    FormsModule,
    ReactiveFormsModule,
    NgxMaterialTimepickerModule,
    NgbModule,
    NgxSpinnerModule,
    // Angular Material Components
    BrowserAnimationsModule,
    MatCheckboxModule,
    MatCheckboxModule,
    MatButtonModule,
    MatInputModule,
    MatAutocompleteModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatFormFieldModule,
    MatRadioModule,
    MatSelectModule,
    MatSliderModule,
    MatSlideToggleModule,
    MatMenuModule,
    MatSidenavModule,
    MatToolbarModule,
    MatListModule,
    MatGridListModule,
    MatCardModule,
    MatStepperModule,
    MatTabsModule,
    MatExpansionModule,
    MatButtonToggleModule,
    MatChipsModule,
    MatIconModule,
    MatProgressSpinnerModule,
    MatProgressBarModule,
    MatDialogModule,
    MatTooltipModule,
    MatSnackBarModule,
    MatTableModule,
    MatSortModule,
    MatPaginatorModule,
    LayoutModule,
    // End Angular Material Imports
    ToastrModule.forRoot({
      positionClass: 'toast-bottom-full-width',
      preventDuplicates: true,
      timeOut: 6000,
      maxOpened: 3,
    }), // ToastrModule added
  ],
  providers: [
    MatDatepickerModule,
    { // register error interceptor
      provide: HTTP_INTERCEPTORS,
      useClass: HttpErrorInterceptor,
      multi: true
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptor,
      multi: true
    },
    AuthGuard,
    DatePipe,
    DaysOfWeekPipe,
    {
      provide: MAT_DATE_LOCALE, useValue: 'pl-PL'
    },
    {provide: DateAdapter, useClass: CustomDateAdapter },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
