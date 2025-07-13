import {
  Component,
  OnInit,
  Input,
  Output,
  EventEmitter,
  ViewChild,
  ElementRef,
} from "@angular/core";
import {Projects} from "../OrganizationObjectives/projects/projects";
import {FormGroup} from "@angular/forms";
import {ProjectsService} from "../Services/Projects/projects.service";
import {UsersService} from "../Services/Users/users.service";
import {UserContextService} from "../Services/user-context.service";
import {OrganizationService} from "../Organization/organization.service";
import {EmployeeService} from "../Organization/employees/employee.service";
import {ActionPlanService} from "./action-plan.service";
import {THIS_EXPR} from "@angular/compiler/src/output/output_ast";
import {ProjectsAssessmentService} from "../Services/projects-assessment.service";

@Component({
  selector: "app-action-plan",
  templateUrl: "./action-plan.component.html",
  styleUrls: ["./action-plan.component.css"],
  providers: [
    OrganizationService,
    EmployeeService,
    ProjectsService,
    ActionPlanService,
  ],
})
export class ActionPlanComponent implements OnInit {
  is_reveiwer_user;
  planned_status;
  unites;
  unite: any;
  actualWeight: number;
  plannedWeight: number;
  notShowActual: boolean;
  showWeight: boolean;
  projectAssessmentList: any;

  getApprovals() {
    this.OrganizationService.GetApprovalSetupByURL(
      window.location.hash,
      this.userContext.CompanyID
    ).subscribe((res) => {
      if (res.IsError) {
        this.is_reveiwer_user = false;
        return;
      }
      this.is_reveiwer_user = res.Data.filter(a => a.reviewing_user.toLocaleLowerCase() === this.userContext.Username.toLocaleLowerCase()).length > 0;
    });
  }

  constructor(
    private empService: EmployeeService,
    private userContext: UserContextService,
    private projService: ProjectsService,
    private OrganizationService: OrganizationService,
    private actionPlansService: ActionPlanService,
    private projectsAssessmentService: ProjectsAssessmentService,
    private userService: UsersService
  ) {
    this.getApprovals();
    this.userService
      .GetLocalResources(
        window.location.hash,
        this.userContext.CompanyID,
        this.userContext.language
      )
      .subscribe((res) => {
        if (res.IsError) {
          alert(res.ErrorMessage + "," + 1);
          return;
        }
        this.PageResources = res.Data;
      });

    this.LoadYears();
    this.loadEmployees();
  }

  GetLocalResourceObject(resourceKey) {
    for (let i = 0; i < this.PageResources.length; i++) {
      if (this.PageResources[i].resource_key === resourceKey) {
        return this.PageResources[i].resource_value;
      }
    }
  }

  //#region Action Plans Entry Form
  txtActionPlanName;
  txtActionPlanNotes;
  ddlProjectKPIEntry;
  ddlEmployeesEntry;
  txtActionReq;
  txtActionCost;
  txtActionWeight;
  txtActionDate;
  ShowEntry = false;
  showTabs = false;
  selectedPlanID;
  //#endregion

  //#region  Form properities starts here
  @Input("AddOnFly") AddOnFly = false;

  @Output()
  AddOnFlyActionPlans = new EventEmitter<string>();

  @ViewChild("EntryPanel", {static: false}) myDivRef: ElementRef;
  @ViewChild("gvActionPlans", {read: false, static: false}) gvActionPlans;
  ddlEmployees;
  EmployeesList;
  ddlProjectsSearch;
  ProjectsList;
  ddlYear;
  yearLst;
  ddlProjectKPISearch;
  ProjectKPIList;
  ShowList = true;
  PageResources;

  //#endregion Form properities Ends here

  //#region  Form Setup Methods Starts

  AddOnFlyValues(year, projectID) {
    this.ddlProjectsSearch = projectID;
    this.ddlYear = year;

    this.LoadPorjectKPIs();
    this.SearchActionPlans();
  }

  loadEmployees() {
    this.empService
      .SearchEmployees(
        "",
        "",
        this.unite > 0 ? this.unite : "",
        "",
        "",
        this.userContext.language,
        this.userContext.CompanyID
      )
      .subscribe((res) => {
        if (res.IsError) {
          alert(res.ErrorMessage + "," + 1);
          return;
        }
        this.EmployeesList = res.Data;
      });
  }

  LoadProjects() {
    this.projService
      .SearchAllProjectsList(
        this.userContext.CompanyID,
        this.userContext.BranchID,
        this.ddlYear,
        this.unite > 0 ? this.unite : 0,
        0,
        this.userContext.language
      )
      .subscribe((res) => {
        if (res.IsError) {
          alert(res.ErrorMessage + "," + 1);
          return;
        }
        this.ProjectsList = this.userContext.RoleId !== 5 ? res.Data : res.Data.filter(a => {
          return a.UnitId === this.userContext.UnitId;
        });
        ;
      });
  }

