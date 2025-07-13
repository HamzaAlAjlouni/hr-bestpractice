import {Injectable} from '@angular/core';
import {Config} from '../../Config';
import {HttpClient} from '../../../../node_modules/@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class UsersService {

  constructor(private http: HttpClient) {
  }

  ValidUser(Username, Password) {
    return this.http.get<any>(Config.WebApiUrl + 'Users/ValidUser', {
      params:
        {
          Username: Username,
          Password: Password
        }
    });
  }

  GetUsersList(companyId, searchKey) {
    return this.http.get<any>(Config.WebApiUrl + 'Users/GetUsersList', {
      params:
        {
          companyId,
          searchKey
        }
    });
  }

  resetAccountPassword(username, newPassword) {
    return this.http.get<any>(Config.WebApiUrl + 'Users/ResetAccountPassword', {
      params:
        {
          username,
          newPassword
        }
    });
  }

  GetLocalResources(url, compID, culture_name) {
    return this.http.get<any>(Config.WebApiUrl + 'Users/GetPageLocalResources', {
      params:
        {
          url: url,
          orgID: compID,
          culture_name: culture_name
        }
    });
  }

  GetSystemUsers(compID) {
    return this.http.get<any>(Config.WebApiUrl + 'Users/GetSystemUsers', {
      params:
        {
          companyID: compID,
        }
    });
  }

  GetMenuPages(compID) {
    return this.http.get<any>(Config.WebApiUrl + 'Users/GetMenuPages', {
      params:
        {
          companyID: compID,
        }
    });
  }

}
