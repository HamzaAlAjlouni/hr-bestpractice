import { Component, OnInit, ViewChild } from '@angular/core';
import { SharedServiceService } from '../Services/shared-service.service';
import { UserContextService } from '../Services/user-context.service';
import { SettingsService } from '../settings/settings.service';  
import { CompanyService } from '../Services/company.service';
import { UsersService } from '../Services/Users/users.service';


@Component({
  selector: 'app-landing',
  templateUrl: './landing.component.html',
  styleUrls: ['./landing.component.css'],
  providers:[SettingsService , UsersService]  
})
export class LandingComponent implements OnInit {

  @ViewChild('DataGrid',{read:false,static:false}) DataGrid;

  txtVision;
  txtMission;
  PageResources=[];

  constructor(private user:UserContextService,
    private sharedService:SharedServiceService,
    private settingsService:SettingsService
    ,private companyService : CompanyService
    ,private userService : UsersService) {
    
      this.LoadCompanyByID(this.user.CompanyID);

      this.userService.GetLocalResources(window.location.hash,this.user.CompanyID,this.user.language).subscribe(res=>{
        if(res.IsError){
          alert(res.ErrorMessage);
          return;
        }
        this.PageResources = res.Data;
      });

  }
  GetLocalResourceObject(resourceKey){
    for(let i=0;i<this.PageResources.length;i++){
      if(this.PageResources[i].resource_key === resourceKey){
        return this.PageResources[i].resource_value;
      }
    }
  }
  ngOnInit() {
     
  }

  LoadCompanyByID(ID)
  {
  
    this.companyService.LoadCompanyByID(ID).subscribe(res => {
      if (res.IsError) {
        alert(res.ErrorMessage);
        return;
      }
 
      this.txtVision = res.Data.Vision;
      this.txtMission = res.Data.Mission;

      document.getElementById('divVision').innerHTML = this.txtVision;
      document.getElementById('divMission').innerHTML = this.txtMission;
    }); 
  }
   
 
}
