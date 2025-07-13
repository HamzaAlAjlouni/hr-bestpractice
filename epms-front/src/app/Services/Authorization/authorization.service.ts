import { Injectable } from '@angular/core';
import { Config } from '../../Config';
import { HttpClient } from '../../../../node_modules/@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AuthorizationService {

  constructor(private http:HttpClient) { }

  LoadAuthorizedMenus(Username , CompanyID , ApplicationCode , SystemCode){
    return this.http.get<any>(Config.WebApiUrl+"Authorization/LoadAuthorizedMenus",{
      params:
      {
        Username : Username,
        CompanyID : CompanyID,
        ApplicationCode : ApplicationCode,
        SystemCode : SystemCode
      }
    });
  } 
}
