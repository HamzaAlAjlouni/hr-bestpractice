import {Injectable} from '@angular/core';
import {Config} from '../Config';
import {HttpClient} from '../../../node_modules/@angular/common/http';
import {identifierModuleUrl} from '@angular/compiler';

@Injectable({
  providedIn: 'root'

})
export class PositionsService {

  constructor(private http: HttpClient) {
  }

  LoadPositions(companyID, positionName, lang, unitId = null) {
    return this.http.get<any>(Config.WebApiUrl + "Positions/GetPositions", {
      params:
        {
          Name: positionName,
          CompanyID: companyID,
          languageCode: lang,
          unitId,
        }
    });
  }

  LoadPositionByID(id, lang) {
    return this.http.get<any>(Config.WebApiUrl + "Positions/getByID", {
      params:
        {
          ID: id,
          languageCode: lang
        }
    });
  }

  SavePosition(code, companyID, createdBy, isManagment, name, lang) {
    return this.http.get<any>(Config.WebApiUrl + "Positions/Save", {
      params:
        {
          Code: code,
          CompanyID: companyID,
          CreatedBy: createdBy,
          IsManagment: isManagment,
          Name: name,
          languageCode: lang
        }
    });
  }

  UpdatePosition(id, code, modifiedBy, isManagment, name, lang) {
    return this.http.get<any>(Config.WebApiUrl + "Positions/Update", {
      params:
        {
          ID: id,
          Code: code,
          ModifiedBy: modifiedBy,
          IsManagment: isManagment,
          Name: name,
          languageCode: lang
        }
    });

  }

  DeletePosition(id) {
    return this.http.get<any>(Config.WebApiUrl + "Positions/Delete", {
      params:
        {
          ID: id
        }
    });

  }

  /********************/
  LoadCompetenceis(companyID, CompetenceName, LanguageCode, NatureId) {
    return this.http.get<any>(Config.WebApiUrl + "Competence/GetCompetences", {
      params:
        {
          CompanyID: companyID,
          CompetenceName: CompetenceName,
          LanguageCode: LanguageCode,
          natureId: NatureId
        }
    });
  }

  /*********************** */
  LoadCompetenceByID(id, LanguageCode) {
    return this.http.get<any>(Config.WebApiUrl + "Competence/getByID", {
      params:
        {
          ID: id,
          LanguageCode: LanguageCode
        }
    });
  }

  /*********************** */
  SaveCompetence(code, companyID, createdBy, natureID, name, name2, isMandetory, LanguageCode, notes) {
    return this.http.get<any>(Config.WebApiUrl + "Competence/Save", {
      params:
        {
          Code: code,
          CompanyID: companyID,
          CreatedBy: createdBy,
          NatureID: natureID,
          Name: name,
          Name2: name2,
          IsMandetory: isMandetory,
          LanguageCode: LanguageCode,
          notes: notes
        }
    });
  }

  UpdateCompetence(id, code, modifiedBy, natureID, name, name2, isMandetory, LanguageCode, notes) {
    return this.http.get<any>(Config.WebApiUrl + "Competence/Update", {
      params:
        {
          ID: id,
          Code: code,
          ModifiedBy: modifiedBy,
          NatureID: natureID,
          Name: name,
          Name2: name2,
          IsMandetory: isMandetory,
          LanguageCode: LanguageCode,
          notes: notes
        }
    });

  }

  DeleteCompetence(id) {
    return this.http.get<any>(Config.WebApiUrl + "Competence/Delete", {
      params:
        {
          ID: id
        }
    });
  }

  /********************/
  LoadJobDescription(positionID, lang) {
    return this.http.get<any>(Config.WebApiUrl + "PositionDescription/GetPositionDescription", {
      params:
        {
          PositionID: positionID,
          languageCode: lang
        }
    });
  }

  LoadJobDescriptionByEmployee(EmployeeID, lang) {
    return this.http.get<any>(Config.WebApiUrl + "PositionDescription/GetPositionDescriptionByEmployee", {
      params:
        {
          EmployeeID: EmployeeID,
          languageCode: lang
        }
    });
  }

  LoadJobDescriptionByID(id, lang) {
    return this.http.get<any>(Config.WebApiUrl + "PositionDescription/getByID", {
      params:
        {
          ID: id,
          languageCode: lang
        }
    });
  }

  SaveJobDescription(PositionID, createdBy, name, lang) {
    return this.http.get<any>(Config.WebApiUrl + "PositionDescription/Save", {
      params:
        {
          PositionID: PositionID,
          CreatedBy: createdBy,
          Name: name,
          languageCode: lang
        }
    });
  }

  UpdateJobDescription(id, modifiedBy, name, lang) {
    return this.http.get<any>(Config.WebApiUrl + "PositionDescription/Update", {
      params:
        {
          ID: id,
          ModifiedBy: modifiedBy,
          Name: name,
          languageCode: lang
        }
    });

  }

