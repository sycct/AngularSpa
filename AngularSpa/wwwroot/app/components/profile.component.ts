import { Component } from '@angular/core';

import { AuthService } from '../security/auth.service';
import { ProfileService } from '../services/profile.service';
import { ProfileViewModel } from '../models/ProfileViewModel';

@Component({
    selector: 'profile',
    templateUrl: '/partial/profileComponent'
})
export class ProfileComponent {
    profileViewModel: ProfileViewModel;
    errorMessage: string;

    public constructor(private profileService: ProfileService, private authService: AuthService) {  }

    ngOnInit() {
        if (this.isLoggedIn()) {
            this.GetProfile();
        }
    }

    GetProfile() {
        this.profileService.getProfileInfo()
            .subscribe((data: ProfileViewModel) => this.profileViewModel = data, error => this.errorMessage = <any>error);
    }

    isLoggedIn() {
        return this.authService.isLoggedIn();
    }
}
