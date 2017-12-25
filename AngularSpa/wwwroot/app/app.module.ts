import { NgModule, enableProdMode } from '@angular/core';
import { BrowserModule, Title } from '@angular/platform-browser';
import { APP_BASE_HREF, Location } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';

import { routing, routedComponents } from './app.routing';
import { AppComponent } from './components/app.component';
import { AuthService } from './security/auth.service';
import { ProfileService } from './services/profile.service';

import './rxjs-operators';

// enableProdMode();

@NgModule({
    imports: [BrowserModule, FormsModule, HttpModule, routing],
    declarations: [AppComponent, routedComponents],
    bootstrap: [AppComponent],
    providers: [
        AuthService,
        ProfileService,
        Title,
        { provide: APP_BASE_HREF, useValue: '/' }]
})
export class AppModule { }