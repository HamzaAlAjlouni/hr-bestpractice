using HR.Busniess;
using HR.Entities.Admin;
using HR.Entities.Models;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;

namespace HR.Entities.Infrastructure
{
    public class HRContext : DbContext
    {
        private string _DataBaseName
        {
            get { return "hr_demo"; }
        }

        public HRContext()
            : base("hr_demo")
        {
            Database.Log = sql => System.Diagnostics.Debug.Write(sql);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<tbl_skills_types>().ToTable("adm_skills_types", _DataBaseName);

            modelBuilder.Entity<AuthorizationEntity>().ToTable("adm_authorization", _DataBaseName);
            modelBuilder.Entity<UsersEntity>().ToTable("adm_users", _DataBaseName);
            modelBuilder.Entity<RolesEntity>().ToTable("adm_roles", _DataBaseName);
            modelBuilder.Entity<UsersRolesEntity>().ToTable("adm_users_roles", _DataBaseName);
            modelBuilder.Entity<CompanyEntity>().ToTable("adm_company", _DataBaseName);
            modelBuilder.Entity<MenusEntity>().ToTable("adm_menus", _DataBaseName);
            modelBuilder.Entity<tbl_emp_levels>().ToTable("tbl_emp_levels", _DataBaseName);
            modelBuilder.Entity<tbl_performance_levels>().ToTable("tbl_performance_levels", _DataBaseName);

            modelBuilder.Entity<CodeEntity>().ToTable("adm_codes", _DataBaseName);
            modelBuilder.Entity<UnitEntity>().ToTable("adm_units", _DataBaseName);
            modelBuilder.Entity<BranchesEntity>().ToTable("adm_branches", _DataBaseName);

            modelBuilder.Entity<PositionCompetenciesEntity>().ToTable("adm_position_competencies", _DataBaseName);
            modelBuilder.Entity<CompetenciesKpiEntity>().ToTable("adm_competencies_kpi", _DataBaseName);
            modelBuilder.Entity<CompetenceEntity>().ToTable("adm_competencies", _DataBaseName);
            modelBuilder.Entity<PositionEntity>().ToTable("adm_positions", _DataBaseName);
            modelBuilder.Entity<CodeEntity>().ToTable("adm_codes", _DataBaseName);
            modelBuilder.Entity<UnitEntity>().ToTable("adm_units", _DataBaseName);

            modelBuilder.Entity<tbl_resources>().ToTable("tbl_resources", _DataBaseName);
            modelBuilder.Entity<EmployeesEntity>().ToTable("adm_employees", _DataBaseName);
            modelBuilder.Entity<ScalesEntity>().ToTable("adm_scales", _DataBaseName);
            modelBuilder.Entity<StratigicObjectivesEntity>().ToTable("adm_stratigic_objectives", _DataBaseName);

            modelBuilder.Entity<YearEntity>().ToTable("adm_years", _DataBaseName);

            modelBuilder.Entity<ProjectEntity>().ToTable("adm_projects", _DataBaseName);
            modelBuilder.Entity<ProjectResultEntity>().ToTable("adm_prj_results", _DataBaseName);
            modelBuilder.Entity<PositionDescriptionEntity>().ToTable("adm_pos_description", _DataBaseName);
            modelBuilder.Entity<EmployeeAssesmentEntity>().ToTable("adm_emp_assesment", _DataBaseName);
            modelBuilder.Entity<EmployeeObjectiveEntity>().ToTable("adm_emp_objective", _DataBaseName);
            modelBuilder.Entity<EmployeeObjectiveKPIEntity>().ToTable("adm_emp_obj_kpi", _DataBaseName);

            modelBuilder.Entity<EmployeeCompetencyEntity>().ToTable("adm_emp_competency", _DataBaseName);
            modelBuilder.Entity<EmployeeCompetencyKpiEntity>().ToTable("adm_emp_competency_kpi", _DataBaseName);
            modelBuilder.Entity<EmployeeCompetencyAssessmentEntity>().ToTable("adm_emp_comp_ass", _DataBaseName);
            modelBuilder.Entity<EmployeeCompetencyKpiAssessmentEntity>().ToTable("adm_emp_comp_kpi_ass", _DataBaseName);

            modelBuilder.Entity<EmployeeObjectiveAssessmentEntity>().ToTable("adm_emp_obj_ass", _DataBaseName);
            modelBuilder.Entity<EmployeeObjectiveKPIAssessmentEntity>().ToTable("adm_emp_obj_kpi_ass", _DataBaseName);
            modelBuilder.Entity<ProjectEvidenceEntity>().ToTable("adm_project_evidence", _DataBaseName);

            modelBuilder.Entity<ActionsEntity>().ToTable("adm_actions", _DataBaseName);
            modelBuilder.Entity<AuthExcludedActionsEntity>().ToTable("adm_auth_actions_excluded", _DataBaseName);

            modelBuilder.Entity<PerformancelevelsQuota>().ToTable("tbl_perf_level_quota", _DataBaseName);
            modelBuilder.Entity<ObjectiveKPIEntity>().ToTable("adm_Objective_kpi", _DataBaseName);
            modelBuilder.Entity<AssessmentMapping>().ToTable("adm_assessment_map", _DataBaseName);
            modelBuilder.Entity<EmployeeEducationEntity>().ToTable("adm_employee_education", _DataBaseName);
            modelBuilder.Entity<EmployeePositionsEntity>().ToTable("adm_employee_positions", _DataBaseName);
            modelBuilder.Entity<ProjectActionPlanEntity>().ToTable("adm_action_plan", _DataBaseName);

            modelBuilder.Entity<EmployeePerformanceSegmentEntity>().ToTable("adm_emp_perf_segments", _DataBaseName);
            modelBuilder.Entity<ApprovalSetupEntity>().ToTable("adm_approval_setup", _DataBaseName);
            modelBuilder.Entity<TrafficLightEntity>().ToTable("adm_traffic_light_indicator", _DataBaseName);
            modelBuilder.Entity<Performance_RecordsEntity>().ToTable("Performance_Records", _DataBaseName);
            modelBuilder.Entity<ProjectAssessmentRecordsEntity>().ToTable("ProjectAssessment_Records", _DataBaseName);
            base.OnModelCreating(modelBuilder);
        }

