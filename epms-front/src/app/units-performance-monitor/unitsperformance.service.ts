import {Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {Config} from "../Config";

@Injectable({
  providedIn: "root",
})
export class UnitsperformanceService {
  constructor(private http: HttpClient) {
  }

  GetUnitsSearch(Year, unitID, companyID, languageCode) {
    return this.http.get<any>(
      Config.WebApiUrl + "UnitsPerformance/GetUnitsSearch",
      {
        params: {
          Year: Year,
          unitID: unitID,
          companyID: companyID,
          languageCode: languageCode,
        },
      }
    );
  }

  GetEmployeeDistribution(Year, companyID) {
    return this.http.get<any>(
      Config.WebApiUrl + "UnitsPerformance/GetEmployeeDistribution",
      {
        params: {
          Year: Year,
          companyID: companyID,
        },
      }
    );
  }

  GetUnitEmployeeQouta(Year, unitID, companyID, successRate) {
    return this.http.get<any>(
      Config.WebApiUrl + "UnitsPerformance/GetUnitEmployeeQouta",
      {
        params: {
          Year: Year,
          unitID: unitID,
          companyID: companyID,
          successRate,
        },
      }
    );
  }

  GetAllEmployeeQouta(Year, companyID, successRate) {
    return this.http.get<any>(
      Config.WebApiUrl + "UnitsPerformance/GetAllEmployeeQouta",
      {
        params: {
          Year: Year,
          companyID: companyID,
          successRate,
        },
      }
    );
  }


  GetNewAllEmployeeQouta(Year, companyID, units, successRates) {
    let encodedSuccessRates = successRates.map(rate => encodeURIComponent(rate.toString()));

    return this.http.get<any>(
      Config.WebApiUrl + "UnitsPerformance/GetAllUnitsEmployeeQouta",
      {
        params: {
          year: Year,
          companyID: companyID,
          Units: units.join(','), // Convert array to string
          successRates: encodedSuccessRates.join(','), // Encode the double array and convert to string
        },
      }
    );
  }
}
