ALTER TABLE adm_projects
  DROP FOREIGN KEY fk_ADM_PROJECTS_CYC;

ALTER TABLE adm_projects
  DROP FOREIGN KEY fk_ADM_PROJECTS_TYPE;

ALTER TABLE adm_projects
  DROP FOREIGN KEY fk_ADM_PROJECTS_R_UNIT;
  
INSERT INTO adm_years (year, created_by, created_date, modified_by,modified_date,id)
VALUES (2010, 'sysAdmin', SYSDATE(),null,null,5);

insert into adm_units (ID,NAME,CODE,ADDRESS,FAX,PHONE1,PHONE2,C_UNIT_TYPE_ID,COMPANY_ID,
parent_id,
created_by,created_date,modified_by,modified_date,name2) 
values(1,'أادارة دعم المؤسسات','0120',null,null,null,null,1,1,null,'SysAdmin',sysdate(),null,null,null);

insert into adm_company (ID,NAME,CODE,ADDRESS,FAX,PHONE1,PHONE2,EMAIL,WEBSITE,
created_by,created_date,modified_by,modified_date,name2) 
values(1,'HR','HRCOM',null,null,null,null,null,null,'SysAdmin',sysdate(),null,null,null);


INSERT INTO adm_stratigic_objectives (ID,COMPANY_ID,adm_stratigic_objectives.CODE,adm_stratigic_objectives.NAME,
adm_stratigic_objectives.ORDER,WEIGHT,
adm_stratigic_objectives.DESCRIPTION,adm_stratigic_objectives.year_id,adm_stratigic_objectives.created_by,
adm_stratigic_objectives.created_date,modified_by,modified_date,name2)
 values (1,1,'BMN2','تنظيم وتطوير قطاع المشاريع الصغيرة والمتوسطة وريادة الأعمال الوطنية',1,2,null,2010,'SysAdmin',sysdate(),null,null,null);
 
 INSERT INTO `hrdb`.`adm_codes` (`MAJOR_NO`, `MINOR_NO`, `NAME`, `CODE`, `company_id`, `created_by`, `created_date`, `name2`) VALUES ('2', '2', 'Jordan', '1', '1', 'admin', sysdate(), 'Jordan');

insert into adm_branches (ID,NAME,CODE,ADDRESS,FAX,PHONE1,PHONE2,EMAIL,WEBSITE,COMPANY_ID,C_COUNTRY_ID,
created_by,created_date,modified_by,modified_date,name2) 
values(1,'Amman','AMMAN',null,null,null,null,null,null,1,16,'SysAdmin',sysdate(),null,null,null);
