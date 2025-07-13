import {Component, EventEmitter, Input, OnInit, Output} from "@angular/core";
import {ProjectsAssessmentService} from "../Services/projects-assessment.service";
import {UserContextService} from "../Services/user-context.service";
import {UsersService} from "../Services/Users/users.service";
import {ProjectsService} from "../Services/Projects/projects.service";
import {OrganizationService} from "../Organization/organization.service";
import {QuartersService} from "../quaters-activation/service/quarters.service";
import {SettingsService} from "../settings/settings.service";


@Component({
  selector: 'app-projects-employees-comparition',
  templateUrl: './projects-employees-comparison.component.html',
  styleUrls: ['./projects-employees-comparison.component.css']
})
export class ProjectsEmployeesComparisonComponent implements OnInit {
  errorMessage: string;
  filesToUpload: Array<File>;
  selectedFileNames: string[] = [];
  public isLoadingData: Boolean = false;
  fileUploadVar: any;
  uploadResult: any;
  filterQ1: boolean;
  filterQ2: boolean;
  filterQ3: boolean;
  filterQ4: boolean;
  documentsList: File[];
  plannedWeight: number = 0;
  actualWeight: number = 0;
  successRate: number = 0;
  progress: number = 0;
  @Input()
  AddOnFly = false;
  @Output()
  AddOnFlyAssessmentSave = new EventEmitter<string>();
  ddlSearchYearsList;
  ddlSearchObjectivesList;
  ddlSearchUnitsList;
  projectAssessmentList;
  ddlSearchYear;
  ddlSearchObjective = 0;
  ddlSearchUnit = -1;
  formRow;
  PageResources = [];
  is_reveiwer_user;
  objectiveSelected: any;
  KpiTypeLst: any;
  showWeight: boolean = false;
  notShowActual: boolean = false;
  perProjectView = false;
  perObjectiveView = false;
  onFlyProjectID;
  onFlyObjID;
  addedFiles: any = [];
  modificationPermission = false;
  quartersActivations: any = [];
  q1Status = false;
  q2Status = false;
  q3Status = false;
  q4Status = false;

  updateTotalWeightValues() {
    if (this.projectAssessmentList && this.projectAssessmentList.length > 0) {
      this.plannedWeight = this.projectAssessmentList.filter(a => {
        return a.planned_status !== "Declined";
      }).reduce((accumulator, object) => {
        return accumulator + object.plannedStratigy;
      }, 0);
      this.actualWeight = this.projectAssessmentList.reduce((total, x) => total + this.getProjectStrategyWeightResult(x), 0);
      this.successRate = (this.actualWeight / this.plannedWeight) * 100;

      const progressPlantWeight = this.projectAssessmentList.filter(a => {
        return a.planned_status !== "Declined";
      }).reduce((accumulator, object) => {
        const r = this.getProjectStrategyWeightResult(object);
        return r === 0 ? accumulator : accumulator + object.plannedStratigy;
      }, 0);
      const progressActualWeight = this.projectAssessmentList.reduce((total, x) => {
        return total + this.getProjectStrategyWeightResult(x);
      }, 0);
      this.progress = (progressActualWeight / progressPlantWeight) * 100;
    }
  }

  filterQ1Changed(event: any) {
    this.filterQ1 = event.target.checked;
    this.updateTotalWeightValues();
  }

  filterQ2Changed(event: any) {
    this.filterQ2 = event.target.checked;
    this.updateTotalWeightValues();
  }

  filterQ3Changed(event: any) {
    this.filterQ3 = event.target.checked;
    this.updateTotalWeightValues();
  }

  filterQ4Changed(event: any) {
    this.filterQ4 = event.target.checked;
    this.updateTotalWeightValues();
  }

  constructor(
    private projectsAssessmentService: ProjectsAssessmentService,
    private userContextService: UserContextService,
    private userService: UsersService,
    private settingsService: SettingsService,
    private projectService: ProjectsService,
    private OrganizationService: OrganizationService,
    private quartersActivationService: QuartersService
  ) {
    this.modificationPermission = this.userContextService.RoleId != 5;
    this.quartersActivationService.getQuarters().subscribe(res => {

      if (res.IsError) {
        alert(res.ErrorMessage + ',' + 1);
        return;
      }
      this.quartersActivations = res.Data;

      this.q1Status = res.Data.filter(a => {
        return a.Name === "Q1";
      })[0].Status === 1;
      this.q2Status = res.Data.filter(a => {
        return a.Name === "Q2";
      })[0].Status === 1;
      this.q3Status = res.Data.filter(a => {
        return a.Name === "Q3";
      })[0].Status === 1;
      this.q4Status = res.Data.filter(a => {
        return a.Name === "Q4";
      })[0].Status === 1;
    });
    this.getApprovals();
    this.errorMessage = "";
    this.filesToUpload = [];
    this.selectedFileNames = [];
    this.uploadResult = "";
    this.fillDDLSearchObjectives();
    this.fillDDLSearchUnits();
    this.fillDDLSearchYears();
    this.userService
      .GetLocalResources(
        window.location.hash,
        this.userContextService.CompanyID,
        this.userContextService.language
      )
      .subscribe((res) => {
        if (res.IsError) {
          alert(res.ErrorMessage);
          return;
        }
        this.PageResources = res.Data;
      });
  }

