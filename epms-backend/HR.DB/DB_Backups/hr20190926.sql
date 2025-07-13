-- MySQL dump 10.13  Distrib 8.0.15, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: hr_db
-- ------------------------------------------------------
-- Server version	8.0.15

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
 SET NAMES utf8 ;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `adm_actions`
--

DROP TABLE IF EXISTS `adm_actions`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `adm_actions` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(100) NOT NULL,
  `menu_id` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_adm_actions_adm_menus` (`menu_id`),
  CONSTRAINT `fk_adm_actions_adm_menus` FOREIGN KEY (`menu_id`) REFERENCES `adm_menus` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `adm_actions`
--

LOCK TABLES `adm_actions` WRITE;
/*!40000 ALTER TABLE `adm_actions` DISABLE KEYS */;
/*!40000 ALTER TABLE `adm_actions` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `adm_auth_actions_excluded`
--

DROP TABLE IF EXISTS `adm_auth_actions_excluded`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `adm_auth_actions_excluded` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `action_id` int(11) NOT NULL,
  `is_readonly` int(11) NOT NULL DEFAULT '1',
  `is_invisible` int(11) NOT NULL DEFAULT '1',
  `role_id` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_adm_auth_actions_excluded_adm_actions` (`action_id`),
  KEY `fk_adm_auth_actions_excluded_adm_roles` (`role_id`),
  CONSTRAINT `fk_adm_auth_actions_excluded_adm_actions` FOREIGN KEY (`action_id`) REFERENCES `adm_actions` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `adm_auth_actions_excluded`
--

