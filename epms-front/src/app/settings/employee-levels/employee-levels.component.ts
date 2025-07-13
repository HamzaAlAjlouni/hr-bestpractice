import { Component, OnInit, ViewChild } from '@angular/core';
import { SettingsService } from '../settings.service';

@Component({
  selector: 'app-employee-levels',
  templateUrl: './employee-levels.component.html',
  styleUrls: ['./employee-levels.component.css']
})
export class EmployeeLevelsComponent implements OnInit {

  @ViewChild('gvLevels',{read:false,static:false}) gvLevels;

  txtLvlCode;
  txtLvlName;
  txtLvlNumber;
  empLevelsList;
  updateMode = false;
  selectID;
  constructor(private settingsService: SettingsService) {
    this.performSettings();
  }

  ngOnInit() {
  }

  performSettings() {
    this.txtLvlCode = '';
    this.txtLvlName = '';
    this.txtLvlNumber = null;

    this.updateMode = false;
    this.selectID = null;
    this.LoadLevels();
  }

  AddMode(){
    this.txtLvlCode = '';
    this.txtLvlName = '';
    this.txtLvlNumber = null;
    this.updateMode = false;
    this.selectID = null;
  }

  SaveLevel() {
    if (!this.updateMode) {
      this.settingsService.SaveLevels(this.txtLvlCode, this.txtLvlName,this.txtLvlNumber).subscribe(res => {
        if (res.IsError) {
          alert(res.ErrorMessage);
          return;
        }
        this.performSettings();
      });
    }
    else {
      this.settingsService.UpdateEmpLvl(this.selectID,this.txtLvlCode, this.txtLvlName,this.txtLvlNumber).subscribe(res => {
        if (res.IsError) {
          alert(res.ErrorMessage);
          return;
        }
        this.performSettings();
      });
    }
  }

  LoadLevels() {
    this.settingsService.LoadAllEmpLevels().subscribe(res => {
      if (res.IsError) {
        alert(res.ErrorMessage);
        return;
      }
      
      this.empLevelsList = res.Data;

      let cols =[
        { 'HeaderText': 'Level Code', 'DataField': 'lvl_code' },
        { 'HeaderText': 'Level Name', 'DataField': 'lvl_name' },
        { 'HeaderText': 'Level Number', 'DataField': 'lvl_number' }
      ];
      let actions=[
        { "title": 'Edit',"DataValue":"id","Icon_Awesome":"fa fa-edit","Action":"edit"},
        { "title": 'Delete',"DataValue":"id","Icon_Awesome":"fa fa-trash","Action":"delete"}
      ];

      this.gvLevels.bind(cols,res.Data,'gvLevels',actions);
    });
  }

  gridEvent(event){
    if(event[1] == 'edit'){
      this.getLevel(event[0]);
    }
    else if(event[1]== 'delete'){
      if(confirm('Are you sure?')){
        this.deleteLevel(event[0]);
      }
    }

  }


  getLevel(id) {
    this.settingsService.getEmpLvlbyID(id).subscribe(res => {
      if (res.IsError) {
        alert(res.ErrorMessage);
        return;
      }
      this.txtLvlCode = res.Data.lvl_code;
      this.txtLvlName = res.Data.lvl_name;
      this.txtLvlNumber = res.Data.lvl_number;

      this.selectID = res.Data.id;
      this.updateMode = true;
    });
  }

  deleteLevel(id) {
    this.settingsService.DeleteEmpLvlByID(id).subscribe(res => {
      if (res.IsError) {
        alert(res.ErrorMessage);
        return;
      } 
      alert("Employee Level Deleted.");
      this.performSettings();
    });
  }

}