  getApprovals() {
    this.OrganizationService.GetApprovalSetupByURL(
      window.location.hash,
      this.userContextService.CompanyID
    ).subscribe((res) => {
      if (res.IsError) {
        this.is_reveiwer_user = false;
        return;
      }
      this.is_reveiwer_user = res.Data.filter(a => a.reviewing_user.toLocaleLowerCase() === this.userContextService.Username.toLocaleLowerCase()).length > 0;

    });
  }

  GetLocalResourceObject(resourceKey) {
    for (let i = 0; i < this.PageResources.length; i++) {
      if (this.PageResources[i].resource_key === resourceKey) {
        return this.PageResources[i].resource_value;
      }
    }
  }

  ngOnInit() {
    // this.fillKpiType();
    this.filterQ1 = true;
    this.filterQ2 = true;
    this.filterQ3 = true;
    this.filterQ4 = true;
  }

  ddlYearChange() {
    this.fillDDLSearchObjectives();
  }

  saveProjectsAssessment() {
    this.projectsAssessmentService
      .SaveProjectAssessment(
        this.projectAssessmentList,
        this.userContextService.Username,
        this.userContextService.language
      )
      .subscribe((res) => {
        if (res.IsError) {
          alert(res.ErrorMessage);
          return;
        }
        if (res.ErrorMessage != "") {
          alert(res.ErrorMessage + "," + 1);
        }
        if (this.AddOnFly) {
          this.AddOnFlyAssessmentSave.emit(
            this.GetLocalResourceObject("lblSavedSuccessfully")
          );
        } else {
          this.searchProjectsAssessment();
        }
      });
  }

  trafficLightList;

  loadTrafficLight() {

    this.settingsService.LoadTrafficLights(this.userContextService.CompanyID, this.ddlSearchYear).subscribe(res => {
      if (res.IsError) {
        alert(res.ErrorMessage);
        return;
      }
      this.trafficLightList = res.Data;
    });

  }

  setDefaultAddOnFly(projectID) {
    this.onFlyProjectID = projectID;
    this.perObjectiveView = false;
    this.perProjectView = true;
    this.projectsAssessmentService
      .getProjectAssessment(
        this.userContextService.CompanyID,
        -1,
        -1,
        -1,
        this.userContextService.language,
        projectID
      )
      .subscribe((res) => {
        if (res.IsError) {
          alert(res.ErrorMessage);
          return;
        }
        this.projectAssessmentList = res.Data;
      });
  }

  setObjDefaultAddOnFly(ObjectiveID) {
    this.onFlyObjID = ObjectiveID;
    this.perObjectiveView = true;
    this.perProjectView = false;
    this.projectsAssessmentService
      .getProjectAssessment(
        this.userContextService.CompanyID,
        -1,
        -1,
        ObjectiveID,
        this.userContextService.language
      )
      .subscribe((res) => {
        if (res.IsError) {
          alert(res.ErrorMessage);
          return;
        }
        this.projectAssessmentList = res.Data;


      });
  }

  searchProjectsAssessment() {
    this.loadTrafficLight();
    this.notShowActual = true;
    this.projectsAssessmentService
      .getProjectAssessmentWithEmployeesAssessment(
        this.userContextService.CompanyID,
        this.ddlSearchYear,
        (this.userContextService.Role === "Admin" || this.userContextService.UnitId === 0) ? this.ddlSearchUnit : this.userContextService.UnitId,
        this.ddlSearchObjective,
        this.userContextService.language
      )
      .subscribe((res) => {
        if (res.IsError) {
          alert(res.ErrorMessage);
          return;
        }

        this.getProjectAssessment(res.Data);
      });
  }

  // }

  getProjectAssessment(projects) {
    this.projectsAssessmentService.getProjectAssessment(
      this.userContextService.CompanyID,
      this.ddlSearchYear,
      -1,
      this.ddlSearchObjective,
      this.userContextService.language
    )
      .subscribe((res) => {
        if (res.IsError) {
          alert(res.ErrorMessage);
          return;
        }
        projects.map((a) => {
          a.KPIs = res.Data.filter(x => x.ID === a.ID)[0].KPIs;
        });
        this.projectAssessmentList = projects;
        console.log("with kpis", this.projectAssessmentList);
      });

  }

