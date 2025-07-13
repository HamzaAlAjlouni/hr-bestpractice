import {Component, OnInit, Input, Output, EventEmitter} from '@angular/core';
import {ProjectsAssessmentService} from '../Services/projects-assessment.service';
import {UserContextService} from '../Services/user-context.service';
import {UsersService} from '../Services/Users/users.service';
import {ProjectsService} from '../Services/Projects/projects.service';
import {OrganizationService} from '../Organization/organization.service';
import {ActionPlanService} from '../action-plan/action-plan.service';


@Component({
  selector: 'app-action-plans-assessment',
  templateUrl: './action-plans-assessment.component.html',
  styleUrls: ['./action-plans-assessment.component.css'],
  providers: [UsersService, ProjectsService, ActionPlanService]
})
export class ActionPlansAssessmentComponent implements OnInit {

  errorMessage: string;
  filesToUpload: Array<File>;
  selectedFileNames: string[] = [];
  public isLoadingData: Boolean = false;
  fileUploadVar: any;
  uploadResult: any;
  documentsList: File[];

  @Input()
  AddOnFly = false;

  @Output()
  AddOnFlyAssessmentSave = new EventEmitter<string>();


  ddlSearchYearsList;
  ddlSearchProjectsList;

  projectAssessmentList;

  ddlSearchYear ;
  ddlSearchProject = null;

  formRow;
  PageResources = [];

  ddlUnitSearch;
  unitLst;
  projectsKPIList = [];
  ddlProjectKPISearch;
  showWeight: boolean = false;
  plannedWeight: any;
  actualWeight: any;

  fillUnitLst() {

    this.projectService.GetUnits(this.userContextService.CompanyID, this.userContextService.language).subscribe(
      res => {
        this.unitLst = this.userContextService.RoleId !== 5 ? res.Data : res.Data.filter(a => {
          return a.ID === this.userContextService.UnitId;
        });


        this.ddlUnitSearch = null;
      }
    );
  }

  is_reveiwer_user;

  getApprovals() {
    this.OrganizationService.GetApprovalSetupByURL(window.location.hash, this.userContextService.CompanyID).subscribe(res => {
      if (res.IsError) {
        this.is_reveiwer_user = false;
        return;
      }
      if (this.userContextService.Username.toLocaleLowerCase() == res.Data.reviewing_user.toLocaleLowerCase()) {
        this.is_reveiwer_user = true;
      } else {
        this.is_reveiwer_user = false;

      }
    });
  }

