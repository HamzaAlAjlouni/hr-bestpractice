import {
  Component,
  OnInit,
  ViewChild,
  Input,
  Output,
  EventEmitter,
  AfterViewInit,
  ElementRef,
} from "@angular/core";
import {Projects} from "./projects";
import {
  FormGroup,
  FormControl,
  Validators,
  FormBuilder,
} from "@angular/forms";
import {ProjectsService} from "src/app/Services/Projects/projects.service";
import {UserContextService} from "src/app/Services/user-context.service";
import {UsersService} from "../../Services/Users/users.service";
import {OrganizationService} from "../../Organization/organization.service";
import {element} from "protractor";
import {ProjectsAssessmentService} from "src/app/Services/projects-assessment.service";
import {log} from "util";
import {ProjectEvaluationService} from "../../Services/project-evaluation.service";
import {ProjectCalculationSetupService} from "../../Services/project-calculation-setup-entity.service";
import {ActivatedRoute} from "@angular/router";

// tslint:disable-next-line:no-trailing-whitespace
@Component({
  selector: "app-projects",
  templateUrl: "./projects.component.html",
  styleUrls: ["./projects.component.css"],
  providers: [OrganizationService],
})
/* tslint:disable */
export class ProjectsComponent implements OnInit, AfterViewInit {
  Type: any;
  formRow;
  @ViewChild("ActionPlanModal", {read: false, static: false}) ActionPlanModal;
  @Input()
  AddOnFly = false;
  listKPI: any;
  @Output()
  AddOnFlyProjectSave = new EventEmitter<string>();

  ShowProjectList = false;

  @ViewChild("gvProjects", {read: false, static: false}) gvProjects;
  @ViewChild("gvDocuments", {read: false, static: false}) gvDocuments;

  oProjects: Projects;
  registerForm: FormGroup;
  submitted = false;
  Helper: ProjectsService;
  ShowEntryPnl: boolean;
  ddlYear: any;
  ddlYearEntry;
  ddlUnit: any;
  ddlProjectStatus;
  ddlCategory: any;
  @Input()
  approvalNote: any;

  invalidValidation: boolean;
  ddlStratigicObj = 0;

  ddlStratigicObjKpi = 0;
  ddlProject: any;
  yearLst: Array<{ id: number; year: number; isActive: boolean }>;
  yearEntryLst: Array<{ id: number }>;
  stratigicObjectivLst: Array<{ id: number; Name: string }>;
  stratigicObjectivEntryLst: Array<{ id: number; Name: string }>;
  unitLst: Array<{ ID: number; NAME: string }>;
  KpiTypeLst: Array<{ MINOR_NO: number; NAME: string }>;
  filteredKpiTypeLst: Array<{ MINOR_NO: number; NAME: string }>;
  BetterUpDownList = [
    {id: 1, name: "Better Up"},
    {id: 2, name: "Better Down"}

  ];
  KpiCycleLst: Array<{ MINOR_NO: number; NAME: string }>;
  BranchLst: Array<{ ID: number; NAME: string }>;
  ResultUnitLst: Array<{ MINOR_NO: number; NAME: string }>;
  projectLst: Array<{ id: number; Name: string }>;
  selectedId;
  EditMode: boolean;

  AddButton_Enabled = false;
  PageResources = [];
  projectsCategories: Array<{ ID: number; NAME: string }>;
  ddlP_type;
  remainingProjectsWeight = 1;
  chkIsProcess = false;

  txtDocumentName;

  selectDocID;
  DocEditMode = false;
  ObjectiveTitleForSelectedProject = "";
  projectsList = [];
  projectsFilteredList = [];
  plannedWeight: number = 0;
  notPlannedWeight: number = 0;
  @ViewChild("EntryPanel", {static: false}) myDivRef: ElementRef;

  is_reveiwer_user = false;
  listAllKpis = [];
  projectKpis = [];
  selectedKpi: number;
  editHandler: boolean = false;
  txtKpiweightOld: any;
  objectiveSelected;
  showPlannedWeight: boolean = false;
  evaluationsList: any[];
  evaluationValuesList: number[] = [];
  weightCalculation; /* 1=> kpi , 2=> objective  */
  getApprovals() {
    this.OrganizationService.GetApprovalSetupByURL(
      window.location.hash,
      this.userContext.CompanyID,
    ).subscribe((res) => {
      if (res.IsError) {
        this.is_reveiwer_user = false;
        return;
      }
      this.is_reveiwer_user = res.Data.filter(a => a.reviewing_user.toLocaleLowerCase() === this.userContext.Username.toLocaleLowerCase()).length > 0;
    });
  }

  constructor(
    private ProjService: ProjectsService,
    private route: ActivatedRoute,
    private evaluationService: ProjectEvaluationService,
    private userContext: UserContextService,
    private userService: UsersService,
    private OrganizationService: OrganizationService,
    private projectCalculationSetupService: ProjectCalculationSetupService,
    private projectsAssessmentService: ProjectsAssessmentService
  ) {
    this.ShowEntryPnl = false;
    this.Helper = ProjService;
    this.oProjects = new Projects();
    this.fillUnitLst();
    // this.GetProjectsCategories();
    this.getEvaluations();
    this.fillYears();
    this.fillKpiType();
    this.fillKpiCycle();
    this.fillResultUnit();
    this.fillStratigicObjective(this.userContext.CompanyID, -1);
    this.getApprovals();
    this.selectedId = null;
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
  }

  getEvaluations(type = 1) {
    this.evaluationService.GetProjectEvaluation("", type).subscribe(res => {

      if (res.IsError) {
        alert(res.ErrorMessage + ',' + 1);
        return;
      }
      this.evaluationsList = res.Data;
    });


  }

  GetLocalResourceObject(resourceKey) {
    for (let i = 0; i < this.PageResources.length; i++) {
      if (this.PageResources[i].resource_key === resourceKey) {
        return this.PageResources[i].resource_value;
      }
    }
  }

