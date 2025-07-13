import {Component, OnInit, ViewChild} from '@angular/core';
import {PositionsService} from '../Services/positions.service';
import {CodesService} from '../Services/codes.service';
import {UserContextService} from '../Services/user-context.service';
import {UsersService} from '../Services/Users/users.service';

import {AuthExcludedActionsService} from '../Services/auth-excluded-actions.service';

@Component({
  selector: 'app-competence',
  templateUrl: './competence.component.html',
  styleUrls: ['./competence.component.css']
  , providers: [UsersService, AuthExcludedActionsService]
})
export class CompetenceComponent implements OnInit {

  @ViewChild('gvCompetence', {read: false, static: false}) gvCompetence;
  @ViewChild('gvCompetenciesKpi', {read: false, static: false}) gvCompetenciesKpi;

  showCompetencEntry = false;
  showKPI = false;
  showKPIEntry = false;
  showNegativeIndicator = false;
  /****************/
  txtSearchCompetenceName = "";

  CompetenceUpdateMode = false;
  selectedCompetenceID;
  //code, companyID, createdBy, natureID, name, name2, positionID, isMandetory
  txtCompetenceCode = "01";
  ddlCompetenceNatureID = 1;
  txtCompetenceName;
  cbCompetenceMandetory;
  competenceNatureList;
  KPILevelList;
  /****************/
  CompetenceKPIUpdateMode = false;
  selectedCompeteceKPIID;
  txtCompeteceKPIName;
  ddlCompeteceKPIType = 1;
  cbNegativeIndicator = false;
  PageResources = [];
  ExcludedActions = [];
  modificationPermission = false;

  txtNotes;

