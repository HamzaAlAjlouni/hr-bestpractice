import {Component, OnInit, ViewChild, Input, Output, EventEmitter} from '@angular/core';
import {OrganizationService} from '../Organization/organization.service';
import {SettingsService} from '../settings/settings.service';
import {UserContextService} from '../Services/user-context.service';
import {UsersService} from '../Services/Users/users.service';
import {EmployeeService} from '../Organization/employees/employee.service';
import {CodesService} from '../Services/codes.service';
import {EmployeeAssesmentService} from '../Services/employee-assesment.service';
import {ProjectsService} from "../Services/Projects/projects.service";
import {PositionsService} from "../Services/positions.service";

@Component({
  selector: 'app-employee-development-plan',
  templateUrl: './employee-development-plan.component.html',
  styleUrls: ['./employee-development-plan.component.css']
})
export class EmployeeDevelopmentPlanComponent implements OnInit {

  @Input()
  AddOnFly = false;

  @Output()
  AddOnFlyObjectiveSave = new EventEmitter<string>();

  @ViewChild('gvEmployeeEducations', {read: false, static: false}) gvEmployeeEducations;
  @ViewChild('gvKPIs', {read: false, static: false}) gvKPIs;

  @ViewChild('ProjectsCom', {read: false, static: false}) ProjectsCom;

  ddlUnitSearch = -1;
  ddlQuarter = -1;
  modificationPermission = false;
  UnitsList;
  //
  showTabs = false;
  selectedObjectiveID;
  txtKpiName;
  txtKpiDesc;
  txtKpiweight;
  txtKpiTarget;
  ddlKpiBSC;
  ddlKpiMeasurement;
  remainingWeight;
  remainingObjectivesKpisWeight;

  showKpiEntry = false;
  EmployeeList;
  competencyList;
  txtObjectiveCode;
  txtObjectiveOrder;


  txtEducationField;
  txtNotes;
  ddlEmployeeObjective;
  ddlSearchYear;
  ddlCompetency = -1;
  SearchEmployeeID;
  txtdescription;
  ObjectivesList;
  years;
  updateMode = false;
  addMode = false;
  selectID;
  ViewTable = false;
  ddlObjBSC;
  PageResources = [];
  lblSelectedStratigicObjective;
  ddlEmployeeCompetency;
  ddlEducationType = -1;
  EducationTypeList;
  ddlEducationMethod = -1;
  EducationMethodList;
  ddlEducationPriority = -1;
  EducationPriorityList;
  ddlExecutionPeriod = -1;
  ExecutionPeriodList;
  ddlExecutionStatus = -1;
  ExecutionStatusList;


  constructor(private OrganizationService: OrganizationService,
              private settingsService: SettingsService,
              private userContextService: UserContextService,
              private userService: UsersService,
              private positionService: PositionsService,
              private projectsService: ProjectsService,
              private employeeService: EmployeeService,
              private codesService: CodesService,
              private employeeAssesmentService: EmployeeAssesmentService) {
    this.performSettings();

    this.userService.GetLocalResources(window.location.hash, this.userContextService.CompanyID, this.userContextService.language).subscribe(res => {
      if (res.IsError) {
        alert(res.ErrorMessage + ',' + 1)
        return;
      }
      this.PageResources = res.Data;
    });

    this.fillCompetences();
    this.fillEmployeeList();
    this.getEducationTypeslList();
    this.getEducationMethodslList();
    this.getEducationPriorityList();
    this.getExecutionPeriodList();
    this.getExecutionStatusList();
    this.fillUnitLst();

    //this.SearchAllStratigicObjectives();
  }

  ngOnInit() {

  }

  fillCompetences() {
    this.positionService
      .LoadCompetenceis(
        this.userContextService.CompanyID,
        "",
        this.userContextService.language,
        -1
      )
      .subscribe((res) => {
        this.competencyList = res.Data;
      });


  }

