alter table adm_stratigic_objectives add column bsc int;

alter table adm_project_evidence add column doc_name varchar(2000);

alter table adm_project_evidence  modify column file_name varchar(150) null;
alter table adm_project_evidence  modify column file_url varchar(500) null;
alter table adm_project_evidence  modify column file_type varchar(45) null;

INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/Projects', 'btnCancel', 'الغاء', '1', 'ar');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/Projects', 'btnSave', 'حفظ', '1', 'ar');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/Projects', 'btnCancel', 'Cacel', '1', 'en');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/Projects', 'btnSave', 'Save', '1', 'en');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/Projects', 'projEvidList', 'Project Evidences Documents', '1', 'en');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/Projects', 'projEvidList', 'الوثائق المطلوبة للتقييم', '1', 'ar');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/Projects', 'projEvidEntry', 'Project Evidences Document Entry', '1', 'en');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/Projects', 'projEvidEntry', 'ادخال الوثائق المطلوبة للتقييم', '1', 'ar');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/Projects', 'docName', 'Document Name', '1', 'en');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/Projects', 'docName', 'اسم الوثيقة', '1', 'ar');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/Projects', 'fileName', 'File Name', '1', 'en');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/Projects', 'fileName', 'اسم المرفق', '1', 'ar');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/Projects', 'confirm', 'Are you sure ?', '1', 'en');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/Projects', 'confirm', 'هل انت متأكد ؟', '1', 'ar');


INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectivesChart', 'btnCancel', 'الغاء', '1', 'ar');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectivesChart', 'btnSave', 'حفظ', '1', 'ar');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectivesChart', 'btnCancel', 'Cacel', '1', 'en');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectivesChart', 'btnSave', 'Save', '1', 'en');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectivesChart', 'projEvidList', 'Project Evidences Documents', '1', 'en');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectivesChart', 'projEvidList', 'الوثائق المطلوبة للتقييم', '1', 'ar');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectivesChart', 'projEvidEntry', 'Project Evidences Document Entry', '1', 'en');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectivesChart', 'projEvidEntry', 'ادخال الوثائق المطلوبة للتقييم', '1', 'ar');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectivesChart', 'docName', 'Document Name', '1', 'en');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectivesChart', 'docName', 'اسم الوثيقة', '1', 'ar');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectivesChart', 'fileName', 'File Name', '1', 'en');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectivesChart', 'fileName', 'اسم المرفق', '1', 'ar');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectivesChart', 'confirm', 'Are you sure ?', '1', 'en');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectivesChart', 'confirm', 'هل انت متأكد ؟', '1', 'ar');


INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/ProjectPlanningChart', 'btnCancel', 'الغاء', '1', 'ar');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/ProjectPlanningChart', 'btnSave', 'حفظ', '1', 'ar');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/ProjectPlanningChart', 'btnCancel', 'Cacel', '1', 'en');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/ProjectPlanningChart', 'btnSave', 'Save', '1', 'en');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/ProjectPlanningChart', 'projEvidList', 'Project Evidences Documents', '1', 'en');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/ProjectPlanningChart', 'projEvidList', 'الوثائق المطلوبة للتقييم', '1', 'ar');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/ProjectPlanningChart', 'projEvidEntry', 'Project Evidences Document Entry', '1', 'en');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/ProjectPlanningChart', 'projEvidEntry', 'ادخال الوثائق المطلوبة للتقييم', '1', 'ar');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/ProjectPlanningChart', 'docName', 'Document Name', '1', 'en');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/ProjectPlanningChart', 'docName', 'اسم الوثيقة', '1', 'ar');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/ProjectPlanningChart', 'fileName', 'File Name', '1', 'en');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/ProjectPlanningChart', 'fileName', 'اسم المرفق', '1', 'ar');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/ProjectPlanningChart', 'confirm', 'Are you sure ?', '1', 'en');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/ProjectPlanningChart', 'confirm', 'هل انت متأكد ؟', '1', 'ar');


INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/projectsAssessment', 'confirmUploadEvident', 'Are you sure, you want to upload this evident ?', '1', 'en');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/projectsAssessment', 'confirmUploadEvident', 'هل انت متأكد من رفع ملف التقييم ?', '1', 'ar');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/projectsAssessment', 'confirmDeleteEvident', 'Are you sure, you want to remove this evident ?', '1', 'en');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/projectsAssessment', 'confirmDeleteEvident', 'هل انت متأكد من حذف ملف التقييم ؟', '1', 'ar');




INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectivesChart', 'confirmUploadEvident', 'Are you sure, you want to upload this evident ?', '1', 'en');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectivesChart', 'confirmUploadEvident', 'هل انت متأكد من رفع ملف التقييم ?', '1', 'ar');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectivesChart', 'confirmDeleteEvident', 'Are you sure, you want to remove this evident ?', '1', 'en');
INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectivesChart', 'confirmDeleteEvident', 'هل انت متأكد من حذف ملف التقييم ؟', '1', 'ar');


INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) 
VALUES ('BackEnd', 'projAssessmentNeededDocs', 'One or more project assessment cannot be saved due to need evidenece documents needed.', '1', 'en');

INSERT INTO `hr_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`)
VALUES ('BackEnd', 'projAssessmentNeededDocs', 'واحد او اكثر من تقييم المشاريع لا يمكن حفظه بسبب الملفات المطلوبة.', '1', 'ar');
