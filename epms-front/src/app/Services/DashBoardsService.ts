import {Injectable} from '@angular/core';
import {Config} from 'src/app/Config';
import {Observable} from 'rxjs';
import {HttpClient} from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
/* tslint:disable */
export class DashBoardService {

  constructor(private http: HttpClient) {
  }

  GetProjectsPerUnits(): Observable<any> {
    return this.http.get(Config.WebApiUrl + 'DashBoard/GetPojPerUnits');
  }

  GetObjectiveWeightPerYear(companyId, yearId, languageCode,unitId:number=0): Observable<any> {
    return this.http.get(Config.WebApiUrl + 'DashBoard/GetObjectiveWeightPerYear?companyId=' + companyId + '&yearId=' + yearId + '&languageCode=' + languageCode+'&unitId='+unitId);
  }

  GetObjectiveResultWeightPercentage(year, companyId, lang,unitId:number=0): Observable<any> {
    return this.http.get(Config.WebApiUrl + 'DashBoard/GetObjectiveResultWeightPercentage?year=' + year + '&companyId=' + companyId + '&languageCode=' + lang+'&unitId='+unitId);
  }

  GetUnitsPercentage(year, companyId, lang,unitId:number=0): Observable<any> {
    return this.http.get(Config.WebApiUrl + 'DashBoard/GetUnitsPercentageData?year=' + year + '&companyId=' + companyId + '&languageCode=' + lang+'&unitId='+unitId);
  }

  GetUnuitTargetVsActualResult(yearId, companyId, lang): Observable<any> {
    return this.http.get(Config.WebApiUrl + 'DashBoard/GetUnuitTargetVsActualResult?yearId=' + yearId + '&companyId=' + companyId + '&languageCode=' + lang);
  }


  RenderActualCostVsPlannedStatistic(year, companyId, lang): Observable<any> {
    return this.http.get(Config.WebApiUrl + 'DashBoard/GetActualCostVsPlannedData?year=' + year + '&companyId=' + companyId + '&languageCode=' + lang);
  }


  GetActualCostStatisticsData(year, companyId, lang): Observable<any> {
    return this.http.get(Config.WebApiUrl + 'DashBoard/GetActualCostStatisticsData?year=' + year + '&companyId=' + companyId + '&languageCode=' + lang);
  }

  ///Employees DashBoards APIs
  GetEmployeeAssessment(year, lang,unitId=0): Observable<any> {
    return this.http.get(Config.WebApiUrl + 'DashBoard/GetEmployeeAssessment?yearId=' + year + '&languageCode=' + lang+ '&unitId=' + unitId);
  }

  GetNumberEmpForUnitsVsNeedNumber(yearId, companyId, lang): Observable<any> {
    return this.http.get(Config.WebApiUrl + 'DashBoard/GetNumberEmpForUnitsVsNeedNumber?yearId=' + yearId + '&companyId=' + companyId + '&languageCode=' + lang);
  }

  GetEmployeeRank(yearId, companyId, unitId, lang): Observable<any> {
    return this.http.get(Config.WebApiUrl + 'DashBoard/GetEmployeeRank?yearId=' + yearId + '&companyId=' + companyId + '&unitId=' + unitId + '&languageCode=' + lang);
  }

  getAdminDashboard(yearId: number, unitId: number = 0): Observable<any> {

    return this.http.get(`${Config.WebApiUrl}Reports/GetProjectActionPlans?year=${yearId}&unitId=${unitId}`);


  }

  getEmployeeDashboard(unitId: number = 0): Observable<any> {

    return this.http.get(`${Config.WebApiUrl}Reports/GetEmployeesStatistics?unitId=${unitId}`);
  }

  getUnitContributionsPerYear(yearId, unitId: number = 0): Observable<any> {

    return this.http.get(`${Config.WebApiUrl}DashBoard/GetUnitContributionsPerYear?yearId=${yearId}&unitId=${unitId}`);


  }

  getObjectiveKPIContributionsPerYear(yearId): Observable<any> {

    return this.http.get(Config.WebApiUrl + 'DashBoard/GetObjectiveKPIContributionsPerYear?yearId=' + yearId);


  }

}
