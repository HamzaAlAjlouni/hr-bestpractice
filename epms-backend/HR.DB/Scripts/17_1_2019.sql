ALTER TABLE `hr1_db`.`adm_employee_education` 
CHANGE COLUMN `field` `field` VARCHAR(500) CHARACTER SET 'utf8' COLLATE 'utf8_general_ci' NULL DEFAULT NULL ;


ALTER TABLE `adm_skills_types` 
ADD COLUMN `name2` VARCHAR(2000) NULL,
CHANGE COLUMN `name` `name` VARCHAR(2000) CHARACTER SET 'utf8' NULL ;


INSERT INTO `tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/skillsTypes', 'MsgSkillsTypeDeleted', 'Skills Type Deleted.', '1', 'en');
INSERT INTO `tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/skillsTypes', 'MsgSkillsTypeDeleted', 'تم حذف المهارة بنجاح', '1', 'ar');
INSERT INTO `tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/skillsTypes', 'AreYouSure', 'Are you sure ?', '1', 'en');
INSERT INTO `tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/skillsTypes', 'AreYouSure', 'هل انت متأكد؟', '1', 'ar');
INSERT INTO `tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/skillsTypes', 'lblSettings', 'Settings', '1', 'en');
INSERT INTO `tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/skillsTypes', 'lblSettings', 'الاعدادات', '1', 'ar');
INSERT INTO `tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/skillsTypes', 'lblHome', 'Home', '1', 'en');
INSERT INTO `tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/skillsTypes', 'lblHome', 'الصفحة الرئيسية', '1', 'ar');
INSERT INTO `tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/skillsTypes', 'lblEdit', 'Edit', '1', 'en');
INSERT INTO `tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/skillsTypes', 'lblEdit', 'تعديل', '1', 'ar');
INSERT INTO `tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/skillsTypes', 'lblDelete', 'Delete', '1', 'en');
INSERT INTO `tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/skillsTypes', 'lblDelete', 'حذف', '1', 'ar');


INSERT INTO `tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/positions', 'lblEdit', 'Edit', '1', 'en');
INSERT INTO `tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/positions', 'lblEdit', 'تعديل', '1', 'ar');
INSERT INTO `tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/positions', 'lblDetails', 'Details', '1', 'en');
INSERT INTO `tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/positions', 'lblDetails', 'التفاصيل', '1', 'ar');
INSERT INTO `tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/positions', 'lblDelete', 'Delete', '1', 'en');
INSERT INTO `tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/positions', 'lblDelete', 'حذف', '1', 'ar');

INSERT INTO `tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/competence', 'lblEdit', 'Edit', '1', 'en');
INSERT INTO `tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/competence', 'lblEdit', 'تعديل', '1', 'ar');
INSERT INTO `tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/competence', 'lblDelete', 'Delete', '1', 'en');
INSERT INTO `tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/competence', 'lblDelete', 'حذف', '1', 'ar');
INSERT INTO `tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/competence', 'lblDetails', 'Details', '1', 'en');
INSERT INTO `tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/competence', 'lblDetails', 'التفاصيل', '1', 'ar');

INSERT INTO `tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('global', 'lblActive', 'Active', '1', 'en');
INSERT INTO `tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('global', 'lblActive', 'فعال', '1', 'ar');
INSERT INTO `tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('global', 'lblInactive', 'Inactive', '1', 'en');
INSERT INTO `tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('global', 'lblInactive', 'غير فعال', '1', 'ar');
