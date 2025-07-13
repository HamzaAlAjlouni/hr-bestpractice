import {
  Component,
  OnInit,
  ViewChild,
  Input,
  Output,
  EventEmitter,
} from "@angular/core";
import {SettingsService} from "../../settings/settings.service";
import {UserContextService} from "../../Services/user-context.service";
import {UsersService} from "../../Services/Users/users.service";
import {CompanyService} from "src/app/Services/company.service";
import {ProjectsService} from "src/app/Services/Projects/projects.service";
import {OrganizationService} from "../organization.service";

declare var $: any;

@Component({
  selector: "app-stratigicobjectives",
  templateUrl: "./stratigicobjectives.component.html",
  styleUrls: ["./stratigicobjectives.component.css"],
  providers: [OrganizationService, ProjectsService],
})
export class StratigicObjectivesComponent implements OnInit {
  @Input()
  AddOnFly = false;

  @Output()
  AddOnFlyObjectiveSave = new EventEmitter<string>();

  @ViewChild("gvObjectives", {read: false, static: false}) gvObjectives;
  @ViewChild("gvKPIs", {read: false, static: false}) gvKPIs;

  @ViewChild("ProjectsCom", {read: false, static: false}) ProjectsCom;
  @ViewChild("gvDocuments", {read: false, static: false}) gvDocuments;
  @ViewChild("evidancesKPI", {read: false, static: false}) evidancesKPI;
  showEvidancesKPI: boolean = false;
  txtEvidanceKPI: string = "";
  showTabs = false;
  selectedObjectiveID;
  txtKpiName;
  txtKpiDesc;
  txtKpiweight;
  txtKpiTarget;
  ddlKpiBSC;
  ddlKpiMeasurement;
  ddlBetterUpDown = 1;
  BetterUpDownList = [
    {id: 1, name: "Better Up"},
    {id: 2, name: "Better Down"}

  ];

  remainingWeight;
  remainingKPIWeight;
  remainingObjectivesKpisWeight;

  showKpiEntry = false;
  showUpdateEvidance: boolean = false;
  txtObjectiveCode;
  txtObjectiveOrder;
  ddlSearchObjectiveYear = new Date().getFullYear();

  txtObjectiveName;
  txtObjectiveWeight;
  ddlObjectiveYear;
  txtdescription;
  ObjectivesList;
  years;
  updateMode = false;
  addMode = false;
  selectID;
  ViewTable = false;
  ddlObjBSC;
  PageResources = [];

  txtValues;
  txtVision;
  txtMission;
  ddlObjBSCSearch = "";
  ddlObjKpiBSCSearch = "";

  lblSelectedStratigicObjective;
  showAddEvidance: boolean = false;
  selectedEvidanceId: number;
  evidanceId: any;
  evidancesList: any = [];
  evidanceSelect: any;
  modificationPermission = false;

  bscList = [
    {id: 1, name: this.GetLocalResourceObject('lblKpiBSCFinancialOption')},
    {id: 2, name: this.GetLocalResourceObject('lblKpiBSCCustomersOption')},
    {id: 3, name: this.GetLocalResourceObject('lblKpiBSCInternalProcessOption')},
    {id: 4, name: this.GetLocalResourceObject('lblKpiBSCLearninggrowthOption')}
  ];

  constructor(
    private OrganizationService: OrganizationService,
    private settingsService: SettingsService,
    private userContextService: UserContextService,
    private userService: UsersService,
    private companyService: CompanyService,
    private projectService: ProjectsService
  ) {
    this.modificationPermission = this.userContextService.RoleId != 5;

    this.performSettings();

    this.userService
      .GetLocalResources(
        window.location.hash,
        this.userContextService.CompanyID,
        this.userContextService.language
      )
      .subscribe((res) => {
        if (res.IsError) {
          alert(res.ErrorMessage + "," + 1);
          return;
        }
        this.PageResources = res.Data;
        this.bscList = [{id: 1, name: this.GetLocalResourceObject('lblKpiBSCFinancialOption')},
          {id: 2, name: this.GetLocalResourceObject('lblKpiBSCCustomersOption')},
          {id: 3, name: this.GetLocalResourceObject('lblKpiBSCInternalProcessOption')},
          {id: 4, name: this.GetLocalResourceObject('lblKpiBSCLearninggrowthOption')}];
      });

    this.LoadCompanyByID(this.userContextService.CompanyID);
  }

