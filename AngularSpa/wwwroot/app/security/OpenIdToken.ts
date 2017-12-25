import { Component } from '@angular/core';

export class OpenIdToken {
    scope: string;
    token_type: string;
    access_token: string;
    expires_in: number;
    refresh_token: string;
    id_token: string;
}