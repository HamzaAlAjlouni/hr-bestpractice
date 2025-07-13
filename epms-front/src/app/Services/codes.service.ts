import { Injectable } from '@angular/core';
import { Config } from '../Config';
import { HttpClient } from '../../../node_modules/@angular/common/http';
import { identifierModuleUrl } from '@angular/compiler';

@Injectable({
  providedIn: 'root'
})
export class CodesService {

  constructor(private http: HttpClient) { }

  LoadCodes(companyID, majorNo,LanguageCode) {
    // tslint:disable-next-line:quotemark
    return this.http.get<any>(Config.WebApiUrl + "Codes/GetCodes", {
      params:
      {
        MajorNo: majorNo,
        CompanyID: companyID,
        LanguageCode:LanguageCode
      }
    });
  }
}
