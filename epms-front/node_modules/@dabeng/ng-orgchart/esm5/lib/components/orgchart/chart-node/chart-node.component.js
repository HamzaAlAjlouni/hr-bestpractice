/**
 * @fileoverview added by tsickle
 * @suppress {checkTypes,extraRequire,missingOverride,missingReturn,unusedPrivateMembers,uselessCode} checked by tsc
 */
import { Component, Input, Output, EventEmitter, TemplateRef } from '@angular/core';
import { Node } from '../shared/models/node.model';
import { NodeSelectService } from '../shared/services/node-select.service';
import { trigger, state, style, animate, transition } from '@angular/animations';
var ChartNodeComponent = /** @class */ (function () {
    function ChartNodeComponent(nodeSelectService) {
        var _this = this;
        this.nodeSelectService = nodeSelectService;
        this.nodeClick = new EventEmitter();
        this.Arr = Array; // Array type captured in a variable
        // Array type captured in a variable
        this.isCollapsed = false;
        // subscribe to node selection status
        this.subscription = this.nodeSelectService.getSelect().subscribe((/**
         * @param {?} selection
         * @return {?}
         */
        function (selection) {
            if (selection && selection.id) {
                _this.isSelected = _this.datasource.id === selection.id;
            }
            else { // clear selection when empty selection received
                _this.isSelected = false;
            }
        }));
    }
    /**
     * @return {?}
     */
    ChartNodeComponent.prototype.ngOnDestroy = /**
     * @return {?}
     */
    function () {
        // unsubscribe to ensure no memory leaks
        this.subscription.unsubscribe();
    };
    /**
     * @return {?}
     */
    ChartNodeComponent.prototype.ngOnInit = /**
     * @return {?}
     */
    function () {
    };
    /**
     * @return {?}
     */
    ChartNodeComponent.prototype.toggleChildren = /**
     * @return {?}
     */
    function () {
        this.isCollapsed = !this.isCollapsed;
    };
    /**
     * @param {?} event
     * @return {?}
     */
    ChartNodeComponent.prototype.toggleAnimStart = /**
     * @param {?} event
     * @return {?}
     */
    function (event) {
        if (this.isCollapsed) {
            // 如果折叠的是根结点的子节点，那么根结点向下的连接线需要隐藏
            if (event.element.parentElement &&
                event.element.parentElement.parentElement &&
                event.element.parentElement.parentElement.classList.contains('orgchart')) {
                event.element.previousElementSibling.classList.add('oc-is-collapsed');
            }
        }
        else { // 刚一展开的时候，需要马上把子节点们显示出来，以便看到“下滑”效果
            this.ecStyles = {
                display: 'flex'
            };
        }
    };
    /**
     * @param {?} event
     * @return {?}
     */
    ChartNodeComponent.prototype.toggleAnimEnd = /**
     * @param {?} event
     * @return {?}
     */
    function (event) {
        if (this.isCollapsed) { // 折叠时，“上滑”动画结束后，需要将子节点们隐藏，以便其不占用空间
            this.ecStyles = {
                display: 'none'
            };
        }
        else {
            // 如果展开的是根结点的子节点，那么根结点向下的连接线需要显示
            if (event.element.parentElement &&
                event.element.parentElement.parentElement &&
                event.element.parentElement.parentElement.classList.contains('orgchart')) {
                event.element.previousElementSibling.classList.remove('oc-is-collapsed');
            }
        }
    };
    /**
     * @param {?} e
     * @return {?}
     */
    ChartNodeComponent.prototype.onClickNode = /**
     * @param {?} e
     * @return {?}
     */
    function (e) {
        this.nodeClick.emit(this.datasource);
        if (this.select === 'single') {
            this.nodeSelectService.sendSelect(this.datasource.id);
        }
        else if (this.select === 'multiple') {
            this.isSelected = !this.isSelected;
        }
    };
    /**
     * @param {?} e
     * @return {?}
     */
    ChartNodeComponent.prototype.onNodeClick = /**
     * @param {?} e
     * @return {?}
     */
    function (e) {
        this.nodeClick.emit(e);
    };
    ChartNodeComponent.decorators = [
        { type: Component, args: [{
                    selector: 'orgchart-node',
                    template: "<div class=\"oc-node\" [id]=\"'oc-' + datasource.id\" [class.oc-is-selected]=\"isSelected\" (click)=\"onClickNode($event)\">\n\t<ng-container\n\t\t*ngTemplateOutlet=\"nodeTemplate ? nodeTemplate : defaultNodeTemplate; context:{ datasource:datasource,nodeHeading: nodeHeading, nodeContent: nodeContent }\">\n\t</ng-container>\n\t<i *ngIf=\"datasource.children\" [ngClass]=\"{'oc-icon-plus': isCollapsed, 'oc-icon-minus': !isCollapsed}\" (click)=\"toggleChildren()\" class=\"oc-toggle-btn oc-icon\"></i>\n</div>\n<div *ngIf=\"datasource.children\" [@expandCollapse]=\"isCollapsed ? 'collapsed' : 'expanded'\"\n\t(@expandCollapse.start)=\"toggleAnimStart($event)\" (@expandCollapse.done)=\"toggleAnimEnd($event)\"\n\t[ngStyle]=\"ecStyles\" class=\"oc-groups\">\n\t<ng-container *ngFor=\"let node of datasource.children;let i=index\">\n\t\t<div *ngIf=\"i % groupScale === 0\" class=\"oc-group\">\n\t\t\t<ng-container *ngFor=\"let temp of Arr(groupScale);let j=index;\">\n\t\t\t\t<orgchart-node *ngIf=\"i + j < datasource.children.length\" [datasource]=\"datasource.children[i + j]\"\n\t\t\t\t\t[nodeHeading]=\"nodeHeading\" [nodeContent]=\"nodeContent\" [nodeTemplate]=\"nodeTemplate\"\n\t\t\t\t\t[groupScale]=\"groupScale\" [select]=\"select\" (nodeClick)=\"onNodeClick($event)\">\n\t\t\t\t</orgchart-node>\n\t\t\t</ng-container>\n\t\t</div>\n\t</ng-container>\n</div>\n\n<ng-template #defaultNodeTemplate let-nodeData=\"datasource\" let-heading=\"nodeHeading\" let-content=\"nodeContent\">\n\t<div class=\"oc-node-heading\">{{nodeData[heading]}}</div>\n\t<div class=\"oc-node-content\">{{nodeData[content]}}</div>\n</ng-template>",
                    animations: [
                        trigger('expandCollapse', [
                            state('expanded', style({
                                transform: 'translateY(0)',
                                opacity: 1
                            })),
                            state('collapsed', style({
                                transform: 'translateY(-50px)',
                                opacity: 0
                            })),
                            transition('expanded => collapsed', [animate('0.2s')]),
                            transition('collapsed => expanded', [animate('0.2s')])
                        ])
                    ],
                    styles: [".oc-groups{display:flex;flex-direction:row}.orgchart>:host{align-items:center}.orgchart>:host>.oc-node::after{content:\"\";box-sizing:border-box;bottom:-10px;left:50%;height:10px;width:2px;position:absolute;background-color:rgba(217,83,79,.8)}.orgchart>:host>.oc-node.oc-is-collapsed::after{display:none}.orgchart>:host>.oc-groups>.oc-group>:host::before{height:35px;top:-10px}.oc-node{width:140px;font-family:Arial;box-sizing:border-box;position:relative;padding:3px;margin:0;border:2px solid transparent;text-align:center}.oc-node.oc-is-selected,.oc-node:hover{background-color:rgba(238,217,54,.5)}.oc-node .oc-node-heading{box-sizing:border-box;padding:2px;margin:0;width:130px;height:20px;text-align:center;font-size:.75rem;font-weight:700;overflow:hidden;text-overflow:ellipsis;white-space:nowrap;background-color:rgba(217,83,79,.8);color:#fff;border-radius:4px 4px 0 0}.oc-node .oc-node-content{box-sizing:border-box;padding:2px;width:130px;height:20px;font-size:.625rem;border:1px solid rgba(217,83,79,.8);border-radius:0 0 4px 4px;text-align:center;background-color:#fff;color:#333;text-overflow:ellipsis;white-space:nowrap}.oc-node .oc-toggle-btn{color:#fff;background-color:rgba(68,157,68,.6);position:absolute;left:5px;bottom:5px}.oc-group{display:flex;flex-direction:column;padding-left:20px;position:relative;padding-top:20px}.orgchart>:host>.oc-groups>.oc-group:first-child{padding-left:10px}.oc-group:not(:last-child)::before{content:\"\";box-sizing:border-box;top:10px;left:12px;width:calc(100% - 2px);position:absolute;border-color:rgba(217,83,79,.8);border-style:solid;border-width:0 0 2px}.orgchart>:host>.oc-groups>.oc-group:first-child::before{width:calc(100% + 8px);left:2px}:host{position:relative;display:flex;flex-direction:column}:host:not(:last-child){padding-bottom:10px}.oc-group>:host::before{content:\"\";box-sizing:border-box;height:35px;width:10px;position:absolute;left:-10px;top:-10px;border-color:rgba(217,83,79,.8);border-style:solid;border-width:0 0 2px 2px}.oc-group:first-child>:host:first-child::before{height:45px;top:-20px}:host:not(:last-child)::after{content:\"\";box-sizing:border-box;height:calc(100% - 35px);width:10px;position:absolute;left:-10px;top:25px;border-color:rgba(217,83,79,.8);border-style:solid;border-width:0 0 0 2px}.oc-icon{display:inline-block;width:1rem;height:1rem;line-height:1rem;text-align:center}.oc-icon-minus::before{content:\"\\2212\"}.oc-icon-plus::before{content:\"\\002B\"}"]
                }] }
    ];
    /** @nocollapse */
    ChartNodeComponent.ctorParameters = function () { return [
        { type: NodeSelectService }
    ]; };
    ChartNodeComponent.propDecorators = {
        datasource: [{ type: Input }],
        nodeHeading: [{ type: Input }],
        nodeContent: [{ type: Input }],
        nodeTemplate: [{ type: Input }],
        groupScale: [{ type: Input }],
        select: [{ type: Input }],
        nodeClick: [{ type: Output }]
    };
    return ChartNodeComponent;
}());
export { ChartNodeComponent };
if (false) {
    /** @type {?} */
    ChartNodeComponent.prototype.datasource;
    /** @type {?} */
    ChartNodeComponent.prototype.nodeHeading;
    /** @type {?} */
    ChartNodeComponent.prototype.nodeContent;
    /** @type {?} */
    ChartNodeComponent.prototype.nodeTemplate;
    /** @type {?} */
    ChartNodeComponent.prototype.groupScale;
    /** @type {?} */
    ChartNodeComponent.prototype.select;
    /** @type {?} */
    ChartNodeComponent.prototype.nodeClick;
    /** @type {?} */
    ChartNodeComponent.prototype.Arr;
    /** @type {?} */
    ChartNodeComponent.prototype.isCollapsed;
    /** @type {?} */
    ChartNodeComponent.prototype.ecStyles;
    /** @type {?} */
    ChartNodeComponent.prototype.isSelected;
    /** @type {?} */
    ChartNodeComponent.prototype.subscription;
    /**
     * @type {?}
     * @private
     */
    ChartNodeComponent.prototype.nodeSelectService;
}
//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJmaWxlIjoiY2hhcnQtbm9kZS5jb21wb25lbnQuanMiLCJzb3VyY2VSb290Ijoibmc6Ly9AZGFiZW5nL25nLW9yZ2NoYXJ0LyIsInNvdXJjZXMiOlsibGliL2NvbXBvbmVudHMvb3JnY2hhcnQvY2hhcnQtbm9kZS9jaGFydC1ub2RlLmNvbXBvbmVudC50cyJdLCJuYW1lcyI6W10sIm1hcHBpbmdzIjoiOzs7O0FBQUEsT0FBTyxFQUFFLFNBQVMsRUFBVSxLQUFLLEVBQUUsTUFBTSxFQUFFLFlBQVksRUFBRSxXQUFXLEVBQUUsTUFBTSxlQUFlLENBQUM7QUFFNUYsT0FBTyxFQUFFLElBQUksRUFBRSxNQUFNLDZCQUE2QixDQUFDO0FBQ25ELE9BQU8sRUFBRSxpQkFBaUIsRUFBRSxNQUFNLHdDQUF3QyxDQUFDO0FBQzNFLE9BQU8sRUFDTCxPQUFPLEVBQ1AsS0FBSyxFQUNMLEtBQUssRUFDTCxPQUFPLEVBQ1AsVUFBVSxFQUNYLE1BQU0scUJBQXFCLENBQUM7QUFFN0I7SUEwQ0UsNEJBQW9CLGlCQUFvQztRQUF4RCxpQkFTQztRQVRtQixzQkFBaUIsR0FBakIsaUJBQWlCLENBQW1CO1FBUjlDLGNBQVMsR0FBRyxJQUFJLFlBQVksRUFBTyxDQUFDO1FBRTlDLFFBQUcsR0FBRyxLQUFLLENBQUMsQ0FBQyxvQ0FBb0M7O1FBQ2pELGdCQUFXLEdBQUcsS0FBSyxDQUFDO1FBTWxCLHFDQUFxQztRQUNyQyxJQUFJLENBQUMsWUFBWSxHQUFHLElBQUksQ0FBQyxpQkFBaUIsQ0FBQyxTQUFTLEVBQUUsQ0FBQyxTQUFTOzs7O1FBQUMsVUFBQSxTQUFTO1lBQ3hFLElBQUksU0FBUyxJQUFJLFNBQVMsQ0FBQyxFQUFFLEVBQUU7Z0JBQzdCLEtBQUksQ0FBQyxVQUFVLEdBQUcsS0FBSSxDQUFDLFVBQVUsQ0FBQyxFQUFFLEtBQUssU0FBUyxDQUFDLEVBQUUsQ0FBQzthQUN2RDtpQkFBTSxFQUFFLGdEQUFnRDtnQkFDdkQsS0FBSSxDQUFDLFVBQVUsR0FBRyxLQUFLLENBQUM7YUFDekI7UUFDSCxD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QUFNLENBQUMsQ0FBQyxDQUFDOzRCQUN0RCxVQUFVLENBQUMsdUJBQXVCLEVBQUUsQ0FBQyxPQUFPLENBQUMsTUFBTSxDQUFDLENBQUMsQ0FBQzt5QkFDdkQsQ0FBQztxQkFDSDs7aUJBQ0Y7Ozs7Z0JBakNRLGlCQUFpQjs7OzZCQW9DdkIsS0FBSzs4QkFDTCxLQUFLOzhCQUNMLEtBQUs7K0JBQ0wsS0FBSzs2QkFDTCxLQUFLO3lCQUNMLEtBQUs7NEJBRUwsTUFBTTs7SUE4RVQseUJBQUM7Q0FBQSxBQWhIRCxJQWdIQztTQXZGWSxrQkFBa0I7OztJQUU3Qix3Q0FBMEI7O0lBQzFCLHlDQUFxQjs7SUFDckIseUNBQXFCOztJQUNyQiwwQ0FBd0M7O0lBQ3hDLHdDQUE0Qjs7SUFDNUIsb0NBQXdCOztJQUV4Qix1Q0FBOEM7O0lBRTlDLGlDQUFZOztJQUNaLHlDQUFvQjs7SUFDcEIsc0NBQWlCOztJQUNqQix3Q0FBb0I7O0lBQ3BCLDBDQUEyQjs7Ozs7SUFFZiwrQ0FBNEMiLCJzb3VyY2VzQ29udGVudCI6WyJpbXBvcnQgeyBDb21wb25lbnQsIE9uSW5pdCwgSW5wdXQsIE91dHB1dCwgRXZlbnRFbWl0dGVyLCBUZW1wbGF0ZVJlZiB9IGZyb20gJ0Bhbmd1bGFyL2NvcmUnO1xuaW1wb3J0IHsgU3Vic2NyaXB0aW9uIH0gZnJvbSAncnhqcyc7XG5pbXBvcnQgeyBOb2RlIH0gZnJvbSAnLi4vc2hhcmVkL21vZGVscy9ub2RlLm1vZGVsJztcbmltcG9ydCB7IE5vZGVTZWxlY3RTZXJ2aWNlIH0gZnJvbSAnLi4vc2hhcmVkL3NlcnZpY2VzL25vZGUtc2VsZWN0LnNlcnZpY2UnO1xuaW1wb3J0IHtcbiAgdHJpZ2dlcixcbiAgc3RhdGUsXG4gIHN0eWxlLFxuICBhbmltYXRlLFxuICB0cmFuc2l0aW9uXG59IGZyb20gJ0Bhbmd1bGFyL2FuaW1hdGlvbnMnO1xuXG5AQ29tcG9uZW50KHtcbiAgc2VsZWN0b3I6ICdvcmdjaGFydC1ub2RlJyxcbiAgdGVtcGxhdGVVcmw6ICcuL2NoYXJ0LW5vZGUuY29tcG9uZW50Lmh0bWwnLFxuICBzdHlsZVVybHM6IFsnLi9jaGFydC1ub2RlLmNvbXBvbmVudC5jc3MnXSxcbiAgYW5pbWF0aW9uczogW1xuICAgIHRyaWdnZXIoJ2V4cGFuZENvbGxhcHNlJywgW1xuICAgICAgc3RhdGUoXG4gICAgICAgICdleHBhbmRlZCcsXG4gICAgICAgIHN0eWxlKHtcbiAgICAgICAgICB0cmFuc2Zvcm06ICd0cmFuc2xhdGVZKDApJyxcbiAgICAgICAgICBvcGFjaXR5OiAxXG4gICAgICAgIH0pXG4gICAgICApLFxuICAgICAgc3RhdGUoXG4gICAgICAgICdjb2xsYXBzZWQnLFxuICAgICAgICBzdHlsZSh7XG4gICAgICAgICAgdHJhbnNmb3JtOiAndHJhbnNsYXRlWSgtNTBweCknLFxuICAgICAgICAgIG9wYWNpdHk6IDBcbiAgICAgICAgfSlcbiAgICAgICksXG4gICAgICB0cmFuc2l0aW9uKCdleHBhbmRlZCA9PiBjb2xsYXBzZWQnLCBbYW5pbWF0ZSgnMC4ycycpXSksXG4gICAgICB0cmFuc2l0aW9uKCdjb2xsYXBzZWQgPT4gZXhwYW5kZWQnLCBbYW5pbWF0ZSgnMC4ycycpXSlcbiAgICBdKVxuICBdXG59KVxuZXhwb3J0IGNsYXNzIENoYXJ0Tm9kZUNvbXBvbmVudCBpbXBsZW1lbnRzIE9uSW5pdCB7XG5cbiAgQElucHV0KCkgZGF0YXNvdXJjZTogTm9kZTtcbiAgQElucHV0KCkgbm9kZUhlYWRpbmc7XG4gIEBJbnB1dCgpIG5vZGVDb250ZW50O1xuICBASW5wdXQoKSBub2RlVGVtcGxhdGU6IFRlbXBsYXRlUmVmPGFueT47XG4gIEBJbnB1dCgpIGdyb3VwU2NhbGU6IG51bWJlcjtcbiAgQElucHV0KCkgc2VsZWN0OiBzdHJpbmc7XG5cbiAgQE91dHB1dCgpIG5vZGVDbGljayA9IG5ldyBFdmVudEVtaXR0ZXI8YW55PigpO1xuXG4gIEFyciA9IEFycmF5OyAvLyBBcnJheSB0eXBlIGNhcHR1cmVkIGluIGEgdmFyaWFibGVcbiAgaXNDb2xsYXBzZWQgPSBmYWxzZTtcbiAgZWNTdHlsZXM6IG9iamVjdDtcbiAgaXNTZWxlY3RlZDogYm9vbGVhbjtcbiAgc3Vic2NyaXB0aW9uOiBTdWJzY3JpcHRpb247XG5cbiAgY29uc3RydWN0b3IocHJpdmF0ZSBub2RlU2VsZWN0U2VydmljZTogTm9kZVNlbGVjdFNlcnZpY2UpIHtcbiAgICAvLyBzdWJzY3JpYmUgdG8gbm9kZSBzZWxlY3Rpb24gc3RhdHVzXG4gICAgdGhpcy5zdWJzY3JpcHRpb24gPSB0aGlzLm5vZGVTZWxlY3RTZXJ2aWNlLmdldFNlbGVjdCgpLnN1YnNjcmliZShzZWxlY3Rpb24gPT4ge1xuICAgICAgaWYgKHNlbGVjdGlvbiAmJiBzZWxlY3Rpb24uaWQpIHtcbiAgICAgICAgdGhpcy5pc1NlbGVjdGVkID0gdGhpcy5kYXRhc291cmNlLmlkID09PSBzZWxlY3Rpb24uaWQ7XG4gICAgICB9IGVsc2UgeyAvLyBjbGVhciBzZWxlY3Rpb24gd2hlbiBlbXB0eSBzZWxlY3Rpb24gcmVjZWl2ZWRcbiAgICAgICAgdGhpcy5pc1NlbGVjdGVkID0gZmFsc2U7XG4gICAgICB9XG4gICAgfSk7XG4gIH1cblxuICBuZ09uRGVzdHJveSgpIHtcbiAgICAvLyB1bnN1YnNjcmliZSB0byBlbnN1cmUgbm8gbWVtb3J5IGxlYWtzXG4gICAgdGhpcy5zdWJzY3JpcHRpb24udW5zdWJzY3JpYmUoKTtcbiAgfVxuXG4gIG5nT25Jbml0KCkge1xuICB9XG5cbiAgdG9nZ2xlQ2hpbGRyZW4oKSB7XG4gICAgdGhpcy5pc0NvbGxhcHNlZCA9ICF0aGlzLmlzQ29sbGFwc2VkO1xuICB9XG5cbiAgdG9nZ2xlQW5pbVN0YXJ0KGV2ZW50KSB7XG4gICAgaWYgKHRoaXMuaXNDb2xsYXBzZWQpIHtcbiAgICAgIC8vIOWmguaenOaKmOWPoOeahOaYr+aguee7k+eCueeahOWtkOiKgueCue+8jOmCo+S5iOaguee7k+eCueWQkeS4i+eahOi/nuaOpee6v+mcgOimgemakOiXj1xuICAgICAgaWYgKFxuICAgICAgICBldmVudC5lbGVtZW50LnBhcmVudEVsZW1lbnQgJiZcbiAgICAgICAgZXZlbnQuZWxlbWVudC5wYXJlbnRFbGVtZW50LnBhcmVudEVsZW1lbnQgJiZcbiAgICAgICAgZXZlbnQuZWxlbWVudC5wYXJlbnRFbGVtZW50LnBhcmVudEVsZW1lbnQuY2xhc3NMaXN0LmNvbnRhaW5zKCdvcmdjaGFydCcpXG4gICAgICApIHtcbiAgICAgICAgZXZlbnQuZWxlbWVudC5wcmV2aW91c0VsZW1lbnRTaWJsaW5nLmNsYXNzTGlzdC5hZGQoJ29jLWlzLWNvbGxhcHNlZCcpO1xuICAgICAgfVxuICAgIH0gZWxzZSB7IC8vIOWImuS4gOWxleW8gOeahOaXtuWAme+8jOmcgOimgemprOS4iuaKiuWtkOiKgueCueS7rOaYvuekuuWHuuadpe+8jOS7peS+v+eci+WIsOKAnOS4i+a7keKAneaViOaenFxuICAgICAgdGhpcy5lY1N0eWxlcyA9IHtcbiAgICAgICAgZGlzcGxheTogJ2ZsZXgnXG4gICAgICB9O1xuICAgIH1cbiAgfVxuXG4gIHRvZ2dsZUFuaW1FbmQoZXZlbnQpIHtcbiAgICBpZiAodGhpcy5pc0NvbGxhcHNlZCkgeyAvLyDmipjlj6Dml7bvvIzigJzkuIrmu5HigJ3liqjnlLvnu5PmnZ/lkI7vvIzpnIDopoHlsIblrZDoioLngrnku6zpmpDol4/vvIzku6Xkvr/lhbbkuI3ljaDnlKjnqbrpl7RcbiAgICAgIHRoaXMuZWNTdHlsZXMgPSB7XG4gICAgICAgIGRpc3BsYXk6ICdub25lJ1xuICAgICAgfTtcbiAgICB9IGVsc2Uge1xuICAgICAgLy8g5aaC5p6c5bGV5byA55qE5piv5qC557uT54K555qE5a2Q6IqC54K577yM6YKj5LmI5qC557uT54K55ZCR5LiL55qE6L+e5o6l57q/6ZyA6KaB5pi+56S6XG4gICAgICBpZiAoXG4gICAgICAgIGV2ZW50LmVsZW1lbnQucGFyZW50RWxlbWVudCAmJlxuICAgICAgICBldmVudC5lbGVtZW50LnBhcmVudEVsZW1lbnQucGFyZW50RWxlbWVudCAmJlxuICAgICAgICBldmVudC5lbGVtZW50LnBhcmVudEVsZW1lbnQucGFyZW50RWxlbWVudC5jbGFzc0xpc3QuY29udGFpbnMoJ29yZ2NoYXJ0JylcbiAgICAgICkge1xuICAgICAgICBldmVudC5lbGVtZW50LnByZXZpb3VzRWxlbWVudFNpYmxpbmcuY2xhc3NMaXN0LnJlbW92ZSgnb2MtaXMtY29sbGFwc2VkJyk7XG4gICAgICB9XG4gICAgfVxuICB9XG5cbiAgb25DbGlja05vZGUoZSkge1xuICAgIHRoaXMubm9kZUNsaWNrLmVtaXQodGhpcy5kYXRhc291cmNlKTtcbiAgICBpZiAodGhpcy5zZWxlY3QgPT09ICdzaW5nbGUnKSB7XG4gICAgICB0aGlzLm5vZGVTZWxlY3RTZXJ2aWNlLnNlbmRTZWxlY3QodGhpcy5kYXRhc291cmNlLmlkKTtcbiAgICB9IGVsc2UgaWYgKHRoaXMuc2VsZWN0ID09PSAnbXVsdGlwbGUnKSB7XG4gICAgICB0aGlzLmlzU2VsZWN0ZWQgPSAhdGhpcy5pc1NlbGVjdGVkO1xuICAgIH1cbiAgfVxuXG4gIG9uTm9kZUNsaWNrKGUpIHtcbiAgICB0aGlzLm5vZGVDbGljay5lbWl0KGUpO1xuICB9XG5cbn1cbiJdfQ==