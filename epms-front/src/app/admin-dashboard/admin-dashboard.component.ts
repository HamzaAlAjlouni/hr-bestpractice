import {Component, Input, OnInit} from '@angular/core';
import {DashBoardService} from "../Services/DashBoardsService";
import {Chart} from 'chart.js';
import chroma from 'chroma-js';
import {UnitsService} from "../Services/units.service";
import {UserContextService} from "../Services/user-context.service";
import {ProjectsAssessmentService} from "../Services/projects-assessment.service";
import {UsersService} from "../Services/Users/users.service";


@Component({
  selector: 'app-admin-dashboard',
  templateUrl: './admin-dashboard.component.html',
  styleUrls: ['./admin-dashboard.component.css']
})
export class AdminDashboardComponent implements OnInit {

  objectives = 0;

  goals = 0;
  goalsPercentage = 0;

  objectivesKpis = 0;

  competenciesKpis = 0;
  successRate = 0;

  projects = 0;

  operations = 0;
  ddlYear = 2032;

  constructor(private dashBoardService: DashBoardService,
              private projectsAssessmentService: ProjectsAssessmentService,
              private userService: UsersService,
              private userContext: UserContextService,
              private unitService: UnitsService) {
  }

  generateRandomColors(count: number): string[] {
    const palette = chroma.scale(['rgba(107,65,197,0.5)', 'rgba(107,65,197,0.5)']).mode('lch').colors(count); // Define the start and end colors of the palette

    return palette;
  }

  ngOnInit() {
    this.dashBoardService.getUnitContributionsPerYear(this.ddlYear).subscribe(result => {

      if (result.IsError) {
        alert(result.ErrorMessage + "," + 1);
        return;
      }

      const unitNames = result.Data.map(a => a.Name);
      const unitWeights = result.Data.map(a => a.Weight);
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

    this.dashBoardService.getObjectiveKPIContributionsPerYear(this.ddlYear).subscribe(result => {

      if (result.IsError) {
        alert(result.ErrorMessage + "," + 1);
        return;
      }

      const unitNames = result.Data.map(a => "  " + a.Name);
      const unitWeights = result.Data.map(a => a.Weight);
      const ctx = document.getElementById('KPIChart') as HTMLCanvasElement;
      const data = {
        labels: unitNames,
        datasets: [
          {
            label: '',
            data: unitWeights,
            backgroundColor: this.generateRandomColors(unitNames.length),
            borderColor: 'rgba(107,65,197,1)',
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
                fontSize: 14, // Change the font size here
                fontFamily: 'Tajawal ,sans-serif',
                fontColor: 'rgba(107,65,197,1)'
              },
            }],
            yAxes: [{
              ticks: {
                fontSize: 14, // Change the font size here
                fontFamily: 'Tajawal ,sans-serif',
                fontColor: 'rgba(107,65,197,1)'
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

    this.dashBoardService.getObjectiveKPIContributionsPerYear(this.ddlYear).subscribe(result => {

      if (result.IsError) {
        alert(result.ErrorMessage + "," + 1);
        return;
      }

      const unitNames = result.Data.map(a => "  " + a.Name);
      const unitWeights = result.Data.map(a => a.Weight);
      const ctx = document.getElementById('KPIChart1') as HTMLCanvasElement;
      const data = {
        labels: unitNames,
        datasets: [
          {
            label: '',
            data: unitWeights,
            backgroundColor: this.generateRandomColors(unitNames.length),
            borderColor: 'rgba(107,65,197,1)',
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
              grid: {
                display: false // Remove x-axis grid lines
              },
              ticks: {
                beginAtZero: true,
                fontSize: 14, // Change the font size here
                fontFamily: 'Tajawal ,sans-serif',
                fontColor: 'rgba(107,65,197,1)'
              },
            }],
            yAxes: [{
              grid: {
                display: false // Remove x-axis grid lines
              },
              drawBorder: false, // Remove x-axis line
              ticks: {
                fontSize: 14, // Change the font size here
                fontFamily: 'Tajawal ,sans-serif',
                fontColor: 'rgba(107,65,197,1)'
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


    this.dashBoardService.getAdminDashboard(2032).subscribe(result => {
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
        -1,
        0,
        this.userContext.language
      )
      .subscribe((res) => {
        if (res.IsError) {
          alert(res.ErrorMessage);
          return;
        }

        const plannedWeight = res.Data.reduce((total, item) => total + item.plannedStratigy, 0);
        const actualWeight = res.Data.reduce((total, x) => total + (x.Result / x.Target * 100) * (x.plannedStratigy / 100), 0);
        this.goalsPercentage = Math.round((actualWeight / plannedWeight) * 100);


      });
  }

}
