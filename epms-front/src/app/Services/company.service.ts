import {Injectable} from '@angular/core';
import {Config} from '../Config';
import {HttpClient} from '../../../node_modules/@angular/common/http';
import {identifierModuleUrl} from '@angular/compiler';

@Injectable({
  providedIn: 'root'
})
export class CompanyService {

  constructor(private http: HttpClient) {
  }

  LoadCompanyByID(id) {
    return this.http.get<any>(Config.WebApiUrl + "Company/getByID", {
      params:
        {
          ID: id
        }
    });
  }

  UpdateCompany(
    ID,
    Code,
    ModifiedBy,
    PlansLink,
    CurrencyCode,
    ProjectsLink,
    Name,
    Name2,
    Address,
    Email,
    Fax,
    Mission,
    Phone1,
    Phone2,
    Vision,
    Website,
    company_value,
    Country,
    City,
    PostalCode,
    LogoUrl,
    ObjectivePerformanceFactor,
    CompetencyPerformanceFactor
  ) {


    const Data = {
      ID,
      Code,
      ModifiedBy,
      PlansLink,
      CurrencyCode,
      ProjectsLink,
      Name,
      Name2,
      Address,
      Email,
      Fax,
      Mission,
      Phone1,
      Phone2,
      Vision,
      Website,
      company_values: company_value,
      Country,
      City,
      PostalCode,
      LogoUrl,
      ObjectiveFactor: ObjectivePerformanceFactor,
      CompetencyFactor: CompetencyPerformanceFactor


    };

    const form = new FormData();
    form.append("CompData", JSON.stringify(Data));


    return this.http.post<any>(Config.WebApiUrl + "Company/Update", form);

  }

}