  toggleComp() {
    $("#dvCompInfo").toggle();
  }

  refreshObjKpisList() {
    this.LoadObjectiveKPIs();
  }

  ngOnInit() {
    setTimeout(() => this.SearchAllStratigicObjectives(), 2000);
  }

  LoadCompanyByID(ID) {
    this.companyService.LoadCompanyByID(ID).subscribe((res) => {
      if (res.IsError) {
        alert(res.ErrorMessage);
        return;
      }

      this.txtValues = res.Data.company_values;
      this.txtVision = res.Data.Vision;
      this.txtMission = res.Data.Mission;
    });
  }

  GetLocalResourceObject(resourceKey) {
    for (let i = 0; i < this.PageResources.length; i++) {
      if (this.PageResources[i].resource_key === resourceKey) {
        return this.PageResources[i].resource_value;
      }
    }
  }

  setYear(year) {
    this.ddlObjectiveYear = year;
  }

  performSettings() {
    this.txtObjectiveName = "";
    this.txtObjectiveWeight = 0;
    this.txtdescription = "";
    this.updateMode = false;
    this.selectID = null;
    this.LoadYears();
    this.ViewTable = false;
    this.addMode = false;
  }

  AddMode() {
    this.txtObjectiveName = "";
    this.txtObjectiveWeight = 0;
    this.txtdescription = "";
    this.ddlObjBSC = null;
    this.updateMode = false;
    this.selectID = null;
    this.addMode = true;
    this.showTabs = false;

    this.getObjectivesRemainingWeight(
      this.userContextService.CompanyID,
      this.ddlObjectiveYear
    );
  }

  SaveStratigicObjective() {
    if (
      this.txtObjectiveWeight > this.remainingWeight ||
      this.txtObjectiveWeight < 1
    ) {
      alert(this.GetLocalResourceObject("InvalidWeight") + "," + 1);
      return;
    }
    if (!this.updateMode) {
      this.OrganizationService.SaveStratigicObjective(
        this.txtObjectiveName,
        this.userContextService.CompanyID,
        this.txtObjectiveWeight,
        this.ddlObjectiveYear,
        this.txtdescription,
        this.userContextService.Username,
        this.userContextService.language,
        this.ddlObjBSC
      ).subscribe((res) => {
        if (res.IsError) {
          alert(res.ErrorMessage + "," + 1);
          return;
        }
        this.addMode = false;
        this.updateMode = false;
        alert("0");
        this.SearchAllStratigicObjectives();
        if (this.AddOnFly) {
          this.AddOnFlyObjectiveSave.emit(
            this.GetLocalResourceObject("SuccessMsg")
          );
        }
      });
    } else {
      this.OrganizationService.UpdateStratigicObjective(
        this.selectID,
        this.txtObjectiveName,
        this.userContextService.CompanyID,
        this.txtObjectiveWeight,
        this.ddlObjectiveYear,
        this.txtdescription,
        this.userContextService.Username,
        this.userContextService.language,
        this.ddlObjBSC
      ).subscribe((res) => {
        if (res.IsError) {
          alert(res.ErrorMessage + "," + 1);
          return;
        }
        this.SearchAllStratigicObjectives();
        this.addMode = false;
        this.updateMode = false;
        alert("0");
        if (this.AddOnFly) {
          this.AddOnFlyObjectiveSave.emit(
            this.GetLocalResourceObject("SuccessMsg")
          );
        }
      });
    }
  }

