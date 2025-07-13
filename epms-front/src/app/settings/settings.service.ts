import { Injectable } from '@angular/core';
import { Config } from '../Config';
import { HttpClient } from '../../../node_modules/@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class SettingsService {

  constructor(private http: HttpClient) { }

  // #region Skills Services
  SaveSettings(code, name,languageCode) {
    return this.http.get<any>(Config.WebApiUrl + "Settings/SaveSkillsTypes", {
      params:
      {
        code: code,
        name: name,
        languageCode:languageCode
      }
    });
  }

  LoadAllSkillsTypes(languageCode) {
    return this.http.get<any>(Config.WebApiUrl + "Settings/LoadAllSkillsTypes", {
      params:
      {
        languageCode:languageCode
      }});
  }

  getSkillbyID(id,languageCode) {
    return this.http.get<any>(Config.WebApiUrl + "Settings/getSkillbyID", {
      params:
      {
        id: id,
        languageCode:languageCode
      }
    });
  }

  DeleteSkillByID(id) {
    return this.http.get<any>(Config.WebApiUrl + "Settings/DeleteSkillByID", {
      params:
      {
        id: id
      }
    });
  }

  UpdateSettings(id, code, name,languageCode) {
    return this.http.get<any>(Config.WebApiUrl + "Settings/UpdateSettings", {
      params:
      {
        id: id,
        code: code,
        name: name,
        languageCode:languageCode
      }
    });
  }
  // #endregion


  // #region Employee Levels Services
  SaveLevels(code, name, number) {
    return this.http.get<any>(Config.WebApiUrl + "Settings/SaveLevels", {
      params:
      {
        code: code,
        name: name,
        number: number
      }
    });
  }

  LoadAllEmpLevels() {
    return this.http.get<any>(Config.WebApiUrl + "Settings/LoadAllEmpLevels"

    );
  }

  getEmpLvlbyID(id) {
    return this.http.get<any>(Config.WebApiUrl + "Settings/getEmpLvlbyID", {
      params:
      {
        id: id
      }
    });
  }

  DeleteEmpLvlByID(id) {
    return this.http.get<any>(Config.WebApiUrl + "Settings/DeleteEmpLvlByID", {
      params:
      {
        id: id
      }
    });
  }

  UpdateEmpLvl(id, code, name, number) {
    return this.http.get<any>(Config.WebApiUrl + "Settings/UpdateEmpLvl", {
      params:
      {
        id: id,
        code: code,
        name: name,
        number: number
      }
    });
  }
  // #endregion



  LoadAllYears() {
    return this.http.get<any>(Config.WebApiUrl + "Years/GetYears");
  }



  LoadAllPerformanceLevels(companyId, yearId) {
    return this.http.get<any>(Config.WebApiUrl + "Settings/LoadAllPerformanceLevels",
      {
        params:
        {
          companyId: companyId,
          yearId: yearId
        }
      }
    );
  }


  getPerformanceByID(id) {
    return this.http.get<any>(Config.WebApiUrl + "Settings/getPerformanceByID", {
      params:
      {
        id: id
      }
    });
  }

  UpdatePerformanceLevel(id, name, number, percent, year, companyId) {
    return this.http.get<any>(Config.WebApiUrl + "Settings/UpdatePerformanceLevel", {
      params:
      {
        id: id,

        name: name,
        number: number,
        percent: percent,
        year: year,
        companyId: companyId,
      }
    });
  }


  SavePerformaceLevels(name, number, percent, year, companyId) {
    return this.http.get<any>(Config.WebApiUrl + "Settings/SavePerformaceLevels", {
      params:
      {
        name: name,
        number: number,
        percent: percent,
        year: year,
        companyId: companyId,
      }
    });
  }


  DeletePerformanceByID(id) {
    return this.http.get<any>(Config.WebApiUrl + "Settings/DeletePerformanceByID", {
      params:
      {
        id: id
      }
    });
  }


  LoadAllPerformanceLevelQuotaView(companyId, yearId,lang) {
    return this.http.get<any>(Config.WebApiUrl + "Settings/LoadAllPerformanceLevelQuotaView",
      {
        params:
        {
          companyId: companyId,
          yearId: yearId,
          languageCode: lang
        }
      }
    );
  }


  EditPerformanceQuota(FromPercentage, companyId, yearId) {
    return this.http.get<any>(Config.WebApiUrl + "Settings/EditPerformanceQuota",
      {
        params:
        {
          FromPercentage: FromPercentage,
          companyId: companyId,
          yearId: yearId

        }
      }
    );
  }


  NewPerformanceQuota(companyId, yearId) {
    return this.http.get<any>(Config.WebApiUrl + "Settings/NewPerformanceQuota",
      {
        params:
        {
          companyId: companyId,
          yearId: yearId
        }
      }
    );
  }

  DeletePerformanceQuota(FromPercentage, companyId, yearId) {
    return this.http.get<any>(Config.WebApiUrl + "Settings/DeletePerformanceQuota",
      {
        params:
        {
          FromPercentage: FromPercentage,
          companyId: companyId,
          yearId: yearId
        }
      }
    );
  }



  SavePerformaceQuota(Quota, companyId, yearId) {

    let form = new FormData();
    form.append("Quota", JSON.stringify(Quota));
    form.append("companyId", JSON.stringify(companyId));
    form.append("yearId", JSON.stringify(yearId));

    return this.http.post<any>(Config.WebApiUrl + "Settings/SavePerformaceQuota", form

    );
  }


  UpdatePerformaceQuota(Quota, companyId, yearId) {
    let form = new FormData();
    form.append("Quota", JSON.stringify(Quota));
    form.append("companyId", JSON.stringify(companyId));
    form.append("yearId", JSON.stringify(yearId));

    return this.http.post<any>(Config.WebApiUrl + "Settings/UpdatePerformaceQuota", form

    );
  }

//#region traffic lights
  LoadTrafficLights(companyId, yearId) {
    return this.http.get<any>(Config.WebApiUrl + "TrafficLight/LoadTrafficLights",
      {
        params:
        {
          companyId: companyId,
          yearId: yearId
        }
      }
    );
  }


  LoadTrafficLightsByID(id) {
    return this.http.get<any>(Config.WebApiUrl + "TrafficLight/LoadTrafficLightsByID",
      {
        params:
        {
          id:id
        }
      }
    );
  }

  DeleteTrafficLight(id) {
    return this.http.get<any>(Config.WebApiUrl + "TrafficLight/DeleteTrafficLight",
      {
        params:
        {
          id:id
        }
      }
    );
  }

  SaveTrafficLight(name, perc_from, perc_to,color, companyID,year) {
    return this.http.get<any>(Config.WebApiUrl + "TrafficLight/SaveTrafficLight",
      {
        params:
        {
          name:name,
          perc_from:perc_from,
          perc_to:perc_to,
          color:color,
          companyID:companyID,
          year:year
        }
      }
    );
  }

  UpdateTrafficLight(id,name, perc_from, perc_to,color, companyID,year) {
    return this.http.get<any>(Config.WebApiUrl + "TrafficLight/UpdateTrafficLight",
      {
        params:
        {
          id:id,
          name:name,
          perc_from:perc_from,
          perc_to:perc_to,
          color:color,
          companyID:companyID,
          year:year
        }
      }
    );
  }
//#endregion traffic light


}
