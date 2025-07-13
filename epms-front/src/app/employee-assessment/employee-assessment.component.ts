import {Component, OnInit, ViewChild} from '@angular/core';
import {EmployeeAssesmentService} from '../Services/employee-assesment.service';
import {CodesService} from '../Services/codes.service';
import {UserContextService} from '../Services/user-context.service';
import {EmployeeService} from '../Organization/employees/employee.service';
import {ProjectsService} from '../Services/Projects/projects.service';
import {UsersService} from '../Services/Users/users.service';

import {PositionsService} from '../Services/positions.service';

@Component({
  selector: 'app-employee-assessment',
  templateUrl: './employee-assessment.component.html',
  styleUrls: ['./employee-assessment.component.css']
  , providers: [UsersService]
})
export class EmployeeAssessmentComponent implements OnInit {
  employeeObjectivesAssessmentList;
  employeeCompetenciesAssessmentList;
  formRow;
  PageResources = [];
  ddlUnitSearch;
  UnitsList;
  @ViewChild('gvAssesment', {read: false, static: false}) gvAssesment;
  @ViewChild('gvObjective', {read: false, static: false}) gvObjective;
  @ViewChild('gvObjectiveKPI', {read: false, static: false}) gvObjectiveKPI;
  AssesmentUpdateMode = false;
  ObjectiveUpdateMode = false;
  ObjectiveKPIUpdateMode = false;
  YearList
  EmployeeList;
  KpiCycleList;
  ProjectList;
  ObjectivePositionDescList;
  AssesmentSearchEmployeeID;
  AssesmentSearchYearID;
  AssesmentID;
  AssesmentAgreementDate;
  AssessmentResult;
  AssessmentColor;
  AssessmentTarget;
  AssesmentAttachment;
  /*******/
  AssesmentKpiCycle;
  AssesmentYearID;
  AssesmentReviewerID;
  AssesmentEmpPositionID;
  AssesmentEmployeeID;
  /*******/
  ObjectiveID;
  ObjectiveCode;
  ObjectiveName;
  ObjectiveName2;
  ObjectiveNote;
  ObjectiveWeight;
  ObjectiveProjectID;
  ObjectivePositionDescID;
  /*******/
  ObjectiveKPIName;
  ObjectiveKPIName2;
  ObjectiveKPITarget = null;
  ObjectiveKPIID;

  constructor(
    private employeeAssesmentService: EmployeeAssesmentService,
    private codesService: CodesService,
    private userContextService: UserContextService,
    private employeeService: EmployeeService,
    private projectsService: ProjectsService,
    private positionsService: PositionsService,
    private userService: UsersService
  ) {
    this.fillEmployeeList();
    this.fillYearList();
    this.fillKPICycleList();


    this.userService.GetLocalResources(window.location.hash, this.userContextService.CompanyID, this.userContextService.language).subscribe(res => {
      if (res.IsError) {
        alert(res.ErrorMessage + ',' + 1);
        return;
      }
      this.PageResources = res.Data;
    });

    this.fillUnitLst();
  }

  RefershEmployees() {
    this.fillEmployeeList();
  }

  GetLocalResourceObject(resourceKey) {
    for (let i = 0; i < this.PageResources.length; i++) {
      if (this.PageResources[i].resource_key === resourceKey) {
        return this.PageResources[i].resource_value;
      }
    }
  }

  fillKPICycleList() {
    var dd = this.codesService.LoadCodes(this.userContextService.CompanyID, 3, this.userContextService.language).subscribe(res => {
      if (res.IsError) {
        alert(res.ErrorMessage + ',' + 1);
        return;
      }

      console.log('KpiCycleList', res.Data);

      this.KpiCycleList = res.Data;
    });
  }

  fillProjectList() {
    var dd = this.projectsService.GetProjectsByEmployeeID(this.AssesmentEmployeeID, this.userContextService.language).subscribe(res => {
      if (res.IsError) {
        alert(res.ErrorMessage + ',' + 1);
        return;
      }
      this.ProjectList = res.Data;
    });
  }

  fillObjectivePositionDescList() {
    var dd = this.positionsService.LoadJobDescriptionByEmployee(this.AssesmentEmployeeID, this.userContextService.language).subscribe(res => {
      if (res.IsError) {
        alert(res.ErrorMessage + ',' + 1);
        return;
      }

      console.log('ObjectivePositionDescList', res.Data);

      this.ObjectivePositionDescList = res.Data;
    });
  }

  fillEmployeeList() {
    var dd = this.employeeService.LoadEmplyeeByCompanyID(this.userContextService.CompanyID, this.userContextService.language, this.ddlUnitSearch).subscribe(res => {
      if (res.IsError) {
        alert(res.ErrorMessage + ',' + 1);
        return;
      }
      this.EmployeeList = res.Data;
    });
  }

  fillYearList() {
    this.employeeAssesmentService.getYears().subscribe(res => {
      if (res.IsError) {
        alert(res.ErrorMessage + ',' + 1);
        return null;
      }
      this.YearList = res.Data;
      if (this.YearList != null && this.YearList.length > 0) {
        this.YearList.forEach((element) => {
          this.AssesmentSearchYearID = element.id;
          this.AssesmentYearID = element.id;

        });

      }
    });
  }

