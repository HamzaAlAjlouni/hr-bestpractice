(function (global, factory) {
    typeof exports === 'object' && typeof module !== 'undefined' ? factory(exports, require('@angular/core'), require('@angular/common'), require('@angular/platform-browser/animations'), require('rxjs'), require('@angular/animations')) :
    typeof define === 'function' && define.amd ? define('@dabeng/ng-orgchart', ['exports', '@angular/core', '@angular/common', '@angular/platform-browser/animations', 'rxjs', '@angular/animations'], factory) :
    (global = global || self, factory((global.dabeng = global.dabeng || {}, global.dabeng['ng-orgchart'] = {}), global.ng.core, global.ng.common, global.ng.platformBrowser.animations, global.rxjs, global.ng.animations));
}(this, (function (exports, core, common, animations, rxjs, animations$1) { 'use strict';

    /**
     * @fileoverview added by tsickle
     * @suppress {checkTypes,extraRequire,missingOverride,missingReturn,unusedPrivateMembers,uselessCode} checked by tsc
     */
    var NodeSelectService = /** @class */ (function () {
        function NodeSelectService() {
            this.subject = new rxjs.Subject();
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
            { type: core.Injectable }
        ];
        /** @nocollapse */
        NodeSelectService.ctorParameters = function () { return []; };
        return NodeSelectService;
    }());
    if (false) {
        /**
         * @type {?}
         * @private
         */
        NodeSelectService.prototype.subject;
    }

    /**
     * @fileoverview added by tsickle
     * @suppress {checkTypes,extraRequire,missingOverride,missingReturn,unusedPrivateMembers,uselessCode} checked by tsc
     */
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
            this.nodeClick = new core.EventEmitter();
            this.chartClick = new core.EventEmitter();
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
            { type: core.Component, args: [{
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
            datasource: [{ type: core.Input }],
            nodeHeading: [{ type: core.Input }],
            nodeContent: [{ type: core.Input }],
            nodeTemplate: [{ type: core.Input }],
            groupScale: [{ type: core.Input }],
            pan: [{ type: core.Input }],
            zoom: [{ type: core.Input }],
            zoomoutLimit: [{ type: core.Input }],
            zoominLimit: [{ type: core.Input }],
            containerClass: [{ type: core.Input }],
            chartClass: [{ type: core.Input }],
            select: [{ type: core.Input }],
            nodeClick: [{ type: core.Output }],
            chartClick: [{ type: core.Output }]
        };
        return ChartContainerComponent;
    }());
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

    /**
     * @fileoverview added by tsickle
     * @suppress {checkTypes,extraRequire,missingOverride,missingReturn,unusedPrivateMembers,uselessCode} checked by tsc
     */
    var Node = /** @class */ (function () {
        function Node() {
        }
        return Node;
    }());
    if (false) {
        /** @type {?} */
        Node.prototype.id;
        /** @type {?} */
        Node.prototype.name;
        /** @type {?} */
        Node.prototype.title;
        /** @type {?} */
        Node.prototype.children;
    }

    /**
     * @fileoverview added by tsickle
     * @suppress {checkTypes,extraRequire,missingOverride,missingReturn,unusedPrivateMembers,uselessCode} checked by tsc
     */
    var ChartNodeComponent = /** @class */ (function () {
        function ChartNodeComponent(nodeSelectService) {
            var _this = this;
            this.nodeSelectService = nodeSelectService;
            this.nodeClick = new core.EventEmitter();
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
            { type: core.Component, args: [{
                        selector: 'orgchart-node',
                        template: "<div class=\"oc-node\" [id]=\"'oc-' + datasource.id\" [class.oc-is-selected]=\"isSelected\" (click)=\"onClickNode($event)\">\n\t<ng-container\n\t\t*ngTemplateOutlet=\"nodeTemplate ? nodeTemplate : defaultNodeTemplate; context:{ datasource:datasource,nodeHeading: nodeHeading, nodeContent: nodeContent }\">\n\t</ng-container>\n\t<i *ngIf=\"datasource.children\" [ngClass]=\"{'oc-icon-plus': isCollapsed, 'oc-icon-minus': !isCollapsed}\" (click)=\"toggleChildren()\" class=\"oc-toggle-btn oc-icon\"></i>\n</div>\n<div *ngIf=\"datasource.children\" [@expandCollapse]=\"isCollapsed ? 'collapsed' : 'expanded'\"\n\t(@expandCollapse.start)=\"toggleAnimStart($event)\" (@expandCollapse.done)=\"toggleAnimEnd($event)\"\n\t[ngStyle]=\"ecStyles\" class=\"oc-groups\">\n\t<ng-container *ngFor=\"let node of datasource.children;let i=index\">\n\t\t<div *ngIf=\"i % groupScale === 0\" class=\"oc-group\">\n\t\t\t<ng-container *ngFor=\"let temp of Arr(groupScale);let j=index;\">\n\t\t\t\t<orgchart-node *ngIf=\"i + j < datasource.children.length\" [datasource]=\"datasource.children[i + j]\"\n\t\t\t\t\t[nodeHeading]=\"nodeHeading\" [nodeContent]=\"nodeContent\" [nodeTemplate]=\"nodeTemplate\"\n\t\t\t\t\t[groupScale]=\"groupScale\" [select]=\"select\" (nodeClick)=\"onNodeClick($event)\">\n\t\t\t\t</orgchart-node>\n\t\t\t</ng-container>\n\t\t</div>\n\t</ng-container>\n</div>\n\n<ng-template #defaultNodeTemplate let-nodeData=\"datasource\" let-heading=\"nodeHeading\" let-content=\"nodeContent\">\n\t<div class=\"oc-node-heading\">{{nodeData[heading]}}</div>\n\t<div class=\"oc-node-content\">{{nodeData[content]}}</div>\n</ng-template>",
                        animations: [
                            animations$1.trigger('expandCollapse', [
                                animations$1.state('expanded', animations$1.style({
                                    transform: 'translateY(0)',
                                    opacity: 1
                                })),
                                animations$1.state('collapsed', animations$1.style({
                                    transform: 'translateY(-50px)',
                                    opacity: 0
                                })),
                                animations$1.transition('expanded => collapsed', [animations$1.animate('0.2s')]),
                                animations$1.transition('collapsed => expanded', [animations$1.animate('0.2s')])
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
            datasource: [{ type: core.Input }],
            nodeHeading: [{ type: core.Input }],
            nodeContent: [{ type: core.Input }],
            nodeTemplate: [{ type: core.Input }],
            groupScale: [{ type: core.Input }],
            select: [{ type: core.Input }],
            nodeClick: [{ type: core.Output }]
        };
        return ChartNodeComponent;
    }());
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

    /**
     * @fileoverview added by tsickle
     * @suppress {checkTypes,extraRequire,missingOverride,missingReturn,unusedPrivateMembers,uselessCode} checked by tsc
     */
    var OrgchartModule = /** @class */ (function () {
        function OrgchartModule() {
        }
        OrgchartModule.decorators = [
            { type: core.NgModule, args: [{
                        declarations: [ChartContainerComponent, ChartNodeComponent],
                        imports: [
                            common.CommonModule,
                            animations.BrowserAnimationsModule
                        ],
                        exports: [ChartContainerComponent, ChartNodeComponent],
                        providers: [NodeSelectService],
                    },] }
        ];
        return OrgchartModule;
    }());

    exports.OrgchartModule = OrgchartModule;
    exports.ɵa = ChartContainerComponent;
    exports.ɵb = NodeSelectService;
    exports.ɵc = ChartNodeComponent;

    Object.defineProperty(exports, '__esModule', { value: true });

})));
//# sourceMappingURL=dabeng-ng-orgchart.umd.js.map
