import {Component, OnInit, ViewChild} from '@angular/core';
import {CodesService} from "../Services/codes.service";
import {UserContextService} from "../Services/user-context.service";
import {UsersService} from "../Services/Users/users.service";
import {UnitsService} from "../Services/units.service";

@Component({
  selector: 'app-account-setup',
  templateUrl: './account-setup.component.html',
  styleUrls: ['./account-setup.component.css']
})
export class AccountSetupComponent implements OnInit {

  @ViewChild("gvUnits", {read: false, static: false}) gvUnits;
  PageResources = [];
  txtSearchUnits = "";
  txtUnitCode = "";
  currentUsername = "";
  unitTypesList;
  showUnitEntry = false;
  modificationPermission = false;

  constructor(
    private codesService: CodesService,
    private userContextService: UserContextService,
    private userService: UsersService,
    private unitsService: UnitsService
  ) {

  }

  GetLocalResourceObject(resourceKey) {
    for (const item of this.PageResources) {
      if (item.resource_key === resourceKey) {
        return item.resource_value;
      }
    }
  }

  ngOnInit() {

    this.LoadUnits();

  }

  gvUnitsEvent(event) {
    if (event[1] === "edit") {
      // event[0] -> unit id
      console.log(event);
      this.currentUsername = event[0];
      this.showUnitEntry = true;
    }
  }

  resetUserPassword() {
    this.userService.resetAccountPassword(this.currentUsername, this.txtUnitCode).subscribe(res => {
      if (res.IsError) {
        alert(res.ErrorMessage);
        return;
      }
      this.currentUsername = "";
      this.txtUnitCode = "";
      this.showUnitEntry = false;
    });


  }

  LoadUnits() {
    this.userService
      .GetUsersList(
        this.userContextService.CompanyID,
        this.txtSearchUnits,
      )
      .subscribe((res) => {
        if (res.IsError) {
          alert(res.ErrorMessage);
          return;
        }

        const cols = [
          {
            HeaderText: "Account Name",
            DataField: "NAME",
          },
          {
            HeaderText: "Account Username",
            DataField: "USERNAME",
          }
        ];
        const actions = [
          {
            title: "Edit",
            DataValue: "USERNAME",
            Icon_Awesome: "fa fa-edit",
            Action: "edit",
          },
        ];

        this.gvUnits.bind(cols, res.Data, "gvUnits", this.userContextService.Role === "Admin" ? actions : []);
      });


    this.showUnitEntry = false;
  }


}
