UPDATE `adm_codes` SET `name2` = 'قسم' WHERE (`ID` = '38');
UPDATE `adm_codes` SET `name2` = 'قطاع' WHERE (`ID` = '39');

INSERT INTO `tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectives', 'lblDetails', 'Details', '1', 'en');
INSERT INTO `tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectives', 'lblDetails', 'التفاصيل', '1', 'ar');