  fillUnitLst() {
    this.projectsService
      .GetUnits(
        this.userContextService.CompanyID,
        this.userContextService.language
      )
      .subscribe((res) => {


        this.UnitsList = this.userContextService.Role === "Admin" || this.userContextService.UnitId === 0 ? res.Data : res.Data.filter(a => {
          return a.ID == this.userContextService.UnitId;
        });

        this.ddlUnitSearch = -1;
      });
  }

  fillEmployeeList() {
    var dd = this.employeeService.LoadEmplyeeByCompanyID(this.userContextService.CompanyID, this.userContextService.language
      , this.userContextService.Role === "Admin" || this.userContextService.UnitId === 0 ? null : this.userContextService.UnitId
    ).subscribe(res => {
      if (res.IsError) {
        alert(res.ErrorMessage + ',' + 1);
        return;
      }
      this.EmployeeList = res.Data;
    });
  }

  EmployeeCompetencyList;
  EmployeeObjectiveList;

  GetEmployeeCompetenciesByEmployee() {
    var dd = this.employeeAssesmentService.GetEmployeeCompetenciesByEmployee(this.SearchEmployeeID, this.userContextService.CompanyID, this.ddlSearchYear, this.userContextService.language).subscribe(res => {
      if (res.IsError) {
        alert(res.ErrorMessage + ',' + 1);
        return;
      }
      this.EmployeeCompetencyList = res.Data;
    });
  }


