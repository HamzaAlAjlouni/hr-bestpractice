import {Injectable} from '@angular/core';
import {Config} from '../Config';
import {HttpClient} from '../../../node_modules/@angular/common/http';

@Injectable({
  providedIn: 'root'

})
export class ProjectEvaluationService {

  constructor(private http: HttpClient) {
  }

  GetProjectEvaluation(name, type) {
    return this.http.get<any>(Config.WebApiUrl + "ProjectEvaluation/GetProjectEvaluation", {
      params:
        {
          name,
          type
        }
    });
  }

  getByID(id) {
    return this.http.get<any>(Config.WebApiUrl + "ProjectEvaluation/getByID", {
      params:
        {
          ID: id,
        }
    });
  }

  SaveProjectEvaluation(createdBy, name, weight, type) {
    return this.http.get<any>(Config.WebApiUrl + "ProjectEvaluation/Save", {
      params:
        {
          CreatedBy: createdBy,
          Name: name,
          Weight: weight,
          type
        }
    });
  }

  UpdateProjectEvaluation(id, modifiedBy, name, weight) {
    return this.http.get<any>(Config.WebApiUrl + "ProjectEvaluation/Update", {
      params:
        {
          ID: id,
          ModifiedBy: modifiedBy,
          Name: name,
          Weight: weight
        }
    });

  }

  DeleteProjectEvaluation(id) {
    return this.http.get<any>(Config.WebApiUrl + "ProjectEvaluation/Delete", {
      params:
        {
          ID: id
        }
    });

  }
}

