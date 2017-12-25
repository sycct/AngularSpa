import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { Router } from '@angular/router';
import { Http } from '@angular/http';

import { AuthService } from '../security/auth.service';
import { RegisterViewModel } from '../models/RegisterViewModel';

@Component({
    selector: 'register',
    templateUrl: '/partial/registerComponent'
})

export class RegisterComponent
{
    registerViewModel: RegisterViewModel;
    errorMessage: string;

    constructor(public router: Router, private titleService: Title, public http: Http, private authService: AuthService) { }

    ngOnInit() {
        this.registerViewModel = new RegisterViewModel();
        this.errorMessage = null;
    }

    setTitle(newTitle: string) {
        this.titleService.setTitle(newTitle);
    }

    register(event: Event): void {
        event.preventDefault();
        let body = {
            'login': this.registerViewModel.login,
            'email': this.registerViewModel.email,
            'password': this.registerViewModel.password,
            'fullName': this.registerViewModel.fullName
        };

        this.http.post('/Account/Register', JSON.stringify(body), { headers: this.authService.jsonHeaders() })
            .subscribe(response => {
                if (response.status == 200)
                {
                    this.router.navigate(['/login']);
                }
                else
                {
                    console.log(response);
                    console.log(response.json());
                    this.errorMessage = response.json().messages[0];
                }
            },
            error =>
            {
                // failed; TODO: add some nice error handling
                //console.log(error);
                //console.log(error.json());
                this.errorMessage = error.json().messages[0];
            });
    }
}