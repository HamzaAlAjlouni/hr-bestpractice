import { Observable } from 'rxjs';
export declare class NodeSelectService {
    private subject;
    constructor();
    sendSelect(id: string): void;
    clearSelect(): void;
    getSelect(): Observable<any>;
}
