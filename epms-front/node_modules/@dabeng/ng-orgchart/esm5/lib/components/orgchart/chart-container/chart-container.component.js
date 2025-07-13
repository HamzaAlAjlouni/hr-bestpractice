/**
 * @fileoverview added by tsickle
 * @suppress {checkTypes,extraRequire,missingOverride,missingReturn,unusedPrivateMembers,uselessCode} checked by tsc
 */
import { Component, Input, Output, EventEmitter, TemplateRef } from '@angular/core';
import { NodeSelectService } from '../shared/services/node-select.service';
var ChartContainerComponent = /** @class */ (function () {
    function ChartContainerComponent(nodeSelectService) {
        this.nodeSelectService = nodeSelectService;
        this.nodeHeading = 'name';
        this.nodeContent = 'title';
        this.groupScale = 3;
        this.pan = false;
        this.zoom = false;
        this.zoomoutLimit = 0.5;
        this.zoominLimit = 7;
        this.containerClass = '';
        this.chartClass = '';
        this.select = 'single';
        this.nodeClick = new EventEmitter();
        this.chartClick = new EventEmitter();
        this.cursorVal = 'default';
        this.panning = false;
        this.startX = 0;
        this.startY = 0;
        this.transformVal = '';
    }
    /**
     * @return {?}
     */
    ChartContainerComponent.prototype.ngOnInit = /**
     * @return {?}
     */
    function () {
    };
    /**
     * @return {?}
     */
    ChartContainerComponent.prototype.panEndHandler = /**
     * @return {?}
     */
    function () {
        this.panning = false;
        this.cursorVal = 'default';
    };
    /**
     * @param {?} e
     * @return {?}
     */
    ChartContainerComponent.prototype.panHandler = /**
     * @param {?} e
     * @return {?}
     */
    function (e) {
        /** @type {?} */
        var newX = 0;
        /** @type {?} */
        var newY = 0;
        if (!e.targetTouches) {
            // pand on desktop
            newX = e.pageX - this.startX;
            newY = e.pageY - this.startY;
        }
        else if (e.targetTouches.length === 1) {
            // pan on mobile device
            newX = e.targetTouches[0].pageX - this.startX;
            newY = e.targetTouches[0].pageY - this.startY;
        }
        else if (e.targetTouches.length > 1) {
            return;
        }
        if (this.transformVal === '') {
            if (this.transformVal.indexOf('3d') === -1) {
                this.transformVal = 'matrix(1,0,0,1,' + newX + ',' + newY + ')';
            }
            else {
                this.transformVal =
                    'matrix3d(1,0,0,0,0,1,0,0,0,0,1,0,' + newX + ', ' + newY + ',0,1)';
            }
        }
        else {
            /** @type {?} */
            var matrix = this.transformVal.split(',');
            if (this.transformVal.indexOf('3d') === -1) {
                matrix[4] = newX.toString();
                matrix[5] = newY + ')';
            }
            else {
                matrix[12] = newX.toString();
                matrix[13] = newY.toString();
            }
            this.transformVal = matrix.join(',');
        }
    };
    /**
     * @param {?} e
     * @return {?}
     */
    ChartContainerComponent.prototype.panStartHandler = /**
     * @param {?} e
     * @return {?}
     */
    function (e) {
        if (e.target.querySelectorAll('.node') && e.target.querySelectorAll('.node').length) {
            this.panning = false;
            return;
        }
        else {
            this.cursorVal = 'move';
            this.panning = true;
        }
        /** @type {?} */
        var lastX = 0;
        /** @type {?} */
        var lastY = 0;
        if (this.transformVal !== '') {
            /** @type {?} */
            var matrix = this.transformVal.split(',');
            if (this.transformVal.indexOf('3d') === -1) {
                lastX = parseInt(matrix[4], 10);
                lastY = parseInt(matrix[5], 10);
            }
            else {
                lastX = parseInt(matrix[12], 10);
                lastY = parseInt(matrix[13], 10);
            }
        }
        if (!e.targetTouches) {
            // pand on desktop
            this.startX = e.pageX - lastX;
            this.startY = e.pageY - lastY;
        }
        else if (e.targetTouches.length === 1) {
            // pan on mobile device
            this.startX = e.targetTouches[0].pageX - lastX;
            this.startY = e.targetTouches[0].pageY - lastY;
        }
        else if (e.targetTouches.length > 1) {
            return;
        }
    };
    /**
     * @param {?} newScale
     * @return {?}
     */
    ChartContainerComponent.prototype.setChartScale = /**
     * @param {?} newScale
     * @return {?}
     */
    function (newScale) {
        /** @type {?} */
        var matrix = [];
        /** @type {?} */
        var targetScale = 1;
        if (this.transformVal === '') {
            this.transformVal =
                'matrix(' + newScale + ', 0, 0, ' + newScale + ', 0, 0)';
        }
        else {
            matrix = this.transformVal.split(',');
            if (this.transformVal.indexOf('3d') === -1) {
                targetScale = Math.abs(parseFloat(matrix[3]) * newScale);
                if (targetScale > this.zoomoutLimit && targetScale < this.zoominLimit) {
                    matrix[0] = 'matrix(' + targetScale;
                    matrix[3] = targetScale;
                    this.transformVal = matrix.join(',');
                }
            }
            else {
                targetScale = Math.abs(parseFloat(matrix[5]) * newScale);
                if (targetScale > this.zoomoutLimit && targetScale < this.zoominLimit) {
                    matrix[0] = 'matrix3d(' + targetScale;
                    matrix[5] = targetScale;
                    this.transformVal = matrix.join(',');
                }
            }
        }
    };
    /**
     * @param {?} e
     * @return {?}
     */
    ChartContainerComponent.prototype.zoomHandler = /**
     * @param {?} e
     * @return {?}
     */
    function (e) {
        /** @type {?} */
        var newScale = 1 + (e.deltaY > 0 ? -0.2 : 0.2);
        this.setChartScale(newScale);
    };
    /**
     * @param {?} e
     * @return {?}
     */
    ChartContainerComponent.prototype.onClickChart = /**
     * @param {?} e
     * @return {?}
     */
    function (e) {
        if (!e.target.closest('.oc-node')) {
            this.chartClick.emit();
            this.nodeSelectService.clearSelect();
        }
    };
    /**
     * @param {?} nodeData
     * @return {?}
     */
    ChartContainerComponent.prototype.onNodeClick = /**
     * @param {?} nodeData
     * @return {?}
     */
    function (nodeData) {
        this.nodeClick.emit(nodeData);
    };
    ChartContainerComponent.decorators = [
        { type: Component, args: [{
                    selector: 'organization-chart',
                    template: "<div class=\"orgchart-container\" [ngClass]=\"containerClass\" (wheel)=\"zoom ? zoomHandler($event) : true\"\n\t(mouseup)=\"pan && panning ? panEndHandler() : true\">\n\t<div class=\"orgchart\" [ngClass]=\"chartClass\" [ngStyle]=\"{'transform': transformVal, 'cursor': cursorVal}\"\n\t\t(mousedown)=\"pan ? panStartHandler($event) : true\" (mousemove)=\"pan && panning ? panHandler($event) : true\"\n\t\t(click)=\"onClickChart($event)\">\n\t\t<orgchart-node [datasource]=\"datasource\" [nodeHeading]=\"nodeHeading\" [nodeContent]=\"nodeContent\"\n\t\t\t[nodeTemplate]=\"nodeTemplate\" [groupScale]=\"groupScale\" [select]=\"select\" (nodeClick)=\"onNodeClick($event)\"></orgchart-node>\n\t</div>\n</div>",
                    styles: [".orgchart-container{box-sizing:border-box;position:relative;margin:10px;height:700px;border:2px dashed #aaa;border-radius:5px;overflow:auto;text-align:center}.orgchart{box-sizing:border-box;display:inline-block;background-image:linear-gradient(90deg,rgba(200,0,0,.15) 10%,rgba(0,0,0,0) 10%),linear-gradient(rgba(200,0,0,.15) 10%,rgba(0,0,0,0) 10%);background-size:10px 10px;border:1px dashed transparent;padding:20px}"]
                }] }
    ];
    /** @nocollapse */
    ChartContainerComponent.ctorParameters = function () { return [
        { type: NodeSelectService }
    ]; };
    ChartContainerComponent.propDecorators = {
        datasource: [{ type: Input }],
        nodeHeading: [{ type: Input }],
        nodeContent: [{ type: Input }],
        nodeTemplate: [{ type: Input }],
        groupScale: [{ type: Input }],
        pan: [{ type: Input }],
        zoom: [{ type: Input }],
        zoomoutLimit: [{ type: Input }],
        zoominLimit: [{ type: Input }],
        containerClass: [{ type: Input }],
        chartClass: [{ type: Input }],
        select: [{ type: Input }],
        nodeClick: [{ type: Output }],
        chartClick: [{ type: Output }]
    };
    return ChartContainerComponent;
}());
export { ChartContainerComponent };
if (false) {
    /** @type {?} */
    ChartContainerComponent.prototype.datasource;
    /** @type {?} */
    ChartContainerComponent.prototype.nodeHeading;
    /** @type {?} */
    ChartContainerComponent.prototype.nodeContent;
    /** @type {?} */
    ChartContainerComponent.prototype.nodeTemplate;
    /** @type {?} */
    ChartContainerComponent.prototype.groupScale;
    /** @type {?} */
    ChartContainerComponent.prototype.pan;
    /** @type {?} */
    ChartContainerComponent.prototype.zoom;
    /** @type {?} */
    ChartContainerComponent.prototype.zoomoutLimit;
    /** @type {?} */
    ChartContainerComponent.prototype.zoominLimit;
    /** @type {?} */
    ChartContainerComponent.prototype.containerClass;
    /** @type {?} */
    ChartContainerComponent.prototype.chartClass;
    /** @type {?} */
    ChartContainerComponent.prototype.select;
    /** @type {?} */
    ChartContainerComponent.prototype.nodeClick;
    /** @type {?} */
    ChartContainerComponent.prototype.chartClick;
    /** @type {?} */
    ChartContainerComponent.prototype.cursorVal;
    /** @type {?} */
    ChartContainerComponent.prototype.panning;
    /** @type {?} */
    ChartContainerComponent.prototype.startX;
    /** @type {?} */
    ChartContainerComponent.prototype.startY;
    /** @type {?} */
    ChartContainerComponent.prototype.transformVal;
    /**
     * @type {?}
     * @private
     */
    ChartContainerComponent.prototype.nodeSelectService;
}
//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJmaWxlIjoiY2hhcnQtY29udGFpbmVyLmNvbXBvbmVudC5qcyIsInNvdXJjZVJvb3QiOiJuZzovL0BkYWJlbmcvbmctb3JnY2hhcnQvIiwic291cmNlcyI6WyJsaWIvY29tcG9uZW50cy9vcmdjaGFydC9jaGFydC1jb250YWluZXIvY2hhcnQtY29udGFpbmVyLmNvbXBvbmVudC50cyJdLCJuYW1lcyI6W10sIm1hcHBpbmdzIjoiOzs7O0FBQUEsT0FBTyxFQUFFLFNBQVMsRUFBVSxLQUFLLEVBQUUsTUFBTSxFQUFFLFlBQVksRUFBRSxXQUFXLEVBQUUsTUFBTSxlQUFlLENBQUM7QUFDNUYsT0FBTyxFQUFFLGlCQUFpQixFQUFFLE1BQU0sd0NBQXdDLENBQUM7QUFFM0U7SUE2QkUsaUNBQW9CLGlCQUFvQztRQUFwQyxzQkFBaUIsR0FBakIsaUJBQWlCLENBQW1CO1FBckIvQyxnQkFBVyxHQUFHLE1BQU0sQ0FBQztRQUNyQixnQkFBVyxHQUFHLE9BQU8sQ0FBQztRQUV0QixlQUFVLEdBQUcsQ0FBQyxDQUFDO1FBQ2YsUUFBRyxHQUFHLEtBQUssQ0FBQztRQUNaLFNBQUksR0FBRyxLQUFLLENBQUM7UUFDYixpQkFBWSxHQUFHLEdBQUcsQ0FBQztRQUNuQixnQkFBVyxHQUFHLENBQUMsQ0FBQztRQUNoQixtQkFBYyxHQUFHLEVBQUUsQ0FBQztRQUNwQixlQUFVLEdBQUcsRUFBRSxDQUFDO1FBQ2hCLFdBQU0sR0FBRyxRQUFRLENBQUM7UUFFakIsY0FBUyxHQUFHLElBQUksWUFBWSxFQUFPLENBQUM7UUFDcEMsZUFBVSxHQUFHLElBQUksWUFBWSxFQUFFLENBQUM7UUFFMUMsY0FBUyxHQUFHLFNBQVMsQ0FBQztRQUN0QixZQUFPLEdBQUcsS0FBSyxDQUFDO1FBQ2hCLFdBQU0sR0FBRyxDQUFDLENBQUM7UUFDWCxXQUFNLEdBQUcsQ0FBQyxDQUFDO1FBQ1gsaUJBQVksR0FBRyxFQUFFLENBQUM7SUFFeUMsQ0FBQzs7OztJQUU1RCwwQ0FBUTs7O0lBQVI7SUFDQSxDQUFDOzs7O0lBRUQsK0NBQWE7OztJQUFiO1FBQ0UsSUFBSSxDQUFDLE9BQU8sR0FBRyxLQUFLLENBQUM7UUFDckIsSUFBSSxDQUFDLFNBQVMsR0FBRyxTQUFTLENBQUM7SUFDN0IsQ0FBQzs7Ozs7SUFFRCw0Q0FBVTs7OztJQUFWLFVBQVcsQ0FBQzs7WUFDTixJQUFJLEdBQUcsQ0FBQzs7WUFDUixJQUFJLEdBQUcsQ0FBQztRQUNaLElBQUksQ0FBQyxDQUFDLENBQUMsYUFBYSxFQUFFO1lBQ3BCLGtCQUFrQjtZQUNsQixJQUFJLEdBQUcsQ0FBQyxDQUFDLEtBQUssR0FBRyxJQUFJLENBQUMsTUFBTSxDQUFDO1lBQzdCLElBQUksR0FBRyxDQUFDLENBQUMsS0FBSyxHQUFHLElBQUksQ0FBQyxNQUFNLENBQUM7U0FDOUI7YUFBTSxJQUFJLENBQUMsQ0FBQyxhQUFhLENBQUMsTUFBTSxLQUFLLENBQUMsRUFBRTtZQUN2Qyx1QkFBdUI7WUFDdkIsSUFBSSxHQUFHLENBQUMsQ0FBQyxhQUFhLENBQUMsQ0FBQyxDQUFDLENBQUMsS0FBSyxHQUFHLElBQUksQ0FBQyxNQUFNLENBQUM7WUFDOUMsSUFBSSxHQUFHLENBQUMsQ0FBQyxhQUFhLENBQUMsQ0FBQyxDQUFDLENBQUMsS0FBSyxHQUFHLElBQUksQ0FBQyxNQUFNLENBQUM7U0FDL0M7YUFBTSxJQUFJLENBQUMsQ0FBQyxhQUFhLENBQUMsTUFBTSxHQUFHLENBQUMsRUFBRTtZQUNyQyxPQUFPO1NBQ1I7UUFDRCxJQUFJLElBQUksQ0FBQyxZQUFZLEtBQUssRUFBRSxFQUFFO1lBQzVCLElBQUksSUFBSSxDQUFDLFlBQVksQ0FBQyxPQUFPLENBQUMsSUFBSSxDQUFDLEtBQUssQ0FBQyxDQUFDLEVBQUU7Z0JBQzFDLElBQUksQ0FBQyxZQUFZLEdBQUcsaUJBQWlCLEdBQUcsSUFBSSxHQUFHLEdBQUcsR0FBRyxJQUFJLEdBQUcsR0FBRyxDQUFDO2FBQ2pFO2lCQUFNO2dCQUNMLElBQUksQ0FBQyxZQUFZO29CQUNmLG1DQUFtQyxHQUFHLElBQUksR0FBRyxJQUFJLEdBQUcsSUFBSSxHQUFHLE9BQU8sQ0FBQzthQUN0RTtTQUNGO2FBQU07O2dCQUNDLE1BQU0sR0FBRyxJQUFJLENBQUMsWUFBWSxDQUFDLEtBQUssQ0FBQyxHQUFHLENBQUM7WUFDM0MsSUFBSSxJQUFJLENBQUMsWUFBWSxDQUFDLE9BQU8sQ0FBQyxJQUFJLENBQUMsS0FBSyxDQUFDLENBQUMsRUFBRTtnQkFDMUMsTUFBTSxDQUFDLENBQUMsQ0FBQyxHQUFHLElBQUksQ0FBQyxRQUFRLEVBQUUsQ0FBQztnQkFDNUIsTUFBTSxDQUFDLENBQUMsQ0FBQyxHQUFHLElBQUksR0FBRyxHQUFHLENBQUM7YUFDeEI7aUJBQU07Z0JBQ0wsTUFBTSxDQUFDLEVBQUUsQ0FBQyxHQUFHLElBQUksQ0FBQyxRQUFRLEVBQUUsQ0FBQztnQkFDN0IsTUFBTSxDQUFDLEVBQUUsQ0FBQyxHQUFHLElBQUksQ0FBQyxRQUFRLEVBQUUsQ0FBQzthQUM5QjtZQUNELElBQUksQ0FBQyxZQUFZLEdBQUcsTUFBTSxDQUFDLElBQUksQ0FBQyxHQUFHLENBQUMsQ0FBQztTQUN0QztJQUNILENBQUM7Ozs7O0lBRUQsaURBQWU7Ozs7SUFBZixVQUFnQixDQUFDO1FBQ2YsSUFBSSxDQUFDLENBQUMsTUFBTSxDQUFDLGdCQUFnQixDQUFDLE9BQU8sQ0FBQyxJQUFJLENBQUMsQ0FBQyxNQUFNLENBQUMsZ0JBQWdCLENBQUMsT0FBTyxDQUFDLENBQUMsTUFBTSxFQUFFO1lBQ25GLElBQUksQ0FBQyxPQUFPLEdBQUcsS0FBSyxDQUFDO1lBQ3JCLE9BQU87U0FDUjthQUFNO1lBQ0wsSUFBSSxDQUFDLFNBQVMsR0FBRyxNQUFNLENBQUM7WUFDeEIsSUFBSSxDQUFDLE9BQU8sR0FBRyxJQUFJLENBQUM7U0FDckI7O1lBQ0csS0FBSyxHQUFHLENBQUM7O1lBQ1QsS0FBSyxHQUFHLENBQUM7UUFDYixJQUFJLElBQUksQ0FBQyxZQUFZLEtBQUssRUFBRSxFQUFFOztnQkFDdEIsTUFBTSxHQUFHLElBQUksQ0FBQyxZQUFZLENBQUMsS0FBSyxDQUFDLEdBQUcsQ0FBQztZQUMzQyxJQUFJLElBQUksQ0FBQyxZQUFZLENBQUMsT0FBTyxDQUFDLElBQUksQ0FBQyxLQUFLLENBQUMsQ0FBQyxFQUFFO2dCQUMxQyxLQUFLLEdBQUcsUUFBUSxDQUFDLE1BQU0sQ0FBQyxDQUFDLENBQUMsRUFBRSxFQUFFLENBQUMsQ0FBQztnQkFDaEMsS0FBSyxHQUFHLFFBQVEsQ0FBQyxNQUFNLENBQUMsQ0FBQyxDQUFDLEVBQUUsRUFBRSxDQUFDLENBQUM7YUFDakM7aUJBQU07Z0JBQ0wsS0FBSyxHQUFHLFFBQVEsQ0FBQyxNQUFNLENBQUMsRUFBRSxDQUFDLEVBQUUsRUFBRSxDQUFDLENBQUM7Z0JBQ2pDLEtBQUssR0FBRyxRQUFRLENBQUMsTUFBTSxDQUFDLEVBQUUsQ0FBQyxFQUFFLEVBQUUsQ0FBQyxDQUFDO2FBQ2xDO1NBQ0Y7UUFDRCxJQUFJLENBQUMsQ0FBQyxDQUFDLGFBQWEsRUFBRTtZQUNwQixrQkFBa0I7WUFDbEIsSUFBSSxDQUFDLE1BQU0sR0FBRyxDQUFDLENBQUMsS0FBSyxHQUFHLEtBQUssQ0FBQztZQUM5QixJQUFJLENBQUMsTUFBTSxHQUFHLENBQUMsQ0FBQyxLQUFLLEdBQUcsS0FBSyxDQUFDO1NBQy9CO2FBQU0sSUFBSSxDQUFDLENBQUMsYUFBYSxDQUFDLE1BQU0sS0FBSyxDQUFDLEVBQUU7WUFDdkMsdUJBQXVCO1lBQ3ZCLElBQUksQ0FBQyxNQUFNLEdBQUcsQ0FBQyxDQUFDLGFBQWEsQ0FBQyxDQUFDLENBQUMsQ0FBQyxLQUFLLEdBQUcsS0FBSyxDQUFDO1lBQy9DLElBQUksQ0FBQyxNQUFNLEdBQUcsQ0FBQyxDQUFDLGFBQWEsQ0FBQyxDQUFDLENBQUMsQ0FBQyxLQUFLLEdBQUcsS0FBSyxDQUFDO1NBQ2hEO2FBQU0sSUFBSSxDQUFDLENBQUMsYUFBYSxDQUFDLE1BQU0sR0FBRyxDQUFDLEVBQUU7WUFDckMsT0FBTztTQUNSO0lBQ0gsQ0FBQzs7Ozs7SUFFRCwrQ0FBYTs7OztJQUFiLFVBQWMsUUFBUTs7WUFDaEIsTUFBTSxHQUFHLEVBQUU7O1lBQ1gsV0FBVyxHQUFHLENBQUM7UUFDbkIsSUFBSSxJQUFJLENBQUMsWUFBWSxLQUFLLEVBQUUsRUFBRTtZQUM1QixJQUFJLENBQUMsWUFBWTtnQkFDZixTQUFTLEdBQUcsUUFBUSxHQUFHLFVBQVUsR0FBRyxRQUFRLEdBQUcsU0FBUyxDQUFDO1NBQzVEO2FBQU07WUFDTCxNQUFNLEdBQUcsSUFBSSxDQUFDLFlBQVksQ0FBQyxLQUFLLENBQUMsR0FBRyxDQUFDLENBQUM7WUFDdEMsSUFBSSxJQUFJLENBQUMsWUFBWSxDQUFDLE9BQU8sQ0FBQyxJQUFJLENBQUMsS0FBSyxDQUFDLENBQUMsRUFBRTtnQkFDMUMsV0FBVyxHQUFHLElBQUksQ0FBQyxHQUFHLENBQUMsVUFBVSxDQUFDLE1BQU0sQ0FBQyxDQUFDLENBQUMsQ0FBQyxHQUFHLFFBQVEsQ0FBQyxDQUFDO2dCQUN6RCxJQUFJLFdBQVcsR0FBRyxJQUFJLENBQUMsWUFBWSxJQUFJLFdBQVcsR0FBRyxJQUFJLENBQUMsV0FBVyxFQUFFO29CQUNyRSxNQUFNLENBQUMsQ0FBQyxDQUFDLEdBQUcsU0FBUyxHQUFHLFdBQVcsQ0FBQztvQkFDcEMsTUFBTSxDQUFDLENBQUMsQ0FBQyxHQUFHLFdBQVcsQ0FBQztvQkFDeEIsSUFBSSxDQUFDLFlBQVksR0FBRyxNQUFNLENBQUMsSUFBSSxDQUFDLEdBQUcsQ0FBQyxDQUFDO2lCQUN0QzthQUNGO2lCQUFNO2dCQUNMLFdBQVcsR0FBRyxJQUFJLENBQUMsR0FBRyxDQUFDLFVBQVUsQ0FBQyxNQUFNLENBQUMsQ0FBQyxDQUFDLENBQUMsR0FBRyxRQUFRLENBQUMsQ0FBQztnQkFDekQsSUFBSSxXQUFXLEdBQUcsSUFBSSxDQUFDLFlBQVksSUFBSSxXQUFXLEdBQUcsSUFBSSxDQUFDLFdBQVcsRUFBRTtvQkFDckUsTUFBTSxDQUFDLENBQUMsQ0FBQyxHQUFHLFdBQVcsR0FBRyxXQUFXLENBQUM7b0JBQ3RDLE1BQU0sQ0FBQyxDQUFDLENBQUMsR0FBRyxXQUFXLENBQUM7b0JBQ3hCLElBQUksQ0FBQyxZQUFZLEdBQUcsTUFBTSxDQUFDLElBQUksQ0FBQyxHQUFHLENBQUMsQ0FBQztpQkFDdEM7YUFDRjtTQUNGO0lBQ0gsQ0FBQzs7Ozs7SUFFRCw2Q0FBVzs7OztJQUFYLFVBQVksQ0FBQzs7WUFDTCxRQUFRLEdBQUcsQ0FBQyxHQUFHLENBQUMsQ0FBQyxDQUFDLE1BQU0sR0FBRyxDQUFDLENBQUMsQ0FBQyxDQUFDLENBQUMsR0FBRyxDQUFDLENBQUMsQ0FBQyxHQUFHLENBQUM7UUFDaEQsSUFBSSxDQUFDLGFBQWEsQ0FBQyxRQUFRLENBQUMsQ0FBQztJQUMvQixDQUFDOzs7OztJQUVELDhDQUFZOzs7O0lBQVosVUFBYSxDQUFDO1FBQ1osSUFBSSxDQUFDLENBQUMsQ0FBQyxNQUFNLENBQUMsT0FBTyxDQUFDLFVBQVUsQ0FBQyxFQUFFO1lBQ2pDLElBQUksQ0FBQyxVQUFVLENBQUMsSUFBSSxFQUFFLENBQUM7WUFDdkIsSUFBSSxDQUFDLGlCQUFpQixDQUFDLFdBQVcsRUFBRSxDQUFDO1NBQ3RDO0lBQ0gsQ0FBQzs7Ozs7SUFFRCw2Q0FBVzs7OztJQUFYLFVBQVksUUFBYTtRQUN2QixJQUFJLENBQUMsU0FBUyxDQUFDLElBQUksQ0FBQyxRQUFRLENBQUMsQ0FBQztJQUNoQyxDQUFDOztnQkFsSkYsU0FBUyxTQUFDO29CQUNULFFBQVEsRUFBRSxvQkFBb0I7b0JBQzlCLDJzQkFBK0M7O2lCQUVoRDs7OztnQkFOUSxpQkFBaUI7Ozs2QkFTdkIsS0FBSzs4QkFDTCxLQUFLOzhCQUNMLEtBQUs7K0JBQ0wsS0FBSzs2QkFDTCxLQUFLO3NCQUNMLEtBQUs7dUJBQ0wsS0FBSzsrQkFDTCxLQUFLOzhCQUNMLEtBQUs7aUNBQ0wsS0FBSzs2QkFDTCxLQUFLO3lCQUNMLEtBQUs7NEJBRUwsTUFBTTs2QkFDTixNQUFNOztJQThIVCw4QkFBQztDQUFBLEFBbkpELElBbUpDO1NBOUlZLHVCQUF1Qjs7O0lBRWxDLDZDQUFvQjs7SUFDcEIsOENBQThCOztJQUM5Qiw4Q0FBK0I7O0lBQy9CLCtDQUF3Qzs7SUFDeEMsNkNBQXdCOztJQUN4QixzQ0FBcUI7O0lBQ3JCLHVDQUFzQjs7SUFDdEIsK0NBQTRCOztJQUM1Qiw4Q0FBeUI7O0lBQ3pCLGlEQUE2Qjs7SUFDN0IsNkNBQXlCOztJQUN6Qix5Q0FBMkI7O0lBRTNCLDRDQUE4Qzs7SUFDOUMsNkNBQTBDOztJQUUxQyw0Q0FBc0I7O0lBQ3RCLDBDQUFnQjs7SUFDaEIseUNBQVc7O0lBQ1gseUNBQVc7O0lBQ1gsK0NBQWtCOzs7OztJQUVOLG9EQUE0QyIsInNvdXJjZXNDb250ZW50IjpbImltcG9ydCB7IENvbXBvbmVudCwgT25Jbml0LCBJbnB1dCwgT3V0cHV0LCBFdmVudEVtaXR0ZXIsIFRlbXBsYXRlUmVmIH0gZnJvbSAnQGFuZ3VsYXIvY29yZSc7XG5pbXBvcnQgeyBOb2RlU2VsZWN0U2VydmljZSB9IGZyb20gJy4uL3NoYXJlZC9zZXJ2aWNlcy9ub2RlLXNlbGVjdC5zZXJ2aWNlJztcblxuQENvbXBvbmVudCh7XG4gIHNlbGVjdG9yOiAnb3JnYW5pemF0aW9uLWNoYXJ0JyxcbiAgdGVtcGxhdGVVcmw6ICcuL2NoYXJ0LWNvbnRhaW5lci5jb21wb25lbnQuaHRtbCcsXG4gIHN0eWxlVXJsczogWycuL2NoYXJ0LWNvbnRhaW5lci5jb21wb25lbnQuY3NzJ11cbn0pXG5leHBvcnQgY2xhc3MgQ2hhcnRDb250YWluZXJDb21wb25lbnQgaW1wbGVtZW50cyBPbkluaXQge1xuXG4gIEBJbnB1dCgpIGRhdGFzb3VyY2U7XG4gIEBJbnB1dCgpIG5vZGVIZWFkaW5nID0gJ25hbWUnO1xuICBASW5wdXQoKSBub2RlQ29udGVudCA9ICd0aXRsZSc7XG4gIEBJbnB1dCgpIG5vZGVUZW1wbGF0ZTogVGVtcGxhdGVSZWY8YW55PjtcbiAgQElucHV0KCkgZ3JvdXBTY2FsZSA9IDM7XG4gIEBJbnB1dCgpIHBhbiA9IGZhbHNlO1xuICBASW5wdXQoKSB6b29tID0gZmFsc2U7XG4gIEBJbnB1dCgpIHpvb21vdXRMaW1pdCA9IDAuNTtcbiAgQElucHV0KCkgem9vbWluTGltaXQgPSA3O1xuICBASW5wdXQoKSBjb250YWluZXJDbGFzcyA9ICcnO1xuICBASW5wdXQoKSBjaGFydENsYXNzID0gJyc7XG4gIEBJbnB1dCgpIHNlbGVjdCA9ICdzaW5nbGUnO1xuXG4gIEBPdXRwdXQoKSBub2RlQ2xpY2sgPSBuZXcgRXZlbnRFbWl0dGVyPGFueT4oKTtcbiAgQE91dHB1dCgpIGNoYXJ0Q2xpY2sgPSBuZXcgRXZlbnRFbWl0dGVyKCk7XG5cbiAgY3Vyc29yVmFsID0gJ2RlZmF1bHQnO1xuICBwYW5uaW5nID0gZmFsc2U7XG4gIHN0YXJ0WCA9IDA7XG4gIHN0YXJ0WSA9IDA7XG4gIHRyYW5zZm9ybVZhbCA9ICcnO1xuXG4gIGNvbnN0cnVjdG9yKHByaXZhdGUgbm9kZVNlbGVjdFNlcnZpY2U6IE5vZGVTZWxlY3RTZXJ2aWNlKSB7fVxuXG4gIG5nT25Jbml0KCkge1xuICB9XG5cbiAgcGFuRW5kSGFuZGxlcigpIHtcbiAgICB0aGlzLnBhbm5pbmcgPSBmYWxzZTtcbiAgICB0aGlzLmN1cnNvclZhbCA9ICdkZWZhdWx0JztcbiAgfVxuXG4gIHBhbkhhbmRsZXIoZSkge1xuICAgIGxldCBuZXdYID0gMDtcbiAgICBsZXQgbmV3WSA9IDA7XG4gICAgaWYgKCFlLnRhcmdldFRvdWNoZXMpIHtcbiAgICAgIC8vIHBhbmQgb24gZGVza3RvcFxuICAgICAgbmV3WCA9IGUucGFnZVggLSB0aGlzLnN0YXJ0WDtcbiAgICAgIG5ld1kgPSBlLnBhZ2VZIC0gdGhpcy5zdGFydFk7XG4gICAgfSBlbHNlIGlmIChlLnRhcmdldFRvdWNoZXMubGVuZ3RoID09PSAxKSB7XG4gICAgICAvLyBwYW4gb24gbW9iaWxlIGRldmljZVxuICAgICAgbmV3WCA9IGUudGFyZ2V0VG91Y2hlc1swXS5wYWdlWCAtIHRoaXMuc3RhcnRYO1xuICAgICAgbmV3WSA9IGUudGFyZ2V0VG91Y2hlc1swXS5wYWdlWSAtIHRoaXMuc3RhcnRZO1xuICAgIH0gZWxzZSBpZiAoZS50YXJnZXRUb3VjaGVzLmxlbmd0aCA+IDEpIHtcbiAgICAgIHJldHVybjtcbiAgICB9XG4gICAgaWYgKHRoaXMudHJhbnNmb3JtVmFsID09PSAnJykge1xuICAgICAgaWYgKHRoaXMudHJhbnNmb3JtVmFsLmluZGV4T2YoJzNkJykgPT09IC0xKSB7XG4gICAgICAgIHRoaXMudHJhbnNmb3JtVmFsID0gJ21hdHJpeCgxLDAsMCwxLCcgKyBuZXdYICsgJywnICsgbmV3WSArICcpJztcbiAgICAgIH0gZWxzZSB7XG4gICAgICAgIHRoaXMudHJhbnNmb3JtVmFsID1cbiAgICAgICAgICAnbWF0cml4M2QoMSwwLDAsMCwwLDEsMCwwLDAsMCwxLDAsJyArIG5ld1ggKyAnLCAnICsgbmV3WSArICcsMCwxKSc7XG4gICAgICB9XG4gICAgfSBlbHNlIHtcbiAgICAgIGNvbnN0IG1hdHJpeCA9IHRoaXMudHJhbnNmb3JtVmFsLnNwbGl0KCcsJyk7XG4gICAgICBpZiAodGhpcy50cmFuc2Zvcm1WYWwuaW5kZXhPZignM2QnKSA9PT0gLTEpIHtcbiAgICAgICAgbWF0cml4WzRdID0gbmV3WC50b1N0cmluZygpO1xuICAgICAgICBtYXRyaXhbNV0gPSBuZXdZICsgJyknO1xuICAgICAgfSBlbHNlIHtcbiAgICAgICAgbWF0cml4WzEyXSA9IG5ld1gudG9TdHJpbmcoKTtcbiAgICAgICAgbWF0cml4WzEzXSA9IG5ld1kudG9TdHJpbmcoKTtcbiAgICAgIH1cbiAgICAgIHRoaXMudHJhbnNmb3JtVmFsID0gbWF0cml4LmpvaW4oJywnKTtcbiAgICB9XG4gIH1cblxuICBwYW5TdGFydEhhbmRsZXIoZSkge1xuICAgIGlmIChlLnRhcmdldC5xdWVyeVNlbGVjdG9yQWxsKCcubm9kZScpICYmIGUudGFyZ2V0LnF1ZXJ5U2VsZWN0b3JBbGwoJy5ub2RlJykubGVuZ3RoKSB7XG4gICAgICB0aGlzLnBhbm5pbmcgPSBmYWxzZTtcbiAgICAgIHJldHVybjtcbiAgICB9IGVsc2Uge1xuICAgICAgdGhpcy5jdXJzb3JWYWwgPSAnbW92ZSc7XG4gICAgICB0aGlzLnBhbm5pbmcgPSB0cnVlO1xuICAgIH1cbiAgICBsZXQgbGFzdFggPSAwO1xuICAgIGxldCBsYXN0WSA9IDA7XG4gICAgaWYgKHRoaXMudHJhbnNmb3JtVmFsICE9PSAnJykge1xuICAgICAgY29uc3QgbWF0cml4ID0gdGhpcy50cmFuc2Zvcm1WYWwuc3BsaXQoJywnKTtcbiAgICAgIGlmICh0aGlzLnRyYW5zZm9ybVZhbC5pbmRleE9mKCczZCcpID09PSAtMSkge1xuICAgICAgICBsYXN0WCA9IHBhcnNlSW50KG1hdHJpeFs0XSwgMTApO1xuICAgICAgICBsYXN0WSA9IHBhcnNlSW50KG1hdHJpeFs1XSwgMTApO1xuICAgICAgfSBlbHNlIHtcbiAgICAgICAgbGFzdFggPSBwYXJzZUludChtYXRyaXhbMTJdLCAxMCk7XG4gICAgICAgIGxhc3RZID0gcGFyc2VJbnQobWF0cml4WzEzXSwgMTApO1xuICAgICAgfVxuICAgIH1cbiAgICBpZiAoIWUudGFyZ2V0VG91Y2hlcykge1xuICAgICAgLy8gcGFuZCBvbiBkZXNrdG9wXG4gICAgICB0aGlzLnN0YXJ0WCA9IGUucGFnZVggLSBsYXN0WDtcbiAgICAgIHRoaXMuc3RhcnRZID0gZS5wYWdlWSAtIGxhc3RZO1xuICAgIH0gZWxzZSBpZiAoZS50YXJnZXRUb3VjaGVzLmxlbmd0aCA9PT0gMSkge1xuICAgICAgLy8gcGFuIG9uIG1vYmlsZSBkZXZpY2VcbiAgICAgIHRoaXMuc3RhcnRYID0gZS50YXJnZXRUb3VjaGVzWzBdLnBhZ2VYIC0gbGFzdFg7XG4gICAgICB0aGlzLnN0YXJ0WSA9IGUudGFyZ2V0VG91Y2hlc1swXS5wYWdlWSAtIGxhc3RZO1xuICAgIH0gZWxzZSBpZiAoZS50YXJnZXRUb3VjaGVzLmxlbmd0aCA+IDEpIHtcbiAgICAgIHJldHVybjtcbiAgICB9XG4gIH1cblxuICBzZXRDaGFydFNjYWxlKG5ld1NjYWxlKSB7XG4gICAgbGV0IG1hdHJpeCA9IFtdO1xuICAgIGxldCB0YXJnZXRTY2FsZSA9IDE7XG4gICAgaWYgKHRoaXMudHJhbnNmb3JtVmFsID09PSAnJykge1xuICAgICAgdGhpcy50cmFuc2Zvcm1WYWwgPVxuICAgICAgICAnbWF0cml4KCcgKyBuZXdTY2FsZSArICcsIDAsIDAsICcgKyBuZXdTY2FsZSArICcsIDAsIDApJztcbiAgICB9IGVsc2Uge1xuICAgICAgbWF0cml4ID0gdGhpcy50cmFuc2Zvcm1WYWwuc3BsaXQoJywnKTtcbiAgICAgIGlmICh0aGlzLnRyYW5zZm9ybVZhbC5pbmRleE9mKCczZCcpID09PSAtMSkge1xuICAgICAgICB0YXJnZXRTY2FsZSA9IE1hdGguYWJzKHBhcnNlRmxvYXQobWF0cml4WzNdKSAqIG5ld1NjYWxlKTtcbiAgICAgICAgaWYgKHRhcmdldFNjYWxlID4gdGhpcy56b29tb3V0TGltaXQgJiYgdGFyZ2V0U2NhbGUgPCB0aGlzLnpvb21pbkxpbWl0KSB7XG4gICAgICAgICAgbWF0cml4WzBdID0gJ21hdHJpeCgnICsgdGFyZ2V0U2NhbGU7XG4gICAgICAgICAgbWF0cml4WzNdID0gdGFyZ2V0U2NhbGU7XG4gICAgICAgICAgdGhpcy50cmFuc2Zvcm1WYWwgPSBtYXRyaXguam9pbignLCcpO1xuICAgICAgICB9XG4gICAgICB9IGVsc2Uge1xuICAgICAgICB0YXJnZXRTY2FsZSA9IE1hdGguYWJzKHBhcnNlRmxvYXQobWF0cml4WzVdKSAqIG5ld1NjYWxlKTtcbiAgICAgICAgaWYgKHRhcmdldFNjYWxlID4gdGhpcy56b29tb3V0TGltaXQgJiYgdGFyZ2V0U2NhbGUgPCB0aGlzLnpvb21pbkxpbWl0KSB7XG4gICAgICAgICAgbWF0cml4WzBdID0gJ21hdHJpeDNkKCcgKyB0YXJnZXRTY2FsZTtcbiAgICAgICAgICBtYXRyaXhbNV0gPSB0YXJnZXRTY2FsZTtcbiAgICAgICAgICB0aGlzLnRyYW5zZm9ybVZhbCA9IG1hdHJpeC5qb2luKCcsJyk7XG4gICAgICAgIH1cbiAgICAgIH1cbiAgICB9XG4gIH1cblxuICB6b29tSGFuZGxlcihlKSB7XG4gICAgY29uc3QgbmV3U2NhbGUgPSAxICsgKGUuZGVsdGFZID4gMCA/IC0wLjIgOiAwLjIpO1xuICAgIHRoaXMuc2V0Q2hhcnRTY2FsZShuZXdTY2FsZSk7XG4gIH1cblxuICBvbkNsaWNrQ2hhcnQoZSkge1xuICAgIGlmICghZS50YXJnZXQuY2xvc2VzdCgnLm9jLW5vZGUnKSkge1xuICAgICAgdGhpcy5jaGFydENsaWNrLmVtaXQoKTtcbiAgICAgIHRoaXMubm9kZVNlbGVjdFNlcnZpY2UuY2xlYXJTZWxlY3QoKTtcbiAgICB9XG4gIH1cblxuICBvbk5vZGVDbGljayhub2RlRGF0YTogYW55KSB7XG4gICAgdGhpcy5ub2RlQ2xpY2suZW1pdChub2RlRGF0YSk7XG4gIH1cbn1cbiJdfQ==