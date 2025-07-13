import { Component, OnInit, Pipe } from "@angular/core";
import { UserContextService } from "../Services/user-context.service";
import { DomSanitizer } from "@angular/platform-browser";
import { UsersService } from "../Services/Users/users.service";
import { AuthorizationService } from "../Services/Authorization/authorization.service";
import { isNgTemplate } from "../../../node_modules/@angular/compiler";

declare var $: any;
@Component({
  selector: "app-master",
  templateUrl: "./master.component.html",
  styleUrls: ["./master.component.css"],
  providers: [UsersService],
})
export class MasterComponent implements OnInit {
  @Pipe({ name: "safe" })
  customAr: any;
  PageResources = [];
  menuContent: string;
  menuLoaded = false;
  // tslint:disable-next-line:typedef-whitespace
  // tslint:disable-next-line:no-trailing-whitespace
  constructor(
    private authorizationService: AuthorizationService,
    public user: UserContextService,
    private sanitizer: DomSanitizer,
    private userService: UsersService
  ) {
    this.setLangStyle();
  }

  AuthorizedMenuList;
  currentYearLong(): number {
    return new Date().getFullYear();
  }
  LoadAuthorizedMenus() {
    this.authorizationService
      .LoadAuthorizedMenus(
        this.user.Username,
        this.user.CompanyID,
        "HRMS",
        "HOBJ"
      )
      .subscribe((res) => {
        if (res.IsError) {
          alert(res.ErrorMessage);
          return;
        }
        console.log('menu', res.Data)
        this.buildMenu(res.Data);
      });
  }
  SignOut() {
    this.user.isLoggedIn = false;
    sessionStorage.clear();
  }

  buildMenu(menu) {
    console.log('menu', menu);
    if (menu) {
      let sLeftMenu = "";
      const listOfModules = menu.filter(x => x.PARENT_ID == null);
      const listOfForms = menu.filter(x => x.PARENT_ID != null);
      let tempListOfForms;

      for (let i = 0; i < listOfModules.length; i++) {
        let MenuModuleName = this.user.language == "en" ? listOfModules[i].NAME : listOfModules[i].name2;

        sLeftMenu += '<li class="treeview">';
        sLeftMenu +=
          "<a>" +
          '<i class="fa ' + listOfModules[i].ICONE + '"></i><span>' +
          MenuModuleName +
          '</span><span class="pull-right-container"><i class="fa fa-angle-left pull-right"></i></span></a>' +
          '<ul class="treeview-menu">';

        tempListOfForms = listOfForms.filter(x => x.PARENT_ID == listOfModules[i].ID);

        for (let j = 0; j < tempListOfForms.length; j++) {
          let MenuItemName = this.user.language == "en" ? tempListOfForms[j].NAME : tempListOfForms[j].name2;
          let menuItem = '<li><a';

          if (!tempListOfForms[j].URL.includes('##')) {
            menuItem += ' href="' + tempListOfForms[j].URL + '"';
          }

          menuItem += ' class="nav-link" onclick="MenuChildClick(this)">' +
            '<i class="fa ' + tempListOfForms[j].ICONE + '"></i><span>' +
            MenuItemName + "</span></a></li>";

          sLeftMenu += menuItem;
        }

        sLeftMenu += "</ul></li>";
      }

      document.getElementById("leftMenu").innerHTML = sLeftMenu;
      this.menuLoaded = true;
    }
  }


