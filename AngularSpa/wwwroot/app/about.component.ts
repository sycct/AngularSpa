import { Component, OnInit } from '@angular/core';
import { OrdersService } from './services/Orders.services';
import { OrderInfo } from './models/OrderInfo';

@Component
({
    selector: 'my-about',
    templateUrl: '/partial/aboutComponent'
})

export class AboutComponent implements OnInit
{
    orderInfo: OrderInfo = null;
    errorMessage: string;

    constructor(private ordersService: OrdersService) { }

    ngOnInit()
    {
        this.getOrder();
    }

    getOrder()
    {
        this.ordersService.getSampleData()
            .subscribe((data: OrderInfo) => this.orderInfo = data, error => this.errorMessage = <any>error);
    }

    addOrder(event: Event):void
    {
        event.preventDefault();

        if (!this.orderInfo)
            return;

        this.ordersService.addSampleData(this.orderInfo)
            .subscribe((data: OrderInfo) => this.orderInfo = data, error => this.errorMessage = <any>error);
    }
}