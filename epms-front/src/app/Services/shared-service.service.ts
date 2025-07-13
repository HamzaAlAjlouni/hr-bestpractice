import { Injectable } from '@angular/core';
import { HttpClient } from '../../../node_modules/@angular/common/http';
import { map } from '../../../node_modules/rxjs/operators';

@Injectable()
export class SharedServiceService {

  constructor(private http:HttpClient) { }
 
}  