  buildMenuold(menu: any[]) {
    console.log('menu',menu);
    if (menu) {
      var sLeftMenu = "";
      var listOfModules = menu.filter((x) => x.PARENT_ID == null);
      var listOfForms = menu.filter((x) => x.PARENT_ID != null);
      var tempListOfForms;
      for (let i = 0; i < listOfModules.length; i++) {
        let MenuModuleName: string = "";

        if (this.user.language == "en") {
          MenuModuleName = listOfModules[i].NAME;
        } else {
          MenuModuleName = listOfModules[i].name2;
        }
        sLeftMenu += '<li class="treeview">';
        sLeftMenu +=
          "<a>" +
          '<i class="fa ' +
          listOfModules[i].ICONE +
          '"></i><span>' +
          MenuModuleName +
          '</span><span class="pull-right-container"><i class="fa fa-angle-left pull-right"></i></span></a>' +
          '<ul class="treeview-menu">';
        tempListOfForms = listOfForms.filter(
          (x) => x.PARENT_ID == listOfModules[i].ID
        );
        for (let j = 0; j < tempListOfForms.length; j++) {
          let MenuItemName: string = "";
          if (this.user.language == "en") {
            MenuItemName = tempListOfForms[j].NAME;
          } else {
            MenuItemName = tempListOfForms[j].name2;
          }
          sLeftMenu +=
            '<li><a href="' +
            tempListOfForms[j].URL +
            "\" class='nav-link' onclick='MenuChildClick(this)'>" +
            '<i class="fa ' +
            tempListOfForms[j].ICONE +
            '"></i><span>' +
            MenuItemName +
            "</span>" +
            "</a></li>";
        }
        sLeftMenu += "</ul></li>";
      }

      document.getElementById("leftMenu").innerHTML = sLeftMenu;
      //this.menuContent = '<ul _ngcontent-hgr-c1="" class="sidebar-menu tree" data-widget="tree" id="leftMenu"><li class="treeview" style="height: auto;"><a href="#"><i class="fa fa-edit"></i><span>Setup</span><span class="pull-right-container"><i class="fa fa-angle-left pull-right"></i></span></a><ul class="treeview-menu" style="display: none;"><li><a href="#/skillsTypes"><i class="fa fa-circle-o"></i><span>Skills Types</span></a></li><li><a href="#/positions"><i class="fa fa-circle-o"></i><span>Positions</span></a></li><li><a href="#/competence"><i class="fa fa-circle-o"></i><span>Competencies</span></a></li><li><a href="#/Employees"><i class="fa fa-circle-o"></i><span>Employees</span></a></li><li><a href="#/Units"><i class="fa fa-circle-o"></i><span>Units</span></a></li></ul></li><li class="treeview"><a href="#"><i class="fa fa-edit"></i><span>Planning</span><span class="pull-right-container"><i class="fa fa-angle-left pull-right"></i></span></a><ul class="treeview-menu"><li><a href="#/stratigicobjectives"><i class="fa fa-circle-o"></i><span>Strategic Objectives</span></a></li><li><a href="#/Projects"><i class="fa fa-circle-o"></i><span>Projects</span></a></li><li><a href="#/stratigicobjectivesChart"><i class="fa fa-circle-o"></i><span>Projects Planner</span></a></li><li><a href="#/EmpStructure"><i class="fa fa-circle-o"></i><span>Employee Structure</span></a></li><li><a href="#/employeeObjectve"><i class="fa fa-circle-o"></i><span>Employee Performance Plan</span></a></li></ul></li><li class="treeview"><a href="#"><i class="fa fa-circle-o"></i><span>Operation Assessment</span><span class="pull-right-container"><i class="fa fa-angle-left pull-right"></i></span></a><ul class="treeview-menu"><li><a href="#/projectsAssessment"><i class="fa fa-circle-o"></i><span>Projects Assessment</span></a></li><li><a href="#/stratigicobjectivesChart"><i class="fa fa-circle-o"></i><span>Projects Navigation</span></a></li><li><a href="#/employeeAssessment"><i class="fa fa-circle-o"></i><span>Employees Performance Assessment</span></a></li><li><a href="#/EmpStructure"><i class="fa fa-circle-o"></i><span>Employee Navigation</span></a></li></ul></li><li class="treeview"><a href="#"><i class="fa fa-edit"></i><span>Dashboard Analysis</span><span class="pull-right-container"><i class="fa fa-angle-left pull-right"></i></span></a><ul class="treeview-menu"><li><a href="#/DashBoards"><i class="fa fa-circle-o"></i><span>Organization Dashboard</span></a></li><li><a href="#/EmpDashBoards"><i class="fa fa-circle-o"></i><span>Employee Dashboard</span></a></li></ul></li></ul>';

      this.menuLoaded = true;
      //document.getElementById('leftMenu').innerHTML = sLeftMenu;
    }
  }
  GetLocalResourceObject(resourceKey) {
    for (let i = 0; i < this.PageResources.length; i++) {
      if (this.PageResources[i].resource_key === resourceKey) {
        return this.PageResources[i].resource_value;
      }
    }
  }

  ngOnInit() {
    this.menuLoaded = false;
    this.userService
      .GetLocalResources("master", this.user.CompanyID, this.user.language)
      .subscribe((res) => {
        if (res.IsError) {
          alert(res.ErrorMessage);
          return;
        }
        this.PageResources = res.Data;
      });

    setTimeout(() => {
      this.menuLoaded = true;
      this.LoadAuthorizedMenus();
    }, 1000);
  }

  setLangStyle() {
    if (this.user.language == "en") {
      this.user.customAr = this.sanitizer.bypassSecurityTrustResourceUrl("");
    } else {
      this.user.customAr = this.sanitizer.bypassSecurityTrustResourceUrl(
        "assets/dist/css/custom-ar.css"
      );
    }
  }

  toggleLang() {
    setTimeout(this.setTimes, 2000);
  }

  setTimes = () => {
    if (this.user.language == "en") {
      this.user.language = "ar";
      localStorage.setItem("userLang", "ar");
    } else {
      this.user.language = "en";
      localStorage.setItem("userLang", "en");
    }
    window.location.reload();
  };
}