  GetEmployeeObjectiveByEmployee() {
    var dd = this.employeeAssesmentService.GetEmployeeObjectiveByEmployee(this.SearchEmployeeID, this.userContextService.CompanyID, this.ddlSearchYear, this.userContextService.language).subscribe(res => {
      if (res.IsError) {
        alert(res.ErrorMessage + ',' + 1);
        return;
      }
      this.EmployeeObjectiveList = res.Data;
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
    this.ddlSearchYear = year;
  }

  performSettings() {
    this.txtEducationField = '';
    this.txtNotes = '';
    this.ddlEmployeeObjective = -1;
    this.ddlEmployeeCompetency = -1;
    this.ddlEducationType = -1;
    this.ddlEducationMethod = -1;
    this.ddlEducationPriority = -1;
    this.ddlExecutionPeriod = -1;
    this.ddlExecutionStatus = -1;

    this.txtdescription = '';
    this.updateMode = false;
    this.selectID = null;
    this.LoadYears();
    this.ViewTable = false;
    this.addMode = false;

  }

  AddMode() {
    this.txtEducationField = '';
    this.txtNotes = '';
    this.ddlEmployeeObjective;
    this.ddlEmployeeCompetency = null;
    this.ddlEducationType = null;
    this.ddlEducationMethod = null;
    this.ddlEducationPriority = null;
    this.ddlExecutionPeriod = null;
    this.ddlExecutionStatus = null;
    this.txtdescription = '';
    this.ddlObjBSC = null;
    this.updateMode = false;
    this.selectID = null;
    this.addMode = true;
    this.showTabs = false;


  }

  getEducationTypeslList() {
    this.codesService.LoadCodes(
      this.userContextService.CompanyID,
      12,
      this.userContextService.language
    ).subscribe(res => {
      if (res.IsError) {
        alert(res.ErrorMessage + ',' + 1);
        return null;
      }
      this.EducationTypeList = res.Data;
    })
  }


  getEducationMethodslList() {
    this.codesService.LoadCodes(
      this.userContextService.CompanyID,
      13,
      this.userContextService.language
    ).subscribe(res => {
      if (res.IsError) {
        alert(res.ErrorMessage + ',' + 1);
        return null;
      }
      this.EducationMethodList = res.Data;
    })
  }


  getEducationPriorityList() {
    this.codesService.LoadCodes(
      this.userContextService.CompanyID,
      14,
      this.userContextService.language
    ).subscribe(res => {
      if (res.IsError) {
        alert(res.ErrorMessage + ',' + 1);
        return null;
      }
      this.EducationPriorityList = res.Data;
    })
  }


  getExecutionPeriodList() {
    this.codesService.LoadCodes(
      this.userContextService.CompanyID,
      6,
      this.userContextService.language
    ).subscribe(res => {
      if (res.IsError) {
        alert(res.ErrorMessage + ',' + 1);
        return null;
      }
      this.ExecutionPeriodList = res.Data;
    })
  }


  getExecutionStatusList() {
    this.codesService.LoadCodes(
      this.userContextService.CompanyID,
      15,
      this.userContextService.language
    ).subscribe(res => {
      if (res.IsError) {
        alert(res.ErrorMessage + ',' + 1);
        return null;
      }
      this.ExecutionStatusList = res.Data;
    })
  }

  SaveEmployeeEducation() {


    if (!this.updateMode) {
      this.employeeAssesmentService.SaveEmployeeEducation(this.userContextService.CompanyID, this.ddlSearchYear,
        this.SearchEmployeeID, this.ddlEmployeeCompetency, this.ddlEmployeeObjective, this.ddlExecutionPeriod,
        this.txtEducationField, this.ddlEducationPriority, this.ddlExecutionStatus, this.ddlEducationType,
        this.ddlEducationMethod, this.userContextService.Username, this.txtNotes, this.userContextService.language
      ).subscribe(res => {
        if (res.IsError) {
          alert(res.ErrorMessage + ',' + 1)
          return;
        }
        this.addMode = false;
        this.updateMode = false;
        alert('0');
        this.SearchEmployeeEducations();
        if (this.AddOnFly) {
          this.AddOnFlyObjectiveSave.emit(this.GetLocalResourceObject('SuccessMsg'));
        }

      });
    } else {
      this.employeeAssesmentService.UpdateEmployeeEducation(this.selectID, this.userContextService.CompanyID, this.ddlSearchYear,
        this.SearchEmployeeID, this.ddlEmployeeCompetency, this.ddlEmployeeObjective, this.ddlExecutionPeriod,
        this.txtEducationField, this.ddlEducationPriority, this.ddlExecutionStatus, this.ddlEducationType,
        this.ddlEducationMethod, this.userContextService.Username, this.txtNotes, this.userContextService.language).subscribe(res => {
        if (res.IsError) {
          alert(res.ErrorMessage + ',' + 1)
          return;
        }
        this.SearchEmployeeEducations();
        this.addMode = false;
        this.updateMode = false;
        alert('0');
        if (this.AddOnFly) {
          this.AddOnFlyObjectiveSave.emit(this.GetLocalResourceObject('SuccessMsg'));
        }
      });
    }
  }

  LoadYears() {
    this.settingsService.LoadAllYears().subscribe(result => {
      if (result.IsError) {
        alert(result.ErrorMessage + ',' + 1)
        return;
      }
      this.years = result.Data;
      if (this.years != null && this.years.length > 0) {
        this.years.forEach(element => {

          this.ddlSearchYear = element.id;
        });
        this.fillEmployeeList();
      }

    }, error => console.error(error));
  }


  gvEmployeeEducationsEvent(event) {
    if (event[1] == 'edit') {
      this.addMode = false;
      this.updateMode = false;
      this.LoadEmployeeEducationByID(event[0], this.userContextService.language);
    } else if (event[1] == 'delete') {
      if (confirm(this.GetLocalResourceObject('AreYouSure'))) {
        this.DeleteEducationByID(event[0]);
      }
    }

  }


  ChangeEmployeeSelection() {
    if (this.SearchEmployeeID != null && this.SearchEmployeeID > 0) {
      this.EmployeeCompetencyList = null;
      this.EmployeeObjectiveList = null;
      this.GetEmployeeCompetenciesByEmployee();
      this.GetEmployeeObjectiveByEmployee();
    } else {
      this.EmployeeCompetencyList = null;
      this.EmployeeObjectiveList = null;
    }
  }

  SearchEmployeeEducations() {
    this.employeeAssesmentService.GetEmployeeDevelopmentPlan(this.userContextService.CompanyID,
      this.ddlSearchYear, this.userContextService.language, this.ddlQuarter, this.userContextService.Role === "Admin" || this.userContextService.UnitId === 0 ? this.ddlUnitSearch : this.userContextService.UnitId).subscribe(res => {
      if (res.IsError) {
        alert(res.ErrorMessage + ',' + 1);
        return;
      }

      //filter data
      if (this.ddlEducationMethod > 0) {
        res.Data = res.Data.filter(a => a.method == this.ddlEducationMethod);
      }

      if (this.ddlEducationType > 0) {
        res.Data = res.Data.filter(a => a.type == this.ddlEducationType);
      }
      if (this.ddlEducationPriority > 0) {
        res.Data = res.Data.filter(a => a.priority == this.ddlEducationPriority);
      }

      if (this.ddlExecutionStatus > 0) {
        res.Data = res.Data.filter(a => a.status == this.ddlExecutionStatus);
      }

      if (this.ddlCompetency > 0) {
        res.Data = res.Data.filter(a => a.competency_id == this.ddlCompetency);
      }


      this.ObjectivesList = res.Data;
      this.addMode = false;
      this.updateMode = false;
      let cols = [
        {'HeaderText': "Employee", 'DataField': 'empName'},
        {'HeaderText': this.GetLocalResourceObject('lblEducationField') + " ", 'DataField': 'field'},
        {'HeaderText': this.GetLocalResourceObject('EducationType'), 'DataField': 'eduType_desc'},
        {'HeaderText': this.GetLocalResourceObject('EducationMethod'), 'DataField': 'eduMethod_desc'},
        {'HeaderText': this.GetLocalResourceObject('EducationPriority'), 'DataField': 'priority_desc'},
        {'HeaderText': this.GetLocalResourceObject('ExecutionStatus'), 'DataField': 'status_desc'},
        {'HeaderText': 'Quarter', 'DataField': 'execution_period'}
      ];
      // let actions = [
      //   // {
      //   //   "title": this.GetLocalResourceObject('lblEdit'),
      //   //   "DataValue": "id",
      //   //   "Icon_Awesome": "fa fa-edit",
      //   //   "Action": "edit"
      //   // },
      //   // {
      //   //   "title": this.GetLocalResourceObject('lblDelete'),
      //   //   "DataValue": "id",
      //   //   "Icon_Awesome": "fa fa-trash",
      //   //   "Action": "delete"
      //   // }
      // ];

      if (this.ObjectivesList.length > 0)
        this.ViewTable = true;

      this.gvEmployeeEducations.bind(cols, this.ObjectivesList, 'gvEmployeeEducations');
      this.showTabs = false;


    });
  }


  LoadEmployeeEducationByID(id, lang) {
    this.employeeAssesmentService.LoadEmployeeEducationByID(id, lang).subscribe(res => {
      if (res.IsError) {
        alert(res.ErrorMessage + ',' + 1)
        return;
      }


      this.selectID = res.Data.id;


      this.ddlEmployeeCompetency = res.Data.emp_competency_id;
      this.ddlEmployeeObjective = res.Data.emp_obj_id;
      this.ddlExecutionPeriod = res.Data.execution_period,
        this.txtEducationField = res.Data.field;
      this.ddlEducationPriority = res.Data.priority;
      this.ddlExecutionStatus = res.Data.status;
      this.ddlEducationType = res.Data.type,
        this.ddlEducationMethod = res.Data.method;

      this.txtNotes = res.Data.notes;


      this.updateMode = true;
      this.showTabs = false;
    });
  }


  DeleteEducationByID(id) {
    this.employeeAssesmentService.DeleteEmployeeEducation(id).subscribe(res => {
      if (res.IsError) {
        alert(res.ErrorMessage + ',' + 1)
        return;
      }
      this.addMode = false;
      this.updateMode = false;
      alert('0');
      this.performSettings();
      this.SearchEmployeeEducations();
    });
  }


  AddOnFlyProjectSave(event) {

  }


}
