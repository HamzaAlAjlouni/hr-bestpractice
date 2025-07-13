import {Component, EventEmitter, Input, OnInit, Output} from "@angular/core";
import {ProjectsAssessmentService} from "../Services/projects-assessment.service";
import {UserContextService} from "../Services/user-context.service";
import {UsersService} from "../Services/Users/users.service";
import {ProjectsService} from "../Services/Projects/projects.service";
import {OrganizationService} from "../Organization/organization.service";
import {QuartersService} from "../quaters-activation/service/quarters.service";
import {SettingsService} from "../settings/settings.service";

@Component({
  selector: "app-projects-assessment",
  templateUrl: "./projects-assessment.component.html",
  styleUrls: ["./projects-assessment.component.css"],
  providers: [UsersService, ProjectsService],
})
export class ProjectsAssessmentComponent implements OnInit {
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
  approvalNote: any;
  selectedId;
  currentProject;
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
  ddlAssessmentStatus;
  ddlSearchObjectivesList;
  ddlSearchUnitsList;
  projectAssessmentList;
  ddlSearchYear;
  ddlSearchObjective = 0;
  ddlSearchUnit = -1;
  formRow;
  PageResources = [];
  assessment_status;
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

      this.actualWeight = this.projectAssessmentList.filter(a => {
        return a.planned_status !== "Declined";
      }).reduce((total, x) => total + this.getProjectStrategyWeightResult(x), 0);
      this.successRate = (this.actualWeight / this.plannedWeight) * 100;

      const progressPlantWeight = this.projectAssessmentList.filter(a => {
        return a.planned_status !== "Declined";
      }).reduce((accumulator, object) => {

        const r = this.getProjectStrategyWeightResult(object);

        return r === 0 ? accumulator : accumulator + object.plannedStratigy;
      }, 0);

