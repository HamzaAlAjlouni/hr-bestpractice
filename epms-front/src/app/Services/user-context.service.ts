import {Injectable} from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class UserContextService {

  constructor() {

    this.Username = "";
    this.fullname = "";
    this.isLoggedIn = false;
    this.CompanyID = 0;
    this.BranchID = 0;
    this.UnitId = 0;
    this.Role = "";
    this.RoleId = 0;
    this.Position = "";
    this.Unit = "";


    if (sessionStorage.getItem('userContext') != null) {

      const userContext = JSON.parse(sessionStorage.getItem('userContext'));
      console.log(userContext);
      this.Username = userContext[0].Username;
      this.isLoggedIn = userContext[1].isLoggedIn;
      this.CompanyID = userContext[2].CompanyID;
      this.BranchID = userContext[3].BranchID;
      this.fullname = userContext[4] ? userContext[4].fullname : "";
      this.UnitId = userContext[5].UnitId;
      this.Role = userContext[6].Role;
      this.RoleId = userContext[7].RoleId;
      this.Position = userContext[8].Position;
      this.Unit = userContext[9].Unit;
    }
  }

  Username = 'ADMIN';
  fullname = 'ADMIN';
  isLoggedIn = false;
  CompanyID = 1;
  UnitId = 1;
  Role = "";
  RoleId = 0;
  isLoading = false;
  BranchID = 1;
  language = 'en';
  Position = '';
  Unit = '';
  customAr: any;

}
