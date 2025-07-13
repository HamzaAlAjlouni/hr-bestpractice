ALTER TABLE adm_competencies ADD position_id int  NOT NULL; 
ALTER TABLE adm_competencies ADD is_mandetory int  NOT NULL DEFAULT 0;
ALTER TABLE adm_competencies ADD CONSTRAINT fk_adm_competencies_adm_positions FOREIGN KEY ( position_id ) REFERENCES `ADM_POSITIONS`( `ID` ) ON DELETE NO ACTION ON UPDATE NO ACTION;
