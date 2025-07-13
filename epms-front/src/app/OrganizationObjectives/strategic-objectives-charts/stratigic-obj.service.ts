import { Injectable } from '@angular/core';
import { HttpClient } from '../../../../node_modules/@angular/common/http';
import { UserContextService } from '../../Services/user-context.service';
import { Config } from '../../Config';

@Injectable({
  providedIn: 'root'
})
export class StratigicObjService {

  constructor(private http:HttpClient,private user:UserContextService) { }

  LoadStratigicObjectives(compid,Year){
    return this.http.get<any>(Config.WebApiUrl+"StratigicObjectivesChart/loadStratigicObjectivesByOrg",{
      params:{
        companyID:compid,
        year:Year
      }
    });
  }

   LoadYears(){ 
    return this.http.get<any>(Config.WebApiUrl+"Years/GetYears");
   }
 
   LoadOrganizations(compid,Year){
    return this.http.get<any>(Config.WebApiUrl+"StratigicObjectivesChart/LoadOrganizations",{
      params:{
        companyID:compid,
        year:Year
      }
    });
  }

  LoadObjectives(compid,Year){
    return this.http.get<any>(Config.WebApiUrl+"StratigicObjectivesChart/LoadObjectives",{
      params:{
        companyID:compid,
        year:Year
      }
    });
  }

  LoadProjects(compid,Year){
    return this.http.get<any>(Config.WebApiUrl+"StratigicObjectivesChart/LoadProjects",{
      params:{
        companyID:compid,
        year:Year
      }
    });
  } 
  
  LoadAll(companyId,Year,lang){
    return this.http.get<any>(Config.WebApiUrl+"StratigicObjectivesChart/LoadAll",{
      params:{
        companyId:companyId,
        year:Year,
        languageCode:lang
      }
    });
  }

}
