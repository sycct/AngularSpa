import { Routes, RouterModule } from '@angular/router';

import { AboutComponent } from './components/about.component';
import { IndexComponent } from './components/index.component';
import { ContactComponent } from './components/contact.component';
import { LoginComponent } from './components/login.component';
import { RegisterComponent } from './components/register.component';
import { CartComponent } from './components/cart.component';
import { ProfileComponent } from './components/profile.component';

const appRoutes: Routes = [
    { path: '', redirectTo: 'home', pathMatch: 'full' },
    { path: 'home', component: IndexComponent, data: { title: 'Главная' } },
    { path: 'about', component: AboutComponent, data: { title: 'О магазине' } },
    { path: 'contact', component: ContactComponent, data: { title: 'Контакты' } },
    { path: 'login', component: LoginComponent, data: { title: 'Вход' } },
    { path: 'register', component: RegisterComponent, data: { title: 'Регистрация' } },
    { path: 'cart', component: CartComponent, data: { title: 'Корзина' } },
    { path: 'profile', component: ProfileComponent, data: { title: 'Профиль пользователя' } }
];

export const routing = RouterModule.forRoot(appRoutes);

export const routedComponents = [AboutComponent, IndexComponent, ContactComponent, LoginComponent, RegisterComponent, CartComponent, ProfileComponent];
