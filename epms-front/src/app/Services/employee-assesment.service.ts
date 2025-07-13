import {Injectable} from "@angular/core";
import {Config} from "../Config";
import {HttpClient} from "../../../node_modules/@angular/common/http";
import {map} from "rxjs/operators";
import {Observable} from "rxjs";

@Injectable({
  providedIn: "root",
})
export class EmployeeAssesmentService {
  constructor(private http: HttpClient) {
  }

  getYears() {
    return this.http.get<any>(Config.WebApiUrl + "Years/GetYears");
  }

  /********************/
  LoadAssesment(EmployeeID, YearID, unitID) {
    return this.http.get<any>(
      Config.WebApiUrl + "EmployeeAssesment/GetEmployeeAssesment",
      {
        params: {
          EmployeeID,
          YearID,
          unitID
        },
      }
    );
  }

  LoadEmployeeProjectCompetency(EmployeeID, YearID, unitID, projectId = -1, competencyId = -1, objectiveId = -1) {
    return this.http.get<any>(
      Config.WebApiUrl + "EmployeeAssesment/GetEmployeeProjectCompetency",
      {
        params: {
          EmployeeID,
          YearID,
          unitID,
          projectId: projectId + "",
          competencyId: competencyId + "",
          objectiveId: objectiveId + ""
        },
      }
    );
  }

  ChangeAssessmentStatus(ID, status) {
    return this.http.get<any>(
      Config.WebApiUrl + "EmployeeAssesment/UpdateEmployeeAssessmentStatus",
      {
        params: {
          ID,
          ModifiedBy: "",
          status
        },
      }
    );
  }

  LoadAssesmentByID(id) {
    return this.http.get<any>(Config.WebApiUrl + "EmployeeAssesment/getByID", {
      params: {
        ID: id,
      },
    });
  }

  SaveAssesment(
    AgreementDate,
    Attachment,
    CreatedBy,
    KpiCycle,
    YearID,
    EmpPositionID,
    EmployeeID,
    Target,
    IsQuarter1,
    IsQuarter2,
    IsQuarter3,
    IsQuarter4,
    emp_manager_id,
    companyId
  ) {
    return this.http.get<any>(Config.WebApiUrl + "EmployeeAssesment/Save", {
      params: {
        AgreementDate: AgreementDate,
        Attachment: Attachment,
        CreatedBy: CreatedBy,
        KpiCycle: KpiCycle,
        YearID: YearID,
        EmpPositionID: EmpPositionID,
        EmployeeID: EmployeeID,
        Target: Target,
        isQuarter1: IsQuarter1 == null ? false : IsQuarter1,
        isQuarter2: IsQuarter2 == null ? false : IsQuarter2,
        isQuarter3: IsQuarter3 == null ? false : IsQuarter3,
        isQuarter4: IsQuarter4 == null ? false : IsQuarter4,
        emp_manager_id: emp_manager_id,
        companyId: companyId,
      },
    });
  }

  UpdateAssesment(
    ID,
    AgreementDate,
    Attachment,
    ModifiedBy,
    KpiCycle,
    YearID,
    EmpPositionID,
    EmployeeID,
    Target,
    IsQuarter1,
    IsQuarter2,
    IsQuarter3,
    IsQuarter4,
    emp_manager_id,
    companyId
  ) {
    return this.http.get<any>(Config.WebApiUrl + "EmployeeAssesment/Update", {
      params: {
        ID: ID,
        AgreementDate: AgreementDate,
        Attachment: Attachment,
        ModifiedBy: ModifiedBy,
        KpiCycle: KpiCycle,
        YearID: YearID,
        EmpPositionID: EmpPositionID,
        EmployeeID: EmployeeID,
        Target: Target,
        isQuarter1: IsQuarter1,
        isQuarter2: IsQuarter2,
        isQuarter3: IsQuarter3,
        isQuarter4: IsQuarter4,
        emp_manager_id: emp_manager_id,
        companyId: companyId,
      },
    });
  }

