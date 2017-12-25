import { Component } from '@angular/core';
import { Injectable } from '@angular/core';
import { Headers } from '@angular/http';
import { OpenIdToken } from './OpenIdToken';


@Injectable()
export class AuthService {

    constructor() { }

    // for requesting secure data using json
    authJsonHeaders() {
        let header = new Headers();
        header.append('Content-Type', 'application/json');
        header.append('Accept', 'application/json');
        header.append('Authorization', 'Bearer ' + localStorage.getItem('access_token'));
        return header;
    }

    // for requesting secure data from a form post
    authFormHeaders() {
        let header = new Headers();
        header.append('Content-Type', 'application/x-www-form-urlencoded');
        header.append('Accept', 'application/json');
        header.append('Authorization', 'Bearer ' + localStorage.getItem('access_token'));
        return header;
    }

    // for requesting unsecured data using json
    jsonHeaders() {
        let header = new Headers();
        header.append('Content-Type', 'application/json');
        header.append('Accept', 'application/json');
        return header;
    }

    // for requesting unsecured data using form post
    contentHeaders() {
        let header = new Headers();
        header.append('Content-Type', 'application/x-www-form-urlencoded');
        header.append('Accept', 'application/json');
        return header;
    }

    // After a successful login, save token data into session storage
    login(responseData: OpenIdToken)
    {
        let access_token: string = responseData.access_token;
        let refresh_token: string = responseData.refresh_token;
        let expires_in: number = responseData.expires_in;


        let now = new Date();
        now.setSeconds(now.getSeconds() + expires_in);

        localStorage.setItem('access_token', access_token);
        localStorage.setItem('refresh_token', refresh_token);
        localStorage.setItem('expires_in', now.toString());
    }

    // called when logging out user; clears tokens from localStorage
    logout()
    {
        localStorage.removeItem('access_token');
        localStorage.removeItem('refresh_token');
        localStorage.removeItem('expires_in');
    }

    isLoggedIn()
    {
        let at = localStorage.getItem('access_token');
        if (at === null)
            return false;

        let expireStr = localStorage.getItem('expires_in');
        if (expireStr === null)
            return false;

        let expire = new Date(expireStr);
        let now = new Date();
        if (now >= expire)
            return false;

        return true;
    }
}