  getComparisionResult(x) {

    const projectResult = this.getProjectFilteredPercentage(x).toFixed(1) > 120 ? 120 : this.getProjectFilteredPercentage(x).toFixed(1);
    const empAvg = x.EmpAvg;

    return Number(projectResult) - empAvg;
  }

  getBackgroundColor(x) {
    const difference = Math.abs(this.getComparisionResult(x));
    return difference <= 5 ? '#7ae47a' : '#e33939';
  }

  fillDDLSearchUnits() {
    this.projectsAssessmentService
      .getUnites(this.userContextService.CompanyID)
      .subscribe((res) => {
        if (res.IsError) {
          alert(res.ErrorMessage);
          return null;
        }


        this.ddlSearchUnitsList = this.userContextService.Role === "Admin" || this.userContextService.UnitId === 0 ? res.Data : res.Data.filter(a => {
          return a.ID == this.userContextService.UnitId;
        });
      });
  }

  fillDDLSearchYears() {
    this.projectsAssessmentService.getYears().subscribe((res) => {
      if (res.IsError) {
        alert(res.ErrorMessage);
        return null;
      }
      this.ddlSearchYearsList = res.Data;
      if (this.ddlSearchYearsList != null && this.ddlSearchYearsList.length > 0) {
        this.ddlSearchYearsList.forEach((element) => {
          this.ddlSearchYear = element.id;
          console.log('year active', this.ddlSearchYear);
        });
        this.fillDDLSearchObjectives();
        this.loadTrafficLight();
      }
    });
  }

  fillDDLSearchObjectives() {
    this.projectsAssessmentService
      .getObjectives(
        this.userContextService.CompanyID,
        this.ddlSearchYear,
        this.ddlSearchYear
      )
      .subscribe((res) => {
        if (res.IsError) {
          alert(res.ErrorMessage);
          return null;
        }
        this.ddlSearchObjectivesList = res.Data;
      });
  }

  RemoveEvident(docID) {
    // Are you sure, you want to remove this evident ?
    if (confirm(this.GetLocalResourceObject("confirmDeleteEvident"))) {
      this.projectsAssessmentService
        .RemoveEvident(docID, this.userContextService.Username)
        .subscribe((res) => {
          if (res.IsError) {
            alert(res.ErrorMessage + "," + 1);
            return;
          }

          if (!this.AddOnFly) {
            this.searchProjectsAssessment();
          } else {
            if (this.perProjectView) {
              this.setDefaultAddOnFly(this.onFlyProjectID);
            } else {
              this.setObjDefaultAddOnFly(this.onFlyObjID);
            }
          }
        });
    }
  }

  getStyleObj(strStyle) {
    return JSON.parse(strStyle);
  }

  //#region Approvals

  getKPISuccessPercentage(kpi: any) {
    if (!this.filterQ1 && !this.filterQ2 && !this.filterQ3 && !this.filterQ4) {
      return 0;
    }
    if (this.filterQ1 && this.filterQ2 && this.filterQ3 && this.filterQ4) {

      const target = this.getFilteredTarget(kpi);
      const result = this.getFilteredResult(kpi);
      if (kpi.BetterUpDown === 2) {
        return result === 0 && target === 0 ? '-' :
          (((result === 0 ? 1.20 : (target / result) > 1.20 ? 1.20 : (target / result)) * 100).toFixed(1));
      } else {
        return (target === 0 ? '-' : (((result / target) > 1.20 ? 1.20 : (result / target)) * 100).toFixed(1));
      }
      //
      // if (kpi.BetterUpDown === 2) {
      //   return (kpi.AnnualResult === 0 ? 120 : (kpi.Target / kpi.AnnualResult) * 100).toFixed(1);
      //
      // } else {
      //   return (kpi.Target === 0 ? 0 : (kpi.AnnualResult / kpi.Target) * 100).toFixed(1);
      // }
    } else {
      const target = this.getFilteredTarget(kpi);
      const result = this.getFilteredResult(kpi);

      if (kpi.BetterUpDown === 2) {
        return result === 0 && target === 0 ? '-' :
          (((result === 0 ? 1.20 : (target / result) > 1.20 ? 1.20 : (target / result)) * 100).toFixed(1));
      } else {
        return (target === 0 ? '-' : (((result / target) > 1.20 ? 1.20 : (result / target)) * 100).toFixed(1));
      }
    }
  }