  LoadYears() {
    this.settingsService.LoadAllYears().subscribe(
      (result) => {
        if (result.IsError) {
          alert(result.ErrorMessage + "," + 1);
          return;
        }
        this.years = result.Data;
        console.log(result.Data)


        if (this.years != null && this.years.length > 0) {
          this.years.forEach((element) => {
            this.ddlObjectiveYear = element.id;
          });
          console.log('default year', this.ddlObjectiveYear)
        }
      },
      (error) => console.error(error)
    );
  }

  gvObjectivesEvent(event) {
    if (event[1] == "edit") {
      this.addMode = false;
      this.updateMode = false;
      this.GetStratigicObjectivebyID(
        event[0],
        this.userContextService.language
      );
    } else if (event[1] == "delete") {
      if (confirm(this.GetLocalResourceObject("AreYouSure"))) {
        this.DeleteStratigicObjectiveByID(event[0]);
      }
    } else if (event[1] == "details") {
      this.selectedObjectiveID = event[0];

      this.ShowObjectiveDetails();
    }
  }

  SearchAllStratigicObjectives() {
    this.OrganizationService.SearchAllStratigicObjectives(
      this.userContextService.CompanyID,
      this.ddlObjectiveYear,
      this.userContextService.language,
      this.ddlObjBSCSearch
    ).subscribe((res) => {
      if (res.IsError) {
        alert(res.ErrorMessage + "," + 1);
        return;
      }

      this.ObjectivesList = res.Data;
      this.addMode = false;
      this.updateMode = false;

      let cols = [
        {HeaderText: "#", DataField: "serial"},
        {
          HeaderText: this.GetLocalResourceObject("lblObjectiveName"),
          DataField: "name",
        },
        {
          HeaderText: this.GetLocalResourceObject("lblObjectivesWeight"),
          DataField: "Weight",
        },
        {
          HeaderText: "KPIs Count",
          DataField: "KpiSCount",
        },
        {
          HeaderText: this.GetLocalResourceObject("lblBSC"),
          DataField: "BSC_Name",
        },
      ];
      let actions = [
        {
          title: this.GetLocalResourceObject("lblEdit"),
          DataValue: "id",
          Icon_Awesome: "fa fa-edit",
          Action: "edit",
        },
        {
          title: this.GetLocalResourceObject("lblDelete"),
          DataValue: "id",
          Icon_Awesome: "fa fa-trash",
          Action: "delete",
        },
        {
          title: this.GetLocalResourceObject("lblDetails"),
          DataValue: "id",
          Icon_Awesome: "fa fa-list-alt",
          Action: "details",
        },
      ];
      let viewActions = [
        {
          title: this.GetLocalResourceObject("lblDetails"),
          DataValue: "id",
          Icon_Awesome: "fa fa-list-alt",
          Action: "details",
        },
      ];

      if (this.ObjectivesList.length > 0) this.ViewTable = true;

      this.gvObjectives.bind(
        cols,
        this.ObjectivesList,
        "gvObjectives",
        this.modificationPermission && this.hasEditPermission() ? actions : viewActions
      );
      this.showTabs = false;
      this.resetObjectiveKpiEntry();
    });
  }

  GetStratigicObjectivebyID(id, lang) {
    this.OrganizationService.GetStratigicObjectivebyID(id, lang).subscribe(
      (res) => {
        if (res.IsError) {
          alert(res.ErrorMessage + "," + 1);
          return;
        }

        this.txtObjectiveName = res.Data.name;
        this.txtObjectiveWeight = res.Data.Weight;
        this.ddlObjectiveYear = res.Data.Year;
        this.txtdescription = res.Data.Description;
        this.remainingWeight = res.Data.max;
        this.selectID = res.Data.id;
        this.ddlObjBSC = res.Data.bsc;
        this.updateMode = true;
        this.showTabs = false;
      }
    );
  }

