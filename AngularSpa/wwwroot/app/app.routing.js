"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var router_1 = require("@angular/router");
var about_component_1 = require("./components/about.component");
var index_component_1 = require("./components/index.component");
var contact_component_1 = require("./components/contact.component");
var login_component_1 = require("./components/login.component");
var register_component_1 = require("./components/register.component");
var cart_component_1 = require("./components/cart.component");
var profile_component_1 = require("./components/profile.component");
var appRoutes = [
    { path: '', redirectTo: 'home', pathMatch: 'full' },
    { path: 'home', component: index_component_1.IndexComponent, data: { title: 'Главная' } },
    { path: 'about', component: about_component_1.AboutComponent, data: { title: 'О магазине' } },
    { path: 'contact', component: contact_component_1.ContactComponent, data: { title: 'Контакты' } },
    { path: 'login', component: login_component_1.LoginComponent, data: { title: 'Вход' } },
    { path: 'register', component: register_component_1.RegisterComponent, data: { title: 'Регистрация' } },
    { path: 'cart', component: cart_component_1.CartComponent, data: { title: 'Корзина' } },
    { path: 'profile', component: profile_component_1.ProfileComponent, data: { title: 'Профиль пользователя' } }
];
exports.routing = router_1.RouterModule.forRoot(appRoutes);
exports.routedComponents = [about_component_1.AboutComponent, index_component_1.IndexComponent, contact_component_1.ContactComponent, login_component_1.LoginComponent, register_component_1.RegisterComponent, cart_component_1.CartComponent, profile_component_1.ProfileComponent];
//# sourceMappingURL=app.routing.js.map