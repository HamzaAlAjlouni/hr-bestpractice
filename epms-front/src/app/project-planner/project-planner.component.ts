import {Component, OnInit, ViewChild} from '@angular/core';
import {EmployeeService} from '../Organization/employees/employee.service';
import {UserContextService} from '../Services/user-context.service';
import {StratigicObjService} from '../OrganizationObjectives/strategic-objectives-charts/stratigic-obj.service';
import {UsersService} from '../Services/Users/users.service';


declare var $: any;
declare var OrgChart: any;
declare var BALKANGraph: any;

@Component({
  selector: 'app-project-planner',
  templateUrl: './project-planner.component.html',
  styleUrls: ['./project-planner.component.css']
})
export class ProjectPlannerComponent implements OnInit {
  @ViewChild('ObjectiveModal', {read: false, static: false}) ObjectiveModal
  @ViewChild('ProjectModal', {read: false, static: false}) ProjectModal
  @ViewChild('AssessmentModal', {read: false, static: false}) AssessmentModal
  @ViewChild('ObjectiveKpiModal', {read: false, static: false}) ObjectiveKpiModal

  constructor(private stratigicService: StratigicObjService, private user: UserContextService, private userService: UsersService) {
    this.PerformSettings();
  }

  AddOnFly;
  objList;
  ddlYearSearch;
  yearslist;
  treeView;
  allList;
  isHide = true;

  ngOnInit() {

    this.userService.GetLocalResources(window.location.hash, this.user.CompanyID, this.user.language).subscribe(res => {
      if (res.IsError) {
        alert(res.ErrorMessage);
        return;
      }
      this.PageResources = res.Data;
    });
  }

  PageResources = [];

  GetLocalResourceObject(resourceKey) {
    for (let i = 0; i < this.PageResources.length; i++) {
      if (this.PageResources[i].resource_key === resourceKey) {
        return this.PageResources[i].resource_value;
      }
    }
  }

  LoadChart() {
    this.isHide = true;
    this.stratigicService.LoadAll(this.user.CompanyID, this.ddlYearSearch, this.user.language).subscribe(res => {
      this.allList = res.Data;
      this.isHide = false;
      setTimeout(() => {

        $('#OrgObject').stiffChart({
          lineColor: '#3498db',
          lineWidth: 2,
          lineShape: 'curved',
          layoutType: 'vertical', // 'vertical' or 'horizontal'
          childCounter: true,
          activeClass: 'chart-active',
          bootstrapPopover: true // enable <a href="https://www.jqueryscript.net/tags.php?/Bootstrap/">Bootstrap</a> popover
        });

      }, 2000);

    });
  }

  PerformSettings() {
    this.stratigicService.LoadYears().subscribe(res => {
      if (res.IsError) {
        alert(res.ErrorMessage);
        return;
      }
      this.yearslist = res.Data;
      if (res.Data.length > 0) {
        this.yearslist.forEach((element) => {
          this.ddlYearSearch = element.id;
        });
        this.LoadChart();
      }
    });
  }

  onAddObjective(id) {
    document.getElementById("addObjectiveModal").style.display = 'block';
    document.getElementById("addObjectiveModal").className = "modal fade in";
    this.ObjectiveModal.AddMode();
    this.ObjectiveModal.setYear(this.ddlYearSearch);
  }

  onEditOjective(id) {
    document.getElementById("addObjectiveModal").style.display = 'block';
    document.getElementById("addObjectiveModal").className = "modal fade in";
    this.ObjectiveModal.GetStratigicObjectivebyID(id.split('_')[0]);
    this.ObjectiveModal.setYear(this.ddlYearSearch);
  }

  onAddProject(id) {
    document.getElementById("addProjectModal").style.display = 'block';
    document.getElementById("addProjectModal").className = "modal fade in";
    this.ProjectModal.AddProject();
    this.ProjectModal.setAddOnFlyDefaults(this.ddlYearSearch, id.split('_')[0], true);
  }

  onEditProject(id) {
    document.getElementById("addProjectModal").style.display = 'block';
    document.getElementById("addProjectModal").className = "modal fade in";
    //this.ProjectModal.setAddOnFlyDefaults(this.ddlYearSearch, id.split('_')[0]);
    this.ProjectModal.EditProject(id.split('_')[0]);
  }

  onAddAssessment(id) {
    document.getElementById("addAssessmentModal").style.display = 'block';
    document.getElementById("addAssessmentModal").className = "modal fade in";
    this.AssessmentModal.setDefaultAddOnFly(id.split('_')[0]);
  }


  onProjectsListClick(id) {
    document.getElementById("addProjectModal").style.display = 'block';
    document.getElementById("addProjectModal").className = "modal fade in";
    this.ProjectModal.AddProject();
    this.ProjectModal.setAddOnFlyDefaults(this.ddlYearSearch, id.split('_')[0], false);
    this.ProjectModal.setOnFlyWithShowProjectsList();
  }

  AddOnFlyObjectiveSave(msg) {
    this.closeaddObjectiveModal();
    this.PerformSettings();
  }


  closeaddObjectiveModal() {
    document.getElementById("addObjectiveModal").className = "modal fade";
    document.getElementById("addObjectiveModal").style.display = 'none';
  }

  AddOnFlyProjectSave(msg) {
    this.closeProjectModal();
    this.PerformSettings();

  }

  closeProjectModal() {
    document.getElementById("addProjectModal").className = "modal fade";
    document.getElementById("addProjectModal").style.display = 'none';
  }

  AddOnFlyAssessmentSave(msg) {
    this.closeAssessmentModal();
    this.PerformSettings();

  }

  closeAssessmentModal() {
    document.getElementById("addAssessmentModal").className = "modal fade";
    document.getElementById("addAssessmentModal").style.display = 'none';
  }


  onAddObjecitveKPIsResult(id) {
    document.getElementById("ObjeciveKpiModal").style.display = 'block';
    document.getElementById("ObjeciveKpiModal").className = "modal fade in";
    this.ObjectiveKpiModal.setObjDefaultAddOnFly(id.split('_')[0], true);
  }

  closeObjeciveKpiModal() {
    document.getElementById("ObjeciveKpiModal").className = "modal fade";
    document.getElementById("ObjeciveKpiModal").style.display = 'none';
  }

}