  /*******/

  ngOnInit() {
  }

  /**************************************/

  setDefaultAddOnFly(employeeAssessmentId) {
    this.employeeAssesmentService.getEmployeeObjectivesAssessment(
      employeeAssessmentId, this.userContextService.CompanyID, this.userContextService.language
    ).subscribe(res => {
      if (res.IsError) {
        alert(res.ErrorMessage + ',' + 1);
        return;
      }
      console.log('employeeObjectivesAssessmentList', res.Data);
      this.employeeObjectivesAssessmentList = res.Data;
    });

    this.employeeAssesmentService.getEmployeeCompetenciesAssessment(
      employeeAssessmentId, this.userContextService.CompanyID, this.userContextService.language
    ).subscribe(res => {
      if (res.IsError) {
        alert(res.ErrorMessage + ',' + 1);
        return;
      }
      console.log('employeeCompetenciesAssessmentList', res.Data);

      this.employeeCompetenciesAssessmentList = res.Data;
    });


  }


  getClassName(color) {

    return "label pull-left bg-" + color;
  }

  getColorStyle(color) {
    if (color != undefined)
      return color;
    else
      return "white";
  }

  LoadAssesment() {
    this.AssesmentID = null;
    this.employeeAssesmentService.LoadAssesment(this.AssesmentSearchEmployeeID, this.AssesmentSearchYearID, null).subscribe(res => {
      if (res.IsError) {
        alert(res.ErrorMessage + ',' + 1);
        return;
      }

      console.log('apiiiiiiiii', res.Data);

      var assId = res.Data[0].ID;

      this.LoadAssesmentByID(assId);
      this.setDefaultAddOnFly(assId);

    });
  }

  LoadAssesmentByID(id) {
    this.employeeAssesmentService.LoadAssesmentByID(id).subscribe(res => {
      if (res.IsError) {
        alert(res.ErrorMessage + ',' + 1);
        return;
      }

      this.AssesmentAgreementDate = res.Data.agreement_date;
      this.AssesmentAttachment = res.Data.attachment;
      this.AssesmentEmpPositionID = res.Data.emp_position_id;
      this.AssesmentEmployeeID = res.Data.employee_id;
      this.AssesmentID = res.Data.ID;
      this.AssesmentKpiCycle = res.Data.c_kpi_cycle;
      this.AssesmentReviewerID = res.Data.emp_reviewer_id;
      this.AssesmentYearID = res.Data.year_id;
      this.AssessmentResult = res.Data.result_after_round;
      this.AssessmentTarget = res.Data.target;
      this.AssessmentColor = res.Data.color;
      this.AssesmentUpdateMode = true;
    });
  }

  /**************************************/


  SaveObjectiveKpiResults(objectivekpiResults) {
    // this.employeeAssesmentService.SaveObjectiveKpiResults(objectivekpiResults, this.userContextService.CompanyID).subscribe(res => {
    //   if (res.IsError) {
    //     alert(res.ErrorMessage + ',' + 1);
    //     return;
    //   }
    //   this.LoadAssesment();
    // });
    // method cal API with params as (kpiDetails)
  }


  SaveObjectivesResults(objectivesResults) {
    this.employeeAssesmentService.SaveObjectivesResults(objectivesResults, this.userContextService.CompanyID).subscribe(res => {
      if (res.IsError) {
        alert(res.ErrorMessage + ',' + 1);
        return;
      }
      this.LoadAssesment();
    });
    // method cal API with params as (kpiDetails)
  }


  SaveCompetencyKpiResults(CompetencykpiResults) {
    // this.employeeAssesmentService.SaveCompetencyKpiResults(CompetencykpiResults).subscribe(res => {
    //   if (res.IsError) {
    //     alert(res.ErrorMessage + ',' + 1);
    //     return;
    //   }
    //   this.LoadAssesment();
    // });
    // method cal API with params as (kpiDetails)
  }


  SaveCompetenciesResults(competenciesResults) {
    this.employeeAssesmentService.SaveCompetenciesResults(competenciesResults).subscribe(res => {
      if (res.IsError) {
        alert(res.ErrorMessage + ',' + 1);
        return;
      }
      this.LoadAssesment();
    });
    // method cal API with params as (kpiDetails)
  }


  fillUnitLst() {
    this.projectsService.GetUnits(this.userContextService.CompanyID, this.userContextService.language).subscribe(
      res => {

        this.UnitsList = this.userContextService.RoleId !== 5 ? res.Data : res.Data.filter(a => {
          return a.ID === this.userContextService.UnitId;
        });

        this.ddlUnitSearch = '';


      }
    )
  }

  loadUnitEmployees() {

    this.employeeService.LoadEmplyeeByCompanyID(this.userContextService.CompanyID, this.userContextService.language, this.ddlUnitSearch)
      .subscribe(res => {
        if (res.IsError) {
          alert(res.ErrorMessage + ',0');
          return;
        }
        this.EmployeeList = res.Data;
      });

  }

  /**************************************/

}
