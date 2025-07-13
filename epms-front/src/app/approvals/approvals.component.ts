import {Component, OnInit, ViewChild} from '@angular/core';
import {ProjectsService} from '../Services/Projects/projects.service';
import {UsersService} from '../Services/Users/users.service';
import {ProjectsAssessmentService} from '../Services/projects-assessment.service';
import {UserContextService} from '../Services/user-context.service';

import {OrganizationService} from '../Organization/organization.service';

@Component({
  selector: 'app-approvals',
  templateUrl: './approvals.component.html',
  styleUrls: ['./approvals.component.css'],
  providers: [UsersService, ProjectsService]
})
export class ApprovalsComponent implements OnInit {

  txtPageNameSearch = '';
  selectedApprovalID;
  showEntry = false;
  txtName;
  ddlUser;
  usersList;
  ddlPageUrl;
  MenuList;


  @ViewChild('gvApprovals', {read: false, static: false}) gvApprovals;

  constructor(
    private projectsAssessmentService: ProjectsAssessmentService,
    private userContextService: UserContextService,
    private userService: UsersService,
    private projectService: ProjectsService,
    private OrganizationService: OrganizationService
  ) {

    this.loadUsers();
    this.loadMenu();

  }

  ngOnInit() {

  }

  loadUsers() {
    this.userService.GetSystemUsers(this.userContextService.CompanyID).subscribe(res => {
      if (res.IsError) {
        alert(res.ErrorMessage + ",1");
        return;
      }
      this.usersList = res.Data;
    });
  }

  loadMenu() {
    this.userService.GetMenuPages(this.userContextService.CompanyID).subscribe(res => {
      if (res.IsError) {
        alert(res.ErrorMessage + ",1");
        return;
      }
      this.MenuList = res.Data;
    });
  }

  LoadApprovals() {
    this.OrganizationService.GetAllApprovalSetup(this.userContextService.CompanyID, this.txtPageNameSearch).subscribe(res => {
      if (res.IsError) {
        alert(res.ErrorMessage + ",1");
        return;
      }
      let cols = [
        {'HeaderText': "Approval Name", 'DataField': 'name'},
        {'HeaderText': "Approval Page", 'DataField': 'page_url'},
        {'HeaderText': "Reveiwer User", 'DataField': 'reviewing_user'}
      ];

      let actions = [
        {"title": "Edit", "DataValue": "ID", "Icon_Awesome": "fa fa-edit", "Action": "edit"},
        {"title": "Delete", "DataValue": "ID", "Icon_Awesome": "fa fa-trash", "Action": "delete"}
      ];
      this.gvApprovals.bind(cols, res.Data, 'gvApprovals', actions);
    });
  }

  AddApproval() {
    this.resetForm();
    this.showEntry = true;
  }

  gvApprovalsHandler(event) {
    if (event[1] == 'edit') {
      this.selectedApprovalID = event[0];
      this.showEntry = true;
      this.OrganizationService.GetApprovalSetupByID(event[0]).subscribe(res => {
        if (res.IsError) {
          alert(res.ErrorMessage + ",1");
          return;
        }
        this.txtName = res.Data.name;
        this.ddlPageUrl = res.Data.page_url;
        this.ddlUser = res.Data.reviewing_user;
      });
    } else if (event[1] == 'delete') {
      if (confirm('Are you sure, you want to delete approval?')) {
        this.OrganizationService.DeleteApproval(event[0]).subscribe(res => {
          if (res.IsError) {
            alert(res.ErrorMessage + ",1");
            return;
          }
          alert("0");

          this.LoadApprovals();
          this.resetForm();
        })
      }
    }
  }

  resetForm() {
    this.txtName = '';
    this.ddlUser = null;
    this.ddlPageUrl = null;
    this.selectedApprovalID = null;
    this.showEntry = false;
  }

  SaveApproval() {
    if (this.selectedApprovalID == null) {
      this.OrganizationService.SaveApproval(this.txtName, this.ddlPageUrl, this.ddlUser, this.userContextService.CompanyID)
        .subscribe(res => {
          if (res.IsError) {
            alert(res.ErrorMessage + ",1");
            return;
          }
          alert("0");

          this.LoadApprovals();
          this.resetForm();
        })
    } else {
      this.OrganizationService.UpdateApproval(this.selectedApprovalID, this.txtName, this.ddlPageUrl, this.ddlUser, this.userContextService.CompanyID)
        .subscribe(res => {
          if (res.IsError) {
            alert(res.ErrorMessage + ",1");
            return;
          }
          alert("0");

          this.LoadApprovals();
          this.resetForm();
        })
    }
  }
}
