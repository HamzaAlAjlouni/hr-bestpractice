import {Component, OnInit, ViewChild} from "@angular/core";
import {EmployeeService} from "../Organization/employees/employee.service";
import {UserContextService} from "../Services/user-context.service";
import {ProjectsService} from "../Services/Projects/projects.service";
import {ActionPlanService} from "../action-plan/action-plan.service";
import {UsersService} from "../Services/Users/users.service";
import {OrganizationService} from "../Organization/organization.service";
import {UnitsperformanceService} from "./unitsperformance.service";
import {DashBoardService} from "../Services/DashBoardsService";
import {ProjectsAssessmentService} from "../Services/projects-assessment.service";
import {SettingsService} from "../settings/settings.service";

declare var Highcharts: any;

@Component({
  selector: "app-units-performance-monitor",
  templateUrl: "./units-performance-monitor.component.html",
  styleUrls: ["./units-performance-monitor.component.css"],
  providers: [UnitsperformanceService],
})
export class UnitsPerformanceMonitorComponent implements OnInit {
  PageResources;
  ddlYear;
  yearLst;
  ddlUnitSearch;
  unitLst;
  AddOnFly;
  projectAssessmentList;
  allEmployeeQouta;
  quotaLevels;
  showActualCol = 1;

  @ViewChild("gvUnits", {read: false, static: false}) gvUnits;

  @ViewChild("gvEmpDistribution", {read: false, static: false})
  gvEmpDistribution;

  @ViewChild("gvUnitQouta", {read: false, static: false}) gvUnitQouta;
  allUnitListDetails = [];
  unitName: string = '';

