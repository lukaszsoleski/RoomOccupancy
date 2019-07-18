import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppComponent } from './app.component';
import { CampusComponent } from './campus/campus.component';
import { RouterModule } from '@angular/router';
import { appRoutes } from './routes';
import { BuildingComponent } from './building/building.component';

@NgModule({
  declarations: [
    AppComponent,
    CampusComponent,
    BuildingComponent
  ],
  imports: [
    BrowserModule,
    RouterModule.forRoot(appRoutes, { enableTracing: true }), // <-- debugging purposes only),
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
