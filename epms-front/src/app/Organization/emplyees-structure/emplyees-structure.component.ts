import { Component, OnInit, ViewChild} from '@angular/core';
import {EmployeeService} from '../employees/employee.service';
import {UserContextService} from '../../Services/user-context.service';
import {Router} from "@angular/router";

declare var OrgChart: any;
declare var BALKANGraph: any;
declare var $: any;
declare var orgchart: any;

@Component({
  selector: 'app-emplyees-structure',
  templateUrl: './emplyees-structure.component.html',
  styleUrls: ['./emplyees-structure.component.css']
})
export class EmplyeesStructureComponent implements OnInit {
  @ViewChild('EmployeeAddOnFly', {read: false, static: false}) EmployeeAddOnFly;
  @ViewChild('empPerformance', {read: false, static: false}) empPerformance;


  constructor(private empService: EmployeeService, private userContextService: UserContextService,
              private router: Router) {
    this.PerformSettings();
  }

  empList;
  SelectedEmployee;
  treeView;

  ngOnInit() {
  }

// <img style='border-radius: 25px;' width='70px' height='70px' src="${data.img}"/>
//
  loadTree() {
    this.empService.LoadEmployeesNewChart(1, null, null, null, this.userContextService.language).subscribe(res => {
      const emp = this.empService;
      const t = this;
      $(() => {

        const datascource = res.Data[0];
        console.log('emp tree', datascource);
        const nodeTemplate = (data) => {
          return `
          <div class="title" style='width:80%'>${data.Unit}</div>
          <div class="content" style='height:100%;font-size:1.1em;'>
          <br/>
          <h2>${data.Name}</h2>
          <h2>${data.title}</h2>

          <button class="btn btn-success btn-sm"  eventType='edit' empID="${data.id}"
          data-widget="Add" data-toggle="tooltip" title="" data-original-title="Edit Objective">
              <i class="fa fa-pencil" eventType='edit' empID="${data.id}"></i>
          </button>
          &nbsp;
          <button class="btn btn-success btn-sm" eventType='add' empID="${data.id}"
          data-widget="Add" data-toggle="tooltip" title="Add Project"
          data-original-title="Add Project">
              <i class="fa fa-plus" eventType='add' empID="${data.id}"></i>
          </button>
          &nbsp;
          <button class="btn btn-success btn-sm" eventType='performance' empID="${data.id}"
          data-widget="Add" data-toggle="tooltip" title="Employee Performance Plan"
          data-original-title="Employee Performance Plan">
              <i class="fa fa-bar-chart" eventType='performance' empID="${data.id}"></i>
          </button>


          </div>
        `;
        };


        const oc = $('#chart-container').orgchart({
          data: datascource,
          draggable: true,
          nodeTemplate,
          createNode: ($node, data) => {
            $node.on('click', (event) => {

              if (event.target.getAttribute('eventType') === 'edit') {
                const empid = event.target.getAttribute('empID');
                document.getElementById("addEmpModal").style.display = 'block';
                document.getElementById("addEmpModal").className = "modal fade in";
                t.SelectedEmployee = empid;
                t.EmployeeAddOnFly.upadteMode = true;
                t.EmployeeAddOnFly.SelectedID = empid;
                t.EmployeeAddOnFly.LoadEmplyeeByID(empid);
              } else if (event.target.getAttribute('eventType') === 'add') {
                const empid = event.target.getAttribute('empID');
                document.getElementById("addEmpModal").style.display = 'block';
                document.getElementById("addEmpModal").className = "modal fade in";
                t.EmployeeAddOnFly.performSettings();
                t.SelectedEmployee = empid;
                t.EmployeeAddOnFly.setManagerID(empid);

              } else if (event.target.getAttribute('eventType') === 'performance') {
                let empid = event.target.getAttribute('empID');
                t.empPerformance.setDefaultsOnFly(empid);

                t.empPerformance.showAssesmentEntry = false;
                t.empPerformance.showTab = false;
                t.empPerformance.showObjectiveEntry = false;
                t.empPerformance.showCompetanceEntry = false;
                t.empPerformance.showObjectiveKPIEntry = false;
                t.empPerformance.showCompetanceKPIEntry = false;
                t.empPerformance.showObjectiveKPIList = false;
                t.empPerformance.showCompetanceKPIList = false;

                document.getElementById("empPerformance").style.display = 'block';
                document.getElementById("empPerformance").className = "modal fade in";
              }

            });
          }
        });


        oc.$chart.on('nodedrop.orgchart', (event, extraParams) => {

          emp.UpdateManager(extraParams.draggedNode[0].id, extraParams.dropZone[0].id).subscribe(res => {
            if (res.IsError) {
              alert(res.ErrorMessage);
              return;
            }
            // t.loadTree();
          });


        });

      });
    });
  }

