ALTER TABLE `adm_position_competencies` 
ADD COLUMN `competence_level` INT(11) NOT NULL AFTER `competence_id`;


INSERT INTO `tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/positions', 'CompetenceLevel', 'Competence Level', '1', 'en');
INSERT INTO `tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/positions', 'CompetenceLevel', 'درجة الكفاءة', '1', 'ar');
