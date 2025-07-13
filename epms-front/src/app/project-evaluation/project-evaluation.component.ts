import {Component, Input, OnInit, ViewChild} from '@angular/core';
import {ProjectEvaluationService} from "../Services/project-evaluation.service";
import {UserContextService} from "../Services/user-context.service";
import {ProjectEvaluationValuesService} from "../Services/project-evaluation-values.service";

@Component({
  selector: 'app-project-evaluation',
  templateUrl: './project-evaluation.component.html',
  styleUrls: ['./project-evaluation.component.css']
})
export class ProjectEvaluationComponent implements OnInit {
  @ViewChild("gvProjectEvaluation", {read: false, static: false}) gvProjectEvaluation;
  @ViewChild("gvProjectEvaluationDetails", {read: false, static: false}) gvProjectEvaluationDetails;
  AddOnFly = false;
  @Input()
  txtProjectEvaluationName: string;
  @Input()
  projectEvaluationWeight: number;
  txtProjectEvaluationValueName: string;
  @Input()
  projectEvaluationValueWeight: number;
  showAddEvaluation = false;
  showEditEvaluation = false;
  showDetailsEvaluation = false;
  showAddEvaluationValue = false;
  showEditEvaluationValue = false;
  projectEvaluationList;
  projectEvaluationValuesList;
  currentProjectEvaluation;
  currentProjectEvaluationValue;


  constructor(
    private projectEvaluationService: ProjectEvaluationService,
    private projectEvaluationValuesService: ProjectEvaluationValuesService,
    private userContextService: UserContextService) {
  }

  getEvalutions(name) {
    this.projectEvaluationService.GetProjectEvaluation(name, 1).subscribe(res => {
      if (res.IsError) {
        alert(res.ErrorMessage);
        return null;
      }
      this.projectEvaluationList = res.Data;
      let cols = [
        {'HeaderText': "Name", 'DataField': 'Name'},
        {'HeaderText': "Weight", 'DataField': 'Weight'}
      ];
      let actions = [
        {
          title: "Edit",
          DataValue: "ID",
          Icon_Awesome: "fa fa-edit",
          Action: "edit"
        },
        {
          title: "Delete",
          DataValue: "ID",
          Icon_Awesome: "fa fa-trash",
          Action: "delete"
        },
        {
          title: "Details",
          DataValue: "ID",
          Icon_Awesome: "fa fa-list-alt",
          Action: "details",
        },
      ];

      this.gvProjectEvaluation.bind(cols, res.Data, 'gvProjectEvaluation', actions);

    });
  }

  getEvaluationValues(name, id) {

    this.projectEvaluationValuesService.GetProjectEvaluationValues(id, name).subscribe(res => {
      if (res.IsError) {
        alert(res.ErrorMessage);
        return null;
      }
      this.projectEvaluationValuesList = res.Data;
      let cols = [
        {'HeaderText': "Name", 'DataField': 'Name'},
        {'HeaderText': "Weight", 'DataField': 'Weight'}
      ];
      let actions = [
        {
          title: "Edit",
          DataValue: "ID",
          Icon_Awesome: "fa fa-edit",
          Action: "edit"
        },
        {
          title: "Delete",
          DataValue: "ID",
          Icon_Awesome: "fa fa-trash",
          Action: "delete"
        },

      ];

      this.gvProjectEvaluationDetails.bind(cols, res.Data, 'gvProjectEvaluationDetails', actions);

    });


  }

  ngOnInit() {
    this.getEvalutions("");
  }

  gvProjectEvaluationEvent(event) {
    console.log(event);
    console.log(this.projectEvaluationList);
    if (event[1] == "edit") {
      this.currentProjectEvaluation = this.projectEvaluationList.filter(a => {
        return a.ID == event[0];
      })[0];
      console.log("currentProjectEvaluation", this.currentProjectEvaluation);
      this.showEditEvaluation = true;
      this.showAddEvaluation = false;
      this.showDetailsEvaluation = false;
      this.txtProjectEvaluationName = this.currentProjectEvaluation.Name;
      this.projectEvaluationWeight = this.currentProjectEvaluation.Weight;
    } else if (event[1] == "delete") {
      if (confirm("Are You Sure")) {
        this.projectEvaluationService.DeleteProjectEvaluation
        (event[0])
          .subscribe(() => this.getEvalutions(""));
      }
    } else if (event[1] == "details") {
      this.currentProjectEvaluation = this.projectEvaluationList.filter(a => {
        return a.ID == event[0];
      })[0];
      this.showProjectEvaluationDetails();
      this.showAddEvaluation = false;
      this.showEditEvaluation = false;
    }
  }

