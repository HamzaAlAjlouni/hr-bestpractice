import {Component, EventEmitter, Input, OnInit, Output, ViewChild,} from "@angular/core";
import {EmployeeAssesmentService} from "../Services/employee-assesment.service";
import {CodesService} from "../Services/codes.service";
import {UserContextService} from "../Services/user-context.service";
import {EmployeeService} from "../Organization/employees/employee.service";
import {ProjectsService} from "../Services/Projects/projects.service";
import {OrganizationService} from "../Organization/organization.service";

import {PositionsService} from "../Services/positions.service";
import {UsersService} from "../Services/Users/users.service";
import {log} from "util";

@Component({
  selector: "app-employee-objectve",
  templateUrl: "./employee-objectve.component.html",
  styleUrls: ["./employee-objectve.component.css"],
  providers: [UsersService],
})
export class EmployeeObjectveComponent implements OnInit {
  @Input()
  AddOnFly = false;
  @Output()
  empPerformanceClose = new EventEmitter<string>();
  ddlKPIWeight: Number;
  KpiTypeLst: any;
  allObjCode: any[] = [];
  isExistObjectiveCode: boolean = false;
  PageResources = [];
  ddlUnitSearch;
  @ViewChild("gvAssesment", {read: false, static: false}) gvAssesment;
  @ViewChild("gvObjective", {read: false, static: false}) gvObjective;
  @ViewChild("gvObjectiveKPI", {read: false, static: false}) gvObjectiveKPI;
  @ViewChild("gvCompetence", {read: false, static: false}) gvCompetence;
  @ViewChild("gvCompetenceKPI", {read: false, static: false}) gvCompetenceKPI;
  ddlBetterUpDown = 1;
  BetterUpDownList = [
    {id: 1, name: "Better Up"},
    {id: 2, name: "Better Down"}

  ];
  AssesmentUpdateMode = false;
  ObjectiveUpdateMode = false;
  ObjectiveKPIUpdateMode = false;
  CompetenceUpdateMode = false;
  CompetenceKPIUpdateMode = false;
  ddlCompetenceNatureID = null;
  showAssesmentEntry = false;
  showTab = false;
  showObjectiveEntry = false;
  showCompetanceEntry = false;
  showObjectiveKPIEntry = false;
  showCompetanceKPIEntry = false;
  showObjectiveKPIList = false;
  showCompetanceKPIList = false;
  competenceNatureList;
  YearList;
  EmployeeList;
  allEmployeesList;
  FilterdEmployeeList;
  KpiCycleList;
  ProjectList;
  ObjectivePositionDescList;
  CompetenceCompetenceList;
  CompetenceKPIList;
  CompetenceLevelList;
  /*******/
  CompetenceCompetenceID = 0;
  CompetenceLevelID;
  CompetenceWeight;
  CompetenceID;
  /*******/
  CompetenceKPITypeID;
  CompetenceKPIID;
  CompetenceKPITarget = 5;
  /*******/
  AssesmentSearchEmployeeID = -1;
  AssesmentSearchYearID;
  AssesmentID;
  AssesmentAgreementDate;
  AssesmentAttachment;
  AssesmentKpiCycle;
  AssesmentYearID;
  AssesmentReviewerID;
  AssesmentManagerID;
  AssesmentEmpPositionID;
  AssesmentEmployeeID;
  AssessmentStatus;
  AssessmentEmpReviewerId;
  AssessmentEmpManagerId;
  AssesmentTarget = 5;
  assesments;
  /*******/
  ObjectiveID;
  ObjectiveCode;
  ObjectiveName;
  ObjectiveNote = "";
  ObjectiveWeight = 0;
  ObjectiveProjectID;
  ObjectiveCompetencyID;
  ObjectivePositionDescID;
  ObjectiveTarget;
  /*******/
  ObjectiveKPIName;
  ObjectiveKPITarget;
  ObjectiveKPIID;
  /*******/
  IsQuarter1;
  IsQuarter2;
  IsQuarter3;
  IsQuarter4;
  obj_Q1_Target;
  obj_Q2_Target;
  obj_Q3_Target;
  obj_Q4_Target;
  objKPI_Q1_Target;
  objKPI_Q2_Target;
  objKPI_Q3_Target;
  objKPI_Q4_Target;
  KPITypeId;
  UnitsList;
  ddlTarget_type;
  counter;
  ObjectiveDateSource = null;
  ObjectiveMaxWeight = 100;
  selectedcompetenceID = null;
  CompetenceMaxWeight = 100;
  CompentenceDataSource = null;
  modificationPermission = false;

  constructor(
    private employeeAssesmentService: EmployeeAssesmentService,
    private codesService: CodesService,
    public userContextService: UserContextService,
    private employeeService: EmployeeService,
    private projectsService: ProjectsService,
    private positionsService: PositionsService,
    private userService: UsersService,
    private OrganizationService: OrganizationService
  ) {
    this.modificationPermission = this.userContextService.RoleId != 5;
    //this.fillEmployeeList();
    this.fillYearList();
    this.getNatureCodesList();
    this.fillKPICycleList();
    this.getCompetenceLevelList();
    this.fillUnitLst();
  }

  setDefaultsOnFly(empID) {
    this.AssesmentSearchEmployeeID = empID;
    this.LoadAssesment();
  }

  GetLocalResourceObject(resourceKey) {
    for (let i = 0; i < this.PageResources.length; i++) {
      if (this.PageResources[i].resource_key === resourceKey) {
        return this.PageResources[i].resource_value;
      }
    }
  }

  fillUnitLst() {
    this.projectsService
      .GetUnits(
        this.userContextService.CompanyID,
        this.userContextService.language
      )
      .subscribe((res) => {


        this.UnitsList = this.userContextService.Role === "Admin" || this.userContextService.UnitId === 0 ? res.Data : res.Data.filter(a => {
          return a.ID == this.userContextService.UnitId;
        });

        this.ddlUnitSearch = "";
      });
  }

  ChangeCompetenceSelection() {
    for (let i = 0; i < this.CompetenceCompetenceList.length; i++) {
      if (
        this.CompetenceCompetenceList[i].CompetenceID.toString() ===
        this.CompetenceCompetenceID
      ) {
        this.CompetenceLevelID = this.CompetenceCompetenceList[
          i
          ].CompetenceLevel;
        this.ddlCompetenceNatureID = this.CompetenceCompetenceList[
          i
          ].CompetenceNature;
      }
    }
  }