  LoadYears() {
    this.projService.GetYears().subscribe((res) => {
      this.yearLst = res.Data;

      if (this.yearLst != null && this.yearLst.length > 0) {
        this.yearLst.forEach((element) => {
          this.ddlYear = element.id;
          this.ddlYearChange();
        });
      }
    });
  }

  LoadPorjectKPIs() {
    this.OrganizationService.getObjectiveKpiByObjective(
      this.ddlProjectsSearch,
      this.userContext.language
    ).subscribe((res) => {
      if (res.IsError) {
        alert(res.ErrorMessage + "," + 1);
        return;
      }
      this.ProjectKPIList = res.Data;
    });
  }

  ResetEntryForm() {
    this.txtActionPlanName = "";
    this.txtActionPlanNotes = "";
    this.ddlProjectKPIEntry = null;
    this.ddlEmployeesEntry = null;
    this.txtActionReq = "";
    this.txtActionCost = "";
    this.txtActionWeight = "";
    this.txtActionDate = "";
    this.selectedPlanID = "";
  }

  //#endregion Form Setup Methods Ends

  //#region  Form Events Starts

  ddlYearChange() {
    let year: number;
    this.ddlYear == null ? (year = -1) : (year = this.ddlYear);
    if (this.ddlYear == -1) {
      return;
    }
    this.LoadProjects();
  }

  unitChange() {
    this.LoadProjects();
    this.loadEmployees();
  }

  LoadKpis() {
    this.LoadPorjectKPIs();
  }

