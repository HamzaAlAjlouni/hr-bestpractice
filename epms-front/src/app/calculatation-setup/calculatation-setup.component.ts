import {Component, OnInit} from '@angular/core';
import {UserContextService} from "../Services/user-context.service";
import {UsersService} from "../Services/Users/users.service";
import {ProjectCalculationSetupService} from "../Services/project-calculation-setup-entity.service";

@Component({
  selector: 'app-calculatation-setup',
  templateUrl: './calculatation-setup.component.html',
  styleUrls: ['./calculatation-setup.component.css']
})
export class CalculatationSetupComponent implements OnInit {

  constructor(
    private userContextService: UserContextService, private userService: UsersService,
    private projectCalculationSetupService: ProjectCalculationSetupService
  ) {
    this.userService.GetLocalResources(window.location.hash, this.userContextService.CompanyID, this.userContextService.language)
      .subscribe(res => {
        if (res.IsError) {
          alert(' ' + res.ErrorMessage);
          return;
        }
        this.PageResources = res.Data;
      });
  }

  optionValue = '';
  hasProjects = false;

  PageResources = [];

  ngOnInit() {
    this.getOptionValue();
  }

  GetLocalResourceObject(resourceKey) {
    for (const item of this.PageResources) {
      if (item.resource_key === resourceKey) {
        return item.resource_value;
      }
    }
  }

  getOptionValue() {
    this.projectCalculationSetupService.get().subscribe(res => {


      if (res.IsError) {
        alert('' + res.ErrorMessage);
        return;
      } else {
        console.log(res.Data);
        console.log(res.Data[0].Calculation);
        this.optionValue = res.Data[0].Calculation.toString();
        this.hasProjects = res.Data[0].hasProjects;

      }

    });


  }

  updateOption() {

    this.projectCalculationSetupService.update(this.optionValue).subscribe(res => {
      if (res.IsError) {
        alert('' + res.ErrorMessage);
        return;
      } else {
        console.log(res.Data);
      }

    });
    console.log('kpi', this.optionValue);
    console.log('so', this.optionValue);
  }

}