  DeleteAssesment(id) {
    return this.http.get<any>(Config.WebApiUrl + "EmployeeAssesment/Delete", {
      params: {
        ID: id,
      },
    });
  }

  /********************/
  LoadEmployeeCompetency(employeeAssessmentId, lang) {
    return this.http.get<any>(
      Config.WebApiUrl + "EmployeeCompetency/GetEmployeeCompetencies",
      {
        params: {
          employeeAssessmentId: employeeAssessmentId,
          languageCode: lang,
        },
      }
    );
  }

  GetEmployeeCompetenciesByEmployee(
    employeeId,
    companyId,
    yearId,
    LanguageCode
  ) {
    return this.http.get<any>(
      Config.WebApiUrl + "EmployeeCompetency/GetEmployeeCompetenciesByEmployee",
      {
        params: {
          employeeId: employeeId,
          companyId: companyId,
          yearId: yearId,
          LanguageCode: LanguageCode,
        },
      }
    );
  }

  LoadEmployeeCompetencyByID(id): Observable<any> {
    return this.http
      .get<any>(Config.WebApiUrl + "EmployeeCompetency/getByID", {
        params: {
          ID: id,
        },
      })
      .pipe(
        map((res) => {
          return res;
        })
      );
  }

  SaveEmployeeCompetency(
    competencyId,
    competencyLevelId,
    weight,
    resultWithoutRound,
    resultAfterRound,
    employeeAssessmentId,
    createdBy,
    kPICycle,
    CompetenceLevelID
  ) {
    return this.http.get<any>(Config.WebApiUrl + "EmployeeCompetency/Save", {
      params: {
        competencyId: competencyId,
        competencyLevelId: competencyLevelId,
        weight: weight,
        resultWithoutRound: resultWithoutRound,
        resultAfterRound: resultAfterRound,
        employeeAssessmentId: employeeAssessmentId,
        createdBy: createdBy,
        kPICycle: kPICycle,
        competenceLevelId: CompetenceLevelID,
      },
    });
  }

  UpdateEmployeeCompetency(
    id,
    competencyId,
    competencyLevelId,
    weight,
    resultWithoutRound,
    resultAfterRound,
    employeeAssessmentId,
    modifiedBy
  ) {
    return this.http.get<any>(Config.WebApiUrl + "EmployeeCompetency/Update", {
      params: {
        id: id,
        competencyId: competencyId,
        competencyLevelId: competencyLevelId,
        weight: weight,
        resultWithoutRound: resultWithoutRound,
        resultAfterRound: resultAfterRound,
        employeeAssessmentId: employeeAssessmentId,
        modifiedBy: modifiedBy,
      },
    });
  }

  DeleteEmployeeCompetency(id) {
    return this.http.get<any>(Config.WebApiUrl + "EmployeeCompetency/Delete", {
      params: {
        ID: id,
      },
    });
  }

  /********************/
  LoadEmployeeCompetencyKPI(employeeCompetencyId) {
    return this.http.get<any>(
      Config.WebApiUrl + "EmployeeCompetencyKpi/GetEmployeeCompetencyKpis",
      {
        params: {
          employeeCompetencyId: employeeCompetencyId,
        },
      }
    );
  }

  LoadEmployeeCompetencyKPIByID(id) {
    return this.http.get<any>(
      Config.WebApiUrl + "EmployeeCompetencyKpi/getByID",
      {
        params: {
          id: id,
        },
      }
    );
  }

  SaveEmployeeCompetencyKPI(
    employeeCompetencyId,
    employeeCompetencyKpiId,
    createdBy,
    kPICycle,
    Target
  ) {
    return this.http.get<any>(Config.WebApiUrl + "EmployeeCompetencyKpi/Save", {
      params: {
        employeeCompetencyId: employeeCompetencyId,
        employeeCompetencyKpiId: employeeCompetencyKpiId,
        createdBy: createdBy,
        kPICycle: kPICycle,
        Target: Target,
      },
    });
  }

