import {Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {Config} from "./../../Config";

@Injectable({
  providedIn: "root",
})
export class QuartersService {
  constructor(private http: HttpClient) {
  }

  getQuarters() {
    return this.http.get<any>(`${Config.WebApiUrl}QuatersActivation/GetQuatersActivation`);
  }

  updateQuarterActivation(ID, Status, modified) {
    return this.http.get<any>(`${Config.WebApiUrl}QuatersActivation/UpdateQuaterActivation`, {
      params: {
        Status,
        ID,
        modified,
      },
    });
  }
}