  constructor(
    private positionsService: PositionsService,
    private codesService: CodesService,
    private userContextService: UserContextService
    , private userService: UsersService
    , private authExcludedActionsService: AuthExcludedActionsService
  ) {
    //this.LoadCompetence();
    console.log("this.userContextService.RoleId", this.userContextService);
    this.modificationPermission = this.userContextService.RoleId != 5;
    this.getNatureCodesList();
    this.getKpiLevelCodesList();

    this.authExcludedActionsService.LoadPageExcludedActions(
      this.userContextService.CompanyID,
      this.userContextService.Username,
      window.location.hash).subscribe(res1 => {
      if (res1.IsError) {
        alert(res1.ErrorMessage);
        return;
      }
      console.log("is read only", res1.Data);
      this.ExcludedActions = res1.Data;
    });

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

  IsExcludedAction(ActionName) {
    for (let i = 0; i < this.ExcludedActions.length; i++) {
      if (this.ExcludedActions[i].name === ActionName) {
        if (this.ExcludedActions[i].is_readonly == 0) {
          return false;
        } else {
          return true;
        }
      }
    }
    return false;
  }

  ngOnInit() {
  }

  getNatureCodesList() {
    this.codesService.LoadCodes(
      this.userContextService.CompanyID,
      1,
      this.userContextService.language
    ).subscribe(res => {
      if (res.IsError) {
        alert(res.ErrorMessage);
        return null;
      }
      this.competenceNatureList = res.Data;
    })
  }

  getKpiLevelCodesList() {
    this.codesService.LoadCodes(
      this.userContextService.CompanyID,
      7,
      this.userContextService.language
    ).subscribe(res => {
      if (res.IsError) {
        alert(res.ErrorMessage);
        return null;
      }
      this.KPILevelList = res.Data;
    })
  }

  /*****************************/
  gvCompetenceEvent(event) {
    if (event[1] == 'edit') {
      this.LoadCompetenceByID(event[0]);

      this.showCompetencEntry = true;
      this.showKPI = false;
      this.showKPIEntry = false;
    } else if (event[1] == 'delete') {
      if (confirm(this.GetLocalResourceObject('Areyousure'))) {
        this.DeleteCompetence(event[0]);

        this.showCompetencEntry = false;
        this.showKPI = false;
        this.showKPIEntry = false;
      }
    } else if (event[1] == 'Details') {
      this.selectedCompetenceID = event[0];
      this.LoadCompetenceKPI();
      this.showCompetencEntry = false;
      this.showKPI = true;
      this.showKPIEntry = false;
    }
  }

  LoadCompetence() {
    if (this.userContextService.CompanyID > 0) {
      this.positionsService.LoadCompetenceis(this.userContextService.CompanyID,
        this.txtSearchCompetenceName, this.userContextService.language, -1).subscribe(res => {
        if (res.IsError) {
          alert(res.ErrorMessage);
          return;
        }

        //this.positionsList = res.Data;

        let cols = [
          // { 'HeaderText': this.GetLocalResourceObject('Code'), 'DataField': 'CODE' },
          {'HeaderText': this.GetLocalResourceObject('Name'), 'DataField': 'NAME'},
          {'HeaderText': this.GetLocalResourceObject('Nature'), 'DataField': 'NatureName'},
          {'HeaderText': this.GetLocalResourceObject('IsMandetory'), 'DataField': 'IsMandetory'}
        ];
        let actions = [
          {
            "title": this.GetLocalResourceObject('lblEdit'),
            "DataValue": "ID",
            "Icon_Awesome": "fa fa-edit",
            "Action": "edit"
          },
          {
            "title": this.GetLocalResourceObject('lblDetails'),
            "DataValue": "ID",
            "Icon_Awesome": "fa fa-list-alt",
            "Action": "Details"
          },
          {
            "title": this.GetLocalResourceObject('lblDelete'),
            "DataValue": "ID",
            "Icon_Awesome": "fa fa-trash",
            "Action": "delete"
          }
        ];

        this.gvCompetence.bind(cols, res.Data, 'gvCompetence', this.modificationPermission ? actions : []);
      });
    } else {
      //this.gvCompetence.bind();
    }

    this.showCompetencEntry = false;
    this.showKPI = false;
    this.showKPIEntry = false;
  }

  SaveCompetence() {
    var isMandetory = 1;
    if (this.cbCompetenceMandetory == false) {
      isMandetory = 0;
    } else {
      isMandetory = 1;
    }
    if (!this.CompetenceUpdateMode) {
      this.positionsService.SaveCompetence(
        //code, companyID, createdBy, natureID, name, name2, positionID, isMandetory
        this.txtCompetenceCode,
        this.userContextService.CompanyID,
        this.userContextService.Username,
        this.ddlCompetenceNatureID,
        this.txtCompetenceName,
        "",
        isMandetory,
        this.userContextService.language,
        this.txtNotes
      ).subscribe(res => {
        if (res.IsError) {
          alert(res.ErrorMessage);
          return;
        }
        this.CompetenceAddMode();
        this.LoadCompetence();
      });
    } else {
      this.positionsService.UpdateCompetence(
        //id, code, modifiedBy, natureID, name, name2, isMandetory
        this.selectedCompetenceID,
        this.txtCompetenceCode,
        this.userContextService.Username,
        this.ddlCompetenceNatureID,
        this.txtCompetenceName,
        "",
        isMandetory,
        this.userContextService.language, this.txtNotes
      ).subscribe(res => {
        if (res.IsError) {
          alert(res.ErrorMessage);
          return;
        }
        this.CompetenceAddMode();
        this.LoadCompetence();
      });
    }

    this.showCompetencEntry = true;
    this.showKPI = false;
    this.showKPIEntry = false;
  }

  DeleteCompetence(id) {
    this.positionsService.DeleteCompetence(id).subscribe(res => {
      if (res.IsError) {
        alert(res.ErrorMessage);
        return;
      }
      alert(this.GetLocalResourceObject("CompetenceDeleted"));
      this.CompetenceAddMode();
      this.LoadCompetence();
    });
  }

  LoadCompetenceByID(id) {
    this.positionsService.LoadCompetenceByID(id, this.userContextService.language).subscribe(res => {
      if (res.IsError) {
        alert(res.ErrorMessage);
        return;
      }
      this.txtCompetenceCode = res.Data.CODE;
      this.txtCompetenceName = res.Data.NAME;
      this.cbCompetenceMandetory = res.Data.is_mandetory;
      this.ddlCompetenceNatureID = res.Data.c_nature_id;

      this.selectedCompetenceID = res.Data.ID;
      this.CompetenceUpdateMode = true;
      this.txtNotes = res.Data.notes;
    });
    this.CompetenceUpdateMode = true;


    /*************************** */
    this.showCompetencEntry = true;
    this.showKPI = true;
    this.showKPIEntry = false;
  }

  CompetenceAddMode() {

    this.txtCompetenceCode = "01";
    this.txtCompetenceName = "";
    this.cbCompetenceMandetory = 0;
    this.ddlCompetenceNatureID = 1;
    this.CompetenceUpdateMode = false;
    this.CompetenceKPIAddMode();
    /*************************** */
    this.showCompetencEntry = true;
    this.showKPI = false;
    this.showKPIEntry = false;
  }

  /*****************************/
  gvCompetenceKPIEvent(event) {
    if (event[1] == 'edit') {
      this.LoadCompetenceKPIByID(event[0]);

      this.showCompetencEntry = false;
      this.showKPI = true;
      this.showKPIEntry = true;
    } else if (event[1] == 'delete') {
      if (confirm(this.GetLocalResourceObject('Areyousure'))) {
        this.DeleteCompetenceKPI(event[0]);

        this.showCompetencEntry = false;
        this.showKPI = true;
        this.showKPIEntry = false;
      }
    }
  }

  LoadCompetenceKPI() {
    this.positionsService.LoadCompetenceKpiByLevel(this.selectedCompetenceID, this.ddlCompeteceKPIType, this.userContextService.language).subscribe(res => {
      if (res.IsError) {
        alert(res.ErrorMessage);
        return;
      }

      //this.positionsList = res.Data;

      let cols = [
        {'HeaderText': this.GetLocalResourceObject('Name'), 'DataField': 'NAME'}
      ];
      let actions = [
        {
          "title": this.GetLocalResourceObject('lblEdit'),
          "DataValue": "ID",
          "Icon_Awesome": "fa fa-edit",
          "Action": "edit"
        },
        {
          "title": this.GetLocalResourceObject('lblDelete'),
          "DataValue": "ID",
          "Icon_Awesome": "fa fa-trash",
          "Action": "delete"
        }
      ];

      this.gvCompetenciesKpi.bind(cols, res.Data, 'gvCompetenciesKpi', actions);
    });

    this.showCompetencEntry = false;
    this.showKPI = true;
    this.showKPIEntry = false;
  }

  SaveCompetenceKPI() {
    if (!this.CompetenceKPIUpdateMode) {
      if (!this.selectedCompetenceID) {
        alert(this.GetLocalResourceObject("Pleasechooseacompetence"));
      } else {
        this.positionsService.SaveCompetenceKpi(
          //CompetenceID, CreatedBy,KpiTypeID,Name, Name2
          this.selectedCompetenceID,
          this.userContextService.Username,
          this.cbNegativeIndicator ? 0 : this.ddlCompeteceKPIType,
          this.txtCompeteceKPIName,
          "", this.userContextService.language
        ).subscribe(res => {
          if (res.IsError) {
            alert(res.ErrorMessage);
            return;
          }
          this.CompetenceKPIAddMode();
          this.LoadCompetenceKPI();
        });
      }
    } else {
      this.positionsService.UpdateCompetenceKpi(
        //ID, ModifiedBy,KpiTypeID,Name, Name2
        this.selectedCompeteceKPIID,
        this.userContextService.Username,
        this.cbNegativeIndicator ? 0 : this.ddlCompeteceKPIType,
        this.txtCompeteceKPIName,
        ""
        , this.userContextService.language
      ).subscribe(res => {
        if (res.IsError) {
          alert(res.ErrorMessage);
          return;
        }
        this.CompetenceKPIAddMode();
        this.LoadCompetenceKPI();
      });
    }
    this.showCompetencEntry = false;
    this.showKPI = true;
    this.showKPIEntry = true;
  }

  DeleteCompetenceKPI(id) {
    this.positionsService.DeleteCompetenceKpi(id).subscribe(res => {
      if (res.IsError) {
        alert(res.ErrorMessage);
        return;
      }
      alert(this.GetLocalResourceObject("CompetenceKPIDeleted"));
      this.CompetenceKPIAddMode();
      this.LoadCompetenceKPI();
    });
  }

  LoadCompetenceKPIByID(id) {
    this.positionsService.LoadCompetenceKpiByID(id, this.userContextService.language).subscribe(res => {
      if (res.IsError) {
        alert(res.ErrorMessage);
        return;
      }
      this.txtCompeteceKPIName = res.Data.NAME;
      this.selectedCompeteceKPIID = res.Data.ID;
      this.CompetenceKPIUpdateMode = true;

      if (res.Data.c_kpi_type_id == 0) {
        this.showNegativeIndicator = true;
        this.cbNegativeIndicator = true;
      } else {
        this.ddlCompeteceKPIType = res.Data.c_kpi_type_id;
        this.showNegativeIndicator = false;
        this.cbNegativeIndicator = false;
      }
    });
    this.CompetenceKPIUpdateMode = true;

    /*************************** */
    this.showCompetencEntry = true;
    this.showKPI = true;
    this.showKPIEntry = true;

  }

  CompetenceKPIAddMode() {
    this.txtCompeteceKPIName = "";
    this.CompetenceKPIUpdateMode = false;
    this.showNegativeIndicator = true;
    this.cbNegativeIndicator = false;
    /*************************** */
    this.showCompetencEntry = false;
    this.showKPI = true;
    this.showKPIEntry = true;
    this.txtNotes = "";
  }
}