  UpdateEmployeeCompetencyKPI(
    id,
    employeeCompetencyId,
    employeeCompetencyKpiId,
    CompetenceKPITarget
  ) {
    return this.http.get<any>(
      Config.WebApiUrl + "EmployeeCompetencyKpi/Update",
      {
        params: {
          id: id,
          employeeCompetencyId: employeeCompetencyId,
          employeeCompetencyKpiId: employeeCompetencyKpiId,
          Target: CompetenceKPITarget,
        },
      }
    );
  }

  DeleteEmployeeCompetencyKPI(id) {
    return this.http.get<any>(
      Config.WebApiUrl + "EmployeeCompetencyKpi/Delete",
      {
        params: {
          id: id,
        },
      }
    );
  }

  /********************/
  LoadEmployeeObjective(EmployeeAssesmentID, lang, q1Filter = 1, q2Filter = 1, q3Filter = 1, q4Filter = 1) {
    return this.http.get<any>(
      Config.WebApiUrl + "EmployeeObjective/GetEmployeeObjective",
      {
        params: {
          EmployeeAssesmentID: EmployeeAssesmentID,
          LanguageCode: lang,
          q1Filter: q1Filter + "",
          q2Filter: q2Filter + "",
          q3Filter: q3Filter + "",
          q4Filter: q4Filter + ""
        },
      }
    );
  }

  GetEmployeeObjectiveByEmployee(employeeId, companyId, yearId, LanguageCode) {
    return this.http.get<any>(
      Config.WebApiUrl + "EmployeeObjective/GetEmployeeObjectiveByEmployee",
      {
        params: {
          employeeId: employeeId,
          companyId: companyId,
          yearId: yearId,
          LanguageCode: LanguageCode,
        },
      }
    );
  }

  GetEmployeeObjectives(employeeId, unitId, yearId, LanguageCode) {
    return this.http.get<any>(
      Config.WebApiUrl + "EmployeeObjective/GetEmployeeObjectives",
      {
        params: {
          yearId,
          LanguageCode,
          employeeId,
          unitId

        },
      }
    );
  }

  LoadEmployeeObjectiveByID(id, lang) {
    return this.http.get<any>(Config.WebApiUrl + "EmployeeObjective/getByID", {
      params: {
        ID: id,
        LanguageCode: lang,
      },
    });
  }

  SaveEmployeeObjective(
    Code,
    EmployeeAssesmentID,
    CreatedBy,
    Name,
    Note,
    Weight,
    ProjectID,
    PositionDescID,
    kPICycle1,
    // Target,
    // Q1_Target,
    // Q2_Target,
    // Q3_Target,
    // Q4_Target,
    lang,
    ObjectiveCompetencyID = "0"

    // target_type
  ) {
    return this.http.get<any>(Config.WebApiUrl + "EmployeeObjective/Save", {
      params: {
        Code: Code,
        EmployeeAssesmentID: EmployeeAssesmentID,
        CreatedBy: CreatedBy,
        Name: Name,
        Note: Note,
        Weight: Weight,
        ProjectID: ProjectID,
        PositionDescID: PositionDescID,
        kPICycle: kPICycle1,
        // Target : Target,
        // Target1:Q1_Target,
        // Target2:Q2_Target,
        // Target3:Q3_Target,
        // Target4:Q4_Target,
        LanguageCode: lang,
        ObjectiveCompetencyID
        // target_type:target_type
      },
    });
  }

  ImportActionPlansToEmployeeObjectives(EmployeeAssesmentID, companyId) {
    return this.http.get<any>(
      Config.WebApiUrl +
      "EmployeeObjective/ImportActionPlansToEmployeeObjectives",
      {
        params: {
          EmployeeAssesmentID: EmployeeAssesmentID,
          companyId: companyId,
        },
      }
    );
  }