  setAddOnFlyDefaults(year, stratigicID, showPanel = false) {
    this.ShowProjectList = false;
    this.ddlStratigicObj = stratigicID;
    this.selectedId = null;
    this.EditMode = false;
    this.AddProject();
    this.ddlYearEntry = parseInt(year);
    this.oProjects.StratigicObjectiveId = stratigicID;

    this.ShowEntryPnl = showPanel;
    this.AddButton_Enabled = true;
    this.getProjectsRemainingWeight(this.weightCalculation == 1 ? this.oProjects.KPI : this.oProjects.StratigicObjectiveId);
  }

  setOnFlyWithShowProjectsList() {
    this.ShowProjectList = true;
    this.GetProjects();
    this.AddButton_Enabled = true;
  }

  ngOnInit() {
    this.route.fragment.subscribe(fragment => {
      if (fragment) {
        this.scrollTo(fragment);
      }
    });
    this.getProjectCalculationMethod();
    //this.GetProjects()
    //this.fillddlProject()
    //  this.GetProjectsCategories()

  }

  scrollTo(elementId: string): void {
    const element = document.getElementById(elementId);
    if (element)
      element.scrollIntoView({behavior: 'smooth', block: 'start', inline: 'start'});
  }

  getProjectCalculationMethod() {

    this.projectCalculationSetupService.get().subscribe(res => {

      if (res.IsError) {
        alert('' + res.ErrorMessage);
        return;
      } else {

        this.weightCalculation = res.Data[0].Calculation;
      }

    });

  }

  ngAfterViewInit() {
  }

  caculateWeight(weight) {
    /// get sum of projects weight
    console.log(weight)
    console.log(this.selectedKpi)
    console.log(this.projectsList.filter(a => {
      return Number(a.KPI) == this.selectedKpi
    }))

    //
    // console.log('projectsList', this.projectsList.filter(a => {
    //   return Number(a.KPI) == this.selectedKpi
    // }))
    // console.log('listAllKpis', this.listAllKpis)
    let totalValue =
      this.projectsList.filter(a => {
        return Number(a.KPI) == this.selectedKpi
      }).length > 0 ?
        this.projectsList.filter(a => {
          return Number(a.KPI) == this.selectedKpi
        })
          .map((item) => Number(item.KPI))
          .reduce((sum, current) => sum + current) : 0;


    //
    // let sum = this.projectsList.reduce((accumulator, object) => {
    //   // console.log("object", object)
    //   // console.log("KPI", this.selectedKpi)
    //   if (this.selectedKpi) {
    //     if (this.selectedKpi == Number(object.KPI))
    //       return accumulator + (object.Weight);
    //     else return 0;
    //   } else return 0;
    // }, 0);

    // console.log("sum", totalValue)

    const result = totalValue == 0 ? 1 : (weight / totalValue);
    return (result * 100).toFixed(2);


  }

  caculateWeightForList(obj) {
    /// get sum of projects weight

    let kpiProjects = this.projectsList.filter(a => {
      return a.KPI == obj.KPI
    })
    let sum = kpiProjects.reduce((accumulator, object) => {
      return accumulator + (object.Weight);

    }, 0);


    return ((obj.Weight / sum) * 100).toFixed(2);


  }

  caculateWeightForTotal() {
    /// get sum of projects weight


     const sum = this.projectsList.filter(a => {
        return a.planned_status != "Declined"
      }).reduce((accumulator, object) => {
        return accumulator + object.plannedStratigy;
      }, 0);


    return Number(sum.toFixed(2));


  }

  caculateNotPlannedWeightForTotal() {
    return Number((100 - this.plannedWeight).toFixed(1));
  }

  fillUnitLst() {
    this.Helper.GetUnits(
      this.userContext.CompanyID,
      this.userContext.language
    ).subscribe((res) => {

      //check if role is unit

      this.unitLst = this.userContext.Role !== "Employee" ? res.Data : res.Data.filter(a => {
        return a.ID == this.userContext.UnitId
      });

      this.ddlUnit = null;
    });
  }

  getFilteredUnitList() {

    return this.userContext.Role === "Admin" ? this.unitLst : this.unitLst.filter(a => a.ID === this.userContext.UnitId)


  }

  fillKpiType() {
    this.Helper.GetCodes(4, this.userContext.CompanyID).subscribe((res) => {
      this.KpiTypeLst = res.Data;
      this.filteredKpiTypeLst = res.Data.filter(a => a.MINOR_NO !== 5);
      if (!this.EditMode) {
        this.oProjects.KPITypeId = null;
      }
    });
  }

  fillKpiCycle() {
    this.Helper.GetCodes(3, this.userContext.CompanyID).subscribe((res) => {
      this.KpiCycleLst = res.Data;
      if (!this.EditMode) {
        this.oProjects.KPICycleId = null;
      }
    });
  }

  fillResultUnit() {
    this.Helper.GetCodes(5, this.userContext.CompanyID).subscribe((res) => {
      this.ResultUnitLst = res.Data;
      if (!this.EditMode) {
        this.oProjects.ResultUnitId = null;
      }
    });
  }

  /*
  fillBranches(){
    this.Helper.GetBranches(this.userContext.CompanyID).subscribe(
      res => {
        this.BranchLst = JSON.parse(res.Data) ;
        if(!this.EditMode){
          this.oProjects.BranchId = null;
        }
      }
    )
  }*/

  fillYears() {
    this.Helper.GetYears().subscribe((res) => {
      this.yearLst = res.Data;
      if (this.yearLst != null && this.yearLst.length > 0) {
        this.yearLst.forEach((element) => {
          this.ddlYear = element.id;
        });
      }
      this.fillStratigicObjective(this.userContext.CompanyID, this.ddlYear);
    });
  }

  /*
  fillEntryYears(){
    this.Helper.GetYears().subscribe(
      res => {
        this.yearEntryLst = res.Data;
        if(!this.EditMode && !this.AddOnFly){
          this.ddlYearEntry = null;
        }

      }
    )
  }*/
  fillStratigicObjective(CompanyID: number, year: number) {
    this.Helper.GetStratigicObjectives(
      CompanyID,
      year,
      this.userContext.language
    ).subscribe((res) => {
      if (res.IsError) {
        alert(res.ErrorMessage + "," + 1);
      } else {
        this.stratigicObjectivLst = res.Data;
        this.stratigicObjectivEntryLst = res.Data;
        this.ddlStratigicObj = 0;

        if (this.ddlYear > 0) {
          this.GetProjects();
        }
      }
    });
  }