  GetStratigicObjectivebyNameById() {
    this.OrganizationService.GetStratigicObjectivebyID(
      this.selectedObjectiveID,
      this.userContextService.language
    ).subscribe((res) => {
      if (res.IsError) {
        alert(res.ErrorMessage + "," + 1);
        return;
      }

      this.lblSelectedStratigicObjective = res.Data.name;
    });
  }

  getObjectivesRemainingWeight(companyId, year) {
    this.OrganizationService.getObjectivesRemainingWeight(
      companyId,
      year
    ).subscribe((res) => {
      if (res.IsError) {
        alert(res.ErrorMessage + "," + 1);
        this.remainingWeight = 0;
      }
      this.remainingWeight = res.Data;
      if (!this.updateMode) {
        this.txtObjectiveWeight = this.remainingWeight;
      }
    });
  }

  getObjectiveKPIRemainingWeight(objID) {
    this.OrganizationService.getObjectiveKPIRemainingWeight(objID, 1).subscribe(
      (res) => {
        if (res.IsError) {
          alert(res.ErrorMessage + "," + 1);
          this.remainingKPIWeight = 0;
        }
        this.remainingKPIWeight = res.Data;
        if (!this.updateMode) {
          this.txtKpiweight = this.remainingKPIWeight;
        }
      }
    );
  }

  getObjectivesKPIRemainingWeight(objectiveId) {
    this.OrganizationService.getObjectivesKPIRemainingWeight(
      objectiveId,
      1
    ).subscribe((res) => {
      if (res.IsError) {
        alert(res.ErrorMessage + "," + 1);
        this.remainingObjectivesKpisWeight = 0;
      }
      this.remainingObjectivesKpisWeight = res.Data;
      if (!this.updateMode) {
        this.txtKpiweight = this.remainingObjectivesKpisWeight;
      }
    });
  }

  DeleteStratigicObjectiveByID(id) {
    this.OrganizationService.DeleteStratigicObjectiveByID(id).subscribe(
      (res) => {
        if (res.IsError) {
          alert(res.ErrorMessage + "," + 1);
          return;
        }
        this.addMode = false;
        this.updateMode = false;
        alert("0");
        this.performSettings();
        this.SearchAllStratigicObjectives();
      }
    );
  }

  ShowObjectiveDetails() {
    this.addMode = false;
    this.updateMode = false;
    this.showTabs = true;
    setTimeout(() => {
      this.GetStratigicObjectivebyNameById();
      this.LoadObjectiveKPIs();
      this.showProjectsRelatedList();
      this.LoadProjectDocuments(this.selectedObjectiveID);
    }, 500);
  }

  showProjectsRelatedList() {
    this.ProjectsCom.setAddOnFlyDefaults(
      this.ddlObjectiveYear,
      this.selectedObjectiveID
    );
    this.ProjectsCom.setOnFlyWithShowProjectsList();
  }

  AddOnFlyProjectSave(event) {
  }

