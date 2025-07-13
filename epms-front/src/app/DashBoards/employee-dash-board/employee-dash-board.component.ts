import {Component, OnInit} from '@angular/core';
import {DashBoardService} from 'src/app/Services/DashBoardsService';
import {ProjectsService} from 'src/app/Services/Projects/projects.service';
import {UserContextService} from 'src/app/Services/user-context.service';
import {UsersService} from '../../Services/Users/users.service';

declare var Highcharts: any;

/* tslint:disable */
@Component({
  selector: 'app-employee-dash-board',
  templateUrl: './employee-dash-board.component.html',
  styleUrls: ['./employee-dash-board.component.css']
})
export class EmployeeDashBoardComponent implements OnInit {
  yearLst: any;
  ddlYear;
  unitLst: Array<{ ID: number, NAME: string }>;
  showDashBoardpnl: boolean;
  showEmpAcessPnl: boolean;
  empPerUnitsPnl: boolean;
  empRank: boolean;
  employees: number = 0;
  comptencies: number = 0;
  comptenciesKPIs: number = 0;

  PageResources = [];
  ddlUnitId: any = 0;

  constructor(private Helper: DashBoardService,
              private ProjectService: ProjectsService, private userContext: UserContextService,
              private userService: UsersService) {

    this.userService.GetLocalResources(window.location.hash, this.userContext.CompanyID, this.userContext.language).subscribe(res => {
      if (res.IsError) {
        alert(res.ErrorMessage);
        return;
      }
      this.PageResources = res.Data;
    });


  }

  GetLocalResourceObject(resourceKey) {
    for (let i = 0; i < this.PageResources.length; i++) {
      if (this.PageResources[i].resource_key === resourceKey) {
        return this.PageResources[i].resource_value;
      }
    }
  }

  search() {
    this.GetEmployeeAssessment();
    this.getEmployeesStatistics()

    this.GetNumberEmpForUnitsVsNeedNumber()

    this.GetEmployeeRank()
    this.showDashBoardpnl = true
  }

  fillYears() {
    this.ProjectService.GetYears().subscribe(
      res => {
        this.yearLst = res.Data;
        if (this.yearLst != null && this.yearLst.length > 0) {
          this.yearLst.forEach((element) => {
            this.ddlYear = element.id;

          });

        }
        this.fillUnitLst();

        this.search();
        this.getEmployeesStatistics()
      }
    )
  }

  ngOnInit() {

    this.fillYears()

  }


  getEmployeesStatistics() {
    this.Helper.getEmployeeDashboard(this.ddlUnitId).subscribe(result => {

      if (result.IsError) {
        alert(result.ErrorMessage + "," + 1);
        return;
      }
      console.log(result);
      this.employees = result.Data.employees;
      this.comptencies = result.Data.comptencies;
      this.comptenciesKPIs = result.Data.comptenciesKPIs;


    });


  }

  GetEmployeeAssessment() {
    this.Helper.GetEmployeeAssessment(this.ddlYear, this.userContext.language, this.ddlUnitId).subscribe(
      res => {
        this.RenderEmployeeAssessment(res)
      },
      err => {
        alert(err.statusText + ',')
      },
      () => {
      }
    )
  }

  fillUnitLst() {
    this.ProjectService.GetUnits(this.userContext.CompanyID, this.userContext.language).subscribe(
      res => {
        this.unitLst = res.Data;
        if (this.unitLst != null && this.unitLst.length > 0) {
          // this.ddlUnitId = this.unitLst[0].ID;
          this.GetEmployeeRank();
        }

      }
    )
  }

  RenderEmployeeAssessment(data) {
    if (data.categories.length == 0) {
      this.showEmpAcessPnl = true
    } else {
      this.showEmpAcessPnl = false
    }
    if (myChart != null) {
      myChart.remove()
    }
    console.log(data);
    var myChart = Highcharts.chart('container', {
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
          text: this.GetLocalResourceObject('lblResult/Target')
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
        name: this.GetLocalResourceObject('lblTarget'),
        color: 'rgba(165,170,217,1)',
        data: data.Targets,
        pointPadding: 0.3,
        pointPlacement: -0.2
      }, {
        name: this.GetLocalResourceObject('lblResult'),
        color: 'rgba(126,86,134,.9)',
        data: data.Results,
        pointPadding: 0.4,
        pointPlacement: -0.2
      }]
    });
  }

  GetNumberEmpForUnitsVsNeedNumber() {
    this.Helper.GetNumberEmpForUnitsVsNeedNumber(this.ddlYear, this.userContext.CompanyID, this.userContext.language).subscribe(
      res => {
        this.RenderEmpForUnitsVsNeedNumberdata(res)
      },
      err => {
        alert(err.statusText + ',')
      },
      () => {
      }
    )
  }

  RenderEmpForUnitsVsNeedNumberdata(data) {
    if (data.categories.length == 0) {
      this.empPerUnitsPnl = true
    } else {
      this.empPerUnitsPnl = false
    }
    if (myChart != null) {
      myChart.remove()
    }
    console.log('asdasd', data.series)
    var myChart = Highcharts.chart('container1', {
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
        categories: data.categories,
        crosshair: true
      },
      yAxis: {
        min: 0,
        title: {
          text: this.GetLocalResourceObject('lblPercentage')
        }
      },
      tooltip: {
        headerFormat: '<span  style="font-size:10px">{point.key}</span><table>',
        pointFormat: '<tr><td style="color:{series.color}">{series.name}: </td>' +
          '<td ><b>{point.y}</b></td></tr>',
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
      series: data.series
    });
  }

  GetEmployeeRank() {
    if (this.ddlUnitId > 0) {
      this.Helper.GetEmployeeRank(this.ddlYear, this.userContext.CompanyID, this.ddlUnitId, this.userContext.language).subscribe(
        res => {
          this.RenderEmployeeRank(res)
        },
        err => {
          alert(err.statusText + ',')
        },
        () => {
        }
      )
    }
  }


  ddlUnitChange() {
    this.GetEmployeeRank();
  }

  RenderEmployeeRank(data) {
    if (data.categories.length == 0) {
      this.empRank = true
    } else {
      this.empRank = false
    }
    if (myChart != null) {
      myChart.remove()
    }
    var myChart = Highcharts.chart('container2', {
      chart: {
        type: 'areaspline'
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
      yAxis: {
        min: 0,
        title: {
          text: this.GetLocalResourceObject('lblPercentage')
        }
      },
      tooltip: {
        headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
        pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
          '<td style="padding:0"><b>{point.y}</b></td></tr>',
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
      series: data.series
    });
  }
}
