import {Component, OnInit, ViewChild} from '@angular/core';
import {PositionsService} from '../Services/positions.service';
import {CodesService} from '../Services/codes.service';
import {UserContextService} from '../Services/user-context.service';
import {UsersService} from '../Services/Users/users.service';
import {EmployeeService} from "../Organization/employees/employee.service";

@Component({
  selector: 'app-positions',
  templateUrl: './positions.component.html',
  styleUrls: ['./positions.component.css']
  , providers: [UsersService]
})
export class PositionsComponent implements OnInit {

  @ViewChild('gvPositions', {read: false, static: false}) gvPositions;
  @ViewChild('gvCompetence', {read: false, static: false}) gvCompetence;
  @ViewChild('gvCompetenciesKpi', {read: false, static: false}) gvCompetenciesKpi;
  @ViewChild('gvJobDescription', {read: false, static: false}) gvJobDescription;


  /**************/
  showPositionEntry = false;
  showTab = false;
  showJobDescEntry = false;
  showCometenceEntry = false;
  showKPIList = false;
  txtSearchPositions = "";
  ddlSearchUnits;
  UnitsSearchList;


  //positionsList;
  txtPositionCode = "01";
  txtPositionName;
  cbPositionlManagerial;
  positionUpdateMode = false;
  selectedPositionID;
  /****************/
  positionCompetenciesUpdateMode = false;
  selectedPositionCompetenciesID;
  ddlCompetenceID;
  competenceList;
  /****************/
  ddlCompetenceLevelID;
  competenceLevelList;
  /***************/
  txtJobDescription;
  JobDescriptionUpdateMode = false;
  selectedJobDescriptionID;
  /************** */
  PageResources = [];
  competenceNatureList;
  ddlCompetenceNatureID = -1;

  modificationPermission = false;
  compList: any;

  Loadunits() {
    this.empService.GetUnites(this.userContextService.CompanyID, this.userContextService.language).subscribe(res => {
      if (res.IsError) {
        alert(res.ErrorMessage);
        return;
      }
      this.UnitsSearchList = res.Data;
    });
  }

