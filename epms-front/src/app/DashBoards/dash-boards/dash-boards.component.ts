import {Component, OnInit} from '@angular/core';
import {DashBoardService} from 'src/app/Services/DashBoardsService';
import {ProjectsService} from 'src/app/Services/Projects/projects.service';
import {UserContextService} from 'src/app/Services/user-context.service';
import {UsersService} from '../../Services/Users/users.service';
import {ProjectsAssessmentService} from "../../Services/projects-assessment.service";

declare var Highcharts: any;
import {Chart} from 'chart.js';
import chroma from 'chroma-js';
import {isPlatformWorkerApp} from "@angular/common";

/* tslint:disable */
@Component({
  selector: 'app-dash-boards',
  templateUrl: './dash-boards.component.html',
  styleUrls: ['./dash-boards.component.css']
})
export class DashBoardsComponent implements OnInit {

  objectives = 0;
  goals = 0;
  goalsPercentage = 0;
  objectivesKpis = 0;
  competenciesKpis = 0;
  successRate = 0;
  projects = 0;
  operations = 0;
  ddlUnit: any = 0;
  projectAssessmentList;

  unitLst: Array<{ ID: number; NAME: string }>;

  constructor(private DashBoardService: DashBoardService,
              private Helper: ProjectsService,
              private projectsAssessmentService: ProjectsAssessmentService,
              private ProjectsService: ProjectsService,
              private userContext: UserContextService,
              private userService: UsersService) {
    Highcharts.setOptions({
      chart: {

        style: {
          fontFamily: 'Tajawal',

        }
      }
    });
    this.userService.GetLocalResources(window.location.hash, this.userContext.CompanyID, this.userContext.language).subscribe(res => {
      if (res.IsError) {
        alert(res.ErrorMessage);
        return;
      }
      this.PageResources = res.Data;
    });
    this.fillUnitLst();
  }

  generateRandomColors(count: number): string[] {
    const palette = chroma.scale(['rgba(107,65,197,0.5)', 'rgba(107,65,197,0.5)']).mode('lch').colors(count); // Define the start and end colors of the palette

    return palette;
  }

  GetLocalResourceObject(resourceKey) {
    for (let i = 0; i < this.PageResources.length; i++) {
      if (this.PageResources[i].resource_key === resourceKey) {
        return this.PageResources[i].resource_value;
      }
    }
  }

  yearLst: any;
  ddlYear: any = 2032;
  showDashBoardpnl: boolean;
  show0101pnl: boolean;
  show0102pnl: boolean;
  show0103pnl: boolean;
  show0104pnl: Boolean;
  PageResources = [];
  showActualCostPnl: Boolean;

  ngOnInit() {
    this.fillYears()
    this.showDashBoardpnl = false;
    this.getAdminDashboard();

  }

  getUnitContributionsPerYear() {

    this.DashBoardService.getUnitContributionsPerYear(this.ddlYear, this.ddlUnit).subscribe(result => {

      if (result.IsError) {
        alert(result.ErrorMessage + "," + 1);
        return;
      }

      const unitNames = result.Data.map(a => a.Name);
      const unitWeights = result.Data.map(a => this.projectAssessmentList.filter(x => x.UnitId === a.UnitId && x.planned_status !== "Declined").reduce((accumulator, object) => {
          return accumulator + (object.plannedStratigy);
        }, 0)
      );
      const unitActuals = result.Data.map(a => Number(this.projectAssessmentList.filter(x => x.UnitId === a.UnitId && x.planned_status !== "Declined").reduce((accumulator, object) => {
          return accumulator + this.getProjectStrategyWeightResult(object);
        }, 0).toFixed(2))
      );

      console.log("unitWeights", unitWeights)
      const ctx = document.getElementById('myChart') as HTMLCanvasElement;
      const data = {
        labels: unitNames,
        datasets: [
          {
            label: 'Contribution Percentage',
            data: unitWeights,
            backgroundColor: this.generateRandomColors(unitNames.length),
            borderColor: 'rgba(107,65,197,1)',
            borderWidth: 1,
          },
          {
            label: 'Actual Percentage',
            data: unitActuals,
            backgroundColor: 'rgb(99,188,228)',
            borderColor: 'rgb(99,188,228)',
            borderWidth: 1,
          },
        ],
      };

      // Chart configuration
      const config = {
        type: 'horizontalBar',
        data,
        options: {
          scales: {
            xAxes: [{
              ticks: {
                beginAtZero: true,
                fontSize: 16, // Change the font size here
                fontFamily: 'Tajawal ,sans-serif',
                fontColor: 'rgba(107,65,197,1)'
              },
            }],
            yAxes: [{
              gridLines: {
                display: false // Remove x-axis grid lines
              },
              ticks: {
                textAlign: 'left',
                padding: 15,
                fontSize: 14, // Change the font size here
                fontFamily: 'Tajawal ,sans-serif',
                fontColor: 'rgba(107,65,197,1)',

              },
            }],
          },
          legend: {
            labels: {
              fontSize: 14, // Change the font size for legend labels
              fontFamily: 'Tajawal ,sans-serif',
              fontColor: 'rgb(107,65,197,1)'
            },
          },
        },
      };


      const myChart = new Chart(ctx, config);


    });


  }

