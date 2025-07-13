/**
 * @fileoverview added by tsickle
 * @suppress {checkTypes,extraRequire,missingOverride,missingReturn,unusedPrivateMembers,uselessCode} checked by tsc
 */
import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
export class NodeSelectService {
    constructor() {
        this.subject = new Subject();
    }
    /**
     * @param {?} id
     * @return {?}
     */
    sendSelect(id) {
        this.subject.next({ id });
    }
    /**
     * @return {?}
     */
    clearSelect() {
        this.subject.next();
    }
    /**
     * @return {?}
     */
    getSelect() {
        return this.subject.asObservable();
    }
}
NodeSelectService.decorators = [
    { type: Injectable }
];
/** @nocollapse */
NodeSelectService.ctorParameters = () => [];
if (false) {
    /**
     * @type {?}
     * @private
     */
    NodeSelectService.prototype.subject;
}
//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJmaWxlIjoibm9kZS1zZWxlY3Quc2VydmljZS5qcyIsInNvdXJjZVJvb3QiOiJuZzovL0BkYWJlbmcvbmctb3JnY2hhcnQvIiwic291cmNlcyI6WyJsaWIvY29tcG9uZW50cy9vcmdjaGFydC9zaGFyZWQvc2VydmljZXMvbm9kZS1zZWxlY3Quc2VydmljZS50cyJdLCJuYW1lcyI6W10sIm1hcHBpbmdzIjoiOzs7O0FBQUEsT0FBTyxFQUFFLFVBQVUsRUFBRSxNQUFNLGVBQWUsQ0FBQztBQUMzQyxPQUFPLEVBQWMsT0FBTyxFQUFFLE1BQU0sTUFBTSxDQUFDO0FBRzNDLE1BQU0sT0FBTyxpQkFBaUI7SUFJNUI7UUFGUSxZQUFPLEdBQUcsSUFBSSxPQUFPLEVBQU8sQ0FBQztJQUVyQixDQUFDOzs7OztJQUVqQixVQUFVLENBQUMsRUFBVTtRQUNuQixJQUFJLENBQUMsT0FBTyxDQUFDLElBQUksQ0FBQyxFQUFFLEVBQUUsRUFBRSxDQUFDLENBQUM7SUFDNUIsQ0FBQzs7OztJQUVELFdBQVc7UUFDVCxJQUFJLENBQUMsT0FBTyxDQUFDLElBQUksRUFBRSxDQUFDO0lBQ3RCLENBQUM7Ozs7SUFFRCxTQUFTO1FBQ1AsT0FBTyxJQUFJLENBQUMsT0FBTyxDQUFDLFlBQVksRUFBRSxDQUFDO0lBQ3JDLENBQUM7OztZQWpCRixVQUFVOzs7Ozs7Ozs7SUFHVCxvQ0FBcUMiLCJzb3VyY2VzQ29udGVudCI6WyJpbXBvcnQgeyBJbmplY3RhYmxlIH0gZnJvbSAnQGFuZ3VsYXIvY29yZSc7XG5pbXBvcnQgeyBPYnNlcnZhYmxlLCBTdWJqZWN0IH0gZnJvbSAncnhqcyc7XG5cbkBJbmplY3RhYmxlKClcbmV4cG9ydCBjbGFzcyBOb2RlU2VsZWN0U2VydmljZSB7XG5cbiAgcHJpdmF0ZSBzdWJqZWN0ID0gbmV3IFN1YmplY3Q8YW55PigpO1xuXG4gIGNvbnN0cnVjdG9yKCkgeyB9XG5cbiAgc2VuZFNlbGVjdChpZDogc3RyaW5nKSB7XG4gICAgdGhpcy5zdWJqZWN0Lm5leHQoeyBpZCB9KTtcbiAgfVxuXG4gIGNsZWFyU2VsZWN0KCkge1xuICAgIHRoaXMuc3ViamVjdC5uZXh0KCk7XG4gIH1cblxuICBnZXRTZWxlY3QoKTogT2JzZXJ2YWJsZTxhbnk+IHtcbiAgICByZXR1cm4gdGhpcy5zdWJqZWN0LmFzT2JzZXJ2YWJsZSgpO1xuICB9XG5cbn1cbiJdfQ==