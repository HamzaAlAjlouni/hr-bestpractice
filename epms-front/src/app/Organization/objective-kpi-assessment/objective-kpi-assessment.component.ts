import { Component, OnInit, ViewChild } from '@angular/core';
import { OrganizationService } from '../organization.service';
import { SettingsService } from '../../settings/settings.service';
import { UserContextService } from '../../Services/user-context.service';
import { UsersService } from '../../Services/Users/users.service';

@Component({
  selector: 'app-objective-kpi-assessment',
  templateUrl: './objective-kpi-assessment.component.html',
  styleUrls: ['./objective-kpi-assessment.component.css']
})
export class ObjectiveKPIAssessmentComponent implements OnInit {
  @ViewChild('gvKPIs', { read: false, static: false }) gvKPIs;
  constructor(private OrganizationService: OrganizationService,
    private settingsService: SettingsService,
    private userContextService: UserContextService,
    private userService: UsersService) { }

  ngOnInit() {

    this.userService.GetLocalResources(window.location.hash,this.userContextService.CompanyID,
      this.userContextService.language).subscribe(res=>{
      if(res.IsError){
        alert(res.ErrorMessage);
        return;
      }
      this.PageResources = res.Data;
    });

  }


  selectedObjectiveID;
  disabledResult = false;
  setObjDefaultAddOnFly(id,disableResult = false) {
    this.selectedObjectiveID = id;
    this.LoadObjectiveKPIs();
    this.disabledResult = disableResult;
  }

  GetLocalResourceObject(resourceKey){
    for(let i=0;i<this.PageResources.length;i++){
      if(this.PageResources[i].resource_key === resourceKey){
        return this.PageResources[i].resource_value;
      }
    }
  }

  PageResources=[];
  objectivesKPIs;

  LoadObjectiveKPIs() {
    this.OrganizationService.getObjectiveKpiByObjective(this.selectedObjectiveID, this.userContextService.language).subscribe(res => {
      if (res.IsError) {
        alert(res.ErrorMessage);
        return;
      }
      this.objectivesKPIs = res.Data;
    });
  }

  onSaveKpisResults(){
    this.OrganizationService.saveObjectivesKPIsAssessment(this.objectivesKPIs).subscribe(res=>{
      if(res.IsError){
        alert(res.ErrorMessage);
        return;
      }
    })
  }

}
