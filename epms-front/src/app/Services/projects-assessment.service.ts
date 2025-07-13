import {Injectable} from "@angular/core";
import {Config} from "../Config";
import {HttpClient} from "../../../node_modules/@angular/common/http";

@Injectable({
  providedIn: "root",
})
export class ProjectsAssessmentService {
  constructor(private http: HttpClient) {
  }

  getProjectAssessment(
    CompanyID,
    YearID,
    UnitID,
    ObjectiveID,
    lang,
    project = null
  ) {
    return this.http.get<any>(
      Config.WebApiUrl + "StratigicObjectives/GetProjectsAssessmentWithKpis",
      {
        params: {
          companyId: CompanyID,
          branchId: null,
          yearId: YearID,
          unitId: UnitID,
          stratigicObjectiveId: ObjectiveID,
          languageCode: lang,
        },
      }
    );
  }


  getProjectAssessmentWithEmployeesAssessment(
    CompanyID,
    YearID,
    UnitID,
    ObjectiveID,
    lang,
    project = null
  ) {
    return this.http.get<any>(
      Config.WebApiUrl + "StratigicObjectives/GetProjectsAssessmentWithEmployeeAssessment",
      {
        params: {
          companyId: CompanyID,
          branchId: null,
          yearId: YearID,
          unitId: UnitID,
          stratigicObjectiveId: ObjectiveID,
          languageCode: lang,
        },
      }
    );
  }


  GetPlannedAndActualWeightForAll(
    CompanyID,
    YearID,
    UnitID,
    ObjectiveID,
    lang,
    branchId,
    projectId
  ) {
    return this.http.get<any>(
      Config.WebApiUrl + "StratigicObjectives/GetPlannedWeightForAll",
      {
        params: {
          companyId: CompanyID,
          branchId: branchId,
          yearId: YearID,
          unitId: UnitID,
          stratigicObjectiveId: ObjectiveID,
          languageCode: lang,
          projectId: projectId,
        },
      }
    );
  }


  GetPlannedWeightForEachObjective(
    CompanyID,
    YearID,
    UnitID,
    lang,
    branchId,
    projectId,
    Bsc
  ) {
    return this.http.get<any>(
      Config.WebApiUrl + "StratigicObjectives/GetPlannedWeightForEachObjective",
      {
        params: {
          companyId: CompanyID,
          branchId: branchId,
          yearId: YearID,
          unitId: UnitID,
          languageCode: lang,
          projectId: projectId,
          bsc: Bsc
        },
      }
    );
  }

  SaveProjectAssessment(listProjectsAssessment, username, lang) {
    let form = new FormData();
    form.append(
      "ProjectsAssessmentData",
      JSON.stringify(listProjectsAssessment)
    );

    return this.http.post<any>(
      Config.WebApiUrl +
      "StratigicObjectives/UpdateProjectsAssessment?username=" +
      username +
      "&lang=" +
      lang,
      form
    );
  }

  getUnites(compID) {
    return this.http.get<any>(Config.WebApiUrl + "unites/GetUnits", {
      params: {
        CompanyID: compID,
      },
    });
  }

  getYears() {
    return this.http.get<any>(Config.WebApiUrl + "Years/GetYears");
  }

  getObjectives(compID, year, lang) {
    return this.http.get<any>(
      Config.WebApiUrl + "StratigicObjectives/SearchAllStratigicObjectives",
      {
        params: {
          companyId: compID,
          year: year,
          languageCode: lang,
        },
      }
    );
  }

  RemoveEvident(docID, username) {
    return this.http.get<any>(
      Config.WebApiUrl +
      "StratigicObjectives/RemoveEvident?docID=" +
      docID +
      "&username=" +
      username
    );
  }

  uploadFiles(file, docID, projectID, username) {
    let form = new FormData();

    let apiUrl =
      "StratigicObjectives/UploadFiles?docID=" +
      docID +
      "&projectID=" +
      projectID +
      "&username=" +
      username;

    if (file != null && file != undefined) {
      form.append(file.files[0].name, file.files[0], file.files[0].name);
    }

    return this.http.post<any>(Config.WebApiUrl + apiUrl, form);
  }
}
