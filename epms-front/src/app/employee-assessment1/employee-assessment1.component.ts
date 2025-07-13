import {Component, Input, OnInit} from "@angular/core";
import {EmployeeAssesmentService} from "../Services/employee-assesment.service";
import {CodesService} from "../Services/codes.service";
import {UserContextService} from "../Services/user-context.service";
import {EmployeeService} from "../Organization/employees/employee.service";
import {ProjectsService} from "../Services/Projects/projects.service";
import {UsersService} from "../Services/Users/users.service";
import {PositionsService} from "../Services/positions.service";
import {ActivatedRoute} from "@angular/router";

@Component({
  selector: "app-employee-assessment1",
  templateUrl: "./employee-assessment1.component.html",
  styleUrls: ["./employee-assessment1.component.css"],
  providers: [UsersService],
})
export class EmployeeAssessment1Component implements OnInit {
  empObj = [];
  formRow;
  AddOnFly;
  empCom = [];
  isUpdate: boolean = false;
  showPerformanceRecord: boolean = false;
  datePerformance;
  behaviorPerformance;
  actionPerformance;
  commentPerformance;
  posativePerformance: boolean;
  negativePerformance: boolean;
  objectiveId: any;
  effectPerformance: boolean;
  CompetencyId: any;
  showModal: boolean = false;
  showModalCom: boolean = false;
  competencyPerformance: boolean = false;
  isObjective: boolean = true;
  editPerformance: boolean = false;
  empInfo: any;
  objectiveFinalResultList: any;
  objectiveFinalResulValue: any;
  objectiveFinalResulDesc: any;
  competencyFinalResultList: any;
  competencyFinalResulValue: any;
  competencyFinalResulDesc: any;
  AssessmentResultDesc: any;
  activeClass: boolean;
  activeClassObj: boolean = true;
  activeClassCom: boolean = false;
  ddlSearchEmp;
  ddlSearchUnit;
  ddlSearchYear;
  ddlSearchYearsList;
  ddlSearchUnitsList;
  ddlSearchEmpList;
  allEmployeesList;
  AssesmentID;
  AssessmentStatus;
  AssesmentManagerID;
  AssesmentYearID;
  AssesmentEmployeeID;
  AssesmentKpiCycle;
  KpiCycleList;
  AssesmentReviewerID;
  AssessmentEmpReviewerId;
  AssessmentEmpManagerId;
  AssesmentAgreementDate;
  AssessmentResult;
  AssesmentPositionName;
  // AssessmentTarget;
  objectiveWieght;
  competencyWieght;
  AssessmentColor;
  AssesmentUpdateMode;
  EmployeeObjectivesList;
  EmployeeCompetencyList;
  currentActiveTab: number = 1;
  modificationPermission = false;
  filterQ1: boolean;
  filterQ2: boolean;
  filterQ3: boolean;
  filterQ4: boolean;

  constructor(
    private employeeAssesmentService: EmployeeAssesmentService,
    private codesService: CodesService,
    private route: ActivatedRoute,
    public userContextService: UserContextService,
    private employeeService: EmployeeService,
    private projectsService: ProjectsService,
  ) {
    this.modificationPermission = this.userContextService.RoleId != 5;
    this.loadUnitEmployees();
    this.fillYearList();
    this.fillUnitLst();
    this.fillKPICycleList();
  }

  ngOnInit() {
    // Accessing URL parameters
    this.filterQ1 = true;
    this.filterQ2 = true;
    this.filterQ3 = true;
    this.filterQ4 = true;

  }

  getEmployeeName(employeeId) {
    const employee = this.allEmployeesList.filter(a => a.ID === employeeId)[0];
    return employee ? employee.name1_1 + " " + employee.name1_4 : "";
  }

  activeTabChanged(tab) {

    this.currentActiveTab = tab;
    console.log(tab);


  }

  compInputChange(data) {


    console.log(data.target.value);

  }

