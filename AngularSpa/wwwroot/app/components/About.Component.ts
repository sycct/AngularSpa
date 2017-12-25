import { Component, OnInit } from '@angular/core';

@Component
({
    selector: 'my-about',
    templateUrl: '/partial/aboutComponent'
})

export class AboutComponent implements OnInit
{
    errorMessage: string;

    constructor() { }

    ngOnInit()
    {
        
    }
}