  DeleteJobDescription(id) {
    return this.http.get<any>(Config.WebApiUrl + "PositionDescription/Delete", {
      params:
        {
          ID: id
        }
    });
  }

  /********************/
  LoadCompetenceKpi(CompetenceID, LanguageCode) {
    return this.http.get<any>(Config.WebApiUrl + "CompetenceKpi/GetCompetencesKPI", {
      params:
        {
          CompetenceID: CompetenceID,
          LanguageCode: LanguageCode
        }
    });
  }

  LoadCompetenceKpiByLevel(CompetenceID, CompetenceLevel, LanguageCode) {
    return this.http.get<any>(Config.WebApiUrl + "CompetenceKpi/GetCompetencesKPIByLevel", {
      params:
        {
          CompetenceID: CompetenceID,
          CompetenceLevel: CompetenceLevel,
          LanguageCode: LanguageCode
        }
    });
  }

  LoadCompetenceKpiByID(id, LanguageCode) {
    return this.http.get<any>(Config.WebApiUrl + "CompetenceKpi/getByID", {
      params:
        {
          ID: id,
          LanguageCode: LanguageCode
        }
    });
  }

  SaveCompetenceKpi(CompetenceID, CreatedBy, KpiTypeID, Name, Name2, LanguageCode) {
    return this.http.get<any>(Config.WebApiUrl + "CompetenceKpi/Save", {
      params:
        {
          CompetenceID: CompetenceID,
          CreatedBy: CreatedBy,
          KpiTypeID: KpiTypeID,
          Name: Name,
          Name2: Name2,
          LanguageCode: LanguageCode
        }
    });
  }

  UpdateCompetenceKpi(ID, ModifiedBy, KpiTypeID, Name, Name2, LanguageCode) {
    return this.http.get<any>(Config.WebApiUrl + "CompetenceKpi/Update", {
      params:
        {
          ID: ID,
          ModifiedBy: ModifiedBy,
          KpiTypeID: KpiTypeID,
          Name: Name,
          Name2: Name2,
          LanguageCode: LanguageCode
        }
    });

  }

  DeleteCompetenceKpi(id) {
    return this.http.get<any>(Config.WebApiUrl + "CompetenceKpi/Delete", {
      params:
        {
          ID: id
        }
    });
  }

  /********************/
  LoadPositionDescription(PositionID) {
    return this.http.get<any>(Config.WebApiUrl + "PositionDescription/GetPositionDescription", {
      params:
        {
          PositionID: PositionID
        }
    });
  }

  LoadPositionDescriptionByID(id) {
    return this.http.get<any>(Config.WebApiUrl + "PositionDescription/getByID", {
      params:
        {
          ID: id
        }
    });
  }

  SavePositionDescription(PositionID, CreatedBy, Name, Name2) {
    return this.http.get<any>(Config.WebApiUrl + "PositionDescription/Save", {
      params:
        {
          PositionID: PositionID,
          CreatedBy: CreatedBy,
          Name: Name,
          Name2: Name2
        }
    });
  }

  UpdatePositionDescription(ID, ModifiedBy, Name, Name2) {
    return this.http.get<any>(Config.WebApiUrl + "PositionDescription/Update", {
      params:
        {
          ID: ID,
          ModifiedBy: ModifiedBy,
          Name: Name,
          Name2: Name2
        }
    });

  }

  DeletePositionDescription(id) {
    return this.http.get<any>(Config.WebApiUrl + "PositionDescription/Delete", {
      params:
        {
          ID: id
        }
    });
  }

  /********************/
  LoadPositionCompetencies(PositionID, lang, natureID) {
    return this.http.get<any>(Config.WebApiUrl + "PositionCompetencies/GetPositionCompetencies", {
      params:
        {
          PositionID: PositionID,
          LanguageCode: lang,
          natureID: natureID
        }
    });
  }

  LoadPositionCompetenciesByID(id) {
    return this.http.get<any>(Config.WebApiUrl + "PositionCompetencies/getByID", {
      params:
        {
          ID: id
        }
    });
  }

  SavePositionCompetencies(PositionID, CompetenceID, CompetenceLevelID) {
    return this.http.get<any>(Config.WebApiUrl + "PositionCompetencies/Save", {
      params:
        {
          PositionID: PositionID,
          CompetenceID: CompetenceID,
          competenceLevel: CompetenceLevelID
        }
    });
  }

  UpdatePositionCompetencies(ID, PositionID, CompetenceID, CompetenceLevelID) {
    return this.http.get<any>(Config.WebApiUrl + "PositionCompetencies/Update", {
      params:
        {
          ID: ID,
          PositionID: PositionID,
          CompetenceID: CompetenceID,
          competenceLevel: CompetenceLevelID
        }
    });

  }

  DeletePositionCompetencies(id, PositionID) {
    return this.http.get<any>(Config.WebApiUrl + "PositionCompetencies/Delete", {
      params:
        {
          ID: id,
          PositionID,
        }
    });
  }
}