  /*
  fillStratigicObjectiveEntrylst(CompanyID:number,year:number){
    this.Helper.GetStratigicObjectives(CompanyID,year,this.userContext.language).subscribe(
      res=>{

        if(res.IsError){
          alert(res.ErrorMessage+','+ 1)
        }
        else{
          this.stratigicObjectivEntryLst = res.Data
        }
      }
    )
  }*/
  ddlYearChange() {
    let year: number;
    this.ddlYear == null ? (year = -1) : (year = this.ddlYear);
    this.fillStratigicObjective(this.userContext.CompanyID, year);
    this.ClearProjectsTable();
    this.ShowEntryPnl = false;
    this.selectedId = null;
  }

  AddProject(ShowEntPnl = true) {
    this.EditMode = false;
    this.oProjects = new Projects();
    this.selectedId = null;
    this.ShowEntryPnl = ShowEntPnl;
    if (this.ddlStratigicObjKpi > 0)
      this.getProjectsRemainingWeight(this.ddlStratigicObjKpi);
    else
      this.getProjectsRemainingWeight(this.ddlStratigicObj);
    this.chkIsProcess = false;
    this.ObjectiveTitleForSelectedProject = "";
    document.getElementById("entryPanel").scrollIntoView();
    this.showTabs = false;
  }

  Cancel() {
    this.oProjects = new Projects();
    this.ShowEntryPnl = false;
    this.EditMode = false;
  }

  ddlStratigicObjChange(id) {
    this.ClearProjectsTable();
    this.ShowEntryPnl = false;
    this.selectedId = null;
    // this.showPlannedWeight = true;
    if (this.ddlStratigicObj > 0) {
      this.AddButton_Enabled = true;
      this.OrganizationService.getObjectiveKpiByObjective(
        id,
        this.userContext.language,
        ""
      ).subscribe((res) => {
        this.projectKpis = res.Data;
        // console.log("kpis", res.Data)


      });
    } else {

      this.AddButton_Enabled = false;
      this.ShowEntryPnl = false;
    }
    this.GetProjects();
  }

  ddlStratigicObjKpiChange(id) {
    this.ClearProjectsTable();
    this.ShowEntryPnl = false;
    this.selectedId = null;
    this.selectedKpi = this.ddlStratigicObjKpi;
    // this.showPlannedWeight = true;
    if (this.ddlStratigicObjKpi > 0) {
      this.AddButton_Enabled = true;
      // filter current project per kpi
      this.kpiSelectChange(this.selectedKpi)

    } else {

      this.AddButton_Enabled = false;
      this.ShowEntryPnl = false;
    }
    this.GetProjects()

    // if (id != 0) {
    //   this.objectiveSelected = this.stratigicObjectivLst.find(
    //     (element) => element.id == id
    //   );
    //   this.plannedWeight = this.objectiveSelected.Weight;
    // }
  }

  BlurQ1Target() {
    if (this.KPICycleId == 1) {
      if (this.Q1_Target != null && this.Q1_Target > 0) {
        this.Q1_Disabled = false;
        this.Q2_Disabled = true;
        this.Q3_Disabled = true;
        this.Q4_Disabled = true;
        this.Q2_Target = null;
        this.Q3_Target = null;
        this.Q4_Target = null;
        this.Q1_Target_Required = true;
        this.Q2_Target_Required = false;
        this.Q3_Target_Required = false;
        this.Q4_Target_Required = false;
      }
    }

    if (this.KPICycleId == 2) {
      if (this.Q1_Target != null && this.Q1_Target > 0) {
        this.Q1_Disabled = false;
        this.Q2_Disabled = true;
        this.Q3_Disabled = false;
        this.Q4_Disabled = true;
        this.Q2_Target = null;
        this.Q4_Target = null;
        this.Q1_Target_Required = true;
        this.Q2_Target_Required = false;
        this.Q3_Target_Required = true;
        this.Q4_Target_Required = false;
      }
    }
  }

  BlurQ2Target() {
    if (this.KPICycleId == 1) {
      if (this.Q2_Target != null && this.Q2_Target > 0) {
        this.Q1_Disabled = true;
        this.Q2_Disabled = false;
        this.Q3_Disabled = true;
        this.Q4_Disabled = true;
        this.Q1_Target = null;
        this.Q3_Target = null;
        this.Q4_Target = null;
        this.Q1_Target_Required = false;
        this.Q2_Target_Required = true;
        this.Q3_Target_Required = false;
        this.Q4_Target_Required = false;
      }
    }

    if (this.KPICycleId == 2) {
      if (this.Q2_Target != null && this.Q2_Target > 0) {
        this.Q1_Disabled = true;
        this.Q2_Disabled = false;
        this.Q3_Disabled = true;
        this.Q4_Disabled = false;
        this.Q1_Target = null;
        this.Q3_Target = null;
        this.Q1_Target_Required = false;
        this.Q2_Target_Required = true;
        this.Q3_Target_Required = false;
        this.Q4_Target_Required = true;
      }
    }
  }

  BlurQ3Target() {
    if (this.KPICycleId == 1) {
      if (this.Q3_Target != null && this.Q3_Target > 0) {
        this.Q1_Disabled = true;
        this.Q2_Disabled = true;
        this.Q3_Disabled = false;
        this.Q4_Disabled = true;
        this.Q1_Target = null;
        this.Q2_Target = null;
        this.Q4_Target = null;
        this.Q1_Target_Required = false;
        this.Q2_Target_Required = false;
        this.Q3_Target_Required = true;
        this.Q4_Target_Required = false;
      }
    }

    if (this.KPICycleId == 2) {
      if (this.Q3_Target != null && this.Q3_Target > 0) {
        this.Q1_Disabled = false;
        this.Q2_Disabled = true;
        this.Q3_Disabled = false;
        this.Q4_Disabled = true;
        this.Q2_Target = null;
        this.Q4_Target = null;
        this.Q1_Target_Required = true;
        this.Q2_Target_Required = false;
        this.Q3_Target_Required = true;
        this.Q4_Target_Required = false;
      }
    }
  }

