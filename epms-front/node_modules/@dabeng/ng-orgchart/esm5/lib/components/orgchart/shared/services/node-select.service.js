/**
 * @fileoverview added by tsickle
 * @suppress {checkTypes,extraRequire,missingOverride,missingReturn,unusedPrivateMembers,uselessCode} checked by tsc
 */
import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
var NodeSelectService = /** @class */ (function () {
    function NodeSelectService() {
        this.subject = new Subject();
    }
    /**
     * @param {?} id
     * @return {?}
     */
    NodeSelectService.prototype.sendSelect = /**
     * @param {?} id
     * @return {?}
     */
    function (id) {
        this.subject.next({ id: id });
    };
    /**
     * @return {?}
     */
    NodeSelectService.prototype.clearSelect = /**
     * @return {?}
     */
    function () {
        this.subject.next();
    };
    /**
     * @return {?}
     */
    NodeSelectService.prototype.getSelect = /**
     * @return {?}
     */
    function () {
        return this.subject.asObservable();
    };
    NodeSelectService.decorators = [
        { type: Injectable }
    ];
    /** @nocollapse */
    NodeSelectService.ctorParameters = function () { return []; };
    return NodeSelectService;
}());
export { NodeSelectService };
if (false) {
    /**
     * @type {?}
     * @private
     */
    NodeSelectService.prototype.subject;
}
//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJmaWxlIjoibm9kZS1zZWxlY3Quc2VydmljZS5qcyIsInNvdXJjZVJvb3QiOiJuZzovL0BkYWJlbmcvbmctb3JnY2hhcnQvIiwic291cmNlcyI6WyJsaWIvY29tcG9uZW50cy9vcmdjaGFydC9zaGFyZWQvc2VydmljZXMvbm9kZS1zZWxlY3Quc2VydmljZS50cyJdLCJuYW1lcyI6W10sIm1hcHBpbmdzIjoiOzs7O0FBQUEsT0FBTyxFQUFFLFVBQVUsRUFBRSxNQUFNLGVBQWUsQ0FBQztBQUMzQyxPQUFPLEVBQWMsT0FBTyxFQUFFLE1BQU0sTUFBTSxDQUFDO0FBRTNDO0lBS0U7UUFGUSxZQUFPLEdBQUcsSUFBSSxPQUFPLEVBQU8sQ0FBQztJQUVyQixDQUFDOzs7OztJQUVqQixzQ0FBVTs7OztJQUFWLFVBQVcsRUFBVTtRQUNuQixJQUFJLENBQUMsT0FBTyxDQUFDLElBQUksQ0FBQyxFQUFFLEVBQUUsSUFBQSxFQUFFLENBQUMsQ0FBQztJQUM1QixDQUFDOzs7O0lBRUQsdUNBQVc7OztJQUFYO1FBQ0UsSUFBSSxDQUFDLE9BQU8sQ0FBQyxJQUFJLEVBQUUsQ0FBQztJQUN0QixDQUFDOzs7O0lBRUQscUNBQVM7OztJQUFUO1FBQ0UsT0FBTyxJQUFJLENBQUMsT0FBTyxDQUFDLFlBQVksRUFBRSxDQUFDO0lBQ3JDLENBQUM7O2dCQWpCRixVQUFVOzs7O0lBbUJYLHdCQUFDO0NBQUEsQUFuQkQsSUFtQkM7U0FsQlksaUJBQWlCOzs7Ozs7SUFFNUIsb0NBQXFDIiwic291cmNlc0NvbnRlbnQiOlsiaW1wb3J0IHsgSW5qZWN0YWJsZSB9IGZyb20gJ0Bhbmd1bGFyL2NvcmUnO1xuaW1wb3J0IHsgT2JzZXJ2YWJsZSwgU3ViamVjdCB9IGZyb20gJ3J4anMnO1xuXG5ASW5qZWN0YWJsZSgpXG5leHBvcnQgY2xhc3MgTm9kZVNlbGVjdFNlcnZpY2Uge1xuXG4gIHByaXZhdGUgc3ViamVjdCA9IG5ldyBTdWJqZWN0PGFueT4oKTtcblxuICBjb25zdHJ1Y3RvcigpIHsgfVxuXG4gIHNlbmRTZWxlY3QoaWQ6IHN0cmluZykge1xuICAgIHRoaXMuc3ViamVjdC5uZXh0KHsgaWQgfSk7XG4gIH1cblxuICBjbGVhclNlbGVjdCgpIHtcbiAgICB0aGlzLnN1YmplY3QubmV4dCgpO1xuICB9XG5cbiAgZ2V0U2VsZWN0KCk6IE9ic2VydmFibGU8YW55PiB7XG4gICAgcmV0dXJuIHRoaXMuc3ViamVjdC5hc09ic2VydmFibGUoKTtcbiAgfVxuXG59XG4iXX0=