  constructor(
    private projectsAssessmentService: ProjectsAssessmentService,
    private userContextService: UserContextService,
    private userService: UsersService,
    private projectService: ProjectsService,
    private OrganizationService: OrganizationService,
    private ActionPlansService: ActionPlanService
  ) {
    this.errorMessage = "";
    this.filesToUpload = [];
    this.selectedFileNames = [];
    this.uploadResult = "";

    this.getApprovals();
    this.fillDDLSearchObjectives();

    this.fillDDLSearchYears();
    this.fillUnitLst();

    this.userService.GetLocalResources(window.location.hash, this.userContextService.CompanyID, this.userContextService.language).subscribe(res => {
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

  ngOnInit() {

  }

  ddlYearChange() {

    this.fillDDLSearchObjectives();

  }

  onSaveKpisResults() {
    this.ActionPlansService.saveActionPlanAssessment(this.actionPlansList).subscribe(res => {
      if (res.IsError) {
        alert(res.ErrorMessage + ",");
        return;
      }
      alert("0");
      this.LoadActionPlans();
    })
  }


  perProjectView = false;
  perObjectiveView = false;
  onFlyProjectID;
  onFlyObjID;

  setDefaultAddOnFly(projectID) {
    this.onFlyProjectID = projectID;
    this.perObjectiveView = false;
    this.perProjectView = true;
    this.projectsAssessmentService.getProjectAssessment(
      this.userContextService.CompanyID,
      -1,
      -1,
      -1,
      this.userContextService.language
      ,
      projectID
    ).subscribe(res => {
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
    this.projectsAssessmentService.getProjectAssessment(
      this.userContextService.CompanyID,
      -1,
      -1,
      ObjectiveID,
      this.userContextService.language
    ).subscribe(res => {
      if (res.IsError) {
        alert(res.ErrorMessage);
        return;
      }
      this.projectAssessmentList = res.Data;
    });
  }


  actionPlansList

  LoadActionPlans() { // Add project kpi in the request

    this.ActionPlansService.GetProjectActionPlansForAssessment(this.ddlSearchProject, this.ddlProjectKPISearch, this.userContextService.language).subscribe(res => {
      if (res.IsError) {
        alert(res.ErrorMessage + ",1");
        return;
      }
      this.actionPlansList = res.Data;
      console.log('resultPercentageAll', res.Data)
      const initialValue = 0;
      this.actualWeight = res.Data.reduce((previousValue, currentValue) => previousValue + currentValue.resultPercentageAll,
        initialValue) / 100
    })

    this.projectsAssessmentService
      .GetPlannedAndActualWeightForAll(
        this.userContextService.CompanyID,
        this.ddlSearchYear,
        this.ddlUnitSearch,
        0,
        this.userContextService.language,
        null,
        this.ddlSearchProject,
      )
      .subscribe((res) => {
        if (res.IsError == false) {
          this.showWeight = true;
          this.plannedWeight = res.Data.PlannedWeights;
          // this.actualWeight = res.Data.ActualWeights;
        } else {
          this.showWeight = false;
          alert(res.ErrorMessage);
        }
      });
  }


  fillDDLSearchYears() {
    this.projectsAssessmentService.getYears().subscribe(res => {
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
      }
    });
  }

  fillDDLSearchObjectives() {
    this.projectService.SearchAllProjectsList(
      this.userContextService.CompanyID,
      this.userContextService.BranchID,
      this.ddlSearchYear,
      (this.userContextService.RoleId !== 5 ? this.ddlUnitSearch == null ? 0 : this.ddlUnitSearch : this.userContextService.UnitId), 0, this.userContextService.language
    ).subscribe(res => {
      if (res.IsError) {
        alert(res.ErrorMessage);
        return null;
      }
      this.ddlSearchProjectsList = res.Data;
    })
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

  addedFiles: any = [];

  addFile(planID, file) {
    // "Are you sure, you want to upload this evident ?"
    if (confirm(this.GetLocalResourceObject('confirmUploadEvident'))) {
      this.ActionPlansService.uploadFiles(file, 1, planID, this.userContextService.Username).subscribe(res => {
        if (res.IsError) {
          alert(res.ErrorMessage + ',' + 1);
          return;
        }
        this.LoadActionPlans();
      });
    }
  }

  RemoveEvident(docID) {
    // Are you sure, you want to remove this evident ?
    if (confirm(this.GetLocalResourceObject('confirmDeleteEvident'))) {
      this.ActionPlansService.RemoveEvident(docID, this.userContextService.Username).subscribe(res => {
        if (res.IsError) {
          alert(res.ErrorMessage + ',' + 1);
          return;
        }
        this.LoadActionPlans();
      });
    }
  }


  getStyleObj(strStyle) {
    return JSON.parse(strStyle);
  }

  ConfirmResult(planID) {
    this.OrganizationService.UpdateActionAssessmentStatus(planID, 2).subscribe(res => {
      if (res.IsError) {
        alert(res.ErrorMessage + ',1');
        return;
      }
      alert("0");
      this.LoadActionPlans();
    });
  }

  DeclineResult(planID) {
    this.OrganizationService.UpdateActionAssessmentStatus(planID, 3).subscribe(res => {
      if (res.IsError) {
        alert(res.ErrorMessage + ',1');
        return;
      }
      alert("0");
      this.LoadActionPlans();
    });
  }

  getProjectKPIByProjectId(id) {
    this.OrganizationService.getObjectiveKpiByObjective(id, this.userContextService.language).subscribe(res => {
      this.projectsKPIList = res.Data;
    })
  }

}

