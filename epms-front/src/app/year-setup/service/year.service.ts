import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Config } from "./../../Config";

@Injectable({
  providedIn: "root",
})
export class YearService {
  constructor(private http: HttpClient) {}

  getYear() {
    return this.http.get<any>(`${Config.WebApiUrl}Years/GetYears`);
  }

  deleteYear(id) {
    return this.http.get<any>(`${Config.WebApiUrl}Years/Delete`, {
      params: {
        id,
      },
    });
  }

  addYear(year, CreatedBy) {
    return this.http.get<any>(`${Config.WebApiUrl}Years/Save`, {
      params: {
        year,
        CreatedBy,
      },
    });
  }
}