  fillCompetenceCompetenceList() {
    this.positionsService
      .LoadPositionCompetencies(
        this.AssesmentEmpPositionID,
        this.userContextService.language,
        this.ddlCompetenceNatureID
      )
      .subscribe((res) => {
        if (res.IsError) {
          alert(res.ErrorMessage + "," + 1);
          return;
        }
        this.CompetenceCompetenceList = res.Data;
      });
  }

  fillCompetenceKPIList(CompetenceID) {
    this.positionsService
      .LoadCompetenceKpi(CompetenceID, this.userContextService.language)
      .subscribe((res) => {
        if (res.IsError) {
          alert(res.ErrorMessage + "," + 1);
          return;
        }
        this.CompetenceKPIList = res.Data;
      });
  }

  fillKpiType() {
    this.projectsService
      .GetCodes(4, this.userContextService.CompanyID)
      .subscribe((res) => {
        this.KpiTypeLst = res.Data;
      });
  }

  fillKPICycleList() {
    var dd = this.codesService
      .LoadCodes(
        this.userContextService.CompanyID,
        3,
        this.userContextService.language
      )
      .subscribe((res) => {
        if (res.IsError) {
          alert(res.ErrorMessage + "," + 1);
          return;
        }
        if (res.Data != null && res.Data.length > 0) {
          this.KpiCycleList = res.Data.filter((t) => {
            return t.MINOR_NO != 1;
          });
        } else this.KpiCycleList = res.Data;
      });
  }

  fillProjectList() {
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
  }

  fillObjectivePositionDescList() {
    var dd = this.positionsService
      .LoadJobDescriptionByEmployee(
        this.AssesmentEmployeeID,
        this.userContextService.language
      )
      .subscribe((res) => {
        if (res.IsError) {
          alert(res.ErrorMessage + "," + 1);
          return;
        }
        this.ObjectivePositionDescList = res.Data;
      });
  }

