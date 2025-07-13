import {Component, EventEmitter, Input, OnInit, Output,} from "@angular/core";
import {ProjectsAssessmentService} from "../Services/projects-assessment.service";
import {UserContextService} from "../Services/user-context.service";
import {UsersService} from "../Services/Users/users.service";
import {ProjectsService} from "../Services/Projects/projects.service";
import {OrganizationService} from "../Organization/organization.service";


declare var Highcharts: any;

@Component({
  selector: "app-strategy-comparison",
  templateUrl: "./strategy-comparison.component.html",
  styleUrls: ["./strategy-comparison.component.css"],
  providers: [UsersService, ProjectsService],
})
export class StrategyComparisonComponent implements OnInit {
  errorMessage: string;
  filesToUpload: Array<File>;
  selectedFileNames: string[] = [];
  public isLoadingData: Boolean = false;
  fileUploadVar: any;
  uploadResult: any;
  documentsList: File[];
  plannedWeight: number = 0;
  actualWeight: number = 0;
  successRate: number = 0;
  unitLst;
  ddlUnit: number = -1;
  @Input()
  AddOnFly = false;

  @Output()
  AddOnFlyAssessmentSave = new EventEmitter<string>();
  operationalSuccessRate = 0;
  strategicSuccessRate = 0;
  totalPlannedObjectiveAssessment = 0;
  totalActualObjectiveAssessment = 0;
  totalPlannedProjectAssessment = 0;
  totalActualProjectAssessment = 0;
  ddlSearchYearsList;
  ddlSearchObjectivesList;
  ddlSearchUnitsList;
  projectAssessmentList;

  ddlSearchYear = new Date().getFullYear() - 1;
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
  projectsAss: any = [];
  objectsAss: any = [];

  constructor(
    private projectsAssessmentService: ProjectsAssessmentService,
    private userContextService: UserContextService,
    private userService: UsersService,
    private projectService: ProjectsService,
    private organizationService: OrganizationService
  ) {

    Highcharts.setOptions({
      chart: {
        style: {
          fontFamily: 'Tajawal'
        }
      }
    });
    this.fillUnitLst();
    this.getApprovals();
    this.errorMessage = "";
    this.filesToUpload = [];
    this.selectedFileNames = [];
    this.uploadResult = "";
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
    this.searchProjectsAssessment();
  }

  fillUnitLst() {
    this.projectService.GetUnits(
      this.userContextService.CompanyID,
      this.userContextService.language
    ).subscribe((res) => {
      this.unitLst = res.Data;
      this.ddlUnit = 0;
    });
  }

