ALTER TABLE `adm_stratigic_objectives` 
ADD COLUMN `actual_cost` FLOAT(12,3) NULL ;


ALTER TABLE `adm_projects` 
ADD COLUMN `actual_cost` FLOAT(12,3) NULL ;

ALTER TABLE `adm_company_obj_performance` 
ADD COLUMN `actual_cost` FLOAT(12,3) NULL ;

ALTER TABLE `adm_unit_projects_performance` 
ADD COLUMN `actual_cost` FLOAT(12,3) NULL ;
