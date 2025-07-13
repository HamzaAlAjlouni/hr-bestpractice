import {Component, OnInit, ViewChild, AfterViewInit} from '@angular/core';
import {SettingsService} from '../settings.service';
import {UserContextService} from '../../Services/user-context.service';
import {UsersService} from '../../Services/Users/users.service';
import {EmployeeSegmentService} from './employee-segment.service';


@Component({
  selector: 'app-performance-levels',
  templateUrl: './performance-levels.component.html',
  styleUrls: ['./performance-levels.component.css'],
  providers: [EmployeeSegmentService]
})
export class PerformanceLevelsComponent implements AfterViewInit {
  @ViewChild('performanceLevelsQuota', {read: false, static: false}) performanceLevelsQuota;
  @ViewChild('gvLevels', {read: false, static: false}) gvLevels;
  segmentData = [];
  maxSegmentValue: number = 0;
  sumOfLevelPercentage: number = 0;
  levelPercentageById: number = 0;
  txtLvlName;
  txtLvlNumber;
  txtLevelPercentage;
  performanceLevelsList;
  updateMode = false;
  selectID;
  YearList;
  ddlYear;
  showEntry = false;
  PageResources = [];

  constructor(
    private settingsService: SettingsService,
    private userContextService: UserContextService,
    private userService: UsersService,
    private segmentService: EmployeeSegmentService) {


    this.userService.GetLocalResources(window.location.hash, this.userContextService.CompanyID, this.userContextService.language).subscribe(res => {
      if (res.IsError) {
        alert(res.ErrorMessage);
        return;
      }
      this.PageResources = res.Data;
    });

    this.fillYearList();
  }

  ngOnInit() {

  }

  ngAfterViewInit() {
  }


  GetLocalResourceObject(resourceKey) {
    for (let i = 0; i < this.PageResources.length; i++) {
      if (this.PageResources[i].resource_key === resourceKey) {
        return this.PageResources[i].resource_value;
      }
    }
  }

  performSettings() {
    this.txtLvlName = '';
    this.txtLvlNumber = null;
    this.txtLevelPercentage = null;
    this.updateMode = false;
    this.selectID = null;
    this.performanceLevelsQuota.setYear(this.ddlYear);
    this.SearchPerformanceLevels();
    this.showEntry = false;
  }


  fillYearList() {
    this.settingsService.LoadAllYears().subscribe(res => {
      if (res.IsError) {
        alert(res.ErrorMessage);
        return null;
      }
      this.YearList = res.Data;

      this.YearList = res.Data;
      if (this.YearList != null && this.YearList.length > 0) {
        this.YearList.forEach((element) => {
          this.ddlYear = element.id;
        });
        this.performSettings();
      }
    });
  }

  AddMode() {
    this.txtLvlName = '';
    this.txtLvlNumber = null;
    this.updateMode = false;
    this.selectID = null;
    this.txtLevelPercentage = null;
    this.showEntry = true;
  }

  SaveLevel() {
    if (!this.updateMode) {
      if (this.sumOfLevelPercentage + this.txtLevelPercentage <= 100) { // check again
        this.settingsService.SavePerformaceLevels(this.txtLvlName, this.txtLvlNumber, this.txtLevelPercentage, this.ddlYear, this.userContextService.CompanyID).subscribe(res => {
          if (res.IsError) {
            alert(res.ErrorMessage);
            return;
          }
          this.performSettings();
        });
      } else {
        alert('Error: The sum of level percentage more than 100%')
      }
    } else {
      if ((this.sumOfLevelPercentage - this.levelPercentageById) + this.txtLevelPercentage <= 100) { // check again
        this.settingsService.UpdatePerformanceLevel(this.selectID, this.txtLvlName, this.txtLvlNumber, this.txtLevelPercentage, this.ddlYear, this.userContextService.CompanyID).subscribe(res => {
          if (res.IsError) {
            alert(res.ErrorMessage);
            return;
          }
          this.performSettings();
        });
      } else {
        alert('Error: The sum of level percentage more than 100%');
      }
    }
  }


