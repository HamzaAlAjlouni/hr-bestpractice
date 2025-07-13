import {Component, OnInit, ViewChild} from "@angular/core";

import {CodesService} from "../Services/codes.service";
import {UserContextService} from "../Services/user-context.service";
import {UsersService} from "../Services/Users/users.service";
import {UnitsService} from "../Services/units.service";

@Component({
  selector: "app-units",
  templateUrl: "./units.component.html",
  styleUrls: ["./units.component.css"],
  providers: [UsersService],
})
export class UnitsComponent implements OnInit {
  @ViewChild("gvUnits", {read: false, static: false}) gvUnits;
  PageResources = [];
  txtSearchUnits = "";
  selectedUnitID;
  txtUnitCode = "";
  txtUnitNameEntry = "";
  txtUnitAddressEntry = "";
  txtUnitFaxEntry = "";
  txtUnitPHONE1Entry = "";
  txtUnitPHONE2Entry = "";
  ddlUnitTypeID = 1;
  unitTypesList;
  showUnitEntry = false;
  UnitUpdateMode;
  modificationPermission = false;

  constructor(
    private codesService: CodesService,
    private userContextService: UserContextService,
    private userService: UsersService,
    private unitsService: UnitsService
  ) {
    this.modificationPermission = this.userContextService.RoleId != 5;
    console.log("can modify?", this.modificationPermission)
    this.getUnitTypeCodesList();
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

  getUnitTypeCodesList() {
    this.codesService
      .LoadCodes(
        this.userContextService.CompanyID,
        8,
        this.userContextService.language
      )
      .subscribe((res) => {
        if (res.IsError) {
          alert(res.ErrorMessage);
          return null;
        }
        this.unitTypesList = res.Data;
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

    this.LoadUnits();

  }

  SaveUnits() {
    if (!this.UnitUpdateMode) {
      this.unitsService
        .SaveUnit(
          this.txtUnitCode,
          this.userContextService.CompanyID,
          this.userContextService.Username,
          this.ddlUnitTypeID,
          this.txtUnitNameEntry,
          this.txtUnitFaxEntry,
          this.txtUnitPHONE1Entry,
          this.txtUnitPHONE2Entry,
          this.txtUnitAddressEntry,
          this.userContextService.language
        )
        .subscribe((res) => {
          if (res.IsError) {
            alert(res.ErrorMessage);
            return;
          }
          this.UnitsAddMode();
          this.LoadUnits();
        });
    } else {
      this.unitsService
        .UpdateUnit(
          this.selectedUnitID,
          this.txtUnitCode,
          this.userContextService.CompanyID,
          this.userContextService.Username,
          this.ddlUnitTypeID,
          this.txtUnitNameEntry,
          this.txtUnitFaxEntry,
          this.txtUnitPHONE1Entry,
          this.txtUnitPHONE2Entry,
          this.txtUnitAddressEntry,
          this.userContextService.language
        )
        .subscribe((res) => {
          if (res.IsError) {
            alert(res.ErrorMessage);
            return;
          }
          this.UnitsAddMode();
          this.LoadUnits();
        });
    }

    this.showUnitEntry = true;
  }

  UnitsAddMode() {
    this.txtUnitAddressEntry = "";
    this.txtUnitCode = "";
    this.txtUnitFaxEntry = "";
    this.txtUnitNameEntry = "";
    this.txtUnitPHONE1Entry = "";
    this.txtUnitPHONE2Entry = "";
    this.ddlUnitTypeID = 1;
    this.UnitUpdateMode = false;
    this.showUnitEntry = true;
  }

  LoadUnitByID(id) {
    this.unitsService
      .LoadUnitByID(id, this.userContextService.language)
      .subscribe((res) => {
        if (res.IsError) {
          alert(res.ErrorMessage);
          return;
        }
        this.txtUnitAddressEntry = res.Data.ADDRESS;
        this.txtUnitFaxEntry = res.Data.FAX;
        this.txtUnitCode = res.Data.CODE;
        this.txtUnitNameEntry = res.Data.NAME;
        this.txtUnitPHONE1Entry = res.Data.PHONE1;
        this.txtUnitPHONE2Entry = res.Data.PHONE2;
        this.ddlUnitTypeID = res.Data.C_UNIT_TYPE_ID;

        this.selectedUnitID = res.Data.ID;
        this.UnitUpdateMode = true;
      });
    this.UnitUpdateMode = true;
    this.showUnitEntry = true;
  }

  gvUnitsEvent(event) {
    if (event[1] == "edit") {
      this.LoadUnitByID(event[0]);
      this.showUnitEntry = true;
    } else if (event[1] == "delete") {
      if (confirm(this.GetLocalResourceObject("Areyousure"))) {
        this.DeleteUnit(event[0]);
        this.showUnitEntry = false;
      }
    }
  }

  LoadUnits() {
    if (this.userContextService.CompanyID > 0) {
      this.unitsService
        .LoadUnits(
          this.userContextService.CompanyID,
          this.txtSearchUnits,
          this.userContextService.language
        )
        .subscribe((res) => {
          if (res.IsError) {
            alert(res.ErrorMessage);
            return;
          }

          let cols = [
            {
              HeaderText: this.GetLocalResourceObject("Name"),
              DataField: "NAME",
            },
            {
              HeaderText: "Number of Employees",
              DataField: "empCount",
            },
            {
              HeaderText: "Account Username",
              DataField: "CODE",
            }
          ];
          let actions = [
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

          this.gvUnits.bind(cols, res.Data, "gvUnits", this.modificationPermission ? actions : []);
        });
    } else {
    }

    this.showUnitEntry = false;
  }

  DeleteUnit(id) {
    this.unitsService.DeleteUnit(id).subscribe((res) => {
      if (res.IsError) {
        alert(res.ErrorMessage);
        return;
      }
      alert(this.GetLocalResourceObject("UnitDeleted"));
      this.UnitsAddMode();
      this.LoadUnits();
    });
  }
}
