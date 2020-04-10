import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { Routes, RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { BeerListComponent } from './data-visualisation/beer-list.component';
import { BeerDetailComponent } from './data-visualisation/beer-detail.component';
import { BeerFormComponent } from './data-manipulations/beer-form.component';
import { BeerCreateComponent } from './data-manipulations/beer-create.component';
import { BeerEditComponent } from './data-manipulations/beer-edit.component';
import { AboutComponent } from './common-pages/about.component';
import { NotFoundComponent } from './common-pages/not-found.component';

import { DataService } from './server-communication/data.service';
 
const appRoutes: Routes = [
    { path: '', component: BeerListComponent },
    { path: 'beer/:id', component: BeerDetailComponent },
    { path: 'create', component: BeerCreateComponent },
    { path: 'edit/:id', component: BeerEditComponent },
    { path: 'about', component: AboutComponent },
    { path: '**', component: NotFoundComponent } 
]; 
 
@NgModule({
    imports: [BrowserModule, FormsModule, HttpClientModule, RouterModule.forRoot(appRoutes)],
    declarations: [AppComponent, BeerListComponent, BeerDetailComponent, BeerCreateComponent, BeerEditComponent,
        BeerFormComponent, AboutComponent, NotFoundComponent],
    providers: [DataService], 
    bootstrap: [AppComponent]
})
export class AppModule { }