import { Injectable } from '@angular/core';
import { Config } from '../Config';
import { HttpClient } from '../../../node_modules/@angular/common/http';
import { identifierModuleUrl } from '@angular/compiler';

@Injectable({
  providedIn: 'root'
})
export class UnitsService {

  constructor(private http: HttpClient) {

  }

  DeleteUnit(id) {
    return this.http.get<any>(Config.WebApiUrl + "Unites/Delete", {
      params:
      {
        ID: id
      }
    });

  }
  LoadUnits(CompanyID, UniteName, LanguageCode) {
    return this.http.get<any>(Config.WebApiUrl + "Unites/GetUnits", {
      params:
      {
        CompanyID: CompanyID,
        UniteName: UniteName,
        LanguageCode:LanguageCode

      }
    });
  }
  LoadUnitByID(id,LanguageCode) {
    return this.http.get<any>(Config.WebApiUrl + "Unites/getByID", {
      params:
      {
        ID: id,
        LanguageCode:LanguageCode
      }
    });
  }

  SaveUnit(Code,
    CompanyID,
    CreatedBy,
    UnitType,
    Name,
    Fax,
    Phone1,
    Phone2,
    Address,
    LanguageCode) {
    return this.http.get<any>(Config.WebApiUrl + "Unites/Save", {
      params:
      {
        Code,
        CompanyID,
        CreatedBy,
        UnitType,
        Name,
        Fax,
        Phone1,
        Phone2,
        Address,
        LanguageCode
      }
    });
  }
  UpdateUnit(
    ID,
    Code,
    CompanyID,
    ModifiedBy,
    UnitType,
    Name,
    Fax,
    Phone1,
    Phone2,
    Address,
    LanguageCode
  ) {
    return this.http.get<any>(Config.WebApiUrl + "Unites/Update", {
      params:
      {
        ID,
        Code,
        CompanyID,
        ModifiedBy,
        UnitType,
        Name,
        Fax,
        Phone1,
        Phone2,
        Address,
        LanguageCode
      }
    });

  }
}