  LoadObjectiveKPIs() {
    console.log("getObjectiveKpiByObjective");
    this.OrganizationService.getObjectiveKpiByObjective(
      this.selectedObjectiveID,
      this.userContextService.language,
      this.ddlObjKpiBSCSearch
    ).subscribe((res) => {
      if (res.IsError) {
        //alert(res.ErrorMessage);
        alert(res.ErrorMessage + "," + 1);
        return;
      }
      this.resetObjectiveKpiEntry();
      res.Data.forEach(a => {
        a.BetterUpDownName = a.BetterUpDown === 1 ? 'Up' : 'Down';

      });
      let cols = [
        {
          HeaderText: this.GetLocalResourceObject("colKpiName"),
          DataField: "Name",
        }, {
          HeaderText: "Better up/down",
          DataField: "BetterUpDownName",
        },
        {
          HeaderText: this.GetLocalResourceObject("colKpiTarget"),
          DataField: "TargetDesc",
        },
        {
          HeaderText: this.GetLocalResourceObject("colKpiWeight"),
          DataField: "WeightDesc",
        },
        {
          HeaderText: this.GetLocalResourceObject("lblBSC"),
          DataField: "BSC_Name",
        },
      ];
      let actions = [
        {
          title: this.GetLocalResourceObject("lblEdit"),
          DataValue: "ID",
          Icon_Awesome: "fa fa-edit",
          Action: "edit",
        },
        {
          title: this.GetLocalResourceObject("lblDelete"),
          DataValue: "ID",
          Icon_Awesome: "fa fa-trash",
          Action: "delete",
        },
        {
          title: this.GetLocalResourceObject("lblDetails"),
          DataValue: "ID",
          Icon_Awesome: "fa fa-list-alt",
          Action: "details",
        },
      ];
      let viewActions = [
        {
          title: this.GetLocalResourceObject("lblDetails"),
          DataValue: "ID",
          Icon_Awesome: "fa fa-list-alt",
          Action: "details",
        },
      ];
      console.log("getObjectiveKpiByObjective", res.Data);
      this.gvKPIs.bind(cols, res.Data, "gvKPIs", this.hasEditPermission() ? actions : viewActions);
    });
  }

  gvKPIsHandler(event) {
    if (event[1] == "edit") {
      this.prepareObjecitveKpiForEdit(event[0]);
    } else if (event[1] == "delete") {
      if (confirm(this.GetLocalResourceObject("AreYouSure"))) {
        this.OrganizationService.DeleteObjectiveKpi(event[0]).subscribe(
          (res) => {
            if (res.IsError) {
              alert(res.ErrorMessage + "," + 1);
              return;
            }
            this.LoadObjectiveKPIs();
          }
        );
      }
    } else if (event[1] == "details") {
      // console.log('this is the evidance', event)
      this.showEvidancesKPI = true;
      this.selectedEvidanceId = event[0];
      this.getEvidanceByObjectiveKPI(this.selectedEvidanceId);
    }
  }

  hasEditPermission() {

    return this.userContextService.Role !== "Unit";

  }

  evidancesKPIHandler(event) {
    this.evidanceId = event[0];
    if (event[1] == "edit") {
      // add edit
      this.showUpdateEvidance = true;
      this.showAddEvidance = false;
      this.evidanceSelect = this.evidancesList.find(
        (evidance) => evidance.ID == this.evidanceId
      );
      this.txtEvidanceKPI = this.evidanceSelect.doc_name;
    } else {
      this.evidanceId = event[0];
      // working here
      this.OrganizationService.deleteEvidanceForKPIById(
        this.evidanceId
      ).subscribe((res) => {
        this.getEvidanceByObjectiveKPI(this.selectedEvidanceId);
        // this.SearchAllStratigicObjectives();
      });
    }
  }

  updateEvidancesForKPI() {
    this.OrganizationService.updateEvidanceForKPIById(
      this.txtEvidanceKPI,
      this.evidanceId,
      this.userContextService.Username
    ).subscribe(() => {
      this.showUpdateEvidance = false;
      this.txtEvidanceKPI = "";
      this.getEvidanceByObjectiveKPI(this.selectedEvidanceId);
    });
  }

  prepareObjecitveKpiForEdit(kpiID) {
    this.OrganizationService.getObjectiveKpiByID(
      kpiID,
      this.userContextService.language
    ).subscribe((res) => {
      if (res.IsError) {
        alert(res.ErrorMessage + "," + 1);
        return;
      }
      this.txtKpiDesc = res.Data.Description;
      this.txtKpiName = res.Data.Name;
      this.ddlBetterUpDown = res.Data.BetterUpDown;
      this.txtKpiTarget = res.Data.Target;
      this.txtKpiweight = res.Data.Weight;
      this.ddlKpiBSC = res.Data.bsc;
      this.ddlKpiMeasurement = res.Data.measurement;
      this.selectedObjectiveKpiId = kpiID;
      this.remainingKPIWeight = res.Data.max;
      this.showKpiEntry = true;
    });
  }