  PerformSettings() {
    this.loadTree();
  }

  AddOnFlySave(msg) {
    this.closeEmployeeModal();
    this.router.navigateByUrl('/RefreshComponent', {skipLocationChange: true}).then(() => {
      this.router.navigate(['EmpStructure']);
    });
  }

  closeEmployeeModal() {
    document.getElementById("addEmpModal").className = "modal fade";
    document.getElementById("addEmpModal").style.display = 'none';
  }

  empPerformanceClose(msg) {
    this.closeEmpPerformacnceModal();
    this.router.navigateByUrl('/RefreshComponent', {skipLocationChange: true}).then(() => {
      this.router.navigate(['EmpStructure']);
    });
  }

  closeEmpPerformacnceModal() {
    document.getElementById("empPerformance").className = "modal fade";
    document.getElementById("empPerformance").style.display = 'none';
  }

  loopChart($hierarchy) {
    const $siblings = $hierarchy.children('.nodes').children('.hierarchy');
    if ($siblings.length) {
      $siblings.filter(':not(.hidden)').first().addClass('first-shown')
        .end().last().addClass('last-shown');
    }
    $siblings.each((index, sibling) => {
      this.loopChart($(sibling));
    });
  }

  filterNodes(keyWord) {
    if (!keyWord.length) {
      window.alert('Please type key word firstly.');
      return;
    } else {
      const $chart = $('#chart-container');
      // disalbe the expand/collapse feture
      $chart.addClass('noncollapsable');
      // distinguish the matched nodes and the unmatched nodes according to the given key word
      $chart.find('.node').filter((index, node) => {
        return $(node).text().toLowerCase().indexOf(keyWord) > -1;
      }).addClass('matched')
        .closest('.hierarchy').parents('.hierarchy').children('.node').addClass('retained');
      // hide the unmatched nodes
      $chart.find('.matched,.retained').each((index, node) => {
        $(node).removeClass('slide-up')
          .closest('.nodes').removeClass('hidden')
          .siblings('.hierarchy').removeClass('isChildrenCollapsed');
        const $unmatched = $(node).closest('.hierarchy').siblings().find('.node:first:not(.matched,.retained)')
          .closest('.hierarchy').addClass('hidden');
      });
      // hide the redundant descendant nodes of the matched nodes
      $chart.find('.matched').each((index, node) => {
        if (!$(node).siblings('.nodes').find('.matched').length) {
          $(node).siblings('.nodes').addClass('hidden')
            .parent().addClass('isChildrenCollapsed');
        }
      });
      // loop chart and adjust lines
      this.loopChart($chart.find('.hierarchy:first'));
    }
  }

  clearFilterResult() {
    $('#chart-container').removeClass('noncollapsable').find('.node').removeClass('matched retained')
      .end().find('.hidden, .isChildrenCollapsed, .first-shown, .last-shown')
      .removeClass('hidden isChildrenCollapsed first-shown last-shown')
      .end().find('.slide-up, .slide-left, .slide-right').removeClass('slide-up slide-right slide-left');
  }


}