  UpdateEmployeeObjective(
    ID,
    Code,
    ModifiedBy,
    Name,
    Note,
    Weight,
    ProjectID,
    PositionDescID,
    lang,
    ObjectiveCompetencyID = "0"
  ) {
    return this.http.get<any>(Config.WebApiUrl + "EmployeeObjective/Update", {
      params: {
        ID: ID,
        Code: Code,
        ModifiedBy: ModifiedBy,
        Name: Name,
        Note: Note,
        Weight: Weight,
        ProjectID: ProjectID,
        PositionDescID: PositionDescID,
        LanguageCode: lang,
        ObjectiveCompetencyID
      },
    });
  }

  DeleteEmployeeObjective(id) {
    return this.http.get<any>(Config.WebApiUrl + "EmployeeObjective/Delete", {
      params: {
        ID: id,
      },
    });
  }

  /********************/
  LoadEmployeeObjectiveKPI(EmployeeObjectiveID) {
    return this.http.get<any>(
      Config.WebApiUrl + "EmployeeObjectiveKPI/GetEmployeeObjectiveKPI",
      {
        params: {
          EmployeeObjectiveID: EmployeeObjectiveID,
        },
      }
    );
  }

  LoadEmployeeObjectiveKPIByID(id) {
    return this.http.get<any>(
      Config.WebApiUrl + "EmployeeObjectiveKPI/getByID",
      {
        params: {
          ID: id,
        },
      }
    );
  }

  SaveEmployeeObjectiveKPI(
    EmployeeObjectiveID,
    CreatedBy,
    Name,
    Target,
    kPICycle,
    objKPI_Q1_Target,
    objKPI_Q2_Target,
    objKPI_Q3_Target,
    objKPI_Q4_Target,
    Weight,
    target_type,
    KPITypeId,
    lang,
    betterUpDown = 1
  ) {
    return this.http.get<any>(Config.WebApiUrl + "EmployeeObjectiveKPI/Save", {
      params: {
        EmployeeObjectiveID: EmployeeObjectiveID,
        CreatedBy: CreatedBy,
        Name: Name,
        Target: Target,
        kPICycle: kPICycle,
        Target1: objKPI_Q1_Target,
        Target2: objKPI_Q2_Target,
        Target3: objKPI_Q3_Target,
        Target4: objKPI_Q4_Target,
        Weight: Weight,
        kpi_type: KPITypeId,
        target_type: target_type,
        LanguageCode: lang,
        betterUpDown: betterUpDown + ""

      },
    });
  }

  UpdateEmployeeObjectiveKPI(
    ID,
    ModifiedBy,
    Name,
    Target,
    ddlKPIWeight,
    ddlTarget_type,
    KPITypeId,
    lang,
    betterUpDown = 1
  ) {
    return this.http.get<any>(
      Config.WebApiUrl + "EmployeeObjectiveKPI/Update",
      {
        params: {
          ID: ID,
          ModifiedBy: ModifiedBy,
          Name: Name,
          Target: Target,
          Weight: ddlKPIWeight,
          target_type: ddlTarget_type,
          kpi_type: KPITypeId,
          LanguageCode: lang,
          betterUpDown: betterUpDown + ""

        },
      }
    );
  }

  DeleteEmployeeObjectiveKPI(id) {
    return this.http.get<any>(
      Config.WebApiUrl + "EmployeeObjectiveKPI/Delete",
      {
        params: {
          ID: id,
        },
      }
    );
  }

  /********************/

  /********************/

  getEmployeeObjectivesAssessment(employeeAssessmentId, companyId, lang) {
    return this.http.get<any>(
      Config.WebApiUrl +
      "EmployeeObjectiveAssessment/GetEmployeeObjectivesAssessments",
      {
        params: {
          employeeAssessmentId: employeeAssessmentId,
          LanguageCode: lang,
          companyId: companyId,
        },
      }
    );
  }

  old_SaveObjectiveKpiResults(objectiveKpiResults, companyId) {
    let form = new FormData();
    form.append("objective_kpi_results", JSON.stringify(objectiveKpiResults));

    return this.http.post<any>(
      Config.WebApiUrl + "EmployeeObjectiveAssessment/SaveObjectiveKpiResults",
      form,
      {
        params: {
          companyId: companyId,
        },
      }
    );
  }