  selectedObjectiveKpiId;

  resetObjectiveKpiEntry() {
    this.txtKpiDesc = "";
    this.txtKpiName = "";
    this.txtKpiTarget = "";
    this.txtKpiweight = "";
    this.ddlKpiBSC = null;
    this.ddlBetterUpDown = 1;
    this.ddlKpiMeasurement = null;
    this.showKpiEntry = false;
    this.selectedObjectiveKpiId = null;
  }

  SaveObjectiveKpi() {
    if (this.txtKpiweight > this.remainingKPIWeight || this.txtKpiweight < 1) {
      alert(this.GetLocalResourceObject("InvalidWeight") + "," + 1);
      return;
    }

    if (
      this.selectedObjectiveKpiId != null ||
      this.selectedObjectiveKpiId != undefined
    ) {

      // get objective kpi bsc

      const bsc = this.ObjectivesList.filter(a => a.id == this.selectedObjectiveID)[0].BSC_Name;
      const bscId = this.bscList.filter(a => a.name == bsc)[0].id;
      this.OrganizationService.UpdateObjectiveKpi(
        this.selectedObjectiveKpiId,
        this.txtKpiName,
        "",
        this.txtKpiDesc,
        this.selectedObjectiveID,
        this.txtKpiweight,
        this.txtKpiTarget,
        // this.ddlKpiBSC,
        bscId,
        this.ddlKpiMeasurement,
        this.userContextService.CompanyID,
        this.userContextService.BranchID,
        this.userContextService.Username,
        this.userContextService.language,
        1,
        1,
        1,
        this.ddlBetterUpDown
      ).subscribe((res) => {
        if (res.IsError) {
          alert(res.ErrorMessage + "," + 1);
          return;
        }
        this.LoadObjectiveKPIs();
        this.resetObjectiveKpiEntry();
      });
    } else {
      const bsc = this.ObjectivesList.filter(a => a.id == this.selectedObjectiveID)[0].BSC_Name;
      const bscId = this.bscList.filter(a => a.name == bsc)[0].id;
      this.OrganizationService.SaveObjectiveKpi(
        this.txtKpiName,
        "",
        this.txtKpiDesc,
        this.selectedObjectiveID,
        this.txtKpiweight,
        this.txtKpiTarget,
        bscId,
        this.ddlKpiMeasurement,
        this.userContextService.CompanyID,
        this.userContextService.BranchID,
        this.userContextService.Username,
        this.userContextService.language,
        1,
        1,
        1,
        1,
        this.ddlBetterUpDown
      ).subscribe((res) => {
        if (res.IsError) {
          alert(res.ErrorMessage + "," + 1);
          return;
        }

        this.LoadObjectiveKPIs();
        this.resetObjectiveKpiEntry();
      });
    }
  }

  ObjectiveKpiAdd() {
    this.showKpiEntry = true;
    this.getObjectiveKPIRemainingWeight(this.selectedObjectiveID);
  }

  ShowDocumentEntry = false;
  txtDocumentName;
  selectDocID;
  DocEditMode = false;

  AddDocument() {
    this.txtDocumentName = "";
    this.ShowDocumentEntry = true;
    this.DocEditMode = false;
  }

