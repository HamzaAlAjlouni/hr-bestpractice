/**
 * @fileoverview added by tsickle
 * @suppress {checkTypes,extraRequire,missingOverride,missingReturn,unusedPrivateMembers,uselessCode} checked by tsc
 */
import { Component, Input, Output, EventEmitter, TemplateRef } from '@angular/core';
import { Node } from '../shared/models/node.model';
import { NodeSelectService } from '../shared/services/node-select.service';
import { trigger, state, style, animate, transition } from '@angular/animations';
export class ChartNodeComponent {
    /**
     * @param {?} nodeSelectService
     */
    constructor(nodeSelectService) {
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
        selection => {
            if (selection && selection.id) {
                this.isSelected = this.datasource.id === selection.id;
            }
            else { // clear selection when empty selection received
                this.isSelected = false;
            }
        }));
    }
    /**
     * @return {?}
     */
    ngOnDestroy() {
        // unsubscribe to ensure no memory leaks
        this.subscription.unsubscribe();
    }
    /**
     * @return {?}
     */
    ngOnInit() {
    }
    /**
     * @return {?}
     */
    toggleChildren() {
        this.isCollapsed = !this.isCollapsed;
    }
    /**
     * @param {?} event
     * @return {?}
     */
    toggleAnimStart(event) {
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
    }
    /**
     * @param {?} event
     * @return {?}
     */
    toggleAnimEnd(event) {
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
    }
    /**
     * @param {?} e
     * @return {?}
     */
    onClickNode(e) {
        this.nodeClick.emit(this.datasource);
        if (this.select === 'single') {
            this.nodeSelectService.sendSelect(this.datasource.id);
        }
        else if (this.select === 'multiple') {
            this.isSelected = !this.isSelected;
        }
    }
    /**
     * @param {?} e
     * @return {?}
     */
    onNodeClick(e) {
        this.nodeClick.emit(e);
    }
}
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
ChartNodeComponent.ctorParameters = () => [
    { type: NodeSelectService }
];
ChartNodeComponent.propDecorators = {
    datasource: [{ type: Input }],
    nodeHeading: [{ type: Input }],
    nodeContent: [{ type: Input }],
    nodeTemplate: [{ type: Input }],
    groupScale: [{ type: Input }],
    select: [{ type: Input }],
    nodeClick: [{ type: Output }]
};
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
//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJmaWxlIjoiY2hhcnQtbm9kZS5jb21wb25lbnQuanMiLCJzb3VyY2VSb290Ijoibmc6Ly9AZGFiZW5nL25nLW9yZ2NoYXJ0LyIsInNvdXJjZXMiOlsibGliL2NvbXBvbmVudHMvb3JnY2hhcnQvY2hhcnQtbm9kZS9jaGFydC1ub2RlLmNvbXBvbmVudC50cyJdLCJuYW1lcyI6W10sIm1hcHBpbmdzIjoiOzs7O0FBQUEsT0FBTyxFQUFFLFNBQVMsRUFBVSxLQUFLLEVBQUUsTUFBTSxFQUFFLFlBQVksRUFBRSxXQUFXLEVBQUUsTUFBTSxlQUFlLENBQUM7QUFFNUYsT0FBTyxFQUFFLElBQUksRUFBRSxNQUFNLDZCQUE2QixDQUFDO0FBQ25ELE9BQU8sRUFBRSxpQkFBaUIsRUFBRSxNQUFNLHdDQUF3QyxDQUFDO0FBQzNFLE9BQU8sRUFDTCxPQUFPLEVBQ1AsS0FBSyxFQUNMLEtBQUssRUFDTCxPQUFPLEVBQ1AsVUFBVSxFQUNYLE1BQU0scUJBQXFCLENBQUM7QUEyQjdCLE1BQU0sT0FBTyxrQkFBa0I7Ozs7SUFpQjdCLFlBQW9CLGlCQUFvQztRQUFwQyxzQkFBaUIsR0FBakIsaUJBQWlCLENBQW1CO1FBUjlDLGNBQVMsR0FBRyxJQUFJLFlBQVksRUFBTyxDQUFDO1FBRTlDLFFBQUcsR0FBRyxLQUFLLENBQUMsQ0FBQyxvQ0FBb0M7O1FBQ2pELGdCQUFXLEdBQUcsS0FBSyxDQUFDO1FBTWxCLHFDQUFxQztRQUNyQyxJQUFJLENBQUMsWUFBWSxHQUFHLElBQUksQ0FBQyxpQkFBaUIsQ0FBQyxTQUFTLEVBQUUsQ0FBQyxTQUFTOzs7O1FBQUMsU0FBUyxDQUFDLEVBQUU7WUFDM0UsSUFBSSxTQUFTLElBQUksU0FBUyxDQUFDLEVBQUUsRUFBRTtnQkFDN0IsSUFBSSxDQUFDLFVBQVUsR0FBRyxJQUFJLENBQUMsVUFBVSxDQUFDLEVBQUUsS0FBSyxTQUFTLENBQUMsRUFBRSxDQUFDO2FBQ3ZEO2lCQUFNLEVBQUUsZ0RBQWdEO2dCQUN2RCxJQUFJLENBQUMsVUFBVSxHQUFHLEtBQUssQ0FB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LENBQUMsT0FBTyxDQUFDLE1BQU0sQ0FBQyxDQUFDLENBQUM7cUJBQ3ZELENBQUM7aUJBQ0g7O2FBQ0Y7Ozs7WUFqQ1EsaUJBQWlCOzs7eUJBb0N2QixLQUFLOzBCQUNMLEtBQUs7MEJBQ0wsS0FBSzsyQkFDTCxLQUFLO3lCQUNMLEtBQUs7cUJBQ0wsS0FBSzt3QkFFTCxNQUFNOzs7O0lBUFAsd0NBQTBCOztJQUMxQix5Q0FBcUI7O0lBQ3JCLHlDQUFxQjs7SUFDckIsMENBQXdDOztJQUN4Qyx3Q0FBNEI7O0lBQzVCLG9DQUF3Qjs7SUFFeEIsdUNBQThDOztJQUU5QyxpQ0FBWTs7SUFDWix5Q0FBb0I7O0lBQ3BCLHNDQUFpQjs7SUFDakIsd0NBQW9COztJQUNwQiwwQ0FBMkI7Ozs7O0lBRWYsK0NBQTRDIiwic291cmNlc0NvbnRlbnQiOlsiaW1wb3J0IHsgQ29tcG9uZW50LCBPbkluaXQsIElucHV0LCBPdXRwdXQsIEV2ZW50RW1pdHRlciwgVGVtcGxhdGVSZWYgfSBmcm9tICdAYW5ndWxhci9jb3JlJztcbmltcG9ydCB7IFN1YnNjcmlwdGlvbiB9IGZyb20gJ3J4anMnO1xuaW1wb3J0IHsgTm9kZSB9IGZyb20gJy4uL3NoYXJlZC9tb2RlbHMvbm9kZS5tb2RlbCc7XG5pbXBvcnQgeyBOb2RlU2VsZWN0U2VydmljZSB9IGZyb20gJy4uL3NoYXJlZC9zZXJ2aWNlcy9ub2RlLXNlbGVjdC5zZXJ2aWNlJztcbmltcG9ydCB7XG4gIHRyaWdnZXIsXG4gIHN0YXRlLFxuICBzdHlsZSxcbiAgYW5pbWF0ZSxcbiAgdHJhbnNpdGlvblxufSBmcm9tICdAYW5ndWxhci9hbmltYXRpb25zJztcblxuQENvbXBvbmVudCh7XG4gIHNlbGVjdG9yOiAnb3JnY2hhcnQtbm9kZScsXG4gIHRlbXBsYXRlVXJsOiAnLi9jaGFydC1ub2RlLmNvbXBvbmVudC5odG1sJyxcbiAgc3R5bGVVcmxzOiBbJy4vY2hhcnQtbm9kZS5jb21wb25lbnQuY3NzJ10sXG4gIGFuaW1hdGlvbnM6IFtcbiAgICB0cmlnZ2VyKCdleHBhbmRDb2xsYXBzZScsIFtcbiAgICAgIHN0YXRlKFxuICAgICAgICAnZXhwYW5kZWQnLFxuICAgICAgICBzdHlsZSh7XG4gICAgICAgICAgdHJhbnNmb3JtOiAndHJhbnNsYXRlWSgwKScsXG4gICAgICAgICAgb3BhY2l0eTogMVxuICAgICAgICB9KVxuICAgICAgKSxcbiAgICAgIHN0YXRlKFxuICAgICAgICAnY29sbGFwc2VkJyxcbiAgICAgICAgc3R5bGUoe1xuICAgICAgICAgIHRyYW5zZm9ybTogJ3RyYW5zbGF0ZVkoLTUwcHgpJyxcbiAgICAgICAgICBvcGFjaXR5OiAwXG4gICAgICAgIH0pXG4gICAgICApLFxuICAgICAgdHJhbnNpdGlvbignZXhwYW5kZWQgPT4gY29sbGFwc2VkJywgW2FuaW1hdGUoJzAuMnMnKV0pLFxuICAgICAgdHJhbnNpdGlvbignY29sbGFwc2VkID0+IGV4cGFuZGVkJywgW2FuaW1hdGUoJzAuMnMnKV0pXG4gICAgXSlcbiAgXVxufSlcbmV4cG9ydCBjbGFzcyBDaGFydE5vZGVDb21wb25lbnQgaW1wbGVtZW50cyBPbkluaXQge1xuXG4gIEBJbnB1dCgpIGRhdGFzb3VyY2U6IE5vZGU7XG4gIEBJbnB1dCgpIG5vZGVIZWFkaW5nO1xuICBASW5wdXQoKSBub2RlQ29udGVudDtcbiAgQElucHV0KCkgbm9kZVRlbXBsYXRlOiBUZW1wbGF0ZVJlZjxhbnk+O1xuICBASW5wdXQoKSBncm91cFNjYWxlOiBudW1iZXI7XG4gIEBJbnB1dCgpIHNlbGVjdDogc3RyaW5nO1xuXG4gIEBPdXRwdXQoKSBub2RlQ2xpY2sgPSBuZXcgRXZlbnRFbWl0dGVyPGFueT4oKTtcblxuICBBcnIgPSBBcnJheTsgLy8gQXJyYXkgdHlwZSBjYXB0dXJlZCBpbiBhIHZhcmlhYmxlXG4gIGlzQ29sbGFwc2VkID0gZmFsc2U7XG4gIGVjU3R5bGVzOiBvYmplY3Q7XG4gIGlzU2VsZWN0ZWQ6IGJvb2xlYW47XG4gIHN1YnNjcmlwdGlvbjogU3Vic2NyaXB0aW9uO1xuXG4gIGNvbnN0cnVjdG9yKHByaXZhdGUgbm9kZVNlbGVjdFNlcnZpY2U6IE5vZGVTZWxlY3RTZXJ2aWNlKSB7XG4gICAgLy8gc3Vic2NyaWJlIHRvIG5vZGUgc2VsZWN0aW9uIHN0YXR1c1xuICAgIHRoaXMuc3Vic2NyaXB0aW9uID0gdGhpcy5ub2RlU2VsZWN0U2VydmljZS5nZXRTZWxlY3QoKS5zdWJzY3JpYmUoc2VsZWN0aW9uID0+IHtcbiAgICAgIGlmIChzZWxlY3Rpb24gJiYgc2VsZWN0aW9uLmlkKSB7XG4gICAgICAgIHRoaXMuaXNTZWxlY3RlZCA9IHRoaXMuZGF0YXNvdXJjZS5pZCA9PT0gc2VsZWN0aW9uLmlkO1xuICAgICAgfSBlbHNlIHsgLy8gY2xlYXIgc2VsZWN0aW9uIHdoZW4gZW1wdHkgc2VsZWN0aW9uIHJlY2VpdmVkXG4gICAgICAgIHRoaXMuaXNTZWxlY3RlZCA9IGZhbHNlO1xuICAgICAgfVxuICAgIH0pO1xuICB9XG5cbiAgbmdPbkRlc3Ryb3koKSB7XG4gICAgLy8gdW5zdWJzY3JpYmUgdG8gZW5zdXJlIG5vIG1lbW9yeSBsZWFrc1xuICAgIHRoaXMuc3Vic2NyaXB0aW9uLnVuc3Vic2NyaWJlKCk7XG4gIH1cblxuICBuZ09uSW5pdCgpIHtcbiAgfVxuXG4gIHRvZ2dsZUNoaWxkcmVuKCkge1xuICAgIHRoaXMuaXNDb2xsYXBzZWQgPSAhdGhpcy5pc0NvbGxhcHNlZDtcbiAgfVxuXG4gIHRvZ2dsZUFuaW1TdGFydChldmVudCkge1xuICAgIGlmICh0aGlzLmlzQ29sbGFwc2VkKSB7XG4gICAgICAvLyDlpoLmnpzmipjlj6DnmoTmmK/moLnnu5PngrnnmoTlrZDoioLngrnvvIzpgqPkuYjmoLnnu5PngrnlkJHkuIvnmoTov57mjqXnur/pnIDopoHpmpDol49cbiAgICAgIGlmIChcbiAgICAgICAgZXZlbnQuZWxlbWVudC5wYXJlbnRFbGVtZW50ICYmXG4gICAgICAgIGV2ZW50LmVsZW1lbnQucGFyZW50RWxlbWVudC5wYXJlbnRFbGVtZW50ICYmXG4gICAgICAgIGV2ZW50LmVsZW1lbnQucGFyZW50RWxlbWVudC5wYXJlbnRFbGVtZW50LmNsYXNzTGlzdC5jb250YWlucygnb3JnY2hhcnQnKVxuICAgICAgKSB7XG4gICAgICAgIGV2ZW50LmVsZW1lbnQucHJldmlvdXNFbGVtZW50U2libGluZy5jbGFzc0xpc3QuYWRkKCdvYy1pcy1jb2xsYXBzZWQnKTtcbiAgICAgIH1cbiAgICB9IGVsc2UgeyAvLyDliJrkuIDlsZXlvIDnmoTml7blgJnvvIzpnIDopoHpqazkuIrmiorlrZDoioLngrnku6zmmL7npLrlh7rmnaXvvIzku6Xkvr/nnIvliLDigJzkuIvmu5HigJ3mlYjmnpxcbiAgICAgIHRoaXMuZWNTdHlsZXMgPSB7XG4gICAgICAgIGRpc3BsYXk6ICdmbGV4J1xuICAgICAgfTtcbiAgICB9XG4gIH1cblxuICB0b2dnbGVBbmltRW5kKGV2ZW50KSB7XG4gICAgaWYgKHRoaXMuaXNDb2xsYXBzZWQpIHsgLy8g5oqY5Y+g5pe277yM4oCc5LiK5ruR4oCd5Yqo55S757uT5p2f5ZCO77yM6ZyA6KaB5bCG5a2Q6IqC54K55Lus6ZqQ6JeP77yM5Lul5L6/5YW25LiN5Y2g55So56m66Ze0XG4gICAgICB0aGlzLmVjU3R5bGVzID0ge1xuICAgICAgICBkaXNwbGF5OiAnbm9uZSdcbiAgICAgIH07XG4gICAgfSBlbHNlIHtcbiAgICAgIC8vIOWmguaenOWxleW8gOeahOaYr+aguee7k+eCueeahOWtkOiKgueCue+8jOmCo+S5iOaguee7k+eCueWQkeS4i+eahOi/nuaOpee6v+mcgOimgeaYvuekulxuICAgICAgaWYgKFxuICAgICAgICBldmVudC5lbGVtZW50LnBhcmVudEVsZW1lbnQgJiZcbiAgICAgICAgZXZlbnQuZWxlbWVudC5wYXJlbnRFbGVtZW50LnBhcmVudEVsZW1lbnQgJiZcbiAgICAgICAgZXZlbnQuZWxlbWVudC5wYXJlbnRFbGVtZW50LnBhcmVudEVsZW1lbnQuY2xhc3NMaXN0LmNvbnRhaW5zKCdvcmdjaGFydCcpXG4gICAgICApIHtcbiAgICAgICAgZXZlbnQuZWxlbWVudC5wcmV2aW91c0VsZW1lbnRTaWJsaW5nLmNsYXNzTGlzdC5yZW1vdmUoJ29jLWlzLWNvbGxhcHNlZCcpO1xuICAgICAgfVxuICAgIH1cbiAgfVxuXG4gIG9uQ2xpY2tOb2RlKGUpIHtcbiAgICB0aGlzLm5vZGVDbGljay5lbWl0KHRoaXMuZGF0YXNvdXJjZSk7XG4gICAgaWYgKHRoaXMuc2VsZWN0ID09PSAnc2luZ2xlJykge1xuICAgICAgdGhpcy5ub2RlU2VsZWN0U2VydmljZS5zZW5kU2VsZWN0KHRoaXMuZGF0YXNvdXJjZS5pZCk7XG4gICAgfSBlbHNlIGlmICh0aGlzLnNlbGVjdCA9PT0gJ211bHRpcGxlJykge1xuICAgICAgdGhpcy5pc1NlbGVjdGVkID0gIXRoaXMuaXNTZWxlY3RlZDtcbiAgICB9XG4gIH1cblxuICBvbk5vZGVDbGljayhlKSB7XG4gICAgdGhpcy5ub2RlQ2xpY2suZW1pdChlKTtcbiAgfVxuXG59XG4iXX0=