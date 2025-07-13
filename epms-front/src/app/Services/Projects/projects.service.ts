import {Injectable} from "@angular/core";
import {Config} from "src/app/Config";
import {Observable} from "rxjs";
import {HttpClient} from "@angular/common/http";

@Injectable({
  providedIn: "root",
})
/* tslint:disable */
export class ProjectsService {
  constructor(private http: HttpClient) {
  }

  Save(oProject: any, Weight, lang, year, category): Observable<any> {
    return this.http.get(Config.WebApiUrl + "StratigicObjectives/SaveProject", {
      params: {
        Name: oProject.Name,
        Name2: oProject.Name2,
        Code: oProject.Code,
        Order: oProject.Order,
        BranchId: oProject.BranchId,
        KPICycleId: oProject.KPICycleId,
        KPITypeId: oProject.KPITypeId,
        UnitId: oProject.UnitId,
        ResultUnitId: oProject.ResultUnitId,
        KPI: oProject.KPI,
        CreatedBy: oProject.CreatedBy,
        Description: oProject.Description,
        StratigicObjectiveId: oProject.StratigicObjectiveId,
        Target: oProject.Target,
        Weight: Weight,
        CompanyId: oProject.CompanyId,
        Q1_Target: oProject.Q1_Target,
        Q2_Target: oProject.Q2_Target,
        Q3_Target: oProject.Q3_Target,
        Q4_Target: oProject.Q4_Target,
        languageCode: lang,
        PlannedCost: oProject.PlannedCost,
        p_type: oProject.p_type,
        Year: year,
        evaluations:oProject.evaluations,
        Category: category,
      },
    });
  }

    GetUnits(CompanyID: number, lang): Observable<any> {
    return this.http.get(
      Config.WebApiUrl +
      "Unites/GetUnits?CompanyID=" +
      CompanyID +
      "&LanguageCode=" +
      lang
    );
  }

  GetCodes(MajorNo: number, CompanyID: number): Observable<any> {
    return this.http.get(
      Config.WebApiUrl +
      "Codes/GetCodes?MajorNo=" +
      MajorNo +
      "&CompanyID=" +
      CompanyID
    );
  }

  GetBranches(CompanyID: number): Observable<any> {
    return this.http.get(
      Config.WebApiUrl + "Branches/GetBranches?CompanyID=" + CompanyID
    );
  }

  GetYears(): Observable<any> {
    return this.http.get(Config.WebApiUrl + "Years/GetYears");
  }

  GetStratigicObjectives(
    CompanyID: number,
    year: number,
    lang: string
  ): Observable<any> {
    return this.http.get(
      Config.WebApiUrl +
      "StratigicObjectives/SearchAllStratigicObjectives?companyId=" +
      CompanyID +
      "&year=" +
      year +
      "&languageCode=" +
      lang
    );
  }

  GetPlannedWeightForAll(
    CompanyID: number,
    year: number,
    lang: string,
    bsc,
    projectId
  ): Observable<any> {
    return this.http.get(
      Config.WebApiUrl +
      "StratigicObjectives/GetPlannedWeightForAll?companyId=" +
      CompanyID +
      "&year=" +
      year +
      "&languageCode=" +
      lang +
      "&bsc=" +
      bsc +
      "&projectId=" +
      projectId
    );
  }

  GetProjects(
    CompanyID,
    branchId,
    yearId,
    unitId,
    stratigicObjectiveId,
    lang
  ): Observable<any> {
    return this.http.get(
      Config.WebApiUrl + "StratigicObjectives/SearchAllProjects",
      {
        params: {
          companyId: CompanyID,
          branchId: branchId,
          yearId: yearId,
          unitId: unitId,
          stratigicObjectiveId: stratigicObjectiveId,
          languageCode: lang,
        },
      }
    );
  }

  GetProjectsWithKPIs(
    CompanyID,
    branchId,
    yearId,
    unitId,
    stratigicObjectiveId,
    lang,
    category = 0
  ): Observable<any> {
    return this.http.get(
      Config.WebApiUrl + "StratigicObjectives/SearchAllProjectsAndKPIs",
      {
        params: {
          companyId: CompanyID,
          branchId: branchId,
          yearId: yearId,
          unitId: unitId,
          stratigicObjectiveId: stratigicObjectiveId,
          languageCode: lang,
          category: category + ""
        },
      }
    );
  }

  getProjectsCategories(
  ): Observable<any> {
    return this.http.get(
      Config.WebApiUrl + "StratigicObjectives/GetProjectsCategories"
    );
  }

  SearchAllProjectsList(
    CompanyID,
    branchId,
    yearId,
    unitId,
    stratigicObjectiveId,
    lang
  ): Observable<any> {
    return this.http.get(
      Config.WebApiUrl + "StratigicObjectives/SearchAllProjectsList",
      {
        params: {
          companyId: CompanyID,
          branchId: branchId,
          yearId: yearId,
          unitId: unitId,
          stratigicObjectiveId: stratigicObjectiveId,
          languageCode: lang,
        },
      }
    );
  }