  gvLevelsEvent(event) {
    if (event[1] == 'edit') {

      this.getPerformanceByID(event[0]);
    } else if (event[1] == 'delete') {
      if (confirm(this.GetLocalResourceObject('lblAreYouSure'))) {
        this.DeletePerformanceByID(event[0]);
      }
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
      this.levelPercentageById = res.Data.lvl_percent;
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
      alert(this.GetLocalResourceObject('PerformanceLevelDeleted') + "," + 1);
      this.performSettings();
    });
  }


  SearchPerformanceLevels() {
    this.LoadSegments();
    this.performanceLevelsQuota.setYear(this.ddlYear);
    this.showEntry = false;
    this.settingsService.LoadAllPerformanceLevels(this.userContextService.CompanyID, this.ddlYear).subscribe(res => {
      if (res.IsError) {
        alert(res.ErrorMessage);
        return;
      } else {
        this.performanceLevelsList = res.Data;
        this.sumOfLevelPercentage = this.performanceLevelsList.reduce((a, b) => a + (b['lvl_percent'] || 0), 0)
        let cols = [

          {'HeaderText': this.GetLocalResourceObject('lblLevelNumber'), 'DataField': 'lvl_number'},
          {'HeaderText': this.GetLocalResourceObject('lblLevelName'), 'DataField': 'lvl_name'},

          {'HeaderText': this.GetLocalResourceObject('lblPercentage'), 'DataField': 'lvl_percent'}
        ];
        let actions = [
          {"title": 'Edit', "DataValue": "id", "Icon_Awesome": "fa fa-edit", "Action": "edit"},
          {"title": 'Delete', "DataValue": "id", "Icon_Awesome": "fa fa-trash", "Action": "delete"}
        ];
        this.gvLevels.bind(cols, this.performanceLevelsList, 'gvLevels', actions);
      }
    });
  }

  //#region Employee Segment
  @ViewChild('gvSegments', {read: false, static: false}) gvSegments;

  showSegmentEntry = false;
  txtSegmentName;
  txtSegment;
  txtPercFrom;
  txtPercTo;
  txtSegmentDesc;

  selectedSegmentID;
  segmentEdit = false;


  LoadSegments() {
    this.segmentService.getEmpPerfSegments('', this.ddlYear, this.userContextService.CompanyID).subscribe(res => {
      if (res.IsError) {
        alert(res.ErrorMessage + ",1");
        return;
      } else {
        this.segmentData = res.Data;
        this.maxSegmentValue = Math.max.apply(Math, this.segmentData.map((percentage) => percentage.percentage_to))
        let cols = [

          {'HeaderText': "Segment", 'DataField': 'segment'},
          {'HeaderText': "Segment Name", 'DataField': 'name'},
          {'HeaderText': "Percentage From", 'DataField': 'percentage_from'},
          {'HeaderText': "Percentage To", 'DataField': 'percentage_to'}
        ];

        let actions = [
          {"title": 'Edit', "DataValue": "id", "Icon_Awesome": "fa fa-edit", "Action": "edit"},
          {"title": 'Delete', "DataValue": "id", "Icon_Awesome": "fa fa-trash", "Action": "delete"}
        ];

        this.gvSegments.bind(cols, res.Data, 'gvSegments', actions);
      }
    })
  }

  AddSegmentMode() {
    this.segmentEdit = false;
    this.showSegmentEntry = true;
    this.ResetSegmentForm();
  }

  ResetSegmentForm() {
    this.txtSegment = '';
    this.txtSegmentDesc = '';
    this.txtSegmentName = '';
    this.txtPercFrom = '';
    this.txtPercTo = '';
  }

  gvSegmentsEventHandler(event) {
    if (event[1] == "edit") {
      this.showSegmentEntry = true;
      this.segmentEdit = true;
      this.loadSegmentByID(event[0]);
    } else if (event[1] == 'delete') {
      this.deleteSegment(event[0]);
    }
  }

  loadSegmentByID(segID) {
    this.selectedSegmentID = segID;
    this.segmentService.getEmpSegmentByID(segID).subscribe(res => {
      if (res.IsError) {
        alert(res.ErrorMessage + ",1");
        return;
      }
      this.txtSegment = res.Data.segment;
      this.txtSegmentDesc = res.Data.description;
      this.txtSegmentName = res.Data.name;
      this.txtPercFrom = res.Data.percentage_from;
      this.txtPercTo = res.Data.percentage_to;
    });
  }

  deleteSegment(segID) {
    this.segmentService.deleteEmpSegment(segID).subscribe(res => {
      if (res.IsError) {
        alert(res.ErrorMessage + ",1");
        return;
      }
      alert("0");
      this.LoadSegments();
    });
  }

  SaveSegment() {
    if (!this.segmentEdit) {
      if (this.txtPercFrom < this.maxSegmentValue) {
        alert('Percentage From must be more than last percent value of segments')
      } else if (this.txtPercFrom >= this.txtPercTo) {
        alert('Percentage To must be more than Percentage Form')
      } else {
        this.segmentService.SaveEmpPerf_segments(this.txtSegmentName,
          this.txtSegmentDesc,
          this.txtSegment,
          this.txtPercFrom,
          this.txtPercTo,
          this.ddlYear,
          this.userContextService.CompanyID
        ).subscribe(res => {
          if (res.IsError) {
            alert(res.ErrorMessage + ",1");
            return;
          }
          alert("0");
          this.LoadSegments();
        });
      }
    } else {
      this.segmentService.UpdateEmpPerf_segments(this.selectedSegmentID,
        this.txtSegmentName,
        this.txtSegmentDesc,
        this.txtSegment,
        this.txtPercFrom,
        this.txtPercTo,
        this.ddlYear,
        this.userContextService.CompanyID
      ).subscribe(res => {
        if (res.IsError) {
          alert(res.ErrorMessage + ",1");
          return;
        }
        alert("0");
        this.LoadSegments();
      });
    }
  }

  //#endregion

}

