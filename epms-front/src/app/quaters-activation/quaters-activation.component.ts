import {Component, Input, OnInit, ViewChild} from '@angular/core';
import {UserContextService} from "../Services/user-context.service";
import {QuartersService} from "./service/quarters.service";

@Component({
  selector: 'app-quaters-activation',
  templateUrl: './quaters-activation.component.html',
  styleUrls: ['./quaters-activation.component.css']
})
export class QuatersActivationComponent implements OnInit {


  @ViewChild("gvQuarter", {read: false, static: false}) gvQuarter;
  editQuarterActivation: boolean = false;

  quarterActivationValue: number;
  quarterID: number;
  QuartersList: any[];

  constructor(private userContextService: UserContextService, private service: QuartersService) {
  }

  ngOnInit() {
    this.getList();
  }

  getList() {

    this.service.getQuarters().subscribe(res => {

      if (res.IsError) {
        alert(res.ErrorMessage + ',' + 1);
        return;
      }


      let cols = [
        {HeaderText: "Name", DataField: 'Name'},
        {HeaderText: "Status", DataField: 'StatusValue'},

      ];
      let actions = [
        {
          "title": 'Edit',
          "DataValue": "ID",
          "Icon_Awesome": "fa fa-edit",
          "Action": "edit"
        }
      ];

      this.QuartersList = res.Data;
      this.gvQuarter.bind(cols, res.Data, 'gvEmployeeEducations', actions);


    });

  }

  gvQuartersEvent(event) {

    if (event[1] == "edit") {
      this.quarterActivationValue = this.QuartersList.filter(a => {
        return a.ID == Number(event[0]);
      })[0].Status;
      this.quarterID = Number(event[0]);
      this.editQuarterActivation = true;
    }
  }

  editquarterActivation() {

    this.service.updateQuarterActivation(this.quarterID, this.quarterActivationValue, this.userContextService.Username).subscribe(res => {

      if (res.IsError) {
        alert(res.ErrorMessage + ',' + 0);
        return;
      }
      this.editQuarterActivation = false;
      this.getList();

    });

  }


}
