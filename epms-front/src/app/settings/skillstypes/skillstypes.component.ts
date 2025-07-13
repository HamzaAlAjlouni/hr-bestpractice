import {Component, OnInit, ViewChild} from "@angular/core";
import {SettingsService} from "../settings.service";
import {UsersService} from "../../Services/Users/users.service";
import {UserContextService} from "../../Services/user-context.service";

@Component({
  selector: "app-skillstypes",
  templateUrl: "./skillstypes.component.html",
  styleUrls: ["./skillstypes.component.css"],
  providers: [SettingsService, UsersService],
})
export class SkillstypesComponent implements OnInit {
  @ViewChild("gvSkills", {read: false, static: false}) gvSkills;

  showSkillEntry = false;
  txtSkillCode = "01";
  txtSkillName;
  skillsList;
  updateMode = false;
  selectID;
  PageResources = [];
  modificationPermission = false;

  constructor(
    private settingsService: SettingsService,
    private userService: UsersService,
    private user: UserContextService
  ) {
    this.modificationPermission = this.user.RoleId != 5;

    this.performSettings();
  }

  ngOnInit() {
  }

  GetLocalResourceObject(resourceKey) {
    for (let i = 0; i < this.PageResources.length; i++) {
      if (this.PageResources[i].resource_key === resourceKey) {
        return this.PageResources[i].resource_value;
      }
    }
  }

  performSettings() {
    this.userService
      .GetLocalResources(
        window.location.hash,
        this.user.CompanyID,
        this.user.language
      )
      .subscribe((res) => {
        if (res.IsError) {
          alert(res.ErrorMessage);
          return;
        }
        this.PageResources = res.Data;
      });

    this.txtSkillCode = "01";
    this.txtSkillName = "";
    this.updateMode = false;
    this.selectID = null;
    this.LoadSkills();
  }

  AddMode() {
    this.txtSkillCode = "01";
    this.txtSkillName = "";
    this.updateMode = false;
    this.selectID = null;

    this.showSkillEntry = true;
  }

  SaveSettings() {
    if (!this.updateMode) {
      this.settingsService
        .SaveSettings(this.txtSkillCode, this.txtSkillName, this.user.language)
        .subscribe((res) => {
          if (res.IsError) {
            alert(res.ErrorMessage);
            return;
          }
          alert("0");
          this.performSettings();
        });
    } else {
      this.settingsService
        .UpdateSettings(
          this.selectID,
          this.txtSkillCode,
          this.txtSkillName,
          this.user.language
        )
        .subscribe((res) => {
          if (res.IsError) {
            alert(res.ErrorMessage);
            return;
          }
          alert("0");
          this.performSettings();
        });
    }

    this.showSkillEntry = false;
  }

  LoadSkills() {
    this.settingsService
      .LoadAllSkillsTypes(this.user.language)
      .subscribe((res) => {
        if (res.IsError) {
          alert(res.ErrorMessage);
          return;
        }

        this.skillsList = res.Data;

        let cols = [
          {
            HeaderText: this.GetLocalResourceObject("lblSkillsName"),
            DataField: "name",
          },
        ];
        let actions = [
          {
            title: this.GetLocalResourceObject("lblEdit"),
            DataValue: "id",
            Icon_Awesome: "fa fa-edit",
            Action: "edit",
          },
          {
            title: this.GetLocalResourceObject("lblDelete"),
            DataValue: "id",
            Icon_Awesome: "fa fa-trash",
            Action: "delete",
          },
        ];

        this.gvSkills.bind(cols, res.Data, "gvSkills", this.modificationPermission?  actions:[]);
      });
  }

  gridEvent(event) {
    if (event[1] == "edit") {
      this.getSkill(event[0]);
      this.showSkillEntry = true;
    } else if (event[1] == "delete") {
      if (confirm(this.GetLocalResourceObject("AreYouSure"))) {
        this.deleteSkill(event[0]);
        this.showSkillEntry = false;
      }
    }
  }

  getSkill(id) {
    this.settingsService
      .getSkillbyID(id, this.user.language)
      .subscribe((res) => {
        if (res.IsError) {
          alert(res.ErrorMessage);
          return;
        }
        this.txtSkillCode = res.Data.code;
        this.txtSkillName = res.Data.name;
        this.selectID = res.Data.id;
        this.updateMode = true;
      });
  }

  deleteSkill(id) {
    this.settingsService.DeleteSkillByID(id).subscribe((res) => {
      if (res.IsError) {
        alert(res.ErrorMessage);
        return;
      }
      // alert("Skills Type Deleted.");
      alert(this.GetLocalResourceObject("MsgSkillsTypeDeleted"));

      this.performSettings();
    });
  }
}
