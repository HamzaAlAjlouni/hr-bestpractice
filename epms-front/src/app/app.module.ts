import {BrowserModule} from "@angular/platform-browser";
import {NgModule} from "@angular/core";
import {ReactiveFormsModule, FormsModule} from "@angular/forms";

// import ngx-translate and the http loader
import {TranslateLoader, TranslateModule} from '@ngx-translate/core';
import {TranslateHttpLoader} from '@ngx-translate/http-loader';
import {HttpClient, HttpClientModule, HTTP_INTERCEPTORS} from '@angular/common/http';


import {AppComponent} from "./app.component";
import {ErrorPageComponent} from "./error-page/error-page.component";
import {RouterModule, Routes} from "@angular/router";

import {MasterComponent} from "./master/master.component";
import {LandingComponent} from "./landing/landing.component";
import {LoginComponent} from "./login/login.component";
import {UserContextService} from "./Services/user-context.service";
import {HttpHandlerService} from "./Services/HttpHandlerService";

import {SharedServiceService} from "./Services/shared-service.service";

import {SkillstypesComponent} from "./settings/skillstypes/skillstypes.component";

import {DataGrid} from "./CustomControls/data-grid/data-grid.component";
import {EmployeeLevelsComponent} from "./settings/employee-levels/employee-levels.component";
import {PerformanceLevelsComponent} from "./settings/performance-levels/performance-levels.component";
import {PerformanceLevelsQuotaComponent} from "./settings/performance-levels-quota/performance-levels-quota.component";
import {TestOrgTreeComponent} from "./test/test-org-tree/test-org-tree.component";
import {OrgChartModule} from "ng2-org-chart";
import {CKEditorModule} from "ckeditor4-angular";
import {PositionsComponent} from "./positions/positions.component";
import {EmployeeObjectveComponent} from "./employee-objectve/employee-objectve.component";
import {EmployeeAssessmentComponent} from "./employee-assessment/employee-assessment.component";

import {StratigicObjectivesComponent} from "./Organization/stratigicobjectives/stratigicobjectives.component";
import {EmployeesComponent} from "./Organization/employees/employees.component";
import {EmplyeesStructureComponent} from "./Organization/emplyees-structure/emplyees-structure.component";

import {ProjectsAssessmentComponent} from "./projects-assessment/projects-assessment.component";
import {CompetenceComponent} from "./competence/competence.component";
import {ProjectsComponent} from "./OrganizationObjectives/projects/projects.component";
// tslint:disable-next-line:max-line-length
import {
  StrategicObjectivesChartsComponent
} from "./OrganizationObjectives/strategic-objectives-charts/strategic-objectives-charts.component";
import {DashBoardsComponent} from "./DashBoards/dash-boards/dash-boards.component";
import {EmployeeDashBoardComponent} from "./DashBoards/employee-dash-board/employee-dash-board.component";
import {UnitsComponent} from "./units/units.component";
import {ProjectPlannerComponent} from "./project-planner/project-planner.component";
import {
  ObjectiveKPIAssessmentComponent
} from "./Organization/objective-kpi-assessment/objective-kpi-assessment.component";
import {CompanyComponent} from "./company/company.component";
import {ValidatorCallOutComponent} from "./validator-call-out/validator-call-out.component";
import {EmployeeEducationPlanComponent} from "./Employee-Education-Plan/employee-education-plan.component";
import {ActionPlanComponent} from "./action-plan/action-plan.component";
import {ObjectivesAssessmentComponent} from "./objectives-assessment/objectives-assessment.component";
import {ActionPlansAssessmentComponent} from "./action-plans-assessment/action-plans-assessment.component";
import {UnitsPerformanceMonitorComponent} from "./units-performance-monitor/units-performance-monitor.component";
import {ApprovalsComponent} from "./approvals/approvals.component";
import {TrafficlightsetupComponent} from "./trafficlightsetup/trafficlightsetup.component";
import {EmployeeAssessment1Component} from "./employee-assessment1/employee-assessment1.component";
import {YearSetupComponent} from './year-setup/year-setup.component';
import {GradeSetupComponent} from './grade-setup/grade-setup.component';
import {StrategyComparisonComponent} from "./strategy-comparison/strategy-comparison.component";
import {ProjectEvaluationComponent} from './project-evaluation/project-evaluation.component';
import {AuthGuard} from "./auth.guard";
import {ForbiddenComponent} from './forbidden/forbidden.component';
import { QuatersActivationComponent } from './quaters-activation/quaters-activation.component';
import { OperationEvaluationComponent } from './operation-evaluation/operation-evaluation.component';
import {NewTreeComponent} from "./test/new-tree/new-tree.component";
import { AdminDashboardComponent } from './admin-dashboard/admin-dashboard.component';
import { CalculatationSetupComponent } from './calculatation-setup/calculatation-setup.component';
import { EmployeeDevelopmentPlanComponent } from './employee-development-plan/employee-development-plan.component';
import { ProjectsEmployeesComparisonComponent } from './projects-employees-comparition/projects-employees-comparison.component';
import { AccountSetupComponent } from './account-setup/account-setup.component';
import { EmployeeProjectCompetencyComponent } from './employee-project-competency/employee-project-competency.component';

