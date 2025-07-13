import { Component, OnInit, ViewChild, Input, Output, EventEmitter } from '@angular/core';
import { EmployeeService } from './employee.service';
import { UserContextService } from '../../Services/user-context.service';
import { UrlHandlingStrategy } from '../../../../node_modules/@angular/router';

import { UsersService } from '../../Services/Users/users.service';

@Component({
  selector: 'app-employees',
  templateUrl: './employees.component.html',
  styleUrls: ['./employees.component.css'],
  providers: [EmployeeService, UsersService]
})
export class EmployeesComponent implements OnInit {


  @ViewChild("EmpImg", { read: false, static: false }) EmpImg;
  @ViewChild("empEntry", { read: false, static: false }) empEntry;


  @Input()
  AddOnFly = false;

  @Output()
  AddOnFlySave = new EventEmitter<string>();


  txtSearchEmpNo = '';
  txtSearchEmpName = '';
  ddlSearchUnits = '';
  UnitsSearchList;
  PositionsSearchList;
  ddlSearchStatus = 1;
  ddlSearchPosition = '';
  showEmployeeEntry = false;
  txtEmpNumber = '';
  txtFirstEmpName = '';
  txtSecondName = '';
  txtThirdName = '';
  txtFamilyName = '';
  ddlUnits = '';
  ddlPosition = null;
  UnitsList;
  postionsList;
  ddlScale = '';
  ScaleList;
  txtAddress = '';
  txtPhone1 = '';
  txtPhone2 = '';
  SelectedID;
  chkStatus = true;
  upadteMode = false;
  ManagersList;
  ddlManagers = null;
  @ViewChild('fileInput', { read: false, static: false }) fileInput;
  ImportFile: boolean;
  @ViewChild('gvEmployees', { read: false, static: false }) gvEmployees;
  modificationPermission = false;
  constructor(private empService: EmployeeService, private userContextService: UserContextService,
    private userService: UsersService) {
    this.modificationPermission = this.userContextService.RoleId != 5;

    if (!this.AddOnFly) {
      this.userService.GetLocalResources(window.location.hash, this.userContextService.CompanyID, this.userContextService.language).subscribe(res => {
        if (res.IsError) {
          alert(res.ErrorMessage);
          return;
        }
        this.PageResources = res.Data;
        //this.SearchEmployees();
      });
    }

    if (this.AddOnFly) {
      this.userService.GetLocalResources("#/Employees", this.userContextService.CompanyID, this.userContextService.language).subscribe(res => {
        if (res.IsError) {
          alert(res.ErrorMessage);
          return;
        }
        this.PageResources = res.Data;
        //this.SearchEmployees();
      });
    }


    this.performSettings();
    this.ImportFile = false;
  }

  ngOnInit() {

  }


  setManagerID(managerID) {
    this.ddlManagers = managerID;
  }

  performSettings() {
    setTimeout(() => {
      let img: any = document.getElementById('imgEmpPic');
      img.src = 'assets/dist/img/Default.jpg';
    }, 500);

    this.txtSearchEmpNo = '';
    this.txtSearchEmpName = '';
    this.ddlSearchUnits = '';
    this.ddlSearchStatus = 1;
    this.ddlSearchPosition = '';
    this.txtEmpNumber = '';
    this.txtFirstEmpName = '';
    this.txtSecondName = '';
    this.txtThirdName = '';
    this.txtFamilyName = '';
    this.ddlUnits = '';
    this.ddlPosition = null;
    this.postionsList;
    this.ddlScale = '';
    this.txtAddress = '';
    this.txtPhone1 = '';
    this.txtPhone2 = '';
    this.ddlManagers = null;
    this.chkStatus = true;
    this.SelectedID = null;
    this.upadteMode = false;
    this.EmpImg = null;

    this.Loadunits();
    this.LoadPostions();
    this.GetScales();
    this.LoadManagers(this.SelectedID);


  }

  addMode() {
    this.performSettings();
    document.getElementById('empEntry').scrollIntoView()
    this.showEmployeeEntry = true;
  }

