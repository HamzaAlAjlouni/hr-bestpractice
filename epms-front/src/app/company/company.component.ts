import { Component, OnInit, ViewChild } from '@angular/core';

import { CodesService } from '../Services/codes.service';
import { UserContextService } from '../Services/user-context.service';
import { UsersService } from '../Services/Users/users.service';
import { CompanyService } from '../Services/company.service';
import { Router } from '@angular/router';



@Component({
  selector: 'app-company',
  templateUrl: './company.component.html',
  styleUrls: ['./company.component.css']
  , providers: [UsersService]
})
export class CompanyComponent implements OnInit {

  txtVision;
  txtMission;
  txtValues;
  currencyList;
  txtCompanyNameEntry;
  txtCompanyAddressEntry;
  txtEMAILEntry;
  txtWebSiteEntry;
  txtCompanyFaxEntry;
  txtCompanyPHONE1Entry;
  txtCompanyPHONE2Entry;
  ddlCurrencyCode;
  txtCompanyName2;
  txtCompanyCode;
  txtObjectivePerformanceFactor;
  txtCompetencyPerformanceFactor;
  txtCountry;
  txtCity;
  txtPostalCode;
  txtLogoUrl;

  PlansLink;
  ProjectsLink;
  rdSetPlanEmployee;
  rdSetPlan;
  rdProjectLinkKPI;
  rdProjectLinkPlan;

  PageResources = [];
  GetLocalResourceObject(resourceKey) {
    for (let i = 0; i < this.PageResources.length; i++) {
      if (this.PageResources[i].resource_key === resourceKey) {
        return this.PageResources[i].resource_value;
      }
    }
  }
  constructor(private codesService: CodesService,
    private userContextService: UserContextService
    , private userService: UsersService
    , private companyService: CompanyService
    , private _router: Router) {

    this.getCurrencyList();
    this.LoadCompanyByID(this.userContextService.CompanyID);
    this.userService.GetLocalResources(window.location.hash, this.userContextService.CompanyID, this.userContextService.language).subscribe(res => {
      if (res.IsError) {
        alert('1111111'+res.ErrorMessage);
        return;
      }
      this.PageResources = res.Data;
    });
  }

  ngOnInit() {

  }

  LoadCompanyByID(ID) {
    this.companyService.LoadCompanyByID(ID).subscribe(res => {
      if (res.IsError) {
        alert('222222'+res.ErrorMessage);
        return;
      }

      this.txtCompanyAddressEntry = res.Data.Address;
      this.txtCountry = res.Data.Country;
      this.txtCity = res.Data.City;
      this.txtPostalCode = res.Data.PostalCode;
      this.txtLogoUrl = res.Data.LogoUrl;
      this.txtCompanyFaxEntry = res.Data.Fax;
      this.txtCompanyNameEntry = res.Data.Name;
      this.txtCompanyPHONE1Entry = res.Data.Phone1;
      this.txtCompanyPHONE2Entry = res.Data.Phone2;
      this.txtEMAILEntry = res.Data.Email;
      this.txtWebSiteEntry = res.Data.Website;
      this.ddlCurrencyCode = res.Data.CurrencyCode;
      this.txtCompanyName2 = res.Data.name2;
      this.txtCompanyCode = res.Data.Code;
      this.txtValues = res.Data.company_values;
      this.txtVision = res.Data.Vision;
      this.txtMission = res.Data.Mission;
      this.PlansLink = res.Data.PlansLink;
      this.ProjectsLink = res.Data.ProjectsLink;
      this.txtObjectivePerformanceFactor = res.Data.ObjectiveFactor;
      this.txtCompetencyPerformanceFactor = res.Data.CompetencyFactor;
      if (this.PlansLink == 1) {
        this.rdSetPlanEmployee = "1";
        this.rdSetPlan = "0";
      } else {
        this.rdSetPlanEmployee = "0";
        this.rdSetPlan = "1";
      }

      if (this.ProjectsLink == 1) {
        this.rdProjectLinkKPI = "1";
        this.rdProjectLinkPlan = "0";
      } else {
        this.rdProjectLinkKPI = "0";
        this.rdProjectLinkPlan = "1";
      }


    });
  }
  onPlansLinkChange(seletcedPlansLink) {
    this.PlansLink = seletcedPlansLink;
  }
  onProjectsLinkChange(seletcedProjectsLink) {
    this.ProjectsLink = seletcedProjectsLink;
  }

  UpdateCompany() {
    this.companyService.UpdateCompany(
      this.userContextService.CompanyID,
      this.txtCompanyCode,
      this.userContextService.Username,
      this.PlansLink,
      this.ddlCurrencyCode,
      this.ProjectsLink,
      this.txtCompanyNameEntry,
      this.txtCompanyName2,
      this.txtCompanyAddressEntry,
      this.txtEMAILEntry,
      this.txtCompanyFaxEntry,
      this.txtMission,
      this.txtCompanyPHONE1Entry,
      this.txtCompanyPHONE2Entry,
      this.txtVision,
      this.txtWebSiteEntry,
      this.txtValues,
      this.txtCountry,
      this.txtCity,
      this.txtPostalCode,
      this.txtLogoUrl,
      this.txtObjectivePerformanceFactor,
      this.txtCompetencyPerformanceFactor
    ).subscribe(res => {
      if (res.IsError) {
        alert('33333'+res.ErrorMessage);
        return;
      }
    });

  }
  getCurrencyList() {
    this.codesService.LoadCodes(
      this.userContextService.CompanyID,
      10,
      this.userContextService.language
    ).subscribe(res => {
      if (res.IsError) {
        alert('444444'+res.ErrorMessage);
        return null;
      }
      this.currencyList = res.Data;
    })
  }
}
