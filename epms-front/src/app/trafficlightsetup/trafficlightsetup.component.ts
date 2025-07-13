import {Component, OnInit, ViewChild} from '@angular/core';
import {SettingsService} from '../settings/settings.service';
import {UserContextService} from '../Services/user-context.service';
import {UsersService} from '../Services/Users/users.service';

@Component({
  selector: 'app-trafficlightsetup',
  templateUrl: './trafficlightsetup.component.html',
  styleUrls: ['./trafficlightsetup.component.css']
})
export class TrafficlightsetupComponent implements OnInit {


  @ViewChild('gvTrafficLights', {read: false, static: false}) gvTrafficLights;

  constructor(
    private settingsService: SettingsService,
    private userContextService: UserContextService,
    private userService: UsersService) {

    this.fillYearList();

    this.userService.GetLocalResources(window.location.hash, this.userContextService.CompanyID, this.userContextService.language).subscribe(res => {
      if (res.IsError) {
        alert(res.ErrorMessage);
        return;
      }
      this.PageResources = res.Data;
    });
  }

  ngOnInit() {
  }

  showEntry = false;
  ddlYear
  trafficLightList
  PageResources = [];

  txtName;
  txtPercFrom;
  txtPercTo;
  txtDesc;
  ddlColors;

  YearList;

  GetLocalResourceObject(resourceKey) {
    for (let i = 0; i < this.PageResources.length; i++) {
      if (this.PageResources[i].resource_key === resourceKey) {
        return this.PageResources[i].resource_value;
      }
    }
  }


  fillYearList() {
    this.settingsService.LoadAllYears().subscribe(res => {
      if (res.IsError) {
        alert(res.ErrorMessage);
        return null;
      }
      this.YearList = res.Data;
      if (this.YearList != null && this.YearList.length > 0) {
        this.YearList.forEach((element) => {
          this.ddlYear = element.id;
        });
        this.SearchFun();
      }

    });
  }


  SearchFun() {
    this.showEntry = false;
    this.settingsService.LoadTrafficLights(this.userContextService.CompanyID, this.ddlYear).subscribe(res => {
      if (res.IsError) {
        alert(res.ErrorMessage);
        return;
      }
      this.trafficLightList = res.Data;

      let cols = [
        {'HeaderText': "Name", 'DataField': 'name'},
        {'HeaderText': "From Percentage", 'DataField': 'perc_from'},
        {'HeaderText': "To Percentage", 'DataField': 'perc_to'},
        {'HeaderText': "Color", 'DataField': 'color'},
      ];

      let actions = [
        {"title": 'Edit', "DataValue": "ID", "Icon_Awesome": "fa fa-edit", "Action": "edit"},
        {"title": 'Delete', "DataValue": "ID", "Icon_Awesome": "fa fa-trash", "Action": "delete"}
      ];

      this.gvTrafficLights.bind(cols, this.trafficLightList, 'gvLevels', actions);
    });
  }

  selectedID = null;

  gvTrafficLightsHandler(event) {
    if (event[1] == 'edit') {
      this.selectedID = event[0];
      this.settingsService.LoadTrafficLightsByID(event[0]).subscribe(res => {
        if (res.IsError) {
          alert(res.ErrorMessage + ",1");
          return;
        }
        this.txtName = res.Data.name;
        this.txtPercFrom = res.Data.perc_from;
        this.txtPercTo = res.Data.perc_to;
        this.ddlColors = res.Data.color;
        this.showEntry = true;


      })
    } else if (event[1] == 'delete') {
      this.settingsService.DeleteTrafficLight(event[0]).subscribe(res => {
        if (res.IsError) {
          alert(res.ErrorMessage + ",1");
          return;
        }
        alert("0");
        this.SearchFun();
      })
    }
  }

  AddMode() {
    this.txtName = '';
    this.txtPercFrom = '';
    this.txtPercTo = '';
    this.ddlColors = '';
    this.selectedID = null;
    this.showEntry = true;

  }

  Save() {
    if (this.selectedID != null) {
      this.settingsService.UpdateTrafficLight(this.selectedID, this.txtName, this.txtPercFrom, this.txtPercTo, this.ddlColors, this.userContextService.CompanyID, this.ddlYear).subscribe(res => {
        if (res.IsError) {
          alert(res.ErrorMessage + ",1");
          return;
        }
        alert("0");
        this.SearchFun();
      })
    } else {
      this.settingsService.SaveTrafficLight(this.txtName, this.txtPercFrom, this.txtPercTo, this.ddlColors, this.userContextService.CompanyID, this.ddlYear).subscribe(res => {
        if (res.IsError) {
          alert(res.ErrorMessage + ",1");
          return;

        }
        alert("0");
        this.SearchFun();
      })
    }
  }

}