LOCK TABLES `adm_auth_actions_excluded` WRITE;
/*!40000 ALTER TABLE `adm_auth_actions_excluded` DISABLE KEYS */;
/*!40000 ALTER TABLE `adm_auth_actions_excluded` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `adm_authorization`
--

DROP TABLE IF EXISTS `adm_authorization`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `adm_authorization` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `MENU_ID` int(11) NOT NULL,
  `ROLE_ID` int(11) NOT NULL,
  `created_by` varchar(100) NOT NULL,
  `created_date` date NOT NULL,
  `modified_by` varchar(100) DEFAULT NULL,
  `modified_date` date DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `idx_ADM_AUTHORIZATION` (`MENU_ID`),
  KEY `idx_ADM_AUTHORIZATION_0` (`ROLE_ID`),
  CONSTRAINT `fk_adm_authorization_adm_menus` FOREIGN KEY (`MENU_ID`) REFERENCES `adm_menus` (`ID`),
  CONSTRAINT `fk_adm_authorization_adm_roles` FOREIGN KEY (`ROLE_ID`) REFERENCES `adm_roles` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `adm_authorization`
--

LOCK TABLES `adm_authorization` WRITE;
/*!40000 ALTER TABLE `adm_authorization` DISABLE KEYS */;
/*!40000 ALTER TABLE `adm_authorization` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `adm_branches`
--

DROP TABLE IF EXISTS `adm_branches`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `adm_branches` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `NAME` varchar(100) NOT NULL,
  `CODE` varchar(50) NOT NULL,
  `ADDRESS` varchar(1000) DEFAULT NULL,
  `FAX` varchar(50) DEFAULT NULL,
  `PHONE1` varchar(50) DEFAULT NULL,
  `PHONE2` varchar(50) DEFAULT NULL,
  `EMAIL` varchar(50) DEFAULT NULL,
  `WEBSITE` varchar(50) DEFAULT NULL,
  `COMPANY_ID` int(11) NOT NULL,
  `C_COUNTRY_ID` int(11) NOT NULL,
  `created_by` varchar(100) NOT NULL,
  `created_date` date NOT NULL,
  `modified_by` varchar(100) DEFAULT NULL,
  `modified_date` date DEFAULT NULL,
  `name2` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `idx_ADM_BRNACHES` (`COMPANY_ID`),
  CONSTRAINT `fk_adm_branches_adm_company` FOREIGN KEY (`COMPANY_ID`) REFERENCES `adm_company` (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `adm_branches`
--

LOCK TABLES `adm_branches` WRITE;
/*!40000 ALTER TABLE `adm_branches` DISABLE KEYS */;
INSERT INTO `adm_branches` VALUES (1,'branch 1','b1',NULL,NULL,NULL,NULL,NULL,NULL,1,1,'arafat','2019-01-01',NULL,NULL,'branch 1');
/*!40000 ALTER TABLE `adm_branches` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `adm_codes`
--

DROP TABLE IF EXISTS `adm_codes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `adm_codes` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `MAJOR_NO` int(11) NOT NULL,
  `MINOR_NO` int(11) NOT NULL,
  `NAME` varchar(1000) NOT NULL,
  `CODE` varchar(50) DEFAULT NULL,
  `company_id` int(11) NOT NULL,
  `created_by` varchar(100) NOT NULL,
  `created_date` date NOT NULL,
  `modified_by` varchar(100) DEFAULT NULL,
  `modified_date` date DEFAULT NULL,
  `name2` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `uniqe_index` (`MAJOR_NO`,`MINOR_NO`,`company_id`),
  KEY `idx_adm_codes` (`company_id`),
  CONSTRAINT `fk_adm_codes` FOREIGN KEY (`company_id`) REFERENCES `adm_company` (`ID`),
  CONSTRAINT `fk_adm_codes_adm_company` FOREIGN KEY (`company_id`) REFERENCES `adm_company` (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=30 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='TITLE , SKILL';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `adm_codes`
--

LOCK TABLES `adm_codes` WRITE;
/*!40000 ALTER TABLE `adm_codes` DISABLE KEYS */;
INSERT INTO `adm_codes` VALUES (14,3,1,'Yearly','1',1,'ADMIN','2019-01-01',NULL,NULL,'Yearly'),(15,3,2,'Midterm','2',1,'ADMIN','2019-01-01',NULL,NULL,'Midterm'),(16,3,3,'Quarterly','3',1,'ADMIN','2019-01-01',NULL,NULL,'Quarterly'),(17,4,1,'Accumulative','1',1,'admin','2019-01-01',NULL,NULL,'Accumulative'),(18,4,2,'Average','2',1,'admin','2019-01-01',NULL,NULL,'Average'),(19,4,3,'Last','3',1,'admin','2019-01-01',NULL,NULL,'Last'),(20,4,4,'Maximum','4',1,'admin','2019-01-01',NULL,NULL,'Maximum'),(21,4,5,'Minimum','5',1,'admin','2019-01-01',NULL,NULL,'Minimum'),(22,5,1,'Percentage','1',1,'admin','2019-01-01',NULL,NULL,'Percentage'),(23,5,2,'Value','1',1,'admin','2019-01-01',NULL,NULL,'Value'),(24,6,1,'Q1','1',1,'admin','2019-01-01',NULL,NULL,'Q1'),(25,6,2,'Q2','2',1,'admin','2019-01-01',NULL,NULL,'Q2'),(26,6,3,'Q3','3',1,'admin','2019-01-01',NULL,NULL,'Q3'),(27,6,4,'Q4','4',1,'admin','2019-01-01',NULL,NULL,'Q4'),(28,1,1,'Sector','1',1,'admin','2019-01-01',NULL,NULL,'Sector'),(29,1,2,'Unit','2',1,'admin','2019-01-01',NULL,NULL,'unit');
/*!40000 ALTER TABLE `adm_codes` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `adm_company`
--

DROP TABLE IF EXISTS `adm_company`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `adm_company` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `NAME` varchar(100) NOT NULL,
  `CODE` varchar(50) DEFAULT NULL,
  `ADDRESS` varchar(1000) DEFAULT NULL,
  `FAX` varchar(50) DEFAULT NULL,
  `PHONE1` varchar(50) DEFAULT NULL,
  `PHONE2` varchar(50) DEFAULT NULL,
  `EMAIL` varchar(50) DEFAULT NULL,
  `WEBSITE` varchar(50) DEFAULT NULL,
  `created_by` varchar(100) NOT NULL,
  `created_date` date NOT NULL,
  `modified_by` varchar(100) DEFAULT NULL,
  `modified_date` date DEFAULT NULL,
  `name2` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `adm_company`
--

LOCK TABLES `adm_company` WRITE;
/*!40000 ALTER TABLE `adm_company` DISABLE KEYS */;
INSERT INTO `adm_company` VALUES (1,'Company 1','Com1','Amman',NULL,NULL,NULL,NULL,NULL,'arafat','2019-01-01',NULL,NULL,'Company 1'),(2,'Company 2','com 2','amman',NULL,NULL,NULL,NULL,NULL,'arafat','2019-01-01',NULL,NULL,'company 2');
/*!40000 ALTER TABLE `adm_company` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `adm_company_obj_performance`
--

DROP TABLE IF EXISTS `adm_company_obj_performance`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `adm_company_obj_performance` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `year_id` int(11) NOT NULL,
  `company_id` int(11) NOT NULL,
  `total_employee` int(11) DEFAULT NULL,
  `Level1_Employee` float(12,3) DEFAULT NULL,
  `Level2_Employee` float(12,3) DEFAULT NULL,
  `Level3_Employee` float(12,3) DEFAULT NULL,
  `Level4_Employee` float(12,3) DEFAULT NULL,
  `Level5_Employee` float(12,3) DEFAULT NULL,
  `Level1_Result_Employee` float(12,3) DEFAULT NULL,
  `Level2_Result_Employee` float(12,3) DEFAULT NULL,
  `Level3_Result_Employee` float(12,3) DEFAULT NULL,
  `Level4_Result_Employee` float(12,3) DEFAULT NULL,
  `Level5_Result_Employee` float(12,3) DEFAULT NULL,
  `Result_Percentage` float(12,3) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `adm_company_obj_performance`
--

LOCK TABLES `adm_company_obj_performance` WRITE;
/*!40000 ALTER TABLE `adm_company_obj_performance` DISABLE KEYS */;
INSERT INTO `adm_company_obj_performance` VALUES (1,2019,1,12,0.240,0.960,8.400,1.800,0.600,NULL,NULL,NULL,NULL,NULL,NULL);
/*!40000 ALTER TABLE `adm_company_obj_performance` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `adm_competencies`
--

DROP TABLE IF EXISTS `adm_competencies`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `adm_competencies` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `code` varchar(50) NOT NULL,
  `name` varchar(1000) NOT NULL,
  `c_nature_id` int(11) NOT NULL,
  `company_id` int(11) NOT NULL,
  `created_by` varchar(100) NOT NULL,
  `created_date` date NOT NULL,
  `modified_by` varchar(100) DEFAULT NULL,
  `modified_date` date DEFAULT NULL,
  `name2` varchar(100) DEFAULT NULL,
  `position_id` int(11) NOT NULL,
  `is_mandetory` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`),
  KEY `fk_adm_competencies_adm_codes` (`c_nature_id`),
  KEY `fk_adm_competencies_adm_company` (`company_id`),
  KEY `fk_adm_competencies_adm_positions` (`position_id`),
  CONSTRAINT `fk_adm_competencies_adm_codes` FOREIGN KEY (`c_nature_id`) REFERENCES `adm_codes` (`ID`),
  CONSTRAINT `fk_adm_competencies_adm_company_0` FOREIGN KEY (`company_id`) REFERENCES `adm_company` (`ID`),
  CONSTRAINT `fk_adm_competencies_adm_positions` FOREIGN KEY (`position_id`) REFERENCES `adm_positions` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `adm_competencies`
--

LOCK TABLES `adm_competencies` WRITE;
/*!40000 ALTER TABLE `adm_competencies` DISABLE KEYS */;
/*!40000 ALTER TABLE `adm_competencies` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `adm_competencies_kpi`
--

DROP TABLE IF EXISTS `adm_competencies_kpi`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `adm_competencies_kpi` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(1000) NOT NULL,
  `c_kpi_type_id` int(11) NOT NULL,
  `competence_id` int(11) NOT NULL,
  `created_by` varchar(100) NOT NULL,
  `created_date` date NOT NULL,
  `modified_by` varchar(100) DEFAULT NULL,
  `modified_date` date DEFAULT NULL,
  `name2` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_adm_competencies_kpi_adm_competencies` (`competence_id`),
  CONSTRAINT `fk_adm_competencies_kpi_adm_competencies` FOREIGN KEY (`competence_id`) REFERENCES `adm_competencies` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `adm_competencies_kpi`
--

LOCK TABLES `adm_competencies_kpi` WRITE;
/*!40000 ALTER TABLE `adm_competencies_kpi` DISABLE KEYS */;
/*!40000 ALTER TABLE `adm_competencies_kpi` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `adm_employee_positions`
--

DROP TABLE IF EXISTS `adm_employee_positions`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `adm_employee_positions` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `YEAR` int(11) NOT NULL,
  `POSITION_ID` int(11) NOT NULL,
  `EMP_ID` int(11) NOT NULL,
  `created_by` varchar(100) NOT NULL,
  `created_date` date NOT NULL,
  `modified_by` varchar(100) DEFAULT NULL,
  `modified_date` date DEFAULT NULL,
  `name2` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `EMP_ID` (`EMP_ID`),
  KEY `YEAR` (`YEAR`),
  KEY `POSITION_ID` (`POSITION_ID`),
  CONSTRAINT `adm_employee_positions_ibfk_1` FOREIGN KEY (`EMP_ID`) REFERENCES `adm_employees` (`ID`),
  CONSTRAINT `adm_employee_positions_ibfk_2` FOREIGN KEY (`YEAR`) REFERENCES `adm_years` (`id`),
  CONSTRAINT `adm_employee_positions_ibfk_3` FOREIGN KEY (`POSITION_ID`) REFERENCES `adm_positions` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `adm_employee_positions`
--

LOCK TABLES `adm_employee_positions` WRITE;
/*!40000 ALTER TABLE `adm_employee_positions` DISABLE KEYS */;
/*!40000 ALTER TABLE `adm_employee_positions` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `adm_employees`
--

DROP TABLE IF EXISTS `adm_employees`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `adm_employees` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `name1_1` varchar(100) NOT NULL,
  `name1_2` varchar(100) DEFAULT NULL,
  `name1_3` varchar(100) DEFAULT NULL,
  `name1_4` varchar(100) NOT NULL,
  `UNIT_ID` int(11) NOT NULL,
  `COMPANY_ID` int(11) NOT NULL,
  `ADDRESS` varchar(1000) DEFAULT NULL,
  `PHONE1` varchar(50) DEFAULT NULL,
  `PHONE2` varchar(50) DEFAULT NULL,
  `PARENT_ID` int(11) DEFAULT NULL,
  `IS_STATUS` int(11) NOT NULL DEFAULT '0',
  `IMAGE` varchar(1000) DEFAULT NULL,
  `BRANCH_ID` int(11) NOT NULL,
  `SCALE_ID` int(11) NOT NULL,
  `START_DATE` date NOT NULL,
  `END_DATE` date DEFAULT NULL,
  `created_by` varchar(100) NOT NULL,
  `created_date` date NOT NULL,
  `modified_by` varchar(100) DEFAULT NULL,
  `modified_date` date DEFAULT NULL,
  `name2_1` varchar(100) DEFAULT NULL,
  `name2_2` varchar(100) DEFAULT NULL,
  `name2_3` varchar(100) DEFAULT NULL,
  `name2_4` varchar(100) DEFAULT NULL,
  `employee_number` int(11) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `idx_HRS_EMPLOYEES_0` (`UNIT_ID`),
  KEY `idx_HRS_EMPLOYEES_1` (`COMPANY_ID`),
  KEY `idx_HRS_EMPLOYEES_2` (`PARENT_ID`),
  KEY `idx_ADM_EMPLOYEES` (`BRANCH_ID`),
  KEY `idx_ADM_EMPLOYEES_0` (`SCALE_ID`),
  CONSTRAINT `fk_ADM_EMPLOYEES` FOREIGN KEY (`SCALE_ID`) REFERENCES `adm_scales` (`ID`),
  CONSTRAINT `fk_ADM_EMPLOYEES_BRN` FOREIGN KEY (`BRANCH_ID`) REFERENCES `adm_branches` (`ID`),
  CONSTRAINT `fk_EMPLOYEES_PARENT` FOREIGN KEY (`PARENT_ID`) REFERENCES `adm_employees` (`ID`),
  CONSTRAINT `fk_EMPLOYEES_UNIT` FOREIGN KEY (`UNIT_ID`) REFERENCES `adm_units` (`ID`),
  CONSTRAINT `fk_adm_employees_adm_company` FOREIGN KEY (`COMPANY_ID`) REFERENCES `adm_company` (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=16 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `adm_employees`
--

LOCK TABLES `adm_employees` WRITE;
/*!40000 ALTER TABLE `adm_employees` DISABLE KEYS */;
INSERT INTO `adm_employees` VALUES (4,'Arafat','Eid','Mohammed','Alarbeed',1,1,NULL,NULL,NULL,NULL,1,NULL,1,1,'2017-01-01',NULL,'arafat','2017-01-01',NULL,NULL,'arafat','eid','mohammed','alarbeed',1),(5,'a','a','a','a',1,1,NULL,NULL,NULL,NULL,1,NULL,1,2,'2017-01-01',NULL,'arafat','2017-01-01',NULL,'2019-01-01','a','a','a','a',2),(6,'a','a','a','a',1,1,NULL,NULL,NULL,NULL,1,NULL,1,2,'2017-01-01',NULL,'arafat','2017-01-01',NULL,NULL,'a','a','a','a',3),(7,'a','a','a','a',1,1,NULL,NULL,NULL,NULL,1,NULL,1,2,'2017-01-01',NULL,'arafat','2017-01-01',NULL,NULL,'a','a','a','a',4),(8,'a','a','a','a',3,1,NULL,NULL,NULL,NULL,1,NULL,1,2,'2017-01-01',NULL,'arafat','2017-01-01',NULL,NULL,'a','a','a','a',5),(9,'a','a','a','a',3,1,NULL,NULL,NULL,NULL,1,NULL,1,2,'2017-01-01',NULL,'arafat','2017-01-01',NULL,NULL,'a','a','a','a',6),(10,'a','a','a','a',2,1,NULL,NULL,NULL,NULL,1,NULL,1,2,'2017-01-01',NULL,'arafat','2017-01-01',NULL,NULL,'a','a','a','a',7),(11,'a','a','a','a',2,1,NULL,NULL,NULL,NULL,1,NULL,1,2,'2017-01-01',NULL,'arafat','2017-01-01',NULL,NULL,'a','a','a','a',8),(12,'a','a','a','a',2,1,NULL,NULL,NULL,NULL,1,NULL,1,2,'2017-01-01',NULL,'arafat','2017-01-01',NULL,NULL,'a','a','a','a',9),(13,'a','a','a','a',2,1,NULL,NULL,NULL,NULL,1,NULL,1,2,'2017-01-01',NULL,'arafat','2017-01-01',NULL,NULL,'a','a','a','a',10),(14,'a','a','a','a',2,1,NULL,NULL,NULL,NULL,1,NULL,1,2,'2017-01-01',NULL,'arafat','2017-01-01',NULL,NULL,'a','a','a','a',11),(15,'a','a','a','a',2,1,NULL,NULL,NULL,NULL,1,NULL,1,2,'2017-01-01',NULL,'arafat','2017-01-01',NULL,NULL,'a','a','a','a',12);
/*!40000 ALTER TABLE `adm_employees` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `adm_menus`
--

DROP TABLE IF EXISTS `adm_menus`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `adm_menus` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `NAME` varchar(100) NOT NULL,
  `URL` varchar(100) DEFAULT NULL,
  `ICONE` varchar(100) DEFAULT NULL,
  `PARENT_ID` int(11) NOT NULL,
  `COMPANY_ID` int(11) NOT NULL,
  `created_by` varchar(100) NOT NULL,
  `created_date` date NOT NULL,
  `modified_by` varchar(100) DEFAULT NULL,
  `modified_date` date DEFAULT NULL,
  `name2` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `idx_ADM_MENUS` (`PARENT_ID`),
  KEY `idx_ADM_MENUS_0` (`COMPANY_ID`),
  CONSTRAINT `fk_adm_menus_adm_company` FOREIGN KEY (`COMPANY_ID`) REFERENCES `adm_company` (`ID`),
  CONSTRAINT `fk_adm_menus_adm_menus` FOREIGN KEY (`PARENT_ID`) REFERENCES `adm_menus` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `adm_menus`
--

LOCK TABLES `adm_menus` WRITE;
/*!40000 ALTER TABLE `adm_menus` DISABLE KEYS */;
/*!40000 ALTER TABLE `adm_menus` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `adm_performance_levels`
--

DROP TABLE IF EXISTS `adm_performance_levels`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `adm_performance_levels` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `CODE` varchar(50) NOT NULL,
  `NAME` varchar(100) NOT NULL,
  `LEVEL_NUMBER` int(11) NOT NULL,
  `PERCENTAGE` float(12,0) NOT NULL,
  `COMPANY_ID` int(11) NOT NULL,
  `created_by` varchar(100) NOT NULL,
  `created_date` date NOT NULL,
  `modified_by` varchar(100) DEFAULT NULL,
  `modified_date` date DEFAULT NULL,
  `name2` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `idx_HRS_PERFORMANCE_LEVELS` (`COMPANY_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `adm_performance_levels`
--

LOCK TABLES `adm_performance_levels` WRITE;
/*!40000 ALTER TABLE `adm_performance_levels` DISABLE KEYS */;
/*!40000 ALTER TABLE `adm_performance_levels` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `adm_position_competencies`
--

DROP TABLE IF EXISTS `adm_position_competencies`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `adm_position_competencies` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `position_id` int(11) NOT NULL,
  `competence_id` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_adm_position_competencies_adm_competencies` (`competence_id`),
  KEY `fk_adm_position_competencies_adm_positions` (`position_id`),
  CONSTRAINT `fk_adm_position_competencies_adm_competencies` FOREIGN KEY (`competence_id`) REFERENCES `adm_competencies` (`id`),
  CONSTRAINT `fk_adm_position_competencies_adm_positions` FOREIGN KEY (`position_id`) REFERENCES `adm_positions` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `adm_position_competencies`
--

LOCK TABLES `adm_position_competencies` WRITE;
/*!40000 ALTER TABLE `adm_position_competencies` DISABLE KEYS */;
/*!40000 ALTER TABLE `adm_position_competencies` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `adm_positions`
--

DROP TABLE IF EXISTS `adm_positions`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `adm_positions` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `CODE` varchar(50) NOT NULL,
  `NAME` varchar(100) NOT NULL,
  `IS_MANAGMENT` int(11) NOT NULL DEFAULT '0',
  `COMPANY_ID` int(11) NOT NULL,
  `created_by` varchar(100) NOT NULL,
  `created_date` date NOT NULL,
  `modified_by` varchar(100) DEFAULT NULL,
  `modified_date` date DEFAULT NULL,
  `name2` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `idx_ADM_POSITIONS` (`COMPANY_ID`),
  CONSTRAINT `fk_adm_positions_adm_company` FOREIGN KEY (`COMPANY_ID`) REFERENCES `adm_company` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `adm_positions`
--

LOCK TABLES `adm_positions` WRITE;
/*!40000 ALTER TABLE `adm_positions` DISABLE KEYS */;
/*!40000 ALTER TABLE `adm_positions` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `adm_prj_results`
--

DROP TABLE IF EXISTS `adm_prj_results`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `adm_prj_results` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `period_no` int(11) NOT NULL,
  `plan_result` float(12,0) NOT NULL,
  `actual_result` float(12,0) DEFAULT NULL,
  `project_id` int(11) NOT NULL,
  `created_by` varchar(100) NOT NULL,
  `created_date` date NOT NULL,
  `modified_by` varchar(100) DEFAULT NULL,
  `modified_date` date DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_adm_prj_results_adm_projects` (`project_id`),
  CONSTRAINT `fk_adm_prj_results_adm_projects` FOREIGN KEY (`project_id`) REFERENCES `adm_projects` (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `adm_prj_results`
--

LOCK TABLES `adm_prj_results` WRITE;
/*!40000 ALTER TABLE `adm_prj_results` DISABLE KEYS */;
INSERT INTO `adm_prj_results` VALUES (7,2,50,25,1,'arafat','2019-01-01',NULL,NULL),(8,4,50,NULL,1,'arafat','2019-01-01',NULL,NULL),(9,1,25,20,2,'arafat','2019-01-01',NULL,NULL),(10,2,25,20,2,'arafat','2019-01-01',NULL,NULL),(11,3,25,NULL,2,'arafat','2019-01-01',NULL,NULL),(12,4,25,NULL,2,'arafat','2019-01-01',NULL,NULL);
/*!40000 ALTER TABLE `adm_prj_results` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `adm_prj_strat_obj`
--

DROP TABLE IF EXISTS `adm_prj_strat_obj`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `adm_prj_strat_obj` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `PROJECT_ID` int(11) NOT NULL,
  `STARTG_OBJ_ID` int(11) NOT NULL,
  `Q1` int(11) NOT NULL DEFAULT '0',
  `Q2` int(11) NOT NULL DEFAULT '0',
  `Q3` int(11) NOT NULL DEFAULT '0',
  `Q4` int(11) NOT NULL DEFAULT '0',
  `created_by` varchar(100) NOT NULL,
  `created_date` date NOT NULL,
  `modified_by` varchar(100) DEFAULT NULL,
  `modified_date` date DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `idx_ADM_PRJ_STRAT_OBJ` (`STARTG_OBJ_ID`),
  KEY `idx_ADM_PRJ_STRAT_OBJ_0` (`PROJECT_ID`),
  CONSTRAINT `fk_ADM_PRJ_STRAT_OBJ` FOREIGN KEY (`STARTG_OBJ_ID`) REFERENCES `adm_stratigic_objectives` (`ID`),
  CONSTRAINT `fk_ADM_PRJ_STRAT_OBJ_0` FOREIGN KEY (`PROJECT_ID`) REFERENCES `adm_projects` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `adm_prj_strat_obj`
--

LOCK TABLES `adm_prj_strat_obj` WRITE;
/*!40000 ALTER TABLE `adm_prj_strat_obj` DISABLE KEYS */;
/*!40000 ALTER TABLE `adm_prj_strat_obj` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `adm_projects`
--

DROP TABLE IF EXISTS `adm_projects`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `adm_projects` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `COMPANY_ID` int(11) NOT NULL,
  `CODE` varchar(50) NOT NULL,
  `NAME` varchar(1000) NOT NULL,
  `PROJECT_ORDER` int(11) NOT NULL,
  `WEIGHT` float(12,3) NOT NULL,
  `UNIT_ID` int(11) NOT NULL,
  `TARGET` int(11) NOT NULL,
  `C_KPI_CYCLE_ID` int(11) NOT NULL,
  `C_KPI_TYPE_ID` int(11) NOT NULL,
  `C_RESULT_UNIT_ID` int(11) NOT NULL,
  `DESCRIPTION` varchar(1000) DEFAULT NULL,
  `KPI` varchar(1000) DEFAULT NULL,
  `STARG_OBJ_ID` int(11) NOT NULL,
  `branch_id` int(11) NOT NULL,
  `created_by` varchar(100) NOT NULL,
  `created_date` date NOT NULL,
  `modified_by` varchar(100) DEFAULT NULL,
  `modified_date` date DEFAULT NULL,
  `name2` varchar(100) DEFAULT NULL,
  `Weight_From_Objective` float(12,3) DEFAULT NULL,
  `Result` float(12,3) DEFAULT NULL,
  `Result_Percentage` float(12,3) DEFAULT NULL,
  `Result_Weight_Percentage` float(12,3) DEFAULT NULL,
  `Result_Weight_Perc_From_Obj` float(12,3) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `idx_ADM_PROJECTS_COM` (`COMPANY_ID`),
  KEY `idx_ADM_PROJECTS_4` (`UNIT_ID`),
  KEY `idx_ADM_PROJECTS` (`STARG_OBJ_ID`),
  KEY `idx_ADM_PROJECTS_0` (`branch_id`),
  CONSTRAINT `fk_ADM_PROJECTS` FOREIGN KEY (`STARG_OBJ_ID`) REFERENCES `adm_stratigic_objectives` (`ID`),
  CONSTRAINT `fk_ADM_PROJECTS_0` FOREIGN KEY (`branch_id`) REFERENCES `adm_branches` (`ID`),
  CONSTRAINT `fk_ADM_PROJECTS_UNIT` FOREIGN KEY (`UNIT_ID`) REFERENCES `adm_units` (`ID`),
  CONSTRAINT `fk_adm_projects_adm_branches` FOREIGN KEY (`branch_id`) REFERENCES `adm_branches` (`ID`),
  CONSTRAINT `fk_adm_projects_adm_company` FOREIGN KEY (`COMPANY_ID`) REFERENCES `adm_company` (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `adm_projects`
--

LOCK TABLES `adm_projects` WRITE;
/*!40000 ALTER TABLE `adm_projects` DISABLE KEYS */;
INSERT INTO `adm_projects` VALUES (1,1,'1','prj 1',1,1.000,1,100,2,1,1,'prj 1','1',1,1,'ADMIN','2019-01-01',NULL,NULL,'prj 1',0.500,25.000,0.250,0.250,0.125),(2,1,'2','prj 2',2,1.000,2,100,3,1,1,'prj 2','1',2,1,'admin','2019-01-01',NULL,NULL,'prj 2',0.500,40.000,0.400,0.400,0.200);
/*!40000 ALTER TABLE `adm_projects` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `adm_roles`
--

DROP TABLE IF EXISTS `adm_roles`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `adm_roles` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `NAME` varchar(100) NOT NULL,
  `COMPANY_ID` int(11) NOT NULL,
  `IS_SYSTEM_ROLE` int(11) NOT NULL,
  `created_by` varchar(100) NOT NULL,
  `created_date` date NOT NULL,
  `modified_by` varchar(100) DEFAULT NULL,
  `modified_date` date DEFAULT NULL,
  `name2` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `idx_ADM_ROLES` (`COMPANY_ID`),
  CONSTRAINT `fk_adm_roles_adm_company` FOREIGN KEY (`COMPANY_ID`) REFERENCES `adm_company` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `adm_roles`
--

LOCK TABLES `adm_roles` WRITE;
/*!40000 ALTER TABLE `adm_roles` DISABLE KEYS */;
/*!40000 ALTER TABLE `adm_roles` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `adm_scales`
--

DROP TABLE IF EXISTS `adm_scales`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `adm_scales` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `SCALE_CODE` varchar(50) NOT NULL,
  `NAME` varchar(100) NOT NULL,
  `SCALE_NUMBER` int(11) NOT NULL,
  `COMPANY_ID` int(11) NOT NULL,
  `created_by` varchar(100) NOT NULL,
  `created_date` date NOT NULL,
  `modified_by` varchar(100) DEFAULT NULL,
  `modified_date` date DEFAULT NULL,
  `name2` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `idx_ADM_SCALES` (`SCALE_NUMBER`),
  KEY `fk_adm_scales_adm_company` (`COMPANY_ID`),
  CONSTRAINT `fk_adm_scales_adm_company` FOREIGN KEY (`COMPANY_ID`) REFERENCES `adm_company` (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `adm_scales`
--

LOCK TABLES `adm_scales` WRITE;
/*!40000 ALTER TABLE `adm_scales` DISABLE KEYS */;
INSERT INTO `adm_scales` VALUES (1,'1','Scale 1',1,1,'arafat','2019-01-01',NULL,NULL,'Scale 1'),(2,'2','Scale 2',2,1,'arafat','2019-01-01',NULL,NULL,'Scale 2');
/*!40000 ALTER TABLE `adm_scales` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `adm_stratigic_objectives`
--

DROP TABLE IF EXISTS `adm_stratigic_objectives`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `adm_stratigic_objectives` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `COMPANY_ID` int(11) NOT NULL,
  `CODE` varchar(50) NOT NULL,
  `NAME` varchar(1000) NOT NULL,
  `ORDER` int(11) NOT NULL,
  `WEIGHT` float(12,3) NOT NULL,
  `DESCRIPTION` varchar(1000) DEFAULT NULL,
  `year_id` int(11) NOT NULL,
  `created_by` varchar(100) NOT NULL,
  `created_date` date NOT NULL,
  `modified_by` varchar(100) DEFAULT NULL,
  `modified_date` date DEFAULT NULL,
  `name2` varchar(100) DEFAULT NULL,
  `Result_Percentage` float(12,3) DEFAULT NULL,
  `Result_Weight_Percentage` float(12,3) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `idx_ADM_STRATIGIC_OBJECTIVES` (`COMPANY_ID`),
  KEY `fk_adm_stratigic_obj_adm_years` (`year_id`),
  CONSTRAINT `fk_adm_stratigic_objectives_adm_company` FOREIGN KEY (`COMPANY_ID`) REFERENCES `adm_company` (`ID`),
  CONSTRAINT `fk_adm_stratigic_objectives_adm_years` FOREIGN KEY (`year_id`) REFERENCES `adm_years` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `adm_stratigic_objectives`
--

LOCK TABLES `adm_stratigic_objectives` WRITE;
/*!40000 ALTER TABLE `adm_stratigic_objectives` DISABLE KEYS */;
INSERT INTO `adm_stratigic_objectives` VALUES (1,1,'1','obj 1',1,0.500,NULL,2019,'ADMIN','2019-09-21',NULL,NULL,'obj 3',0.250,0.125),(2,1,'2','obj 2',2,0.500,NULL,2019,'ADMIN','2019-09-21',NULL,NULL,'obj 2',0.400,0.200);
/*!40000 ALTER TABLE `adm_stratigic_objectives` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `adm_unit_projects_performance`
--

DROP TABLE IF EXISTS `adm_unit_projects_performance`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `adm_unit_projects_performance` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `year_id` int(11) NOT NULL,
  `company_id` int(11) NOT NULL,
  `branch_id` int(11) NOT NULL,
  `unit_id` int(11) NOT NULL,
  `total_employee` int(11) DEFAULT NULL,
  `Level1_Employee` float(12,3) DEFAULT NULL,
  `Level2_Employee` float(12,3) DEFAULT NULL,
  `Level3_Employee` float(12,3) DEFAULT NULL,
  `Level4_Employee` float(12,3) DEFAULT NULL,
  `Level5_Employee` float(12,3) DEFAULT NULL,
  `Level1_Result_Employee` float(12,3) DEFAULT NULL,
  `Level2_Result_Employee` float(12,3) DEFAULT NULL,
  `Level3_Result_Employee` float(12,3) DEFAULT NULL,
  `Level4_Result_Employee` float(12,3) DEFAULT NULL,
  `Level5_Result_Employee` float(12,3) DEFAULT NULL,
  `Result_Percentage` float(12,3) DEFAULT NULL,
  `Projects_Weight_Perc_From_Objs` float(12,3) DEFAULT NULL,
  `Result_Weight_Perc_From_Objs` float(12,3) DEFAULT NULL,
  `Employee_Percentage` float(12,3) DEFAULT NULL,
  `Prjs_Level1_Employee` float(12,3) DEFAULT NULL,
  `Prjs_Level2_Employee` float(12,3) DEFAULT NULL,
  `Prjs_Level3_Employee` float(12,3) DEFAULT NULL,
  `Prjs_Level4_Employee` float(12,3) DEFAULT NULL,
  `Prjs_Level5_Employee` float(12,3) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `adm_unit_projects_performance`
--

LOCK TABLES `adm_unit_projects_performance` WRITE;
/*!40000 ALTER TABLE `adm_unit_projects_performance` DISABLE KEYS */;
INSERT INTO `adm_unit_projects_performance` VALUES (1,2019,1,1,1,NULL,0.080,0.320,2.800,0.600,0.200,0.030,0.120,1.050,0.225,0.075,NULL,0.500,0.125,NULL,0.120,0.480,4.200,0.900,0.300),(2,2019,1,1,2,NULL,0.120,0.480,4.200,0.900,0.300,0.048,0.192,1.680,0.360,0.120,NULL,0.500,0.200,NULL,0.120,0.480,4.200,0.900,0.300);
/*!40000 ALTER TABLE `adm_unit_projects_performance` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `adm_units`
--

DROP TABLE IF EXISTS `adm_units`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `adm_units` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `NAME` varchar(1000) NOT NULL,
  `CODE` varchar(50) NOT NULL,
  `ADDRESS` varchar(1000) DEFAULT NULL,
  `FAX` varchar(50) DEFAULT NULL,
  `PHONE1` varchar(50) DEFAULT NULL,
  `PHONE2` varchar(50) DEFAULT NULL,
  `C_UNIT_TYPE_ID` int(11) NOT NULL,
  `COMPANY_ID` int(11) NOT NULL,
  `parent_id` int(11) DEFAULT NULL,
  `created_by` varchar(100) NOT NULL,
  `created_date` date NOT NULL,
  `modified_by` varchar(100) DEFAULT NULL,
  `modified_date` date DEFAULT NULL,
  `name2` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `idx_ADM_UNITS_0` (`COMPANY_ID`),
  KEY `fk_adm_units_adm_units` (`parent_id`),
  CONSTRAINT `fk_ADM_UNITS_0` FOREIGN KEY (`COMPANY_ID`) REFERENCES `adm_company` (`ID`),
  CONSTRAINT `fk_adm_units_adm_company` FOREIGN KEY (`COMPANY_ID`) REFERENCES `adm_company` (`ID`),
  CONSTRAINT `fk_adm_units_adm_units` FOREIGN KEY (`parent_id`) REFERENCES `adm_units` (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=18 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `adm_units`
--

LOCK TABLES `adm_units` WRITE;
/*!40000 ALTER TABLE `adm_units` DISABLE KEYS */;
INSERT INTO `adm_units` VALUES (1,'Sector 1','s1',NULL,NULL,NULL,NULL,1,1,NULL,'arafat','2019-01-01',NULL,NULL,'Sector 1'),(2,'Unit 1','u1',NULL,NULL,NULL,NULL,2,1,1,'arafat','2019-01-01',NULL,NULL,'Unit 1'),(3,'Unit 2','u2',NULL,NULL,NULL,NULL,2,1,1,'arafat','2019-01-01',NULL,NULL,'Unit 2');
/*!40000 ALTER TABLE `adm_units` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `adm_users`
--

DROP TABLE IF EXISTS `adm_users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `adm_users` (
  `USERNAME` varchar(100) NOT NULL,
  `NAME` varchar(100) NOT NULL,
  `COMPANY_ID` int(11) NOT NULL,
  `PASSWORD` varchar(100) NOT NULL,
  `created_by` varchar(100) NOT NULL,
  `created_date` date NOT NULL,
  `modified_by` varchar(100) DEFAULT NULL,
  `modified_date` date DEFAULT NULL,
  `name2` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`USERNAME`),
  KEY `idx_ADM_USERS` (`COMPANY_ID`),
  CONSTRAINT `fk_adm_users_adm_company` FOREIGN KEY (`COMPANY_ID`) REFERENCES `adm_company` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `adm_users`
--

LOCK TABLES `adm_users` WRITE;
/*!40000 ALTER TABLE `adm_users` DISABLE KEYS */;
/*!40000 ALTER TABLE `adm_users` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `adm_users_roles`
--

DROP TABLE IF EXISTS `adm_users_roles`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `adm_users_roles` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `USERNAME` varchar(100) NOT NULL,
  `ROLE_ID` int(11) NOT NULL,
  `created_by` varchar(100) NOT NULL,
  `created_date` date NOT NULL,
  `modified_by` varchar(100) DEFAULT NULL,
  `modified_date` date DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `idx_ADM_USERS_ROLES` (`ROLE_ID`),
  KEY `idx_ADM_USERS_ROLES_0` (`USERNAME`),
  CONSTRAINT `fk_adm_users_roles_adm_roles` FOREIGN KEY (`ROLE_ID`) REFERENCES `adm_roles` (`ID`),
  CONSTRAINT `fk_adm_users_roles_adm_users` FOREIGN KEY (`USERNAME`) REFERENCES `adm_users` (`USERNAME`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `adm_users_roles`
--

LOCK TABLES `adm_users_roles` WRITE;
/*!40000 ALTER TABLE `adm_users_roles` DISABLE KEYS */;
/*!40000 ALTER TABLE `adm_users_roles` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `adm_years`
--

DROP TABLE IF EXISTS `adm_years`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `adm_years` (
  `id` int(11) NOT NULL,
  `created_by` varchar(100) NOT NULL,
  `created_date` date NOT NULL,
  `modified_by` varchar(100) DEFAULT NULL,
  `modified_date` date DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `adm_years`
--

LOCK TABLES `adm_years` WRITE;
/*!40000 ALTER TABLE `adm_years` DISABLE KEYS */;
INSERT INTO `adm_years` VALUES (2019,'arafat','2019-01-01',NULL,NULL),(2020,'arafat','2019-01-01',NULL,NULL),(2021,'arafat','2019-01-01',NULL,NULL);
/*!40000 ALTER TABLE `adm_years` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tbl_emp_levels`
--

DROP TABLE IF EXISTS `tbl_emp_levels`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `tbl_emp_levels` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `lvl_code` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `lvl_name` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `lvl_number` bigint(20) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tbl_emp_levels`
--

LOCK TABLES `tbl_emp_levels` WRITE;
/*!40000 ALTER TABLE `tbl_emp_levels` DISABLE KEYS */;
/*!40000 ALTER TABLE `tbl_emp_levels` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tbl_performance_levels`
--

DROP TABLE IF EXISTS `tbl_performance_levels`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `tbl_performance_levels` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `lvl_code` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `lvl_name` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `lvl_number` bigint(20) NOT NULL,
  `lvl_percent` bigint(20) NOT NULL,
  `lvl_year` bigint(20) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=16 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tbl_performance_levels`
--

LOCK TABLES `tbl_performance_levels` WRITE;
/*!40000 ALTER TABLE `tbl_performance_levels` DISABLE KEYS */;
INSERT INTO `tbl_performance_levels` VALUES (11,'1','1',1,2,2019),(12,'2','2',2,8,2019),(13,'3','3',3,70,2019),(14,'4','4',4,15,2019),(15,'5','5',5,5,2019);
/*!40000 ALTER TABLE `tbl_performance_levels` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tbl_skills_types`
--

DROP TABLE IF EXISTS `tbl_skills_types`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `tbl_skills_types` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `code` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `name` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tbl_skills_types`
--

LOCK TABLES `tbl_skills_types` WRITE;
/*!40000 ALTER TABLE `tbl_skills_types` DISABLE KEYS */;
/*!40000 ALTER TABLE `tbl_skills_types` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping events for database 'hr_db'
--

--
-- Dumping routines for database 'hr_db'
--
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2019-09-26 16:03:00
