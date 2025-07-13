CREATE SCHEMA `hr_db` ;

create table tbl_skills_types 
(
id bigint AUTO_INCREMENT,
code nvarchar(200) not null,
name nvarchar(200) not null,
primary key (id)
);