const router: Routes = [
  // {path:"NewTreeComponent",component:NewTreeComponent , CanActivate:[AuthGuard]},
  {path: "test-org", component: TestOrgTreeComponent, canActivate: [AuthGuard]},
  {path: "stratigicobjectivesChart", component: StrategicObjectivesChartsComponent, canActivate: [AuthGuard]},
  {path: "account-setup", component: AccountSetupComponent, canActivate: [AuthGuard]},
  {path: "projects-employee-comparison", component: ProjectsEmployeesComparisonComponent, canActivate: [AuthGuard]},
  {path: "employee-development-plan", component: EmployeeDevelopmentPlanComponent, canActivate: [AuthGuard]},
  {path: "stratigicobjectives", component: StratigicObjectivesComponent, canActivate: [AuthGuard]},
  {path: "EmpStructure", component: EmplyeesStructureComponent, canActivate: [AuthGuard]},
  {path: "Employees", component: EmployeesComponent, canActivate: [AuthGuard]},
  {path: "performanceLevels", component: PerformanceLevelsComponent, canActivate: [AuthGuard]},
  {path: "performanceLevelsQuota", component: PerformanceLevelsQuotaComponent, canActivate: [AuthGuard]},
  {path: "empLevels", component: EmployeeLevelsComponent, canActivate: [AuthGuard]},
  {path: "skillsTypes", component: SkillstypesComponent, canActivate: [AuthGuard]},
  {path: "positions", component: PositionsComponent, canActivate: [AuthGuard]},
  {path: "employeeObjectve", component: EmployeeObjectveComponent, canActivate: [AuthGuard]},
  {path: "employeeAssessment", component: EmployeeAssessment1Component, canActivate: [AuthGuard]},
  {path: "projectsAssessment", component: ProjectsAssessmentComponent, canActivate: [AuthGuard]},
  {path: "emp-project-competency", component: EmployeeProjectCompetencyComponent, canActivate: [AuthGuard]},
  {path: "strategyComparison", component: StrategyComparisonComponent, canActivate: [AuthGuard]},
  {path: "competence", component: CompetenceComponent, canActivate: [AuthGuard]},
  {path: "admin-dashboard", component: AdminDashboardComponent, canActivate: [AuthGuard]},
  {path: "Projects", component: ProjectsComponent, canActivate: [AuthGuard]},
  {path: "project-evaluation-setup", component: ProjectEvaluationComponent, canActivate: [AuthGuard]},
  {path: "operation-evaluation-setup", component: OperationEvaluationComponent, canActivate: [AuthGuard]},
  {path: "DashBoards", component: DashBoardsComponent, canActivate: [AuthGuard]},
  {path: "EmpDashBoards", component: EmployeeDashBoardComponent, canActivate: [AuthGuard]},
  {path: "Units", component: UnitsComponent, canActivate: [AuthGuard]},
  {path: "ProjectPlanningChart", component: ProjectPlannerComponent, canActivate: [AuthGuard]},
  {path: "EmployeeEducationPlan", component: EmployeeEducationPlanComponent, canActivate: [AuthGuard]},
  {path: "Company", component: CompanyComponent, canActivate: [AuthGuard]},
  {path: "Landing", component: LandingComponent, canActivate: [AuthGuard]},
  {path: "ActionPlans", component: ActionPlanComponent, canActivate: [AuthGuard]},
  {path: "objKpiAssessment", component: ObjectivesAssessmentComponent, canActivate: [AuthGuard]},
  {path: "ActionPlansAssessment", component: ActionPlansAssessmentComponent, canActivate: [AuthGuard]},
  {path: "UnitPerformance", component: UnitsPerformanceMonitorComponent, canActivate: [AuthGuard]},
  {path: "Approvals", component: ApprovalsComponent, canActivate: [AuthGuard]},
  {path: "trafficlightSetup", component: TrafficlightsetupComponent, canActivate: [AuthGuard]},
  {path: "YearsSetup", component: YearSetupComponent, canActivate: [AuthGuard]},
  {path: "quarters-activation", component: QuatersActivationComponent, canActivate: [AuthGuard]},
  {path: "calculation-setup", component: CalculatationSetupComponent, canActivate: [AuthGuard]},
  {path: "GradesSetup", component: GradeSetupComponent, canActivate: [AuthGuard]},
  { path: 'forbidden', component: ForbiddenComponent },
  {path: "", component: DashBoardsComponent},
  {path: "**", component: ErrorPageComponent},
];

