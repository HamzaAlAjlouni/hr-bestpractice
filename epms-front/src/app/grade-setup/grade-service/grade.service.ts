import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Config } from "../../Config";

@Injectable({
  providedIn: "root",
})
export class GradeService {
  constructor(private http: HttpClient) {}
  getGrade(CompanyID) {
    return this.http.get<any>(`${Config.WebApiUrl}Scales/GetScales`, {
      params: {
        CompanyID,
      },
    });
  }

  getGradeById(ID, languageCode) {
    return this.http.get<any>(`${Config.WebApiUrl}Scales/getByID`, {
      params: {
        ID,
        languageCode,
      },
    });
  }
  deleteGrade(ID) {
    return this.http.get<any>(`${Config.WebApiUrl}Scales/Delete`, {
      params: {
        ID,
      },
    });
  }

  updateGrade(
    ID,
    ScaleNumber,
    CompanyID,
    ModifiedBy,
    Name,
    ScaleCode,
    languageCode
  ) {
    return this.http.get<any>(`${Config.WebApiUrl}Scales/Update`, {
      params: {
        ID,
        ScaleNumber,
        CompanyID,
        ModifiedBy,
        Name,
        ScaleCode,
        languageCode,
      },
    });
  }

  addGrade(
    ScaleNumber,
    CompanyID,
    CreatedBy,
    Name,
    ScaleCode,
    languageCode
  ) {
    return this.http.get<any>(`${Config.WebApiUrl}Scales/Save`, {
      params: {
        ScaleNumber,
        CompanyID,
        CreatedBy,
        Name,
        ScaleCode,
        languageCode,
      },
    });
  }
}
