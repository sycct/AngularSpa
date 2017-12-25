import { Component } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { Router } from '@angular/router';

import { Http } from '@angular/http';
import { AuthService } from '../security/auth.service';

@Component({
    selector: 'my-app',
    templateUrl: '/partial/appComponent'
})
export class AppComponent {
    public constructor(public router: Router, private titleService: Title, public http: Http, private authService: AuthService) { }
 
    angularClientSideData = 'Angular';
 
    public setTitle(newTitle: string) {
        this.titleService.setTitle(newTitle);
    }

    isLoggedIn()
    {
        return this.authService.isLoggedIn();
    }

    public logout()
    {
        // we cannot revocate token, so just clear token in browser
        this.authService.logout();

        // return to 'home' page
        this.router.navigate(['']);
    }
}