  fillEmployeeList(unitId = null) {
    var dd = this.employeeService
      .LoadEmplyeeByCompanyID(
        this.userContextService.CompanyID,
        this.userContextService.language,
        this.userContextService.Role === "Admin" || this.userContextService.UnitId === 0 ? this.ddlUnitSearch : this.userContextService.UnitId
      )
      .subscribe((res) => {
        if (res.IsError) {
          alert(res.ErrorMessage + "," + 1);
          return;
        }
        this.allEmployeesList = res.Data;
        this.EmployeeList = this.getEmployeeChilderns(res.Data);

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
      }
    });
  }

  ngOnInit() {
    if (this.AddOnFly) {
      this.userService
        .GetLocalResources(
          "#/employeeObjectve",
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
    } else {
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
    this.fillKpiType();
  }

  /**************************************/
  gvAssesmentEvent(event) {
    if (event[1] == "edit") {
      this.LoadAssesmentByID(event[0]);

      this.showObjectiveKPIList = false;
      this.showCompetanceKPIList = false;
      this.showObjectiveEntry = false;
      this.showCompetanceEntry = false;
      this.showObjectiveKPIEntry = false;
      this.showCompetanceKPIEntry = false;
      this.showAssesmentEntry = true;
      this.showTab = false;
    } else if (event[1] == "delete") {
      if (confirm(this.GetLocalResourceObject("Areyousure"))) {
        this.DeleteAssesment(event[0]);

        this.showObjectiveKPIList = false;
        this.showCompetanceKPIList = false;
        this.showObjectiveEntry = false;
        this.showCompetanceEntry = false;
        this.showObjectiveKPIEntry = false;
        this.showCompetanceKPIEntry = false;
        this.showAssesmentEntry = false;
        this.showTab = false;
      }
    } else if (event[1] == "Details") {
      this.AssesmentID = event[0];
      this.LoadAssesmentByID(event[0]);
      this.LoadCompetence(event[0]);
      this.LoadObjective(event[0], this.userContextService.language);

      this.showObjectiveKPIList = false;
      this.showCompetanceKPIList = false;
      this.showObjectiveEntry = false;
      this.showCompetanceEntry = false;
      this.showObjectiveKPIEntry = false;
      this.showCompetanceKPIEntry = false;
      this.showAssesmentEntry = false;
      this.showTab = true;
    }
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

  getApprovalActionText() {

    if (this.AssessmentStatus === 0) {
      return "Approve Review";
    } else if (this.AssessmentStatus === 1) {
      return "Approve";
    } else {
      return "";
    }


  }

  changeAssessmentStatus() {

    const nextStatus = this.AssessmentStatus === 0 ? 1 : this.AssessmentStatus === 1 ? 2 : 0;
    if (nextStatus !== 0)
      this.employeeAssesmentService.ChangeAssessmentStatus(this.AssesmentID, nextStatus).subscribe(
        (res) => {
          if (res.IsError) {
            alert(res.ErrorMessage + "," + 1);
            return;
          }
          // reload assessment
          this.LoadAssesmentByID(this.AssesmentID);
          this.LoadCompetence(this.AssesmentID);
          this.LoadObjective(this.AssesmentID, this.userContextService.language);
          this.showObjectiveKPIList = false;
          this.showCompetanceKPIList = false;
          this.showObjectiveEntry = false;
          this.showCompetanceEntry = false;
          this.showObjectiveKPIEntry = false;
          this.showCompetanceKPIEntry = false;
          this.showAssesmentEntry = false;
          this.showTab = true;
        });
  }

  showApproveAction() {
    const employee = this.EmployeeList.filter(a => a.employee_number === this.userContextService.Username)[0];
    if (employee) {
      return (employee.ID === this.AssessmentEmpManagerId && this.AssessmentStatus === 0)
        || (employee.ID === this.AssessmentEmpReviewerId && this.AssessmentStatus === 1);
    } else {
      return false;
    }
  }

  LoadAssesment() {
    this.employeeAssesmentService
      .LoadAssesment(
        this.AssesmentSearchEmployeeID,
        this.AssesmentSearchYearID,
        this.userContextService.Role === "Admin" || this.userContextService.UnitId === 0 ? this.ddlUnitSearch : this.userContextService.UnitId
      )
      .subscribe((res) => {
        if (res.IsError) {
          alert(res.ErrorMessage + "," + 1);
          return;
        }
        //this.positionsList = res.Data;

        res.Data.map(a => {
          a.statusText = this.getEmployeeAssessmentStatusText(a.status);
          a.manager = this.getEmployeeName(a.emp_manager_id);
          a.director = this.getEmployeeName(a.emp_reviewer_id);

        });

        let cols = [
          {
            HeaderText: this.GetLocalResourceObject("Employee"),
            DataField: "EmployeeName",
          },
          {
            HeaderText: "Position",
            DataField: "positionName",
          },
          {
            HeaderText: this.GetLocalResourceObject("Unit"),
            DataField: "Unit",
          },
          {
            HeaderText: "Direct Manager",
            DataField: "manager",
          },
          {
            HeaderText: "Revising Director",
            DataField: "director",
          },
          {
            HeaderText: "Assessment Status",
            DataField: "statusText",
          },
        ];
        let actions = [
          {
            title: "Edit",
            DataValue: "ID",
            Icon_Awesome: "fa fa-edit",
            Action: "edit",
          },
          {
            title: "Details",
            DataValue: "ID",
            Icon_Awesome: "fa fa-list-alt",
            Action: "Details",
          },
          {
            title: "Delete",
            DataValue: "ID",
            Icon_Awesome: "fa fa-trash",
            Action: "delete",
          },
        ];

        this.assesments = res.Data;
        this.gvAssesment.bind(cols, res.Data, "gvAssesment", actions);
      });

    this.showCompetanceKPIList = false;
    this.showCompetanceKPIEntry = false;
    this.showCompetanceEntry = false;
    this.showObjectiveEntry = false;
    this.showObjectiveKPIEntry = false;
    this.showObjectiveKPIList = false;
    this.showAssesmentEntry = false;
    this.showTab = false;
  }

  SaveAssesment() {
    if (!this.AssesmentUpdateMode) {
      this.counter = 0;

      if (this.IsQuarter1) this.counter++;
      if (this.IsQuarter2) this.counter++;
      if (this.IsQuarter3) this.counter++;
      if (this.IsQuarter4) this.counter++;
      if (this.AssesmentKpiCycle == 2) {
        if (this.counter != 2) {
          alert("you have to select two quarters." + "," + 1);
          return;
        }
      } else if (this.AssesmentKpiCycle == 3) {
        if (this.counter != 4) {
          alert("you have to select four quarters." + "," + 1);
          return;
        }
      }

      this.employeeAssesmentService
        .SaveAssesment(
          this.AssesmentAgreementDate,
          this.AssesmentAttachment,
          this.userContextService.Username,
          this.AssesmentKpiCycle,
          this.AssesmentSearchYearID,

          1, //this.AssesmentEmpPositionID,
          this.AssesmentEmployeeID,
          this.AssesmentTarget,
          this.IsQuarter1,
          this.IsQuarter2,
          this.IsQuarter3,
          this.IsQuarter4,
          this.AssesmentManagerID,
          this.userContextService.CompanyID
        )
        .subscribe((res) => {
          if (res.IsError) {
            alert(res.ErrorMessage + "," + 1);
            return;
          }
          this.AssesmentAddMode();
          this.LoadAssesment();
        });

      this.fillProjectList();
      this.fillObjectivePositionDescList();
    } else {
      this.employeeAssesmentService
        .UpdateAssesment(
          this.AssesmentID,
          this.AssesmentAgreementDate,
          this.AssesmentAttachment,
          this.userContextService.Username,
          this.AssesmentKpiCycle,
          this.AssesmentYearID,

          this.AssesmentEmpPositionID,
          this.AssesmentEmployeeID,
          this.AssesmentTarget,
          this.IsQuarter1,
          this.IsQuarter2,
          this.IsQuarter3,
          this.IsQuarter4,
          this.AssesmentManagerID,
          this.userContextService.CompanyID
        )
        .subscribe((res) => {
          if (res.IsError) {
            alert(res.ErrorMessage + "," + 1);
            return;
          }
          this.AssesmentAddMode();
          this.LoadAssesment();
        });
    }

    this.showCompetanceKPIList = false;
    this.showCompetanceKPIEntry = false;
    this.showCompetanceEntry = false;
    this.showObjectiveEntry = false;
    this.showObjectiveKPIEntry = false;
    this.showObjectiveKPIList = false;
    this.showAssesmentEntry = true;
    this.showTab = false;
  }

  DeleteAssesment(id) {
    this.employeeAssesmentService.DeleteAssesment(id).subscribe((res) => {
      if (res.IsError) {
        alert(res.ErrorMessage + "," + 1);
        return;
      }
      alert(this.GetLocalResourceObject("AssesmentDeleted"));
      this.AssesmentAddMode();
      this.LoadAssesment();
    });
  }

  LoadAssesmentByID(id) {
    this.employeeAssesmentService.LoadAssesmentByID(id).subscribe((res) => {
      if (res.IsError) {
        alert(res.ErrorMessage + "," + 1);
        return;
      }
      this.AssesmentAgreementDate = res.Data.agreement_date;
      this.AssesmentAttachment = res.Data.attachment;
      this.AssesmentEmpPositionID = res.Data.emp_position_id;
      this.AssesmentEmployeeID = res.Data.employee_id;
      this.AssesmentID = res.Data.ID;
      this.AssesmentKpiCycle = res.Data.c_kpi_cycle;
      this.AssesmentReviewerID = res.Data.emp_reviewer_id;
      this.AssesmentManagerID = res.Data.emp_manager_id;
      this.AssesmentYearID = res.Data.year_id;
      this.AssesmentTarget = res.Data.target;
      this.IsQuarter1 = res.Data.isQuarter1;
      this.IsQuarter2 = res.Data.isQuarter2;
      this.IsQuarter3 = res.Data.isQuarter3;
      this.IsQuarter4 = res.Data.isQuarter4;
      this.AssessmentStatus = res.Data.status;
      this.AssessmentEmpManagerId = res.Data.emp_manager_id;
      this.AssessmentEmpReviewerId = res.Data.emp_reviewer_id;
      this.fillProjectList();
      this.fillObjectivePositionDescList();
      this.AssesmentUpdateMode = true;
    });
    this.AssesmentUpdateMode = true;
    this.LoadObjective(this.AssesmentID, this.userContextService.language);
    this.fillProjectList();
    this.fillObjectivePositionDescList();
  }

  AssesmentAddMode() {
    // filter employee list
    this.FilterdEmployeeList = this.EmployeeList.filter(employee => !this.assesments.some(a => a.employee_id === employee.ID));
    //
    // console.log('filtered', this.EmployeeList.filter(employee => !this.assesments.some(a => a.employee_id === employee.ID)));
    // console.log('all', this.EmployeeList);
    this.AssesmentAgreementDate = "";
    this.AssesmentAttachment = "";
    this.AssesmentEmpPositionID = null;
    this.AssesmentEmployeeID = null;
    this.AssesmentID = null;
    this.AssesmentKpiCycle = null;
    this.AssesmentReviewerID = null;
    this.AssesmentYearID = this.AssesmentSearchYearID;
    this.AssesmentTarget = 5;
    this.AssesmentUpdateMode = false;
    this.gvObjective = null;
    this.gvObjectiveKPI = null;

    /***************************** */
    this.showCompetanceKPIList = false;
    this.showCompetanceKPIEntry = false;
    this.showCompetanceEntry = false;
    this.showObjectiveEntry = false;
    this.showObjectiveKPIEntry = false;
    this.showObjectiveKPIList = false;
    this.showAssesmentEntry = true;
    this.showTab = false;
  }

  /**************************************/
  gvObjectiveEvent(event) {
    if (event[1] == "edit") {
      this.LoadObjectiveByID(event[0], this.userContextService.language);

      this.showObjectiveKPIList = false;
      this.showCompetanceKPIList = false;
      this.showObjectiveEntry = true;
      this.showCompetanceEntry = false;
      this.showObjectiveKPIEntry = false;
      this.showCompetanceKPIEntry = false;
      this.showAssesmentEntry = false;
      this.showTab = true;
    } else if (event[1] == "delete") {
      if (confirm(this.GetLocalResourceObject("Areyousure"))) {
        this.DeleteObjective(event[0]);

        this.showObjectiveKPIList = false;
        this.showCompetanceKPIList = false;
        this.showObjectiveEntry = false;
        this.showCompetanceEntry = false;
        this.showObjectiveKPIEntry = false;
        this.showCompetanceKPIEntry = false;
        this.showAssesmentEntry = false;
        this.showTab = true;
      }
    } else if (event[1] == "Details") {
      this.ObjectiveID = event[0];
      this.LoadObjectiveKPI(event[0]);

      this.showObjectiveKPIList = true;
      this.showCompetanceKPIList = false;
      this.showObjectiveEntry = false;
      this.showCompetanceEntry = false;
      this.showObjectiveKPIEntry = false;
      this.showCompetanceKPIEntry = false;
      this.showAssesmentEntry = false;
      this.showTab = true;
    }
  }

  LoadObjective(AssesmentID, lang) {
    if (AssesmentID > 0) {
      this.employeeAssesmentService
        .LoadEmployeeObjective(AssesmentID, lang)
        .subscribe((res) => {
          if (res.IsError) {
            alert(res.ErrorMessage + "," + 1);
            return;
          }

          this.allObjCode = res.Data.map(objCode => objCode.code);
          this.ObjectiveCode = !this.allObjCode ? "1" : (this.allObjCode.length + 1).toString();
          console.log('this.ObjectiveCode', this.ObjectiveCode);
          let cols = [
            {HeaderText: "#", DataField: "code"},
            {
              HeaderText: this.GetLocalResourceObject("Name"),
              DataField: "name",
            },
            {
              HeaderText: this.GetLocalResourceObject("Weight"),
              DataField: "weight",
            },
            // {
            //   HeaderText: this.GetLocalResourceObject("Target"),
            //   DataField: "target",
            // },
          ];
          let actions = [
            {
              title: "Edit",
              DataValue: "ID",
              Icon_Awesome: "fa fa-edit",
              Action: "edit",
            },
            {
              title: "Details",
              DataValue: "ID",
              Icon_Awesome: "fa fa-list-alt",
              Action: "Details",
            },
            {
              title: "Delete",
              DataValue: "ID",
              Icon_Awesome: "fa fa-trash",
              Action: "delete",
            },
          ];

          this.ObjectiveDateSource = res.Data;

          this.gvObjective.bind(cols, res.Data, "gvObjective", actions);
        });
    } else {
      //this.gvCompetence.bind();
    }
    // this.ObjectiveAddMode();
  }

  SaveObjective() {
    this.isExistObjectiveCode = this.allObjCode.includes(this.ObjectiveCode)
    if (!this.ObjectiveUpdateMode) {
      if (this.AssesmentID) {
        if (!this.isExistObjectiveCode) {

          this.employeeAssesmentService
            .SaveEmployeeObjective(
              this.ObjectiveCode,
              this.AssesmentID,
              this.userContextService.Username,
              this.ObjectiveName,
              this.ObjectiveNote,
              this.ObjectiveWeight,
              this.ObjectiveProjectID,
              this.ObjectivePositionDescID,
              this.AssesmentKpiCycle,
              this.userContextService.language,
              this.ObjectiveCompetencyID
            )
            .subscribe((res) => {
              if (res.IsError) {
                alert(res.ErrorMessage + "," + 1);
                return;
              }
              // this.ObjectiveAddMode();
              this.LoadObjective(
                this.AssesmentID,
                this.userContextService.language
              );
              // add here
              this.ObjectiveWeight = 0;
              this.showObjectiveEntry = false;
            });
        } else {
          alert("This objective code is exist");
        }
      } else {
        alert(this.GetLocalResourceObject("PleasechooseaAssement"));
      }
    } else {
      this.employeeAssesmentService
        .UpdateEmployeeObjective(
          this.ObjectiveID,
          this.ObjectiveCode,
          this.userContextService.Username,
          this.ObjectiveName,
          this.ObjectiveNote,
          this.ObjectiveWeight,
          this.ObjectiveProjectID,
          this.ObjectivePositionDescID,
          this.userContextService.language,
          this.ObjectiveCompetencyID
        )
        .subscribe((res) => {
          if (res.IsError) {
            alert(res.ErrorMessage + "," + 1);
            return;
          }
          // this.ObjectiveAddMode();
          this.LoadObjective(
            this.AssesmentID,
            this.userContextService.language
          );
          this.showObjectiveEntry = false;
        });
    }
    this.showCompetanceKPIList = false;
    this.showCompetanceKPIEntry = false;
    this.showCompetanceEntry = false;
    this.showObjectiveKPIEntry = false;
    this.showObjectiveKPIList = false;
    this.showAssesmentEntry = false;
    this.showTab = true;
  }

  DeleteObjective(id) {
    this.employeeAssesmentService
      .DeleteEmployeeObjective(id)
      .subscribe((res) => {
        if (res.IsError) {
          alert(res.ErrorMessage + "," + 1);
          return;
        }
        alert(this.GetLocalResourceObject("ObjectiveDeleted"));
        // this.ObjectiveAddMode();
        this.LoadObjective(this.AssesmentID, this.userContextService.language);
      });
  }

  LoadObjectiveByID(id, lang) {
    this.employeeAssesmentService
      .LoadEmployeeObjectiveByID(id, lang)
      .subscribe((res) => {
        if (res.IsError) {
          alert(res.ErrorMessage + "," + 1);
          return;
        }
        this.ObjectiveCode = res.Data.code;
        this.ObjectiveID = res.Data.ID;
        this.ObjectiveName = res.Data.name;
        this.ObjectiveNote = res.Data.note == null ? "" : res.Data.note;
        this.ObjectivePositionDescID = res.Data.pos_desc_id;
        this.ObjectiveProjectID = res.Data.project_id;
        this.ObjectiveCompetencyID = res.Data.objective_competency_id;
        this.ObjectiveWeight = res.Data.weight;
        this.ObjectiveTarget = res.Data.target;
        this.obj_Q1_Target = res.Data.Target1;
        this.obj_Q2_Target = res.Data.Target2;
        this.obj_Q3_Target = res.Data.Target3;
        this.obj_Q4_Target = res.Data.Target4;

        this.ObjectiveUpdateMode = true;
        this.calcMaxOfWieght();
      });
    this.ObjectiveUpdateMode = true;
    this.calcMaxOfWieght();
    /***************************** */
    this.showObjectiveKPIList = true;
    this.showCompetanceKPIList = false;
    this.showObjectiveEntry = true;
    this.showCompetanceEntry = false;
    this.showObjectiveKPIEntry = false;
    this.showCompetanceKPIEntry = false;
  }

  calcMaxOfWieght() {


    let totalWeight = 0;
    if (this.ObjectiveDateSource != null) {
      for (let i = 0; i < this.ObjectiveDateSource.length; i++) {
        totalWeight += this.ObjectiveDateSource[i].weight;
      }

    }
    if (this.ObjectiveUpdateMode) {
      this.ObjectiveMaxWeight = 100 - (totalWeight - this.ObjectiveWeight);
    } else {
      this.ObjectiveMaxWeight = 100 - (totalWeight + this.ObjectiveWeight);
    }
  }

  ObjectiveAddMode() {
    this.calcMaxOfWieght();

    this.ObjectiveCode = !this.allObjCode ? "1" : (this.allObjCode.length + 1).toString();
    this.ObjectiveID = 0;
    this.ObjectiveName = "";
    this.ObjectiveNote = "";
    this.ObjectivePositionDescID = 0;
    this.ObjectiveProjectID = null;
    this.ObjectiveWeight = 0;
    this.ObjectiveTarget = null;

    this.ObjectiveUpdateMode = false;

    this.obj_Q1_Target = 0;
    this.obj_Q2_Target = 0;
    this.obj_Q3_Target = 0;
    this.obj_Q4_Target = 0;
    /***************************** */
    this.showCompetanceKPIList = false;
    this.showCompetanceKPIEntry = false;
    this.showCompetanceEntry = false;
    this.showObjectiveEntry = true;
    this.showObjectiveKPIEntry = false;
    this.showObjectiveKPIList = false;
    this.showAssesmentEntry = false;
    this.showTab = true;
  }

  /**************************************/
  gvObjectiveKPIEvent(event) {
    if (event[1] == "edit") {
      this.LoadObjectiveKPIByID(event[0]);

      this.showCompetanceKPIList = false;
      this.showCompetanceKPIEntry = false;
      this.showCompetanceEntry = false;
      this.showObjectiveEntry = false;
      this.showObjectiveKPIEntry = true;
      this.showObjectiveKPIList = true;
      this.showAssesmentEntry = false;
      this.showTab = true;
    } else if (event[1] == "delete") {
      if (confirm(this.GetLocalResourceObject("Areyousure"))) {
        this.DeleteObjectiveKPI(event[0]);

        this.showCompetanceKPIList = false;
        this.showCompetanceKPIEntry = false;
        this.showCompetanceEntry = false;
        this.showObjectiveEntry = false;
        this.showObjectiveKPIEntry = false;
        this.showObjectiveKPIList = true;
        this.showAssesmentEntry = false;
        this.showTab = true;
      }
    }
  }

  LoadObjectiveKPI(ObjectiveID) {
    if (ObjectiveID > 0) {
      this.employeeAssesmentService
        .LoadEmployeeObjectiveKPI(ObjectiveID)
        .subscribe((res) => {
          if (res.IsError) {
            alert(res.ErrorMessage + "," + 1);
            return;
          }

          const cols = [
            {HeaderText: "#", DataField: "code"},
            {
              HeaderText: this.GetLocalResourceObject("Name"),
              DataField: "name",
            },
            // here
            {
              HeaderText: this.GetLocalResourceObject("Target"),
              DataField: "target",
            }, {
              HeaderText: "KPI Trend",
              DataField: "kpiTrend",
            },
          ];
          const actions = [
            {
              title: "Edit",
              DataValue: "ID",
              Icon_Awesome: "fa fa-edit",
              Action: "edit",
            },
            {
              title: "Delete",
              DataValue: "ID",
              Icon_Awesome: "fa fa-trash",
              Action: "delete",
            },
          ];
          res.Data.forEach(item => {
            item.kpiTrend = item.BetterUpDown === 1 ? 'Up' : 'Down';
          });
          this.gvObjectiveKPI.bind(cols, res.Data, "gvObjectiveKPI", actions);
        });
    } else {
      //this.gvCompetence.bind();
    }
    this.ObjectiveKPIAddMode();
  }

  SaveObjectiveKPI() {
    if (!this.ObjectiveKPIUpdateMode) {
      if (this.ObjectiveID) {
        this.employeeAssesmentService
          .SaveEmployeeObjectiveKPI(
            this.ObjectiveID,
            this.userContextService.Username,
            this.ObjectiveKPIName,
            this.ObjectiveKPITarget,
            this.AssesmentKpiCycle,
            this.objKPI_Q1_Target,
            this.objKPI_Q2_Target,
            this.objKPI_Q3_Target,
            this.objKPI_Q4_Target,
            this.ddlKPIWeight = 0,
            this.ddlTarget_type,
            this.KPITypeId,
            this.userContextService.language,
            this.ddlBetterUpDown
          )
          .subscribe((res) => {
            if (res.IsError) {
              alert(res.ErrorMessage + "," + 1);
              return;
            }
            this.ObjectiveKPIAddMode();
            this.LoadObjectiveKPI(this.ObjectiveID);
            this.calcMaxOfWieght();
            this.ddlKPIWeight = 0;
            this.showObjectiveKPIEntry = false;
          });
      } else {
        alert(this.GetLocalResourceObject("PleasechooseaObjective"));
      }
    } else {
      this.employeeAssesmentService
        .UpdateEmployeeObjectiveKPI(
          this.ObjectiveKPIID,
          this.userContextService.Username,
          this.ObjectiveKPIName,
          this.ObjectiveKPITarget,
          this.ddlKPIWeight,
          this.ddlTarget_type,
          this.KPITypeId,
          this.userContextService.language,
          this.ddlBetterUpDown
        )
        .subscribe((res) => {
          if (res.IsError) {
            alert(res.ErrorMessage + "," + 1);
            return;
          }
          this.ObjectiveKPIAddMode();
          this.LoadObjectiveKPI(this.ObjectiveID);
          this.calcMaxOfWieght();
        });
    }

    this.showCompetanceKPIList = false;
    this.showCompetanceKPIEntry = false;
    this.showCompetanceEntry = false;
    this.showObjectiveEntry = false;
    this.showObjectiveKPIEntry = true;
    this.showObjectiveKPIList = true;
    this.showAssesmentEntry = false;
    this.showTab = true;
  }

  DeleteObjectiveKPI(id) {
    this.employeeAssesmentService
      .DeleteEmployeeObjectiveKPI(id)
      .subscribe((res) => {
        if (res.IsError) {
          alert(res.ErrorMessage + "," + 1);
          return;
        }
        alert(this.GetLocalResourceObject("ObjectiveKPIDeleted"));
        this.ObjectiveKPIAddMode();
        // ObjectiveAddMode()
        this.LoadObjectiveKPI(this.ObjectiveID);
      });
  }

  LoadObjectiveKPIByID(id) {
    this.employeeAssesmentService
      .LoadEmployeeObjectiveKPIByID(id)
      .subscribe((res) => {
        if (res.IsError) {
          alert(res.ErrorMessage + "," + 1);
          return;
        }
        this.ObjectiveKPIID = res.Data.ID;
        this.ObjectiveKPIName = res.Data.name;
        this.ddlTarget_type = res.Data.code;
        this.ObjectiveKPITarget = res.Data.target;
        this.objKPI_Q1_Target = res.Data.Target1;
        this.objKPI_Q2_Target = res.Data.Target2;
        this.objKPI_Q3_Target = res.Data.Target3;
        this.objKPI_Q4_Target = res.Data.Target4;
        this.ddlKPIWeight = res.Data.weight;
        this.KPITypeId = res.Data.KPI_type;
        this.ddlBetterUpDown = res.Data.BetterUpDown;
        this.ObjectiveKPIUpdateMode = true;
      });
    this.ObjectiveKPIUpdateMode = true;

    /***************************** */
    this.showObjectiveKPIList = true;
    this.showCompetanceKPIList = false;
    this.showObjectiveEntry = true;
    this.showCompetanceEntry = false;
    this.showObjectiveKPIEntry = true;
    this.showCompetanceKPIEntry = false;
  }

  ObjectiveKPIAddMode() {
    this.calcMaxOfWieght();
    this.ObjectiveKPIID = 0;
    this.ObjectiveKPIName = "";
    this.ObjectiveKPITarget = null;
    this.ObjectiveKPIUpdateMode = false;
    /***************************** */
    this.showCompetanceKPIList = false;
    this.showCompetanceKPIEntry = false;
    this.showCompetanceEntry = false;
    this.showObjectiveEntry = false;
    this.showObjectiveKPIEntry = true;
    this.showObjectiveKPIList = true;
    this.showAssesmentEntry = false;
    this.showTab = true;
    this.objKPI_Q1_Target = 0;
    this.objKPI_Q2_Target = 0;
    this.objKPI_Q3_Target = 0;
    this.objKPI_Q4_Target = 0;
  }

  /**************************************/
  gvCompetenceEvent(event) {
    if (event[1] == "edit") {
      this.LoadCompetenceByID(event[0]);

      this.showCompetanceKPIList = false;
      this.showCompetanceKPIEntry = false;
      this.showCompetanceEntry = true;
      this.showObjectiveEntry = false;
      this.showObjectiveKPIEntry = false;
      this.showObjectiveKPIList = false;
      this.showAssesmentEntry = false;
      this.showTab = true;
    } else if (event[1] == "delete") {
      if (confirm(this.GetLocalResourceObject("Areyousure"))) {
        this.DeleteCompetence(event[0]);
        this.showCompetanceKPIList = false;
        this.showCompetanceKPIEntry = false;
        this.showCompetanceEntry = false;
        this.showObjectiveEntry = false;
        this.showObjectiveKPIEntry = false;
        this.showObjectiveKPIList = false;
        this.showAssesmentEntry = false;
        this.showTab = true;
      }
    } else if (event[1] == "Details") {
      this.LoadCompetenceByID(event[0]);
      this.LoadCompetenceKPI(event[0]);
      this.selectedcompetenceID = event[0];

      this.showCompetanceKPIList = true;
      this.showCompetanceKPIEntry = false;
      this.showCompetanceEntry = false;
      this.showObjectiveEntry = false;
      this.showObjectiveKPIEntry = false;
      this.showObjectiveKPIList = false;
      this.showAssesmentEntry = false;
      this.showTab = true;
    }
  }

  getCompetenceLevelList() {
    this.codesService
      .LoadCodes(
        this.userContextService.CompanyID,
        7,
        this.userContextService.language
      )
      .subscribe((res) => {
        if (res.IsError) {
          alert(res.ErrorMessage + "," + 1);
          return null;
        }
        this.CompetenceLevelList = res.Data;
      });
  }

  LoadCompetence(AssesmentID) {
    if (AssesmentID > 0) {
      this.employeeAssesmentService
        .LoadEmployeeCompetency(AssesmentID, this.userContextService.language)
        .subscribe((res) => {
          if (res.IsError) {
            alert(res.ErrorMessage + "," + 1);
            return;
          }

          let cols = [
            {HeaderText: "#", DataField: "code"},
            {
              HeaderText: this.GetLocalResourceObject("Competence"),
              DataField: "name",
            },
            {HeaderText: "Weight", DataField: "Weight"},
            {
              HeaderText: this.GetLocalResourceObject("Level"),
              DataField: "CompetencyLevelName",
            },
          ];
          let actions = [
            {
              title: "Edit",
              DataValue: "Id",
              Icon_Awesome: "fa fa-edit",
              Action: "edit",
            },
            {
              title: "Details",
              DataValue: "Id",
              Icon_Awesome: "fa fa-list-alt",
              Action: "Details",
            },
            {
              title: "Delete",
              DataValue: "Id",
              Icon_Awesome: "fa fa-trash",
              Action: "delete",
            },
          ];
          this.CompentenceDataSource = res.Data;

          this.gvCompetence.bind(cols, res.Data, "gvCompetence", actions);
        });
    } else {
    }
    //this.CompetenceAddMode();
  }

  ImportActionPlansToEmployeeObjectives() {
    if (confirm(this.GetLocalResourceObject("Areyousure"))) {
      this.employeeAssesmentService
        .ImportActionPlansToEmployeeObjectives(
          this.AssesmentID,
          this.userContextService.CompanyID
        )
        .subscribe((res) => {
          alert("0");
          this.LoadObjective(
            this.AssesmentID,
            this.userContextService.language
          );
          if (res.IsError) {
            alert(res.ErrorMessage + "," + 1);
            return;
          }
        });
    }
  }

  SaveCompetence() {
    // debugger;
    console.log('save');
    if (!this.CompetenceID) {
      // validate weight
      const totalWeight = this.CompentenceDataSource.reduce((sum, competency) => sum + competency.Weight, 0);
      console.log('totalWeight', totalWeight);

      if (totalWeight + this.CompetenceWeight > 100) {

        alert("Invalid Weight");
        return;
      }

      if (this.AssesmentID) {
        this.employeeAssesmentService
          .SaveEmployeeCompetency(
            this.CompetenceCompetenceID,
            this.CompetenceLevelID,
            this.CompetenceWeight,
            0,
            0,
            this.AssesmentID,
            this.userContextService.Username,
            this.AssesmentKpiCycle,
            this.CompetenceLevelID
          )
          .subscribe((res) => {
            if (res.IsError) {
              alert(res.ErrorMessage + "," + 1);
              return;
            }
            this.CompetenceAddMode();
            this.showCompetanceEntry = false;
            this.LoadCompetence(this.AssesmentID);
          });
      } else {
        alert(this.GetLocalResourceObject("PleasechooseaAssement"));
      }
    } else {
      // validate weight
      const totalWeight = this.CompentenceDataSource.filter(a => a.Id !== this.CompetenceID).reduce((sum, competency) =>
            sum + competency.Weight,
          0
        )
      ;
      console.log("sum", totalWeight);
      console.log("list", this.CompentenceDataSource.filter(a => a.Id !== this.CompetenceID));
      if (totalWeight + this.CompetenceWeight > 100) {
        alert("Invalid Weight");
        return;
      }
      this.employeeAssesmentService
        .UpdateEmployeeCompetency(
          this.CompetenceID,
          this.CompetenceCompetenceID,
          this.CompetenceLevelID,
          this.CompetenceWeight,
          0,
          0,
          this.AssesmentID,
          this.userContextService.Username
        )
        .subscribe((res) => {
          if (res.IsError) {
            alert(res.ErrorMessage + "," + 1);
            return;
          }
          this.CompetenceAddMode();
          this.LoadCompetence(this.AssesmentID);
          this.showCompetanceEntry = false;

        });
    }

    this.showCompetanceKPIList = false;
    this.showCompetanceKPIEntry = false;
    this.showCompetanceEntry = true;
    this.showObjectiveEntry = false;
    this.showObjectiveKPIEntry = false;
    this.showObjectiveKPIList = false;
    this.showAssesmentEntry = false;
    this.showTab = true;
  }

  DeleteCompetence(id) {
    this.employeeAssesmentService
      .DeleteEmployeeCompetency(id)
      .subscribe((res) => {
        if (res.IsError) {
          alert(res.ErrorMessage + "," + 1);
          return;
        }
        alert(this.GetLocalResourceObject("CompetenceDeleted"));
        //this.CompetenceAddMode();
        this.LoadCompetence(this.AssesmentID);
      });
  }

  LoadCompetenceByID(id) {
    this.employeeAssesmentService
      .LoadEmployeeCompetencyByID(id)
      .subscribe((res) => {
        if (res.IsError) {
          alert(res.ErrorMessage + "," + 1);
          return;
        }

        this.ddlCompetenceNatureID = res.Data.CompetenceNature;
        this.fillCompetenceCompetenceList();
        setTimeout(() => {
          this.CompetenceID = res.Data.Id;
          this.CompetenceCompetenceID = res.Data.CompetencyId;
          this.CompetenceWeight = res.Data.Weight;
          this.CompetenceLevelID = res.Data.CompetencyLevelId;

          this.CompetenceUpdateMode = true;
        }, 300);

        //this.LoadCompetenceKPI(res.Data.ID);
      });
    this.CompetenceUpdateMode = true;

    this.fillCompetenceCompetenceList();
    /***************************** */
    this.showObjectiveKPIList = false;
    this.showCompetanceKPIList = true;
    this.showObjectiveEntry = false;
    //this.showCompetanceEntry = true;
    this.showObjectiveKPIEntry = false;
    this.showCompetanceKPIEntry = false;
  }

  CompetenceAddMode() {
    let totalWeight = 0;
    if (this.CompentenceDataSource != null) {
      for (const item of this.CompentenceDataSource) {
        totalWeight += item.Weight;
      }
    }

    this.CompetenceMaxWeight = 100 - totalWeight;
    this.CompetenceWeight = 100 - totalWeight;
    this.CompetenceID = null;
    this.CompetenceCompetenceID = null;
    this.CompetenceUpdateMode = false;
    this.CompetenceLevelID = null;
    // this.fillCompetenceCompetenceList();
    /***************************** */
    this.showCompetanceKPIList = false;
    this.showCompetanceKPIEntry = false;
    this.showCompetanceEntry = true;
    this.showObjectiveEntry = false;
    this.showObjectiveKPIEntry = false;
    this.showObjectiveKPIList = false;
    this.showAssesmentEntry = false;
    this.showTab = true;
  }

  /**************************************/
  gvCompetenceKPIEvent(event) {
    if (event[1] == "edit") {
      this.LoadCompetenceKPIByID(event[0]);

      this.showCompetanceKPIList = true;
      this.showCompetanceKPIEntry = true;
      this.showCompetanceEntry = false;
      this.showObjectiveEntry = false;
      this.showObjectiveKPIEntry = false;
      this.showObjectiveKPIList = false;
      this.showAssesmentEntry = false;
      this.showTab = true;
    } else if (event[1] == "delete") {
      if (confirm(this.GetLocalResourceObject("Areyousure"))) {
        this.DeleteCompetenceKPI(event[0]);

        this.showCompetanceKPIList = true;
        this.showCompetanceKPIEntry = false;
        this.showCompetanceEntry = false;
        this.showObjectiveEntry = false;
        this.showObjectiveKPIEntry = false;
        this.showObjectiveKPIList = false;
        this.showAssesmentEntry = false;
        this.showTab = true;
      }
    }
  }

  LoadCompetenceKPI(CompetenceID) {
    if (CompetenceID > 0) {
      this.employeeAssesmentService
        .LoadEmployeeCompetencyKPI(CompetenceID)
        .subscribe((res) => {
          if (res.IsError) {
            alert(res.ErrorMessage + "," + 1);
            return;
          }

          let cols = [
            {HeaderText: "#", DataField: "code"},
            {
              HeaderText: this.GetLocalResourceObject("KPI"),
              DataField: "CompetencyKpiName",
            },
            {HeaderText: "Target", DataField: "Target"},
          ];
          let actions = [
            {
              title: "Edit",
              DataValue: "Id",
              Icon_Awesome: "fa fa-edit",
              Action: "edit",
            },
            {
              title: "Delete",
              DataValue: "Id",
              Icon_Awesome: "fa fa-trash",
              Action: "delete",
            },
          ];

          this.gvCompetenceKPI.bind(cols, res.Data, "gvCompetenceKPI", actions);
        });
    } else {
    }
    this.CompetenceKPIAddMode();
  }

  SaveCompetenceKPI() {
    if (!this.CompetenceKPIUpdateMode) {
      if (this.CompetenceID) {
        this.employeeAssesmentService
          .SaveEmployeeCompetencyKPI(
            this.CompetenceID,
            this.CompetenceKPITypeID,
            this.userContextService.Username,
            this.AssesmentKpiCycle,
            this.CompetenceKPITarget
          )
          .subscribe((res) => {
            if (res.IsError) {
              alert(res.ErrorMessage + "," + 1);
              return;
            }
            this.CompetenceKPIAddMode();
            this.LoadCompetenceKPI(this.CompetenceID);
            this.showCompetanceEntry = false;
          });
      } else {
        alert(this.GetLocalResourceObject("PleasechooseaCompetence"));
      }
    } else {

      this.employeeAssesmentService
        .UpdateEmployeeCompetencyKPI(
          this.CompetenceKPIID,
          this.CompetenceID,
          this.CompetenceKPITypeID,
          this.CompetenceKPITarget
        )
        .subscribe((res) => {
          if (res.IsError) {
            alert(res.ErrorMessage + "," + 1);
            return;
          }
          this.CompetenceKPIAddMode();
          this.LoadCompetenceKPI(this.CompetenceID);
          this.showCompetanceEntry = false;
        });
    }

    this.showCompetanceKPIList = true;
    this.showCompetanceKPIEntry = true;
    this.showCompetanceEntry = false;
    this.showObjectiveEntry = false;
    this.showObjectiveKPIEntry = false;
    this.showObjectiveKPIList = false;
    this.showAssesmentEntry = false;
    this.showTab = true;
  }

  DeleteCompetenceKPI(id) {
    this.employeeAssesmentService
      .DeleteEmployeeCompetencyKPI(id)
      .subscribe((res) => {
        if (res.IsError) {
          alert(res.ErrorMessage + "," + 1);
          return;
        }
        alert(this.GetLocalResourceObject("CompetenceKPIDeleted"));
        this.CompetenceKPIAddMode();
        this.LoadCompetenceKPI(this.selectedcompetenceID);
      });
  }

  getNatureCodesList() {
    this.codesService
      .LoadCodes(
        this.userContextService.CompanyID,
        1,
        this.userContextService.language
      )
      .subscribe((res) => {
        if (res.IsError) {
          alert(res.ErrorMessage + "," + 1);
          return null;
        }
        this.competenceNatureList = res.Data;
      });
  }

  LoadCompetenceKPIByID(id) {
    this.employeeAssesmentService
      .LoadEmployeeCompetencyKPIByID(id)
      .subscribe((res) => {
        if (res.IsError) {
          alert(res.ErrorMessage + "," + 1);
          return;
        }
        this.fillCompetenceKPIList(this.CompetenceCompetenceID);

        this.CompetenceKPIID = res.Data.Id;
        this.CompetenceKPITypeID = res.Data.EmployeeCompetencyKpiId;
        this.CompetenceKPITarget = res.Data.Target;

        this.CompetenceKPIUpdateMode = true;
      });
    this.CompetenceKPIUpdateMode = true;
    /***************************** */
    this.showObjectiveKPIList = false;
    this.showCompetanceKPIList = true;
    this.showObjectiveEntry = false;
    //this.showCompetanceEntry = true;
    this.showObjectiveKPIEntry = false;
    this.showCompetanceKPIEntry = true;
  }

  CompetenceKPIAddMode() {
    this.CompetenceKPIID = 0;
    this.CompetenceKPITypeID = 0;
    this.CompetenceKPITarget = 5;
    this.CompetenceKPIUpdateMode = false;
    this.fillCompetenceKPIList(this.CompetenceCompetenceID);
    /***************************** */
    this.showCompetanceKPIList = true;
    this.showCompetanceKPIEntry = true;
    this.showCompetanceEntry = false;
    this.showObjectiveEntry = false;
    this.showObjectiveKPIEntry = false;
    this.showObjectiveKPIList = false;
    this.showAssesmentEntry = false;
    this.showTab = true;
  }

  getPositionDesc() {
    //ObjectivePositionDescID
  }

  RefershEmployees() {
    this.fillEmployeeList();
  }

  selectEmployeeEntry() {
    this.AssesmentEmployeeID = this.AssesmentSearchEmployeeID;
    this.loadEmpManager();
  }

  loadEmpManager() {
    this.employeeAssesmentService
      .LoadEmployeeManager(this.AssesmentEmployeeID)
      .subscribe((res) => {
        if (res.IsError) {
          alert(res.ErrorMessage + ",0");
          return;
        }

        this.AssesmentManagerID = this.AssesmentReviewerID = res.Data;
      });
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
        this.LoadAssesment();
      });
  }

  WeightInValid() {
    if (
      this.CompetenceWeight > this.CompetenceMaxWeight ||
      this.CompetenceWeight == 0
    ) {
      return true;
    }
    return false;
  }

  ObjectiveWeightValid() {
    if (
      this.ObjectiveWeight > this.ObjectiveMaxWeight ||
      this.ObjectiveWeight == 0
    ) {
      return true;
    }
    return false;
  }

  changeKPIType(id) {
    this.OrganizationService.CalculateTargetBasedonKPI_Type(
      id,
      this.objKPI_Q1_Target == null ? 0 : this.objKPI_Q1_Target,
      this.objKPI_Q2_Target == null ? 0 : this.objKPI_Q2_Target,
      this.objKPI_Q3_Target == null ? 0 : this.objKPI_Q3_Target,
      this.objKPI_Q4_Target == null ? 0 : this.objKPI_Q4_Target
    ).subscribe((annualTarget) => {
      this.ObjectiveKPITarget = annualTarget;
    });
  }

  betterUpDownChange() {
    console.log(this.ddlBetterUpDown)


  }
}


