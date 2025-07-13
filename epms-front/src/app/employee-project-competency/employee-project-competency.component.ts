import {Component, EventEmitter, Input, OnInit, Output, ViewChild} from '@angular/core';
import {EmployeeAssesmentService} from "../Services/employee-assesment.service";
import {CodesService} from "../Services/codes.service";
import {UserContextService} from "../Services/user-context.service";
import {EmployeeService} from "../Organization/employees/employee.service";
import {ProjectsService} from "../Services/Projects/projects.service";
import {PositionsService} from "../Services/positions.service";
import {UsersService} from "../Services/Users/users.service";

@Component({
  selector: 'app-employee-project-competency',
  templateUrl: './employee-project-competency.component.html',
  styleUrls: ['./employee-project-competency.component.css']
})
export class EmployeeProjectCompetencyComponent implements OnInit {
  PageResources = [];
  ddlUnitSearch;
  @ViewChild("gvAssesment", {read: false, static: false}) gvAssesment;
  ProjectList;
  objectivesList;
  projectId = -1;
  competencyId = -1;
  objectiveId = -1;
  showAssesmentEntry = false;
  showTab = false;
  YearList;
  EmployeeList;
  allEmployeesList;
  CompetenciesList;
  CompetenceID;
  AssesmentSearchEmployeeID = -1;
  AssesmentSearchYearID;
  AssesmentEmployeeID;
  assesments;
  KPITypeId;
  UnitsList;
  counter;
  modificationPermission = false;

  constructor(
    private employeeAssesmentService: EmployeeAssesmentService,
    private codesService: CodesService,
    private positionService: PositionsService,
    public userContextService: UserContextService,
    private employeeService: EmployeeService,
    private projectsService: ProjectsService,
    private userService: UsersService,
  ) {
    this.modificationPermission = this.userContextService.RoleId != 5;
    this.fillYearList();
    this.LoadCompetence();
    this.fillUnitLst();
    this.fillProjectList();
    this.loadUnitEmployees();
  }

  GetLocalResourceObject(resourceKey) {
    for (let i = 0; i < this.PageResources.length; i++) {
      if (this.PageResources[i].resource_key === resourceKey) {
        return this.PageResources[i].resource_value;
      }
    }
  }

  fillEmployeeObjectiveList() {
    console.log(this.AssesmentSearchEmployeeID);
    this.employeeAssesmentService
      .GetEmployeeObjectives(this.AssesmentSearchEmployeeID != -1
          ? this.AssesmentSearchEmployeeID : null,
        this.userContextService.Role === "Admin"
        || this.userContextService.UnitId === 0 ? this.ddlUnitSearch : this.userContextService.UnitId,
        this.AssesmentSearchYearID,
        this.userContextService.language,
      ).subscribe((res) => {
      if (res.IsError) {
        alert(res.ErrorMessage + "," + 1);
        return;
      }
      this.objectivesList = res.Data;

    });


  }

  fillUnitLst() {
    this.projectsService
      .GetUnits(
        this.userContextService.CompanyID,
        this.userContextService.language
      )
      .subscribe((res) => {
        this.UnitsList = this.userContextService.Role === "Admin"
        || this.userContextService.UnitId === 0 ? res.Data : res.Data.filter(a => {
          return a.ID == this.userContextService.UnitId;
        });

        this.ddlUnitSearch = "";
      });
  }

  fillEmployeeList(unitId = null) {
    const dd = this.employeeService
      .LoadEmplyeeByCompanyID(
        this.userContextService.CompanyID,
        this.userContextService.language,
        this.userContextService.Role === "Admin"
        || this.userContextService.UnitId === 0 ? this.ddlUnitSearch : this.userContextService.UnitId
      )
      .subscribe((res) => {
        if (res.IsError) {
          alert(res.ErrorMessage + "," + 1);
          return;
        }
        this.allEmployeesList = res.Data;

      });
  }


  fillYearList() {
    this.employeeAssesmentService.getYears().subscribe((res) => {
      if (res.IsError) {
        alert(res.ErrorMessage + "," + 1);
        return null;
      }
      this.YearList = res.Data;

      if (this.YearList != null && this.YearList.length > 0) {
        this.YearList.forEach((element) => {
          this.AssesmentSearchYearID = element.id;
        });
        this.fillEmployeeList();
        this.fillEmployeeObjectiveList();

      }
    });
  }

  ngOnInit() {

    this.userService
      .GetLocalResources(
        window.location.hash,
        this.userContextService.CompanyID,
        this.userContextService.language
      )
      .subscribe((res) => {
        if (res.IsError) {
          alert(res.ErrorMessage + "," + 1);
          return;
        }
        this.PageResources = res.Data;
      });
  }