  SaveObjectiveKpiResults(objectiveKpiResults, companyId, username, empObjID) {
    let form = new FormData();
    form.append("KPIs_List", JSON.stringify(objectiveKpiResults));

    return this.http.post<any>(
      Config.WebApiUrl + "EmployeeObjectiveAssessment/SaveObjectiveKpiResults",
      form,
      {
        params: {
          companyId: companyId,
          username: username,
          empObjID: empObjID,
        },
      }
    );
  }

  SaveObjectivesResults(objectiveResults, companyId) {
    let form = new FormData();
    form.append("objectives_results", JSON.stringify(objectiveResults));

    return this.http.post<any>(
      Config.WebApiUrl + "EmployeeObjectiveAssessment/SaveObjectiveResults",
      form,
      {
        params: {
          companyId: companyId,
        },
      }
    );
  }

  getEmployeeCompetenciesAssessment(employeeAssessmentId, companyId, lang) {
    return this.http.get<any>(
      Config.WebApiUrl +
      "EmployeeCompetencyAssessment/GetEmployeeCompetenciesAssessments",
      {
        params: {
          employeeAssessmentId: employeeAssessmentId,
          LanguageCode: lang,
          companyId: companyId,
        },
      }
    );
  }

  SaveCompetencyKpiResults(
    competencyKpiResults,
    companyId,
    username,
    empObjID
  ) {
    let form = new FormData();
    form.append("competency_kpi_results", JSON.stringify(competencyKpiResults));

    return this.http.post<any>(
      Config.WebApiUrl + "EmployeeCompetency/SaveCompetancyKpiResults",
      form,
      {
        params: {
          companyId: companyId,
          username: username,
          empComID: empObjID,
        },
      }
    );
  }

  SaveCompetenciesResults(competenciesResults) {
    let form = new FormData();
    form.append("competencies_results", JSON.stringify(competenciesResults));

    return this.http.post<any>(
      Config.WebApiUrl + "EmployeeCompetencyAssessment/SaveCompetenciesResults",
      form
    );
  }

  /**************************************** */

  GetEmployeeEducations(CompanyID, EmployeeId, YearId, languageCode) {
    return this.http.get<any>(
      Config.WebApiUrl + "EmployeeEducation/GetEmployeeEducations",
      {
        params: {
          CompanyID: CompanyID,
          EmployeeId: EmployeeId,
          YearId: YearId,
          languageCode: languageCode,
        },
      }
    );
  }

  /**************************************** */

  GetEmployeeDevelopmentPlan(CompanyID, YearId, languageCode, quarterFilter = -1, unitId = -1) {
    return this.http.get<any>(
      Config.WebApiUrl + "EmployeeEducation/GetEmployeeDevelopmentPlan",
      {
        params: {
          CompanyID: CompanyID,
          YearId: YearId,
          languageCode: languageCode,
          quarterFilter: quarterFilter + "",
          unitId: unitId + ""
        },
      }
    );
  }

  SaveEmployeeEducation(
    company_id,
    year_id,
    employee_id,
    emp_competency_id,
    emp_obj_id,
    execution_period,
    field,
    priority,
    status,
    type,
    method,
    created_by,
    notes,
    languageCode
  ) {
    return this.http.get<any>(Config.WebApiUrl + "EmployeeEducation/Save", {
      params: {
        company_id: company_id,
        year_id: year_id,
        employee_id: employee_id,
        emp_competency_id: emp_competency_id,
        emp_obj_id: emp_obj_id,
        execution_period: execution_period,
        field: field,
        priority: priority,
        status: status,
        type: type,
        method: method,
        created_by: created_by,
        notes: notes,
        languageCode: languageCode,
      },
    });
  }