  gvSubProjectEvaluationEvent(event) {
    if (event[1] == "edit") {
      this.currentProjectEvaluationValue = this.projectEvaluationValuesList.filter(a => {
        return a.ID == event[0];
      })[0];
      console.log("currentProjectEvaluationValue", this.currentProjectEvaluationValue);
      this.showEditEvaluationValue = true;
      this.showAddEvaluationValue = false;
      this.txtProjectEvaluationValueName = this.currentProjectEvaluationValue.Name;
      this.projectEvaluationValueWeight = this.currentProjectEvaluationValue.Weight;
    } else if (event[1] == "delete") {
      if (confirm("Are You Sure")) {
        this.projectEvaluationValuesService.DeleteProjectEvaluationValues
        (event[0])
          .subscribe(() => this.getEvaluationValues("", this.currentProjectEvaluationValue.ID));
      }
    }

  }


  SaveProjectEvaluation() {
    console.log(this.txtProjectEvaluationName);
// check weight value
    let weightSum = this.projectEvaluationList.map(a => {
      return a.Weight;
    }).reduce((partialSum, a) => partialSum + a, 0);

    if (weightSum + Number(this.projectEvaluationWeight) > 100) {
      alert("The weight of evaluations more then 100%");
      return;
    }
    this.projectEvaluationService.SaveProjectEvaluation(this.userContextService.Username, this.txtProjectEvaluationName, this.projectEvaluationWeight, 1).subscribe(
      res => {

        if (res.IsError) {
          alert(res.ErrorMessage);
          return;
        }
        this.getEvalutions("");
        this.showAddEvaluation = false;

      }
    );

  }

  SaveProjectEvaluationValue() {
    console.log(this.txtProjectEvaluationValueName);
    let weightSum = this.projectEvaluationValuesList.map(a => {
      return a.Weight;
    }).reduce((partialSum, a) => partialSum + a, 0);
    console.log(weightSum + Number(this.projectEvaluationValueWeight));
    if (weightSum + Number(this.projectEvaluationValueWeight) > 100) {
      alert("The weight of evaluations Values more then 100%");
      return;
    }
    this.projectEvaluationValuesService.SaveProjectEvaluationValue(this.userContextService.Username, this.txtProjectEvaluationValueName, this.currentProjectEvaluation.ID, this.projectEvaluationValueWeight).subscribe(
      res => {
        if (res.IsError) {
          alert(res.ErrorMessage);
          return;
        }
        this.getEvaluationValues("", this.currentProjectEvaluation.ID);
        this.showAddEvaluationValue = false;

      }
    );

  }

  UpdateProjectEvaluation() {


    console.log(this.currentProjectEvaluation);
    console.log("this.projectEvaluationValueWeight", this.projectEvaluationWeight);
// check weight value
    let weightSum = this.projectEvaluationList.map(a => {

      return a.ID != this.currentProjectEvaluation.ID ? a.Weight : 0;
    }).reduce((partialSum, a) => partialSum + a, 0);
    console.log(weightSum + Number(this.projectEvaluationWeight));
    if (weightSum + Number(this.projectEvaluationWeight) > 100) {
      alert("The weight of evaluations more then 100%");
      return;
    }

    this.projectEvaluationService.UpdateProjectEvaluation(this.currentProjectEvaluation.ID, this.userContextService.Username, this.txtProjectEvaluationName, this.projectEvaluationWeight).subscribe(
      res => {

        if (res.IsError) {
          alert(res.ErrorMessage);
          return;
        }
        this.showEditEvaluation = false;
        this.getEvalutions("");

      }
    );

  }

  UpdateProjectEvaluationValue() {


    console.log(this.currentProjectEvaluationValue);
    let weightSum = this.projectEvaluationValuesList.map(a => {
      return a.ID != this.currentProjectEvaluationValue.ID ? a.Weight : 0;
    }).reduce((partialSum, a) => partialSum + a, 0);

    if (weightSum + Number(this.projectEvaluationValueWeight) > 100) {
      alert("The weight of evaluations more then 100%");
      return;
    }
    this.projectEvaluationValuesService.UpdateProjectEvaluationValue(this.currentProjectEvaluationValue.ID, this.userContextService.Username, this.txtProjectEvaluationValueName, this.projectEvaluationValueWeight).subscribe(
      res => {

        if (res.IsError) {
          alert(res.ErrorMessage);
          return;
        }
        this.showEditEvaluationValue = false;
        this.getEvaluationValues("", this.currentProjectEvaluation.ID);

      }
    );

  }

  showProjectEvaluationDetails() {
    //get sub evaluation list
    this.getEvaluationValues("", this.currentProjectEvaluation.ID);
    this.showDetailsEvaluation = true;
    this.showEditEvaluation = false;
    this.showAddEvaluation = false;

  }

  AddMode() {
    this.txtProjectEvaluationName = "";
    this.showAddEvaluation = true;
    this.showEditEvaluation = false;
  }

  AddSubMode() {
    this.showAddEvaluationValue = true;
    this.showEditEvaluationValue = false;
  }


}
