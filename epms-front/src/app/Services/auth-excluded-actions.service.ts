import { Injectable } from '@angular/core'; 
import { Config } from '../Config';
import { HttpClient } from '../../../node_modules/@angular/common/http';
import { identifierModuleUrl } from '@angular/compiler';

@Injectable({
  providedIn: 'root'
})
export class AuthExcludedActionsService {

  constructor(private http: HttpClient) { }

  LoadPageExcludedActions(CompanyID, Username , URL) {
    return this.http.get<any>(Config.WebApiUrl + "AuthExcludedActions/LoadPageExcludedActions", {
      params:
      {
        Username: Username,
        URL: URL,
        CompanyID :  CompanyID

      }
    });
  }
}
