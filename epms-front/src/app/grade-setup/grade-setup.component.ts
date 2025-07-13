import {Component, OnInit, ViewChild} from "@angular/core";
import {GradeService} from "./grade-service/grade.service";
import {UserContextService} from "./../Services/user-context.service";

@Component({
  selector: "app-grade-setup",
  templateUrl: "./grade-setup.component.html",
  styleUrls: ["./grade-setup.component.css"],
})
export class GradeSetupComponent implements OnInit {
  @ViewChild("gvGrade", {read: false, static: false}) gvGrade;
  showGradeEntry: boolean = false;
  addGradeEntry: boolean = false;
  gradeName: string = "";
  scaleCode: string = "";
  scaleNumber: number;

  gradeSelected: any;
  modificationPermission = false;

  constructor(
    private gradeService: GradeService,
    private userContextService: UserContextService
  ) {
    this.modificationPermission = this.userContextService.RoleId != 5;


  }

  ngOnInit() {
    this.loadGrade();
  }

  gradeAddMode() {
    this.addGradeEntry = true;
    this.showGradeEntry = false;
  }

  gvGradeEvent(event) {
    if (event[1] == "edit") {
      this.addGradeEntry = false;
      this.gradeService
        .getGradeById(event[0], this.userContextService.language)
        .subscribe((res) => {
          this.gradeSelected = res.Data;
          this.gradeName = res.Data.NAME;
          this.scaleCode = res.Data.SCALE_CODE;
          this.scaleNumber = res.Data.SCALE_NUMBER;
        });
      this.showGradeEntry = true;
    } else if (event[1] == "delete") {
      if (confirm("Are You Sure")) {
        this.gradeService
          .deleteGrade(event[0])
          .subscribe(() => this.loadGrade());
      }
    }
  }

  loadGrade() {
    this.gradeService
      .getGrade(this.userContextService.CompanyID)
      .subscribe((res) => {

        if (res.IsError) {
          alert(res.ErrorMessage);
          return;
        }

        let cols = [
          {
            HeaderText: "Name",
            DataField: "NAME",
          },
          {
            HeaderText: "Scale Code",
            DataField: "SCALE_CODE",
          },
          {
            HeaderText: "Scale Number",
            DataField: "SCALE_NUMBER",
          },
        ];
        let actions = [
          {
            title: "Edit",
            DataValue: "ID",
            Icon_Awesome: "fa fa-edit",
            Action: "edit",
          },
          {
            title: "Delete",
            DataValue: "ID",
            Icon_Awesome: "fa fa-trash",
            Action: "delete",
          },
        ];

        this.gvGrade.bind(cols, res.Data, "gvGrade", this.modificationPermission ? actions : []);
      });
  }

  editGrade() {

    this.gradeService
      .updateGrade(
        this.gradeSelected.ID,
        this.scaleNumber,
        this.gradeSelected.COMPANY_ID,
        this.gradeSelected.ModifiedBy,
        this.gradeName,
        this.scaleCode,
        this.userContextService.language
      )
      .subscribe((res) => {
        if (res.IsError) {
          alert(res.ErrorMessage);
        } else {
          this.loadGrade();
          this.showGradeEntry = false;
          this.gradeName = "";
        }
      });
  }

  addGrade() {
    this.gradeService
      .addGrade(
        this.scaleNumber,
        this.userContextService.CompanyID,
        this.userContextService.Username,
        this.gradeName,
        this.scaleCode,
        this.userContextService.language
      )
      .subscribe((res) => {
        if (res.IsError) {
          alert(res.ErrorMessage);
        } else {
          this.loadGrade();
          this.addGradeEntry = false;
          this.gradeName = "";
          this.scaleCode = "";
          this.scaleNumber = null;
        }
      });
  }
}