  SaveProjectDocuments() {
    if (!this.DocEditMode) {
      this.projectService
        .SaveProjectDocuments(
          this.selectedObjectiveID,
          this.txtDocumentName,
          this.userContextService.Username,
          "1"
        )
        .subscribe((res) => {
          if (res.IsError) {
            alert(res.ErrorMessage + "," + 1);
            return;
          }
          alert("0");
          this.LoadProjectDocuments(this.selectedObjectiveID);
        });
    } else {
      this.projectService
        .UpdateProjectDocuments(
          this.selectDocID,
          this.selectedObjectiveID,
          this.txtDocumentName,
          this.userContextService.Username,
          "1"
        )
        .subscribe((res) => {
          if (res.IsError) {
            alert(res.ErrorMessage + "," + 1);
            return;
          }
          alert("0");
          this.LoadProjectDocuments(this.selectedObjectiveID);
        });
    }
    this.ShowDocumentEntry = false;
  }

  LoadProjectDocuments(id) {
    this.projectService
      .GetProjectDocuments(id, this.userContextService.language, "1")
      .subscribe((res) => {
        // console.log('print res: ', res)
        if (res.IsError) {
          alert(res.ErrorMessage + "," + 1);
          return;
        }

        const cols = [
          {HeaderText: "Name", DataField: "doc_name", Width: "60%"},
          {HeaderText: "File Name", DataField: "FileName", Width: "40%"},
        ];
        const actions = [
          {
            title: "Edit",
            DataValue: "ID",
            Icon_Awesome: "fa fa-edit",
            Action: "edit",
          },
          {
            title: "Delete",
            DataValue: "ID",
            Icon_Awesome: "fa fa-trash",
            Action: "delete",
          },
        ];
        const viewActions = [];
        this.gvDocuments.bind(cols, res.Data, "gvDocuments", this.hasEditPermission() ? actions : viewActions);
      });
  }

  gvDocumentsHandler(event) {
    if (event[1] == "edit") {
      this.projectService.GetProjectDocumentByID(event[0]).subscribe((res) => {
        if (res.IsError) {
          alert(res.ErrorMessage + "," + 1);
          return;
        }
        this.ShowDocumentEntry = true;
        this.txtDocumentName = res.Data.doc_name;
        this.selectDocID = res.Data.ID;
        this.DocEditMode = true;
      });
    } else if (event[1] == "delete") {
      if (confirm(this.GetLocalResourceObject("confirm"))) {
        this.projectService
          .DeleteProjectDocuments(event[0])
          .subscribe((res) => {
            if (res.IsError) {
              alert(res.ErrorMessage + "," + 1);
              return;
            }
            this.LoadProjectDocuments(this.selectedObjectiveID);
          });
      }
    }
  }

  viewAddEvidancesForKPI() {
    this.showAddEvidance = true;
    this.showUpdateEvidance = false;
  }

  AddEvidancesForKPI() {
    this.OrganizationService.addEvidanceForKPI(
      this.selectedEvidanceId,
      this.txtEvidanceKPI,
      this.userContextService.CompanyID
    ).subscribe(() => {
      this.txtEvidanceKPI = "";
      this.showAddEvidance = false;
      this.getEvidanceByObjectiveKPI(this.selectedEvidanceId);
    });
  }

  // get evidance for KPI by id and bind it in table (data grid)
  getEvidanceByObjectiveKPI(id) {
    this.OrganizationService.GetEvidanceByObjectiveKPI_id(id).subscribe(
      (evidance) => {
        this.evidancesList = evidance.Data;
        const cols = [
          {HeaderText: "Name", DataField: "doc_name", Width: "60%"},
          {HeaderText: "File Name", DataField: "FileName", Width: "40%"},
        ];
        const actions = [
          {
            title: "Edit",
            DataValue: "ID",
            Icon_Awesome: "fa fa-edit",
            Action: "edit",
          },
          {
            title: "Delete",
            DataValue: "ID",
            Icon_Awesome: "fa fa-trash",
            Action: "delete",
          },
        ];
        const viewActions = [];
        this.evidancesKPI.bind(cols, evidance.Data, "evidancesKPI", this.hasEditPermission() ? actions : viewActions);
      }
    );
  }

  betterUpDownChange() {
    console.log(this.ddlBetterUpDown);


  }
}