  constructor(
    private empService: EmployeeService,
    private userContext: UserContextService,
    private projService: ProjectsService,
    private dashBoardService: DashBoardService,
    private OrganizationService: OrganizationService,
    private actionPlansService: ActionPlanService,
    private settingsService: SettingsService,
    private userService: UsersService,
    private projectsAssessmentService: ProjectsAssessmentService,
    private unitPerService: UnitsperformanceService
  ) {

    Highcharts.setOptions({
      chart: {
        style: {
          fontFamily: 'Tajawal'
        }
      }
    });
    this.LoadYears();
    this.fillUnitLst();

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

  gvEmpDistributionHandler(event) {
  }

  showActualColChange(event) {

    this.showActualCol = event.target.value;
  }

  getProjectsAssessments() {
    this.projectsAssessmentService
      .getProjectAssessment(
        this.userContext.CompanyID,
        this.ddlYear,
        this.ddlUnitSearch == null ? -1 : this.ddlUnitSearch,
        0,
        this.userContext.language,
      )
      .subscribe((res) => {
        if (res.IsError) {
          alert(res.ErrorMessage);
          return;
        }
        this.projectAssessmentList = res.Data;
        this.allUnitListDetails = this.allUnitListDetails.map(item => {
            const unitWeight = this.projectAssessmentList && this.projectAssessmentList.length > 0 ?
              this.projectAssessmentList.filter(a => a.UnitId === item.ID && a.planned_status !== "Declined")
                .reduce((total, x) => total + this.getProjectStrategyWeightResult(x), 0)
              : item.UnitActualWeight;

            const unitPlannedWeight = this.projectAssessmentList && this.projectAssessmentList.length > 0 ?
              this.projectAssessmentList.filter(a => a.UnitId === item.ID && a.planned_status !== "Declined")
                .reduce((total, x) => total + x.plannedStratigy, 0)
              : item.UnitPlanWeight;
            const successRate = unitPlannedWeight !== 0 ? (unitWeight / unitPlannedWeight) * 100 : item.SuccessRate;


            return {
              ...item,
              UnitActualWeight: Number(unitWeight.toFixed(2)),
              SuccessRate: Number(successRate.toFixed(2)),
              UnitPlanWeight: Number(unitPlannedWeight.toFixed(2)),
              UnitWeightGap: (unitWeight - unitPlannedWeight) > 0 ? 0 : Number(Math.abs(unitWeight - unitPlannedWeight).toFixed(2)),
              PlusWeight: (unitWeight - unitPlannedWeight) > 0 ? Number(Math.abs(unitWeight - unitPlannedWeight).toFixed(2)) : 0,
              FTE: Number((item.UnitEmp - (Number(unitPlannedWeight.toFixed(2)) / 100 * item.sumUnitEmp)).toFixed(2))
            }
              ;
          }
        );
        const units = this.allUnitListDetails.map(a => a.ID);
        const successRates = this.allUnitListDetails.map(a => a.SuccessRate);
        this.GetNewEmployeeDistribution(units, successRates);
        let cols = [
          {HeaderText: "Name", DataField: "UnitName"},
          {HeaderText: "Employees Count", DataField: "UnitEmp", Sum: "UnitEmp"},
          {
            HeaderText: "Planned Weight %",
            DataField: "UnitPlanWeight",
            Sum: "UnitPlanWeight",
          },
          {
            HeaderText: "Actual Weight %",
            DataField: "UnitActualWeight",
            Sum: "UnitActualWeight",
          },
          {
            HeaderText: "Success Rate %",
            DataField: "SuccessRate",
          },
          {
            HeaderText: "Negative Gap %",
            DataField: "UnitWeightGap",
            Sum: "UnitWeightGap",
          },
          {
            HeaderText: "Positive Gap %",
            DataField: "PlusWeight",
            Sum: "PlusWeight",
          },
          {
            HeaderText: "FTE",
            DataField: "FTE",
            Sum: "FTE",
          },
        ];
        const plannedCols = [
          {HeaderText: "Name", DataField: "UnitName"},
          {HeaderText: "Employees Count", DataField: "UnitEmp", Sum: "UnitEmp"},
          {
            HeaderText: "Planned Weight %",
            DataField: "UnitPlanWeight",
            Sum: "UnitPlanWeight",
          },
          {
            HeaderText: "Success Rate %",
            DataField: "SuccessRate",
            Avg: "SuccessRate",
          },
          {
            HeaderText: "Negative Gap %",
            DataField: "UnitWeightGap",
            Sum: "UnitWeightGap",
          },
          {
            HeaderText: "Positive Gap %",
            DataField: "PlusWeight",
            Sum: "PlusWeight",
          },
          {
            HeaderText: "FTE",
            DataField: "FTE",
            Sum: "FTE",
          },
        ];
        let actions = [
          {
            title: "Details",
            DataValue: "ID",
            Icon_Awesome: "fa fa-list-alt",
            Action: "Details",
          },
        ];
        this.gvUnits.bind(this.showActualCol == 1 ? cols : plannedCols, this.allUnitListDetails, "gvActionPlans", actions);


        let gvUnitChart = Highcharts.chart('gvUnitChartContainer', {
          chart: {
            type: 'column',
            zoomType: 'y'
          },
          title: {
            text: 'Units Performance - Comparison List Chart'
          },
          subtitle: {
            text: ''
          },
          xAxis: {
            categories: this.allUnitListDetails.map(item => {

              return item.UnitName;

            }),
            title: {
              text: null
            }
          },
          yAxis: {
            min: 0,
            title: {
              text: '%',
              align: 'high'
            },
            labels: {
              overflow: 'justify'
            }
          },
          tooltip: {
            valueSuffix: ' %'
          },
          plotOptions: {
            bar: {
              dataLabels: {
                enabled: true
              }
            }
          },
          legend: {
            layout: 'vertical',
            align: 'right',
            verticalAlign: 'top',
            x: -180,
            y: 100,
            floating: true,
            borderWidth: 1,
            backgroundColor:
              Highcharts.defaultOptions.legend.backgroundColor || '#FFFFFF',
            shadow: true
          },
          credits: {
            enabled: false
          },
          series: [{
            name: 'Planned Weight %',
            data: this.allUnitListDetails.map(obj => {
              return obj.UnitPlanWeight;
            }, 0)
          }, {
            name: 'Actual Weight %',
            data: this.allUnitListDetails.map(item => {
              return item.UnitActualWeight;
            }, 0)
          },
            //
            //   {
            //   name: 'FTE',
            //   data: res.Data.map(item => {
            //
            //     return item.FTE;
            //
            //   })
            // }
            //
          ]
        });

        const plannedWeight = this.projectAssessmentList.filter(a => {
          return a.planned_status !== "Declined";
        }).reduce((accumulator, object) => {
          return accumulator + object.plannedStratigy;
        }, 0).toFixed(2);
        const actualWeight = this.projectAssessmentList.filter(a => {
          return a.planned_status !== "Declined";
        }).reduce((total, x) => total + this.getProjectStrategyWeightResult(x), 0).toFixed(2);
        const gvUnitChartSingle = Highcharts.chart('gvUnitChartSingleContainer', {
          chart: {
            type: 'bar'
          },
          title: {
            text: 'Units Performance - Comparison List Chart'
          },
          subtitle: {
            text: ''
          },
          xAxis: {
            categories: ["All Units"],
            title: {
              text: null
            }
          },
          yAxis: {
            min: 0,
            title: {
              text: '%',
              align: 'high'
            },
            labels: {
              overflow: 'justify'
            }
          },
          tooltip: {
            valueSuffix: ' '
          },
          plotOptions: {
            bar: {
              dataLabels: {
                enabled: true
              }
            }
          },
          legend: {
            layout: 'vertical',
            align: 'right',
            verticalAlign: 'top',
            x: -40,
            y: 270,
            floating: true,
            borderWidth: 1,
            backgroundColor:
              Highcharts.defaultOptions.legend.backgroundColor || '#FFFFFF',
            shadow: true
          },
          credits: {
            enabled: false
          },
          series: [{
            name: 'Planned Weight %',
            data: [Number(plannedWeight)]
          },
            {
              name: 'Actual Weight %',
              data: [Number(actualWeight)]
            },
            {
              name: 'FTE',
              data: [0]
            }]
        });
      });


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

      console.log(a.KPI_name, kpiResult);
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
        return accumulator + (Number(kpiResult) * kpi.Weight) / 100;
      } else {
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

  fillUnitLst() {
    this.projService
      .GetUnits(this.userContext.CompanyID, this.userContext.language)
      .subscribe((res) => {
        this.unitLst = res.Data;
        this.ddlUnitSearch = null;
      });
  }

  ngOnInit() {
  }

  LoadYears() {


    this.projService.GetYears().subscribe((res) => {
      this.yearLst = res.Data;
      console.log('get years results', res.Data);

      if (res.Data.length > 0) {
        this.ddlYear = res.Data[res.Data.length - 1].id;
        this.SearchUnits();
        this.LoadAllPerformanceLevelQuotaView();

      }
    });
  }

  GetEmployeeDistribution() {
    // this.unitPerService
    //   .GetEmployeeDistribution(this.ddlYear, this.userContext.CompanyID)
    //   .subscribe((res) => {
    //     if (res.IsError) {
    //       alert(res.ErrorMessage + ",1");
    //       return;
    //     }
    //     let cols = [
    //       {HeaderText: "Level Name", DataField: "lvl_name"},
    //       {HeaderText: "Level Number", DataField: "lvl_number"},
    //       {HeaderText: "Level Percentage %", DataField: "lvl_percent"},
    //       {
    //         HeaderText: "Employee Level Count",
    //         DataField: "empCount",
    //         Sum: "empCount",
    //       },
    //     ];
    //     console.log(res.Data);
    //     this.gvEmpDistribution.bind(cols, res.Data, "gvEmpDistribution", null);
    //     // let newchart = Highcharts.chart('container500', {
    //     //   chart: {
    //     //     type: 'column'
    //     //   },
    //     //   title: {
    //     //     text: 'Employees Performance Distribution'
    //     //   },
    //     //   subtitle: {
    //     //     text: ''
    //     //   },
    //     //   xAxis: {
    //     //     categories: res.Data.map(item => {
    //     //
    //     //       return item.lvl_name;
    //     //
    //     //     }),
    //     //     crosshair: true
    //     //   },
    //     //   yAxis: {
    //     //     min: 0,
    //     //     title: {
    //     //       text: 'Employee Level Count'
    //     //     }
    //     //   },
    //     //   tooltip: {
    //     //     headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
    //     //     pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
    //     //       '<td style="padding:0"><b>{point.y:.1f} Employee</b></td></tr>',
    //     //     footerFormat: '</table>',
    //     //     shared: true,
    //     //     useHTML: true
    //     //   },
    //     //   plotOptions: {
    //     //     column: {
    //     //       pointPadding: 0.2,
    //     //       borderWidth: 0
    //     //     }
    //     //   },
    //     //   series: [{
    //     //     name: 'Performance Level',
    //     //     data: res.Data.map(item => {
    //     //
    //     //       return item.empCount;
    //     //
    //     //     })
    //     //
    //     //   }]
    //     // });
    //
    //
    //     let line = Highcharts.chart('container500', {
    //       chart: {
    //         type: 'area'
    //       },
    //       accessibility: {
    //         description: ''
    //       },
    //       title: {
    //         text: 'Employees Performance Distribution'
    //       },
    //       subtitle: {
    //         text: ''
    //       },
    //       xAxis: {
    //         categories: res.Data.map(item => {
    //
    //           return item.lvl_name;
    //
    //         }),
    //         crosshair: true,
    //       },
    //       yAxis: {
    //         min: 0,
    //         title: {
    //           text: 'Employee Level Count',
    //           align: 'high'
    //         },
    //         labels: {
    //           overflow: 'justify'
    //         }
    //       },
    //       tooltip: {
    //         pointFormat: '{series.name} had stockpiled <b>{point.y:,.0f}</b><br/>warheads in {point.x}'
    //       },
    //       plotOptions: {
    //         bar: {
    //           dataLabels: {
    //             enabled: true
    //           }
    //         }
    //       },
    //       series: [
    //
    //         {
    //           name: 'Performance Level',
    //           data: res.Data.map(item => {
    //
    //             return ((item.empCount / 190.00) * 100);
    //
    //           }),
    //         }]
    //     });
    //   });

    this.unitPerService
      .GetAllEmployeeQouta(this.ddlYear, this.userContext.CompanyID, 0)
      .subscribe((res) => {
        if (res.IsError) {
          alert(res.ErrorMessage + ",1");
          return;
        }
        let employeeCount = res.Data.reduce((total, item) => total + item.empCount, 0);
        res.Data = res.Data.map(obj => ({
          ...obj,
          unitEmpAfterAssessmentPercent: ((obj.unitEmpAfterAssessment / employeeCount) * 100).toFixed(1)
        }));

        let cols = [
          {HeaderText: "Level Name", DataField: "lvl_name"},
          {HeaderText: "Level Number", DataField: "lvl_number"},
          {HeaderText: "Level Percentage %", DataField: "lvl_percent"}, {
            HeaderText: "Planned Quota",
            DataField: "empCount",
            Sum: "empCount",
          },

          {
            HeaderText: "Actual Quota",
            DataField: "unitEmpAfterAssessment",
            Sum: "unitEmpAfterAssessment",
          },
          {
            HeaderText: "Actual Percentage %",
            DataField: "unitEmpAfterAssessmentPercent",
          }
        ];
        let plannedCols = [
          {HeaderText: "Level Name", DataField: "lvl_name"},
          {HeaderText: "Level Number", DataField: "lvl_number"},
          {HeaderText: "Level Percentage %", DataField: "lvl_percent"},
          {
            HeaderText: "Planned Quota",
            DataField: "empCount",
            Sum: "empCount",
          },


        ];
        console.log('gvUnitQouta gvUnitQouta', res.Data);

        console.log('static data', res.Data[0]);
        // res.Data[0].unitEmpAfterAssessmentPercent = parseFloat(((res.Data[0].unitEmpAfterAssessment / employeeCount) * 100) + '').toFixed(1);
        // res.Data[1].unitEmpAfterAssessmentPercent = parseFloat(((res.Data[1].unitEmpAfterAssessment / employeeCount) * 100) + '').toFixed(1);
        // res.Data[2].unitEmpAfterAssessmentPercent = parseFloat(((res.Data[2].unitEmpAfterAssessment / employeeCount) * 100) + '').toFixed(1);
        // res.Data[3].unitEmpAfterAssessmentPercent = parseFloat(((res.Data[3].unitEmpAfterAssessment / employeeCount) * 100) + '').toFixed(1);
        // res.Data[4].unitEmpAfterAssessmentPercent = parseFloat(((res.Data[4].unitEmpAfterAssessment / employeeCount) * 100) + '').toFixed(1);


        this.gvEmpDistribution.bind(this.showActualCol == 1 ? cols : plannedCols, res.Data, "gvEmpDistribution", null);

        // let line = Highcharts.chart('container500', {
        //   chart: {
        //     type: 'area'
        //   },
        //   accessibility: {
        //     description: ''
        //   },
        //   title: {
        //     text: 'Employees Performance Distribution'
        //   },
        //   subtitle: {
        //     text: ''
        //   },
        //   xAxis: {
        //     categories: res.Data.map(item => {
        //
        //       return item.lvl_name;
        //
        //     }),
        //     crosshair: true,
        //   },
        //   yAxis: {
        //     min: 0,
        //     title: {
        //       text: 'Employee Level Count',
        //       align: 'high'
        //     },
        //     labels: {
        //       overflow: 'justify'
        //     }
        //   },
        //   tooltip: {
        //     pointFormat: '{series.name} had stockpiled <b>{point.y:,.0f}</b><br/>warheads in {point.x}'
        //   },
        //   plotOptions: {
        //     bar: {
        //       dataLabels: {
        //         enabled: true
        //       }
        //     }
        //   },
        //   series: [
        //
        //     {
        //       name: 'Performance Level',
        //       data: res.Data.map(item => {
        //
        //         return ((item.empCount / 190.00) * 100);
        //
        //       }),
        //     }, {
        //       name: 'Actual Level',
        //       data: [
        //         (res.Data[0].unitEmpAfterAssessment / 97) * 100,
        //         (res.Data[1].unitEmpAfterAssessment / 97) * 100,
        //         (res.Data[2].unitEmpAfterAssessment / 97) * 100,
        //         (res.Data[3].unitEmpAfterAssessment / 97) * 100,
        //         (res.Data[4].unitEmpAfterAssessment / 97) * 100,
        //
        //       ]
        //     }
        //
        //
        //   ]
        // });
        //
        //
        let line1 = Highcharts.chart('container500', {
            chart: {
              type: 'line'
            },
            title: {
              text: 'Employees Performance Distribution'
            },
            subtitle: {
              text: ''
            },
            xAxis: {
              categories:
                res.Data.map(item => {

                  return item.lvl_name;

                })
            },
            yAxis: {
              title: {
                text: 'Employee Level Count'
              }
            }
            ,
            plotOptions: {
              line: {
                dataLabels: {
                  enabled: true
                },
                enableMouseTracking: false
              }
            },
            series: [

              {
                name: 'Performance Level',
                data: res.Data.map(item => {

                  return item.empCount;


                }),
              },

              {
                name: 'Actual Level',
                data: res.Data.map(item => {

                  return item.unitEmpAfterAssessment;


                }),
              }

            ]
          })
        ;

      });
  }

  GetNewEmployeeDistribution(units, successRates) {
    this.unitPerService
      .GetNewAllEmployeeQouta(this.ddlYear, this.userContext.CompanyID, units, successRates)
      .subscribe((res) => {
        if (res.IsError) {
          alert(res.ErrorMessage + ",1");
          return;
        }
        let employeeCount = res.Data.reduce((total, item) => total + item.empCount, 0);
        res.Data = res.Data.map(obj => ({
          ...obj,
          unitEmpAfterAssessmentPercent: parseFloat(((obj.unitEmpAfterAssessment / employeeCount) * 100) + '').toFixed(2)
        }));
        this.allEmployeeQouta = res.Data;

        let cols = [
          {HeaderText: "Level Name", DataField: "lvl_name"},
          {HeaderText: "Level Number", DataField: "lvl_number"},
          {HeaderText: "Level Percentage %", DataField: "lvl_percent"},
          {
            HeaderText: "Planned Quota",
            DataField: "empCount",
            Sum: "empCount",
          },

          {
            HeaderText: "Actual Quota",
            DataField: "unitEmpAfterAssessment",
            Sum: "unitEmpAfterAssessment",
          },
          {
            HeaderText: "Actual Percentage %",
            DataField: "unitEmpAfterAssessmentPercent",
          }
        ];
        let plannedCols = [
          {HeaderText: "Level Name", DataField: "lvl_name"},
          {HeaderText: "Level Number", DataField: "lvl_number"},
          {HeaderText: "Level Percentage %", DataField: "lvl_percent"},
          {
            HeaderText: "Planned Quota",
            DataField: "empCount",
            Sum: "empCount",
          }
        ];

        console.log('static data', res.Data[0]);
        res.Data[0].unitEmpAfterAssessmentPercent = parseFloat(((res.Data[0].unitEmpAfterAssessment / employeeCount) * 100) + '').toFixed(2);
        res.Data[1].unitEmpAfterAssessmentPercent = parseFloat(((res.Data[1].unitEmpAfterAssessment / employeeCount) * 100) + '').toFixed(2);
        res.Data[2].unitEmpAfterAssessmentPercent = parseFloat(((res.Data[2].unitEmpAfterAssessment / employeeCount) * 100) + '').toFixed(2);
        res.Data[3].unitEmpAfterAssessmentPercent = parseFloat(((res.Data[3].unitEmpAfterAssessment / employeeCount) * 100) + '').toFixed(2);
        res.Data[4].unitEmpAfterAssessmentPercent = parseFloat(((res.Data[4].unitEmpAfterAssessment / employeeCount) * 100) + '').toFixed(2);


        this.gvEmpDistribution.bind(this.showActualCol == 1 ? cols : plannedCols, res.Data, "gvEmpDistribution", null);

        let line1 = Highcharts.chart('container500', {
            chart: {
              type: 'line'
            },
            title: {
              text: 'Employees Performance Distribution'
            },
            subtitle: {
              text: ''
            },
            xAxis: {
              categories:
                res.Data.map(item => {

                  return item.lvl_name;

                })
            },
            yAxis: {
              title: {
                text: 'Employee Level Count'
              }
            }
            ,
            plotOptions: {
              line: {
                dataLabels: {
                  enabled: true
                },
                enableMouseTracking: false
              }
            },
          series: [

            {
              name: 'Performance Level',
              data: res.Data.map(item => {

                return item.empCount;


              }),
            },

            {
              name: 'Actual Level',
              data: res.Data.map(item => {

                return item.unitEmpAfterAssessment;


              }),
            }

          ]
          })
        ;

      });
  }

  SearchUnits() {
    //this.GetEmployeeDistribution();
    this.unitPerService
      .GetUnitsSearch(
        this.ddlYear,
        this.ddlUnitSearch,
        this.userContext.CompanyID,
        this.userContext.language
      )
      .subscribe((res) => {
        if (res.IsError) {
          alert(res.ErrorMessage + ",1");
          return;
        }
        this.getProjectsAssessments();
        res.Data.sort((a, b) => a.UnitPlanWeight + b.UnitPlanWeight);
        let sumData = 0;
        res.Data.forEach((element) => {
          sumData += element.UnitActualWeight;
        });
        this.allUnitListDetails = res.Data;
        let gvUnitChart = Highcharts.chart('gvUnitChartContainer', {
          chart: {
            type: 'column',
            zoomType: 'y'
          },
          title: {
            text: 'Units Performance - Comparison List Chart'
          },
          subtitle: {
            text: ''
          },
          xAxis: {
            categories: res.Data.map(item => {

              return item.UnitName;

            }),
            title: {
              text: null
            }
          },
          yAxis: {
            min: 0,
            title: {
              text: '%',
              align: 'high'
            },
            labels: {
              overflow: 'justify'
            }
          },
          tooltip: {
            valueSuffix: ' %'
          },
          plotOptions: {
            bar: {
              dataLabels: {
                enabled: true
              }
            }
          },
          legend: {
            layout: 'vertical',
            align: 'right',
            verticalAlign: 'top',
            x: -180,
            y: 100,
            floating: true,
            borderWidth: 1,
            backgroundColor:
              Highcharts.defaultOptions.legend.backgroundColor || '#FFFFFF',
            shadow: true
          },
          credits: {
            enabled: false
          },
          series: [{
            name: 'Planned Weight %',
            data: res.Data.map(obj => {
              return obj.UnitPlanWeight;
            }, 0)
          }, {
            name: 'Actual Weight %',
            data: res.Data.map(item => {
              return item.UnitActualWeight;
            }, 0)
          },
            //
            //   {
            //   name: 'FTE',
            //   data: res.Data.map(item => {
            //
            //     return item.FTE;
            //
            //   })
            // }
            //
          ]
        });
        // Highcharts.chart('gvUnitChartContainer', {
        //   chart: {
        //     type: 'column',
        //     zoomType: 'y'
        //   },
        //   title: {
        //     text: 'Corn vs wheat estimated production for 2020 (1000 MT)'
        //   },
        //   subtitle: {
        //     text:
        //       'Source: <a href="https://www.indexmundi.com/agriculture/?commodity=corn">indexmundi</a>'
        //   },
        //   xAxis: {
        //     categories: ['USA', 'China', 'Brazil', 'EU', 'India', 'Russia'],
        //     title: {
        //       text: null
        //     },
        //     accessibility: {
        //       description: 'Countries'
        //     }
        //   },
        //   yAxis: {
        //     title: {
        //       text: 'Production in 1000 million ton'
        //     },
        //     labels: {
        //       overflow: 'justify'
        //     }
        //   },
        //   plotOptions: {
        //     column: {
        //       dataLabels: {
        //         enabled: true
        //       }
        //     }
        //   },
        //   tooltip: {
        //     valueSuffix: ' (1000 MT)',
        //     stickOnContact: true,
        //     backgroundColor: 'rgba(255, 255, 255, 0.93)'
        //   },
        //   legend: {
        //     enabled: true
        //   },
        //   series: [
        //     {
        //       name: 'Corn',
        //       data: [406292, 260000, 107000, 68300, 27500, 14500],
        //       borderColor: '#949494'
        //     },
        //     {
        //       name: 'Wheat',
        //       data: [51086, 136000, 5500, 141000, 107180, 77000]
        //     }
        //   ]
        // });
        const plannedWeight = this.allUnitListDetails.reduce((accumulator, object) => {
          return accumulator + (object.UnitPlanWeight);
        }, 0).toFixed(2);
        const actualWeight = this.allUnitListDetails.reduce((accumulator, object) => {
          return accumulator + (object.UnitActualWeight);
        }, 0).toFixed(2);
        let gvUnitChartSingle = Highcharts.chart('gvUnitChartSingleContainer', {
          chart: {
            type: 'bar'
          },
          title: {
            text: 'Units Performance - Comparison List Chart'
          },
          subtitle: {
            text: ''
          },
          xAxis: {
            categories: ["All Units"],
            title: {
              text: null
            }
          },
          yAxis: {
            min: 0,
            title: {
              text: '%',
              align: 'high'
            },
            labels: {
              overflow: 'justify'
            }
          },
          tooltip: {
            valueSuffix: ' '
          },
          plotOptions: {
            bar: {
              dataLabels: {
                enabled: true
              }
            }
          },
          legend: {
            layout: 'vertical',
            align: 'right',
            verticalAlign: 'top',
            x: -40,
            y: 270,
            floating: true,
            borderWidth: 1,
            backgroundColor:
              Highcharts.defaultOptions.legend.backgroundColor || '#FFFFFF',
            shadow: true
          },
          credits: {
            enabled: false
          },
          series: [{
            name: 'Planned Weight %',
            data: [Number(plannedWeight)]
          },
            {
              name: 'Actual Weight %',
              data: [Number(actualWeight)]
            },
            {
              name: 'FTE',
              data: [0]
            }]
        });
      });
  }

  gvunitsHandler(event) {
    console.log('event', event);

    if (event[1] == "Details") {
      console.log('event', event);
      this.GetUnitEmployeeQouta(event[0]);
    }
  }

  GetUnitEmployeeQouta(unitid) {
    console.log(unitid);
    const unit = this.allUnitListDetails.find((unit) => unit.ID == unitid);
    console.log('unit', unit);
    const {SuccessRate} = unit;
    this.unitName = unit.UnitName;

    this.unitPerService
      .GetUnitEmployeeQouta(this.ddlYear, unitid, this.userContext.CompanyID, SuccessRate)
      .subscribe((res) => {
        if (res.IsError) {
          alert(res.ErrorMessage + ",1");
          return;
        }

        console.log("this.allUnitListDetails.filter(a => a.ID === unitid)[0]", this.allUnitListDetails.filter(a => a.ID === unitid)[0]);
        const unitPlannedWeight = this.allUnitListDetails.filter(a => a.ID === unitid)[0].UnitPlanWeight;
        console.log("per unit", res.Data);
        console.log("all ", this.allEmployeeQouta);
        res.Data = res.Data.map(item => {
          const levelPercent = this.allEmployeeQouta.find(a => a.lvl_number === item.lvl_number);
          console.log(levelPercent, "level");
          return {
            ...item,
            unitEmpQouta: Number((levelPercent.empCount * (unitPlannedWeight / 100)).toFixed(3))
          };
        });
        let cols = [
          {HeaderText: "Level Name", DataField: "lvl_name"},
          {HeaderText: "Level Number", DataField: "lvl_number"},
          {HeaderText: "Level Percentage %", DataField: "lvl_percent"},
          {HeaderText: "Planned Quota", DataField: "unitEmpQouta", Sum: "unitEmpQouta"},
          {
            HeaderText: "Actual  Quota",
            DataField: "unitEmpAfterAssessment",
            Sum: "unitEmpAfterAssessment",
          },
          {HeaderText: "FTE", DataField: "FTEQouta"},
        ];
        let plannedCols = [
          {HeaderText: "Level Name", DataField: "lvl_name"},
          {HeaderText: "Level Number", DataField: "lvl_number"},
          {HeaderText: "Level Percentage %", DataField: "lvl_percent"},
          {HeaderText: "Planned Quota", DataField: "unitEmpQouta", Sum: "unitEmpQouta"},
          {HeaderText: "FTE", DataField: "FTEQouta"},
        ];
        const sum = res.Data.reduce((total, item) => total + item.unitEmpQouta, 0);
        this.gvUnitQouta.bind(this.showActualCol == 1 ? cols : plannedCols, res.Data, "gvUnitQouta", null);
        Highcharts.chart('container500Sub', {
          chart: {
            type: 'line'
          },
          title: {
            text: 'Employees Performance Distribution'
          },
          subtitle: {
            text: ''
          },
          xAxis: {
            categories:
              res.Data.map(item => {

                return item.lvl_name;

              })
          },
          yAxis: {
            title: {
              text: 'Employee Level Count'
            }
          }
          ,
          plotOptions: {
            line: {
              dataLabels: {
                enabled: true
              },
              enableMouseTracking: false
            }
          },
          series: [

            {
              name: 'Performance Level',
              data: res.Data.map(item => {
                return Number(item.plannedQuota.toFixed(2));
              }),
            },

            {
              name: 'Actual Level',
              data: [
                res.Data[0].unitEmpAfterAssessment,
                res.Data[1].unitEmpAfterAssessment,
                res.Data[2].unitEmpAfterAssessment,
                res.Data[3].unitEmpAfterAssessment,
                res.Data[4].unitEmpAfterAssessment
              ]
            }

          ]
        });
      });
  }

  LoadAllPerformanceLevelQuotaView() {
    this.settingsService.LoadAllPerformanceLevelQuotaView(this.userContext.CompanyID,
      this.ddlYear, this.userContext.language).subscribe(res => {
      if (res.IsError) {
        return;
      }
      this.quotaLevels = res.Data;
    });
  }

}
