import {Injectable} from '@angular/core';
import {Config} from '../Config';
import {HttpClient} from '../../../node_modules/@angular/common/http';
import {identifierModuleUrl} from '@angular/compiler';

@Injectable({
  providedIn: 'root'
})
export class ProjectCalculationSetupService {

  constructor(private http: HttpClient) {
  }

  get() {
    return this.http.get<any>(Config.WebApiUrl + "ProjectCalculationSetup/Get");
  }

  update(
    value: string
  ) {
    return this.http.get<any>(Config.WebApiUrl + "ProjectCalculationSetup/Update", {

      params: {
        value
      }

    });

  }

}
