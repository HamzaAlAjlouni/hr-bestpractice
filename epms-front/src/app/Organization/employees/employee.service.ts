import {Injectable, ViewChild} from '@angular/core';
import {HttpClient, JsonpClientBackend, HttpHeaders} from '../../../../node_modules/@angular/common/http';
import {config} from '../../../../node_modules/rxjs';
import {Config} from '../../Config';
import {UserContextService} from '../../Services/user-context.service';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {

  constructor(private http: HttpClient, private user: UserContextService) {
  }

  SearchEmployees(empNumber, empName, unitID, position, status, LanguageCode, companyId) {
    return this.http.get<any>(Config.WebApiUrl + "Employees/SearchEmployees", {
      params: {
        empNumber: empNumber,
        empName: empName,
        unitID: unitID,
        position: position,
        status: status,
        LanguageCode: LanguageCode,
        companyId: companyId
      }
    });
  }

  LoadEmplyeeByCompanyID(CompanyID, LanguageCode, unitID = null) {
    return this.http.get<any>(Config.WebApiUrl + "Employees/LoadEmplyeeByCompanyID", {
      params: {
        CompanyID: CompanyID,
        LanguageCode: LanguageCode,
        unitID: unitID
      }
    });
  }


  GetUnites(compID, LanguageCode) {
    return this.http.get<any>(Config.WebApiUrl + "unites/GetUnites", {
      params: {
        CompanyID: compID,
        LanguageCode
      }
    });
  }

  GetPositions(compID, lang) {
    return this.http.get<any>(Config.WebApiUrl + "Positions/GetPositions", {
      params: {
        Name: '',
        CompanyID: compID,
        languageCode: lang

      }
    });
  }

  GetScales(compID) {
    return this.http.get<any>(Config.WebApiUrl + "Scales/GetScales", {
      params: {
        CompanyID: compID
      }
    });
  }


  SaveEmployee(
    txtEmpNumber,
    txtFirstEmpName,
    txtSecondName,
    txtThirdName,
    txtFamilyName,
    ddlUnits,
    ddlPosition,
    ddlScale,
    txtAddress,
    txtPhone1,
    txtPhone2,
    chkStatus,
    LanguageCode,
    ddlManagers,
    empImg?: File) {


    let Data = {

      EmpNumber: txtEmpNumber,
      firstName: txtFirstEmpName,
      secondName: txtSecondName,
      thirdName: txtThirdName,
      famaliyName: txtFamilyName,
      unitID: ddlUnits,
      postionID: ddlPosition,
      scaleID: ddlScale,
      address: txtAddress,
      phone1: txtPhone1,
      phone2: txtPhone2,
      status: chkStatus,
      Branch: this.user.BranchID,
      Username: this.user.Username,
      CompanyID: this.user.CompanyID,
      ManagerID: ddlManagers,
      LanguageCode
    };
    let form = new FormData();
    form.append("empData", JSON.stringify(Data));
    if (empImg != null && empImg != undefined) {
      form.append("empImg", empImg, empImg.name);
    }
    return this.http.post<any>(Config.WebApiUrl + "Employees/SaveEmployee", form);
  }

  LoadEmplyeeByID(empID, LanguageCode) {
    return this.http.get<any>(Config.WebApiUrl + "Employees/LoadEmplyeeByID", {
      params: {
        empID: empID,
        LanguageCode: LanguageCode
      }
    });
  }

  RemoveEmployee(empID) {
    return this.http.get<any>(Config.WebApiUrl + "Employees/RemoveEmployee", {
      params: {
        empID: empID
      }
    });
  }

  LoadManagers(empID, LanguageCode) {
    return this.http.get<any>(Config.WebApiUrl + "Employees/LoadManagers",
      {
        params: {
          empID: empID,
          LanguageCode: LanguageCode
        }
      });
  }

  UpdateEmployee(
    employeeID,
    txtEmpNumber,
    txtFirstEmpName,
    txtSecondName,
    txtThirdName,
    txtFamilyName,
    ddlUnits,
    ddlPosition,
    ddlScale,
    txtAddress,
    txtPhone1,
    txtPhone2,
    chkStatus,
    LanguageCode,
    ddlManagers,
    empImg?: File,
  ) {
    let Data = {
      ID: employeeID,
      EmpNumber: txtEmpNumber,
      firstName: txtFirstEmpName,
      secondName: txtSecondName,
      thirdName: txtThirdName,
      famaliyName: txtFamilyName,
      unitID: ddlUnits,
      postionID: ddlPosition,
      scaleID: ddlScale,
      address: txtAddress,
      phone1: txtPhone1,
      phone2: txtPhone2,
      status: chkStatus,
      Branch: this.user.BranchID,
      Username: this.user.Username,
      CompanyID: this.user.CompanyID,
      LanguageCode: LanguageCode,
      ManagerID: ddlManagers,
    }
    let form = new FormData();
    form.append("empData", JSON.stringify(Data));
    if (empImg != null && empImg != undefined) {
      form.append("empImg", empImg, empImg.name);
    }
    return this.http.post<any>(Config.WebApiUrl + "Employees/UpdateEmployee", form);
  }
//#region Employee Structure
  LoadStructure(compID, branch, unit, Manager, LanguageCode) {
    return this.http.get<any>(Config.WebApiUrl + "Employees/LoadEmployeesChart", {
      params: {
        compID: compID,
        branch: branch,
        unit: unit,
        Manager: Manager,
        LanguageCode: LanguageCode
      }
    });
  }
  LoadEmployeesNewChart(compID, branch, unit, Manager, LanguageCode) {
    return this.http.get<any>(Config.WebApiUrl + "Employees/LoadEmployeesNewChart", {
      params: {
        compID: compID,
        branch: branch,
        unit: unit,
        Manager: Manager,
        LanguageCode: LanguageCode
      }
    });
  }
  UpdateManager(empID, managerID) {
    return this.http.get<any>(Config.WebApiUrl + "Employees/UpdateManager", {
      params: {
        empID: empID,
        managerID: managerID
      }
    });
  }

//#endregion
  UploadExcel(formData: FormData, companyId, branchId, userName) {
    const headers = new HttpHeaders();
    headers.append('Content-Type', 'multipart/form-data');
    headers.append('Accept', 'application/json');
    // tslint:disable-next-line:object-literal-shorthand
    const httpOptions = {headers: headers};
    // tslint:disable-next-line:whitespace
    // tslint:disable-next-line:max-line-length
    return this.http.post(Config.WebApiUrl + '/Employees/Import?userName=' + userName + '&companyId=' + companyId + '&branchId=' + branchId, formData, httpOptions);
  }
}
