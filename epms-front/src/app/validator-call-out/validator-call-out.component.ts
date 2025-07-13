import { Component, OnInit, Input } from '@angular/core';
import { $ } from '../../../node_modules/protractor';

@Component({
  selector: 'ValidatorCallOut',
  template: `
      <table *ngIf="Visibiltiy" [id]="ID" cellpadding="0" cellspacing="0" border="0" width="200px"
             class="ajax__validatorcallout ajax__validatorcallout_popup_table"
             style="position: absolute; top: 0px; z-index: 1000;"
             [style.left]="PopupPosition == 'right'? (getElementOffsetLeft() > 850 ? 0 : getElementOffsetWidth()) :''"
             [style.right]="PopupPosition == 'left'?getElementOffsetWidth():''">
          <tbody>
          <tr class="ajax__validatorcallout_popup_table_row">
              <td class="ajax__validatorcallout_callout_cell">
                  <table cellpadding="0" cellspacing="0" border="0" class="ajax__validatorcallout_callout_table">
                      <tbody>
                      <tr class="ajax__validatorcallout_callout_table_row">
                          <td class="ajax__validatorcallout_callout_arrow_cell">
                              <div class="ajax__validatorcallout_innerdiv">
                                  <div style="width: 14px;"></div>
                                  <div style="width: 13px;"></div>
                                  <div style="width: 12px;"></div>
                                  <div style="width: 11px;"></div>
                                  <div style="width: 10px;"></div>
                                  <div style="width: 9px;"></div>
                                  <div style="width: 8px;"></div>
                                  <div style="width: 7px;"></div>
                                  <div style="width: 6px;"></div>
                                  <div style="width: 5px;"></div>
                                  <div style="width: 4px;"></div>
                                  <div style="width: 3px;">
                                  </div>
                                  <div style="width: 2px;"></div>
                                  <div style="width: 1px;"></div>
                              </div>
                          </td>
                      </tr>
                      </tbody>
                  </table>
              </td>
              <td class="ajax__validatorcallout_icon_cell">
                  <img border="0" src="../assets/Images/WebResource1.gif"></td>
              <td class="ajax__validatorcallout_error_message_cell">{{ErrorMessage}}</td>
              <td class="ajax__validatorcallout_close_button_cell">
                  <div class="ajax__validatorcallout_innerdiv">
                      <div (click)="HideCallOut()" style="vertical-align:top;" >

                        X
                      </div>
                  </div>
              </td>
          </tr>
          </tbody>
      </table>`,
  styleUrls: ['./validator-call-out.component.css']
})
export class ValidatorCallOutComponent implements OnInit {
  @Input()
  ID = ""

  @Input()
  ErrorMessage = "The control is invalid"

  @Input()
  Visibiltiy = false;

  @Input()
  ControlToValidate = "";

  @Input()
  PopupPosition = "right";

  constructor() { }

  ngOnInit() {
  }



  HideCallOut() {
    this.Visibiltiy = false;
  }

  getElementOffsetWidth() {

    var el = document.getElementById(this.ControlToValidate);
    if (el != null) {
      return el.offsetWidth.toString() + "px";
    }
  }

  getElementOffsetLeft() {
    var el = document.getElementById(this.ControlToValidate);
    if (el != null) {
      return el.parentElement.parentElement.parentElement.offsetLeft;
    }
  }


}