      const progressActualWeight = this.projectAssessmentList.filter(a => {
        return a.planned_status !== "Declined";
      }).reduce((total, x) => {
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
      console.log("rev", res.Data);
      this.is_reveiwer_user = res.Data.filter(a => a.reviewing_user.toLocaleLowerCase() === this.userContextService.Username.toLocaleLowerCase()).length > 0;
      console.log("rev", this.is_reveiwer_user);

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
    this.searchProjectsAssessment();
  }

  setObjDefaultAddOnFly(ObjectiveID) {
    this.onFlyObjID = ObjectiveID;
    this.perObjectiveView = true;
    this.perProjectView = false;
    this.searchProjectsAssessment();
  }

  // selectStrategicObjective() {
  //   if (this.ddlSearchObjective != 0) {
  //     this.notShowActual = false;
  //     this.objectiveSelected = this.ddlSearchObjectivesList.find(
  //       (element) => element.id == this.ddlSearchObjective
  //     );
  //     console.log('objectiveSelected: ',this.objectiveSelected)
  //     this.plannedWeight = this.objectiveSelected.Weight;
  //   }

  searchProjectsAssessment() {

    this.loadTrafficLight();
    this.notShowActual = true;
    this.projectsAssessmentService.getProjectAssessment(
      this.userContextService.CompanyID,
      this.ddlSearchYear,
      this.ddlSearchUnit,
      this.ddlSearchObjective,
      this.userContextService.language
    )
      .subscribe((res) => {
        if (res.IsError) {
          alert(res.ErrorMessage);
          return;
        }
        console.log('res.Datares.Datares.Data', res.Data);
        this.projectAssessmentList = this.is_reveiwer_user
        || this.userContextService.UnitId === 0
        || this.userContextService.Role === "Admin" ?
          res.Data.filter(a => a.planned_status === "Confirmed")
          : res.Data.filter(a => a.UnitId === this.userContextService.UnitId).filter(a => a.planned_status === "Confirmed");
        this.projectAssessmentList = this.ddlAssessmentStatus ?
          this.projectAssessmentList.filter(a => a.assessment_status == this.ddlAssessmentStatus)
          : this.projectAssessmentList;
        this.projectsAssessmentService
          .GetPlannedAndActualWeightForAll(
            this.userContextService.CompanyID,
            this.ddlSearchYear,
            this.ddlSearchUnit,
            this.ddlSearchObjective,
            this.userContextService.language,
            null,
            null
          )
          .subscribe((res) => {
            if (res.IsError === false) {
              this.showWeight = true;
              this.updateTotalWeightValues();
            } else {
              this.showWeight = false;
              console.log(res.ErrorMessage);
            }
          });
      });


    // this.projectsAssessmentService
    //   .GetPlannedWeightForEachObjective(
    //     this.userContextService.CompanyID,
    //     this.ddlSearchYear,
    //     this.ddlSearchUnit,
    //     this.userContextService.language,
    //     null,
    //     null,
    //     null
    //   )
    //   .subscribe((res) => {
    //     if (res.IsError == false) {
    //       this.showWeight = true;
    //       console.log('final test', res.Data);
    //
    //     } else {
    //       // this.showWeight = false;
    //       // alert(res.ErrorMessage);
    //     }
    //   });
  }

  hasEditPermission() {
    return true;
  }

  // }
  fillDDLSearchUnits() {
    this.projectsAssessmentService
      .getUnites(this.userContextService.CompanyID)
      .subscribe((res) => {
        if (res.IsError) {
          alert(res.ErrorMessage);
          return null;
        }
        this.ddlSearchUnitsList = this.is_reveiwer_user || this.userContextService.Role === "Admin" ? res.Data : res.Data.filter(a => {
          return a.ID === this.userContextService.UnitId;
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
        this.searchProjectsAssessment();
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

  fileChangeEvent(fileInput: any) {
    // Clear Uploaded Files result message
    this.uploadResult = "";
    this.filesToUpload = <Array<File>>fileInput.target.files;

    for (let i = 0; i < this.filesToUpload.length; i++) {
      this.selectedFileNames.push(this.filesToUpload[i].name);
    }
  }

  cancelUpload() {
    this.filesToUpload = [];
    this.fileUploadVar.nativeElement.value = "";
    this.selectedFileNames = [];
    this.uploadResult = "";
    this.errorMessage = "";
  }

  addFile(projectID, file, docID) {
    // "Are you sure, you want to upload this evident ?"
    if (confirm(this.GetLocalResourceObject("confirmUploadEvident"))) {
      this.projectsAssessmentService
        .uploadFiles(file, docID, projectID, this.userContextService.Username)
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

  ConfirmResult() {
    this.OrganizationService.UpdateProjectAssessmentStatus(
      this.selectedId,
      2,
      this.userContextService.Username,
      this.approvalNote
    ).subscribe((res) => {
      if (res.IsError) {
        alert(res.ErrorMessage + ",1");
        return;
      }
      alert("0");
      this.approvalNote = "";
      this.closeAssessmentApprovalModal();
      this.searchProjectsAssessment();

    });
  }

  DeclineResult() {
    this.OrganizationService.UpdateProjectAssessmentStatus(
      this.selectedId,
      3,
      this.userContextService.Username,
      this.approvalNote
    ).subscribe((res) => {
      if (res.IsError) {
        alert(res.ErrorMessage + ",1");
        return;
      }
      alert("0");
      this.approvalNote = "";
      this.closeAssessmentApprovalModal();
      this.searchProjectsAssessment();
    });
  }

  getProjectSuccessPercetage(x: any) {

    if (x.KPIs && x.KPIs.length > 0) {
      if (!this.filterQ1 && !this.filterQ2 && !this.filterQ3 && !this.filterQ4) {
        return 0;
      }
      if (this.filterQ1 && this.filterQ2 && this.filterQ3 && this.filterQ4) {

        return (x.KPIs.reduce((accumulator, kpi) => {
            // console.log(kpi);
            if (kpi.BetterUpDown === 2) {
              return accumulator + (kpi.AnnualResult === 0 ? 0 : (kpi.Target / kpi.AnnualResult) * kpi.Weight);

            } else {
              return accumulator + (kpi.Target === 0 ? 0 : (kpi.AnnualResult / kpi.Target) * kpi.Weight);
            }
          }, 0)

        );
      } else {

        return (x.KPIs.reduce((accumulator, kpi) => {
            // console.log(kpi);
            const target = this.getFilteredTarget(kpi);
            const result = this.getFilteredResult(kpi);

            if (x.ID === 189) {
              console.log(kpi.KPI_name, ' target ', target);
              console.log(kpi.KPI_name, ' result ', result);
              console.log(kpi.KPI_name, ' kpi  weight ', kpi.Weight);
              console.log(kpi.KPI_name, ' up / down ', kpi.BetterUpDown);
            }
            if (kpi.BetterUpDown === 2) {
              return accumulator + (result === 0 ? 0 : (target / result) * kpi.Weight);
            } else {
              return accumulator + (target === 0 ? 0 : (result / target) * kpi.Weight);
            }
          }, 0)
        );
      }
      // else {
      //   // get filtered Qs
      //
      //   const kpisFilterResult = [];
      //   for (const i of x.KPIs) {
      //     const q1P = this.filterQ1 ? i.Q1_P : 0;
      //     const q2P = this.filterQ2 ? i.Q2_P : 0;
      //     const q3P = this.filterQ3 ? i.Q3_P : 0;
      //     const q4P = this.filterQ4 ? i.Q4_P : 0;
      //
      //     const q1A = this.filterQ1 ? i.Q1_A : 0;
      //     const q2A = this.filterQ2 ? i.Q2_A : 0;
      //     const q3A = this.filterQ3 ? i.Q3_A : 0;
      //     const q4A = this.filterQ4 ? i.Q4_A : 0;
      //
      //
      //     const nonZeroValues = [];
      //     console.log('i.betterUpDown', i.BetterUpDown);
      //     if (i.BetterUpDown === 1) {
      //
      //       const pushQ1 = q1P !== 0 && nonZeroValues.push(q1A / q1P);
      //       const pushQ2 = q2P !== 0 && nonZeroValues.push(q1A / q2P);
      //       const pushQ3 = q3P !== 0 && nonZeroValues.push(q1A / q3P);
      //       const pushQ4 = q4P !== 0 && nonZeroValues.push(q1A / q4P);
      //       if (i.ID === 362) {
      //
      //         console.log(i.KPI_name, ' values ', q1A, '/', q1P);
      //         console.log(i.KPI_name, ' values ', q2A, '/', q2P);
      //         console.log(i.KPI_name, ' values ', q3A, '/', q3P);
      //         console.log(i.KPI_name, ' values ', q4A, '/', q4P);
      //       }
      //     } else {
      //
      //       const pushQ1 = q1A !== 0 && nonZeroValues.push(q1P / q1A);
      //       const pushQ2 = q2A !== 0 && nonZeroValues.push(q1P / q1A);
      //       const pushQ3 = q3A !== 0 && nonZeroValues.push(q1P / q1A);
      //       const pushQ4 = q4A !== 0 && nonZeroValues.push(q1P / q1A);
      //       if (i.ID === 362) {
      //
      //         console.log(i.KPI_name, ' values ', q1P, '/', q1A);
      //         console.log(i.KPI_name, ' values ', q2P, '/', q2A);
      //         console.log(i.KPI_name, ' values ', q3P, '/', q3A);
      //         console.log(i.KPI_name, ' values ', q4P, '/', q4A);
      //       }
      //     }
      //
      //     // check kpi type
      //     if (i.kpiType === 1) {
      //       const sum = nonZeroValues.reduce((sumValue, value) => sumValue + value, 0);
      //       if (sum !== 0) {
      //         kpisFilterResult.push(sum);
      //       }
      //
      //     } else if (i.kpiType === 2) {
      //       // avg
      //       console.log('// avg', nonZeroValues);
      //
      //       const sumOfNonZeroValues = nonZeroValues.reduce((sum, value) => sum + value, 0);
      //       const countOfNonZeroValues = nonZeroValues.length;
      //       const avg = countOfNonZeroValues === 0 ? 0 : sumOfNonZeroValues / countOfNonZeroValues;
      //       if (avg !== 0) {
      //         kpisFilterResult.push(avg);
      //       }
      //     } else if (i.kpiType === 3) {
      //       // last
      //       // console.log('// last');
      //       const last = nonZeroValues && nonZeroValues.length > 0 ? nonZeroValues[nonZeroValues.length - 1] : 0;
      //
      //       if (last !== 0) {
      //         kpisFilterResult.push(last);
      //
      //       }
      //     } else if (i.kpiType === 4) {
      //       // max
      //       // console.log('// max');
      //       kpisFilterResult.push(nonZeroValues && nonZeroValues.length > 0 ? Math.max(...nonZeroValues) : 0);
      //     } else if (i.kpiType === 5) {
      //       // min
      //       // console.log('// min');
      //       kpisFilterResult.push(nonZeroValues && nonZeroValues.length > 0 ? Math.min(...nonZeroValues) : 0);
      //     }
      //   }
      //   // console.log(kpisFilterResult);
      //   const nonZerokpisFilterResult = kpisFilterResult.filter(a => a !== 0);
      //   const totalSum = nonZerokpisFilterResult && nonZerokpisFilterResult.length > 0 ?
      //     nonZerokpisFilterResult.reduce((sum, value) => sum + value, 0) : 0;
      //
      //   return kpisFilterResult.length === 0 ? 0 :
      //     totalSum / kpisFilterResult.length > 1.20 ? 1.20 * 100 : totalSum / kpisFilterResult.length * 100;
      //
      // }

    }
    return 0;
  }


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
          console.log(kpi.KPI_name);
          console.log(kpi.Weight, Number(kpiResult), (Number(kpiResult) * kpi.Weight) / 100);
        }
        return accumulator + (Number(kpiResult) * kpi.Weight) / 100;
      } else {
        console.log(kpi.KPI_name);
        console.log(kpi.Weight);
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

  closeAssessmentApprovalModal() {
    document.getElementById("approvalModal").className = "modal fade";
    document.getElementById("approvalModal").style.display = "none";
  }

  openAssesmentApprovalModal(x) {
    this.selectedId = x.ID;
    this.currentProject = x;
    document.getElementById("approvalModal").style.display = "block";
    document.getElementById("approvalModal").className = "modal fade in";

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

}
