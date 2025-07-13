import {Component, OnInit, ViewChild, Input, Output, EventEmitter} from '@angular/core';
import {SettingsService} from '../settings.service';
import {UserContextService} from '../../Services/user-context.service';
import {UsersService} from '../../Services/Users/users.service';

@Component({
  selector: 'app-performance-levels-quota',
  templateUrl: './performance-levels-quota.component.html',
  styleUrls: ['./performance-levels-quota.component.css']
})
export class PerformanceLevelsQuotaComponent implements OnInit {


  @Input()
  IsChildInOtherPage = false;

  @ViewChild('gvLevels', {read: false, static: false}) gvLevels;

  performanceLevelQuotaView;
  RangesQuota;
  updateMode = false;
  selectID;
  YearList;
  showEntry = false;
  ddlYear = new Date().getFullYear();
  PageResources = [];

  constructor(
    private settingsService: SettingsService,
    public userContextService: UserContextService,
    private userService: UsersService
  ) {
    this.userService.GetLocalResources(window.location.hash, this.userContextService.CompanyID, this.userContextService.language)
      .subscribe(res => {
      if (res.IsError) {
        return;
      }
      this.PageResources = res.Data;
    });
    this.fillYearList();
    this.performSettings();
  }


  GetLocalResourceObject(resourceKey) {
    for (let i = 0; i < this.PageResources.length; i++) {
      if (this.PageResources[i].resource_key === resourceKey) {
        return this.PageResources[i].resource_value;
      }
    }
  }

  ngOnInit() {
  }

  performSettings() {

    this.updateMode = false;
    this.selectID = null;
    this.LoadAllPerformanceLevelQuotaView();
    this.showEntry = false;
    this.NewPerformanceQuota();
  }

  setYear(year) {
    this.ddlYear = year;
    this.LoadAllPerformanceLevelQuotaView();
  }

  fillYearList() {
    this.settingsService.LoadAllYears().subscribe(res => {
      if (res.IsError) {
        alert(res.ErrorMessage);
        return null;
      }
      this.YearList = res.Data;
      this.performSettings();
    });
  }

  AddMode() {
    this.updateMode = false;
    this.selectID = null;
    this.showEntry = true;
    this.NewPerformanceQuota();
  }


  SavePerformaceQuota() {
    if (!this.updateMode) {
      this.settingsService.SavePerformaceQuota(this.RangesQuota, this.userContextService.CompanyID, this.ddlYear).subscribe(res => {
        if (res.IsError) {
          alert(res.ErrorMessage);
          return;
        }
        this.performSettings();
      });
    } else {
      this.settingsService.UpdatePerformaceQuota(this.RangesQuota, this.userContextService.CompanyID, this.ddlYear).subscribe(res => {
        if (res.IsError) {
          alert(res.ErrorMessage);
          return;
        }
        this.performSettings();
      });
    }
  }

  /*
    SaveLevel() {
      debugger;
      if (!this.updateMode) {
        this.settingsService.SavePerformaceLevels(this.txtLvlName, this.txtLvlNumber, this.txtLevelPercentage, this.ddlYear, this.userContextService.CompanyID).subscribe(res => {
          if (res.IsError) {
            alert(res.ErrorMessage);
            return;
          }
          this.performSettings();
        });
      }
      else {
        this.settingsService.UpdatePerformanceLevel(this.selectID, this.txtLvlName, this.txtLvlNumber, this.txtLevelPercentage, this.ddlYear, this.userContextService.CompanyID).subscribe(res => {
          if (res.IsError) {
            alert(res.ErrorMessage);
            return;
          }
          this.performSettings();
        });
      }
    }




    getPerformanceByID(id) {
      this.settingsService.getPerformanceByID(id).subscribe(res => {
        if (res.IsError) {
          alert(res.ErrorMessage);
          return;
        }

        this.txtLvlName = res.Data.lvl_name;
        this.txtLvlNumber = res.Data.lvl_number;
        this.txtLevelPercentage = res.Data.lvl_percent;
        this.selectID = res.Data.id;
        this.updateMode = true;
        this.showEntry = true;
      });
    }

    DeletePerformanceByID(id) {
      this.showEntry = false;
      this.settingsService.DeletePerformanceByID(id).subscribe(res => {
        if (res.IsError) {
          alert(res.ErrorMessage);
          return;
        }
        alert("Employee Level Deleted.");
        this.performSettings();
      });
    }
  */

  /*
    SearchPerformanceLevelsQuota() {
      this.showEntry = false;
      this.settingsService.LoadAllPerformanceLevels(this.userContextService.CompanyID, this.ddlYear).subscribe(res => {
        if (res.IsError) {
          alert(res.ErrorMessage);
          return;
        }
        this.empLevelsList = res.Data;

        let cols = [

          { 'HeaderText': 'Level Number', 'DataField': 'lvl_number' },
          { 'HeaderText': 'Level Name', 'DataField': 'lvl_name' },

          { 'HeaderText': 'Percentage %', 'DataField': 'lvl_percent' }
        ];
        let actions = [
          { "title": 'Edit', "DataValue": "id", "Icon_Awesome": "fa fa-edit", "Action": "edit" },
          { "title": 'Delete', "DataValue": "id", "Icon_Awesome": "fa fa-trash", "Action": "delete" }
        ];

        this.gvLevels.bind(cols, res.Data, 'gvLevels', actions);
      });
    }
  */

  LoadAllPerformanceLevelQuotaView() {
    this.showEntry = false;
    this.settingsService.LoadAllPerformanceLevelQuotaView(this.userContextService.CompanyID, this.ddlYear, this.userContextService.language).subscribe(res => {
      if (res.IsError) {
        return;
      }
      this.performanceLevelQuotaView = res.Data;
    });
  }

  DeletePerformanceQuota(FromPercentage) {
    this.showEntry = false;
    this.settingsService.DeletePerformanceQuota(FromPercentage, this.userContextService.CompanyID, this.ddlYear).subscribe(res => {
      if (res.IsError) {
        alert(res.ErrorMessage);
        return;
      }
      alert("Performance Quota Deleted Deleted.");
      this.performSettings();
    });
  }

  EditPerformanceQuota(FromPercentage) {
    this.settingsService.EditPerformanceQuota(FromPercentage, this.userContextService.CompanyID, this.ddlYear).subscribe(res => {
      if (res.IsError) {
        alert(res.ErrorMessage);
        return;
      }
      this.RangesQuota = res.Data;
      this.selectID = res.Data.yearId;
      this.updateMode = true;
      this.showEntry = true;
    });
  }


  NewPerformanceQuota() {
    this.settingsService.NewPerformanceQuota(this.userContextService.CompanyID, this.ddlYear).subscribe(res => {
      if (res.IsError) {
        return;
      }
      this.RangesQuota = res.Data;
      this.selectID = null;
      this.updateMode = false;
    });
  }

}

