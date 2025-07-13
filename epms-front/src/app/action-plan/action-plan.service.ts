import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Config } from 'src/app/Config';

@Injectable({
  providedIn: 'root'
})
export class ActionPlanService {

  constructor(private http: HttpClient) { }

  GetProjectActionPlans(projectId, projectKpiID, languageCode,empID, UnitId) {
    return this.http.get<any>(Config.WebApiUrl +
      'ActionPlans/GetProjectActionPlans', {
      params:
      {
        projectId: projectId,
        projectKpiID: projectKpiID,
        empID:empID,
        languageCode: languageCode,
        UnitId: UnitId
      }
    });
  }

  GetProjectActionPlansByID(planID) {
    return this.http.get<any>(Config.WebApiUrl +
      'ActionPlans/GetProjectActionPlansByID', {
      params:
      {
       ID:planID
      }
    });
  }

  DeleteActionPlan(planID) {
    return this.http.get<any>(Config.WebApiUrl +
      'ActionPlans/DeleteActionPlan', {
      params:
      {
       ID:planID
      }
    });
  }

  SaveProjectActionPlans(
    projectId,
    action_cost,
    username,
    action_date,
    action_name,
    action_notes,
    action_req,
    action_weight,
    emp_id,
    project_kpi_id) {
    return this.http.get<any>(Config.WebApiUrl +
      'ActionPlans/SaveProjectActionPlans', {
      params:
        {
          projectId: projectId,
          action_cost: action_cost,
          username: username,
          action_date: action_date,
          action_name: action_name,
          action_notes: action_notes,
          action_req: action_req,
          action_weight: action_weight,
          emp_id: emp_id,
          project_kpi_id: project_kpi_id
        }
    });
  }


  UpdateProjectActionPlans(
    ID,
    projectId,
    action_cost,
    username,
    action_date,
    action_name,
    action_notes,
    action_req,
    action_weight,
    emp_id,
    project_kpi_id) {
    return this.http.get<any>(Config.WebApiUrl +
      'ActionPlans/UpdateProjectActionPlans', {
      params:
        {
          ID:ID,
          projectId: projectId,
          action_cost: action_cost,
          username: username,
          action_date: action_date,
          action_name: action_name,
          action_notes: action_notes,
          action_req: action_req,
          action_weight: action_weight,
          emp_id: emp_id,
          project_kpi_id: project_kpi_id
        }
    });
  }


GetProjectPlannedCost(projectID,planID) {
    return this.http.get<any>(Config.WebApiUrl +
      'ActionPlans/GetProjectPlannedCost', {
      params:
      {
       id:projectID,
       planID:planID
      }
    });
  }

  getActionPlanRemainingWeight(projectID, projKPI) {
    return this.http.get<any>(Config.WebApiUrl +
      'ActionPlans/getActionPlanRemainingWeight', {
      params:
      {
        projectID: projectID,
        projKPI: projKPI
      }
    });
  }


  GetProjectActionPlansForAssessment(projectId,projectKPI,languageCode) {
    return this.http.get<any>(Config.WebApiUrl +
        'ActionPlans/GetProjectActionPlansForAssessment', {
      params:
      {
        projectId: projectId,
        projKPI: projectKPI,
        languageCode: languageCode
      }
    });
  }

  saveActionPlanAssessment(Data){
    let form = new FormData();
     form.append("kpiData",JSON.stringify(Data));

    return this.http.post<any>(Config.WebApiUrl+"ActionPlans/saveActionPlanAssessment",form);
  }

  uploadFiles(file, docID, planID, username) {
    let form = new FormData();
    let apiUrl = "ActionPlans/UploadFiles?docID=" + docID + "&planID=" + planID + "&username=" + username;
    if (file != null && file != undefined) {
      form.append(file.files[0].name, file.files[0], file.files[0].name);
    }
    return this.http.post<any>(Config.WebApiUrl + apiUrl, form);
  }

  RemoveEvident(docID,username){
    return this.http.get<any>(Config.WebApiUrl + "ActionPlans/RemoveEvident?docID=" + docID + "&username="+username);
  }


}
