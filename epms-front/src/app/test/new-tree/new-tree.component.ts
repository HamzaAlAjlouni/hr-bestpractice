import { Component, OnInit, AfterViewInit } from '@angular/core';
import { StratigicObjService } from '../../OrganizationObjectives/strategic-objectives-charts/stratigic-obj.service';
import { EmployeeService } from '../../Organization/employees/employee.service';
import { UserContextService } from '../../Services/user-context.service';

declare var $: any;
declare var orgchart: any;
@Component({
  selector: 'app-new-tree',
  templateUrl: './new-tree.component.html',
  styleUrls: ['./new-tree.component.css'],
  providers: [StratigicObjService, EmployeeService]
})
export class NewTreeComponent implements AfterViewInit {

  constructor(private s: StratigicObjService, private emp: EmployeeService, private userContextService: UserContextService) {

    this.loadTree();
  }


  loadTree(){
    this.emp.LoadEmployeesNewChart(1, null, null, null, this.userContextService.language).subscribe(res => {

      var emp = this.emp;
      var t = this;
      $(function () {
        
        var datascource = res.Data[0];
        var nodeTemplate = function (data) {
          return `
          <div class="title" style='width:100%'>${data.Unit}</div>
          <div class="content" style='height:100%;font-size:1.1em;'>
          <img style='border-radius: 25px;' width='70px' height='70px' src="${data.img}"/> 
          <h2>${data.Name}</h2>
          

          <button class="btn btn-success btn-sm"  eventType='edit' empID="${data.id}"
          data-widget="Add" data-toggle="tooltip" title="" data-original-title="Edit Objective">
              <i class="fa fa-pencil"></i>
          </button>
          &nbsp;
          <button class="btn btn-success btn-sm" eventType='add' empID="${data.id}"
          data-widget="Add" data-toggle="tooltip" title="Add Project"
          data-original-title="Add Project">
              <i class="fa fa-plus"></i>
          </button>


          </div> 
        `;
        };



        var oc = $('#chart-container').orgchart({
          'data': datascource,
          'draggable': true,
          'nodeTemplate': nodeTemplate,
          'createNode': ($node, data) => {
            $node.on('click', (event) => {
              if(event.target.type === "submit"){
                if(event.target.getAttribute('eventType') === 'edit'){
                  // event.target.getAttribute('empID')
                  // show popup
                }
                else if(event.target.getAttribute('eventType') === 'add'){
                  // event.target.getAttribute('empID')
                  // show popup
                }
              }
            });
          }
        });

        
        oc.$chart.on('nodedrop.orgchart', function(event, extraParams) {
 
          emp.UpdateManager(extraParams.draggedNode[0].id,extraParams.dropZone[0].id).subscribe(res=>{
            if(res.IsError){
              alert(res.ErrorMessage);
              return;
            }
            //t.loadTree();
          }); 


        });

      });
    });
  }

  alerttt(){
    alert('sss');
  }
  ngAfterViewInit() {

  }


}