  getAdminDashboard() {
    this.DashBoardService.getAdminDashboard(this.ddlYear, this.ddlUnit ? this.ddlUnit : 0).subscribe(result => {
      if (result.IsError) {
        alert(result.ErrorMessage + "," + 1);
        return;
      }
      console.log(result);
      this.objectives = result.Data.objectives;
      this.goals = result.Data.goals;
      this.objectivesKpis = result.Data.objectivesKpis;
      this.competenciesKpis = result.Data.competenciesKpis;
      this.projects = result.Data.projects;
      this.operations = result.Data.operations;
    });
    this.projectsAssessmentService
      .getProjectAssessment(
        this.userContext.CompanyID,
        this.ddlYear,
        this.ddlUnit!==0 ? this.ddlUnit  : -1,
        0,
        this.userContext.language
      )
      .subscribe((res) => {
        if (res.IsError) {
          alert(res.ErrorMessage);
          return;
        }
        this.projectAssessmentList = res.Data;

        this.getUnitContributionsPerYear()


        console.log("goal% log", res.Data)
        const progressPlantWeight = res.Data.filter(a => {
          return a.planned_status !== "Declined";
        }).reduce((accumulator, object) => {

          const r = this.getProjectStrategyWeightResult(object);

          return r === 0 ? accumulator : accumulator + object.plannedStratigy;
        }, 0);

        const progressActualWeight = res.Data.filter(a => {
          return a.planned_status !== "Declined";
        }).reduce((total, x) => {
          return total + this.getProjectStrategyWeightResult(x);
        }, 0);
        this.goalsPercentage = (progressActualWeight / progressPlantWeight) * 100;


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

  fillYears() {
    this.ProjectsService.GetYears().subscribe(
      res => {
        this.yearLst = res.Data;
        if (this.yearLst != null && this.yearLst.length > 0) {
          this.yearLst.forEach((element) => {
            this.ddlYear = element.id;
            console.log('year active', this.ddlYear);
          });
          this.search();

        }
      }
    )
  }

  fillUnitLst() {
    this.Helper.GetUnits(
      this.userContext.CompanyID,
      this.userContext.language
    ).subscribe((res) => {

      //check if role is unit
      this.unitLst = this.userContext.RoleId != 5 ? res.Data : res.Data.filter(a => {
        return a.ID == this.userContext.UnitId
      });
      this.ddlUnit = null;
    });
  }

  search() {
    //this.GetProjectPerUnits()
    this.getChart0101Data()
    this.getChart0102Data()
    this.getChart0103Data()
    this.getActualCostStatisticData();
    this.getActualCostVsPlannedStatisticData();
    this.showDashBoardpnl = true
    this.GetUnuitTargetVsActualResult()
    this.getAdminDashboard()
  }


  getChart0101Data() {
    this.DashBoardService.GetObjectiveWeightPerYear(this.userContext.CompanyID,
      this.ddlYear, this.userContext.language, this.ddlUnit ? this.ddlUnit : 0).subscribe(
      res => {
        this.RenderObjectiveWeightPerYear(res)
      },
      err => {
        alert(err.statusText + ',')
      },
      () => {
      }
    )
  }


  getActualCostStatisticData() {
    this.DashBoardService.GetActualCostStatisticsData(this.ddlYear, this.userContext.CompanyID, this.userContext.language).subscribe(
      res => {

        console.log("ajoishfjjksdkjk", res)
        this.RenderActualCostStatistic(res)
      },
      err => {
        alert(err.statusText + ',')
      },
      () => {
      }
    )
  }


  getActualCostVsPlannedStatisticData() {
    this.DashBoardService.RenderActualCostVsPlannedStatistic(this.ddlYear, this.userContext.CompanyID, this.userContext.language).subscribe(
      res => {
        this.RenderActualCostVsPlannedStatistic(res)
      },
      err => {
        alert(err.statusText + ',')
      },
      () => {
      }
    )
  }

  RenderActualCostStatistic(data) {
    // if (data.LEVEL1.length == 0) {
    //     this.showActualCostPnl = true
    //  }
    //  else {
    //      this.showActualCostPnl = false
    //  }
    if (myChart != null) {
      myChart.remove()
    }
    var myChart = Highcharts.chart('container5', {
      chart: {
        type: 'packedbubble',

      },
      title: {
        text: null
      },
      tooltip: {
        useHTML: true,
        pointFormat: '<b>{point.fullName}:</b> {point.value}'
      },
      plotOptions: {
        packedbubble: {
          minSize: '20%',
          maxSize: '100%',
          zMin: 0,
          zMax: 1000,
          layoutAlgorithm: {
            gravitationalConstant: 0.05,
            splitSeries: true,
            seriesInteraction: false,
            dragBetweenSeries: true,
            parentNodeLimit: true
          },
          dataLabels: {
            enabled: true,
            format: '{point.name}',
            filter: {
              property: 'y',
              operator: '>',
              value: 250
            },
            style: {
              color: 'black',
              textOutline: 'none',
              fontWeight: 'normal'
            }
          }
        }
      },
      series: data
    });

  }


  RenderActualCostVsPlannedStatistic(data) {
    // if (data.LEVEL1.length == 0) {
    //     this.showActualCostPnl = true
    //  }
    //  else {
    //      this.showActualCostPnl = false
    //  }
    if (myChart != null) {
      myChart.remove()
    }
    var myChart = Highcharts.chart('container6', {
      chart: {
        type: 'column'
      },
      title: {
        text: null
      },
      xAxis: {
        categories: data.categories
      },
      yAxis: [{
        min: 0,
        title: {
          text: this.GetLocalResourceObject('lblCost')
        }
      }, {
        title: {
          text: ''
        },
        opposite: true
      }],
      legend: {
        shadow: false
      },
      tooltip: {
        shared: true
      },
      plotOptions: {
        column: {
          grouping: false,
          shadow: false,
          borderWidth: 0
        }
      },
      series: [{
        name: this.GetLocalResourceObject('lblCost'),
        color: 'rgba(165,170,217,1)',
        data: data.costs,
        pointPadding: 0.3,
        pointPlacement: -0.2
      }, {
        name: this.GetLocalResourceObject('lblActualCost'),
        color: 'rgba(126,86,134,.9)',
        data: data.actualCosts,
        pointPadding: 0.4,
        pointPlacement: -0.2
      }]
    });

  }

  RenderObjectiveWeightPerYear(data) {

    console.log("data", data)
    if (data.LEVEL1.length == 0) {
      this.show0101pnl = true
    } else {
      this.show0101pnl = false
    }
    if (myChart != null) {
      myChart.remove()
    }
    var myChart = Highcharts.chart('container2', {
      chart: {
        type: 'pie'
      },
      title: {
        text: null
      },
      subtitle: {
        text: null
      },
      xAxis: {
        type: 'category'
      },
      yAxis: {
        title: {
          text: this.GetLocalResourceObject('lblWeight')
        }

      },
      legend: {
        enabled: true
      },
      plotOptions: {
        series: {
          dataLabels: {
            useHTML: true,
            enabled: true,
            format: '{point.y} %',
            color: 'white',
            distance: -70,
            style: {
              fontSize: '14px' // Change the font size here
            }
          },
          showInLegend: true,
        }
      },

      "series": [
        {
          "name": this.GetLocalResourceObject('lblSterategicWeight'),
          "data": data.LEVEL1,
        }],
      "drilldown": {
        "series": data.LEVEL2
      }
    });

  }

  getChart0102Data() {
    this.DashBoardService.GetObjectiveResultWeightPercentage(this.ddlYear, this.userContext.CompanyID, this.userContext.language, this.ddlUnit).subscribe(
      res => {
        this.RenderObjectiveResultWeightPercentage(res)
      },
      err => {
        alert(err.statusText + ',')
      },
      () => {
      }
    )
  }

  RenderObjectiveResultWeightPercentage(data) {
    if (data.LEVEL1.length == 0) {
      this.show0102pnl = true
    } else {
      this.show0102pnl = false
    }
    if (myChart != null) {
      myChart.remove()
    }
    var myChart = Highcharts.chart('container', {
      chart: {
        type: 'column'
      },
      title: {
        text: null
      },
      subtitle: {
        text: null
      },
      xAxis: {
        type: 'category'
      },
      legend: {
        enabled: false
      },
      plotOptions: {
        series: {
          borderWidth: 0,
          dataLabels: {
            enabled: true,
            format: '{point.y:.1f}%'
          }
        }
      },
      tooltip: {
        headerFormat: '<span style="font-size:15px">{series.name}</span><br>',
        pointFormat: '<span style="color:{point.color}">{point.fullName}</span>: <b>{point.y:.2f}%</b> of total<br/>'
      },
      yAxis: {

        title: {
          text: this.GetLocalResourceObject('lblResultWeightPercentage')
        },


      },
      credits: {
        enabled: false
      },


      "series": [
        {
          "name": this.GetLocalResourceObject('lblSterategicResultWeight'),
          "colorByPoint": true,
          "data": data.LEVEL1,
        }],
      "drilldown": {
        "series": data.LEVEL2
      }
    });
  }

  getChart0103Data() {
    this.DashBoardService.GetUnitsPercentage(this.ddlYear, this.userContext.CompanyID, this.userContext.language, this.ddlUnit).subscribe(
      res => {
        this.RenderUnitsPercentage(res)
      },
      err => {
        alert(err.statusText + ',')
      },
      () => {
      }
    )
  }

  RenderUnitsPercentage(data) {
    if (data.LEVEL1.length == 0) {
      this.show0103pnl = true
    } else {
      this.show0103pnl = false
    }
    if (myChart != null) {
      myChart.remove()
    }
    var myChart = Highcharts.chart('container3', {
      chart: {

        type: 'pie'
      },
      title: {
        text: null
      },

      subtitle: {
        text: null
      },
      xAxis: {
        type: 'category'
      },
      yAxis: {
        title: {
          text: null
        }

      },
      plotOptions: {
        series: {
          dataLabels: {
            enabled: true,
            format: '{point.name}: {point.y:.2f}%'
          }
        }
      },
      legend: {
        reversed: true
      },
      tooltip: {
        headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
        pointFormat: '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:.2f}%</b> of total<br/>'
      },
      "series": [
        {
          "name": this.GetLocalResourceObject('lblSterategicResultWeight'),
          "data": data.LEVEL1,
          colorByPoint: true,
        }],
      "drilldown": {
        "series": data.LEVEL2
      }
    });
  }

  GetUnuitTargetVsActualResult() {
    this.DashBoardService.GetUnuitTargetVsActualResult(this.ddlYear, this.userContext.CompanyID, this.userContext.language).subscribe(
      res => {
        this.RenderUnuitTargetVsActualResult(res)
      },
      err => {
        alert(err.statusText + ',')
      },
      () => {
      }
    )
  }

  RenderUnuitTargetVsActualResult(data) {
    if (data.categories.length == 0) {
      this.show0104pnl = true
    } else {
      this.show0104pnl = false
    }
    if (myChart != null) {
      myChart.remove()
    }
    var myChart = Highcharts.chart('container4', {
      chart: {
        type: 'bar'
      },
      title: {
        text: null
      },
      subtitle: {
        text: null
      },
      xAxis: {
        categories: data.categories,
        crosshair: true
      },
      legend: {
        reversed: true
      },
      yAxis: {
        min: 0,
        title: {
          text: this.GetLocalResourceObject('lblUnits')
        }
      },
      tooltip: {
        headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
        pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
          '<td style="padding:0"><b>{point.y} %</b></td></tr>',
        footerFormat: '</table>',
        shared: true,
        useHTML: true
      },
      plotOptions: {
        series: {
          stacking: 'normal'
        }
      },
      series: data.series
    });
  }
}
