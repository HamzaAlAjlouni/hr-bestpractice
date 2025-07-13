import { Component, OnInit, ViewChild } from "@angular/core";
import { UserContextService } from "./../Services/user-context.service";
import { YearService } from "./service/year.service";

@Component({
  selector: "app-year-setup",
  templateUrl: "./year-setup.component.html",
  styleUrls: ["./year-setup.component.css"],
})
export class YearSetupComponent implements OnInit {
  addYearEntry: boolean = false;
  showYearEntry: boolean = false;
  yearValue: number;
  @ViewChild("gvYear", { read: false, static: false }) gvYear;

  constructor(
    private userContextService: UserContextService,
    private yearService: YearService
  ) {}

  ngOnInit() {
    this.loadYear();
  }

  yearAddMode() {
    this.addYearEntry = true;
    this.showYearEntry = false;
  }

  gvYearEvent(event) {
    if (event[1] == "delete") {
      if (confirm("Are You Sure")) {
        this.yearService.deleteYear(event[0]).subscribe(() => {
          this.loadYear();
        });
      }
    }
  }

  loadYear() {
    this.yearService.getYear().subscribe((res) => {

      if (res.IsError) {
        alert(res.ErrorMessage);
        return;
      }

      let cols = [
        {
          HeaderText: "Name",
          DataField: "year",
        },
      ];
      let actions = [
        {
          title: "Delete",
          DataValue: "id",
          Icon_Awesome: "fa fa-trash",
          Action: "delete",
        },
      ];

      this.gvYear.bind(cols, res.Data, "gvGrade", actions);
    });
  }

  addYear() {
    this.yearService
      .addYear(this.yearValue, this.userContextService.Username)
      .subscribe((res) => {
        this.loadYear();
        this.yearValue = null;
      });
  }
}