  SearchActionPlans() {
    this.ShowEntry = false;
    this.actionPlansService
      .GetProjectActionPlans(
        this.ddlProjectsSearch,
        this.ddlProjectKPISearch,
        this.userContext.language,
        this.ddlEmployees,
        this.unite
      )
      .subscribe((res) => {
        if (res.IsError) {
          alert(res.ErrorMessage + "," + 1);
          return;
        }

        let cols = [
          {HeaderText: "Project", DataField: "ProjectName"},
          {HeaderText: "Unit", DataField: "Unit_Name"},
          {HeaderText: "KPI", DataField: "KpiName"},
          {HeaderText: "Activity Name", DataField: "PlanName"},
          {HeaderText: "Requirement", DataField: "req"},
          {HeaderText: "Employee", DataField: "EmpName"},
          {HeaderText: "Activity Date", DataField: "planDate"},
          {HeaderText: "Weight %", DataField: "planWeight"},
          {HeaderText: "Activity Cost", DataField: "planCost"},
          {HeaderText: "Activity Status", DataField: "planned_status"},
        ];
        let actions = [
          {
            title: "Edit",
            DataValue: "ID",
            Icon_Awesome: "fa fa-edit",
            Action: "edit",
          },
          {
            title: "KPI Details",
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

        this.gvActionPlans.bind(cols, res.Data, "gvActionPlans", actions);
      });
    this.searchProjectsAssessment();

    this.projectsAssessmentService
      .GetPlannedAndActualWeightForAll(
        this.userContext.CompanyID,
        this.ddlYear,
        this.unite,
        0,
        this.userContext.language,
        null,
        this.ddlProjectsSearch,
      )
      .subscribe((res) => {
        if (res.IsError == false) {
          this.showWeight = true;
          this.plannedWeight = res.Data.PlannedWeights;
          this.actualWeight = res.Data.ActualWeights;
        } else {
          this.showWeight = false;
          alert(res.ErrorMessage);
        }
      });

  }

  remainingPlanWeight;
  txtPlanweight;

  AddActionPlan() {
    this.ResetEntryForm();
    this.ShowEntry = true;
    this.showTabs = false;
    this.ddlEmployeesEntry = this.ddlEmployees;
    this.ddlProjectKPIEntry = this.ddlProjectKPISearch;
    this.getActionPlanRemainingWeight(this.ddlProjectsSearch);
  }

  onKPI_planEntryChange() {
    this.getActionPlanRemainingWeight(this.ddlProjectsSearch);
  }

  SaveActionPlan() {
    this.actionPlansService
      .GetProjectPlannedCost(this.ddlProjectsSearch, this.selectedPlanID)
      .subscribe((res) => {
        if (res.IsError) {
          alert(res.ErrorMessage + "," + 1);
          return;
        }
        let totalActionsCost = res.Data.actionPlansCost;
        let projectPlannedCost = res.Data.PlannedCost;
        let currentPlancost = this.txtActionCost - res.Data.planCost;

        if (totalActionsCost + currentPlancost > projectPlannedCost) {
          alert(
            "Action plan cost should not be more than project planned budget = " +
            projectPlannedCost +
            "," +
            1
          );
          return false;
        }

        if (this.selectedPlanID == "") {
          this.actionPlansService
            .SaveProjectActionPlans(
              this.ddlProjectsSearch,
              this.txtActionCost,
              this.userContext.Username,
              this.txtActionDate,
              this.txtActionPlanName,
              this.txtActionPlanNotes,
              this.txtActionReq,
              this.txtActionWeight,
              this.ddlEmployeesEntry,
              this.ddlProjectKPIEntry
            )
            .subscribe((res) => {
              if (res.IsError) {
                alert(res.ErrorMessage + "," + 1);
                return;
              }
              alert("0");
              this.SearchActionPlans();
              this.ResetEntryForm();
              this.ShowEntry = false;
              currentPlancost = 0;
            });
        } else {
          this.actionPlansService
            .UpdateProjectActionPlans(
              this.selectedPlanID,
              this.ddlProjectsSearch,
              this.txtActionCost,
              this.userContext.Username,
              this.txtActionDate,
              this.txtActionPlanName,
              this.txtActionPlanNotes,
              this.txtActionReq,
              this.txtActionWeight,
              this.ddlEmployeesEntry,
              this.ddlProjectKPIEntry
            )
            .subscribe((res) => {
              if (res.IsError) {
                alert(res.ErrorMessage + "," + 1);
                return;
              }
              alert("0");
              this.SearchActionPlans();
              this.ResetEntryForm();
              this.ShowEntry = false;
              currentPlancost = 0;
            });
        }
        this.selectedPlanID = "";
      });
  }

  gvActionPlansEvent(event) {
    if (event[1] == "edit") {
      this.selectedPlanID = event[0];
      this.LoadActionPlan();
    } else if (event[1] == "delete") {
      this.OrganizationService.GetActionPlanStatus(event[0]).subscribe(
        (res) => {
          if (res.IsError) {
            alert(res.ErrorMessage + ",1");
            return;
          }

          if (
            res.Data.planned_status == 1 ||
            (this.is_reveiwer_user && res.Data.planned_status == 3)
          ) {
            if (confirm("Are you sure to delete this plan?")) {
              this.ShowEntry = false;
              this.deleteActionPlan(event[0]);
            }
          }
        }
      );
    } else if (event[1] == "Details") {
      this.ShowEntry = false;
      this.showTabs = true;
      this.selectedPlanID = event[0];
      this.actionPlansService
        .GetProjectActionPlansByID(this.selectedPlanID)
        .subscribe((res) => {
          if (res.IsError) {
            alert(res.ErrorMessage + "," + 1);
            return;
          }
          this.lblSelectedPlanName = res.Data.action_name;
          this.LoadActionPlanKPIs();
        });
    }
  }

  deleteActionPlan(id) {
    this.actionPlansService.DeleteActionPlan(id).subscribe((res) => {
      if (res.IsError) {
        alert(res.ErrorMessage + "," + 1);
        return;
      }
      this.SearchActionPlans();
      alert("0");
    });
  }

  LoadActionPlan() {
    this.OrganizationService.GetActionPlanStatus(this.selectedPlanID).subscribe(
      (res) => {
        if (res.IsError) {
          alert(res.ErrorMessage + ",1");
          return;
        }
        this.planned_status = res.Data.planned_status;
      }
    );

    this.showTabs = false;
    this.ShowEntry = true;

    this.actionPlansService
      .GetProjectActionPlansByID(this.selectedPlanID)
      .subscribe((res) => {
        if (res.IsError) {
          alert(res.ErrorMessage + "," + 1);
          return;
        }

        this.txtActionCost = res.Data.action_cost;
        this.txtActionDate = res.Data.action_date;
        this.txtActionPlanName = res.Data.action_name;
        this.txtActionPlanNotes = res.Data.action_notes;
        this.txtActionReq = res.Data.action_req;
        this.txtActionWeight = res.Data.action_weight;
        this.ddlEmployeesEntry = res.Data.emp_id;
        this.ddlProjectKPIEntry = res.Data.project_kpi_id;
        this.remainingPlanWeight = res.Data.max;
      });
  }

  //#endregion Form Events Ends

  ngOnInit() {
    this.empService
      .GetUnites(this.userContext.CompanyID, this.userContext.language)
      .subscribe((res) => {
        this.unites = this.userContext.RoleId !== 5 ? res.Data : res.Data.filter(a => {
          return a.ID === this.userContext.UnitId;
        });


      });
  }

  ngAfterViewInit() {
  }

  getActionPlanRemainingWeight(prjectID) {
    this.actionPlansService
      .getActionPlanRemainingWeight(prjectID, this.ddlProjectKPIEntry)
      .subscribe((res) => {
        if (res.IsError) {
          alert(res.ErrorMessage + "," + 1);
          this.remainingPlanWeight = 0;
        }
        this.remainingPlanWeight = res.Data;
        if (this.selectedPlanID == "") {
          this.txtActionWeight = this.remainingPlanWeight;
        }
      });
  }

  //#region  KPIs Related code starts here
  @ViewChild("gvKPIs", {read: false, static: false}) gvKPIs;
  showKpiEntry = false;
  remainingKPIWeight;
  txtKpiweight;
  // showTabs = false;
  txtKpiName;
  txtKpiDesc;
  txtKpiTarget;
  ddlKpiBSC;
  ddlKpiMeasurement;
  ddlObjKpiBSCSearch = "";
  selectedActionPlanKpiId;
  lblSelectedPlanName;
  EditMode = false;

  ActioPlanKpiAdd() {
    this.getActionPlanKPIRemainingWeight(this.selectedPlanID);
    this.resetActionPlanKpiEntry();
    this.showKpiEntry = true;
  }

  getActionPlanKPIRemainingWeight(panID) {
    this.OrganizationService.getObjectiveKPIRemainingWeight(panID, 3).subscribe(
      (res) => {
        if (res.IsError) {
          alert(res.ErrorMessage + "," + 1);
          this.remainingKPIWeight = 0;
        }
        this.remainingKPIWeight = res.Data;
        if (!this.EditMode) {
          this.txtKpiweight = this.remainingKPIWeight;
        }
      }
    );
  }

  LoadActionPlanKPIs() {
    this.OrganizationService.getObjectiveKpiByObjective(
      this.selectedPlanID,
      this.userContext.language,
      this.ddlObjKpiBSCSearch
    ).subscribe((res) => {
      if (res.IsError) {
        alert(res.ErrorMessage + "," + 1);
        return;
      }
      this.resetActionPlanKpiEntry();
      let cols = [
        {
          HeaderText: this.GetLocalResourceObject("colKpiName"),
          DataField: "Name",
        },
        {
          HeaderText: this.GetLocalResourceObject("colKpiTarget"),
          DataField: "TargetDesc",
        },
        {
          HeaderText: this.GetLocalResourceObject("colKpiWeight"),
          DataField: "WeightDesc",
        },
        {
          HeaderText: this.GetLocalResourceObject("lblBSC"),
          DataField: 'BSC_Name',
        },
      ];
      let actions = [
        {
          title: this.GetLocalResourceObject("lblEdit"),
          DataValue: "ID",
          Icon_Awesome: "fa fa-edit",
          Action: "edit",
        },
        {
          title: this.GetLocalResourceObject("lblDelete"),
          DataValue: "ID",
          Icon_Awesome: "fa fa-trash",
          Action: "delete",
        },
      ];

      this.gvKPIs.bind(cols, res.Data, "gvKPIs", actions);
    });
  }

  resetActionPlanKpiEntry() {
    this.txtKpiDesc = "";
    this.txtKpiName = "";
    this.txtKpiTarget = "";
    this.txtKpiweight = "";
    this.ddlKpiBSC = null;
    this.ddlKpiMeasurement = null;
    this.selectedActionPlanKpiId = null;
    this.EditMode = false;
    this.showKpiEntry = false;
  }

  refreshObjKpisList() {
    this.LoadActionPlanKPIs();
  }

  gvKPIsHandler(event) {
    if (event[1] == "edit") {
      this.prepareObjecitveKpiForEdit(event[0]);
    } else if (event[1] == "delete") {
      this.OrganizationService.GetActionPlanStatus(
        this.selectedPlanID
      ).subscribe((res) => {
        if (res.IsError) {
          alert(res.ErrorMessage + ",1");
          return;
        }

        if (
          res.Data.planned_status == 1 ||
          (this.is_reveiwer_user && res.Data.planned_status == 3)
        ) {
          if (confirm(this.GetLocalResourceObject("AreYouSure"))) {
            this.OrganizationService.DeleteObjectiveKpi(event[0]).subscribe(
              (res) => {
                if (res.IsError) {
                  alert(res.ErrorMessage + "," + 1);
                  return;
                }
                this.LoadActionPlanKPIs();
              }
            );
          }
        }
      });
    }
  }

  prepareObjecitveKpiForEdit(kpiID) {
    this.OrganizationService.getObjectiveKpiByID(
      kpiID,
      this.userContext.language
    ).subscribe((res) => {
      if (res.IsError) {
        alert(res.ErrorMessage + "," + 1);
        return;
      }
      this.EditMode = true;
      this.txtKpiDesc = res.Data.Description;
      this.txtKpiName = res.Data.Name;
      this.txtKpiTarget = res.Data.Target;
      this.txtKpiweight = res.Data.Weight;
      this.ddlKpiBSC = res.Data.bsc;
      this.ddlKpiMeasurement = res.Data.measurement;
      this.selectedActionPlanKpiId = kpiID;
      this.remainingKPIWeight = res.Data.max;
      this.showKpiEntry = true;
    });
  }

  SaveObjectiveKpi() {
    if (this.txtKpiweight > this.remainingKPIWeight || this.txtKpiweight < 1) {
      alert("InvalidWeight" + "," + 1);
      return;
    }
    if (
      this.selectedActionPlanKpiId != null ||
      this.selectedActionPlanKpiId != undefined
    ) {
      this.OrganizationService.UpdateObjectiveKpi(
        this.selectedActionPlanKpiId,
        this.txtKpiName,
        "",
        this.txtKpiDesc,
        this.selectedPlanID,
        this.txtKpiweight,
        this.txtKpiTarget,
        this.ddlKpiBSC,
        this.ddlKpiMeasurement,
        this.userContext.CompanyID,
        this.userContext.BranchID,
        this.userContext.Username,
        this.userContext.language,
        1,
        1,
        1
      ).subscribe((res) => {
        if (res.IsError) {
          alert(res.ErrorMessage + "," + 1);
          return;
        }
        this.LoadActionPlanKPIs();
        this.resetActionPlanKpiEntry();
      });
    } else {
      this.OrganizationService.SaveObjectiveKpi(
        this.txtKpiName,
        "",
        this.txtKpiDesc,
        this.selectedPlanID,
        this.txtKpiweight,
        this.txtKpiTarget,
        this.ddlKpiBSC,
        this.ddlKpiMeasurement,
        this.userContext.CompanyID,
        this.userContext.BranchID,
        this.userContext.Username,
        this.userContext.language,
        3,
        1,
        1,
        1
      ).subscribe((res) => {
        if (res.IsError) {
          alert(res.ErrorMessage + "," + 1);
          return;
        }
        this.LoadActionPlanKPIs();
        this.resetActionPlanKpiEntry();
      });
    }
  }

  ShowObjectiveDetails() {
    this.showTabs = true;
    setTimeout(() => {
      this.actionPlansService
        .GetProjectActionPlansByID(this.selectedPlanID)
        .subscribe((res) => {
          if (res.IsError) {
            alert(res.ErrorMessage + "," + 1);
            return;
          }
          this.lblSelectedPlanName = res.Data.action_name;
          this.LoadActionPlanKPIs();
        });
    }, 500);
  }

  //#endregion KPIs

  ConfirmProject() {
    this.OrganizationService.UpdateActionStatus(
      this.selectedPlanID,
      2
    ).subscribe((res) => {
      if (res.IsError) {
        alert(res.ErrorMessage + ",1");
        return;
      }
      alert("0");
      this.SearchActionPlans();
      this.ResetEntryForm();
      this.ShowEntry = false;
    });
  }

  DeclineProject() {
    this.OrganizationService.UpdateActionStatus(
      this.selectedPlanID,
      3
    ).subscribe((res) => {
      if (res.IsError) {
        alert(res.ErrorMessage + ",1");
        return;
      }
      alert("0");
      this.SearchActionPlans();
      this.ResetEntryForm();
      this.ShowEntry = false;
    });
  }

  searchProjectsAssessment() {
    this.actualWeight = 0;
    this.plannedWeight = 0;
    this.notShowActual = true;
    // this.projectsAssessmentService
    //   .getProjectAssessment(
    //     this.userContext.CompanyID,
    //     this.ddlYear,
    //     this.unite,
    //     0,
    //     this.userContext.language
    //   )
    //   .subscribe((res) => {
    //     if (res.IsError) {
    //       alert(res.ErrorMessage);
    //       return;
    //     }
    //     // debugger;
    //     console.log(111, res);
    //     this.showWeight = true;
    //     this.projectAssessmentList = res.Data;

    //     this.projectAssessmentList.forEach((element) => {
    //       this.actualWeight += element.projectPercentageFromEntireStratigic;
    //     });
    //     this.plannedWeight = res.Data[0].PlannedWeights;
    //   });
  }
}
