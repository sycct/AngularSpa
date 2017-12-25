import { Injectable } from '@angular/core';
import { Http, Response, Headers } from '@angular/http';
import { Observable } from 'rxjs/Observable';

import { ProfileViewModel } from '../models/ProfileViewModel';
import { AuthService } from '../security/auth.service';

@Injectable()
export class ProfileService {
    private url: string = 'api/profiles';

    constructor(private http: Http, private authService: AuthService) { }

    getProfileInfo(): Observable<ProfileViewModel> {
        return this.http.get(this.url, { headers: this.authService.authJsonHeaders()})
            .map((resp: Response) => resp.json())
            .catch(this.handleError);
    }

    private handleError(error: Response | any) {
        let errMsg: string;

        if (error instanceof Response) 
        {
            if (error.status == 401) // Unauthorized
            {
                errMsg = '401 - Требуется авторизация';
            }
            else
            {
                const body = error.json() || '';
                const err = body.error || JSON.stringify(body);
                errMsg = `${error.status} - ${error.statusText || ''} ${err}`;
            }
        }
        else
        {
            errMsg = error.message ? error.message : error.toString();
        }

        return Observable.throw(errMsg);
    }
}