  getEmployeeAssessmentStatusText(status) {
    if (status === 0) {
      return "Waiting Review";
    } else if (status === 1) {
      return "Reviewed";
    } else if (status === 2) {
      return "Approved";
    } else if (status === 3) {
      return "Result Reviewed";
    } else if (status === 4) {
      return "Result Approved";
    }
  }

  getEmployeeName(employeeId) {
    const employee = this.allEmployeesList.filter(a => a.ID === employeeId)[0];
    return employee ? employee.name1_1 + " " + employee.name1_4 : "";
  }

  fillProjectList() {

    if (this.AssesmentEmployeeID > 0) {
      this.projectsService
        .GetProjectsByEmployeeID(
          this.AssesmentEmployeeID,
          this.userContextService.language
        )
        .subscribe((res) => {
          if (res.IsError) {
            alert(res.ErrorMessage + "," + 1);
            return;
          }
          this.ProjectList = res.Data;
        });
    } else {

      this.projectsService
        .GetProjectsByUnitID(
          this.ddlUnitSearch,
          this.userContextService.language
        )
        .subscribe((res) => {
          if (res.IsError) {
            alert(res.ErrorMessage + "," + 1);
            return;
          }
          this.ProjectList = res.Data;
        });
    }

  }

  LoadAssesment() {
    this.employeeAssesmentService
      .LoadEmployeeProjectCompetency(
        this.AssesmentSearchEmployeeID,
        this.AssesmentSearchYearID,
        this.userContextService.Role === "Admin"
        || this.userContextService.UnitId === 0 ? this.ddlUnitSearch : this.userContextService.UnitId,
        this.projectId,
        this.competencyId,
        this.objectiveId
      )
      .subscribe((res) => {
        if (res.IsError) {
          alert(res.ErrorMessage + "," + 1);
          return;
        }
        res.Data.map(a => {
          a.statusText = this.getEmployeeAssessmentStatusText(a.status);
          a.manager = this.getEmployeeName(a.emp_manager_id);
          a.director = this.getEmployeeName(a.emp_reviewer_id);

        });

        const cols = [
          {
            HeaderText: "Employee",
            DataField: "EmployeeName",
          },
          {
            HeaderText: "Unit",
            DataField: "Unit",
          },
          {
            HeaderText: "Project",
            DataField: "projectName",
          },
          {
            HeaderText: "Individual objective",
            DataField: "objectiveName",
          },
          {
            HeaderText: "Competency",
            DataField: "competencyName",
          },
        ];

        this.assesments = res.Data;
        this.gvAssesment.bind(cols, res.Data, "gvAssesment", []);
      });
  }

  selectEmployeeEntry() {
    this.fillEmployeeObjectiveList();
    this.fillProjectList();
  }

  unitChange() {
    this.loadUnitEmployees();
    this.fillEmployeeObjectiveList();
  }

  employeeChanged() {
    this.fillEmployeeObjectiveList();
  }

  loadUnitEmployees() {
    this.employeeService
      .LoadEmplyeeByCompanyID(
        this.userContextService.CompanyID,
        this.userContextService.language,
        this.ddlUnitSearch
      )
      .subscribe((res) => {
        if (res.IsError) {
          alert(res.ErrorMessage + ",0");
          return;
        }
        this.EmployeeList = (this.userContextService.RoleId === 2 || this.userContextService.Role === "Employee")
          ? this.getEmployeeChilderns(res.Data)
          : res.Data;
        this.fillProjectList();
      });
  }

  getEmployeeChilderns(employeeList: any) {
    if (this.userContextService.Role === "Employee") {
      const currentEmployee = employeeList.filter(a => a.employee_number === this.userContextService.Username)[0];
      return employeeList.filter(a => (a.employee_number === this.userContextService.Username || a.PARENT_ID === currentEmployee.ID));
    }
    if (this.userContextService.Role === "Unit") {
      return employeeList.filter(a => (a.UNIT_ID === this.userContextService.UnitId));
    }
    return employeeList;

  }

  LoadCompetence() {
    if (this.userContextService.CompanyID > 0) {
      this.positionService.LoadCompetenceis(this.userContextService.CompanyID,
        "", this.userContextService.language, -1).subscribe(res => {
        if (res.IsError) {
          alert(res.ErrorMessage);
          return;
        }
        this.CompetenciesList = res.Data;

      });
    } else {
    }
  }

}
