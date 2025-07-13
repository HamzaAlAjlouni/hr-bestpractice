import {Injectable} from '@angular/core';
import {Config} from '../Config';
import {HttpClient} from '../../../node_modules/@angular/common/http';

@Injectable({
  providedIn: 'root'

})
export class ProjectEvaluationValuesService {

  constructor(private http: HttpClient) {
  }

  GetProjectEvaluationValues(id, name) {
    return this.http.get<any>(Config.WebApiUrl + "ProjectEvaluationValues/GetProjectEvaluationValues", {
      params:
        {
          id,
          name,
        }
    });
  }

  getByID(id) {
    return this.http.get<any>(Config.WebApiUrl + "ProjectEvaluationValues/getByID", {
      params:
        {
          ID: id,
        }
    });
  }

  SaveProjectEvaluationValue(createdBy, name, id, Weight) {
    return this.http.get<any>(Config.WebApiUrl + "ProjectEvaluationValues/Save", {
      params:
        {
          CreatedBy: createdBy,
          Name: name,
          id,
          Weight
        }
    });
  }

  UpdateProjectEvaluationValue(id, modifiedBy, name, Weight) {
    return this.http.get<any>(Config.WebApiUrl + "ProjectEvaluationValues/Update", {
      params:
        {
          ID: id,
          ModifiedBy: modifiedBy,
          Name: name,
          Weight
        }
    });

  }

  DeleteProjectEvaluationValues(id) {
    return this.http.get<any>(Config.WebApiUrl + "ProjectEvaluationValues/Delete", {
      params:
        {
          ID: id
        }
    });

  }
}