  BlurQ4Target() {
    if (this.KPICycleId == 1) {
      if (this.Q4_Target != null && this.Q4_Target > 0) {
        this.Q1_Disabled = true;
        this.Q2_Disabled = true;
        this.Q3_Disabled = true;
        this.Q4_Disabled = false;
        this.Q1_Target = null;
        this.Q2_Target = null;
        this.Q3_Target = null;
        this.Q1_Target_Required = false;
        this.Q2_Target_Required = false;
        this.Q3_Target_Required = false;
        this.Q4_Target_Required = true;
      }
    }

    if (this.KPICycleId == 2) {
      if (this.Q4_Target != null && this.Q4_Target > 0) {
        this.Q1_Disabled = true;
        this.Q2_Disabled = false;
        this.Q3_Disabled = true;
        this.Q4_Disabled = false;
        this.Q1_Target = null;
        this.Q3_Target = null;
        this.Q1_Target_Required = false;
        this.Q2_Target_Required = true;
        this.Q3_Target_Required = false;
        this.Q4_Target_Required = true;
      }
    }
  }

  ddlKPICycleChange() {
    this.Q1_Disabled = false;
    this.Q2_Disabled = false;
    this.Q3_Disabled = false;
    this.Q4_Disabled = false;
    this.Q1_Target = null;
    this.Q2_Target = null;
    this.Q3_Target = null;
    this.Q4_Target = null;
    this.Q1_Target_Required = false;
    this.Q2_Target_Required = false;
    this.Q3_Target_Required = false;
    this.Q4_Target_Required = false;

    if (this.KPICycleId == 3) {
      this.Q1_Disabled = false;
      this.Q2_Disabled = false;
      this.Q3_Disabled = false;
      this.Q4_Disabled = false;

      this.Q1_Target_Required = true;
      this.Q2_Target_Required = true;
      this.Q3_Target_Required = true;
      this.Q4_Target_Required = true;
    }


  }

  GetProjectsCategories() {
    this.Helper.getProjectsCategories().subscribe(res => {
      if (res.IsError) {
        alert(res.ErrorMessage);
      } else {
        console.log("categories", res.Data)
        this.projectsCategories = res.Data;
        this.ddlCategory = null;
      }
    });

  }

  GetProjects() {
    this.selectedId = null;
    this.showTabs = false;
    this.EditMode = false;
    console.log(this.ddlUnit);
    let categoryFilter = this.ddlCategory == undefined || this.ddlCategory == "null" ? 0 : this.ddlCategory;
    this.Helper.GetProjectsWithKPIs(
      this.userContext.CompanyID,
      this.userContext.BranchID,
      this.ddlYear == undefined || this.ddlYear == "null" ? 0 : this.ddlYear,
       this.ddlUnit > 0  ? this.ddlUnit : 0,

      this.ddlStratigicObj == undefined || this.ddlStratigicObj == null
        ? 0
        : this.ddlStratigicObj,
      this.userContext.language,
      0,
    ).subscribe((res) => {
      if (res.IsError) {
        alert(res.ErrorMessage + "," + 1);
      } else {
        console.log('from get projects', this.ddlStratigicObjKpi)
        this.projectsList = this.ddlStratigicObjKpi > 0 ? res.Data.filter(a => a.KPI === this.ddlStratigicObjKpi) : res.Data;
        this.projectsList= this.ddlProjectStatus ? this.projectsList.filter(a=>a.planned_status==this.ddlProjectStatus) : this.projectsList;
        this.showPlannedWeight = true;
        this.plannedWeight = this.caculateWeightForTotal();
        this.notPlannedWeight = this.caculateNotPlannedWeightForTotal();
        this.projectsFilteredList = this.ddlCategory == undefined || this.ddlCategory == "null" ?
          this.projectsList
          : this.projectsList.filter(a => {
            return a.category == categoryFilter
          });

      }
    });
  }

  hasEditPermission(x) {
    // check if current user own the project
    if (x && x.UnitId > 0)
      return (x.UnitId === this.userContext.UnitId && this.userContext.Role === "Unit") || this.userContext.Role==="Admin";
    const project = this.projectsList.filter(a => a.ID === this.selectedId)[0];
    return project && (project.UnitId === this.userContext.UnitId && this.userContext.Role === "Unit" || this.userContext.Role=="Admin");
  }

  goToProject() {


  }

  ClearProjectsTable() {
    this.selectedId = null;
    const cols = [
      {
        HeaderText: "Stratigic Objective",
        DataField: "StratigicObjectiveName",
        Width: "30%",
      },
      {HeaderText: "Project", DataField: "Name", Width: "30%"},
      {HeaderText: "Unit", DataField: "UnitName", Width: "20%"},
      {HeaderText: "Weight", DataField: "Weight", Width: "10%"},
      {HeaderText: "Target", DataField: "Target", Width: "10%"},
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
      {
        title: "Details",
        DataValue: "ID",
        Icon_Awesome: "fa fa-list-alt",
        Action: "details",
      },
    ];
    this.gvProjects;
  }

  EditProject(id) {
    this.selectedId = id;
    this.EditMode = true;
    this.GetProjectData(id);
  }

