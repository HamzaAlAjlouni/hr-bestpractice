CREATE TABLE `adm_assessment_map` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `company_id` INT NOT NULL,
  `from` DECIMAL(12,3) NOT NULL,
  `to` DECIMAL(12,3) NOT NULL,
  `point` DECIMAL(12,3) NOT NULL DEFAULT 0,
  `color` VARCHAR(45) NOT NULL DEFAULT 'white',
  PRIMARY KEY (`id`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


INSERT INTO `adm_assessment_map` (`company_id`, `from`, `to`, `point`, `color`) VALUES ('1', '0', '74', '1', 'Red');
INSERT INTO `adm_assessment_map` (`company_id`, `from`, `to`, `point`, `color`) VALUES ('1', '75', '99', '2', 'Yellow');
INSERT INTO `adm_assessment_map` (`company_id`, `from`, `to`, `point`, `color`) VALUES ('1', '100', '105', '3', 'Orange');
INSERT INTO `adm_assessment_map` (`company_id`, `from`, `to`, `point`, `color`) VALUES ('1', '106', '119', '4', 'Green');
INSERT INTO `adm_assessment_map` (`company_id`, `from`, `to`, `point`, `color`) VALUES ('1', '120', '500', '5', 'Gray');
