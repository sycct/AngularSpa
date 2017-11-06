import { Injectable } from '@angular/core';
import { Http, Response, Headers } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { OrderInfo } from '../models/OrderInfo';

@Injectable()
export class OrdersService
{
    private url: string = 'api/orders';

    constructor(private http: Http) { }

    getSampleData(): Observable<OrderInfo>
    {
        return this.http.get(this.url)
            .map((resp: Response) => resp.json())
            .catch(this.handleError);
    }

    addSampleData(orderInfo: OrderInfo): Observable<OrderInfo>
    {
        let headers = new Headers(
        {
            'Content-Type': 'application/json'
        });

        return this.http
            .post(this.url, JSON.stringify(orderInfo), { headers: headers })
            .map((resp: Response) => resp.json())
            .catch(this.handleError);
    }

    private handleError(error: Response | any)
    {
        let errMsg: string;

        if (error instanceof Response)
        {
            const body = error.json() || '';
            const err  = body.error || JSON.stringify(body);
            errMsg = `${error.status} - ${error.statusText || ''} ${err}`;
        } else
        {
            errMsg = error.message ? error.message : error.toString();
        }

        console.error(errMsg);

        return Observable.throw(errMsg);
    }
}