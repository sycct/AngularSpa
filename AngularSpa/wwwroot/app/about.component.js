"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var Orders_services_1 = require("./services/Orders.services");
var AboutComponent = (function () {
    function AboutComponent(ordersService) {
        this.ordersService = ordersService;
        this.orderInfo = null;
    }
    AboutComponent.prototype.ngOnInit = function () {
        this.getOrder();
    };
    AboutComponent.prototype.getOrder = function () {
        var _this = this;
        this.ordersService.getSampleData()
            .subscribe(function (data) { return _this.orderInfo = data; }, function (error) { return _this.errorMessage = error; });
    };
    AboutComponent.prototype.addOrder = function (event) {
        var _this = this;
        event.preventDefault();
        if (!this.orderInfo)
            return;
        this.ordersService.addSampleData(this.orderInfo)
            .subscribe(function (data) { return _this.orderInfo = data; }, function (error) { return _this.errorMessage = error; });
    };
    return AboutComponent;
}());
AboutComponent = __decorate([
    core_1.Component({
        selector: 'my-about',
        templateUrl: '/partial/aboutComponent'
    }),
    __metadata("design:paramtypes", [Orders_services_1.OrdersService])
], AboutComponent);
exports.AboutComponent = AboutComponent;
//# sourceMappingURL=about.component.js.map