// required for AOT compilation
export function HttpLoaderFactory(http: HttpClient): TranslateHttpLoader {
  return new TranslateHttpLoader(http);
}


@NgModule({
  declarations: [
    AppComponent,
    ErrorPageComponent,
    MasterComponent,
    LandingComponent,
    LoginComponent,
    SkillstypesComponent,
    DataGrid,
    PositionsComponent,
    EmployeeObjectveComponent,
    StratigicObjectivesComponent,
    EmployeeLevelsComponent,
    PerformanceLevelsComponent,
    TestOrgTreeComponent,
    NewTreeComponent,
    EmployeesComponent,
    EmplyeesStructureComponent,
    ProjectsAssessmentComponent,
    CompetenceComponent,
    ProjectsComponent,
    StrategicObjectivesChartsComponent,
    DashBoardsComponent,
    EmployeeDashBoardComponent,
    EmployeeAssessmentComponent,
    UnitsComponent,
    CompanyComponent,
    ProjectPlannerComponent,
    PerformanceLevelsQuotaComponent,
    ObjectiveKPIAssessmentComponent,
    ValidatorCallOutComponent,
    EmployeeEducationPlanComponent,
    ActionPlanComponent,
    ObjectivesAssessmentComponent,
    ActionPlansAssessmentComponent,
    UnitsPerformanceMonitorComponent,
    ApprovalsComponent,
    TrafficlightsetupComponent,
    EmployeeAssessment1Component,
    YearSetupComponent, // ,
    GradeSetupComponent,
    StrategyComparisonComponent,
    ProjectEvaluationComponent,
    ForbiddenComponent,
    QuatersActivationComponent,
    OperationEvaluationComponent,
    AdminDashboardComponent,
    CalculatationSetupComponent,
    EmployeeDevelopmentPlanComponent,
    ProjectsEmployeesComparisonComponent,
    AccountSetupComponent,
    EmployeeProjectCompetencyComponent,
    // NewTreeComponent
  ],
  imports: [
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    RouterModule.forRoot(router, {useHash: true}),
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: HttpLoaderFactory,
        deps: [HttpClient]
      }
    }),
    BrowserModule,
    OrgChartModule,
    CKEditorModule,
  ],
  exports: [RouterModule],
  providers: [
    UserContextService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: HttpHandlerService,
      multi: true,
    },
    SharedServiceService,
  ],
  bootstrap: [AppComponent],
})
export class AppModule {
}
