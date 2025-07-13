create table tbl_emp_levels 
(
id bigint AUTO_INCREMENT,
lvl_code nvarchar(200) not null,
lvl_name nvarchar(200) not null,
lvl_number bigint not null,
primary key (id)
);


create table tbl_performance_levels 
(
id bigint AUTO_INCREMENT,
lvl_code nvarchar(200) not null,
lvl_name nvarchar(200) not null,
lvl_number bigint not null,
lvl_percent bigint not null,
lvl_year bigint not null,
primary key (id)
);