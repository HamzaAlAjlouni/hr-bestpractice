alter table hr_db.adm_projects add p_type int null default 1;

create table adm_Objective_kpi (
id  int not null auto_increment primary key,
objective_id int not null,
name varchar(2000) not null,
name2 varchar(2000) null,
weight float not null,
target float not null,
bsc int not null,
measurement int not null,
description varchar(2000) null,
company_id int not null,
branch_id int not null,
created_by           varchar(50)  NOT NULL    ,
created_date         date  NOT NULL    ,
modified_by          varchar(50)      ,
modified_date        date );
ALTER TABLE adm_Objective_kpi ADD CONSTRAINT fk_obj_kpi_objectives_id FOREIGN KEY ( objective_id ) REFERENCES `adm_stratigic_objectives`( `ID` ) ON DELETE NO ACTION ON UPDATE NO ACTION;


INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectives', 'lblQ4Target', 'المستهدف للربع 4', '1', 'ar');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectives', 'lblQ4Target', 'Q4 Target', '1', 'en');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectives', 'lblQ3Target', 'المستهدف للربع 3', '1', 'ar');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectives', 'lblQ3Target', 'Q3 Target', '1', 'en');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectives', 'lblQ2Target', 'المستهدف للربع 2', '1', 'ar');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectives', 'lblQ2Target', 'Q2 Target', '1', 'en');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectives', 'lblQ1Target', 'المستهدف للربع 1', '1', 'ar');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectives', 'lblQ1Target', 'Q1 Target', '1', 'en');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectives', 'lblDescription', 'الوصف', '1', 'ar');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectives', 'lblDescription', 'Description', '1', 'en');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectives', 'lblResultUnit', 'وحدة النتيجة', '1', 'ar');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectives', 'lblResultUnit', 'Result Unit', '1', 'en');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectives', 'lblKPIType', 'طريقة الاحتساب', '1', 'ar');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectives', 'lblKPIType', 'KPI Type', '1', 'en');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectives', 'lblKPICycle', 'Review Cycle', '1', 'en');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectives', 'lblKPICycle', 'دورية المراجعة', '1', 'ar');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectives', 'lblTarget', 'المستهدف', '1', 'ar');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectives', 'lblKPIs', 'المؤشرات', '1', 'ar');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectives', 'lblTarget', 'Target', '1', 'en');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectives', 'lblKPIs', 'KPIs', '1', 'en');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectives', 'lblPleaseSelect', 'الرجاء الاختيار', '1', 'ar');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectives', 'lblPleaseSelect', 'lblPleaseSelect', '1', 'en');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectives', 'lblProjectName', 'المشروع', '1', 'ar');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectives', 'lblProjectName', 'Project', '1', 'en');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectives', 'lblProject', 'المشروع', '1', 'ar');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectives', 'lblProject', 'Project', '1', 'en');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectives', 'lblQ4T', 'المستهدف ربع 4', '1', 'ar');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectives', 'lblQ4T', 'Q4 T', '1', 'en');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectives', 'lblQ3T', 'المستهدف ربع 3', '1', 'ar');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectives', 'lblQ3T', 'Q3 T', '1', 'en');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectives', 'lblQ2T', 'المستهدف ربع 2', '1', 'ar');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectives', 'lblQ2T', 'Q2 T', '1', 'en');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectives', 'lblQ1T', 'المستهدف ربع 1', '1', 'ar');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectives', 'lblQ1T', 'Q1 T', '1', 'en');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectives', 'lblWeight', 'الوزن', '1', 'ar');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectives', 'lblWeight', 'Weight', '1', 'en');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectives', 'lblProjectDetails', 'تفاصيل المشروع', '1', 'ar');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectives', 'lblProjectDetails', 'Project Details', '1', 'en');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectives', 'lblUnit', 'القسم', '1', 'ar');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectives', 'lblUnit', 'Unit', '1', 'en');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectives', 'lblAll', '--- الكل ---', '1', 'ar');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectives', 'lblAll', '--- All ---', '1', 'en');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectives', 'lblStratigicObjective', 'الهدف الاستراتيجي', '1', 'ar');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectives', 'lblStratigicObjective', 'Stratigic Objective', '1', 'en');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectives', 'lblYear', 'السنة', '1', 'ar');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectives', 'lblYear', 'Year', '1', 'en');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectives', 'lblSearch', 'بحث', '1', 'ar');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectives', 'lblSearch', 'Search', '1', 'en');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectives', 'lblProjects', 'المشاريع', '1', 'ar');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectives', 'lblProjects', 'Projects', '1', 'en');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectives', 'lblSettings', 'الاعدادات', '1', 'ar');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectives', 'lblSettings', 'Settings', '1', 'en');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectives', 'lblOrganizationProjects', 'مشاريع المؤسسة', '1', 'ar');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectives', 'lblOrganizationProjects', 'Organization Projects', '1', 'en');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectives', 'lblPlannedCost', 'التكلفة المخططة', '1', 'ar');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectives', 'lblPlannedCost', 'Planned Cost', '1', 'en');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectives', 'lblIsProcess', 'Is Process', '1', 'en');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectives', 'optProject', 'Project', '1', 'en');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectives', 'optProcess', 'Process', '1', 'en');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectives', 'lblIsProcess', 'نوع المدخل', '1', 'ar');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectives', 'optProject', 'مشروع', '1', 'ar');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectives', 'optProcess', 'عملية', '1', 'ar');

INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectives', 'lblKpiName', 'Name', '1', 'en');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectives', 'lblKpiName', 'الاسم', '1', 'ar');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectives', 'lblKpiDesc', 'Description', '1', 'en');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectives', 'lblKpiDesc', 'الوصف', '1', 'ar');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectives', 'lblKpiWeight', 'Weight', '1', 'en');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectives', 'lblKpiWeight', 'الوزن', '1', 'ar');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectives', 'lblKpiTarget', 'Targer', '1', 'en');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectives', 'lblKpiTarget', 'الهدف', '1', 'ar');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectives', 'lblBSC', 'BCS', '1', 'en');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectives', 'lblBSC', 'نتيجة التوازن', '1', 'ar');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectives', 'lblKpiMeasure', 'Measurement', '1', 'en');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectives', 'lblKpiMeasure', 'القياس', '1', 'ar');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectives', 'lblKpiBSCFinancialOption', 'Financial', '1', 'en');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectives', 'lblKpiBSCFinancialOption', 'المالية', '1', 'ar');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectives', 'lblKpiBSCCustomersOption', 'Customers', '1', 'en');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectives', 'lblKpiBSCCustomersOption', 'العملاء', '1', 'ar');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectives', 'lblKpiBSCInternalProcessOption', 'Internal Process', '1', 'en');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectives', 'lblKpiBSCInternalProcessOption', 'عمليات داخلية', '1', 'ar');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectives', 'lblKpiBSCLearninggrowthOption', 'Learning growth', '1', 'en');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectives', 'lblKpiBSCLearninggrowthOption', 'النمو التعليمي', '1', 'ar');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectives', 'lblKpiMeasurePercentageOption', 'Percentage', '1', 'en');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectives', 'lblKpiMeasurePercentageOption', 'نسبة مئوية', '1', 'ar');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectives', 'lblKpiMeasureValueOption', 'Value', '1', 'en');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectives', 'lblKpiMeasureValueOption', 'قيمة', '1', 'ar');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectives', 'btnKpiSave', 'Save', '1', 'en');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectives', 'btnKpiSave', 'حفظ', '1', 'ar');

INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectives', 'lblObjKpiEntry', 'Objective KPIs Entry', '1', 'en');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectives', 'lblObjKpiEntry', 'معاير اداء الاهداف الاستراتيجية', '1', 'ar');

INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectives', 'lblObjKpiList', 'Objective KPIs List', '1', 'en');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectives', 'lblObjKpiList', 'قائمة معاير اداء الاهداف الاستراتيجية', '1', 'ar');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectives', 'colKpiName', 'Name', '1', 'en');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectives', 'colKpiName', 'الاسم', '1', 'ar');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectives', 'colKpiTarget', 'Target', '1', 'en');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectives', 'colKpiTarget', 'الهدف', '1', 'ar');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectives', 'colKpiWeight', 'Weight', '1', 'en');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectives', 'colKpiWeight', 'الوزن', '1', 'ar');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectives', 'lblTabRelatedProjets', 'Related Projects', '1', 'en');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectives', 'lblTabRelatedProjets', 'المشاريع ', '1', 'ar');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectives', 'lblTabRelatedKpis', 'Related Objectives KPIs', '1', 'en');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectives', 'lblTabRelatedKpis', 'معايير الاداء', '1', 'ar');

INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/ProjectPlanningChart', 'lblProjectPlannerHeader', 'Projects Planner', '1', 'en');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/ProjectPlanningChart', 'lblProjectPlannerHeader', 'مخطط المشاريع', '1', 'ar');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/ProjectPlanningChart', 'lblStratigicHeader', 'Stratigic Planning', '1', 'en');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/ProjectPlanningChart', 'lblStratigicHeader', 'مخطط استراتيجي', '1', 'ar');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/ProjectPlanningChart', 'lblSearchHeader', 'Search', '1', 'en');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/ProjectPlanningChart', 'lblSearchHeader', 'بحث', '1', 'ar');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/ProjectPlanningChart', 'lblYear', 'Year', '1', 'en');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/ProjectPlanningChart', 'lblYear', 'السنة', '1', 'ar');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/ProjectPlanningChart', 'lblOrgObjStrucHeader', 'Organization Stratigic Objectives Structure', '1', 'en');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/ProjectPlanningChart', 'lblOrgObjStrucHeader', 'الاهداف الستراتيجية للمؤسسة', '1', 'ar');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/ProjectPlanningChart', 'lblModalStrucObj', 'Stratigic Objectives', '1', 'en');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/ProjectPlanningChart', 'lblModalStrucObj', 'الاهداف الاستراتيجية', '1', 'ar');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/ProjectPlanningChart', 'lblModalProjects', 'Projects', '1', 'en');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/ProjectPlanningChart', 'lblModalProjects', 'المشاريع', '1', 'ar');

INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/ProjectPlanningChart', 'lblModalProjAssessment', 'Project Assessment', '1', 'en');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/ProjectPlanningChart', 'lblModalProjAssessment', 'تقييم المشاريع', '1', 'ar');

INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectivesChart', 'lblHeaderProjectNavigation', 'Project Navigation ', '1', 'en');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectivesChart', 'lblHeaderProjectNavigation', 'التنقل بين المشاريع', '1', 'ar');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectivesChart', 'lblHeaderOperationAssessment', 'Operation Assessment', '1', 'en');
INSERT INTO `hr_db`.`tbl_resources` (`id`, `url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES (NULL, '#/stratigicobjectivesChart', 'lblHeaderOperationAssessment', 'تقييم العمليات', '1', 'ar');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectivesChart', 'lblSearchHead', 'Search', '1', 'en');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectivesChart', 'lblSearchHead', 'بحث', '1', 'ar');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectivesChart', 'lblYear', 'Year', '1', 'en');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectivesChart', 'lblYear', 'السنة', '1', 'ar');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectivesChart', 'lblMainPanelHeader', 'Organization Stratigic Objectives Structure', '1', 'en');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectivesChart', 'lblMainPanelHeader', 'الهيكل التنظيمي للاهداف الاستراتيجية', '1', 'ar');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectivesChart', 'lblModalObjectiveHeader', 'Stratigic Objectives', '1', 'en');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectivesChart', 'lblModalObjectiveHeader', 'الاهداف الاستراتيجية', '1', 'ar');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectivesChart', 'lblModalProjects', 'Projects', '1', 'en');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectivesChart', 'lblModalProjects', 'المشاريع', '1', 'ar');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectivesChart', 'lblModalProjectsAssessment', 'Project Assessment', '1', 'en');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectivesChart', 'lblModalProjectsAssessment', 'تقييم المشاريع', '1', 'ar');


alter table adm_objective_kpi add column result float default 0;