  fillKPICycleList() {
    const dd = this.codesService
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
        this.KpiCycleList = res.Data;
      });
  }

  fillUnitLst() {
    // debugger;
    this.projectsService
      .GetUnits(
        this.userContextService.CompanyID,
        this.userContextService.language
      )
      .subscribe((res) => {
        this.ddlSearchUnitsList = this.userContextService.RoleId !== 5 ? res.Data : res.Data.filter(a => {
          return a.ID === this.userContextService.UnitId;
        });
        this.ddlSearchUnit = "";
      });
  }

  loadUnitEmployees() {
    this.employeeService
      .LoadEmplyeeByCompanyID(
        this.userContextService.CompanyID,
        this.userContextService.language,
        this.ddlSearchUnit
      )
      .subscribe((res) => {
        if (res.IsError) {
          alert(res.ErrorMessage + ",0");
          return;
        }
        this.allEmployeesList = res.Data;
        this.ddlSearchEmpList = this.getEmployeeChilderns(res.Data);

      });
  }

  getEmployeeChilderns(employeeList: any) {
    if (this.userContextService.Role === "Employee") {
      const currentEmployee = employeeList.filter(a => a.employee_number === this.userContextService.Username)[0];
      console.log(currentEmployee);
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
      this.ddlSearchYearsList = res.Data;
      if (this.ddlSearchYearsList != null && this.ddlSearchYearsList.length > 0) {
        this.ddlSearchYearsList.forEach((element) => {
          this.ddlSearchYear = element.id;
          this.route.queryParams.subscribe(params => {
            // params is an object containing the query parameters
            if (params['ddlSearchEmp']) {
              this.ddlSearchEmp = params['ddlSearchEmp']; // Accessing the ddlSearchEmp query parameter
              console.log('ddlSearchEmp:', this.ddlSearchEmp);

              this.LoadEmployeeAssessment();
            }
          });

        });

      }
    });
  }

  getColorStyle(color) {
    if (color != undefined) return color;
    else return "white";
  }

  LoadAssesmentByID(id) {
    // debugger;
    this.employeeAssesmentService.LoadAssesmentByID(id).subscribe((res) => {
      if (res.IsError) {
        alert(res.ErrorMessage + "," + 1);
        return;
      }
      console.log("dadata", res.Data);
      this.AssesmentAgreementDate = res.Data.agreement_date;
      this.AssesmentEmployeeID = res.Data.employee_id;
      this.AssesmentID = res.Data.ID;
      this.AssesmentManagerID = res.Data.emp_manager_id;
      this.AssesmentReviewerID = res.Data.emp_reviewer_id;
      this.AssessmentEmpManagerId = res.Data.emp_manager_id;
      this.AssessmentEmpReviewerId = res.Data.emp_reviewer_id;
      this.AssessmentStatus = res.Data.status;
      this.AssesmentKpiCycle = res.Data.c_kpi_cycle;
      this.AssesmentYearID = res.Data.year_id;
      // this.AssessmentResult = res.Data.result_after_round;
      // this.AssessmentTarget = res.Data.target;
      this.AssessmentColor = res.Data.color;
      this.AssesmentUpdateMode = true;
      this.objectiveWieght = res.Data.objectives_weight;
      this.competencyWieght = res.Data.competencies_weight;
      this.getFinalResual();
      this.LoadEmployeeCompetency();
      this.LoadEmployeeObjectives();
    });
  }

  getApprovalActionText() {

    if (this.AssessmentStatus === 2) {
      return "Approve Review Result";
    } else if (this.AssessmentStatus === 3) {
      return "Approve Result";
    } else {
      return "";
    }
  }

  changeAssessmentStatus() {

    const nextStatus = this.AssessmentStatus === 2 ? 3 : this.AssessmentStatus === 3 ? 4 : 0;
    if (nextStatus !== 0)
      this.employeeAssesmentService.ChangeAssessmentStatus(this.AssesmentID, nextStatus).subscribe(
        (res) => {
          if (res.IsError) {
            alert(res.ErrorMessage + "," + 1);
            return;
          }
          // reload assessment
          this.LoadAssesmentByID(this.AssesmentID);
        });
  }

  showApproveAction() {
    const employee = this.ddlSearchEmpList.filter(a => a.employee_number === this.userContextService.Username)[0];
    if (employee) {
      return (employee.ID === this.AssessmentEmpManagerId && this.AssessmentStatus === 2)
        || (employee.ID === this.AssessmentEmpReviewerId && this.AssessmentStatus === 3);
    } else {
      return false;
    }
  }

  LoadEmployeeAssessment() {
    this.AssesmentID = null;
    this.employeeAssesmentService
      .LoadAssesment(this.ddlSearchEmp, this.ddlSearchYear, null
      ).subscribe((res) => {
      if (res.IsError) {
        alert(res.ErrorMessage + "," + 1);
        return;
      }

      console.log('apiiiiiiiii', res.Data);

      var assId = res.Data[0].ID;
      this.AssesmentPositionName = res.Data[0].positionName;
      // this.AssessmentResult = res.Data[0].final_result;
      // this.AssessmentResultDesc = res.Data[0].final_result_desc;
      this.LoadAssesmentByID(assId);
    });
  }

  SaveObjAss(x, empObjID) {
    //save current tab
    this.currentActiveTab = 1;
    this.employeeAssesmentService
      .SaveObjectiveKpiResults(
        x,
        this.userContextService.CompanyID,
        this.userContextService.Username,
        empObjID
      )
      .subscribe((res) => {
        if (res.IsError) {
          alert(res.ErrorMessage + ",1");
          return;
        }
        this.LoadEmployeeObjectives();
      });
  }

  LoadEmployeeObjectives() {
    this.employeeAssesmentService
      .LoadEmployeeObjective(this.AssesmentID, this.userContextService.language,
        this.filterQ1 ? 1 : 0,
        this.filterQ2 ? 1 : 0,
        this.filterQ3 ? 1 : 0,
        this.filterQ4 ? 1 : 0,
      )
      .subscribe((res) => {
        if (res.IsError) {
          alert(res.ErrorMessage + ",1");
          return;
        }
        console.log('EmployeeObjectivesList1', res.Data);

        this.EmployeeObjectivesList = res.Data;
      });
  }

  LoadEmployeeCompetency() {
    this.employeeAssesmentService
      .LoadEmployeeCompetency(
        this.AssesmentID,
        this.userContextService.language
      )
      .subscribe((res) => {
        if (res.IsError) {
          alert(res.ErrorMessage + ",1");
          return;
        }
        console.log('EmployeeCompetencyList', res.Data)
        this.EmployeeCompetencyList = res.Data;
        this.getFinalResual();
      });
  }

  SaveComAss(x, empObjID) {

    //save current tab
    this.currentActiveTab = 2;
    console.log('try to save test');
    console.log("xcxc", x[0]);
    // values must be 1 - > 5 only
    if ((x[0].Q1_A > 5 && x[0].Q1_P != 0) || (x[0].Q2_A > 5 && x[0].Q2_P != 0) || (x[0].Q3_A > 5 && x[0].Q3_P != 0) || (x[0].Q4_A > 5 && x[0].Q4_P != 0)) {
      alert("values must be from 1 to 5");
      return;
    }
    this.employeeAssesmentService
      .SaveCompetencyKpiResults(
        x,
        this.userContextService.CompanyID,
        this.userContextService.Username,
        empObjID
      )
      .subscribe((res) => {
        if (res.IsError) {
          alert(res.ErrorMessage + ",1");
          return;
        }
        this.LoadEmployeeObjectives();
        this.LoadEmployeeAssessment();

        document.getElementById("tab_2").classList.add("active");
        document.getElementById("competency_tab").classList.add("active");
        document.getElementById("objective_tab").classList.remove("active");
      });
  }

  showPerformanceRecordsForObj(id) {
    this.isObjective = true;
    this.competencyPerformance = false;
    this.objectiveId = id;
    this.showModal = true;
    // this.showPerformanceRecord = true;
    this.getEmployeeRecordForObj();
    // this.employeeAssesmentService
    //   .getEmployeeRecordByObjectiveIdAndEmpId(
    //     this.objectiveId,
    //     this.ddlSearchEmp
    //   )
    //   .subscribe((res) => {
    //     this.empObj = res.Data;
    //   });
  }

  showPerformanceRecordsForCom(id) {
    this.isObjective = false;
    this.competencyPerformance = true;
    this.showModal = true;
    this.CompetencyId = id;
    this.getEmployeeRecordForComp();
  }

  savePerformance() {
    if (this.posativePerformance == true) {
      this.effectPerformance = true;
    }
    if (this.negativePerformance == true) {
      this.effectPerformance = false;
    }
    // console.log(this.effectPerformance);
    // console.log(this.posativePerformance);
    // console.log(this.negativePerformance);
    // console.log(
    //   this.ddlSearchEmp,
    //   this.objectiveId,
    //   this.CompetencyId,
    //   this.behaviorPerformance,
    //   this.datePerformance,
    //   this.effectPerformance,
    //   this.actionPerformance,
    //   this.commentPerformance
    // );
    if (!this.competencyPerformance) {
      this.CompetencyId = undefined;
      this.employeeAssesmentService
        .SaveEmployeeRecords(
          this.ddlSearchEmp,
          this.objectiveId,
          this.CompetencyId,
          this.behaviorPerformance,
          this.datePerformance,
          this.effectPerformance,
          this.actionPerformance,
          this.commentPerformance
        )
        .subscribe((res) => {
          this.getEmployeeRecordForObj();
          this.emptyFiles();
        });
    } else {
      this.objectiveId = undefined;
      this.employeeAssesmentService
        .SaveEmployeeRecords(
          this.ddlSearchEmp,
          this.objectiveId,
          this.CompetencyId,
          this.behaviorPerformance,
          this.datePerformance,
          this.effectPerformance,
          this.actionPerformance,
          this.commentPerformance
        )
        .subscribe((res) => {
          this.getEmployeeRecordForComp();
          this.emptyFiles();
        });
    }
  }

  editEmpPerformance(emp) {
    this.isUpdate = true;
    this.empInfo = emp;
    this.editPerformance = true;
    this.behaviorPerformance = emp.Behavior;
    this.datePerformance = emp.PerformanceDate;
    this.effectPerformance = emp.Effect;
    this.actionPerformance = emp.AgreedAction;
    this.commentPerformance = emp.Comment;
  }

  updatePerformance() {
    // console.log(emp);
    // if(this.isObjective){
    //   this.CompetencyId = undefined;
    // }else{
    //   // this.ObjectiveId = undefined;
    // }

    this.employeeAssesmentService
      .updateEmployeeRecord(
        this.empInfo.id,
        this.empInfo.EmpId,
        this.empInfo.ObjectiveId,
        this.empInfo.CompetencyId,
        this.behaviorPerformance,
        this.datePerformance,
        this.effectPerformance,
        this.actionPerformance,
        this.commentPerformance
      )
      .subscribe((res) => {
        this.emptyFiles();
        if (this.competencyPerformance) {
          this.getEmployeeRecordForComp();
        } else {
          this.getEmployeeRecordForObj();
        }
        this.isUpdate = false;
      });
  }

  deleteEmpPerformance(id) {
    this.employeeAssesmentService
      .deleteEmployeePerformanceRecord(id)
      .subscribe((res) => {
        if (this.competencyPerformance) {
          this.getEmployeeRecordForComp();
        } else {
          this.getEmployeeRecordForObj();
        }
      });
  }

  getEmployeeRecordForObj() {
    this.employeeAssesmentService
      .getEmployeeRecordByObjectiveIdAndEmpId(
        this.objectiveId,
        this.ddlSearchEmp
      )
      .subscribe((res) => {
        this.empObj = res.Data;
      });
  }

  getEmployeeRecordForComp() {
    this.employeeAssesmentService
      .getPerformanceByCompatancyId(this.CompetencyId, this.ddlSearchEmp)
      .subscribe((res) => {
        this.empCom = res.Data;
      });
  }

  emptyFiles() {
    this.behaviorPerformance = "";
    this.datePerformance = "";
    this.effectPerformance = true;
    this.actionPerformance = "";
    this.commentPerformance = "";
  }

  updateWieght(objectiveWieght, competencyWieght) {
    if (objectiveWieght + competencyWieght <= 100) {
      this.employeeAssesmentService
        .updateWeightOfEmployee(
          this.AssesmentID,
          objectiveWieght,
          competencyWieght
        )
        .subscribe((res) => {
          this.LoadEmployeeAssessment();
          console.log(res);
        });
    } else {
      alert('The sum of objective weight and competency weight should not be more than 100')
    }
  }

  getFinalResual() {
    this.employeeAssesmentService
      .getFinalResultsForObjective(
        this.AssesmentID,
        this.userContextService.language
      )
      .subscribe((res) => {
        console.log('final data', res.Data)
        this.objectiveFinalResultList = res.Data.data;
        this.objectiveFinalResulValue = res.Data.sumObjFinalResult;
        this.objectiveFinalResulDesc = res.Data.sumObjFinalResultValue;
      });
    this.employeeAssesmentService
      .getFinalResultsForCompetency(
        this.AssesmentID,
        this.userContextService.language
      )
      .subscribe((res) => {
        this.competencyFinalResultList = res.Data.data;
        this.competencyFinalResulValue = res.Data.sumComFinalResult;
        this.competencyFinalResulDesc = res.Data.sumComFinalResultValue;
        setTimeout(() => {
          const value = (this.objectiveFinalResulValue * (this.objectiveWieght / 100))
            + (this.competencyFinalResulValue * (this.competencyWieght / 100));
          console.log('objectiveFinalResulValue', this.objectiveFinalResulValue);
          console.log('objectiveWieght', this.objectiveWieght);
          console.log('competencyFinalResulValue', this.competencyFinalResulValue);
          console.log('competencyWieght', this.competencyWieght);
          this.AssessmentResult = value.toFixed(2);
          this.projectsService.GetFinalResultDescription(value)
            .subscribe(res => this.AssessmentResultDesc = res.Data);
        }, 1000);
      });
  }

  filterQ1Changed(event: any) {
    this.filterQ1 = event.target.checked;
    // this.updateTotalWeightValues();
  }

  filterQ2Changed(event: any) {
    this.filterQ2 = event.target.checked;
    // this.updateTotalWeightValues();


  }

  filterQ3Changed(event: any) {
    this.filterQ3 = event.target.checked;
    // this.updateTotalWeightValues();


  }

  filterQ4Changed(event: any) {
    this.filterQ4 = event.target.checked;
    //this.updateTotalWeightValues();


  }

}