  getApprovals() {
    this.organizationService.GetApprovalSetupByURL(
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
  }

  ddlYearChange(event) {

    console.log('event', event)
  }


  getProjectsAssessments() {
    this.projectsAssessmentService
      .getProjectAssessment(
        this.userContextService.CompanyID,
        this.ddlSearchYear,
        -1,
        0,
        this.userContextService.language,
      )
      .subscribe((res) => {
        if (res.IsError) {
          alert(res.ErrorMessage);
          return;
        }
        this.projectAssessmentList = res.Data;

        this.plannedWeight = res.Data.filter(a => {
          return a.planned_status !== "Declined";
        }).reduce((accumulator, object) => {
          return accumulator + object.plannedStratigy;
        }, 0);

        this.actualWeight = res.Data.filter(a => {
          return a.planned_status !== "Declined";
        }).reduce((total, x) => total + this.getProjectStrategyWeightResult(x), 0);

        this.totalActualProjectAssessment = this.actualWeight;
        this.totalPlannedProjectAssessment = this.plannedWeight;
        this.operationalSuccessRate = (this.actualWeight / this.plannedWeight) * 100;
        let mychart = Highcharts.chart('container', {
          chart: {
            type: 'column'
          },
          title: {
            text: 'Projects Assessment'
          },
          subtitle: {
            text: ''
          },
          xAxis: {
            categories: this.projectsAss.map(nameItem => {

              return nameItem.Name;


            }),
            crosshair: true
          },
          yAxis: {
            min: 0,
            title: {
              text: '%'
            }
          },
          tooltip: {
            headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
            pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
              '<td style="padding:0"><b>{point.y:.2f} mm</b></td></tr>',
            footerFormat: '</table>',
            shared: true,
            useHTML: true
          },
          plotOptions: {
            column: {
              pointPadding: 0.2,
              borderWidth: 0
            }
          },
          series: [{
            name: 'Planned Projects Assessment',
            data: this.projectsAss.map(nameItem => {
              return this.projectAssessmentList.filter(a => a.StratigicObjectiveId === nameItem.Id && a.planned_status !== "Declined")
                .reduce((accumulator, object) => {
                  return accumulator + object.plannedStratigy;
                }, 0.0);
            })

          }, {
            name: 'Actual Projects Assessment',
            data: this.projectsAss.map(nameItem => {
              return this.projectAssessmentList.filter(a => a.StratigicObjectiveId === nameItem.Id && a.planned_status !== "Declined")
                .reduce((total, x) => total + this.getProjectStrategyWeightResult(x), 0.0);
            })
          }]
        });
        let finalChart = Highcharts.chart('final-container', {
          chart: {
            type: 'column'
          },
          title: {
            text: 'Projects vs Objective Assessment'
          },
          subtitle: {
            text: ''
          },
          xAxis: {
            categories: this.projectsAss.map(nameItem => {

              return nameItem.Name;


            }),
            crosshair: true
          },
          yAxis: {
            min: 0,
            title: {
              text: '%'
            }
          },
          tooltip: {
            headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
            pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
              '<td style="padding:0"><b>{point.y:.2f} mm</b></td></tr>',
            footerFormat: '</table>',
            shared: true,
            useHTML: true
          },
          plotOptions: {
            column: {
              pointPadding: 0.2,
              borderWidth: 0
            }
          },
          colors: [
            '#434348',
            '#AA4643',
            '#89A54E', '#80699B', '#3D96AE',
            '#DB843D', '#d61277', '#A47D7C', '#7cac21'],
          series: [{
            name: 'Actual Projects Assessment',
            data: this.projectsAss.map(nameItem => {
              return this.projectAssessmentList.filter(a => a.StratigicObjectiveId === nameItem.Id && a.planned_status !== "Declined")
                .reduce((total, x) => total + this.getProjectStrategyWeightResult(x), 0);
            })

          }, {
            name: 'Actual Objective Assessment',
            data: this.objectsAss.map(nameItem => {
              return nameItem.TotalActuleWiegth;
            })
          }]
        });

        this.organizationService
          .getObjectiveCollectionSummary(
            this.userContextService.CompanyID,
            this.userContextService.language,
            this.ddlSearchYear,
            null
          )
          .subscribe((res) => {
            if (res.IsError == false) {
              this.showWeight = true;
              console.log('final objective test', res.Data);
              this.objectsAss = res.Data;
              this.totalPlannedObjectiveAssessment = res.Data.reduce((sum, item) => {
                return sum + item.TotalStrategyWiegth;
              }, 0);
              this.totalActualObjectiveAssessment = res.Data.reduce((sum, item) => {
                return sum + item.TotalActuleWiegth;
              }, 0);
              this.strategicSuccessRate = res.Data.reduce((value, item) => {
                return (item.TotalActuleWiegth
                  + value);
              }, 0);
              let mychart1 = Highcharts.chart('container-1', {
                chart: {
                  type: 'column'
                },
                title: {
                  text: 'Objective Assessment'
                },
                subtitle: {
                  text: ''
                },
                xAxis: {
                  categories: res.Data.map(nameItem => {
                    return nameItem.name;
                  }),
                  crosshair: true
                },
                yAxis: {
                  min: 0,
                  title: {
                    text: '%'
                  }
                },
                colors: [
                  '#7fc7fd',
                  '#AA4643',
                  '#89A54E', '#80699B', '#3D96AE',
                  '#DB843D', '#d61277', '#A47D7C', '#7cac21']
                , tooltip: {
                  headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
                  pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                    '<td style="padding:0"><b>{point.y:.2f} mm</b></td></tr>',
                  footerFormat: '</table>',
                  shared: true,
                  useHTML: true
                },
                plotOptions: {
                  column: {
                    pointPadding: 0.2,
                    borderWidth: 0
                  }
                },
                series: [{
                  name: 'Planned Objective Assessment',
                  data: this.objectsAss.map(nameItem => {
                    return nameItem.TotalStrategyWiegth;
                  })
                },
                  {
                    name: 'Actual Objective Assessment',
                    data: this.objectsAss.map(nameItem => {

                      return nameItem.TotalActuleWiegth;


                    })

                  }]
              });


            } else {
              this.showWeight = false;
              alert(res.ErrorMessage);
            }
          });


      });
  }

  setDefaultAddOnFly(projectID) {
    this.onFlyProjectID = projectID;
    this.perObjectiveView = false;
    this.perProjectView = true;

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
    this.notShowActual = true;
    this.projectsAssessmentService
      .GetPlannedWeightForEachObjective(
        this.userContextService.CompanyID,
        this.ddlSearchYear,
        this.ddlSearchUnit,
        this.userContextService.language,
        null,
        null,
        null
      )
      .subscribe((res) => {
        if (res.IsError == false) {
          this.showWeight = true;
          console.log('final test', res.Data);
          this.projectsAss = res.Data;
          this.getProjectsAssessments();
        } else {
          this.showWeight = false;
          alert(res.ErrorMessage);
        }
      });
  }

  getProjectSuccessPercetage(x: any) {

    if (x.KPIs && x.KPIs.length > 0) {
      return (x.KPIs.reduce((accumulator, kpi) => {
          // console.log(kpi);
          if (kpi.BetterUpDown === 2) {
            return accumulator + (kpi.AnnualResult === 0 ? 0 : (kpi.Target / kpi.AnnualResult) * kpi.Weight);

          } else {
            return accumulator + (kpi.Target === 0 ? 0 : (kpi.AnnualResult / kpi.Target) * kpi.Weight);
          }
        }, 0)

      );
    }
    return 0;
  }


  getKPISuccessPercentage(kpi: any) {

    const target = this.getFilteredTarget(kpi);
    const result = this.getFilteredResult(kpi);
    if (kpi.BetterUpDown === 2) {
      return result === 0 && target === 0 ? '-' :
        (((result === 0 ? 1.20 : (target / result) > 1.20 ? 1.20 : (target / result)) * 100).toFixed(1));
    } else {
      return (target === 0 ? '-' : (((result / target) > 1.20 ? 1.20 : (result / target)) * 100).toFixed(1));
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
    if (kpi.Q1_P !== 0) {
      QPlannedList.push(kpi.Q1_P);
    }
    if (kpi.Q2_P !== 0) {
      QPlannedList.push(kpi.Q2_P);
    }
    if (kpi.Q3_P !== 0) {
      QPlannedList.push(kpi.Q3_P);
    }
    if (kpi.Q4_P !== 0) {
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
    if (kpi.Q1_P !== 0) {
      QResultList.push(kpi.Q1_A);
    }
    if (kpi.Q2_P !== 0) {
      QResultList.push(kpi.Q2_A);
    }
    if (kpi.Q3_P !== 0) {
      QResultList.push(kpi.Q3_A);
    }
    if (kpi.Q4_P !== 0) {
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

  // }
  fillDDLSearchUnits() {
    this.projectsAssessmentService
      .getUnites(this.userContextService.CompanyID)
      .subscribe((res) => {
        if (res.IsError) {
          alert(res.ErrorMessage);
          return null;
        }
        this.ddlSearchUnitsList = res.Data;
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
        });
      }
      this.searchProjectsAssessment();

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
    //Clear Uploaded Files result message
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

  ConfirmResult(projectID) {
    this.organizationService.UpdateProjectAssessmentStatus(
      projectID,
      2
    ).subscribe((res) => {
      if (res.IsError) {
        alert(res.ErrorMessage + ",1");
        return;
      }
      alert("0");
      this.searchProjectsAssessment();
    });
  }

  DeclineResult(projectID) {
    this.organizationService.UpdateProjectAssessmentStatus(
      projectID,
      3
    ).subscribe((res) => {
      if (res.IsError) {
        alert(res.ErrorMessage + ",1");
        return;
      }
      alert("0");
      this.searchProjectsAssessment();
    });
  }

  //#endregion
}
