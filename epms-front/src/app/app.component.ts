import { Component, HostListener } from "@angular/core";
import { UserContextService } from "./Services/user-context.service";
import { TranslateService } from "@ngx-translate/core";

@Component({
  selector: "app-root",
  templateUrl: "./app.component.html",
  styleUrls: ["./app.component.css"],
})
export class AppComponent {
  constructor(
    public user: UserContextService,
    private translate: TranslateService
  ) {
    translate.setDefaultLang("en");

    if (
      localStorage.getItem("userLang") != undefined ||
      localStorage.getItem("userLang") != ""
    ) {
      this.user.language = localStorage.getItem("userLang");
      this.useLanguage(this.user.language);
    } else {
      this.user.language = "en";
      this.useLanguage(this.user.language);
    }

    if (sessionStorage.getItem("userContext") != null) {
      // tslint:disable-next-line:prefer-const
      let userContext = JSON.parse(sessionStorage.getItem("userContext"));
      this.user.Username = userContext[0].Username;
      this.user.isLoggedIn = userContext[1].isLoggedIn;
      this.user.CompanyID = userContext[2].CompanyID;
      this.user.BranchID = userContext[3].BranchID;
    }
  }

  useLanguage(language: string): void {
    this.translate.use(language);
  }
}