  SaveEmployeeRecords(
    EmpId,
    ObjectiveId,
    CompetencyId,
    Behavior,
    PerformanceDate,
    Effect,
    AgreedAction,
    Comment
  ) {
    return this.http.get<any>(Config.WebApiUrl + "PerformanceRecords/Save", {
      params: {
        EmpId,
        ObjectiveId,
        CompetencyId,
        Behavior,
        PerformanceDate,
        Effect,
        AgreedAction,
        Comment,
      },
    });
  }

  updateEmployeeRecord(
    ID,
    EmpId,
    ObjectiveId,
    CompetencyId,
    Behavior,
    PerformanceDate,
    Effect,
    AgreedAction,
    Comment
  ) {
    return this.http.get<any>(
      `${Config.WebApiUrl}PerformanceRecords/Update/${ID}`,
      {
        params: {
          EmpId,
          ObjectiveId,
          CompetencyId,
          Behavior,
          PerformanceDate,
          Effect,
          AgreedAction,
          Comment,
        },
      }
    );
  }

  deleteEmployeePerformanceRecord(ID) {
    return this.http.get<any>(
      `${Config.WebApiUrl}PerformanceRecords/Delete/${ID}`
    );
  }

  getEmployeeRecordByObjectiveIdAndEmpId(ObjectiveID, EmpId) {
    return this.http.get<any>(
      Config.WebApiUrl + "PerformanceRecords/getByObjectiveIdAndEmpId",
      {
        params: {
          ObjectiveID,
          EmpId,
        },
      }
    );
  }

  getPerformanceByCompatancyId(CompetencyId, EmpId) {
    return this.http.get<any>(
      `${Config.WebApiUrl}/PerformanceRecords/getByCompetencyIdAndEmpId`,
      {
        params: {
          CompetencyId,
          EmpId,
        },
      }
    );
  }

  UpdateEmployeeEducation(
    id,
    company_id,
    year_id,
    employee_id,
    emp_competency_id,
    emp_obj_id,
    execution_period,
    field,
    priority,
    status,
    type,
    method,
    modified_by,
    notes,
    languageCode
  ) {
    return this.http.get<any>(Config.WebApiUrl + "EmployeeEducation/Update", {
      params: {
        id: id,
        company_id: company_id,
        year_id: year_id,
        employee_id: employee_id,
        emp_competency_id: emp_competency_id,
        emp_obj_id: emp_obj_id,
        execution_period: execution_period,
        field: field,
        priority: priority,
        status: status,
        type: type,
        method: method,
        modified_by: modified_by,
        notes: notes,
        languageCode: languageCode,
      },
    });
  }

  DeleteEmployeeEducation(id) {
    return this.http.get<any>(Config.WebApiUrl + "EmployeeEducation/Delete", {
      params: {
        ID: id,
      },
    });
  }

  LoadEmployeeEducationByID(id, languageCode) {
    return this.http.get<any>(Config.WebApiUrl + "EmployeeEducation/getByID", {
      params: {
        id: id,
        languageCode: languageCode,
      },
    });
  }

  LoadEmployeeManager(empID) {
    return this.http.get<any>(
      Config.WebApiUrl + "EmployeeAssesment/LoadEmployeeManager",
      {
        params: {
          id: empID,
        },
      }
    );
  }

  getFinalResultsForObjective(EmployeeAssesmentID, LanguageCode) {
    return this.http.get<any>(
      `${Config.WebApiUrl}EmployeeObjective/GetEmployeeObjectiveFinalResult`,
      {
        params: {
          EmployeeAssesmentID,
          LanguageCode,
        },
      }
    );
  }

  getFinalResultsForCompetency(employeeAssessmentId, languageCode) {
    return this.http.get<any>(
      `${Config.WebApiUrl}EmployeeCompetency/GetEmployeeCompetenciesFinalResult`,
      {
        params: {
          employeeAssessmentId,
          languageCode,
        },
      }
    );
  }

  updateWeightOfEmployee(id, objectives_weight, competencies_weight) {
    return this.http.get<any>(
      `${Config.WebApiUrl}EmployeeAssesment/UpdateWeight/${id}`,
      {
        params: {
          objectives_weight,
          competencies_weight,
        },
      }
    );
  }
}