  getProjectFilteredPercentage(x: any) {

    if (!x.KPIs || x.KPIs.length === 0) {
      return 0;
    }

    let check = true;

    x.KPIs.map(a => {
      const kpiResult = this.getKPISuccessPercentage(a);

      // console.log(a.KPI_name, kpiResult);
      if (kpiResult !== '0.0' && kpiResult !== '-') {
        check = false;
      }
    });

    if (check === true) {

      return 0;

    }

    return (x.KPIs.reduce((accumulator, kpi) => {

      const kpiResult = this.getKPISuccessPercentage(kpi);
      if (kpiResult !== '-') {
        if (x.ID === 207) {
          // console.log(kpi.KPI_name);
          // console.log(kpi.Weight, Number(kpiResult), (Number(kpiResult) * kpi.Weight) / 100);
        }
        return accumulator + (Number(kpiResult) * kpi.Weight) / 100;
      } else {
        // console.log(kpi.KPI_name);
        // console.log(kpi.Weight);
        return accumulator + kpi.Weight;
      }

    }, 0));


  }

  getFilteredTarget(kpi: any) {
    const QPlannedList = [];
    let target = 0;
    if (this.filterQ1 && kpi.Q1_P !== 0) {
      QPlannedList.push(kpi.Q1_P);
    }
    if (this.filterQ2 && kpi.Q2_P !== 0) {
      QPlannedList.push(kpi.Q2_P);
    }
    if (this.filterQ3 && kpi.Q3_P !== 0) {
      QPlannedList.push(kpi.Q3_P);
    }
    if (this.filterQ4 && kpi.Q4_P !== 0) {
      QPlannedList.push(kpi.Q4_P);
    }

    if (QPlannedList.length === 0) {
      return 0;
    }
    switch (kpi.kpiType) {

      case 1 :
        // accumulative
        target = QPlannedList.reduce((sum, value) => sum + value, 0);
        break;
      case 2 :
        // avg
        target = QPlannedList.length === 0 ? 0 : QPlannedList.reduce((sum, value) => sum + value, 0) / QPlannedList.length;
        break;
      case 3 :
        // last
        target = QPlannedList.length === 0 ? 0 : QPlannedList[QPlannedList.length - 1];
        break;
      case 4 :
        // max
        target = Math.max(...QPlannedList);
        break;
      case 5 :
        // min
        target = Math.min(...QPlannedList);
        break;
      default :
        return 0;

    }

    return target;

  }

  getFilteredResult(kpi: any) {
    const QResultList = [];
    let result = 0;
    if (this.filterQ1 && kpi.Q1_P !== 0) {
      QResultList.push(kpi.Q1_A);
    }
    if (this.filterQ2 && kpi.Q2_P !== 0) {
      QResultList.push(kpi.Q2_A);
    }
    if (this.filterQ3 && kpi.Q3_P !== 0) {
      QResultList.push(kpi.Q3_A);
    }
    if (this.filterQ4 && kpi.Q4_P !== 0) {
      QResultList.push(kpi.Q4_A);
    }

    if (QResultList.length === 0) {
      return 0;
    }
    switch (kpi.kpiType) {

      case 1 :
        // accumulative
        result = QResultList.reduce((sum, value) => sum + value, 0);
        break;
      case 2 :
        // avg
        result = QResultList.length === 0 ? 0 : QResultList.reduce((sum, value) => sum + value, 0) / QResultList.length;
        break;
      case 3 :
        // last
        result = QResultList.length === 0 ? 0 : QResultList[QResultList.length - 1];
        break;
      case 4 :
        // max
        result = Math.max(...QResultList);
        break;
      case 5 :
        // min
        result = Math.min(...QResultList);
        break;
      default :
        return 0;

    }

    return result;

  }

  getProjectWeightResult(x: any) {
    const result = this.getProjectFilteredPercentage(x);
    return result * x.WeigthValue / 100;
  }

  getProjectStrategyWeightResult(x: any) {
    const result = this.getProjectFilteredPercentage(x);
    return result * x.plannedStratigy / 100;
  }

  //#endregion
  getColorHex(project: any) {

    const result = this.getProjectFilteredPercentage(project);

    // get color from traffic light list

    const colorResult = this.trafficLightList.filter(a => {
        return result >= a.perc_from && result <= a.perc_to;
      }
    );
    if (!colorResult || colorResult.length === 0) {
      return "white";
    }

    if (colorResult[0].color == "green") {

      return "#7ae47a";
    } else if (colorResult[0].color == "red") {

      return "#e33939";
    } else if (colorResult[0].color == "yellow") {

      return "#eeee5c";
    } else if (colorResult[0].color == "gray") {

      return "#a2a2a2";
    }

  }

}
