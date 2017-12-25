import { Component } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { Router } from '@angular/router';

import { Http } from '@angular/http';
import { AuthService } from '../security/auth.service';
import { LoginViewModel } from '../models/LoginViewModel';

@Component({
    selector: 'login',
    templateUrl: '/partial/loginComponent'
})

export class LoginComponent {
    loginViewModel: LoginViewModel;
    errorMessage: string;

    constructor(public router: Router, private titleService: Title, public http: Http, private authService: AuthService) { }

    ngOnInit() {
        this.loginViewModel = new LoginViewModel();
        this.errorMessage = null;
    }

    public setTitle(newTitle: string) {
        this.titleService.setTitle(newTitle);
    }

    // post the user's login details to server, if authenticated token is returned, then token is saved to session storage
    onLoginClick(event: Event): void {
        event.preventDefault();
        let body = 'username=' + this.loginViewModel.login + '&password=' + this.loginViewModel.password + '&grant_type=password&scope=openid offline_access';

        this.http.post('/connect/token', body, { headers: this.authService.contentHeaders() })
            .subscribe(response =>
            {
                // success, save the token
                this.errorMessage = null;
                this.authService.login(response.json());
                this.router.navigate(['/']);
            },
            error =>
            {
                // failed
                this.errorMessage = "Запрос не был обработан. Что-то нехорошее с сервером.";
            }
            );
    }
}