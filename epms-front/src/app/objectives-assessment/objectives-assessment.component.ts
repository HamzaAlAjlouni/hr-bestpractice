import {Component, OnInit, Input, Output, EventEmitter} from "@angular/core";
import {ProjectsService} from "../Services/Projects/projects.service";
import {UsersService} from "../Services/Users/users.service";
import {ProjectsAssessmentService} from "../Services/projects-assessment.service";
import {UserContextService} from "../Services/user-context.service";
import {OrganizationService} from "../Organization/organization.service";

@Component({
  selector: "app-objectives-assessment",
  templateUrl: "./objectives-assessment.component.html",
  styleUrls: ["./objectives-assessment.component.css"],
  providers: [UsersService, ProjectsService],
})
export class ObjectivesAssessmentComponent implements OnInit {
  errorMessage: string;
  filesToUpload: Array<File>;
  selectedFileNames: string[] = [];
  public isLoadingData: Boolean = false;
  fileUploadVar: any;
  uploadResult: any;
  documentsList: File[];
  selectedObjectiveFilterWeight: any = 0;

  @Input()
  AddOnFly = false;

  @Output()
  AddOnFlyAssessmentSave = new EventEmitter<string>();

  ddlSearchYearsList;
  ddlSearchObjectivesList;

  projectAssessmentList;

  ddlSearchYear = new Date().getFullYear() - 1;
  ddlSearchObjective = null;

  formRow;
  PageResources = [];
  ddlSearchUnit: any;
  plannedWeight: number = 0;
  actualWeight: number = 0;

  constructor(
    private projectsAssessmentService: ProjectsAssessmentService,
    private userContextService: UserContextService,
    private userService: UsersService,
    private projectService: ProjectsService,
    private OrganizationService: OrganizationService
  ) {
    this.errorMessage = "";
    this.filesToUpload = [];
    this.selectedFileNames = [];
    this.uploadResult = "";
    this.fillDDLSearchObjectives();

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
    this.OrganizationService.saveObjectivesKPIsAssessment(
      this.objectivesKPIsList
    ).subscribe((res) => {
      if (res.IsError) {
        alert(res.ErrorMessage + ",");
        return;
      }
      alert("0");
      this.LoadObjectiveKPIs();
    });
  }

  perProjectView = false;
  perObjectiveView = false;
  onFlyProjectID;
  onFlyObjID;

  setDefaultAddOnFly(projectID) {
    this.onFlyProjectID = projectID;
    this.perObjectiveView = false;
    this.perProjectView = true;
    this.projectsAssessmentService
      .getProjectAssessment(
        this.userContextService.CompanyID,
        -1,
        -1,
        -1,
        this.userContextService.language,
        projectID
      )
      .subscribe((res) => {
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

  objectivesKPIsList;

  LoadObjectiveKPIs() {
    this.OrganizationService.getObjectiveKpiByObjective(
      this.ddlSearchObjective,
      this.userContextService.language
    ).subscribe((res) => {
      if (res.IsError) {
        alert(res.ErrorMessage + ",1");
        return;
      }
      this.objectivesKPIsList = res.Data;
      console.log('res check', res);
      this.actualWeight = res.TotalActuleWiegth;
      this.plannedWeight = res.TotalStrategyWiegth;
    });

    this.projectsAssessmentService
      .getProjectAssessment(
        this.userContextService.CompanyID,
        this.ddlSearchYear,
        -1,
        this.ddlSearchObjective,
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
        this.fillDDLSearchObjectives();
      }
    });
  }

  stratigicObjectiveFilterChange(event: any) {
    this.selectedObjectiveFilterWeight = this.ddlSearchObjectivesList.filter(a => a.id == event.target.value)[0].Weight;
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
        console.log(res.Data);
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

  addedFiles: any = [];

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
            //   this.searchProjectsAssessment();
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
            // this.searchProjectsAssessment();
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

  getColorHex(resultColor: any) {
    if (resultColor == "green") {

      return "#7ae47a";
    } else if (resultColor == "red") {

      return "#e33939";
    } else if (resultColor == "yellow") {

      return "#eeee5c";
    } else if (resultColor == "gray") {

      return "#a2a2a2";
    }

  }
}
