CREATE TABLE adm_pos_description ( 
	id                   int  NOT NULL  AUTO_INCREMENT  PRIMARY KEY,
	name                 varchar(1000)  NOT NULL    ,
	name2                varchar(1000)      ,
	created_by           varchar(50)  NOT NULL    ,
	created_date         date  NOT NULL    ,
	modified_by          varchar(50)      ,
	modified_date        date      ,
	position_id          int  NOT NULL    
 ) engine=InnoDB;

CREATE INDEX fk_adm_pos_description_adm_positions ON adm_pos_description ( position_id );

ALTER TABLE adm_pos_description ADD CONSTRAINT fk_adm_pos_description_adm_positions FOREIGN KEY ( position_id ) REFERENCES `ADM_POSITIONS`( `ID` ) ON DELETE NO ACTION ON UPDATE NO ACTION;