  LoadPostions() {
    this.empService.GetPositions(this.userContextService.CompanyID, this.userContextService.language).subscribe(res => {
      if (res.IsError) {
        alert(res.ErrorMessage);
        return;
      }
      this.postionsList = res.Data;
      this.PositionsSearchList = res.Data;
    });
  }

  GetScales() {
    this.empService.GetScales(this.userContextService.CompanyID).subscribe(res => {
      if (res.IsError) {
        alert(res.ErrorMessage);
        return;
      }
      this.ScaleList = res.Data;
    });
  }

  Loadunits() {
    this.empService.GetUnites(this.userContextService.CompanyID, this.userContextService.language).subscribe(res => {
      if (res.IsError) {
        alert(res.ErrorMessage);
        return;
      }
      this.UnitsList = res.Data;
      this.UnitsSearchList = res.Data;
    });
  }

  SearchEmployees() {
    this.empService.SearchEmployees(this.txtSearchEmpNo, this.txtSearchEmpName,
      this.ddlSearchUnits, this.ddlSearchPosition, this.ddlSearchStatus, this.userContextService.language, this.userContextService.CompanyID).subscribe(res => {
        if (res.IsError) {
          alert(res.ErrorMessage);
          return;
        }
        let cols = [
          { 'HeaderText': this.GetLocalResourceObject('EmployeeNo'), 'DataField': 'employee_number' },
          { 'HeaderText': this.GetLocalResourceObject('Name'), 'DataField': 'FullName' },
          { 'HeaderText': this.GetLocalResourceObject('Position'), 'DataField': 'Position' },
          { 'HeaderText': this.GetLocalResourceObject('Unit'), 'DataField': 'Unit' },
          { 'HeaderText': this.GetLocalResourceObject('Manager'), 'DataField': 'Manager' },
          { 'HeaderText': this.GetLocalResourceObject('Status'), 'DataField': 'Status' }
        ];

        let actions = [
          { "title": 'Edit', "DataValue": "ID", "Icon_Awesome": "fa fa-edit", "Action": "edit" },
          { "title": 'Delete', "DataValue": "ID", "Icon_Awesome": "fa fa-trash", "Action": "delete" }
        ];

        this.gvEmployees.bind(cols, res.Data, 'gvEmployees', this.modificationPermission?  actions:[]);
      });

    this.showEmployeeEntry = false;
  }

  gridEvent(event) {
    if (event[1] == 'edit') {
      this.LoadEmplyeeByID(event[0]);
      this.SelectedID = event[0];
      this.upadteMode = true;
      this.LoadManagers(this.SelectedID);

      this.showEmployeeEntry = true;
      document.getElementById('empEntry').scrollIntoView()
    }
    if (event[1] == 'delete') {
      if (confirm('Are you sure ?')) {
        this.empService.RemoveEmployee(event[0]).subscribe(res => {
          if (res.IsError) {
            alert(res.ErrorMessage);
            return;
          }
          this.addMode();
          this.SearchEmployees();
        });
      }

      this.showEmployeeEntry = false;
    }
  }
  PageResources = [];
  GetLocalResourceObject(resourceKey) {
    for (let i = 0; i < this.PageResources.length; i++) {
      if (this.PageResources[i].resource_key === resourceKey) {
        return this.PageResources[i].resource_value;
      }
    }
  }