  SaveProject() {

    if (this.oProjects.Weight <= 0) {
      alert('invalid project weight');
      return;
    }
    if (
      this.oProjects.Weight > this.remainingProjectsWeight ||
      this.remainingProjectsWeight < 1
    ) {

      alert('invalid project weight');
      return;
    }

    if (this.oProjects.Target == null || this.oProjects.Target <= 0) {
      alert("invalid project target." + "," + 1);
      return;
    }
    if (this.weightCalculation == 1 && !this.selectedKpi) {
      alert("invalid project kpi." + "," + 1);
      return;
    }
    if (!this.oProjects.UnitId) {
      alert("invalid project unit." + "," + 1);
      return;
    }

    if (!this.EditMode) {
      this.oProjects.evaluations = this.evaluationValuesList.map(a => {
          return Number(a);
        }
      );
      this.oProjects.CreatedBy = this.userContext.Username;
      this.oProjects.CompanyId = this.userContext.CompanyID;
      this.oProjects.BranchId = this.userContext.BranchID;
      this.oProjects.Code = "1";
      this.oProjects.StratigicObjectiveId = this.ddlStratigicObj;
      this.oProjects.KPI = this.selectedKpi + "";
      this.Helper.Save(
        this.oProjects,
        parseFloat(this.oProjects.Weight.toString()),
        this.userContext.language,
        this.ddlYear,
        this.oProjects.Category
      ).subscribe((res) => {
        if (res.IsError) {
          alert(res.ErrorMessage + "," + 1);
        } else {
          this.GetProjects();
          this.oProjects = new Projects();
          this.oProjects.StratigicObjectiveId = this.ddlStratigicObj;
          this.ShowEntryPnl = false;
          alert("0");
          if (this.AddOnFly) {
            this.AddOnFlyProjectSave.emit("Saved Suucessfully");
          }
        }
      });
    } else {

      this.oProjects.id = this.selectedId;
      this.oProjects.ModifiedBy = this.userContext.Username;
      this.oProjects.CompanyId = this.userContext.CompanyID;
      this.oProjects.KPI = this.selectedKpi + "";
      this.Helper.Update(
        this.oProjects,
        parseFloat(this.oProjects.Weight.toString()),
        this.userContext.language,
        this.ddlYear,
        this.oProjects.Category
      ).subscribe((res) => {
        if (res.IsError) {
          alert(res.ErrorMessage + "," + 1);
        } else {
          this.GetProjects();
          this.ShowEntryPnl = false;
          alert("0");
          if (this.AddOnFly) {
            this.AddOnFlyProjectSave.emit("Saved Successfully");
          }
        }
      });
    }
  }

  EditProjectEvent(id) {
    this.EditMode = true;
    this.GetProjectData(id);

    this.selectedId = id;
  }

  DeleteProjectEvent(id) {
    this.OrganizationService.GetProjectStatus(id).subscribe((res) => {
      if (res.IsError) {
        alert(res.ErrorMessage + ",1");
        return;
      }
      if (
        res.Data.planned_status == 1 ||
        (this.is_reveiwer_user && res.Data.planned_status == 3)
      ) {
        if (confirm("Are you sure?")) {
          this.Helper.Delete(id).subscribe((res) => {
            if (res.IsError) {
              alert("Delete all results for this projects first" + "," + 1);
            } else {
              this.oProjects = new Projects();
              this.oProjects.StratigicObjectiveId = this.ddlStratigicObj;
              this.ShowEntryPnl = false;
              this.GetProjects();
            }
          });
        }
      }
    });
  }

  ProjectDetailsEvent(id) {
    this.selectedId = id;
    this.ShowObjectiveDetails();
    this.EditMode = false;
    this.ShowEntryPnl = false;
  }

  gvProjectsEvent(event) {
    if (event[1] == "edit") {
      this.EditMode = true;
      console.log("get project data for edit")
      this.GetProjectData(event[0]);

      this.selectedId = event[0];
    } else if (event[1] == "delete") {
      if (confirm("Are you sure?")) {
        this.Helper.Delete(event[0]).subscribe((res) => {
          if (res.IsError) {
            alert("Delete all results for this projects first" + "," + 1);
          } else {
            this.oProjects = new Projects();
            this.oProjects.StratigicObjectiveId = this.ddlStratigicObj;
            this.ShowEntryPnl = false;
            this.GetProjects();
          }
        });
      }
    } else if (event[1] == "details") {
      this.selectedId = event[0];
      this.ShowObjectiveDetails();
    } else if (event[1] == "ActionPlan") {
      this.OpenModal(event[0]);
    }
  }

  getProjectsRemainingWeight(kpiId) {
    this.Helper.getProjectsRemainingWeight(kpiId).subscribe((res) => {
      if (res.IsError) {
        alert(res.ErrorMessage + "," + 1);
        this.remainingProjectsWeight = 0;
      }
      this.remainingProjectsWeight = res.Data;
      if (!this.EditMode) {
        this.oProjects.Weight = this.remainingProjectsWeight;
      } else {
        this.remainingProjectsWeight = this.remainingProjectsWeight + this.oProjects.Weight;
      }
    });
  }


  GetKPIList(id) {

    let objectiveId = this.projectsFilteredList.filter(a => {
      return a.ID == id
    })[0].StratigicObjectiveId;
    this.OrganizationService.getObjectiveKpiByObjective(
      objectiveId,
      this.userContext.language,
      ""
    ).subscribe((res) => {
      this.projectKpis = res.Data;
    });

  }

  GetProjectData(id) {
    console.log("test project load kpi")

    this.Helper.GetProjectById(id, this.userContext.language).subscribe(
      (res) => {
        if (res.IsError) {
          alert(res.ErrorMessage + "," + 1);
        } else {
          console.log("load kpi list")
          this.GetKPIList(id);
          this.ObjectiveTitleForSelectedProject =
            " - " + res.Data.StratigicObjectiveName;
          this.oProjects = new Projects();
          this.oProjects.Q1_Target = null;
          this.oProjects.Q2_Target = null;
          this.oProjects.Q3_Target = null;
          this.oProjects.Q4_Target = null;
          this.ShowEntryPnl = true;
          this.evaluationValuesList = res.Data.evaluations && res.Data.evaluations.length > 0 ? res.Data.evaluations : [];
          //this.fillBranches()
          //this.fillEntryYears()
          //this.fillStratigicObjectiveEntrylst(this.userContext.CompanyID,-1)
          this.oProjects = res.Data;
          this.oProjects.WeigthValue = this.projectsList.filter(a => {
            return a.ID == id
          })[0].WeigthValue;
          this.remainingProjectsWeight = res.Data.max;
          if (this.oProjects.p_type == 2) {
            this.chkIsProcess = true;
          } else {
            this.chkIsProcess = false;
          }
          if (this.oProjects != null) {
            this.oProjects.Q1_Disabled = true;
            this.oProjects.Q2_Disabled = true;
            this.oProjects.Q3_Disabled = true;
            this.oProjects.Q4_Disabled = true;

            this.oProjects.Q1_Target_Required = false;
            this.oProjects.Q2_Target_Required = false;
            this.oProjects.Q3_Target_Required = false;
            this.oProjects.Q4_Target_Required = false;

            if (
              this.oProjects.Q1_Target != null &&
              this.oProjects.Q1_Target > 0
            ) {
              this.oProjects.Q1_Disabled = false;
              this.oProjects.Q1_Target_Required = true;
            }
            if (
              this.oProjects.Q2_Target != null &&
              this.oProjects.Q2_Target > 0
            ) {
              this.oProjects.Q2_Disabled = false;
              this.oProjects.Q2_Target_Required = true;
            }

            if (
              this.oProjects.Q3_Target != null &&
              this.oProjects.Q3_Target > 0
            ) {
              this.oProjects.Q3_Disabled = false;
              this.oProjects.Q3_Target_Required = true;
            }

            if (
              this.oProjects.Q4_Target != null &&
              this.oProjects.Q4_Target > 0
            ) {
              this.oProjects.Q4_Disabled = false;
              this.oProjects.Q4_Target_Required = true;
            }
          }

          if (res.Data.Description == "undefined") {
            this.oProjects.Description = "";
          }
          this.ddlYearEntry = res.Data.Year;

          this.selectedKpi = res.Data.KPI;
          this.getProjectsRemainingWeight(res.Data.KPI)

          document.getElementById("entryPanel").scrollIntoView();

          this.OrganizationService.GetProjectStatus(id).subscribe((res) => {
            if (res.IsError) {
              alert(res.ErrorMessage + ",1");
              return;
            }
            this.planned_status = res.Data.planned_status;
          });
        }
      }
    );
  }