  GetLocalResourceObject(resourceKey) {
    for (let i = 0; i < this.PageResources.length; i++) {
      if (this.PageResources[i].resource_key === resourceKey) {
        return this.PageResources[i].resource_value;
      }
    }
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

  constructor(
    private positionsService: PositionsService,
    private codesService: CodesService,
    private empService: EmployeeService,
    private userContextService: UserContextService,
    private userService: UsersService
  ) {
    this.modificationPermission = this.userContextService.RoleId != 5;
    this.userService.GetLocalResources(window.location.hash, this.userContextService.CompanyID, this.userContextService.language).subscribe(res => {
      if (res.IsError) {
        alert(res.ErrorMessage);
        return;
      }
      this.PageResources = res.Data;
    });

    this.Loadunits();
    this.LoadPositions(userContextService.CompanyID, "");
    this.getCompetenceLevelList();
    this.getNatureCodesList();
  }

  ngOnInit() {
  }

  loadPositions() {
    this.LoadPositions(this.userContextService.CompanyID, this.txtSearchPositions);
  }

  getCompetenceList() {
    this.ddlCompetenceLevelID = null;
    this.positionsService.LoadCompetenceis(
      this.userContextService.CompanyID, "", this.userContextService.language, this.ddlCompetenceNatureID
    ).subscribe(res => {
      if (res.IsError) {
        alert(res.ErrorMessage);
        return null;
      }
      console.log("res com data", res.Data);
      this.competenceList = res.Data;
    });
  }

  /************************ */
  getCompetenceLevelList() {
    this.codesService.LoadCodes(
      this.userContextService.CompanyID,
      7,
      this.userContextService.language
    ).subscribe(res => {
      if (res.IsError) {
        alert(res.ErrorMessage);
        return null;
      }
      this.competenceLevelList = res.Data;
    })
  }

  /*****************************/
  gvPositionsEvent(event) {
    if (event[1] == 'edit') {
      this.LoadPositionByID(event[0], this.userContextService.language);
      this.ddlCompetenceLevelID = null;
      this.showPositionEntry = true;
      this.showTab = false;
      this.showJobDescEntry = false;
      this.showCometenceEntry = false;
      this.showKPIList = false;
    } else if (event[1] == 'delete') {
      if (confirm(this.GetLocalResourceObject('Areyousure'))) {
        this.DeletePosition(event[0]);

        this.showPositionEntry = false;
        this.showTab = false;
        this.showJobDescEntry = false;
        this.showCometenceEntry = false;
        this.showKPIList = false;
      }
    } else if (event[1] == 'Details') {

      this.selectedPositionID = event[0];
      this.LoadCompetence(event[0]);
      this.LoadJobDescription(event[0]);
      this.showPositionEntry = false;
      this.showTab = true;
      this.showJobDescEntry = false;
      this.showCometenceEntry = false;
      this.showKPIList = false;

    }
  }

  LoadPositions(CompanyID, Name) {
    this.positionsService.LoadPositions(CompanyID, Name, this.userContextService.language, this.ddlSearchUnits).subscribe(res => {
      if (res.IsError) {
        alert(res.ErrorMessage);
        return;
      }
      res.Data.forEach(a =>

        a.managerPosition = a.IS_MANAGMENT ? 'Yes' : 'No'
      )
      let cols = [
        //{ 'HeaderText': this.GetLocalResourceObject('Code'), 'DataField': 'CODE' },
        {'HeaderText': this.GetLocalResourceObject('Name'), 'DataField': 'NAME'},
        {'HeaderText': 'Manager Position', 'DataField': 'managerPosition'}
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

      this.gvPositions.bind(cols, res.Data, 'gvPositions', this.modificationPermission ? actions : []);
    });
  }

  SavePosition() {

    var isManagerial = 1;
    console.log('cbPositionlManagerial', this.cbPositionlManagerial);
    if (this.cbPositionlManagerial === false) {
      isManagerial = 0;
    }
    if (!this.positionUpdateMode) {
      this.positionsService.SavePosition(this.txtPositionCode,
        this.userContextService.CompanyID,
        this.userContextService.Username,
        isManagerial,
        this.txtPositionName,
        this.userContextService.language
      ).subscribe(res => {
        if (res.IsError) {
          alert(res.ErrorMessage);
          return;
        }
        this.PositionAddMode();
        this.LoadPositions(this.userContextService.CompanyID, "");
      });
    } else {
      this.positionsService.UpdatePosition(
        this.selectedPositionID,
        this.txtPositionCode,
        this.userContextService.Username,
        isManagerial,
        this.txtPositionName,
        this.userContextService.language
      ).subscribe(res => {
        if (res.IsError) {
          alert(res.ErrorMessage);
          return;
        }
        this.PositionAddMode();
        this.LoadPositions(this.userContextService.CompanyID, "");
        this.LoadCompetence(this.selectedPositionID);
        //this.gvCompetenciesKpi = null;
      });
    }

    this.showPositionEntry = true;
    this.showTab = false;
    this.showJobDescEntry = false;
    this.showCometenceEntry = false;
    this.showKPIList = false;
  }

  DeletePosition(id) {
    this.positionsService.DeletePosition(id).subscribe(res => {
      if (res.IsError) {
        alert(res.ErrorMessage);
        return;
      }

      alert(this.GetLocalResourceObject('PositionDeleted'));
      this.PositionAddMode();
      this.LoadPositions(this.userContextService.CompanyID, "");
    });
  }

  LoadPositionByID(id, lang) {
    this.positionsService.LoadPositionByID(id, lang).subscribe(res => {
      if (res.IsError) {
        alert(res.ErrorMessage);
        return;
      }

      this.txtPositionCode = res.Data.CODE;
      this.txtPositionName = res.Data.NAME;
      this.cbPositionlManagerial = res.Data.IS_MANAGMENT;
      this.selectedPositionID = res.Data.ID;
      this.positionUpdateMode = true;
    });
    this.positionUpdateMode = true;
    this.LoadCompetence(id);
    //this.gvCompetenciesKpi = null;

    this.showPositionEntry = true;
    this.showTab = true;
    this.showJobDescEntry = false;
    this.showCometenceEntry = false;
    this.showKPIList = false;
  }

  PositionAddMode() {
    this.txtPositionCode = "01";
    this.txtPositionName = "";
    this.selectedPositionID = 0;
    this.cbPositionlManagerial = 0;
    this.positionUpdateMode = false;

    this.gvJobDescription = null;
    this.gvCompetence = null;

    this.showPositionEntry = true;
    this.showTab = false;
    this.showJobDescEntry = false;
    this.showCometenceEntry = false;
    this.showKPIList = false;


    //this.gvCompetenciesKpi = null;
  }

  /*****************************/
  gvJobDescriptionEvent(event) {
    if (event[1] == 'edit') {

      this.LoadJobDescriptionByID(event[0], this.userContextService.language);

      this.showPositionEntry = false;
      this.showTab = true;
      this.showJobDescEntry = true;
      this.showCometenceEntry = false;
      this.showKPIList = false;
    } else if (event[1] == 'delete') {
      if (confirm(this.GetLocalResourceObject('Areyousure'))) {
        this.DeleteJobDescription(event[0]);

        this.showPositionEntry = false;
        this.showTab = true;
        this.showJobDescEntry = false;
        this.showCometenceEntry = false;
        this.showKPIList = false;
      }
    }
  }

  LoadJobDescription(PositionID) {
    if (PositionID > 0) {
      this.positionsService.LoadJobDescription(PositionID, this.userContextService.language).subscribe(res => {
        if (res.IsError) {
          alert(res.ErrorMessage);
          return;
        }

        let cols = [
          {'HeaderText': this.GetLocalResourceObject('Name'), 'DataField': 'Name'}
        ];
        let actions = [
          {
            "title": this.GetLocalResourceObject('lblEdit'),
            "DataValue": "id",
            "Icon_Awesome": "fa fa-edit",
            "Action": "edit"
          },
          {
            "title": this.GetLocalResourceObject('lblDelete'),
            "DataValue": "id",
            "Icon_Awesome": "fa fa-trash",
            "Action": "delete"
          }
        ];

        this.gvJobDescription.bind(cols, res.Data, 'gvJobDescription', actions);
      });
    } else {
      //this.gvCompetence.bind();
    }
  }

  SaveJobDescription() {
    if (!this.JobDescriptionUpdateMode) {
      if (this.selectedPositionID) {
        this.positionsService.SaveJobDescription(
          this.selectedPositionID,
          this.userContextService.Username,
          this.txtJobDescription,
          this.userContextService.language
        ).subscribe(res => {
          if (res.IsError) {
            alert(res.ErrorMessage);
            return;
          }
          this.JobDescriptionAddMode();
          this.LoadJobDescription(this.selectedPositionID);
        });
      } else {
        alert(this.GetLocalResourceObject('Pleasechooseaposition!'));
      }

    } else {
      this.positionsService.UpdateJobDescription(
        this.selectedJobDescriptionID,
        this.userContextService.Username,
        this.txtJobDescription,
        this.userContextService.language
      ).subscribe(res => {
        if (res.IsError) {
          alert(res.ErrorMessage);
          return;
        }
        this.JobDescriptionAddMode();
        this.LoadJobDescription(this.selectedPositionID);
      });
    }

    this.showPositionEntry = false;
    this.showTab = true;
    this.showJobDescEntry = true;
    this.showCometenceEntry = false;
    this.showKPIList = false;
  }

  DeleteJobDescription(id) {
    this.positionsService.DeleteJobDescription(id).subscribe(res => {
      if (res.IsError) {
        alert(res.ErrorMessage);
        return;
      }
      alert(this.GetLocalResourceObject('JobDescriptionDeleted'));
      this.JobDescriptionAddMode();
      this.LoadJobDescription(this.selectedPositionID);
    });
  }

  LoadJobDescriptionByID(id, lang) {
    this.positionsService.LoadJobDescriptionByID(id, lang).subscribe(res => {
      if (res.IsError) {
        alert(res.ErrorMessage);
        return;
      }
      this.txtJobDescription = res.Data.Name;
      this.selectedJobDescriptionID = res.Data.id;
      this.JobDescriptionUpdateMode = true;
    });
    this.JobDescriptionUpdateMode = true;

    this.showPositionEntry = true;
    this.showTab = true;
    this.showJobDescEntry = true;
    this.showCometenceEntry = false;
    this.showKPIList = false;
  }

  JobDescriptionAddMode() {
    this.JobDescriptionUpdateMode = false;
    this.txtJobDescription = "";

    this.showPositionEntry = false;
    this.showTab = true;
    this.showJobDescEntry = true;
    this.showCometenceEntry = false;
    this.showKPIList = false;

  }

  /*****************************/
  gvCompetenceEvent(event) {
    if (event[1] == 'edit') {
      this.ddlCompetenceLevelID = null;
      let id = this.compList.filter(a => {
        return a.ID == event[0];
      })[0].PositionCompetencyId;
      this.LoadCompetenceByID(id);

      this.showPositionEntry = false;
      this.showTab = true;
      this.showJobDescEntry = false;
      this.showCometenceEntry = true;
      this.showKPIList = false;
    } else if (event[1] == 'delete') {
      if (confirm(this.GetLocalResourceObject('Areyousure'))) {
        this.DeleteCompetence(event[0], this.selectedPositionID);
        this.showPositionEntry = false;
        this.showTab = true;
        this.showJobDescEntry = false;
        this.showCometenceEntry = false;
        this.showKPIList = false;
      }
    } else if (event[1] == 'Details') {
      this.selectedPositionCompetenciesID = event[0];
      this.LoadCompetenceKPI(event[0]);

      this.showPositionEntry = false;
      this.showTab = true;
      this.showJobDescEntry = false;
      this.showCometenceEntry = false;
      this.showKPIList = true;
    }
  }

  LoadCompetence(PositionID) {

    if (PositionID > 0) {
      this.positionsService.LoadPositionCompetencies(PositionID, this.userContextService.language, null).subscribe(res => {
        if (res.IsError) {
          alert(res.ErrorMessage);
          return;
        }

        let cols = [
          // { 'HeaderText': this.GetLocalResourceObject('Code'), 'DataField': 'CompetenceCode' },
          {'HeaderText': this.GetLocalResourceObject('Name'), 'DataField': 'CompetenceName'},
          {'HeaderText': this.GetLocalResourceObject('Nature'), 'DataField': 'CompetenceType'}
          //,{ 'HeaderText': this.GetLocalResourceObject('Mandetory'), 'DataField': 'CompetenceMandetory' }
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
            "DataValue": "CompetenceID",
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
        console.log("res data compete", res.Data);
        this.compList = res.Data;
        this.gvCompetence.bind(cols, res.Data, 'gvCompetence', actions);
      });
    } else {
      //this.gvCompetence.bind();
    }
    this.CompetenceAddMode();
  }

  SaveCompetence() {
    if (!this.positionCompetenciesUpdateMode) {
      if (this.selectedPositionID) {
        this.positionsService.SavePositionCompetencies(
          this.selectedPositionID,
          this.ddlCompetenceID,
          this.ddlCompetenceLevelID
        ).subscribe(res => {
          if (res.IsError) {
            alert(res.ErrorMessage);
            return;
          }
          this.CompetenceAddMode();
          this.LoadCompetence(this.selectedPositionID);
        });
      } else {
        alert(this.GetLocalResourceObject('Pleasechooseaposition'));
      }

    } else {
      this.positionsService.UpdatePositionCompetencies(
        this.selectedPositionCompetenciesID,
        this.selectedPositionID,
        this.ddlCompetenceID,
        this.ddlCompetenceLevelID
      ).subscribe(res => {
        if (res.IsError) {
          alert(res.ErrorMessage);
          return;
        }
        this.CompetenceAddMode();
        this.LoadCompetence(this.selectedPositionID);
      });
    }

    this.showPositionEntry = false;
    this.showTab = true;
    this.showJobDescEntry = false;
    this.showCometenceEntry = true;
    this.showKPIList = false;
  }

  DeleteCompetence(id, selectedPositionID) {
    this.positionsService.DeletePositionCompetencies(id, selectedPositionID).subscribe(res => {
      if (res.IsError) {
        alert(res.ErrorMessage);
        return;
      }
      alert(this.GetLocalResourceObject('CompetenceDeleted'));
      this.CompetenceAddMode();
      this.LoadCompetence(this.selectedPositionID);
    });
  }

  LoadCompetenceByID(id) {
    this.positionsService.LoadPositionCompetenciesByID(id).subscribe(res => {
      if (res.IsError) {
        alert(res.ErrorMessage);
        return;
      }
      this.ddlCompetenceID = res.Data.competence_id;
      this.ddlCompetenceLevelID = res.Data.competence_level;
      this.selectedPositionCompetenciesID = res.Data.ID;
      this.ddlCompetenceNatureID = -1;
      this.getCompetenceList();
      this.positionCompetenciesUpdateMode = true;

    });
    this.positionCompetenciesUpdateMode = true;

    this.showPositionEntry = true;
    this.showTab = true;
    this.showJobDescEntry = false;
    this.showCometenceEntry = true;
    this.showKPIList = true;
  }

  CompetenceAddMode() {

    this.ddlCompetenceID = null;
    this.positionCompetenciesUpdateMode = false;

    this.showPositionEntry = false;
    this.showTab = true;
    this.showJobDescEntry = false;
    this.showCometenceEntry = true;
    this.showKPIList = false;
    this.ddlCompetenceNatureID = null;

  }

  /*****************************/
  LoadCompetenceKPI(CompetenceID) {
    this.positionsService.LoadCompetenceKpi(CompetenceID, this.userContextService.language).subscribe(res => {
      if (res.IsError) {
        alert(res.ErrorMessage);
        return;
      }

      //this.positionsList = res.Data;

      let cols = [
        {'HeaderText': this.GetLocalResourceObject('Name'), 'DataField': 'NAME'}
      ];
      let actions = [];

      this.gvCompetenciesKpi.bind(cols, res.Data, 'gvCompetenciesKpi', actions);
    });
  }
}