        private void _mappingEntityTables(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<tbl_skills_types>().ToTable("adm_skills_types", _DataBaseName);
            modelBuilder.Entity<ActionsEntity>().ToTable("adm_actions", _DataBaseName);
            modelBuilder.Entity<AuthExcludedActionsEntity>().ToTable("adm_auth_actions_excluded", _DataBaseName);
            modelBuilder.Entity<AuthorizationEntity>().ToTable("adm_authorization", _DataBaseName);
            modelBuilder.Entity<UsersEntity>().ToTable("adm_users", _DataBaseName);
            modelBuilder.Entity<RolesEntity>().ToTable("adm_roles", _DataBaseName);
            modelBuilder.Entity<UsersRolesEntity>().ToTable("adm_users_roles", _DataBaseName);
            modelBuilder.Entity<CompanyEntity>().ToTable("adm_company", _DataBaseName);
            modelBuilder.Entity<MenusEntity>().ToTable("adm_menus", _DataBaseName);
            modelBuilder.Entity<tbl_emp_levels>().ToTable("tbl_emp_levels", _DataBaseName);
            modelBuilder.Entity<tbl_performance_levels>().ToTable("tbl_performance_levels", _DataBaseName);
            modelBuilder.Entity<StratigicObjectivesEntity>().ToTable("adm_stratigic_objectives", _DataBaseName);
            modelBuilder.Entity<PositionCompetenciesEntity>().ToTable("adm_position_competencies", _DataBaseName);
            modelBuilder.Entity<CompetenciesKpiEntity>().ToTable("adm_competencies_kpi", _DataBaseName);
            modelBuilder.Entity<CompetenceEntity>().ToTable("adm_competencies", _DataBaseName);
            modelBuilder.Entity<PositionEntity>().ToTable("adm_positions", _DataBaseName);
            modelBuilder.Entity<CodeEntity>().ToTable("adm_codes", _DataBaseName);
            modelBuilder.Entity<UnitEntity>().ToTable("adm_units", _DataBaseName);
            modelBuilder.Entity<BranchesEntity>().ToTable("adm_branches", _DataBaseName);
            modelBuilder.Entity<tbl_resources>().ToTable("tbl_resources", _DataBaseName);
            modelBuilder.Entity<EmployeesEntity>().ToTable("adm_employees", _DataBaseName);
            modelBuilder.Entity<ScalesEntity>().ToTable("adm_scales", _DataBaseName);
            modelBuilder.Entity<YearEntity>().ToTable("adm_years", _DataBaseName);
            modelBuilder.Entity<ProjectEntity>().ToTable("adm_projects", _DataBaseName);
            modelBuilder.Entity<ProjectApprovalHistoryEntity>().ToTable("adm_project_approvals_history", _DataBaseName);
            modelBuilder.Entity<ProjectResultEntity>().ToTable("adm_prj_results", _DataBaseName);
            modelBuilder.Entity<EmployeePositionsEntity>().ToTable("adm_employee_positions", _DataBaseName);
            modelBuilder.Entity<PositionDescriptionEntity>().ToTable("adm_pos_description", _DataBaseName);
            modelBuilder.Entity<CompanyObjectivesPerformanceEntity>().ToTable("adm_company_obj_performance", _DataBaseName);
            modelBuilder.Entity<UnitProjectsPerformanceEntity>().ToTable("adm_unit_projects_performance", _DataBaseName);
            modelBuilder.Entity<EmployeeAssesmentEntity>().ToTable("adm_emp_assesment", _DataBaseName);
            modelBuilder.Entity<EmployeeObjectiveEntity>().ToTable("adm_emp_objective", _DataBaseName);
            modelBuilder.Entity<EmployeeObjectiveKPIEntity>().ToTable("adm_emp_obj_kpi", _DataBaseName);
            modelBuilder.Entity<EmployeeCompetencyEntity>().ToTable("adm_emp_competency", _DataBaseName);
            modelBuilder.Entity<EmployeeCompetencyKpiEntity>().ToTable("adm_emp_competency_kpi", _DataBaseName);
            modelBuilder.Entity<EmployeeCompetencyAssessmentEntity>().ToTable("adm_emp_comp_ass", _DataBaseName);
            modelBuilder.Entity<EmployeeCompetencyKpiAssessmentEntity>().ToTable("adm_emp_comp_kpi_ass", _DataBaseName);
            modelBuilder.Entity<EmployeeObjectiveAssessmentEntity>().ToTable("adm_emp_obj_ass", _DataBaseName);
            modelBuilder.Entity<EmployeeObjectiveKPIAssessmentEntity>().ToTable("adm_emp_obj_kpi_ass", _DataBaseName);
            modelBuilder.Entity<ProjectEvidenceEntity>().ToTable("adm_project_evidence", _DataBaseName);
            modelBuilder.Entity<PerformancelevelsQuota>().ToTable("tbl_perf_level_quota", _DataBaseName);
            modelBuilder.Entity<ObjectiveKPIEntity>().ToTable("adm_Objective_kpi", _DataBaseName);
            modelBuilder.Entity<AssessmentMapping>().ToTable("adm_assessment_map", _DataBaseName);
            modelBuilder.Entity<EmployeeEducationEntity>().ToTable("adm_employee_education", _DataBaseName);
            modelBuilder.Entity<ProjectActionPlanEntity>().ToTable("adm_action_plan", _DataBaseName);
            modelBuilder.Entity<ProjectEvaluationEntity>().ToTable("adm_project_evaluation", _DataBaseName);
            modelBuilder.Entity<ProjectEvaluatedValuesEntity>().ToTable("adm_project_evaluated_values", _DataBaseName);
            modelBuilder.Entity<ProjectCategoryEntity>().ToTable("adm_project_category", _DataBaseName);
            modelBuilder.Entity<ProjectEvaluationValuesEntity>().ToTable("adm_project_evaluation_values", _DataBaseName);
            modelBuilder.Entity<EmployeePerformanceSegmentEntity>().ToTable("adm_emp_perf_segments", _DataBaseName);
            modelBuilder.Entity<ApprovalSetupEntity>().ToTable("adm_approval_setup", _DataBaseName);
            modelBuilder.Entity<ProjectCalculationSetupEntity>().ToTable("adm_projects_calculation_setup", _DataBaseName);
            modelBuilder.Entity<TrafficLightEntity>().ToTable("adm_traffic_light_indicator", _DataBaseName);
            modelBuilder.Entity<Performance_RecordsEntity>().ToTable("Performance_Records", _DataBaseName);
            modelBuilder.Entity<ProjectAssessmentRecordsEntity>().ToTable("ProjectAssessment_Records", _DataBaseName);
            modelBuilder.Entity<QuatersActiviationEntity>().ToTable("adm_quaters_activation", _DataBaseName);
        }