  @Input()
  planned_status;

  toggleProcess() {
    if (this.chkIsProcess) {
      this.oProjects.p_type = 2;
      this.getEvaluations(2);
    } else {
      this.oProjects.p_type = 1;
      this.getEvaluations(1);
    }
  }

  ShowDocumentEntry = false;

  AddDocument() {
    this.txtDocumentName = "";
    this.ShowDocumentEntry = true;
    this.DocEditMode = false;
    this.editHandler = false;
  }

  LoadProjectDocuments(id) {
    this.Helper.GetProjectDocuments(id, this.userContext.language).subscribe(
      (res) => {
        if (res.IsError) {
          alert(res.ErrorMessage + "," + 1);
          return;
        }

        const cols = [
          {
            HeaderText: this.GetLocalResourceObject("docName"),
            DataField: "doc_name",
            Width: "60%",
          },
          {
            HeaderText: this.GetLocalResourceObject("fileName"),
            DataField: "FileName",
            Width: "40%",
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
        this.gvDocuments.bind(cols, res.Data, "gvDocuments", this.hasEditPermission(undefined) ? actions : []);
      }
    );
  }

  SaveProjectDocuments() {
    if (!this.DocEditMode) {
      this.Helper.SaveProjectDocuments(
        this.selectedId,
        this.txtDocumentName,
        this.userContext.Username
      ).subscribe((res) => {
        if (res.IsError) {
          alert(res.ErrorMessage + "," + 1);
          return;
        }
        alert("0");
        this.LoadProjectDocuments(this.selectedId);
      });
    } else {
      this.Helper.UpdateProjectDocuments(
        this.selectDocID,
        this.selectedId,
        this.txtDocumentName,
        this.userContext.Username
      ).subscribe((res) => {
        if (res.IsError) {
          alert(res.ErrorMessage + "," + 1);
          return;
        }
        alert("0");
        this.LoadProjectDocuments(this.selectedId);
      });
    }
    this.ShowDocumentEntry = false;
  }

  gvDocumentsHandler(event) {
    if (event[1] == "edit") {
      this.Helper.GetProjectDocumentByID(event[0]).subscribe((res) => {
        if (res.IsError) {
          alert(res.ErrorMessage + "," + 1);
          return;
        }
        this.ShowDocumentEntry = true;
        this.txtDocumentName = res.Data.doc_name;
        this.selectDocID = res.Data.ID;
        this.DocEditMode = true;
      });
    } else if (event[1] == "delete") {
      this.OrganizationService.GetProjectStatus(this.selectedId).subscribe(
        (res) => {
          if (res.IsError) {
            alert(res.ErrorMessage + ",1");
            return;
          }
          if (
            res.Data.planned_status == 1 ||
            (this.is_reveiwer_user && res.Data.planned_status == 3)
          ) {
            if (confirm(this.GetLocalResourceObject("confirm"))) {
              this.Helper.DeleteProjectDocuments(event[0]).subscribe((res) => {
                if (res.IsError) {
                  alert(res.ErrorMessage + "," + 1);
                  return;
                }
                this.LoadProjectDocuments(this.selectedId);
              });
            }
          }
        }
      );
    }
  }

  // KPIs Related code starts here
  @ViewChild("gvKPIs", {read: false, static: false}) gvKPIs;
  showKpiEntry = false;
  remainingKPIWeight;
  txtKpiweight;
  showTabs = false;
  txtKpiName;
  txtKpiDesc;
  txtKpiTarget;
  ddlKpiBSC;
  ddlKpiMeasurement;
  ddlObjKpiBSCSearch = "";
  ddlBetterUpDown = 1;
  selectedObjectiveKpiId;
  lblSelectedStratigicObjective;
  KPICycleId;
  KpiResultUnitId;
  KPITypeId;
  Q1_Target;
  Q2_Target;
  Q3_Target;
  Q4_Target;
  Q4_Target_Required;
  Q3_Target_Required;
  Q2_Target_Required;
  Q1_Target_Required;
  Q1_Disabled = true;
  Q2_Disabled = true;
  Q3_Disabled = true;
  Q4_Disabled = false;

  ObjectiveKpiAdd() {
    this.showKpiEntry = true;
    this.getObjectiveKPIRemainingWeight(this.selectedId);
    this.resetObjectiveKpiEntry();
    this.editHandler = false;
    this.Q1_Target = null;
    this.Q2_Target = null;
    this.Q3_Target = null;
    this.Q4_Target = null;
  }

  getObjectiveKPIRemainingWeight(objID) {
    this.OrganizationService.getObjectiveKPIRemainingWeight(objID, 2).subscribe(
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

  LoadObjectiveKPIs() {
    this.OrganizationService.getObjectiveKpiByObjective(
      this.selectedId,
      this.userContext.language,
      this.ddlObjKpiBSCSearch
    ).subscribe((res) => {
      if (res.IsError) {
        //alert(res.ErrorMessage);
        alert(res.ErrorMessage + "," + 1);
        return;
      }
      this.resetObjectiveKpiEntry();
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
          DataField: "bsc",
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

      this.gvKPIs.bind(cols, res.Data, "gvKPIs", this.hasEditPermission(undefined) ? actions : []);
    });
  }

  resetObjectiveKpiEntry() {
    this.txtKpiDesc = "";
    this.txtKpiName = "";
    this.txtKpiTarget = "";
    this.txtKpiweight = "";
    this.ddlKpiBSC = null;
    this.ddlKpiMeasurement = null;
    this.selectedObjectiveKpiId = null;
    this.KPICycleId = null;
    this.KpiResultUnitId = null;
    this.KPITypeId = null;
  }

  refreshObjKpisList() {
    this.LoadObjectiveKPIs();
  }

  gvKPIsHandler(event) {
    if (event[1] == "edit") {
      this.OrganizationService.GetProjectStatus(this.selectedId).subscribe(
        (res) => {
          if (res.IsError) {
            alert(res.ErrorMessage + ",1");
            return;
          }
          this.planned_status = res.Data.planned_status;
          this.prepareObjecitveKpiForEdit(event[0]);
          this.editHandler = true;
        }
      );
    } else if (event[1] == "delete") {
      this.OrganizationService.GetProjectStatus(this.selectedId).subscribe(
        (res) => {
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
                  this.LoadObjectiveKPIs();
                }
              );
            }
          }
        }
      );
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
      this.txtKpiDesc = res.Data.Description;
      this.txtKpiName = res.Data.Name;
      this.txtKpiTarget = res.Data.Target;
      this.txtKpiweight = res.Data.Weight;
      this.txtKpiweightOld = res.Data.Weight;
      this.ddlKpiBSC = res.Data.bsc;
      this.ddlKpiMeasurement = res.Data.measurement;
      this.selectedObjectiveKpiId = kpiID;
      this.remainingKPIWeight = res.Data.max;
      this.KPICycleId = res.Data.C_KPI_CYCLE_ID;
      this.KpiResultUnitId = res.Data.C_RESULT_UNIT_ID;
      this.KPITypeId = res.Data.C_KPI_TYPE_ID;
      this.ddlBetterUpDown = res.Data.BetterUpDown;
      this.betterUpDownChange();
      this.Q1_Target =
        res.Data.q1_traget.length > 0
          ? res.Data.q1_traget[0]
          : res.Data.q1_traget;
      this.Q2_Target =
        res.Data.q2_traget.length > 0
          ? res.Data.q2_traget[0]
          : res.Data.q2_traget;
      this.Q3_Target =
        res.Data.q3_traget.length > 0
          ? res.Data.q3_traget[0]
          : res.Data.q3_traget;
      this.Q4_Target =
        res.Data.q4_traget.length > 0
          ? res.Data.q4_traget[0]
          : res.Data.q4_traget;
      this.showKpiEntry = true;
      // this.BlurQ1Target();
      // this.BlurQ2Target();
      // this.BlurQ3Target();
      // this.BlurQ4Target();
    });
  }

  SaveObjectiveKpi() {
    // debugger;

    let q_total =
      parseInt(this.Q1_Target == null ? 0 : this.Q1_Target) +
      parseInt(this.Q2_Target == null ? 0 : this.Q2_Target) +
      parseInt(this.Q3_Target == null ? 0 : this.Q3_Target) +
      parseInt(this.Q4_Target == null ? 0 : this.Q4_Target);

    if (this.txtKpiTarget != q_total && this.KPITypeId == 1) {
      alert(
        "Operation Faild : 'Quarters' and 'KPI Annual Target' are not equals."
      );
      return;
    }

    if (this.editHandler) {
      const actualRemainingKPIWeight =
        this.remainingKPIWeight + this.txtKpiweightOld;
      if (this.txtKpiweight > this.remainingKPIWeight) {
        alert("the weight more than remining weight");
      } else if (this.txtKpiweight > 100 || this.txtKpiweight < 1) {
        alert("the weigth must be more than 1% and less than 100%");
      } else if (this.remainingKPIWeight < 1 && this.txtKpiweight > 0) {
        alert("The weight of KPIs more then 100%");
      } else if (
        this.selectedObjectiveKpiId == null ||
        this.selectedObjectiveKpiId == undefined
      ) {
        alert("the selected Objective Kpi unknown");
      } else {
        console.log('this.editHandler update', this.editHandler)

        this.OrganizationService.UpdateObjectiveKpi(
          this.selectedObjectiveKpiId,
          this.txtKpiName,
          "",
          this.txtKpiDesc,
          this.selectedId,
          this.txtKpiweight,
          this.txtKpiTarget,
          this.ddlKpiBSC,
          this.ddlKpiMeasurement,
          this.userContext.CompanyID,
          this.userContext.BranchID,
          this.userContext.Username,
          this.userContext.language,
          this.KPICycleId,
          this.KPITypeId,
          this.KpiResultUnitId,
          this.ddlBetterUpDown
        ).subscribe((res) => {
          if (res.IsError) {
            alert(res.ErrorMessage + "," + 1);
            return;
          }
          this.OrganizationService.SavePlannedKPIs(
            this.userContext.Username,
            this.selectedId,
            this.Q1_Target == null ? 0 : this.Q1_Target,
            this.Q2_Target == null ? 0 : this.Q2_Target,
            this.Q3_Target == null ? 0 : this.Q3_Target,
            this.Q4_Target == null ? 0 : this.Q4_Target,
            this.selectedObjectiveKpiId
          ).subscribe((res) => {
            if (res.IsError) {
              alert(res.ErrorMessage);
              return;
            }
            alert("0");
            this.LoadObjectiveKPIs();
            this.resetObjectiveKpiEntry();
            this.showKpiEntry = false;
            this.GetProjects();
          });
        });
      }
    } else {
      if (this.txtKpiweight > this.remainingKPIWeight) {
        alert("the weight more than remining weight");
      } else if (this.remainingKPIWeight < 1) {
        alert("the weight of all KPIs more than 100%");
      } else if (this.txtKpiweight > 100 || this.txtKpiweight < 1) {
        alert("the weigth must be more than 1% and less than 100%");
      } else if (this.remainingKPIWeight < 1 && this.txtKpiweight > 0) {
        alert("The weight of KPIs more then 100%");
      } else {
        console.log('this.editHandler save', this.editHandler)

        this.OrganizationService.SaveObjectiveKpi(
          this.txtKpiName,
          "",
          this.txtKpiDesc,
          this.selectedId,
          this.txtKpiweight,
          this.txtKpiTarget,
          this.ddlKpiBSC,
          this.ddlKpiMeasurement,
          this.userContext.CompanyID,
          this.userContext.BranchID,
          this.userContext.Username,
          this.userContext.language,
          2,
          this.KPICycleId,
          this.KPITypeId,
          this.KpiResultUnitId,
          this.ddlBetterUpDown
        ).subscribe((res) => {
          if (res.IsError) {
            alert(res.ErrorMessage + "," + 1);
            return;
          }
          this.OrganizationService.SavePlannedKPIs(
            this.userContext.Username,
            this.selectedId,
            this.Q1_Target == null ? 0 : this.Q1_Target,
            this.Q2_Target == null ? 0 : this.Q2_Target,
            this.Q3_Target == null ? 0 : this.Q3_Target,
            this.Q4_Target == null ? 0 : this.Q4_Target,
            res.Data
          ).subscribe((res) => {
            if (res.IsError) {
              alert(res.ErrorMessage);
              return;
            }
            alert("0");
            this.LoadObjectiveKPIs();
            this.resetObjectiveKpiEntry();
            this.showKpiEntry = false;
            this.GetProjects();
          });
        });
      }
    }
  }

  kpiSelectChange(data) {
    this.getProjectsRemainingWeight(data)
  }

  SavePlannedKPIs(projectID, kpi_id) {
    this.OrganizationService.SavePlannedKPIs;
  }

  ShowObjectiveDetails() {
    this.EditMode = false;
    this.showTabs = true;
    setTimeout(() => {
      this.ProjService.GetProjectById(
        this.selectedId,
        this.userContext.language
      ).subscribe((res) => {
        if (res.IsError) {
          alert(res.ErrorMessage + "," + 1);
          return;
        }
        this.lblSelectedStratigicObjective = res.Data.Name;

        this.LoadObjectiveKPIs();
        this.LoadProjectDocuments(this.selectedId);
        document.getElementById("tabsDiv").scrollIntoView();
      });
    }, 500);
  }


  closeApprovalModal() {
    document.getElementById("approvalModal").className = "modal fade";
    document.getElementById("approvalModal").style.display = "none";
  }

  //#region Action plan modal

  closeActionPlanModal() {
    document.getElementById("ActionPlanModal").className = "modal fade";
    document.getElementById("ActionPlanModal").style.display = "none";
  }

  OpenModal(projectID) {
    this.OrganizationService.GetProjectStatus(projectID).subscribe((res) => {
      if (res.IsError) {
        alert(res.ErrorMessage + ",1");
        return;
      }

      if (
        res.Data.planned_status == 1 ||
        (this.is_reveiwer_user && res.Data.planned_status == 3)
      ) {
        document.getElementById("ActionPlanModal").style.display = "block";
        document.getElementById("ActionPlanModal").className = "modal fade in";
        this.ActionPlanModal.AddOnFlyValues(this.ddlYear, projectID);
      }
    });
  }

  openApprovalModal(x) {

    this.oProjects = x;
    this.selectedId = x.ID;
    document.getElementById("approvalModal").style.display = "block";
    document.getElementById("approvalModal").className = "modal fade in";

  }

  //#endregion Action plan modal

  //#region Approvals

  ConfirmProject() {
    this.OrganizationService.UpdateProjectStatus(this.selectedId, 2, this.userContext.Username, this.approvalNote).subscribe(
      (res) => {
        if (res.IsError) {
          alert(res.ErrorMessage + ",1");
          return;
        }
        alert("0");
        this.GetProjects();
        this.closeApprovalModal();
      }
    );
  }

  DeclineProject() {
    this.OrganizationService.UpdateProjectStatus(this.selectedId, 3, this.userContext.Username, this.approvalNote).subscribe(
      (res) => {
        if (res.IsError) {
          alert(res.ErrorMessage + ",1");
          return;
        }
        alert("0");
        this.GetProjects();
        this.closeApprovalModal();
      }
    );
  }

  getApprovalStatusText(status) {
    if (status === 1) {
      return "Waiting for planning approval";
    } else if (status === 2) {
      return "Planning approved";
    } else if (status === 3) {
      return "Planning declined";
    } else if (status === 4) {
      return "Assessment approved";
    } else if (status === 5) {
      return "Assessment declined";
    } else {
      return "";
    }

  }

  //#endregion

  changeKPIType(id) {
    this.OrganizationService.CalculateTargetBasedonKPI_Type(
      id,
      this.Q1_Target == null ? 0 : this.Q1_Target,
      this.Q2_Target == null ? 0 : this.Q2_Target,
      this.Q3_Target == null ? 0 : this.Q3_Target,
      this.Q4_Target == null ? 0 : this.Q4_Target
    ).subscribe((annualTarget) => {
      this.txtKpiTarget = annualTarget;
    });
  }

  betterUpDownChange() {
    console.log(this.ddlBetterUpDown)

    if (this.ddlBetterUpDown == 1) {
      //up
      this.filteredKpiTypeLst = this.KpiTypeLst.filter(a => a.MINOR_NO !== 5);
    }
    if (this.ddlBetterUpDown == 2) {
      //up
      this.filteredKpiTypeLst = this.KpiTypeLst.filter(a => a.MINOR_NO !== 4);

    }

  }

  quarterTargetChange(kpi) {

    if (kpi > 0) {

      this.OrganizationService.CalculateTargetBasedonKPI_Type(
        kpi,
        this.Q1_Target == null ? 0 : this.Q1_Target,
        this.Q2_Target == null ? 0 : this.Q2_Target,
        this.Q3_Target == null ? 0 : this.Q3_Target,
        this.Q4_Target == null ? 0 : this.Q4_Target
      ).subscribe((annualTarget) => {
        this.txtKpiTarget = annualTarget;
      });


    }


  }
}


