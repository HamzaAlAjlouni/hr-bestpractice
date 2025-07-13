import {Injectable} from '@angular/core';
import {Config} from '../Config';
import {HttpClient} from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class OrganizationService {

  constructor(private http: HttpClient) {
  }

  // #region Skills Services
  SaveStratigicObjective(name, companyId, weight, year, description, createdBy, lang, bsc) {
    return this.http.get<any>(Config.WebApiUrl + "StratigicObjectives/SaveStratigicObjective", {
      params:
        {
          name: name,
          companyId: companyId,
          weight: weight,
          year: year,
          description: description,
          createdBy: createdBy,
          LanguageCode: lang,
          bsc: bsc
        }
    });
  }

  // -1 as default
  SearchAllStratigicObjectives(companyId, year, lang, bsc = '') {
    return this.http.get<any>(Config.WebApiUrl + "StratigicObjectives/SearchAllStratigicObjectives", {
      params:
        {
          companyId: companyId,
          year: year,
          LanguageCode: lang,
          BSC: bsc
        }
    });
  }

  getObjectivesRemainingWeight(companyId, year) {
    return this.http.get<any>(Config.WebApiUrl + "StratigicObjectives/getObjectivesRemainingWeight", {
      params:
        {
          company: companyId,
          year: year,
        }
    });
  }

  getObjectiveKPIRemainingWeight(objID, isObjKpi) {
    return this.http.get<any>(Config.WebApiUrl + "StratigicObjectives/getObjectiveKPIRemainingWeight", {
      params:
        {
          objID: objID,
          isObjKpi: isObjKpi
        }
    });
  }

  GetStratigicObjectivebyID(id, lang) {
    return this.http.get<any>(Config.WebApiUrl + "StratigicObjectives/getStratigicObjectivebyID", {
      params:
        {
          id: id,
          LanguageCode: lang
        }
    });
  }


  DeleteStratigicObjectiveByID(id) {
    return this.http.get<any>(Config.WebApiUrl + "StratigicObjectives/DeleteStratigicObjectiveByID", {
      params:
        {
          id: id
        }
    });
  }

  UpdateStratigicObjective(id, name, companyId, weight, year, description, modifiedBy, lang, bsc) {
    return this.http.get<any>(Config.WebApiUrl + "StratigicObjectives/UpdateStratigicObjective", {
      params:
        {
          id: id,
          name: name,
          companyId: companyId,
          weight: weight,
          year: year,
          description: description,
          modifiedBy: modifiedBy,
          LanguageCode: lang,
          bsc: bsc
        }
    });
  }

  // #endregion


  // #endregion


  getObjectiveKpiByObjective(id, lang, bsc = '') {
    return this.http.get<any>(Config.WebApiUrl + "StratigicObjectives/getObjectiveKpiByObjective", {
      params:
        {
          objective_id: id,
          languageCode: lang,
          BSC: bsc
        }
    });
  }

  getObjectiveCollectionSummary(companyId, lang, year, bsc) {
    return this.http.get<any>(Config.WebApiUrl + "StratigicObjectives/getObjectiveCollectionSummary", {
      params:
        {
          companyId,
          year,
          languageCode: lang,
          BSC: bsc
        }
    });
  }

  getObjectiveKpiByID(id, lang) {
    return this.http.get<any>(Config.WebApiUrl + "StratigicObjectives/getObjectiveKpiByID", {
      params:
        {
          ID: id,
          languageCode: lang
        }
    });
  }

  GetEvidanceByObjectiveKPI_id(id) {
    return this.http.get<any>(Config.WebApiUrl + `StratigicObjectives/GetEvidanceByObjectiveKPI_id/${id}`, {
      params:
        {
          id: id,
          // languageCode: lang
        }
    });
  }

  addEvidanceForKPI(selectedEvidanceId, txtEvidanceKPI, username) {
    return this.http.get<any>(Config.WebApiUrl + 'StratigicObjectives/SaveEvidanceForObjectiveKPI', {
      params:
        {
          ObjectiveKPI_id: selectedEvidanceId,
          documentName: txtEvidanceKPI,
          username: username
        }
    });
  }

  deleteEvidanceForKPIById(id) {
    return this.http.get<any>(`${Config.WebApiUrl}StratigicObjectives/DeleteEvidanceForObjectiveKPI`, {
      params:
        {
          Evidance_id: id
        }
    })
  }

  updateEvidanceForKPIById(EvidanceName, id, UpdatedBy) {
    return this.http.get<any>(`${Config.WebApiUrl}StratigicObjectives/UpdateEvidance`, {
      params:
        {
          EvidanceName: EvidanceName,
          Evidance_id: id,
          UpdatedBy: UpdatedBy
        }
    })
  }

  DeleteObjectiveKpi(id) {
    return this.http.get<any>(Config.WebApiUrl + "StratigicObjectives/DeleteObjectiveKpi", {
      params:
        {
          id: id
        }
    });
  }


  SaveObjectiveKpi(
    name,
    name2,
    description,
    objective_id,
    weight,
    target,
    bsc,
    measurement,
    company_id,
    branch_id,
    username,
    LanguageCode,
    isObjKpi,
    ReviewCycle,
    kpi_type,
    resultUint,
    betterUpDown = 1) {
    return this.http.get<any>(Config.WebApiUrl + "StratigicObjectives/SaveObjectiveKpi", {
      params:
        {
          name: name,
          name2: name2,
          description: description,
          objective_id: objective_id,
          weight: weight,
          target: target,
          bsc: bsc,
          measurement: measurement,
          company_id: company_id,
          branch_id: branch_id,
          username: username,
          LanguageCode: LanguageCode,
          isObjKpi: isObjKpi,
          ReviewCycle: ReviewCycle,
          kpi_type: kpi_type,
          resultUint: resultUint,
          betterUpDown: betterUpDown + ""

        }
    });
  }

  CalculateTargetBasedonKPI_Type(kpiTypeId, Q1_Target, Q2_Target, Q3_Target, Q4_Target) {
    console.log(kpiTypeId, Q1_Target, Q2_Target, Q3_Target, Q4_Target)
    return this.http.get<any>(Config.WebApiUrl + 'StratigicObjectives/CalculateTargetBasedonKPI_Type', {
      params:
        {
          KPITypeId: kpiTypeId,
          Q1_Target: Q1_Target,
          Q2_Target: Q2_Target,
          Q3_Target: Q3_Target,
          Q4_Target: Q4_Target
        }
    });
  }

  UpdateObjectiveKpi(
    obj_kpi_id,
    name,
    name2,
    description,
    objective_id,
    weight,
    target,
    bsc,
    measurement,
    company_id,
    branch_id,
    username,
    LanguageCode,
    ReviewCycle,
    kpi_type,
    resultUint,
    betterUpDown = 1
  ) {
    return this.http.get<any>(Config.WebApiUrl + "StratigicObjectives/UpdateObjectiveKpi", {
      params:
        {
          obj_kpi_id: obj_kpi_id,
          name: name,
          name2: name2,
          description: description,
          objective_id: objective_id,
          weight: weight,
          target: target,
          bsc: bsc,
          measurement: measurement,
          company_id: company_id,
          branch_id: branch_id,
          username: username,
          LanguageCode: LanguageCode,
          ReviewCycle: ReviewCycle,
          kpi_type: kpi_type,
          resultUint: resultUint,
          betterUpDown: betterUpDown + ""
        }
    });
  }


  saveObjectivesKPIsAssessment(Data) {
    let form = new FormData();
    form.append("kpiData", JSON.stringify(Data));

    return this.http.post<any>(Config.WebApiUrl + "StratigicObjectives/saveObjectivesKPIsAssessment", form);
  }

  getObjectivesKPIRemainingWeight(objectiveId, isObjKpi) {
    return this.http.get<any>(Config.WebApiUrl + "StratigicObjectives/getObjectivesKPIRemainingWeight", {
      params:
        {
          objective_id: objectiveId,
          isObjKpi: isObjKpi
        }
    });
  }


  SavePlannedKPIs(
    CreatedBy,
    ProjectID,
    Q1_Target,
    Q2_Target,
    Q3_Target,
    Q4_Target,
    KPI_ID) {
    return this.http.get<any>(Config.WebApiUrl + "StratigicObjectives/SavePlannedKPIs", {
      params:
        {
          CreatedBy: CreatedBy,
          ProjectID: ProjectID,
          Q1_Target: Q1_Target,
          Q2_Target: Q2_Target,
          Q3_Target: Q3_Target,
          Q4_Target: Q4_Target,
          KPI_ID: KPI_ID
        }
    });
  }

  UpdatePlannedKPIs(
    CreatedBy,
    ProjectID,
    Q1_Target,
    Q2_Target,
    Q3_Target,
    Q4_Target,
    KPI_ID) {
    return this.http.get<any>(Config.WebApiUrl + "StratigicObjectives/UpdatePlannedKPIs", {
      params:
        {
          CreatedBy: CreatedBy,
          ProjectID: ProjectID,
          Q1_Target: Q1_Target,
          Q2_Target: Q2_Target,
          Q3_Target: Q3_Target,
          Q4_Target: Q4_Target,
          KPI_ID: KPI_ID
        }
    });
  }

  //#region Approvals
  GetApprovalSetupByURL(page_url, companyID) {
    return this.http.get<any>(Config.WebApiUrl + "ApprovalSetup/GetApprovalSetupByURL", {
      params:
        {
          page_url: page_url,
          companyID: companyID
        }
    });
  }

  GetProjectStatus(projectID) {
    return this.http.get<any>(Config.WebApiUrl + "ApprovalSetup/GetProjectStatus", {
      params:
        {
          projectID: projectID
        }
    });
  }

  UpdateProjectStatus(projectID, status, createdBy = "", note = "") {
    return this.http.get<any>(Config.WebApiUrl + "ApprovalSetup/UpdateProjectStatus", {
      params:
        {
          projectID: projectID,
          status: status,
          note: note,
          createdBy: createdBy,

        }
    });
  }


  GetActionPlanStatus(planID) {
    return this.http.get<any>(Config.WebApiUrl + "ApprovalSetup/GetActionPlanStatus", {
      params:
        {
          planID: planID
        }
    });
  }

  UpdateActionStatus(PlanID, status) {
    return this.http.get<any>(Config.WebApiUrl + "ApprovalSetup/UpdateActionStatus", {
      params:
        {
          PlanID: PlanID,
          status: status
        }
    });
  }

  UpdateActionAssessmentStatus(PlanID, status) {
    return this.http.get<any>(Config.WebApiUrl + "ApprovalSetup/UpdateActionAssessmentStatus", {
      params:
        {
          PlanID: PlanID,
          status: status
        }
    });
  }


  UpdateProjectAssessmentStatus(projectID, status, createdBy = "", note = "") {
    return this.http.get<any>(Config.WebApiUrl + "ApprovalSetup/UpdateProjectAssessmentStatus", {
      params:
        {
          projectID: projectID,
          status: status,
          note: note,
          createdBy: createdBy,
        }
    });
  }


  GetAllApprovalSetup(companyID, pagename) {
    return this.http.get<any>(Config.WebApiUrl + "ApprovalSetup/GetAllApprovalSetup", {
      params:
        {
          pagename: pagename,
          companyID: companyID
        }
    });
  }

  GetApprovalSetupByID(id) {
    return this.http.get<any>(Config.WebApiUrl + "ApprovalSetup/GetApprovalSetupByID", {
      params:
        {
          id: id
        }
    });
  }

  SaveApproval(name, url, user, companyID) {
    return this.http.get<any>(Config.WebApiUrl + "ApprovalSetup/SaveApproval", {
      params:
        {
          name: name,
          url: url,
          user: user,
          companyID: companyID
        }
    });
  }

  UpdateApproval(id, name, url, user, companyID) {
    return this.http.get<any>(Config.WebApiUrl + "ApprovalSetup/UpdateApproval", {
      params:
        {
          id: id,
          name: name,
          url: url,
          user: user,
          companyID: companyID
        }
    });
  }


  DeleteApproval(id) {
    return this.http.get<any>(Config.WebApiUrl + "ApprovalSetup/DeleteApproval", {
      params:
        {
          id: id
        }
    });
  }


  //#endregion


}
