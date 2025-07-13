import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Config } from 'src/app/Config';

@Injectable({
  providedIn: 'root'
})
export class EmployeeSegmentService {

  constructor(private http: HttpClient) { }

  SaveEmpPerf_segments(
    name,
    desc,
    segment,
    perc_from,
    perc_to,
    year,
    companyID
  ) {
    return this.http.get<any>(Config.WebApiUrl + "EmployeePerformance/SaveEmpPerf_segments", {
      params:
      {
        name: name,
        desc: desc,
        segment: segment,
        perc_from: perc_from,
        perc_to: perc_to,
        year: year,
        companyID: companyID
      }
    });
  }

  UpdateEmpPerf_segments(
    id,
    name,
    desc,
    segment,
    perc_from,
    perc_to,
    year,
    companyID
  ) {
    return this.http.get<any>(Config.WebApiUrl + "EmployeePerformance/UpdateEmpPerf_segments", {
      params:
      {
        id: id,
        name: name,
        desc: desc,
        segment: segment,
        perc_from: perc_from,
        perc_to: perc_to,
        year: year,
        companyID: companyID
      }
    });
  }

  deleteEmpSegment(id) {
    return this.http.get<any>(Config.WebApiUrl + "EmployeePerformance/deleteEmpSegment", {
      params:
      {
        id: id
      }
    });
  }

  getEmpSegmentByID(id) {
    return this.http.get<any>(Config.WebApiUrl + "EmployeePerformance/getEmpSegmentByID", {
      params:
      {
        id: id
      }
    });
  }
  
  getEmpPerfSegments(name,Year,companyID) {
    return this.http.get<any>(Config.WebApiUrl + "EmployeePerformance/getEmpPerfSegments", {
      params:
      {
        name: name,
        Year:Year,
        companyID:companyID
      }
    });
  }
}
