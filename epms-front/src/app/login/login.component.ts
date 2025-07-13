import {Component, OnInit} from '@angular/core';
import {UserContextService} from '../Services/user-context.service';
import {UsersService} from '../Services/Users/users.service';
import {window} from 'rxjs/operators';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
  providers: [UsersService]
})
export class LoginComponent implements OnInit {

  constructor(private user: UserContextService,
              private usersService: UsersService) {
  }

  ngOnInit() {
  }

  login(Username, Password) {
    this.usersService.ValidUser(Username, Password).subscribe(
      res => {
        if (res.IsError) {
          alert(res.ErrorMessage);
          return;
        }
        this.user.isLoading = false;

        if (res.Data == "[]") {
          this.user.isLoggedIn = false;
          alert('Invalid username or password'); // show alert invalid username or password
          return;
        } else {

          let resJson = JSON.parse(res.Data);
          console.log('login res.Data', resJson);
          this.user.isLoggedIn = true;
          this.user.CompanyID = 1;
          this.user.Username = Username;
          this.user.fullname = resJson[0].fullname;
          this.user.UnitId = resJson[0].unitId;
          this.user.Role = resJson[0].Role;
          this.user.RoleId = resJson[0].RoleId;
          this.user.Position = resJson[0].Position;
          this.user.Unit = resJson[0].Unit;
          this.user.BranchID = 1;
          this.user.language = 'en';
          console.log("this.user", resJson);
        }

        let userContext = [
          {'Username': this.user.Username},
          {'isLoggedIn': this.user.isLoggedIn},
          {'CompanyID': this.user.CompanyID},
          {'BranchID': this.user.BranchID},
          {'fullname': this.user.fullname},
          {'UnitId': this.user.UnitId},
          {'Role': this.user.Role},
          {'RoleId': this.user.RoleId},
          {'Position': this.user.Position},
          {'Unit': this.user.Unit},
        ];

        sessionStorage.setItem('userContext', JSON.stringify(userContext));

        localStorage.setItem('userLang', this.user.language);
        globalThis.location.reload();

      }
    );

  }


}