        public DbSet<AuthExcludedActionsEntity> AuthExcludedActionsCollection { get; set; }
        public DbSet<ActionsEntity> ActionsCollection { get; set; }

        public DbSet<EmployeeAssesmentEntity> EmployeeAssesmentCollection { get; set; }
        public DbSet<EmployeeObjectiveKPIEntity> EmployeeObjectiveKPICollection { get; set; }
        public DbSet<ProjectCategoryEntity> ProjectCategoryCollection { get; set; }

        public DbSet<EmployeeObjectiveEntity> EmployeeObjectiveCollection { get; set; }
        public DbSet<PositionDescriptionEntity> PositionDescriptionEntityCollection { get; set; }
        public DbSet<PositionCompetenciesEntity> PositionCompetenciesCollection { get; set; }
        public DbSet<CompetenciesKpiEntity> CompetenciesKpiCollection { get; set; }
        public DbSet<CompetenceEntity> CompetenceCollection { get; set; }
        public DbSet<PositionEntity> PositionCollection { get; set; }
        public DbSet<CodeEntity> CodesCollection { get; set; }
        public DbSet<UnitEntity> UnitCollection { get; set; }
        public DbSet<ProjectEvaluationEntity> ProjectEvaluationCollection { get; set; }
        public DbSet<ProjectEvaluationValuesEntity> ProjectEvaluationValuesCollection { get; set; }
        public DbSet<ProjectEvaluatedValuesEntity> ProjectEvaluatedValuesCollection { get; set; }
        public DbSet<BranchesEntity> BranchesCollection { get; set; }
        public DbSet<QuatersActiviationEntity> QuatersActiviationCollection { get; set; }