  GetProjectById(id, lang): Observable<any> {
    return this.http.get(
      Config.WebApiUrl +
      "StratigicObjectives/getProjectbyID?id=" +
      id +
      "&languageCode=" +
      lang
    );
  }

  Update(oProject: any, Weight, lang, year, category): Observable<any> {
    return this.http.get(
      Config.WebApiUrl + "StratigicObjectives/UpdateProject",
      {
        params: {
          id: oProject.id,
          Name: oProject.Name,
          Code: oProject.Code,
          Order: oProject.Order,
          BranchId: oProject.BranchId,
          KPICycleId: oProject.KPICycleId,
          KPITypeId: oProject.KPITypeId,
          UnitId: oProject.UnitId,
          ResultUnitId: oProject.ResultUnitId,
          KPI: oProject.KPI,
          CreatedBy: oProject.CreatedBy,
          Description: oProject.Description,
          StratigicObjectiveId: oProject.StratigicObjectiveId,
          Target: oProject.Target,
          Weight: Weight,
          CompanyId: oProject.CompanyId,
          languageCode: lang,
          PlannedCost: oProject.PlannedCost,
          Q1_Target: oProject.Q1_Target,
          Q2_Target: oProject.Q2_Target,
          Q3_Target: oProject.Q3_Target,
          Q4_Target: oProject.Q4_Target,
          p_type: oProject.p_type,
          Year: year,
          Category: category
        },
      }
    );
  }

  Delete(id): Observable<any> {
    return this.http.get(
      Config.WebApiUrl + "StratigicObjectives/DeleteProjectByID?id=" + id
    );
  }

  GetProjectsByEmployeeID(EmployeeID, lang): Observable<any> {
    return this.http.get(
      Config.WebApiUrl + "StratigicObjectives/getProjectbyEmployeeID",
      {
        params: {
          EmployeeID: EmployeeID,
          languageCode: lang,
        },
      }
    );
  }
  GetProjectsByUnitID(unitId, lang): Observable<any> {
    return this.http.get(
      Config.WebApiUrl + "StratigicObjectives/getProjectbyUnit",
      {
        params: {
          unitId: unitId,
          languageCode: lang,
        },
      }
    );
  }

  getProjectsRemainingWeight(objectiveId) {
    return this.http.get<any>(
      Config.WebApiUrl + "StratigicObjectives/getProjectsRemainingWeight",
      {
        params: {
          kpiId: objectiveId,
        },
      }
    );
  }

  GetProjectDocuments(id, lang, isObj = "0"): Observable<any> {
    return this.http.get(
      Config.WebApiUrl +
      "StratigicObjectives/GetProjectDocuments?id=" +
      id +
      "&languageCode=" +
      lang +
      "&IsObjective=" +
      isObj
    );
  }

  GetProjectDocumentByID(id): Observable<any> {
    return this.http.get(
      Config.WebApiUrl + "StratigicObjectives/GetProjectDocumentByID?id=" + id
    );
  }

  SaveProjectDocuments(
    projectID,
    DocumentName,
    username,
    isObj = "0"
  ): Observable<any> {
    return this.http.get(
      Config.WebApiUrl + "StratigicObjectives/SaveProjectDocuments",
      {
        params: {
          projectId: projectID,
          DocumentName: DocumentName,
          username: username,
          IsObjective: isObj,
        },
      }
    );
  }

  UpdateProjectDocuments(
    id,
    projectID,
    DocumentName,
    username,
    isObj = "0"
  ): Observable<any> {
    return this.http.get(
      Config.WebApiUrl + "StratigicObjectives/UpdateProjectDocuments",
      {
        params: {
          id: id,
          projectId: projectID,
          DocumentName: DocumentName,
          username: username,
          IsObjective: isObj,
        },
      }
    );
  }

  DeleteProjectDocuments(id): Observable<any> {
    return this.http.get(
      Config.WebApiUrl + "StratigicObjectives/DeleteProjectDocuments",
      {
        params: {
          id: id,
        },
      }
    );
  }

  GetProjectActionPlans(id, lang): Observable<any> {
    return this.http.get(
      Config.WebApiUrl +
      "StratigicObjectives/GetProjectActionPlans?id=" +
      id +
      "&languageCode=" +
      lang
    );
  }

  GetFinalResultDescription(value): Observable<any> {
    return this.http.get(
      `${Config.WebApiUrl}/EmployeeAssesment/GetFinalResult`, {
        params: {
          num: value
        }
      }
    );
  }

  SaveProjectActionPlans(
    projectID,
    DocumentName,
    username,
    description
  ): Observable<any> {
    return this.http.get(
      Config.WebApiUrl + "StratigicObjectives/SaveProjectActionPlans",
      {
        params: {
          projectId: projectID,
          DocumentName: DocumentName,
          username: username,
          description: description,
        },
      }
    );
  }
}