  SaveEmployee() {
    if (!this.upadteMode) {
      this.empService.SaveEmployee(this.txtEmpNumber,
        this.txtFirstEmpName,
        this.txtSecondName,
        this.txtThirdName,
        this.txtFamilyName,
        this.ddlUnits,
        this.ddlPosition,
        this.ddlScale,
        this.txtAddress,
        this.txtPhone1,
        this.txtPhone2,
        this.chkStatus,
        this.userContextService.language,
        this.ddlManagers == null || this.ddlManagers == undefined || this.ddlManagers == '' ? null : this.ddlManagers,
        this.EmpImg ? this.EmpImg.nativeElement.files.length > 0 ? this.EmpImg.nativeElement.files[0] : null : null)
        .subscribe(res => {
          if (res.IsError) {
            alert(res.ErrorMessage);
            return;
          }
          alert("Employee Saved Successfully");
          this.addMode();
          this.SearchEmployees();
          if (this.AddOnFly) {
            this.AddOnFlySave.emit("Saved Suucessfully");
          }
        });
    }
    else {
      this.empService.UpdateEmployee(
        this.SelectedID,
        this.txtEmpNumber,
        this.txtFirstEmpName,
        this.txtSecondName,
        this.txtThirdName,
        this.txtFamilyName,
        this.ddlUnits,
        this.ddlPosition,
        this.ddlScale,
        this.txtAddress,
        this.txtPhone1,
        this.txtPhone2,
        this.chkStatus,
        this.userContextService.language,
        this.ddlManagers == null || this.ddlManagers == undefined || this.ddlManagers == '' ? null : this.ddlManagers,
        this.EmpImg.nativeElement.files.length > 0 ? this.EmpImg.nativeElement.files[0] : null).subscribe(res => {
          if (res.IsError) {
            alert(res.ErrorMessage);
            return;
          }
          alert("Employee Saved Successfully");
          this.addMode();
          this.SearchEmployees();
          if (this.AddOnFly) {
            this.AddOnFlySave.emit("Saved Suucessfully");
          }

        });
    }
    this.showEmployeeEntry = false;
  }

  LoadEmplyeeByID(empID) {
    this.empService.LoadEmplyeeByID(empID, this.userContextService.language).subscribe(res => {
      if (res.IsError) {
        alert(res.ErrorMessage);
        return;
      }

      let img: any = document.getElementById('imgEmpPic');

      this.txtEmpNumber = res.Data.employee_number;
      this.txtFirstEmpName = res.Data.name1_1;
      this.txtSecondName = res.Data.name1_2;
      this.txtThirdName = res.Data.name1_3;
      this.txtFamilyName = res.Data.name1_4;
      this.ddlUnits = res.Data.UNIT_ID;
      this.ddlPosition = res.Data.PositionID;
      this.ddlScale = res.Data.SCALE_ID;
      this.txtAddress = res.Data.ADDRESS;
      this.txtPhone1 = res.Data.PHONE1;
      this.txtPhone2 = res.Data.PHONE2;
      this.chkStatus = res.Data.IS_STATUS;
      this.ddlManagers = res.Data.ManagerID;
      img.src = res.Data.IMAGE;
    });
  }


  LoadManagers(empID) {
    this.empService.LoadManagers(empID, this.userContextService.language).subscribe(res => {
      if (res.IsError) {
        alert(res.ErrorMessage);
        return;
      }
      this.ManagersList = res.Data;
    });
  }

  uploadImg(empImg) {
    empImg.click();
  }


  readURL(input) {
    if (input.files && input.files[0]) {

    }
  }

  PreviewImg() {
    let preview: any = document.getElementById('imgEmpPic');
    var reader = new FileReader();

    var reader = new FileReader();
    reader.onload = function () {
      preview.src = reader.result;
    }
    reader.readAsDataURL(this.EmpImg.nativeElement.files[0]);

  }
  ShowImportPnl() {
    this.ImportFile = true;
    let formData = new FormData();
  }
  HideImportPnl() {
    this.ImportFile = false;
  }
  ImportFromExcel() {
    // tslint:disable-next-line:no-debugger
    // tslint:disable-next-line:prefer-const
    let formData = new FormData();
    // tslint:disable-next-line:align
    formData.append('upload', this.fileInput.nativeElement.files[0]);
    // tslint:disable-next-line:no-trailing-whitespace
    // tslint:disable-next-line:align
    this.empService.UploadExcel(formData, this.userContextService.CompanyID, this.userContextService.BranchID, this.userContextService.Username).subscribe(
      res => { alert('0') },
      err => {  alert(err.error + ',' + 1) },
      () => { this.ImportFile = false; }
    );
  }

}