        public DbSet<tbl_skills_types> tbl_skills_typesCollection { get; set; }

        public DbSet<AuthorizationEntity> AuthorizationCollection { get; set; }
        public DbSet<UsersEntity> UsersCollection { get; set; }
        public DbSet<RolesEntity> RolesCollection { get; set; }
        public DbSet<UsersRolesEntity> UsersRolesCollection { get; set; }
        
        public DbSet<ProjectCalculationSetupEntity> ProjectCalculationSetupCollection { get; set; }
        public DbSet<CompanyEntity> CompanyCollection { get; set; }
        public DbSet<MenusEntity> MenusCollection { get; set; }
        public DbSet<StratigicObjectivesEntity> StratigicObjectivesCollection { get; set; }

        public DbSet<tbl_emp_levels> tbl_emp_levelsCollection { get; set; }
        public DbSet<tbl_performance_levels> tbl_performance_levelsCollection { get; set; }

        public DbSet<tbl_resources> tbl_resourcesCollection { get; set; }
        public DbSet<EmployeesEntity> EmployeesCollection { get; set; }
        public DbSet<ScalesEntity> ScalesCollection { get; set; }
        public DbSet<YearEntity> YearsCollection { get; set; }
        public DbSet<ProjectEntity> ProjectsCollection { get; set; }
        public DbSet<ProjectApprovalHistoryEntity> ProjectApprovalHistoryCollection { get; set; }
        public DbSet<ProjectResultEntity> ProjectResultCollection { get; set; }
        public DbSet<CompanyObjectivesPerformanceEntity> CompanyObjectivesPerformanceCollection { get; set; }
        public DbSet<UnitProjectsPerformanceEntity> UnitProjectsPerformanceCollection { get; set; }

        public DbSet<EmployeePositionsEntity> EmployeesPostionsCollection { get; set; }


        public DbSet<EmployeeCompetencyEntity> EmployeeCompetencyCollection { get; set; }
        public DbSet<EmployeeCompetencyKpiEntity> EmployeeCompetencyKpiCollection { get; set; }
        public DbSet<EmployeeCompetencyAssessmentEntity> EmployeeCompetencyAssessmentCollection { get; set; }
        public DbSet<EmployeeCompetencyKpiAssessmentEntity> EmployeeCompetencyKpiAssessmentCollection { get; set; }

        public DbSet<EmployeeObjectiveAssessmentEntity> EmployeeObjectiveAssessmentCollection { get; set; }
        public DbSet<EmployeeObjectiveKPIAssessmentEntity> EmployeeObjectiveKPIAssessmentCollection { get; set; }

        public DbSet<ProjectEvidenceEntity> ProjectEvidenceCollection { get; set; }

        public DbSet<PerformancelevelsQuota> PerformancelevelsQuotaCollection { get; set; }

        public DbSet<ObjectiveKPIEntity> ObjectiveKpiCollection { get; set; }

        public DbSet<AssessmentMapping> AssessmentMappingCollection { get; set; }

        public DbSet<EmployeeEducationEntity> EmployeeEducationCollection { get; set; }

        public DbSet<ProjectActionPlanEntity> ProjectActionPlanCollection { get; set; }

        public DbSet<EmployeePerformanceSegmentEntity> EmployeePerformanceSegmentCollection { get; set; }

        public DbSet<ApprovalSetupEntity> ApprovalSetupCollection { get; set; }

        public DbSet<TrafficLightEntity> TrafficLightCollection { get; set; }
        public DbSet<Performance_RecordsEntity> Performance_RecordsCollection { get; set; }
        public DbSet<ProjectAssessmentRecordsEntity> ProjectAssessmentRecordsCollection { get; set; }
    }
}