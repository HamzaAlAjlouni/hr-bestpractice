import { OnInit, EventEmitter, TemplateRef } from '@angular/core';
import { Subscription } from 'rxjs';
import { Node } from '../shared/models/node.model';
import { NodeSelectService } from '../shared/services/node-select.service';
export declare class ChartNodeComponent implements OnInit {
    private nodeSelectService;
    datasource: Node;
    nodeHeading: any;
    nodeContent: any;
    nodeTemplate: TemplateRef<any>;
    groupScale: number;
    select: string;
    nodeClick: EventEmitter<any>;
    Arr: ArrayConstructor;
    isCollapsed: boolean;
    ecStyles: object;
    isSelected: boolean;
    subscription: Subscription;
    constructor(nodeSelectService: NodeSelectService);
    ngOnDestroy(): void;
    ngOnInit(): void;
    toggleChildren(): void;
    toggleAnimStart(event: any): void;
    toggleAnimEnd(event: any): void;
    onClickNode(e: any): void;
    onNodeClick(e: any): void;
}
