-- MySQL dump 10.13  Distrib 8.0.17, for Win64 (x86_64)
--
-- Host: localhost    Database: hr_db4
-- ------------------------------------------------------
-- Server version	8.0.17

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
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
/*!50503 SET character_set_client = utf8mb4 */;
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
/*!50503 SET character_set_client = utf8mb4 */;
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
/*!50503 SET character_set_client = utf8mb4 */;
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
  KEY `idx_ADM_AUTHORIZATION_ROLE` (`ROLE_ID`),
  CONSTRAINT `fk_adm_authorization_adm_menus` FOREIGN KEY (`MENU_ID`) REFERENCES `adm_menus` (`ID`),
  CONSTRAINT `fk_adm_authorization_adm_roles` FOREIGN KEY (`ROLE_ID`) REFERENCES `adm_roles` (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=25 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `adm_authorization`
--

LOCK TABLES `adm_authorization` WRITE;
/*!40000 ALTER TABLE `adm_authorization` DISABLE KEYS */;
INSERT INTO `adm_authorization` VALUES (1,1,1,'ADMIN','2019-01-01','ADMIN','2019-01-01'),(2,2,1,'ADMIN','2019-01-01','ADMIN','2019-01-01'),(3,3,1,'ADMIN','2019-01-01','ADMIN','2019-01-01'),(4,4,1,'ADMIN','2019-01-01','ADMIN','2019-01-01'),(5,5,1,'ADMIN','2019-01-01','ADMIN','2019-01-01'),(6,6,1,'ADMIN','2019-01-01','ADMIN','2019-01-01'),(7,7,1,'ADMIN','2019-01-01','ADMIN','2019-01-01'),(8,8,1,'ADMIN','2019-01-01','ADMIN','2019-01-01'),(9,9,1,'ADMIN','2019-01-01','ADMIN','2019-01-01'),(10,10,1,'ADMIN','2019-01-01','ADMIN','2019-01-01'),(11,11,1,'ADMIN','2019-01-01','ADMIN','2019-01-01'),(12,12,1,'ADMIN','2019-01-01','ADMIN','2019-01-01'),(13,13,1,'ADMIN','2019-01-01','ADMIN','2019-01-01'),(14,14,1,'ADMIN','2019-01-01','ADMIN','2019-01-01'),(15,15,1,'ADMIN','2019-01-01','ADMIN','2019-01-01'),(16,16,1,'ADMIN','2019-01-01','ADMIN','2019-01-01'),(17,17,1,'ADMIN','2019-01-01','ADMIN','2019-01-01'),(18,18,1,'ADMIN','2019-01-01','ADMIN','2019-01-01'),(19,19,1,'ADMIN','2019-01-01','ADMIN','2019-01-01'),(21,20,1,'ADMIN','2019-01-20','ADMIN','2019-01-20'),(22,21,1,'ADMIN','2019-10-25','ADMIN','2019-10-25'),(23,22,1,'ADMIN','2019-10-25','ADMIN','2019-10-25'),(24,24,1,'ADMIN','2019-10-25','ADMIN','2019-10-25');
/*!40000 ALTER TABLE `adm_authorization` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `adm_branches`
--

DROP TABLE IF EXISTS `adm_branches`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
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
  KEY `idx_ADM_BRNACHES_0` (`C_COUNTRY_ID`),
  CONSTRAINT `fk_adm_branches_adm_company` FOREIGN KEY (`COMPANY_ID`) REFERENCES `adm_company` (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `adm_branches`
--

LOCK TABLES `adm_branches` WRITE;
/*!40000 ALTER TABLE `adm_branches` DISABLE KEYS */;
INSERT INTO `adm_branches` VALUES (1,'Head','HQ','Amman',NULL,NULL,NULL,NULL,NULL,1,1,'admin','2019-09-17',NULL,NULL,NULL);
/*!40000 ALTER TABLE `adm_branches` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `adm_codes`
--

DROP TABLE IF EXISTS `adm_codes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
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
) ENGINE=InnoDB AUTO_INCREMENT=244 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='TITLE , SKILL';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `adm_codes`
--

LOCK TABLES `adm_codes` WRITE;
/*!40000 ALTER TABLE `adm_codes` DISABLE KEYS */;
INSERT INTO `adm_codes` VALUES (1,1,1,'Basic','UNIT',1,'admin','2019-09-17',NULL,NULL,'الاساسية'),(2,1,2,'Leadership','SEC',1,'admin','2019-09-17',NULL,NULL,'القيادية'),(3,2,1,'Jordan','JOR',1,'admin','2019-09-17',NULL,NULL,NULL),(12,5,1,'Percentage','1',1,'admin','2019-01-01',NULL,NULL,'Percentage'),(13,5,2,'Value','1',1,'admin','2019-01-01',NULL,NULL,'Value'),(18,3,1,'Annual','1',1,'ADMIN','2019-01-01',NULL,NULL,'Yearly'),(19,3,2,'Semi Annual','2',1,'ADMIN','2019-01-01',NULL,NULL,'Midterm'),(20,3,3,'Quarterly','3',1,'ADMIN','2019-01-01',NULL,NULL,'Quarterly'),(21,4,1,'Accumulative','1',1,'admin','2019-01-01',NULL,NULL,'Accumulative'),(22,4,2,'Average','2',1,'admin','2019-01-01',NULL,NULL,'Average'),(23,4,3,'Last','3',1,'admin','2019-01-01',NULL,NULL,'Last'),(24,4,4,'Maximum','4',1,'admin','2019-01-01',NULL,NULL,'Maximum'),(25,4,5,'Minimum','5',1,'admin','2019-01-01',NULL,NULL,'Minimum'),(28,6,1,'Q1','1',1,'admin','2019-01-01',NULL,NULL,'Q1'),(29,6,2,'Q2','2',1,'admin','2019-01-01',NULL,NULL,'Q2'),(30,6,3,'Q3','3',1,'admin','2019-01-01',NULL,NULL,'Q3'),(31,6,4,'Q4','4',1,'admin','2019-01-01',NULL,NULL,'Q4'),(32,2,2,'KSA','KSA',1,'admin','2019-10-05',NULL,NULL,'Jordan'),(33,7,0,'CompetenceLevel','',1,'ADMIN','2019-01-01',NULL,NULL,'CompetenceLevel'),(34,7,1,'Beginner','',1,'ADMIN','2019-01-01',NULL,NULL,'مبتديء'),(35,7,2,'Middle','',1,'ADMIN','2019-01-01',NULL,NULL,'متوسط'),(36,7,3,'Advanced',NULL,1,'ADMIN','2019-01-01',NULL,NULL,'متقدم'),(37,8,0,'UnitType',NULL,1,'ADMIN','2019-01-10',NULL,NULL,'UnitType'),(38,8,1,'Unit',NULL,1,'ADMIN','2019-01-10',NULL,NULL,'Unit'),(39,8,2,'Sector',NULL,1,'ADMIN','2019-01-10',NULL,NULL,'Sector'),(40,9,0,'',NULL,1,'ADMIN','2019-01-10',NULL,NULL,''),(41,9,1,'Planned Quota',NULL,1,'ADMIN','2019-01-10',NULL,NULL,'كوتا المخطط لها'),(42,9,2,'Remaning Employees',NULL,1,'ADMIN','2019-01-10',NULL,NULL,'باقي الموظفين'),(44,10,1,'United States Dollars','USD',1,'ADMIN','0000-00-00','ADMIN','0000-00-00','دولار أمريكي'),(45,10,2,'Algeria Dinars','DZD',1,'ADMIN','0000-00-00','ADMIN','0000-00-00','دينار جزائري'),(46,10,3,'Argentina Pesos','ARP',1,'ADMIN','0000-00-00','ADMIN','0000-00-00','بيزو أرجنتيني'),(47,10,4,'Australia Dollars','AUD',1,'ADMIN','0000-00-00','ADMIN','0000-00-00','دولار أسترالي'),(48,10,5,'Austria Schillings','ATS',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(49,10,6,'Bahamas Dollars','BSD',1,'ADMIN','0000-00-00','ADMIN','0000-00-00','دولار الباهاما'),(50,10,7,'Barbados Dollars','BBD',1,'ADMIN','0000-00-00','ADMIN','0000-00-00','دولار البارابادوس'),(51,10,8,'Belgium Francs','BEF',1,'ADMIN','0000-00-00','ADMIN','0000-00-00','فرنك بلجيكي'),(52,10,9,'Bermuda Dollars','BMD',1,'ADMIN','0000-00-00','ADMIN','0000-00-00','دولار البرمودا'),(53,10,10,'Brazil Reals','BRL',1,'ADMIN','0000-00-00','ADMIN','0000-00-00','ريل برازيلي'),(54,10,11,'United Kingdom Pounds','GBP',1,'ADMIN','0000-00-00','ADMIN','0000-00-00','جنيه انجليزي'),(55,10,12,'Bulgaria Leva','BGL',1,'ADMIN','0000-00-00','ADMIN','0000-00-00','ليفا بلغاري'),(56,10,13,'Canada Dollars','CAD',1,'ADMIN','0000-00-00','ADMIN','0000-00-00','دولار كندي'),(57,10,14,'Chile Pesos','CLP',1,'ADMIN','0000-00-00','ADMIN','0000-00-00','بيزو تشيلي'),(58,10,15,'China Yuan Renminbi','CNY',1,'ADMIN','0000-00-00','ADMIN','0000-00-00','China Yuan Renminbi'),(59,10,16,'Cyprus Pounds','CYP',1,'ADMIN','0000-00-00','ADMIN','0000-00-00','جنيه قبرصي'),(60,10,17,'Czech Republic Koruny','CZK',1,'ADMIN','0000-00-00','ADMIN','0000-00-00','كورني جمهوريّة التّشيك'),(61,10,18,'Denmark Kroner','DKK',1,'ADMIN','0000-00-00','ADMIN','0000-00-00','كرونر دنماركي'),(62,10,19,'East Caribbean Dollars','XCD',1,'ADMIN','0000-00-00','ADMIN','0000-00-00','دولار كاريبي شرقي'),(63,10,20,'Egypt Pounds','EGP',1,'ADMIN','0000-00-00','ADMIN','0000-00-00','جنيه مصري'),(64,10,21,'Euros','EUR',1,'ADMIN','0000-00-00','ADMIN','0000-00-00','يورو'),(65,10,22,'Fiji Dollars','FJD',1,'ADMIN','0000-00-00','ADMIN','0000-00-00','دولار فيجي'),(66,10,23,'Finland Markkaa','FIM',1,'ADMIN','0000-00-00','ADMIN','0000-00-00','ماركا فنلندي'),(67,10,24,'France Francs','FRF',1,'ADMIN','0000-00-00','ADMIN','0000-00-00','فرنك فرنسي'),(68,10,25,'Germany Deutsche Marks','DEM',1,'ADMIN','0000-00-00','ADMIN','0000-00-00','مارك ألماني'),(69,10,26,'Gold Ounces','XAU',1,'ADMIN','0000-00-00','ADMIN','0000-00-00','وقية ذهبية'),(70,10,27,'Greece Drachmae','GRD',1,'ADMIN','0000-00-00','ADMIN','0000-00-00','دراخما يوناني'),(71,10,28,'Hong Kong Dollars','HKD',1,'ADMIN','0000-00-00','ADMIN','0000-00-00','دولار هونج كونج'),(72,10,29,'Hungary Forints','HUF',1,'ADMIN','0000-00-00','ADMIN','0000-00-00','فورينتس هنجاري'),(73,10,30,'Iceland Kronur','ISK',1,'ADMIN','0000-00-00','ADMIN','0000-00-00','كرونر أيسلندا'),(74,10,31,'India Rupees','INR',1,'ADMIN','0000-00-00','ADMIN','0000-00-00','روبيه هندية'),(75,10,32,'Indonesia Rupiahs','IDR',1,'ADMIN','0000-00-00','ADMIN','0000-00-00','روبيه أندونيسية'),(76,10,33,'Ireland Pounds','IEP',1,'ADMIN','0000-00-00','ADMIN','0000-00-00','جنيه ايرلندي'),(77,10,34,'Israel New Shekels','ILS',1,'ADMIN','0000-00-00','ADMIN','0000-00-00','شيكل اسرائيلي'),(78,10,35,'Italy Lire','ITL',1,'ADMIN','0000-00-00','ADMIN','0000-00-00','لاير ايطالي'),(79,10,36,'Jamaica Dollars','JMD',1,'ADMIN','0000-00-00','ADMIN','0000-00-00','دولار جامايكي'),(80,10,37,'Japan Yen','JPY',1,'ADMIN','0000-00-00','ADMIN','0000-00-00','ين ياباني'),(81,10,38,'Jordanian Dinar','JOD',1,'ADMIN','0000-00-00','ADMIN','0000-00-00','دينار أردني'),(82,10,39,'Lebanon Pounds','LBP',1,'ADMIN','0000-00-00','ADMIN','0000-00-00','ليرة لبنلني'),(83,10,40,'Luxembourg Francs','LUF',1,'ADMIN','0000-00-00','ADMIN','0000-00-00','فرنك لوكسمبورغ'),(84,10,41,'Malaysia Ringgits','MYR',1,'ADMIN','0000-00-00','ADMIN','0000-00-00','Malaysia Ringgits'),(85,10,42,'Mexico Pesos','MXP',1,'ADMIN','0000-00-00','ADMIN','0000-00-00','بيزو مكسيكي'),(86,10,43,'Netherlands Guilders','NLG',1,'ADMIN','0000-00-00','ADMIN','0000-00-00','جلدر هولندي'),(87,10,44,'New Zealand Dollars','NZD',1,'ADMIN','0000-00-00','ADMIN','0000-00-00','دولار نيوزيلندي'),(88,10,45,'Norway Kroner','NOK',1,'ADMIN','0000-00-00','ADMIN','0000-00-00','كرونر نرويجي'),(89,10,46,'Pakistan Rupees','PKR',1,'ADMIN','0000-00-00','ADMIN','0000-00-00','روبية باكستانية'),(90,10,47,'Philippines Pesos','PHP',1,'ADMIN','0000-00-00','ADMIN','0000-00-00','بيزو فلبينية'),(91,10,48,'Platinum Ounces','XPT',1,'ADMIN','0000-00-00','ADMIN','0000-00-00','وقية بلاتين'),(92,10,49,'Poland Zlotych','PLZ',1,'ADMIN','0000-00-00','ADMIN','0000-00-00','Poland Zlotych'),(93,10,50,'Portugal Escudos','PTE',1,'ADMIN','0000-00-00','ADMIN','0000-00-00','Portugal Escudos'),(94,10,51,'Romania Lei','ROL',1,'ADMIN','0000-00-00','ADMIN','0000-00-00','لي روماني'),(95,10,52,'Russia Rubles','RUR',1,'ADMIN','0000-00-00','ADMIN','0000-00-00','ربل روسي'),(96,10,53,'Saudi Arabia Riyals','SAR',1,'ADMIN','0000-00-00','ADMIN','0000-00-00','ريال سعودي'),(97,10,54,'Silver Ounces','XAG',1,'ADMIN','0000-00-00','ADMIN','0000-00-00','وقية فضة'),(98,10,55,'Singapore Dollars','SGD',1,'ADMIN','0000-00-00','ADMIN','0000-00-00','دولار سنغافوري'),(99,10,56,'Slovakia Koruny','SKK',1,'ADMIN','0000-00-00','ADMIN','0000-00-00','كورني سلوفاكي'),(100,10,57,'South Africa Rand','ZAR',1,'ADMIN','0000-00-00','ADMIN','0000-00-00','راند جنوب افريقيا'),(101,10,58,'South Korea Won','KRW',1,'ADMIN','0000-00-00','ADMIN','0000-00-00','ون كوريا الجنوبية'),(102,10,59,'Spain Pesetas','ESP',1,'ADMIN','0000-00-00','ADMIN','0000-00-00','بيزيتا اسبانية'),(103,10,60,'Sudan Dinars','SDG',1,'ADMIN','0000-00-00','ADMIN','0000-00-00','دينار سوداني'),(104,10,61,'Sweden Kronor','SEK',1,'ADMIN','0000-00-00','ADMIN','0000-00-00','كرونر سويدي'),(105,10,62,'Switzerland Francs','CHF',1,'ADMIN','0000-00-00','ADMIN','0000-00-00','فرنك سويسري'),(106,10,63,'Taiwan New Dollars','TWD',1,'ADMIN','0000-00-00','ADMIN','0000-00-00','دولار تايواني'),(107,10,64,'Thailand Baht','THB',1,'ADMIN','0000-00-00','ADMIN','0000-00-00','باهت تايلندي'),(108,10,65,'Turkey Liras','TRL',1,'ADMIN','0000-00-00','ADMIN','0000-00-00','ليرة تركي'),(109,10,66,'Venezuela Bolivares','VEB',1,'ADMIN','0000-00-00','ADMIN','0000-00-00','بوليفر فنزويلي'),(110,10,67,'Zambia Kwacha','ZMK',1,'ADMIN','0000-00-00','ADMIN','0000-00-00','كواخا زامبي'),(111,10,68,'Dollar (Liberian)','LRD',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(112,10,69,'Afghani','J',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(113,10,70,'Dollar (Namibian)','NAD',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(114,10,71,'Balboa','BAP',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(115,10,72,'Birr','ETB',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(116,10,73,'Boliviano','BOB',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(117,10,74,'Cedi','GHC',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(118,10,75,'Colon (Costa Rican)','CRC',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(119,10,76,'Colon (El Salvadorian)','SVC',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(120,10,77,'Cordoba','NIC',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(121,10,78,'Dalasi','GMD',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(122,10,79,'Bahraini Dinar','BHD',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(123,10,80,'Dinar (Bosnian)','BAD',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(124,10,81,'Dinar (Croatian)','HRD',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(125,10,82,'Dinar (Iraqi)','IQD',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(126,10,83,'Dinar (Kuwaiti)','KWD',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(127,10,84,'Dinar (Libyan)','LYD',1,'ADMIN','0000-00-00','ADMIN','0000-00-00','دينار ليبي'),(128,10,85,'Dinar (Yugoslavian New)','YUD',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(129,10,86,'Dinar (Tunisian)','TND',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(130,10,87,'Riyal (Yemeni)','YER',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(131,10,88,'Dirham (Moroccan)','MAD',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(132,10,89,'Dirham (UAE)','AED',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(133,10,90,'Dollar (Belize)','BZD',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(134,10,91,'Dollar (Brunei)','BND',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(135,10,92,'Dollar (Guyana)','GYD',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(136,10,93,'Dollar (Solomon Islands)','SBD',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(137,10,94,'Dollar (Zimbabwe)','SWD',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(138,10,95,'Dong','VND',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(139,10,96,'Dram','AMD',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(140,10,97,'Ekwele','GQE',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(141,10,98,'Escudo (Timorian)','TPE',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(142,10,99,'Franc','XAF',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(143,10,100,'Franc','XPF',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(144,10,101,'Franc (Burkina Faso)','BFF',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(145,10,102,'Franc (Burundi)','BIF',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(146,10,103,'Franc (Comorian)','KMF',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(147,10,104,'Franc (Djibouti)','DJF',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(148,10,105,'Franc (Guinea)','GNS',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(149,10,106,'Franc (Malagasy)','MGF',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(150,10,107,'Franc (Malian)','MLF',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(151,10,108,'Franc (Rwanda)','RWF',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(152,10,109,'Franc (West African)','XOF',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(153,10,110,'Gourde','HTG',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(154,10,111,'Guarani','PYG',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(155,10,112,'Guilder (Aruban)','AWG',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(156,10,113,'Guilder (Surinam)','SRG',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(157,10,114,'Hryvna','UAH',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(158,10,115,'Inti','PEI',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(159,10,116,'Karbovanet','UAK',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(160,10,117,'Koruna (Czech)','CSK',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(161,10,118,'Kroon','EEK',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(162,10,119,'Kwacha (Malawian)','MWK',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(163,10,120,'Kyat','MMK',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(164,10,121,'Lat','LVL',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(165,10,122,'Lek','ALL',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(166,10,123,'Lempira','HNL',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(167,10,124,'Leone','SLL',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(168,10,125,'Leu (Moldavian)','MDL',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(169,10,126,'Manat (Azerbaijani)','AZM',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(170,10,127,'Manat (Turkmenistani)','TMM',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(171,10,128,'Metical','MZM',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(172,10,129,'Nakfa','ERN',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(173,10,130,'Nigerian Naira','NGN',1,'ADMIN','0000-00-00','ADMIN','0000-00-00','Nigerian Naira'),(174,10,131,'New Kwanza','AON',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(175,10,132,'New Peso (Uruguayan)','UYU',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(176,10,133,'New Sol','PEN',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(177,10,134,'Ngultrum','BTN',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(178,10,135,'Paanga','TOP',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(179,10,136,'Peseta (Andorran)','ADP',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(180,10,137,'Peso (Bolivian)','BOP',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(181,10,138,'Peso (Colombian)','COP',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(182,10,139,'Peso (Cuban)','CUP',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(183,10,140,'Peso (Dominican Republic)','DOP',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(184,10,141,'Pound (Falkland)','FKP',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(185,10,142,'Pound (Syrian)','SYP',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(186,10,143,'Pula','BWP',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(187,10,144,'Quetzal','GTQ',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(188,10,145,'Rial (Iranian)','IRR',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(189,10,146,'Rial (Omani)','OMR',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(190,10,147,'Riel','KHR',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(191,10,148,'Riyal (Qatari)','QAR',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(192,10,149,'Rouble (Belarussian)','BYR',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(193,10,150,'Rouble (Tajik)','TJR',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(194,10,151,'Rufiyaa','MVR',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(195,10,152,'Rupee (Mauritius)','MUR',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(196,10,153,'Rupee (Nepalese)','NPR',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(197,10,154,'Rupee (Sri Lankan)','LKR',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(198,10,155,'Shilling (Kenyan)','KES',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(199,10,156,'Shilling (Somali)','SOS',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(200,10,157,'Shilling (Tanzanian)','TZS',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(201,10,158,'Shilling (Ugandan)','UGS',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(202,10,159,'Som (Kyrgyzstani)','KGS',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(203,10,160,'Som (Uzbekistani)','UZS',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(204,10,161,'Sucre','ECS',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(205,10,162,'Taka','BDT',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(206,10,163,'Tala','WST',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(207,10,164,'Tenge','KZT',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(208,10,165,'Tolar','SIT',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(209,10,166,'Tugrik','MNT',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(210,10,167,'Vatu','VUV',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(211,10,168,'Won (North Korean)','KPW',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(212,10,169,'Zaire (New)','CDZ',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(213,10,170,'Escudo (Caboverdiano)','CVE',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(214,10,171,'Dollar (Cayman Islands)','KYD',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(215,10,172,'Lari','GEL',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(216,10,173,'Pound (Gibraltar)','GIP',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(217,10,174,'Peso (Guinea-Bissau)','GWP',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(218,10,175,'Kip','LAK',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(219,10,176,'Loti','LSL',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(220,10,177,'Litas','LTL',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(221,10,178,'Pataca','MOP',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(222,10,179,'Dinar (Macedonian)','MKD',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(223,10,180,'Lira (Maltese)','MTL',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(224,10,181,'Ouguiya','MRO',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(225,10,182,'Guilder (Netherlands Antilles)','ANG',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(226,10,183,'Balboa','PAB',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(227,10,184,'Kina','PGK',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(228,10,185,'Pound (St Helena)','SHP',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(229,10,186,'Rupee (Seychelles)','SCR',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(230,10,187,'Lilangeni','SZL',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(231,10,188,'Dollar (Trinidad and Tobago)','TTD',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(232,10,189,'Dollar (Zimbabwe)','ZWD',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(233,10,190,'SDR Clearing Currency','SDR',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(234,10,191,'Euro','EURO',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(235,10,192,'United Arab Emarates','UAE',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(236,10,193,'farah','farah',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(237,10,194,'Lebanese Lira','LL',1,'ADMIN','0000-00-00','ADMIN','0000-00-00',''),(238,1,3,'Technical/Specialist','Tech',1,'admin','2019-11-02','mohannad','2019-11-02','التقنية'),(239,1,4,'Others','Oth',1,'amin','2019-11-02','mohannad','2019-11-02','الأخرى'),(240,11,0,'Planning','0',1,'admin','2019-11-02','',NULL,'التخطيط'),(241,11,1,'Review','1',1,'admin','2019-11-02',NULL,NULL,'المراجعة'),(242,11,2,'Final Evaluation','2',1,'admin','2019-11-02',NULL,NULL,'النهائية'),(243,11,3,'Completed','3',1,'admin','2019-11-02',NULL,NULL,'اكتمال التقييم');
/*!40000 ALTER TABLE `adm_codes` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `adm_company`
--

DROP TABLE IF EXISTS `adm_company`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
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
  `mission` longtext,
  `vision` longtext,
  `currency_c` int(11) NOT NULL DEFAULT '1',
  `projects_link` int(11) NOT NULL DEFAULT '1',
  `plan_link` int(11) NOT NULL DEFAULT '1',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `adm_company`
--

LOCK TABLES `adm_company` WRITE;
/*!40000 ALTER TABLE `adm_company` DISABLE KEYS */;
INSERT INTO `adm_company` VALUES (1,'Eskadenia','MAN','Amman','null','null','null','null','null','admin','2019-09-17','admin','2019-11-13','اسكدنيا','<p>Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry&#39;s standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.</p>\n','<p>Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry&#39;s standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.</p>\n',1,1,1);
/*!40000 ALTER TABLE `adm_company` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `adm_company_obj_performance`
--

DROP TABLE IF EXISTS `adm_company_obj_performance`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
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
  `actual_cost` float(12,3) DEFAULT NULL,
  `planned_cost` float(12,3) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `adm_company_obj_performance`
--

LOCK TABLES `adm_company_obj_performance` WRITE;
/*!40000 ALTER TABLE `adm_company_obj_performance` DISABLE KEYS */;
INSERT INTO `adm_company_obj_performance` VALUES (1,2019,1,65,1.300,5.200,45.500,9.750,3.250,NULL,NULL,NULL,NULL,NULL,76.857,778900.000,2066800.000);
/*!40000 ALTER TABLE `adm_company_obj_performance` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `adm_competencies`
--

DROP TABLE IF EXISTS `adm_competencies`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
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
  `is_mandetory` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`),
  KEY `fk_adm_competencies_adm_codes` (`c_nature_id`),
  KEY `fk_adm_competencies_adm_company` (`company_id`),
  CONSTRAINT `fk_adm_competencies_adm_company_0` FOREIGN KEY (`company_id`) REFERENCES `adm_company` (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=19 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `adm_competencies`
--

LOCK TABLES `adm_competencies` WRITE;
/*!40000 ALTER TABLE `adm_competencies` DISABLE KEYS */;
INSERT INTO `adm_competencies` VALUES (4,'2','Leadership and Inspiration of Others',2,1,'ADMIN','2019-10-13','admin','2019-10-31',NULL,1),(5,'3','Serving the community and responding to the needs of its customers',2,1,'ADMIN','2019-10-13','admin','2019-10-13','خدمة المجتمع والاستجابة لاحتياجات المتعاملين من أفراده ',1),(6,'4','Excellence in implementation',1,1,'ADMIN','2019-10-13','admin','2019-11-24',NULL,0),(7,'5','Awareness of Financial Affairs',1,1,'ADMIN','2019-10-13','admin','2019-10-31',NULL,1),(10,'01','Effective Communications',1,1,'admin','2019-10-28','admin','2019-10-31',NULL,1),(11,'01','Recalling & Spreading Positive Energy',1,1,'admin','2019-10-31',NULL,NULL,NULL,1),(12,'01',' Community Service and Response to The Clients\' Needs',1,1,'admin','2019-10-31',NULL,NULL,NULL,1),(13,'01','Innovation and Creativity',1,1,'admin','2019-10-31','admin','2019-10-31',NULL,1),(14,'01',' Organizational Understanding',1,1,'admin','2019-10-31',NULL,NULL,NULL,1),(15,'01',' Supporting and Enabling Change',2,1,'admin','2019-10-31',NULL,NULL,NULL,1),(16,'01','Make Sound Decisions and Judgments (Additional)',2,1,'admin','2019-10-31','admin','2019-10-31',NULL,1),(17,'01','Employee Development (Additional)',2,1,'admin','2019-10-31',NULL,NULL,NULL,1),(18,'01','Strategic Thinking (Additional)',2,1,'admin','2019-10-31',NULL,NULL,NULL,1);
/*!40000 ALTER TABLE `adm_competencies` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `adm_competencies_kpi`
--

DROP TABLE IF EXISTS `adm_competencies_kpi`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `adm_competencies_kpi` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(1000) NOT NULL,
  `c_kpi_type_id` int(11) NOT NULL,
  `competence_id` int(11) NOT NULL,
  `created_by` varchar(100) NOT NULL,
  `created_date` date NOT NULL,
  `modified_by` varchar(100) DEFAULT NULL,
  `modified_date` date DEFAULT NULL,
  `name2` varchar(2000) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_adm_competencies_kpi_adm_competencies` (`competence_id`),
  CONSTRAINT `fk_adm_competencies_kpi_adm_competencies` FOREIGN KEY (`competence_id`) REFERENCES `adm_competencies` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=56 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `adm_competencies_kpi`
--

LOCK TABLES `adm_competencies_kpi` WRITE;
/*!40000 ALTER TABLE `adm_competencies_kpi` DISABLE KEYS */;
INSERT INTO `adm_competencies_kpi` VALUES (10,'Demonstrates confidence in his ability to perform his job successfully and highlights a yearning for greater and more challenging tasks and responsibilities.',2,4,'Admin','2019-10-13','Admin','2019-10-13',' يبدي قدراً من الثقة في قدرته على أداء مهام وظيفته بنجاح ويبرز توقاً لمهام ومسؤوليات أكبر وأكثر تحدياً. '),(12,'relies heavily on his own abilities and often finishes its tasks within the set time or early, contributing new ideas that add value to the department\'s activities.',2,4,'Admin','2019-10-13','Admin','2019-10-13',' يعتمد بشكل كبير على قدراته الذاتية وغالباً ما ينهي مهامه ضمن الوقت المحدد أو بشكل مبكر، يساهم بأفكار جديدة تضيف قيمة لأنشطة الدائرة.'),(13,'Demonstrates great cooperation with colleagues and clients in solving work problems and exhibits significant flexibility in adapting to change in workloads and responsibilities, encouraging colleagues and assisting them in performing tasks that may not be within their competence.',2,4,'Admin','2019-10-13','Admin','2019-10-13',' يبدي تعاوناً كبيراً مع زملائه ومع المتعاملين في حل مشكلات العمل ، ويظهر قدراً مهما من المرونة في التكييف مع التغيير في أعباء العمل والمسؤوليات، يشجع زملائه ويقدم لهم المساعدة في تأدية مهام قد لا تكون من ضمن إختصاصه.'),(14,'He shares knowledge and experience with his colleagues and plays the role of mentor and mentor to his newly joined teammates, and contributes appreciably to the qualification of those who follow him in his job or replace him.',2,4,'Admin','2019-10-13','Admin','2019-10-13',' يتشاطر المعرفة والتجارب مع زملائه ويلعب دور المرشد والموجه لزملائه المنضمين حديثاً لفريق عمله، ويساهم بشكل مقدر في تأهيل من يعقبه في وظيفته أو يحل محله.'),(15,'Demonstrates acceptance of challenges, criticism and differences, and adapts his behavior and methods accordingly without affecting the stability of his performance.',2,5,'Admin','2019-10-13','Admin','2019-10-13',' يظهر تقبلا للتحديات والنقد والإختلافات، ويكيف سلوكه وأساليب عمله وفقاً لذلك دونما تأثير على ثبات أدائه.'),(16,'Demonstrates respect for diversity and differences, and can work efficiently within a diverse and less homogeneous team.',2,5,'Admin','2019-10-13','Admin','2019-10-13','يظهر إحتراماً للتنوع والإختلافات، ويمكنه العمل بكفاءه ضمن  فريق متنوع وأقل تجانساً.'),(22,'kpi 1',1,7,'admin','2019-10-24',NULL,NULL,NULL),(23,'kpi 2',1,7,'admin','2019-10-24',NULL,NULL,NULL),(24,'midlle kpi 1',2,7,'admin','2019-10-24',NULL,NULL,NULL),(25,'advance kpi 1',3,7,'admin','2019-10-24',NULL,NULL,NULL),(27,'Communication  skill KPI (BIginner)',1,10,'admin','2019-10-28',NULL,NULL,NULL),(28,'Communication  skill KPI (MIddle)',2,10,'admin','2019-10-28',NULL,NULL,NULL),(29,'Communication  skill KPI (ADvance)',3,10,'admin','2019-10-28',NULL,NULL,NULL),(30,'Communication  skill KPI (adv ) 2',3,10,'admin','2019-10-28',NULL,NULL,NULL),(32,'1. Considers the highest quality standards in his/ her work.',1,6,'admin','2019-11-01','admin','2019-11-01',NULL),(33,'2. Verifies the accuracy and validity of data and documents honestly and conscientiously.',1,6,'admin','2019-11-01',NULL,NULL,NULL),(34,'3. Looks for ways to improve the provision of services/ operations and never gives in case of any failures.',1,6,'admin','2019-11-01',NULL,NULL,NULL),(35,'4. Regularly checks the progress of work according to the specific quality standards.',1,6,'admin','2019-11-01',NULL,NULL,NULL),(36,'5. Accepts the comments of others on the existing projects, procedures and policies to apply the necessary improvements.',1,6,'admin','2019-11-01',NULL,NULL,NULL),(37,'6. Follows a structured approach in work and show interest in prioritizing.',1,6,'admin','2019-11-01',NULL,NULL,NULL),(38,'7. Wisely uses time and resources, and is always aware of his/ her work developments.',1,6,'admin','2019-11-01',NULL,NULL,NULL),(39,'8. Accepts responsibility for performance at a level that meets the performance expectations.',1,6,'admin','2019-11-01',NULL,NULL,NULL),(40,'1. Predicts the problems that may affect the quality of services provided',2,6,'admin','2019-11-01',NULL,NULL,NULL),(41,'2. Supports others in their quest for excellence.',2,6,'admin','2019-11-01',NULL,NULL,NULL),(42,'3. Systematically applies The lessons learned from past experiences regarding quality.',2,6,'admin','2019-11-01',NULL,NULL,NULL),(43,'4. Works to find radical solutions rather than temporary solutions that will not improve quality on the long term.',2,6,'admin','2019-11-01',NULL,NULL,NULL),(44,'5. Organizes individuals work and assigns tasks/ responsibilities to them in a manner that ensures the achievement of multiple objectives.',2,6,'admin','2019-11-01',NULL,NULL,NULL),(45,'6. Clarifies objectives, sets priorities and constructively directs others to maintain focus.',2,6,'admin','2019-11-01',NULL,NULL,NULL),(46,'7. Encourages others to use planning mechanisms and tools to facilitate and ensure the success of work.',2,6,'admin','2019-11-01',NULL,NULL,NULL),(47,'1. Bears the responsibility with his/ her team of achieving high quality results.',3,6,'admin','2019-11-01',NULL,NULL,NULL),(48,'2. Adopts or develops methods for applying high quality standards (such as forming high-performance teams and investing in quality).',3,6,'admin','2019-11-01',NULL,NULL,NULL),(49,'3. Sets clear goals for himself/ herself and his/ her team to achieve superior performance, and measures results according to the highest standards.',3,6,'admin','2019-11-01',NULL,NULL,NULL),(50,'4. Rewards individuals and teams for excellence.',3,6,'admin','2019-11-01',NULL,NULL,NULL),(51,'5. Makes staff responsible for measuring the accuracy and quality of their work, as well as the work of others.',3,6,'admin','2019-11-01',NULL,NULL,NULL),(52,'6. Oversees the efficiency of employing and exploiting resources.',3,6,'admin','2019-11-01',NULL,NULL,NULL),(53,'7. Ensures that planning and resource allocation processes are properly completed before the initiation of the major projects.',3,6,'admin','2019-11-01',NULL,NULL,NULL),(54,'8. Is capable of forming and leading overlapping-task teams as well as organizing key tasks and following up the overall performance.',3,6,'admin','2019-11-01',NULL,NULL,NULL),(55,'111',1,4,'admin','2019-11-02',NULL,NULL,NULL);
/*!40000 ALTER TABLE `adm_competencies_kpi` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `adm_emp_assesment`
--

DROP TABLE IF EXISTS `adm_emp_assesment`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `adm_emp_assesment` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `year_id` int(11) NOT NULL,
  `c_kpi_cycle` int(11) NOT NULL,
  `agreement_date` date NOT NULL,
  `emp_reviewer_id` int(11) DEFAULT NULL,
  `created_by` varchar(50) NOT NULL,
  `created_date` date NOT NULL,
  `modified_by` varchar(50) DEFAULT NULL,
  `modified_date` date DEFAULT NULL,
  `attachment` varchar(1000) DEFAULT NULL,
  `emp_position_id` int(11) NOT NULL,
  `target` float(18,3) NOT NULL,
  `objectives_weight` float(18,3) DEFAULT NULL,
  `competencies_weight` float(18,3) DEFAULT NULL,
  `objectives_result` float(18,3) DEFAULT NULL,
  `competencies_result` float(18,3) DEFAULT NULL,
  `objectives_weight_result` float(18,3) DEFAULT NULL,
  `competencies_weight_result` float(18,3) DEFAULT NULL,
  `result_before_round` float(18,3) DEFAULT NULL,
  `result_after_round` float(18,3) DEFAULT NULL,
  `employee_id` int(11) NOT NULL,
  `objectives_result_after_round` float(18,3) DEFAULT NULL,
  `competencies_result_after_round` float(18,3) DEFAULT NULL,
  `objectives_weight_result_after_round` float(18,3) DEFAULT NULL,
  `competencies_weight_result_after_round` float(18,3) DEFAULT NULL,
  `status` int(11) NOT NULL DEFAULT '0',
  `isQuarter1` int(11) NOT NULL DEFAULT '0',
  `isQuarter2` int(11) NOT NULL DEFAULT '0',
  `isQuarter3` int(11) NOT NULL DEFAULT '0',
  `isQuarter4` int(11) NOT NULL DEFAULT '0',
  `emp_manager_id` int(11) DEFAULT NULL,
  `final_date` datetime DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_adm_emp_assesment_adm_employees` (`emp_reviewer_id`),
  KEY `fk_adm_emp_assesment_adm_years` (`year_id`),
  KEY `fk_adm_emp_assesment_adm_employees_0` (`employee_id`),
  CONSTRAINT `fk_adm_emp_assesment_adm_employees` FOREIGN KEY (`emp_reviewer_id`) REFERENCES `adm_employees` (`ID`),
  CONSTRAINT `fk_adm_emp_assesment_adm_employees_0` FOREIGN KEY (`employee_id`) REFERENCES `adm_employees` (`ID`),
  CONSTRAINT `fk_adm_emp_assesment_adm_years` FOREIGN KEY (`year_id`) REFERENCES `adm_years` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=17 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `adm_emp_assesment`
--

LOCK TABLES `adm_emp_assesment` WRITE;
/*!40000 ALTER TABLE `adm_emp_assesment` DISABLE KEYS */;
INSERT INTO `adm_emp_assesment` VALUES (14,2019,2,'2019-10-28',2,'admin','2019-10-31','admin','2019-10-31','null',1,5.000,NULL,NULL,3.150,2.450,3.150,2.450,2.500,3.000,24,3.000,2.000,3.000,2.000,0,0,0,0,0,NULL,NULL),(15,2019,3,'2019-02-06',NULL,'admin','2019-11-01','admin','2019-11-01','null',1,5.000,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,2,NULL,NULL,NULL,NULL,0,0,0,0,0,NULL,NULL),(16,2019,2,'2019-01-09',40,'admin','2019-11-01',NULL,NULL,NULL,1,4.000,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,90,NULL,NULL,NULL,NULL,0,0,0,0,0,NULL,NULL);
/*!40000 ALTER TABLE `adm_emp_assesment` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `adm_emp_comp_ass`
--

DROP TABLE IF EXISTS `adm_emp_comp_ass`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `adm_emp_comp_ass` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `period_no` int(11) NOT NULL,
  `emp_competency_id` int(11) NOT NULL,
  `result_before_round` float(18,3) DEFAULT NULL,
  `result_after_round` float(18,3) DEFAULT NULL,
  `created_by` varchar(45) NOT NULL,
  `created_date` date NOT NULL,
  `modified_by` varchar(45) DEFAULT NULL,
  `modified_date` date DEFAULT NULL,
  `target` float(18,3) NOT NULL,
  `weight_result_without_round` float(18,3) DEFAULT NULL,
  `weight_result_after_round` float(18,3) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `adm_emp_comp_ass_adm_emp_competency_idx` (`emp_competency_id`),
  CONSTRAINT `adm_emp_comp_ass_adm_emp_competency` FOREIGN KEY (`emp_competency_id`) REFERENCES `adm_emp_competency` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=16 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `adm_emp_comp_ass`
--

LOCK TABLES `adm_emp_comp_ass` WRITE;
/*!40000 ALTER TABLE `adm_emp_comp_ass` DISABLE KEYS */;
INSERT INTO `adm_emp_comp_ass` VALUES (6,2,9,0.000,80.000,'admin','2019-10-28','ADMIN','2019-10-31',100.000,0.000,0.000),(7,4,9,0.000,15.000,'admin','2019-10-28','ADMIN','2019-10-31',100.000,0.000,0.000),(8,2,10,0.000,25.000,'admin','2019-10-28','ADMIN','2019-10-31',100.000,0.000,0.000),(9,4,10,0.000,55.000,'admin','2019-10-28','ADMIN','2019-10-31',100.000,0.000,0.000),(10,2,11,17.500,55.000,'admin','2019-10-28','ADMIN','2019-10-31',100.000,3.600,0.000),(11,4,11,20.000,63.000,'admin','2019-10-28','ADMIN','2019-10-31',100.000,4.000,0.000),(12,2,12,0.000,0.000,'admin','2019-10-29',NULL,NULL,6.000,0.000,0.000),(13,4,12,0.000,0.000,'admin','2019-10-29',NULL,NULL,6.000,0.000,0.000),(14,2,13,3.500,4.000,'admin','2019-10-29','ADMIN','2019-11-01',10.000,40.000,0.000),(15,4,13,3.250,3.000,'admin','2019-10-29','ADMIN','2019-11-01',10.000,30.000,0.000);
/*!40000 ALTER TABLE `adm_emp_comp_ass` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `adm_emp_comp_kpi_ass`
--

DROP TABLE IF EXISTS `adm_emp_comp_kpi_ass`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `adm_emp_comp_kpi_ass` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `period_no` int(11) NOT NULL,
  `emp_competency_kpi_id` int(11) NOT NULL,
  `result` float(18,3) DEFAULT NULL,
  `created_by` varchar(45) NOT NULL,
  `created_date` date NOT NULL,
  `modified_by` varchar(45) DEFAULT NULL,
  `modified_date` date DEFAULT NULL,
  `target` float(18,3) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `adm_emp_comp_kpi_ass_adm_emp_competency_kpi_idx` (`emp_competency_kpi_id`),
  CONSTRAINT `adm_emp_comp_kpi_ass_adm_emp_competency_kpi` FOREIGN KEY (`emp_competency_kpi_id`) REFERENCES `adm_emp_competency_kpi` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=40 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `adm_emp_comp_kpi_ass`
--

LOCK TABLES `adm_emp_comp_kpi_ass` WRITE;
/*!40000 ALTER TABLE `adm_emp_comp_kpi_ass` DISABLE KEYS */;
INSERT INTO `adm_emp_comp_kpi_ass` VALUES (14,2,10,0.000,'admin','2019-10-28',NULL,NULL,100.000),(15,4,10,0.000,'admin','2019-10-28',NULL,NULL,100.000),(16,2,11,0.000,'admin','2019-10-28',NULL,NULL,100.000),(17,4,11,0.000,'admin','2019-10-28',NULL,NULL,100.000),(18,2,12,0.000,'admin','2019-10-28',NULL,NULL,100.000),(19,4,12,0.000,'admin','2019-10-28',NULL,NULL,100.000),(20,2,13,0.000,'admin','2019-10-28',NULL,NULL,100.000),(21,4,13,0.000,'admin','2019-10-28',NULL,NULL,100.000),(22,2,14,70.000,'admin','2019-10-28','ADMIN','2019-10-28',100.000),(23,4,14,80.000,'admin','2019-10-28','ADMIN','2019-10-28',100.000),(24,2,15,0.000,'admin','2019-10-28',NULL,NULL,100.000),(25,4,15,0.000,'admin','2019-10-28',NULL,NULL,100.000),(26,2,16,0.000,'admin','2019-10-28',NULL,NULL,100.000),(27,4,16,0.000,'admin','2019-10-28',NULL,NULL,100.000),(28,2,17,0.000,'admin','2019-10-28',NULL,NULL,100.000),(29,4,17,0.000,'admin','2019-10-28',NULL,NULL,100.000),(30,2,18,0.000,'admin','2019-10-29',NULL,NULL,0.000),(31,4,18,0.000,'admin','2019-10-29',NULL,NULL,0.000),(32,2,19,4.000,'admin','2019-10-29','ADMIN','2019-11-01',0.000),(33,4,19,4.000,'admin','2019-10-29','ADMIN','2019-11-01',0.000),(34,2,20,4.000,'admin','2019-10-29','ADMIN','2019-11-01',0.000),(35,4,20,3.000,'admin','2019-10-29','ADMIN','2019-11-01',0.000),(36,2,21,3.000,'admin','2019-10-29','ADMIN','2019-11-01',0.000),(37,4,21,3.000,'admin','2019-10-29','ADMIN','2019-11-01',0.000),(38,2,22,3.000,'admin','2019-10-29','ADMIN','2019-11-01',0.000),(39,4,22,3.000,'admin','2019-10-29','ADMIN','2019-11-01',0.000);
/*!40000 ALTER TABLE `adm_emp_comp_kpi_ass` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `adm_emp_competency`
--

DROP TABLE IF EXISTS `adm_emp_competency`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `adm_emp_competency` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `competency_id` int(11) NOT NULL,
  `competency_level_id` int(11) DEFAULT NULL,
  `weight` float(18,3) DEFAULT NULL,
  `result_without_round` float(18,3) DEFAULT NULL,
  `target` float(18,3) NOT NULL,
  `result_after_round` float(18,3) DEFAULT NULL,
  `emp_assessment_id` int(11) NOT NULL,
  `created_by` varchar(45) NOT NULL,
  `created_date` date NOT NULL,
  `modified_by` varchar(45) DEFAULT NULL,
  `modified_date` date DEFAULT NULL,
  `project_desc` varchar(500) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `admin_emp_comp_adm_assessment_idx` (`emp_assessment_id`),
  KEY `adm_emp_comp_adm_comp_idx` (`competency_id`),
  CONSTRAINT `adm_emp_comp_adm_comp` FOREIGN KEY (`competency_id`) REFERENCES `adm_competencies` (`id`),
  CONSTRAINT `admin_emp_comp_adm_assessment` FOREIGN KEY (`emp_assessment_id`) REFERENCES `adm_emp_assesment` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `adm_emp_competency`
--

LOCK TABLES `adm_emp_competency` WRITE;
/*!40000 ALTER TABLE `adm_emp_competency` DISABLE KEYS */;
INSERT INTO `adm_emp_competency` VALUES (9,7,1,50.000,47.500,5.000,48.000,14,'admin','2019-10-28',NULL,NULL,NULL),(10,10,3,30.000,40.000,5.000,40.000,14,'admin','2019-10-28',NULL,NULL,NULL),(11,4,2,20.000,59.000,5.000,59.000,14,'admin','2019-10-28',NULL,NULL,NULL),(12,7,3,11.000,0.000,5.000,0.000,15,'admin','2019-10-29',NULL,NULL,NULL),(13,4,2,10.000,3.500,5.000,4.000,15,'admin','2019-10-29',NULL,NULL,NULL);
/*!40000 ALTER TABLE `adm_emp_competency` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `adm_emp_competency_kpi`
--

DROP TABLE IF EXISTS `adm_emp_competency_kpi`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `adm_emp_competency_kpi` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `emp_competency_id` int(11) NOT NULL,
  `competency_kpi_id` int(11) NOT NULL,
  `created_by` varchar(45) DEFAULT NULL,
  `created_date` date NOT NULL,
  `target` float(18,3) DEFAULT '5.000',
  PRIMARY KEY (`id`),
  KEY `adm_emp_comp_kpi_adm_comp_id_idx` (`emp_competency_id`),
  KEY `adm_emp_comp_kpi_adm_comp_kpi_idx` (`competency_kpi_id`),
  CONSTRAINT `adm_emp_comp_kpi_adm_comp` FOREIGN KEY (`emp_competency_id`) REFERENCES `adm_emp_competency` (`id`),
  CONSTRAINT `adm_emp_comp_kpi_adm_comp_kpi` FOREIGN KEY (`competency_kpi_id`) REFERENCES `adm_competencies_kpi` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=23 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `adm_emp_competency_kpi`
--

LOCK TABLES `adm_emp_competency_kpi` WRITE;
/*!40000 ALTER TABLE `adm_emp_competency_kpi` DISABLE KEYS */;
INSERT INTO `adm_emp_competency_kpi` VALUES (10,9,22,'admin','2019-10-28',5.000),(11,9,23,'admin','2019-10-28',5.000),(12,10,29,'admin','2019-10-28',5.000),(13,10,30,'admin','2019-10-28',5.000),(14,11,10,'admin','2019-10-28',5.000),(15,11,12,'admin','2019-10-28',5.000),(16,11,13,'admin','2019-10-28',5.000),(17,11,14,'admin','2019-10-28',5.000),(18,12,25,'admin','2019-10-29',5.000),(19,13,10,'admin','2019-10-29',5.000),(20,13,12,'admin','2019-10-29',5.000),(21,13,13,'admin','2019-10-29',5.000),(22,13,14,'admin','2019-10-29',5.000);
/*!40000 ALTER TABLE `adm_emp_competency_kpi` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `adm_emp_obj_ass`
--

DROP TABLE IF EXISTS `adm_emp_obj_ass`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `adm_emp_obj_ass` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `period_no` int(11) NOT NULL,
  `emp_objective_id` int(11) NOT NULL,
  `result_before_round` float(18,3) DEFAULT NULL,
  `result_after_round` float(18,3) DEFAULT NULL,
  `created_by` varchar(45) NOT NULL,
  `created_date` date NOT NULL,
  `modified_by` varchar(45) DEFAULT NULL,
  `modified_date` date DEFAULT NULL,
  `target` float(18,3) NOT NULL,
  `weight_result_without_round` float(18,3) DEFAULT NULL,
  `weight_result_after_round` float(18,3) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `adm_emp_obj_ass_adm_emp_obj_idx` (`emp_objective_id`),
  CONSTRAINT `adm_emp_obj_ass_adm_emp_objective` FOREIGN KEY (`emp_objective_id`) REFERENCES `adm_emp_objective` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=23 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `adm_emp_obj_ass`
--

LOCK TABLES `adm_emp_obj_ass` WRITE;
/*!40000 ALTER TABLE `adm_emp_obj_ass` DISABLE KEYS */;
INSERT INTO `adm_emp_obj_ass` VALUES (9,2,10,48.000,75.000,'admin','2019-10-28','ADMIN','2019-10-31',100.000,24.000,0.000),(10,4,10,47.500,80.000,'admin','2019-10-28','ADMIN','2019-10-31',100.000,24.000,0.000),(11,2,11,0.000,58.000,'admin','2019-10-28','ADMIN','2019-10-31',100.000,0.000,0.000),(12,4,11,0.000,37.000,'admin','2019-10-28','ADMIN','2019-10-31',100.000,0.000,0.000),(13,2,12,0.000,0.000,'admin','2019-10-29',NULL,NULL,3.000,0.000,0.000),(14,4,12,0.000,0.000,'admin','2019-10-29',NULL,NULL,3.000,0.000,0.000),(15,2,13,0.000,0.000,'admin','2019-11-01',NULL,NULL,25.000,0.000,0.000),(16,4,13,0.000,0.000,'admin','2019-11-01',NULL,NULL,25.000,0.000,0.000),(17,2,14,0.000,0.000,'admin','2019-11-01',NULL,NULL,0.000,0.000,0.000),(18,4,14,0.000,0.000,'admin','2019-11-01',NULL,NULL,0.000,0.000,0.000);
/*!40000 ALTER TABLE `adm_emp_obj_ass` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `adm_emp_obj_kpi`
--

DROP TABLE IF EXISTS `adm_emp_obj_kpi`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `adm_emp_obj_kpi` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(1000) NOT NULL,
  `name2` varchar(1000) DEFAULT NULL,
  `created_by` varchar(50) NOT NULL,
  `created_date` date NOT NULL,
  `modified_by` varchar(50) DEFAULT NULL,
  `modified_date` date DEFAULT NULL,
  `emp_obj_id` int(11) NOT NULL,
  `target` float(18,3) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_adm_emp_obj_kpi_adm_emp_objective` (`emp_obj_id`),
  CONSTRAINT `fk_adm_emp_obj_kpi_adm_emp_objective` FOREIGN KEY (`emp_obj_id`) REFERENCES `adm_emp_objective` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=21 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `adm_emp_obj_kpi`
--

LOCK TABLES `adm_emp_obj_kpi` WRITE;
/*!40000 ALTER TABLE `adm_emp_obj_kpi` DISABLE KEYS */;
INSERT INTO `adm_emp_obj_kpi` VALUES (14,'OBj KPi 1',NULL,'admin','2019-10-28',NULL,NULL,10,100.000),(15,'Obj KPI 2',NULL,'admin','2019-10-28',NULL,NULL,10,100.000),(16,'kpi 1',NULL,'admin','2019-10-28',NULL,NULL,11,100.000),(17,'kpi 2',NULL,'admin','2019-10-28',NULL,NULL,11,100.000),(18,'kpi 3',NULL,'admin','2019-10-28',NULL,NULL,11,100.000),(19,'no of internal meeting ',NULL,'admin','2019-11-01',NULL,NULL,13,10.000),(20,'report timing preparation (hours)',NULL,'admin','2019-11-01',NULL,NULL,13,5.000);
/*!40000 ALTER TABLE `adm_emp_obj_kpi` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `adm_emp_obj_kpi_ass`
--

DROP TABLE IF EXISTS `adm_emp_obj_kpi_ass`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `adm_emp_obj_kpi_ass` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `period_no` int(11) NOT NULL,
  `emp_obj_kpi_id` int(11) NOT NULL,
  `result` float(18,3) DEFAULT NULL,
  `created_by` varchar(45) NOT NULL,
  `created_date` date NOT NULL,
  `modified_by` varchar(45) DEFAULT NULL,
  `modified_date` date DEFAULT NULL,
  `target` float(18,3) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `adm_emp_obj_kpi_ass_adm_emp_obj_kpi_idx` (`emp_obj_kpi_id`),
  CONSTRAINT `adm_emp_obj_kpi_ass_adm_emp_obj_kpi` FOREIGN KEY (`emp_obj_kpi_id`) REFERENCES `adm_emp_obj_kpi` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=30 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `adm_emp_obj_kpi_ass`
--

LOCK TABLES `adm_emp_obj_kpi_ass` WRITE;
/*!40000 ALTER TABLE `adm_emp_obj_kpi_ass` DISABLE KEYS */;
INSERT INTO `adm_emp_obj_kpi_ass` VALUES (16,2,14,90.000,'admin','2019-10-28','ADMIN','2019-10-28',100.000),(17,4,14,90.000,'admin','2019-10-28','ADMIN','2019-10-28',100.000),(18,2,15,6.000,'admin','2019-10-28','ADMIN','2019-10-29',100.000),(19,4,15,5.000,'admin','2019-10-28','ADMIN','2019-10-29',100.000),(20,2,16,0.000,'admin','2019-10-28',NULL,NULL,100.000),(21,4,16,0.000,'admin','2019-10-28',NULL,NULL,100.000),(22,2,17,0.000,'admin','2019-10-28',NULL,NULL,100.000),(23,4,17,0.000,'admin','2019-10-28',NULL,NULL,100.000),(24,2,18,0.000,'admin','2019-10-28',NULL,NULL,100.000),(25,4,18,0.000,'admin','2019-10-28',NULL,NULL,100.000),(26,2,19,0.000,'admin','2019-11-01',NULL,NULL,10.000),(27,4,19,0.000,'admin','2019-11-01',NULL,NULL,10.000),(28,2,20,0.000,'admin','2019-11-01',NULL,NULL,5.000),(29,4,20,0.000,'admin','2019-11-01',NULL,NULL,5.000);
/*!40000 ALTER TABLE `adm_emp_obj_kpi_ass` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `adm_emp_objective`
--

DROP TABLE IF EXISTS `adm_emp_objective`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `adm_emp_objective` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(1000) NOT NULL,
  `name2` varchar(1000) DEFAULT NULL,
  `code` varchar(50) NOT NULL,
  `weight` float(18,3) NOT NULL,
  `note` varchar(1000) DEFAULT NULL,
  `emp_assesment_id` int(11) NOT NULL,
  `created_by` varchar(100) NOT NULL,
  `created_date` date NOT NULL,
  `modified_by` varchar(100) DEFAULT NULL,
  `modified_date` date DEFAULT NULL,
  `project_id` int(11) DEFAULT NULL,
  `pos_desc_id` int(11) DEFAULT NULL,
  `target` float(18,3) DEFAULT NULL,
  `result_without_round` float(18,3) DEFAULT NULL,
  `result_after_round` float(18,3) DEFAULT NULL,
  `project_desc` varchar(500) DEFAULT NULL,
  `final_point_result` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_adm_emp_project_adm_employees` (`emp_assesment_id`),
  CONSTRAINT `fk_adm_emp_objective_adm_emp_assesment` FOREIGN KEY (`emp_assesment_id`) REFERENCES `adm_emp_assesment` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=17 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `adm_emp_objective`
--

LOCK TABLES `adm_emp_objective` WRITE;
/*!40000 ALTER TABLE `adm_emp_objective` DISABLE KEYS */;
INSERT INTO `adm_emp_objective` VALUES (10,'Arafat Individual obj 1',NULL,'1',50.000,NULL,14,'admin','2019-10-28',NULL,NULL,1,4,100.000,77.500,78.000,NULL,NULL),(11,'Arafat Individual obj 2',NULL,'2',50.000,NULL,14,'admin','2019-10-28',NULL,NULL,6,0,100.000,47.500,48.000,NULL,NULL),(12,'preparing the meeting report within 3 working days',NULL,'3',10.000,'report 1',15,'admin','2019-10-29','admin','2019-11-01',2,12,3.000,NULL,NULL,NULL,NULL),(13,'visiting all branches of company and doing all reports (days)',NULL,'4',0.000,'visiting including discussing all relative customers complains',15,'admin','2019-11-01',NULL,NULL,3,12,25.000,NULL,NULL,NULL,NULL),(14,'preparing best practices of customers service centers',NULL,'5',15.000,'international best practice and bench marking',15,'admin','2019-11-01',NULL,NULL,2,13,0.000,NULL,NULL,NULL,NULL);
/*!40000 ALTER TABLE `adm_emp_objective` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `adm_employee_positions`
--

DROP TABLE IF EXISTS `adm_employee_positions`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
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
) ENGINE=InnoDB AUTO_INCREMENT=16 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `adm_employee_positions`
--

LOCK TABLES `adm_employee_positions` WRITE;
/*!40000 ALTER TABLE `adm_employee_positions` DISABLE KEYS */;
INSERT INTO `adm_employee_positions` VALUES (9,2019,1,2,'ADMIN','2019-09-25',NULL,NULL,NULL),(10,2019,1,24,'ADMIN','2019-09-28',NULL,NULL,NULL),(11,2019,1,25,'ADMIN','2019-09-28',NULL,NULL,NULL),(13,2019,1,27,'ADMIN','2019-10-11',NULL,NULL,NULL),(14,2019,2,33,'ADMIN','2019-10-13',NULL,NULL,NULL),(15,2019,2,33,'ADMIN','2019-10-14',NULL,NULL,NULL);
/*!40000 ALTER TABLE `adm_employee_positions` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `adm_employees`
--

DROP TABLE IF EXISTS `adm_employees`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
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
  `employee_number` varchar(2000) NOT NULL,
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
) ENGINE=InnoDB AUTO_INCREMENT=91 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `adm_employees`
--

LOCK TABLES `adm_employees` WRITE;
/*!40000 ALTER TABLE `adm_employees` DISABLE KEYS */;
INSERT INTO `adm_employees` VALUES (2,'Zeyad','','','Abed Al Fatah',2,1,NULL,NULL,NULL,NULL,1,'http://bp-hr.com/Staging/hr_apis/employeeimages/images.jpg',1,1,'2019-10-14',NULL,'ADMIN','2019-10-14',NULL,NULL,'زياد',NULL,NULL,'عبدالفتاح','222'),(24,'Arafat','','','Arbeid ',1,1,NULL,'','',2,1,'http://bp-hr.com/Staging/hr_apis/employeeimages/download.jpg',1,1,'2019-10-13',NULL,'ADMIN','2019-10-13',NULL,NULL,'عرفات','عيد','محمد','العربيد','333'),(25,'Ameen','','','Al Hayek',5,1,NULL,'','',2,1,'http://bp-hr.com/Staging/hr_apis/employeeimages/x98O5-jB_400x400.jpg',1,1,'2019-10-14',NULL,'ADMIN','2019-10-14',NULL,NULL,NULL,NULL,NULL,NULL,'11122'),(27,'Mohammad','','','Ishaq',1,1,NULL,'','',24,1,'http://bp-hr.com/Staging/hr_apis/employeeimages/download (2).jpg',1,1,'2019-10-14',NULL,'ADMIN','2019-10-14',NULL,NULL,NULL,NULL,NULL,NULL,'123'),(30,'Ismael',NULL,NULL,'Mosa',1,1,NULL,NULL,NULL,25,1,'http://bp-hr.com/Staging/hr_apis/employeeimages/images (1).jpg',1,1,'2019-10-14',NULL,'ADMIN','2019-10-14',NULL,NULL,NULL,NULL,NULL,NULL,'112'),(31,'Samer',NULL,NULL,'Ramahi',1,1,NULL,NULL,NULL,27,1,'http://bp-hr.com/Staging/hr_apis/employeeimages/download (3).jpg',1,1,'2019-10-14',NULL,'ADMIN','2019-10-14',NULL,NULL,NULL,NULL,NULL,NULL,'111'),(32,'Sameera',NULL,NULL,'kamel',1,1,NULL,NULL,NULL,25,1,'http://bp-hr.com/Staging/hr_apis/employeeimages/340032-1090068-2_320x400.jpg',1,1,'2019-10-14',NULL,'ADMIN','2019-10-14',NULL,NULL,NULL,NULL,NULL,NULL,'321'),(33,'Mohannad','','','Masri',5,1,NULL,'','',25,1,'http://bp-hr.com/Staging/hr_apis/employeeimages/download (1).jpg',1,1,'2019-10-13',NULL,'ADMIN','2019-10-13',NULL,NULL,NULL,NULL,NULL,NULL,'5533341'),(34,'abdel fattah','','','alajlouni',3,1,'','','',2,1,'http://bp-hr.com/Staging/hr_apis/employeeimages/Default.jpg',1,1,'2019-10-25',NULL,'admin','2019-10-25',NULL,NULL,NULL,NULL,NULL,NULL,'4'),(35,'Zeyad','','','Abed Al Fatah',2,1,NULL,NULL,NULL,NULL,1,'http://bp-hr.com/Staging/hr_apis/employeeimages/images.jpg',1,1,'2019-10-14',NULL,'ADMIN','2019-10-14',NULL,NULL,NULL,NULL,NULL,NULL,'222'),(36,'Arafat','','','Arbeid ',1,1,NULL,'','',2,1,'http://bp-hr.com/Staging/hr_apis/employeeimages/download.jpg',1,1,'2019-10-13',NULL,'ADMIN','2019-10-13',NULL,NULL,NULL,NULL,NULL,NULL,'333'),(37,'Ameen','','','Al Hayek',5,1,NULL,'','',2,1,'http://bp-hr.com/Staging/hr_apis/employeeimages/x98O5-jB_400x400.jpg',1,1,'2019-10-14',NULL,'ADMIN','2019-10-14',NULL,NULL,NULL,NULL,NULL,NULL,'11122'),(38,'Mohammad','','','Ishaq',1,1,NULL,'','',24,1,'http://bp-hr.com/Staging/hr_apis/employeeimages/download (2).jpg',1,1,'2019-10-31',NULL,'admin','2019-10-31',NULL,NULL,NULL,NULL,NULL,NULL,'123'),(39,'Ismael',NULL,NULL,'Mosa',1,1,NULL,NULL,NULL,24,1,'http://bp-hr.com/Staging/hr_apis/employeeimages/images (1).jpg',1,1,'2019-10-14',NULL,'ADMIN','2019-10-14',NULL,NULL,NULL,NULL,NULL,NULL,'112'),(40,'Zeyad','','','Abed Al Fatah',2,1,NULL,NULL,NULL,NULL,1,'http://bp-hr.com/Staging/hr_apis/employeeimages/images.jpg',1,1,'2019-10-14',NULL,'ADMIN','2019-10-14',NULL,NULL,NULL,NULL,NULL,NULL,'222'),(41,'Arafat','','','Arbeid ',1,1,NULL,'','',2,1,'http://bp-hr.com/Staging/hr_apis/employeeimages/download.jpg',1,1,'2019-10-13',NULL,'ADMIN','2019-10-13',NULL,NULL,NULL,NULL,NULL,NULL,'333'),(42,'Ameen','','','Al Hayek',5,1,NULL,'','',2,1,'http://bp-hr.com/Staging/hr_apis/employeeimages/x98O5-jB_400x400.jpg',1,1,'2019-10-14',NULL,'ADMIN','2019-10-14',NULL,NULL,NULL,NULL,NULL,NULL,'11122'),(43,'Mohammad','','','Ishaq',1,1,NULL,'','',24,1,'http://bp-hr.com/Staging/hr_apis/employeeimages/download (2).jpg',1,1,'2019-10-14',NULL,'ADMIN','2019-10-14',NULL,NULL,NULL,NULL,NULL,NULL,'123'),(44,'Ismael',NULL,NULL,'Mosa',1,1,NULL,NULL,NULL,24,1,'http://bp-hr.com/Staging/hr_apis/employeeimages/images (1).jpg',1,1,'2019-10-14',NULL,'ADMIN','2019-10-14',NULL,NULL,NULL,NULL,NULL,NULL,'112'),(45,'Zeyad','','','Abed Al Fatah',2,1,NULL,NULL,NULL,NULL,1,'http://bp-hr.com/Staging/hr_apis/employeeimages/images.jpg',1,1,'2019-10-14',NULL,'ADMIN','2019-10-14',NULL,NULL,NULL,NULL,NULL,NULL,'222'),(46,'Arafat','','','Arbeid ',1,1,NULL,'','',2,1,'http://bp-hr.com/Staging/hr_apis/employeeimages/download.jpg',1,1,'2019-10-13',NULL,'ADMIN','2019-10-13',NULL,NULL,NULL,NULL,NULL,NULL,'333'),(47,'Ameen','','','Al Hayek',5,1,NULL,'','',2,1,'http://bp-hr.com/Staging/hr_apis/employeeimages/x98O5-jB_400x400.jpg',1,1,'2019-10-14',NULL,'ADMIN','2019-10-14',NULL,NULL,NULL,NULL,NULL,NULL,'11122'),(48,'Mohammad','','','Ishaq',1,1,NULL,'','',24,1,'http://bp-hr.com/Staging/hr_apis/employeeimages/download (2).jpg',1,1,'2019-10-14',NULL,'ADMIN','2019-10-14',NULL,NULL,NULL,NULL,NULL,NULL,'123'),(49,'Ismael',NULL,NULL,'Mosa',1,1,NULL,NULL,NULL,24,1,'http://bp-hr.com/Staging/hr_apis/employeeimages/images (1).jpg',1,1,'2019-10-14',NULL,'ADMIN','2019-10-14',NULL,NULL,NULL,NULL,NULL,NULL,'112'),(50,'Zeyad','','','Abed Al Fatah',2,1,NULL,NULL,NULL,NULL,1,'http://bp-hr.com/Staging/hr_apis/employeeimages/images.jpg',1,1,'2019-10-14',NULL,'ADMIN','2019-10-14',NULL,NULL,NULL,NULL,NULL,NULL,'222'),(51,'Arafat','','','Arbeid ',1,1,NULL,'','',2,1,'http://bp-hr.com/Staging/hr_apis/employeeimages/download.jpg',1,1,'2019-10-13',NULL,'ADMIN','2019-10-13',NULL,NULL,NULL,NULL,NULL,NULL,'333'),(52,'Ameen','','','Al Hayek',5,1,NULL,'','',2,1,'http://bp-hr.com/Staging/hr_apis/employeeimages/x98O5-jB_400x400.jpg',1,1,'2019-10-14',NULL,'ADMIN','2019-10-14',NULL,NULL,NULL,NULL,NULL,NULL,'11122'),(53,'Mohammad','','','Ishaq',1,1,NULL,'','',24,1,'http://bp-hr.com/Staging/hr_apis/employeeimages/download (2).jpg',1,1,'2019-10-14',NULL,'ADMIN','2019-10-14',NULL,NULL,NULL,NULL,NULL,NULL,'123'),(54,'Ismael',NULL,NULL,'Mosa',1,1,NULL,NULL,NULL,24,1,'http://bp-hr.com/Staging/hr_apis/employeeimages/images (1).jpg',1,1,'2019-10-14',NULL,'ADMIN','2019-10-14',NULL,NULL,NULL,NULL,NULL,NULL,'112'),(55,'Zeyad','','','Abed Al Fatah',2,1,NULL,NULL,NULL,NULL,1,'http://bp-hr.com/Staging/hr_apis/employeeimages/images.jpg',1,1,'2019-10-14',NULL,'ADMIN','2019-10-14',NULL,NULL,NULL,NULL,NULL,NULL,'222'),(56,'Arafat','','','Arbeid ',1,1,NULL,'','',2,1,'http://bp-hr.com/Staging/hr_apis/employeeimages/download.jpg',1,1,'2019-10-13',NULL,'ADMIN','2019-10-13',NULL,NULL,NULL,NULL,NULL,NULL,'333'),(57,'Ameen','','','Al Hayek',5,1,NULL,'','',2,1,'http://bp-hr.com/Staging/hr_apis/employeeimages/x98O5-jB_400x400.jpg',1,1,'2019-10-14',NULL,'ADMIN','2019-10-14',NULL,NULL,NULL,NULL,NULL,NULL,'11122'),(58,'Mohammad','','','Ishaq',1,1,NULL,'','',24,1,'http://bp-hr.com/Staging/hr_apis/employeeimages/download (2).jpg',1,1,'2019-10-14',NULL,'ADMIN','2019-10-14',NULL,NULL,NULL,NULL,NULL,NULL,'123'),(59,'Ismael',NULL,NULL,'Mosa',1,1,NULL,NULL,NULL,24,1,'http://bp-hr.com/Staging/hr_apis/employeeimages/images (1).jpg',1,1,'2019-10-14',NULL,'ADMIN','2019-10-14',NULL,NULL,NULL,NULL,NULL,NULL,'112'),(60,'Zeyad','','','Abed Al Fatah',2,1,NULL,NULL,NULL,NULL,1,'http://bp-hr.com/Staging/hr_apis/employeeimages/images.jpg',1,1,'2019-10-14',NULL,'ADMIN','2019-10-14',NULL,NULL,NULL,NULL,NULL,NULL,'222'),(61,'Arafat','','','Arbeid ',1,1,NULL,'','',2,1,'http://bp-hr.com/Staging/hr_apis/employeeimages/download.jpg',1,1,'2019-10-13',NULL,'ADMIN','2019-10-13',NULL,NULL,NULL,NULL,NULL,NULL,'333'),(62,'Ameen','','','Al Hayek',5,1,NULL,'','',2,1,'http://bp-hr.com/Staging/hr_apis/employeeimages/x98O5-jB_400x400.jpg',1,1,'2019-10-14',NULL,'ADMIN','2019-10-14',NULL,NULL,NULL,NULL,NULL,NULL,'11122'),(63,'Mohammad','','','Ishaq',1,1,NULL,'','',24,1,'http://bp-hr.com/Staging/hr_apis/employeeimages/download (2).jpg',1,1,'2019-10-14',NULL,'ADMIN','2019-10-14',NULL,NULL,NULL,NULL,NULL,NULL,'123'),(64,'Ismael',NULL,NULL,'Mosa',1,1,NULL,NULL,NULL,24,1,'http://bp-hr.com/Staging/hr_apis/employeeimages/images (1).jpg',1,1,'2019-10-14',NULL,'ADMIN','2019-10-14',NULL,NULL,NULL,NULL,NULL,NULL,'112'),(65,'Zeyad','','','Abed Al Fatah',2,1,NULL,NULL,NULL,NULL,1,'http://bp-hr.com/Staging/hr_apis/employeeimages/images.jpg',1,1,'2019-10-14',NULL,'ADMIN','2019-10-14',NULL,NULL,NULL,NULL,NULL,NULL,'222'),(66,'Arafat','','','Arbeid ',1,1,NULL,'','',2,1,'http://bp-hr.com/Staging/hr_apis/employeeimages/download.jpg',1,1,'2019-10-13',NULL,'ADMIN','2019-10-13',NULL,NULL,NULL,NULL,NULL,NULL,'333'),(67,'Ameen','','','Al Hayek',5,1,NULL,'','',2,1,'http://bp-hr.com/Staging/hr_apis/employeeimages/x98O5-jB_400x400.jpg',1,1,'2019-10-14',NULL,'ADMIN','2019-10-14',NULL,NULL,NULL,NULL,NULL,NULL,'11122'),(68,'Mohammad','','','Ishaq',1,1,NULL,'','',24,1,'http://bp-hr.com/Staging/hr_apis/employeeimages/download (2).jpg',1,1,'2019-10-14',NULL,'ADMIN','2019-10-14',NULL,NULL,NULL,NULL,NULL,NULL,'123'),(69,'Ismael',NULL,NULL,'Mosa',1,1,NULL,NULL,NULL,24,1,'http://bp-hr.com/Staging/hr_apis/employeeimages/images (1).jpg',1,1,'2019-10-14',NULL,'ADMIN','2019-10-14',NULL,NULL,NULL,NULL,NULL,NULL,'112'),(70,'Zeyad','','','Abed Al Fatah',2,1,NULL,NULL,NULL,NULL,1,'http://bp-hr.com/Staging/hr_apis/employeeimages/images.jpg',1,1,'2019-10-14',NULL,'ADMIN','2019-10-14',NULL,NULL,NULL,NULL,NULL,NULL,'222'),(71,'Arafat','','','Arbeid ',1,1,NULL,'','',2,1,'http://bp-hr.com/Staging/hr_apis/employeeimages/download.jpg',1,1,'2019-10-13',NULL,'ADMIN','2019-10-13',NULL,NULL,NULL,NULL,NULL,NULL,'333'),(72,'Ameen','','','Al Hayek',5,1,NULL,'','',2,1,'http://bp-hr.com/Staging/hr_apis/employeeimages/x98O5-jB_400x400.jpg',1,1,'2019-10-14',NULL,'ADMIN','2019-10-14',NULL,NULL,NULL,NULL,NULL,NULL,'11122'),(73,'Mohammad','','','Ishaq',1,1,NULL,'','',24,1,'http://bp-hr.com/Staging/hr_apis/employeeimages/download (2).jpg',1,1,'2019-10-14',NULL,'ADMIN','2019-10-14',NULL,NULL,NULL,NULL,NULL,NULL,'123'),(74,'Ismael',NULL,NULL,'Mosa',1,1,NULL,NULL,NULL,24,1,'http://bp-hr.com/Staging/hr_apis/employeeimages/images (1).jpg',1,1,'2019-10-14',NULL,'ADMIN','2019-10-14',NULL,NULL,NULL,NULL,NULL,NULL,'112'),(75,'Zeyad','','','Abed Al Fatah',2,1,NULL,NULL,NULL,NULL,1,'http://bp-hr.com/Staging/hr_apis/employeeimages/images.jpg',1,1,'2019-10-14',NULL,'ADMIN','2019-10-14',NULL,NULL,NULL,NULL,NULL,NULL,'222'),(76,'Arafat','','','Arbeid ',1,1,NULL,'','',2,1,'http://bp-hr.com/Staging/hr_apis/employeeimages/download.jpg',1,1,'2019-10-13',NULL,'ADMIN','2019-10-13',NULL,NULL,NULL,NULL,NULL,NULL,'333'),(77,'Ameen','','','Al Hayek',5,1,NULL,'','',2,1,'http://bp-hr.com/Staging/hr_apis/employeeimages/x98O5-jB_400x400.jpg',1,1,'2019-10-14',NULL,'ADMIN','2019-10-14',NULL,NULL,NULL,NULL,NULL,NULL,'11122'),(78,'Mohammad','','','Ishaq',1,1,NULL,'','',24,1,'http://bp-hr.com/Staging/hr_apis/employeeimages/download (2).jpg',1,1,'2019-10-14',NULL,'ADMIN','2019-10-14',NULL,NULL,NULL,NULL,NULL,NULL,'123'),(79,'Ismael',NULL,NULL,'Mosa',1,1,NULL,NULL,NULL,24,1,'http://bp-hr.com/Staging/hr_apis/employeeimages/images (1).jpg',1,1,'2019-10-14',NULL,'ADMIN','2019-10-14',NULL,NULL,NULL,NULL,NULL,NULL,'112'),(80,'Zeyad','','','Abed Al Fatah',2,1,NULL,NULL,NULL,NULL,1,'http://bp-hr.com/Staging/hr_apis/employeeimages/images.jpg',1,1,'2019-10-14',NULL,'ADMIN','2019-10-14',NULL,NULL,NULL,NULL,NULL,NULL,'222'),(81,'Arafat','','','Arbeid ',1,1,NULL,'','',2,1,'http://bp-hr.com/Staging/hr_apis/employeeimages/download.jpg',1,1,'2019-10-13',NULL,'ADMIN','2019-10-13',NULL,NULL,NULL,NULL,NULL,NULL,'333'),(82,'Ameen','','','Al Hayek',5,1,NULL,'','',2,1,'http://bp-hr.com/Staging/hr_apis/employeeimages/x98O5-jB_400x400.jpg',1,1,'2019-10-14',NULL,'ADMIN','2019-10-14',NULL,NULL,NULL,NULL,NULL,NULL,'11122'),(83,'Mohammad','','','Ishaq',1,1,NULL,'','',24,1,'http://bp-hr.com/Staging/hr_apis/employeeimages/download (2).jpg',1,1,'2019-10-14',NULL,'ADMIN','2019-10-14',NULL,NULL,NULL,NULL,NULL,NULL,'123'),(84,'Ismael',NULL,NULL,'Mosa',1,1,NULL,NULL,NULL,24,1,'http://bp-hr.com/Staging/hr_apis/employeeimages/images (1).jpg',1,1,'2019-10-14',NULL,'ADMIN','2019-10-14',NULL,NULL,NULL,NULL,NULL,NULL,'112'),(85,'Zeyad','','','Abed Al Fatah',2,1,NULL,NULL,NULL,NULL,1,'http://bp-hr.com/Staging/hr_apis/employeeimages/images.jpg',1,1,'2019-10-14',NULL,'ADMIN','2019-10-14',NULL,NULL,NULL,NULL,NULL,NULL,'222'),(86,'Arafat','','','Arbeid ',1,1,NULL,'','',2,1,'http://bp-hr.com/Staging/hr_apis/employeeimages/download.jpg',1,1,'2019-10-13',NULL,'ADMIN','2019-10-13',NULL,NULL,NULL,NULL,NULL,NULL,'333'),(87,'Ameen','','','Al Hayek',5,1,NULL,'','',2,1,'http://bp-hr.com/Staging/hr_apis/employeeimages/x98O5-jB_400x400.jpg',1,1,'2019-10-14',NULL,'ADMIN','2019-10-14',NULL,NULL,NULL,NULL,NULL,NULL,'11122'),(88,'Mohammad','','','Ishaq',1,1,NULL,'','',24,1,'http://bp-hr.com/Staging/hr_apis/employeeimages/download (2).jpg',1,1,'2019-10-14',NULL,'ADMIN','2019-10-14',NULL,NULL,NULL,NULL,NULL,NULL,'123'),(89,'Ismael',NULL,NULL,'Mosa',1,1,NULL,NULL,NULL,24,1,'http://bp-hr.com/Staging/hr_apis/employeeimages/images (1).jpg',1,1,'2019-10-14',NULL,'ADMIN','2019-10-14',NULL,NULL,NULL,NULL,NULL,NULL,'112'),(90,'ruba ','','','berawi',5,1,'','','',40,1,'http://bp-hr.com/Staging/hr_apis/employeeimages/Default.jpg',1,1,'2019-10-31',NULL,'admin','2019-10-31',NULL,NULL,'ربى',NULL,NULL,'بيراوي','12345');
/*!40000 ALTER TABLE `adm_employees` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `adm_menus`
--

DROP TABLE IF EXISTS `adm_menus`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `adm_menus` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `NAME` varchar(100) NOT NULL,
  `URL` varchar(100) DEFAULT NULL,
  `ICONE` varchar(100) DEFAULT NULL,
  `PARENT_ID` int(11) DEFAULT NULL,
  `COMPANY_ID` int(11) NOT NULL,
  `created_by` varchar(100) NOT NULL,
  `created_date` date NOT NULL,
  `modified_by` varchar(100) DEFAULT NULL,
  `modified_date` date DEFAULT NULL,
  `name2` varchar(100) DEFAULT NULL,
  `order` int(11) NOT NULL,
  `application_code` varchar(5) NOT NULL,
  `system_code` varchar(5) NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `idx_ADM_MENUS_0` (`COMPANY_ID`),
  CONSTRAINT `fk_adm_menus_adm_company` FOREIGN KEY (`COMPANY_ID`) REFERENCES `adm_company` (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=25 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `adm_menus`
--

LOCK TABLES `adm_menus` WRITE;
/*!40000 ALTER TABLE `adm_menus` DISABLE KEYS */;
INSERT INTO `adm_menus` VALUES (1,'Setup','/','fa-edit',NULL,1,'ADMIN','2019-10-01',NULL,NULL,'الاعدادات',1,'HRMS','HOBJ'),(2,'Skills Types','#/skillsTypes','fa-circle-o',1,1,'ADMIN','2019-10-02',NULL,NULL,'المهارات',1,'HRMS','HOBJ'),(3,'Positions','#/positions','fa-circle-o',1,1,'ADMIN','2019-10-03',NULL,NULL,'المناصب',2,'HRMS','HOBJ'),(4,'Competencies','#/competence','fa-circle-o',1,1,'ADMIN','2019-10-04',NULL,NULL,'الكفاءات',3,'HRMS','HOBJ'),(5,'Employees','#/Employees','fa-address-card',1,1,'ADMIN','2019-10-05',NULL,NULL,'الموظفون',4,'HRMS','HOBJ'),(6,'Strategic Planning','/','fa-briefcase',NULL,1,'ADMIN','2019-10-06',NULL,NULL,'تخطيط',2,'HRMS','HOBJ'),(7,'Strategic Objectives','#/stratigicobjectives','fa-black-tie',6,1,'ADMIN','2019-10-07',NULL,NULL,'الأهداف الإستراتيجية',1,'HRMS','HOBJ'),(8,'Projects','#/Projects','fa-circle-o',6,1,'ADMIN','2019-10-08',NULL,NULL,'مشاريع',2,'HRMS','HOBJ'),(9,'Projects Planner','#/ProjectPlanningChart','fa-circle-o',6,1,'ADMIN','2019-10-09',NULL,NULL,'مخطط المشاريع',3,'HRMS','HOBJ'),(10,'Employee Structure','#/EmpStructure','fa-circle-o',21,1,'ADMIN','2019-10-10',NULL,NULL,'هيكل الموظف',4,'HRMS','HOBJ'),(11,'Employee Performance Plan','#/employeeObjectve','fa-circle-o',21,1,'ADMIN','2019-10-11',NULL,NULL,'خطة أداء الموظف',5,'HRMS','HOBJ'),(12,'Operation Assessment','/','fa-edit',NULL,1,'ADMIN','2019-10-12',NULL,NULL,'التشغيل والتقييم',4,'HRMS','HOBJ'),(13,'Projects Assessment','#/projectsAssessment','fa-circle-o',12,1,'ADMIN','2019-10-13',NULL,NULL,'تقييم المشروعات',1,'HRMS','HOBJ'),(14,'Projects Navigation','#/stratigicobjectivesChart','fa-circle-o',12,1,'ADMIN','2019-10-14',NULL,NULL,'التنقل في المشروعات',2,'HRMS','HOBJ'),(15,'Employees Performance Assessment','#/employeeAssessment','fa-circle-o',12,1,'ADMIN','2019-10-15',NULL,NULL,'تقييم أداء الموظفين',3,'HRMS','HOBJ'),(16,'Employee Navigation','#/EmpStructure','fa-circle-o',12,1,'ADMIN','2019-10-16',NULL,NULL,'تنقل الموظف',4,'HRMS','HOBJ'),(17,'Dashboard Analysis','/','fa-percent',NULL,1,'ADMIN','2019-10-17',NULL,NULL,'لوحة القيادة والتحليل',5,'HRMS','HOBJ'),(18,'Organization Dashboard','#/DashBoards','fa-circle-o',17,1,'ADMIN','2019-10-18',NULL,NULL,'لوحة معلومات المنظمة',1,'HRMS','HOBJ'),(19,'Employee Dashboard','#/EmpDashBoards','fa-circle-o',17,1,'ADMIN','2019-10-19',NULL,NULL,'لوحة معلومات الموظف',2,'HRMS','HOBJ'),(20,'Units','#/Units','fa-circle-o',1,1,'ADMIN','2019-10-20',NULL,NULL,'الاقسام',5,'HRMS','HOBJ'),(21,'Employee Planning','/','fa-user',NULL,1,'ADMIN','2019-10-25',NULL,NULL,'تخطيط الموظفيين',3,'HRMS','HOBJ'),(22,'Performance Levels','#/performanceLevels','fa-circle-o',1,1,'ADMIN','2019-10-25',NULL,NULL,'نسب التوزيع',6,'HRMS','HOBJ'),(23,'Company','#/Company','fa-circle-o',1,1,'ADMIN','2019-10-25',NULL,NULL,'Company',6,'HRMS','HOBJ'),(24,'Performance Levels Quota','#/performanceLevelsQuota','fa-circle-o',1,1,'ADMIN','2019-10-25',NULL,NULL,'حصص نسب التوزيع',7,'HRMS','HOBJ');
/*!40000 ALTER TABLE `adm_menus` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `adm_objective_kpi`
--

DROP TABLE IF EXISTS `adm_objective_kpi`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `adm_objective_kpi` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `objective_id` int(11) NOT NULL,
  `name` varchar(2000) NOT NULL,
  `name2` varchar(2000) DEFAULT NULL,
  `weight` float NOT NULL,
  `target` float NOT NULL,
  `bsc` int(11) NOT NULL,
  `measurement` int(11) NOT NULL,
  `description` varchar(2000) DEFAULT NULL,
  `company_id` int(11) NOT NULL,
  `branch_id` int(11) NOT NULL,
  `created_by` varchar(50) NOT NULL,
  `created_date` date NOT NULL,
  `modified_by` varchar(50) DEFAULT NULL,
  `modified_date` date DEFAULT NULL,
  `result` float(18,3) NOT NULL DEFAULT '0.000',
  PRIMARY KEY (`id`),
  KEY `fk_obj_kpi_objectives_id` (`objective_id`)
) ENGINE=MyISAM AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `adm_objective_kpi`
--

LOCK TABLES `adm_objective_kpi` WRITE;
/*!40000 ALTER TABLE `adm_objective_kpi` DISABLE KEYS */;
INSERT INTO `adm_objective_kpi` VALUES (1,1,'Spread Happiness ',NULL,20,4,1,1,'null',1,1,'admin','2019-11-06',NULL,NULL,0.000),(2,1,'customer satisfaction rate ',NULL,11,80,2,1,NULL,1,1,'admin','2019-10-31',NULL,NULL,0.000),(3,1,'value kpi',NULL,10,5,1,2,NULL,1,1,'admin','2019-11-05',NULL,NULL,0.000),(4,1,'fdfdg',NULL,59,1,1,1,NULL,1,1,'admin','2019-11-06',NULL,NULL,0.000);
/*!40000 ALTER TABLE `adm_objective_kpi` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `adm_performance_levels`
--

DROP TABLE IF EXISTS `adm_performance_levels`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
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
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `adm_performance_levels`
--

LOCK TABLES `adm_performance_levels` WRITE;
/*!40000 ALTER TABLE `adm_performance_levels` DISABLE KEYS */;
INSERT INTO `adm_performance_levels` VALUES (1,'frst','First',1,5,1,'Mohannad','2019-10-11',NULL,NULL,'الأول');
/*!40000 ALTER TABLE `adm_performance_levels` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `adm_pos_description`
--

DROP TABLE IF EXISTS `adm_pos_description`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `adm_pos_description` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(1000) NOT NULL,
  `name2` varchar(1000) DEFAULT NULL,
  `created_by` varchar(50) NOT NULL,
  `created_date` date NOT NULL,
  `modified_by` varchar(50) DEFAULT NULL,
  `modified_date` date DEFAULT NULL,
  `position_id` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_adm_pos_description_adm_positions` (`position_id`),
  CONSTRAINT `fk_adm_pos_description_adm_positions` FOREIGN KEY (`position_id`) REFERENCES `adm_positions` (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=15 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `adm_pos_description`
--

LOCK TABLES `adm_pos_description` WRITE;
/*!40000 ALTER TABLE `adm_pos_description` DISABLE KEYS */;
INSERT INTO `adm_pos_description` VALUES (4,'Developemnt of Apps','تطوير البرامج','ADMIN','2019-10-05','admin','2019-10-13',2),(5,'Analysis','تحليل البيانات','ADMIN','2019-10-05','admin','2019-10-13',2),(6,'Find Solutions','ايجاد حلول برمجية','ADMIN','2019-10-05','admin','2019-10-13',2),(7,'Tech Support','الدعم الفني','ADMIN','2019-10-05','admin','2019-10-13',3),(12,'executing all filed visiting to follow up customers satisfaction and feedback.','تنفيذ جميع الزيارات','admin','2019-10-28','admin','2019-11-12',1),(13,'make searching for all customers requirements and needs.','make searching for all customers requirements and needs.','admin','2019-10-28','admin','2019-11-01',1),(14,'Job DEsc 3','Job DEsc 3','admin','2019-10-28',NULL,NULL,1);
/*!40000 ALTER TABLE `adm_pos_description` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `adm_position_competencies`
--

DROP TABLE IF EXISTS `adm_position_competencies`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `adm_position_competencies` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `position_id` int(11) NOT NULL,
  `competence_id` int(11) NOT NULL,
  `competence_level` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_adm_position_competencies_adm_competencies` (`competence_id`),
  KEY `fk_adm_position_competencies_adm_positions` (`position_id`),
  CONSTRAINT `fk_adm_position_competencies_adm_competencies` FOREIGN KEY (`competence_id`) REFERENCES `adm_competencies` (`id`),
  CONSTRAINT `fk_adm_position_competencies_adm_positions` FOREIGN KEY (`position_id`) REFERENCES `adm_positions` (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `adm_position_competencies`
--

LOCK TABLES `adm_position_competencies` WRITE;
/*!40000 ALTER TABLE `adm_position_competencies` DISABLE KEYS */;
INSERT INTO `adm_position_competencies` VALUES (3,2,7,0),(4,3,4,0),(5,2,4,0),(8,1,7,1),(9,1,10,3),(10,1,4,2);
/*!40000 ALTER TABLE `adm_position_competencies` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `adm_positions`
--

DROP TABLE IF EXISTS `adm_positions`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
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
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `adm_positions`
--

LOCK TABLES `adm_positions` WRITE;
/*!40000 ALTER TABLE `adm_positions` DISABLE KEYS */;
INSERT INTO `adm_positions` VALUES (1,'TTL','Team Leader',1,1,'admin','2019-09-15','admin','2019-11-12','قائد فريق'),(2,'DEV','Developer',1,1,'admin','2019-10-12','admin','2019-10-12','مبرمج'),(3,'Tech','Technical',1,1,'admin','2019-10-12','admin','2019-10-12','فني'),(5,'01','researcher ',1,1,'admin','2019-11-01',NULL,NULL,NULL);
/*!40000 ALTER TABLE `adm_positions` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `adm_prj_results`
--

DROP TABLE IF EXISTS `adm_prj_results`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
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
) ENGINE=InnoDB AUTO_INCREMENT=39 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `adm_prj_results`
--

LOCK TABLES `adm_prj_results` WRITE;
/*!40000 ALTER TABLE `adm_prj_results` DISABLE KEYS */;
INSERT INTO `adm_prj_results` VALUES (1,4,20,5,1,'ADMIN','2019-11-09','ADMIN','2019-11-09'),(2,4,24,5,2,'ADMIN','2019-11-09','ADMIN','2019-11-09'),(3,4,12,7,3,'ADMIN','2019-11-09','ADMIN','2019-11-09'),(4,4,100,33,4,'ADMIN','2019-11-09','ADMIN','2019-11-09'),(5,4,20,43,5,'ADMIN','2019-11-09','ADMIN','2019-11-09'),(6,4,15,25,6,'ADMIN','2019-11-09','ADMIN','2019-11-09'),(7,4,10,13,7,'ADMIN','2019-11-09','ADMIN','2019-11-09'),(8,4,60,55,8,'ADMIN','2019-11-09','ADMIN','2019-11-09'),(9,4,40,30,9,'ADMIN','2019-11-09','ADMIN','2019-11-09'),(10,4,20,4,10,'ADMIN','2019-11-09','ADMIN','2019-11-09'),(11,4,30,10,11,'ADMIN','2019-11-09','ADMIN','2019-11-09'),(12,4,50,40,12,'ADMIN','2019-11-09','ADMIN','2019-11-09'),(13,4,8,4,13,'ADMIN','2019-11-09','ADMIN','2019-11-09'),(14,4,100,50,14,'ADMIN','2019-11-09','ADMIN','2019-11-09'),(15,4,100,30,15,'ADMIN','2019-11-09','ADMIN','2019-11-09'),(16,4,100,70,16,'ADMIN','2019-11-09','ADMIN','2019-11-09'),(17,4,10,9,28,'ADMIN','2019-11-09','ADMIN','2019-11-09'),(25,4,100,90,32,'ADMIN','2019-11-09','ADMIN','2019-11-09'),(37,2,49,0,41,'admin','2019-11-11',NULL,NULL),(38,4,51,0,41,'admin','2019-11-11',NULL,NULL);
/*!40000 ALTER TABLE `adm_prj_results` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `adm_prj_strat_obj`
--

DROP TABLE IF EXISTS `adm_prj_strat_obj`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
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
-- Table structure for table `adm_project_evidence`
--

DROP TABLE IF EXISTS `adm_project_evidence`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `adm_project_evidence` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `file_name` varchar(150) DEFAULT NULL,
  `file_url` varchar(500) DEFAULT NULL,
  `file_type` varchar(45) DEFAULT NULL,
  `project_id` int(11) NOT NULL,
  `created_by` varchar(45) NOT NULL,
  `created_date` date NOT NULL,
  `modified_by` varchar(45) DEFAULT NULL,
  `modified_date` date DEFAULT NULL,
  `doc_name` varchar(2000) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `adm_evid_adm_project_fk_idx` (`project_id`),
  CONSTRAINT `adm_evid_adm_project_fk` FOREIGN KEY (`project_id`) REFERENCES `adm_projects` (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `adm_project_evidence`
--

LOCK TABLES `adm_project_evidence` WRITE;
/*!40000 ALTER TABLE `adm_project_evidence` DISABLE KEYS */;
INSERT INTO `adm_project_evidence` VALUES (1,'1_1_1.png','http://localhost/HR.APIs/Documents/1_1_1.png',NULL,1,'admin','2019-11-12','admin','2019-11-12','doc 1'),(2,NULL,NULL,NULL,1,'admin','2019-11-12',NULL,NULL,'doc 2'),(3,NULL,NULL,NULL,2,'admin','2019-11-12',NULL,NULL,'doc 1');
/*!40000 ALTER TABLE `adm_project_evidence` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `adm_projects`
--

DROP TABLE IF EXISTS `adm_projects`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
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
  `BRANCH_ID` int(11) NOT NULL,
  `created_by` varchar(100) NOT NULL,
  `created_date` date NOT NULL,
  `modified_by` varchar(100) DEFAULT NULL,
  `modified_date` date DEFAULT NULL,
  `name2` varchar(1000) DEFAULT NULL,
  `Weight_From_Objective` float(12,3) DEFAULT NULL,
  `Result` float(12,3) DEFAULT NULL,
  `Result_Percentage` float(12,3) DEFAULT NULL,
  `Result_Weight_Percentage` float(12,3) DEFAULT NULL,
  `Result_Weight_Perc_From_Obj` float(12,3) DEFAULT NULL,
  `actual_cost` float(12,3) DEFAULT NULL,
  `planned_cost` float(12,3) DEFAULT NULL,
  `p_type` int(11) DEFAULT '1',
  PRIMARY KEY (`ID`),
  KEY `idx_ADM_PROJECTS_COM` (`COMPANY_ID`),
  KEY `idx_ADM_PROJECTS_1` (`C_KPI_CYCLE_ID`),
  KEY `idx_ADM_PROJECTS_2` (`C_KPI_TYPE_ID`),
  KEY `idx_ADM_PROJECTS_3` (`C_RESULT_UNIT_ID`),
  KEY `idx_ADM_PROJECTS_4` (`UNIT_ID`),
  KEY `idx_ADM_PROJECTS` (`STARG_OBJ_ID`),
  KEY `idx_ADM_PROJECTS_0` (`BRANCH_ID`),
  CONSTRAINT `fk_ADM_PROJECTS` FOREIGN KEY (`STARG_OBJ_ID`) REFERENCES `adm_stratigic_objectives` (`ID`),
  CONSTRAINT `fk_ADM_PROJECTS_0` FOREIGN KEY (`BRANCH_ID`) REFERENCES `adm_branches` (`ID`),
  CONSTRAINT `fk_ADM_PROJECTS_UNIT` FOREIGN KEY (`UNIT_ID`) REFERENCES `adm_units` (`ID`),
  CONSTRAINT `fk_adm_projects_adm_company` FOREIGN KEY (`COMPANY_ID`) REFERENCES `adm_company` (`ID`),
  CONSTRAINT `fk_adm_projects_adm_company_0` FOREIGN KEY (`COMPANY_ID`) REFERENCES `adm_company` (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=42 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `adm_projects`
--

LOCK TABLES `adm_projects` WRITE;
/*!40000 ALTER TABLE `adm_projects` DISABLE KEYS */;
INSERT INTO `adm_projects` VALUES (1,1,'1','Holding forums to introduce laws, policies and procedures for supporting SMEs',1,15.000,1,20,1,1,1,'Holding forums to introduce laws, policies and procedures for supporting SMEs','Number of Forums',1,1,'ADMIN','2019-10-12',NULL,'2019-11-08','',3.000,5.000,25.000,3.750,0.750,70000.000,120000.000,2),(2,1,'1','Providing advisory support services for national economic projects in cooperation with local support and organizing organizations for small projects in the country',1,15.000,2,25,1,1,1,'Providing advisory support services for national economic projects in cooperation with local support and organizing organizations for small projects in the country','Number of projects for which advisory support is provided',1,1,'ADMIN','2019-10-12',NULL,'2019-10-18','Providing advisory support services for national economic projects in cooperation with local support and organizing organizations for small projects in the country',3.000,5.000,20.000,3.000,0.600,100000.000,240000.000,1),(3,1,'1','Coordinate with industrial training and rehabilitation centers to train national entrepreneurs',1,10.000,2,12,1,1,1,'Coordinate with industrial training and rehabilitation centers to train national entrepreneurs','Number of training programs implemented',1,1,'ADMIN','2019-10-12',NULL,'2019-10-18','Coordinate with industrial training and rehabilitation centers to train national entrepreneurs',2.000,7.000,58.333,5.833,1.167,13000.000,23000.000,1),(4,1,'1','Preparation of a study to provide facilities and incentive programs for citizenship projects',1,15.000,3,100,1,1,1,'Preparation of a study to provide facilities and incentive programs for citizenship projects','Percentage of study completion',1,1,'ADMIN','2019-10-12',NULL,'2019-10-18','Preparation of a study to provide facilities and incentive programs for citizenship projects',3.000,33.000,33.000,4.950,0.990,340000.000,680000.000,1),(5,1,'1','Prepare guidance programs on industrial incubators for owners of small and medium-sized industrial enterprises',1,20.000,1,20,1,1,1,'Prepare guidance programs on industrial incubators for owners of small and medium-sized industrial enterprises','Number of programs',1,1,'ADMIN','2019-10-12',NULL,'2019-10-28','Prepare guidance programs on industrial incubators for owners of small and medium-sized industrial enterprises',4.000,43.000,215.000,43.000,8.600,NULL,20000.000,1),(6,1,'1','Providing moral support and assistance in obtaining financial support for small projects provided by UAE productive families in cooperation with relevant government agencies',1,15.000,1,15,1,1,1,'Providing moral support and assistance in obtaining financial support for small projects provided by UAE productive families in cooperation with relevant government agencies','Number of projects sponsored by the Ministry',1,1,'ADMIN','2019-10-12',NULL,'2019-10-18','Providing moral support and assistance in obtaining financial support for small projects provided by UAE productive families in cooperation with relevant government agencies',3.000,25.000,166.667,25.000,5.000,NULL,50000.000,1),(7,1,'1','Organizing policy training workshops on small and medium-sized enterprise programs for government and private entities',1,10.000,1,10,1,1,1,'Organizing policy training workshops on small and medium-sized enterprise programs for government and private entities','Number of workshops implemented',1,1,'ADMIN','2019-10-12',NULL,'2019-11-09','',2.000,13.000,130.000,13.000,2.600,NULL,100000.000,1),(8,1,'1','Legal follow-up of intellectual property rights',1,60.000,1,60,1,1,1,'Legal follow-up of intellectual property rights','Number of follow-ups',2,1,'Mohannad','2019-10-12',NULL,'2019-10-31','Legal follow-up of intellectual property rights',18.000,55.000,91.667,55.000,16.500,50000.000,100000.000,1),(9,1,'1','Sensitizing intellectual property owners to register their rights',1,30.000,5,40,1,1,1,'Sensitizing intellectual property owners to register their rights','Number of projects whose intellectual property rights have been registered',2,1,'Mohannad','2019-10-12',NULL,'2019-11-06','Sensitizing intellectual property owners to register their rights',9.000,30.000,75.000,22.500,6.750,NULL,20000.000,1),(10,1,'1','Technical examination of commercial offers',1,20.000,1,20,1,1,1,'Technical examination of commercial offers','Number of screened offers',3,1,'Mohannad','2019-10-12',NULL,'2019-10-31','Technical examination of commercial offers',4.000,4.000,20.000,4.000,0.800,23500.000,47000.000,1),(11,1,'1','Awareness of e-commerce',1,30.000,1,30,1,1,1,'Awareness of e-commerce','Number of projects for which advisory support is provided',3,1,'Mohannad','2019-10-12',NULL,'2019-10-31','Awareness of e-commerce',6.000,10.000,33.333,10.000,2.000,132400.000,264800.000,1),(12,1,'1','Holding educational courses in commercial laws',1,50.000,3,50,1,1,1,'Holding educational courses in commercial laws','Number of training programs implemented',3,1,'Mohannad','2019-10-12',NULL,'2019-10-31','Holding educational courses in commercial laws',10.000,40.000,80.000,40.000,8.000,NULL,100000.000,1),(13,1,'1','Holding specialized courses in the commercial field',1,30.000,1,8,1,1,1,'Holding specialized courses in the commercial field','Number of sessions implemented',4,1,'Mohannad','2019-10-12',NULL,'2019-10-31','Holding specialized courses in the commercial field',4.500,4.000,50.000,15.000,2.250,20000.000,40000.000,1),(14,1,'1','Examining the competence of employees',1,30.000,1,100,1,1,1,'Examining the competence of employees','Check all employees',4,1,'Mohannad','2019-10-12',NULL,'2019-10-31','Examining the competence of employees',4.500,50.000,50.000,15.000,2.250,30000.000,60000.000,1),(15,1,'1','Implement transparency and incentive system',1,30.000,4,100,1,1,1,'Implement transparency and incentive system','Implement the incentive system',4,1,'Mohannad','2019-10-12',NULL,'2019-10-31','Implement transparency and incentive system',4.500,30.000,30.000,9.000,1.350,NULL,100000.000,1),(16,1,'1','Create a clear career path',1,10.000,4,100,1,1,1,'Create a clear career path','Percentage of study completion',4,1,'Mohannad','2019-10-12',NULL,'2019-10-31','Create a clear career path',1.500,70.000,70.000,7.000,1.050,NULL,100000.000,1),(28,1,'1','test',1,9.000,1,10,1,1,0,'null','hhh',2,1,'ADMIN','2019-10-18',NULL,'2019-11-11','',3.000,9.000,90.000,9.000,2.700,NULL,1000.000,1),(32,1,'1','project 1',1,100.000,1,100,1,1,0,NULL,'undefined',11,1,'admin','2019-10-31',NULL,'2019-10-31','مشروع 1',15.000,90.000,90.000,90.000,13.500,NULL,1000.000,2),(41,1,'1','ddd',1,1.000,1,100,2,1,0,NULL,'kpi',2,1,'admin','2019-11-11',NULL,'2019-11-11','',NULL,NULL,NULL,NULL,NULL,NULL,1000.000,NULL);
/*!40000 ALTER TABLE `adm_projects` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `adm_roles`
--

DROP TABLE IF EXISTS `adm_roles`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
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
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `adm_roles`
--

LOCK TABLES `adm_roles` WRITE;
/*!40000 ALTER TABLE `adm_roles` DISABLE KEYS */;
INSERT INTO `adm_roles` VALUES (1,'Administrator',1,1,'ADMIN','2019-01-01',NULL,NULL,NULL);
/*!40000 ALTER TABLE `adm_roles` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `adm_scales`
--

DROP TABLE IF EXISTS `adm_scales`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
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
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `adm_scales`
--

LOCK TABLES `adm_scales` WRITE;
/*!40000 ALTER TABLE `adm_scales` DISABLE KEYS */;
INSERT INTO `adm_scales` VALUES (1,'ST','First',1,1,'admin','2019-09-17',NULL,NULL,NULL);
/*!40000 ALTER TABLE `adm_scales` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `adm_skills_types`
--

DROP TABLE IF EXISTS `adm_skills_types`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `adm_skills_types` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `code` varchar(200) NOT NULL,
  `name` varchar(2000) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `created_by` varchar(45) DEFAULT NULL,
  `created_date` date NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `adm_skills_types`
--

LOCK TABLES `adm_skills_types` WRITE;
/*!40000 ALTER TABLE `adm_skills_types` DISABLE KEYS */;
INSERT INTO `adm_skills_types` VALUES (1,'Com','Communication Skill',NULL,'0000-00-00'),(4,'01','skill1',NULL,'0000-00-00'),(5,'01','skill2',NULL,'0000-00-00');
/*!40000 ALTER TABLE `adm_skills_types` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `adm_stratigic_objectives`
--

DROP TABLE IF EXISTS `adm_stratigic_objectives`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
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
  `actual_cost` float(12,3) DEFAULT NULL,
  `planned_cost` float(12,3) DEFAULT NULL,
  `bsc` int(11) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `idx_ADM_STRATIGIC_OBJECTIVES` (`COMPANY_ID`),
  KEY `fk_adm_stratigic_obj_adm_years` (`year_id`),
  CONSTRAINT `fk_adm_stratigic_objectives_adm_company` FOREIGN KEY (`COMPANY_ID`) REFERENCES `adm_company` (`ID`),
  CONSTRAINT `fk_adm_stratigic_objectives_adm_years` FOREIGN KEY (`year_id`) REFERENCES `adm_years` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=20 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `adm_stratigic_objectives`
--

LOCK TABLES `adm_stratigic_objectives` WRITE;
/*!40000 ALTER TABLE `adm_stratigic_objectives` DISABLE KEYS */;
INSERT INTO `adm_stratigic_objectives` VALUES (1,1,'','1. Enhance the competitiveness of SME’s and national entrepreneurship.',1,20.000,'undefined',2019,'ADMIN','2019-09-24','admin','2019-11-02','تنظيم وتطوير',98.533,19.707,523000.000,1233000.000,NULL),(2,1,'','2. Increase the attraction of investments.',1,30.000,'undefined',2019,'ADMIN','2019-09-24','admin','2019-10-28','الهدف الثاني',86.500,25.950,50000.000,121000.000,NULL),(3,1,'','هدف',1,20.000,'undefined',2019,'ADMIN','2019-09-24','admin','2019-11-06','هدف',54.000,10.800,155900.000,411800.000,NULL),(4,1,'','4. Develop organizational and human competence ',1,15.000,'undefined',2019,'ADMIN','2019-09-26','admin','2019-10-28','تطوير المؤسسة وتنافس الموظفين',46.000,6.900,50000.000,300000.000,NULL),(11,1,'','5.  Ensure the provision of all administrative services in as per the standards of quality, efficiency, and transparency',1,15.000,'undefined',2019,'admin','2019-10-27','admin','2019-11-05',NULL,90.000,13.500,0.000,1000.000,NULL);
/*!40000 ALTER TABLE `adm_stratigic_objectives` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `adm_unit_projects_performance`
--

DROP TABLE IF EXISTS `adm_unit_projects_performance`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
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
  `Result_Weight_Percentage` float(12,3) DEFAULT NULL,
  `Result_Weight_Perc_From_Obj` float(12,3) DEFAULT NULL,
  `actual_cost` float(12,3) DEFAULT NULL,
  `planned_cost` float(12,3) DEFAULT NULL,
  `Projects_Weight_Perc_From_Objs` float(12,3) DEFAULT NULL,
  `Result_Weight_Perc_From_Objs` float(12,3) DEFAULT NULL,
  `Employee_Percentage` float(12,3) DEFAULT NULL,
  `Prjs_Level1_Employee` float(12,3) DEFAULT NULL,
  `Prjs_Level2_Employee` float(12,3) DEFAULT NULL,
  `Prjs_Level3_Employee` float(12,3) DEFAULT NULL,
  `Prjs_Level4_Employee` float(12,3) DEFAULT NULL,
  `Prjs_Level5_Employee` float(12,3) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `adm_unit_projects_performance`
--

LOCK TABLES `adm_unit_projects_performance` WRITE;
/*!40000 ALTER TABLE `adm_unit_projects_performance` DISABLE KEYS */;
INSERT INTO `adm_unit_projects_performance` VALUES (1,2019,1,1,2,12,0.240,0.960,8.400,1.800,0.600,0.023,0.092,0.804,0.172,0.057,35.333,NULL,NULL,113000.000,263000.000,5.000,1.767,NULL,0.065,0.260,2.275,0.488,0.163),(2,2019,1,1,3,1,0.020,0.080,0.700,0.150,0.050,0.117,0.467,4.090,0.877,0.292,69.154,NULL,NULL,340000.000,780000.000,13.000,8.990,NULL,0.169,0.676,5.915,1.267,0.422),(3,2019,1,1,5,14,0.280,1.120,9.800,2.100,0.700,0.088,0.351,3.071,0.658,0.219,75.000,NULL,NULL,0.000,20000.000,9.000,6.750,NULL,0.117,0.468,4.095,0.878,0.293),(4,2019,1,1,1,38,0.760,3.040,26.600,5.700,1.900,0.740,2.961,25.912,5.553,1.851,85.000,NULL,NULL,325900.000,803800.000,67.000,56.950,NULL,0.871,3.484,30.485,6.533,2.178),(5,2019,1,1,4,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL);
/*!40000 ALTER TABLE `adm_unit_projects_performance` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `adm_units`
--

DROP TABLE IF EXISTS `adm_units`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
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
  KEY `idx_ADM_UNITS` (`C_UNIT_TYPE_ID`),
  KEY `idx_ADM_UNITS_0` (`COMPANY_ID`),
  KEY `fk_adm_units_adm_units` (`parent_id`),
  CONSTRAINT `fk_ADM_UNITS_0` FOREIGN KEY (`COMPANY_ID`) REFERENCES `adm_company` (`ID`),
  CONSTRAINT `fk_adm_units_adm_company` FOREIGN KEY (`COMPANY_ID`) REFERENCES `adm_company` (`ID`),
  CONSTRAINT `fk_adm_units_adm_units` FOREIGN KEY (`parent_id`) REFERENCES `adm_units` (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `adm_units`
--

LOCK TABLES `adm_units` WRITE;
/*!40000 ALTER TABLE `adm_units` DISABLE KEYS */;
INSERT INTO `adm_units` VALUES (1,'Commercial Registration','HR','Amman',NULL,NULL,NULL,2,1,NULL,'admin','2019-10-12','Mohannad','2019-10-12','ادارة التسجيل التجاري'),(2,'Department of Enterprise Support','ES','Amman',NULL,NULL,NULL,2,1,NULL,'admin','2019-10-12','Mohannad','2019-10-12','إدارة دعم المؤسسات'),(3,'Manage Enterprise Empowerment','EE','Amman',NULL,NULL,NULL,2,1,NULL,'admin','2019-10-12','Mohannad','2019-10-12','إدارة تمكين المؤسسات'),(4,'Industrial Licensing Department ','EL','Amman','null','null','null',2,1,NULL,'admin','2019-10-12','Admin','2019-10-24','????? ???????? ????????'),(5,'SME Management','SME','Amman','null','null','null',2,1,NULL,'admin','2019-10-12','Admin','2019-10-24','????? ???????? ??????? ?????????'),(6,'Industrial Property Management','IP','Amman',NULL,NULL,NULL,2,1,NULL,'admin','2019-10-12','Mohannad','2019-10-12','إدارة الملكية الصناعية'),(7,'HR ','01',NULL,NULL,NULL,NULL,2,1,NULL,'admin','2019-10-31',NULL,NULL,NULL),(8,'Strategic Development','01',NULL,NULL,NULL,NULL,1,1,NULL,'admin','2019-11-01',NULL,NULL,NULL);
/*!40000 ALTER TABLE `adm_units` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `adm_users`
--

DROP TABLE IF EXISTS `adm_users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
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
INSERT INTO `adm_users` VALUES ('ADMIN','admin',1,'123','ADMIN','2019-02-02',NULL,NULL,NULL);
/*!40000 ALTER TABLE `adm_users` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `adm_users_roles`
--

DROP TABLE IF EXISTS `adm_users_roles`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
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
  KEY `idx_ADM_USERS_USERNAME` (`USERNAME`),
  CONSTRAINT `fk_adm_users_roles_adm_roles` FOREIGN KEY (`ROLE_ID`) REFERENCES `adm_roles` (`ID`),
  CONSTRAINT `fk_adm_users_roles_adm_users` FOREIGN KEY (`USERNAME`) REFERENCES `adm_users` (`USERNAME`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `adm_users_roles`
--

LOCK TABLES `adm_users_roles` WRITE;
/*!40000 ALTER TABLE `adm_users_roles` DISABLE KEYS */;
INSERT INTO `adm_users_roles` VALUES (1,'ADMIN',1,'ADMIN','2019-01-01',NULL,NULL);
/*!40000 ALTER TABLE `adm_users_roles` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `adm_years`
--

DROP TABLE IF EXISTS `adm_years`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `adm_years` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `created_by` varchar(100) NOT NULL,
  `created_date` date NOT NULL,
  `modified_by` varchar(100) DEFAULT NULL,
  `modified_date` date DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2021 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `adm_years`
--

LOCK TABLES `adm_years` WRITE;
/*!40000 ALTER TABLE `adm_years` DISABLE KEYS */;
INSERT INTO `adm_years` VALUES (2019,'admin','2019-09-22',NULL,NULL),(2020,'admin','2019-10-10',NULL,NULL);
/*!40000 ALTER TABLE `adm_years` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tbl_emp_levels`
--

DROP TABLE IF EXISTS `tbl_emp_levels`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
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
-- Table structure for table `tbl_perf_level_quota`
--

DROP TABLE IF EXISTS `tbl_perf_level_quota`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_perf_level_quota` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `year_id` int(11) NOT NULL,
  `from_percentage` float(12,3) NOT NULL,
  `to_percentage` float(12,3) NOT NULL,
  `lvl_number` int(11) NOT NULL,
  `quota_type` int(11) NOT NULL,
  `company_Id` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `adm_level_quota_adm_year_idx` (`year_id`),
  KEY `adm_level_quota_adm_company_idx` (`company_Id`),
  CONSTRAINT `adm_level_quota_adm_year` FOREIGN KEY (`year_id`) REFERENCES `adm_years` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=26 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tbl_perf_level_quota`
--

LOCK TABLES `tbl_perf_level_quota` WRITE;
/*!40000 ALTER TABLE `tbl_perf_level_quota` DISABLE KEYS */;
INSERT INTO `tbl_perf_level_quota` VALUES (1,2019,100.000,200.000,5,1,1),(2,2019,100.000,200.000,4,1,1),(3,2019,100.000,200.000,3,2,1),(4,2019,100.000,200.000,2,0,1),(5,2019,100.000,200.000,1,0,1),(6,2019,91.000,99.000,5,1,1),(7,2019,91.000,99.000,4,1,1),(8,2019,91.000,99.000,3,2,1),(9,2019,91.000,99.000,2,1,1),(10,2019,91.000,99.000,1,0,1),(11,2019,71.000,90.000,5,0,1),(12,2019,71.000,90.000,4,0,1),(13,2019,71.000,90.000,3,2,1),(14,2019,71.000,90.000,2,1,1),(15,2019,71.000,90.000,1,1,1),(16,2019,51.000,70.000,5,0,1),(17,2019,51.000,70.000,4,0,1),(18,2019,51.000,70.000,3,2,1),(19,2019,51.000,70.000,2,1,1),(20,2019,51.000,70.000,1,1,1),(21,2019,0.000,50.000,5,0,1),(22,2019,0.000,50.000,4,0,1),(23,2019,0.000,50.000,3,2,1),(24,2019,0.000,50.000,2,1,1),(25,2019,0.000,50.000,1,1,1);
/*!40000 ALTER TABLE `tbl_perf_level_quota` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tbl_performance_levels`
--

DROP TABLE IF EXISTS `tbl_performance_levels`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_performance_levels` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `lvl_code` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `lvl_name` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `lvl_number` bigint(20) NOT NULL,
  `lvl_percent` bigint(20) NOT NULL,
  `lvl_year` int(11) NOT NULL,
  `company_id` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `adm_perf_level_adm_company_idx` (`company_id`),
  KEY `adm_perf_level_adm_year_idx` (`lvl_year`),
  CONSTRAINT `adm_per_level_adm_company` FOREIGN KEY (`company_id`) REFERENCES `adm_company` (`ID`),
  CONSTRAINT `adm_per_level_adm_year` FOREIGN KEY (`lvl_year`) REFERENCES `adm_years` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tbl_performance_levels`
--

LOCK TABLES `tbl_performance_levels` WRITE;
/*!40000 ALTER TABLE `tbl_performance_levels` DISABLE KEYS */;
INSERT INTO `tbl_performance_levels` VALUES (1,'','1',1,2,2019,1),(2,'','2',2,8,2019,1),(3,'','3',3,70,2019,1),(4,'','4',4,15,2019,1),(5,'','5',5,5,2019,1);
/*!40000 ALTER TABLE `tbl_performance_levels` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tbl_resources`
--

DROP TABLE IF EXISTS `tbl_resources`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_resources` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `url` varchar(2000) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `resource_key` varchar(2000) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `resource_value` varchar(2000) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `org_id` bigint(20) NOT NULL,
  `culture_name` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM AUTO_INCREMENT=1411 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tbl_resources`
--

LOCK TABLES `tbl_resources` WRITE;
/*!40000 ALTER TABLE `tbl_resources` DISABLE KEYS */;
INSERT INTO `tbl_resources` VALUES (1,'#/skillsTypes','lblSkillTypePageHeader','Skills Types',1,'en'),(2,'#/skillsTypes','lblSkillTypesSearchHeader','Skill Types',1,'en'),(3,'#/skillsTypes','lblSkillsEntry','Skill Entry',1,'en'),(4,'#/skillsTypes','lblSkillsCode','Code',1,'en'),(5,'#/skillsTypes','lblSkillsName','Name',1,'en'),(6,'#/skillsTypes','lblSkillsSave','Save',1,'en'),(7,'#/skillsTypes','lblSkillTypePageHeader','المهارات',1,'ar'),(8,'#/skillsTypes','lblSkillTypesSearchHeader','المهارات',1,'ar'),(9,'#/skillsTypes','lblSkillsEntry','ادخال المهارات',1,'ar'),(10,'#/skillsTypes','lblSkillsCode','الرمز',1,'ar'),(11,'#/skillsTypes','lblSkillsName','الاسم',1,'ar'),(12,'#/skillsTypes','lblSkillsSave','حفظ',1,'ar'),(326,'#/projectsAssessment','lblProjectsAssessmentList','قائمة تقييم المشاريع',1,'ar'),(325,'#/projectsAssessment','btnSearch','بحث',1,'ar'),(324,'#/projectsAssessment','lblSearchUnit','الوحدة',1,'ar'),(323,'#/projectsAssessment','lblSearchStratigicObjective','الاهداف الاستراتيجية',1,'ar'),(322,'#/projectsAssessment','All','الكل',1,'ar'),(321,'#/projectsAssessment','lblSearchYear','السنة',1,'ar'),(320,'#/projectsAssessment','lblSearch','بحث',1,'ar'),(319,'#/projectsAssessment','lblHeaderOrganizationObjectives','اهداف المنظمة',1,'ar'),(318,'#/projectsAssessment','lblHeaderProjectsAssessment','تقييم المشاريع',1,'ar'),(317,'#/projectsAssessment','lblSave','Save',1,'en'),(316,'#/projectsAssessment','thPercObj','Result %',1,'en'),(315,'#/projectsAssessment','thPrjPerce','Result % / Project',1,'en'),(314,'#/projectsAssessment','thPerce','Success Percentage %',1,'en'),(313,'#/projectsAssessment','thWObj','Strategic',1,'en'),(311,'#/projectsAssessment','thQ4','Q4',1,'en'),(312,'#/projectsAssessment','thResult','Result',1,'en'),(310,'#/projectsAssessment','thQ3','Q3',1,'en'),(309,'#/projectsAssessment','thQ2','Q2',1,'en'),(307,'#/projectsAssessment','thKPI','KPI/Measurement',1,'en'),(308,'#/projectsAssessment','thQ1','Q1',1,'en'),(306,'#/projectsAssessment','thWeight','Weight',1,'en'),(305,'#/projectsAssessment','thUnit','Unit',1,'en'),(304,'#/projectsAssessment','lblProjectsAssessmentList','Projects Assessment List',1,'en'),(303,'#/projectsAssessment','btnSearch','Search',1,'en'),(302,'#/projectsAssessment','lblSearchUnit','Unit',1,'en'),(301,'#/projectsAssessment','lblSearchStratigicObjective','Strategic Objective',1,'en'),(300,'#/projectsAssessment','All','All',1,'en'),(299,'#/projectsAssessment','lblSearchYear','Year',1,'en'),(298,'#/projectsAssessment','lblSearch','Search',1,'en'),(297,'#/projectsAssessment','lblHeaderOrganizationObjectives','Organization Objectives',1,'en'),(296,'#/projectsAssessment','lblHeaderProjectsAssessment','Projects Assessment',1,'en'),(59,'#/competence','HeaderSettings','Settings',1,'en'),(60,'#/competence','CompetenciesList','Competencies List',1,'en'),(61,'#/competence','CompetenceEntry','Competency Entry',1,'en'),(62,'#/competence','Code','Code',1,'en'),(63,'#/competence','Name','Name',1,'en'),(64,'#/competence','Nature','Type',1,'en'),(65,'#/competence','IsMandetory','Is Mandetory',1,'en'),(66,'#/competence','Save','Save',1,'en'),(67,'#/competence','KPIsList','Competency Indicators',1,'en'),(68,'#/competence','KPIEntry','Competency Indicator Entry',1,'en'),(69,'#/competence','HeaderCompetencies','الكفاءات',1,'ar'),(70,'#/competence','HeaderSettings','الكفاءات',1,'ar'),(71,'#/competence','CompetenciesList','قائمة الكفاءات',1,'ar'),(72,'#/competence','CompetenceEntry','ادخال الكفاءات',1,'ar'),(73,'#/competence','Code','الرمز',1,'ar'),(74,'#/competence','Name','الاسم',1,'ar'),(75,'#/competence','Nature','النوع',1,'ar'),(76,'#/competence','IsMandetory','مطلوب',1,'ar'),(77,'#/competence','Save','حفظ',1,'ar'),(78,'#/competence','KPIsList','قائمة المعايير',1,'ar'),(79,'#/competence','KPIEntry','ادخال المعايير',1,'ar'),(80,'#/competence','HeaderCompetencies','Competencies',1,'en'),(81,'#/positions','Code','Code',1,'en'),(82,'#/positions','Competence','Competency',1,'en'),(83,'#/positions','CompetenceEntry','Competency Entry',1,'en'),(84,'#/positions','Competencies','Competencies',1,'en'),(85,'#/positions','CompetenciesList','Competencies List',1,'en'),(86,'#/positions','Description','Description',1,'en'),(87,'#/positions','IsManagerialPosition','Is Managerial Position',1,'en'),(88,'#/positions','JobDescription','Job Description',1,'en'),(89,'#/positions','JobDescriptionEntry','Job Description Entry',1,'en'),(90,'#/positions','KPIsList','KPIs List',1,'en'),(91,'#/positions','Name','Name',1,'en'),(92,'#/positions','Positions','Positions',1,'en'),(93,'#/positions','PositionsEntry','Positions Entry',1,'en'),(94,'#/positions','PositionsList','Positions List',1,'en'),(95,'#/positions','Save','Save',1,'en'),(96,'#/positions','Settings','Settings',1,'en'),(97,'#/positions','Code','الرمز',1,'ar'),(98,'#/positions','Competence','الكفاءة',1,'ar'),(99,'#/positions','CompetenceEntry','ادخال الكفاءة',1,'ar'),(100,'#/positions','Competencies','الكفاءات',1,'ar'),(101,'#/positions','CompetenciesList','قائمة الكفاءات',1,'ar'),(102,'#/positions','Description','الوصف',1,'ar'),(103,'#/positions','IsManagerialPosition','موقع اداري',1,'ar'),(104,'#/positions','JobDescription','الوصف الوظيفي',1,'ar'),(105,'#/positions','JobDescriptionEntry','ادخال الوصف الوظيفي',1,'ar'),(106,'#/positions','KPIsList','قائمةالمعايير',1,'ar'),(107,'#/positions','Name','الاسم',1,'ar'),(108,'#/positions','Positions','الموقع الوظيفي',1,'ar'),(109,'#/positions','PositionsEntry','ادخال الموقع الوظيفي',1,'ar'),(110,'#/positions','PositionsList','قائمة المواقع الوظيفية',1,'ar'),(111,'#/positions','Save','حفظ',1,'ar'),(112,'#/positions','Settings','الاعدادات',1,'ar'),(113,'#/positions','JobDescriptionList','Job Description List',1,'en'),(114,'#/positions','JobDescriptionList','قائمة المهام الوظيفية',1,'ar'),(115,'#/positions','Nature','Type',1,'en'),(116,'#/positions','Nature','النوع',1,'ar'),(117,'#/positions','Mandetory','Mandetory',1,'en'),(118,'#/positions','Mandetory','اجباري',1,'ar'),(121,'#/positions','PositionDeleted','Position Deleted',1,'en'),(122,'#/positions','PositionDeleted','تم حذف الموقع الوظيفي',1,'ar'),(123,'#/positions','Pleasechooseaposition','Please choose a position',1,'en'),(124,'#/positions','Pleasechooseaposition','الرجاء اختيار الموقع الوظيفي',1,'ar'),(125,'#/positions','JobDescriptionDeleted','Job Description Deleted',1,'en'),(126,'#/positions','JobDescriptionDeleted','تم حذف المهام الوظيفية',1,'ar'),(127,'#/positions','CompetenceDeleted','Competency Deleted',1,'en'),(128,'#/positions','CompetenceDeleted','تم حذف الكفاءة',1,'ar'),(129,'#/positions','Areyousure','Are you sure ?',1,'en'),(130,'#/positions','Areyousure','هل انت متاكد ؟',1,'ar'),(131,'#/employeeObjectve','AgreementDate','Agreement Date',1,'en'),(132,'#/employeeObjectve','All','All',1,'en'),(133,'#/employeeObjectve','Areyousure','Are you sure ?',1,'en'),(134,'#/employeeObjectve','AssesmentDeleted','Assesment Deleted',1,'en'),(135,'#/employeeObjectve','AssesmentEntry','Assesment Entry',1,'en'),(136,'#/employeeObjectve','Competence','Competency',1,'en'),(137,'#/employeeObjectve','CompetenceDeleted','Competency Deleted',1,'en'),(138,'#/employeeObjectve','CompetenceEntry','Competency Entry',1,'en'),(139,'#/employeeObjectve','CompetenceKPIDeleted','Competency KPI Deleted',1,'en'),(140,'#/employeeObjectve','CompetenceKPIEntry','Competency KPI Entry',1,'en'),(141,'#/employeeObjectve','CompetenceKPIList','Competency KPI List',1,'en'),(142,'#/employeeObjectve','Competencies','Competencies',1,'en'),(143,'#/employeeObjectve','Employee','Employee',1,'en'),(144,'#/employeeObjectve','EmployeesAssesmentList','Employees Assesment List',1,'en'),(145,'#/employeeObjectve','EmployeesperfomancePlan','Employees Perfomance Plan',1,'en'),(146,'#/employeeObjectve','KPI','KPI',1,'en'),(147,'#/employeeObjectve','KPICycle','Reviewing Cycle',1,'en'),(148,'#/employeeObjectve','ObjectiveDeleted','Individual Objective Deleted',1,'en'),(149,'#/employeeObjectve','ObjectiveKPIDeleted','Individual Objective KPI Deleted',1,'en'),(150,'#/employeeObjectve','Objectivies','Individual Objectivies',1,'en'),(151,'#/employeeObjectve','Organization','Organization',1,'en'),(152,'#/employeeObjectve','PleasechooseaAssement','Please choose a Assement',1,'en'),(153,'#/employeeObjectve','PleasechooseaCompetence','Please choose a Competency',1,'en'),(154,'#/employeeObjectve','PleasechooseaObjective','Please choose a Individual Objective',1,'en'),(155,'#/employeeObjectve','Reviewer','Direct Manager',1,'en'),(156,'#/employeeObjectve','Save','Save',1,'en'),(157,'#/employeeObjectve','Search','Search',1,'en'),(158,'#/employeeObjectve','Weight','Weight %',1,'en'),(159,'#/employeeObjectve','Year','Year',1,'en'),(160,'#/employeeObjectve','Code','Code',1,'en'),(161,'#/employeeObjectve','Name','Name',1,'en'),(162,'#/employeeObjectve','Areyousure','هل انت متاكد ؟',1,'ar'),(163,'#/employeeObjectve','AgreementDate','تاريخ الاتفاقية',1,'ar'),(164,'#/employeeObjectve','All','الكل',1,'ar'),(165,'#/employeeObjectve','AssesmentDeleted','تم حذف التقييم',1,'ar'),(166,'#/employeeObjectve','AssesmentEntry','ادخال التقييم',1,'ar'),(167,'#/employeeObjectve','Competence','الكفاءة',1,'ar'),(168,'#/employeeObjectve','CompetenceDeleted','تم حذف الكفاءة',1,'ar'),(169,'#/employeeObjectve','CompetenceEntry','ادخال الكفاءة',1,'ar'),(170,'#/employeeObjectve','CompetenceKPIDeleted','تم حذف المعيار',1,'ar'),(171,'#/employeeObjectve','CompetenceKPIEntry','ادخال معيار الكفاءة',1,'ar'),(172,'#/employeeObjectve','CompetenceKPIList','قائمة معايير الكفاءة',1,'ar'),(173,'#/employeeObjectve','Competencies','الكفاءات',1,'ar'),(174,'#/employeeObjectve','Employee','الموظف',1,'ar'),(175,'#/employeeObjectve','EmployeesAssesmentList','قائمة تقييم الموظفيين',1,'ar'),(176,'#/employeeObjectve','EmployeesperfomancePlan','خطة اداء الموظفيين',1,'ar'),(177,'#/employeeObjectve','KPI','المعايير',1,'ar'),(178,'#/employeeObjectve','KPICycle','مراجعة الدورة',1,'ar'),(179,'#/employeeObjectve','ObjectiveDeleted','تم حذف الهدف الفردي',1,'ar'),(180,'#/employeeObjectve','ObjectiveKPIDeleted','تم حذف المعيار',1,'ar'),(181,'#/employeeObjectve','Objectivies','الاهداف الفردية',1,'ar'),(182,'#/employeeObjectve','Organization','المنظمة',1,'ar'),(183,'#/employeeObjectve','PleasechooseaAssement','الرجاء اختيار التقييم',1,'ar'),(184,'#/employeeObjectve','PleasechooseaCompetence','الرجاء اختيار  الكفاءة',1,'ar'),(185,'#/employeeObjectve','PleasechooseaObjective','الرجاء اختيار  الهدف الفردي',1,'ar'),(186,'#/employeeObjectve','Reviewer','المدير المباشر',1,'ar'),(187,'#/employeeObjectve','Save','حفظ',1,'ar'),(188,'#/employeeObjectve','Search','بحث',1,'ar'),(189,'#/employeeObjectve','Weight','الوزن %',1,'ar'),(190,'#/employeeObjectve','Year','السنة',1,'ar'),(192,'#/employeeObjectve','Name','الاسم',1,'ar'),(193,'#/employeeObjectve','ObjectiveList','Individual Objective List',1,'en'),(194,'#/employeeObjectve','ObjectiveKPIList','Individual Objective KPI List',1,'en'),(195,'#/employeeObjectve','ObjectiveKPIEntry','Individual Objective KPI Entry',1,'en'),(197,'#/employeeObjectve','ObjectiveEntry','Individual Objective Entry',1,'en'),(200,'#/employeeObjectve','Project','Project',1,'en'),(201,'#/employeeObjectve','PositionDesc','Position Desc.',1,'en'),(202,'#/employeeObjectve','Note','Note',1,'en'),(203,'#/employeeObjectve','ObjectiveKPIList','قائمة معايير الهدف الفردي',1,'ar'),(204,'#/employeeObjectve','ObjectiveKPIEntry','ادخال معيار الهدف الفردي',1,'ar'),(205,'#/employeeObjectve','ObjectiveEntry','ادخال الاهداف الفردية',1,'ar'),(206,'#/employeeObjectve','Code','الرمز',1,'ar'),(207,'#/employeeObjectve','Project','المشروع',1,'ar'),(208,'#/employeeObjectve','PositionDesc','وصف المهام',1,'ar'),(209,'#/employeeObjectve','Note','ملاحظات',1,'ar'),(210,'#/employeeObjectve','ObjectiveList','قائمة الاهداف الفردية',1,'ar'),(211,'#/employeeObjectve','CompetenceList','Competency List',1,'en'),(212,'#/employeeObjectve','CompetenceList','قائمة الكفاءات',1,'ar'),(213,'#/competence','Areyousure','Are you sure!',1,'en'),(214,'#/competence','Pleasechooseacompetence','Please choose a competency!',1,'en'),(215,'#/competence','CompetenceDeleted','Competency Deleted',1,'en'),(216,'#/competence','CompetenceKPIDeleted','Competency KPI Deleted.',1,'en'),(217,'#/competence','Areyousure','هل انت متاكد !',1,'ar'),(218,'#/competence','Pleasechooseacompetence','الرجاء اختيار الكفاءة',1,'ar'),(219,'#/competence','CompetenceDeleted','تم حذف الكفاءة',1,'ar'),(220,'#/competence','CompetenceKPIDeleted','تم حف المعيار',1,'ar'),(221,'#/employeeObjectve','Target','Target',1,'en'),(222,'#/employeeObjectve','Target','استهداف',1,'ar'),(223,'#/competence','SearchCompetence','Competency',1,'en'),(224,'#/competence','Search','Search',1,'en'),(225,'#/competence','SearchCompetence','الكفاءة',1,'ar'),(226,'#/competence','Search','بحث',1,'ar'),(227,'#/competence','Level','Level (PL)',1,'en'),(228,'#/competence','Level','المستوى',1,'ar'),(229,'#/Projects','lblQ4Target','المستهدف للربع 4',1,'ar'),(230,'#/Projects','lblQ4Target','Q4 Target',1,'en'),(231,'#/Projects','lblQ3Target','المستهدف للربع 3',1,'ar'),(232,'#/Projects','lblQ3Target','Q3 Target',1,'en'),(233,'#/Projects','lblQ2Target','المستهدف للربع 2',1,'ar'),(234,'#/Projects','lblQ2Target','Q2 Target',1,'en'),(235,'#/Projects','lblQ1Target','المستهدف للربع 1',1,'ar'),(236,'#/Projects','lblQ1Target','Q1 Target',1,'en'),(237,'#/Projects','lblDescription','الوصف',1,'ar'),(238,'#/Projects','lblDescription','Description',1,'en'),(239,'#/Projects','lblResultUnit','وحدة النتيجة',1,'ar'),(240,'#/Projects','lblResultUnit','Result Unit',1,'en'),(241,'#/Projects','lblKPIType','طريقة الاحتساب',1,'ar'),(242,'#/Projects','lblKPIType','KPI Type',1,'en'),(243,'#/Projects','lblKPICycle','Review Cycle',1,'en'),(244,'#/Projects','lblKPICycle','دورية المراجعة',1,'ar'),(245,'#/Projects','lblTarget','المستهدف',1,'ar'),(246,'#/Projects','lblKPIs','المؤشرات',1,'ar'),(247,'#/Projects','lblTarget','Annual Target',1,'en'),(248,'#/Projects','lblKPIs','KPIs-Measurment',1,'en'),(249,'#/Projects','lblPleaseSelect','الرجاء الاختيار',1,'ar'),(250,'#/Projects','lblPleaseSelect','lblPleaseSelect',1,'en'),(251,'#/Projects','lblProjectName','المشروع',1,'ar'),(252,'#/Projects','lblProjectName','Project',1,'en'),(253,'#/Projects','lblProject','المشروع',1,'ar'),(254,'#/Projects','lblProject','Project',1,'en'),(257,'#/Projects','lblQ4T','المستهدف ربع 4',1,'ar'),(258,'#/Projects','lblQ4T','Q4 T',1,'en'),(259,'#/Projects','lblQ3T','المستهدف ربع 3',1,'ar'),(260,'#/Projects','lblQ3T','Q3 T',1,'en'),(261,'#/Projects','lblQ2T','المستهدف ربع 2',1,'ar'),(262,'#/Projects','lblQ2T','Q2 T',1,'en'),(263,'#/Projects','lblQ1T','المستهدف ربع 1',1,'ar'),(264,'#/Projects','lblQ1T','Q1 T',1,'en'),(265,'#/Projects','lblWeight','الوزن %',1,'ar'),(266,'#/Projects','lblWeight','Weight %',1,'en'),(278,'#/Projects','lblProjectDetails','تفاصيل المشروع',1,'ar'),(279,'#/Projects','lblProjectDetails','Project Details',1,'en'),(280,'#/Projects','lblUnit','القسم',1,'ar'),(281,'#/Projects','lblUnit','Unit',1,'en'),(282,'#/Projects','lblAll','--- الكل ---',1,'ar'),(283,'#/Projects','lblAll','--- All ---',1,'en'),(284,'#/Projects','lblStratigicObjective','الهدف الاستراتيجي',1,'ar'),(285,'#/Projects','lblStratigicObjective','Strategic Objective',1,'en'),(286,'#/Projects','lblYear','السنة',1,'ar'),(287,'#/Projects','lblYear','Year',1,'en'),(288,'#/Projects','lblSearch','بحث',1,'ar'),(289,'#/Projects','lblSearch','Search',1,'en'),(290,'#/Projects','lblProjects','المشاريع',1,'ar'),(291,'#/Projects','lblProjects','Projects',1,'en'),(292,'#/Projects','lblSettings','الاعدادات',1,'ar'),(293,'#/Projects','lblSettings','Settings',1,'en'),(294,'#/Projects','lblOrganizationProjects','مشاريع المؤسسة',1,'ar'),(295,'#/Projects','lblOrganizationProjects','Organization Projects',1,'en'),(327,'#/projectsAssessment','thUnit','الوحدة',1,'ar'),(328,'#/projectsAssessment','thWeight','الوزن',1,'ar'),(329,'#/projectsAssessment','thKPI','KPI',1,'ar'),(330,'#/projectsAssessment','thQ1','Q1',1,'ar'),(331,'#/projectsAssessment','thQ2','Q2',1,'ar'),(332,'#/projectsAssessment','thQ3','Q3',1,'ar'),(333,'#/projectsAssessment','thQ4','Q4',1,'ar'),(334,'#/projectsAssessment','thResult','النتيجة',1,'ar'),(335,'#/projectsAssessment','thWObj','WObj',1,'ar'),(336,'#/projectsAssessment','thPerce','%',1,'ar'),(337,'#/projectsAssessment','thPrjPerce','Prj %',1,'ar'),(338,'#/projectsAssessment','thPercObj','Obj %',1,'ar'),(339,'#/projectsAssessment','lblSave','حفظ',1,'ar'),(340,'#/projectsAssessment','thProject','Project',1,'en'),(341,'#/projectsAssessment','thProject','المشروع',1,'ar'),(342,'#/projectsAssessment','thActualCost','Actual Cost',1,'en'),(343,'#/projectsAssessment','thActualCost','التكلفة الفعلية',1,'ar'),(344,'#/projectsAssessment','thPlannedCost','Planned Cost',1,'en'),(345,'#/projectsAssessment','thPlannedCost','التكلفة المخططة',1,'ar'),(346,'#/projectsAssessment','thEvidencesFiles','Evidences Files',1,'en'),(347,'#/projectsAssessment','thEvidencesFiles','الملفات',1,'ar'),(348,'#/projectsAssessment','thTarget','Annual Target',1,'en'),(349,'#/projectsAssessment','thTarget','المستهدف',1,'ar'),(350,'#/projectsAssessment','lblUploadFile','Upload file...',1,'en'),(351,'#/projectsAssessment','lblUploadFile','رفع ملف...',1,'ar'),(352,'#/projectsAssessment','lblUpload','Upload',1,'en'),(353,'#/projectsAssessment','lblUpload','رفع',1,'ar'),(354,'#/projectsAssessment','lblCancel','Cancel',1,'en'),(355,'#/projectsAssessment','lblCancel','الغاء',1,'ar'),(356,'#/projectsAssessment','lblOneFileAtLeastMsg','Please select at least 1 files to upload!',1,'en'),(357,'#/projectsAssessment','lblOneFileAtLeastMsg','الرجاء اختيار ملف واحد على الاقل لرفعه',1,'ar'),(358,'#/projectsAssessment','lblThreeFileMaxMsg','Please select a maximum of 3 files to upload!',1,'en'),(359,'#/projectsAssessment','lblThreeFileMaxMsg','الرجاء اختيار ثلاث ملفات على الاكثر لرفعهم',1,'ar'),(360,'#/projectsAssessment','lblSavedSuccessfully','Saved Successfully.',1,'en'),(361,'#/projectsAssessment','lblSavedSuccessfully','تم الحفظ بنجاح',1,'ar'),(362,'#/projectsAssessment','lblChooseFile','Choose File',1,'en'),(363,'#/projectsAssessment','lblChooseFile','اختر ملف',1,'ar'),(364,'#/employeeObjectve','Unit','Unit',1,'en'),(365,'#/employeeObjectve','Unit','القسم',1,'ar'),(383,'#/stratigicobjectives','lblSearch','بحث',1,'ar'),(382,'#/stratigicobjectives','lblSearch','Search',1,'en'),(381,'#/stratigicobjectives','lblOrganization','المؤسسة',1,'ar'),(380,'#/stratigicobjectives','lblOrganization','Organization',1,'en'),(379,'#/stratigicobjectives','lblStratigicObjectives','الأهداف الاستراتيجية',1,'ar'),(378,'#/stratigicobjectives','lblStratigicObjectives','Strategic Objectives',1,'en'),(384,'#/stratigicobjectives','lblYear','Year',1,'en'),(385,'#/stratigicobjectives','lblYear','السنة',1,'ar'),(386,'#/stratigicobjectives','lblPleaseSelect','Please Select',1,'en'),(387,'#/stratigicobjectives','lblPleaseSelect','الرجاء الاختيار',1,'ar'),(388,'#/stratigicobjectives','lblDetails','Details',1,'en'),(389,'#/stratigicobjectives','lblDetails','التفاصيل',1,'ar'),(390,'#/stratigicobjectives','lblObjectiveName','Objective',1,'en'),(391,'#/stratigicobjectives','lblObjectiveName','الهدف',1,'ar'),(392,'#/stratigicobjectives','lblObjectivesWeight','Weight %',1,'en'),(393,'#/stratigicobjectives','lblObjectivesWeight','الوزن %',1,'ar'),(394,'#/stratigicobjectives','lblEdit','Edit',1,'en'),(395,'#/stratigicobjectives','lblEdit','تعديل',1,'ar'),(396,'#/stratigicobjectives','lblDelete','Delete',1,'en'),(397,'#/stratigicobjectives','lblDelete','حذف',1,'ar'),(398,'#/stratigicobjectives','SuccessOperationMsg','Operation done successfully',1,'en'),(399,'#/stratigicobjectives','SuccessOperationMsg','تمت العملية بنجاح',1,'ar'),(400,'#/stratigicobjectives','lblSave','Save',1,'en'),(401,'#/stratigicobjectives','lblSave','حفظ',1,'ar'),(402,'#/projectsAssessment','lblUnit','Unit',1,'en'),(403,'#/projectsAssessment','lblUnit','القسم',1,'ar'),(404,'#/stratigicobjectivesChart','lblStratigicObjectives','Strategic Objectives',1,'en'),(405,'#/stratigicobjectivesChart','lblStratigicObjectives','الأهداف الاستراتيجية',1,'ar'),(406,'#/stratigicobjectivesChart','lblOrganization','Organization',1,'en'),(407,'#/stratigicobjectivesChart','lblOrganization','المؤسسة',1,'ar'),(408,'#/stratigicobjectivesChart','lblSearch','Search',1,'en'),(410,'#/stratigicobjectivesChart','lblYear','Year',1,'en'),(412,'#/stratigicobjectivesChart','lblPleaseSelect','Please Select',1,'en'),(413,'#/stratigicobjectivesChart','lblPleaseSelect','الرجاء الاختيار',1,'ar'),(414,'#/stratigicobjectivesChart','lblDetails','Details',1,'en'),(415,'#/stratigicobjectivesChart','lblDetails','التفاصيل',1,'ar'),(416,'#/stratigicobjectivesChart','lblObjectiveName','Objective',1,'en'),(417,'#/stratigicobjectivesChart','lblObjectiveName','الهدف',1,'ar'),(418,'#/stratigicobjectivesChart','lblObjectivesWeight','Weight',1,'en'),(419,'#/stratigicobjectivesChart','lblObjectivesWeight','الوزن',1,'ar'),(420,'#/stratigicobjectivesChart','lblEdit','Edit',1,'en'),(421,'#/stratigicobjectivesChart','lblEdit','تعديل',1,'ar'),(422,'#/stratigicobjectivesChart','lblDelete','Delete',1,'en'),(423,'#/stratigicobjectivesChart','lblDelete','حذف',1,'ar'),(424,'#/stratigicobjectivesChart','SuccessOperationMsg','Operation done successfully',1,'en'),(425,'#/stratigicobjectivesChart','SuccessOperationMsg','تمت العملية بنجاح',1,'ar'),(426,'#/stratigicobjectivesChart','lblSave','Save',1,'en'),(427,'#/stratigicobjectivesChart','lblSave','حفظ',1,'ar'),(428,'#/stratigicobjectivesChart','lblQ4Target','المستهدف للربع 4',1,'ar'),(429,'#/stratigicobjectivesChart','lblQ4Target','Q4 Target',1,'en'),(430,'#/stratigicobjectivesChart','lblQ3Target','المستهدف للربع 3',1,'ar'),(431,'#/stratigicobjectivesChart','lblQ3Target','Q3 Target',1,'en'),(432,'#/stratigicobjectivesChart','lblQ2Target','المستهدف للربع 2',1,'ar'),(433,'#/stratigicobjectivesChart','lblQ2Target','Q2 Target',1,'en'),(434,'#/stratigicobjectivesChart','lblQ1Target','المستهدف للربع 1',1,'ar'),(435,'#/stratigicobjectivesChart','lblQ1Target','Q1 Target',1,'en'),(436,'#/stratigicobjectivesChart','lblDescription','الوصف',1,'ar'),(437,'#/stratigicobjectivesChart','lblDescription','Description',1,'en'),(438,'#/stratigicobjectivesChart','lblResultUnit','وحدة النتيجة',1,'ar'),(439,'#/stratigicobjectivesChart','lblResultUnit','Result Unit',1,'en'),(440,'#/stratigicobjectivesChart','lblKPIType','طريقة الاحتساب',1,'ar'),(441,'#/stratigicobjectivesChart','lblKPIType','KPI Type',1,'en'),(442,'#/stratigicobjectivesChart','lblKPICycle','Review Cycle',1,'en'),(443,'#/stratigicobjectivesChart','lblKPICycle','دورية المراجعة',1,'ar'),(444,'#/stratigicobjectivesChart','lblTarget','المستهدف',1,'ar'),(445,'#/stratigicobjectivesChart','lblKPIs','المؤشرات',1,'ar'),(446,'#/stratigicobjectivesChart','lblTarget','Target',1,'en'),(447,'#/stratigicobjectivesChart','lblKPIs','KPIs',1,'en'),(450,'#/stratigicobjectivesChart','lblProjectName','المشروع',1,'ar'),(451,'#/stratigicobjectivesChart','lblProjectName','Project',1,'en'),(452,'#/stratigicobjectivesChart','lblProject','المشروع',1,'ar'),(453,'#/stratigicobjectivesChart','lblProject','Project',1,'en'),(456,'#/stratigicobjectivesChart','lblQ4T','المستهدف ربع 3',1,'ar'),(457,'#/stratigicobjectivesChart','lblQ4T','Q3 T',1,'en'),(458,'#/stratigicobjectivesChart','lblQ3T','المستهدف ربع 3',1,'ar'),(459,'#/stratigicobjectivesChart','lblQ3T','Q3 T',1,'en'),(461,'#/stratigicobjectivesChart','lblQ2T','Q2 T',1,'en'),(462,'#/stratigicobjectivesChart','lblQ1T','المستهدف ربع 1',1,'ar'),(463,'#/stratigicobjectivesChart','lblQ1T','Q1 T',1,'en'),(464,'#/stratigicobjectivesChart','lblWeight','الوزن',1,'ar'),(472,'#/stratigicobjectivesChart','lblQ2T','المستهدف ربع 2',1,'ar'),(476,'#/stratigicobjectivesChart','lblWeight','Weight',1,'en'),(477,'#/stratigicobjectivesChart','lblProjectDetails','تفاصيل المشروع',1,'ar'),(478,'#/stratigicobjectivesChart','lblProjectDetails','Project Details',1,'en'),(479,'#/stratigicobjectivesChart','lblUnit','القسم',1,'ar'),(480,'#/stratigicobjectivesChart','lblUnit','Unit',1,'en'),(481,'#/stratigicobjectivesChart','lblAll','--- الكل ---',1,'ar'),(482,'#/stratigicobjectivesChart','lblAll','--- All ---',1,'en'),(483,'#/stratigicobjectivesChart','lblStratigicObjective','الهدف الاستراتيجي',1,'ar'),(484,'#/stratigicobjectivesChart','lblStratigicObjective','Strategic Objective',1,'en'),(485,'#/stratigicobjectivesChart','lblYear','السنة',1,'ar'),(487,'#/stratigicobjectivesChart','lblSearch','بحث',1,'ar'),(489,'#/stratigicobjectivesChart','lblProjects','المشاريع',1,'ar'),(490,'#/stratigicobjectivesChart','lblProjects','Projects',1,'en'),(491,'#/stratigicobjectivesChart','lblSettings','الاعدادات',1,'ar'),(492,'#/stratigicobjectivesChart','lblSettings','Settings',1,'en'),(493,'#/stratigicobjectivesChart','lblOrganizationProjects','مشاريع المؤسسة',1,'ar'),(494,'#/stratigicobjectivesChart','lblOrganizationProjects','Organization Projects',1,'en'),(495,'#/stratigicobjectivesChart','lblHeaderProjectsAssessment','Projects Assessment',1,'en'),(496,'#/stratigicobjectivesChart','lblHeaderOrganizationObjectives','Organization Objectives',1,'en'),(498,'#/stratigicobjectivesChart','lblSearchYear','Year',1,'en'),(499,'#/stratigicobjectivesChart','All','All',1,'en'),(500,'#/stratigicobjectivesChart','lblSearchStratigicObjective','Strategic Objective',1,'en'),(501,'#/stratigicobjectivesChart','lblSearchUnit','Unit',1,'en'),(502,'#/stratigicobjectivesChart','btnSearch','Search',1,'en'),(503,'#/stratigicobjectivesChart','lblProjectsAssessmentList','Projects Assessment List',1,'en'),(504,'#/stratigicobjectivesChart','thUnit','Unit',1,'en'),(505,'#/stratigicobjectivesChart','thWeight','Weight',1,'en'),(506,'#/stratigicobjectivesChart','thKPI','KPI',1,'en'),(507,'#/stratigicobjectivesChart','thQ1','Q1',1,'en'),(508,'#/stratigicobjectivesChart','thQ2','Q2',1,'en'),(509,'#/stratigicobjectivesChart','thQ3','Q3',1,'en'),(510,'#/stratigicobjectivesChart','thQ4','Q4',1,'en'),(511,'#/stratigicobjectivesChart','thResult','Result',1,'en'),(512,'#/stratigicobjectivesChart','thWObj','WObj',1,'en'),(513,'#/stratigicobjectivesChart','thPerce','%',1,'en'),(514,'#/stratigicobjectivesChart','thPrjPerce','Prj %',1,'en'),(515,'#/stratigicobjectivesChart','thPercObj','Obj %',1,'en'),(517,'#/stratigicobjectivesChart','lblHeaderProjectsAssessment','تقييم المشاريع',1,'ar'),(518,'#/stratigicobjectivesChart','lblHeaderOrganizationObjectives','اهداف المنظمة',1,'ar'),(520,'#/stratigicobjectivesChart','lblSearchYear','السنة',1,'ar'),(521,'#/stratigicobjectivesChart','All','الكل',1,'ar'),(522,'#/stratigicobjectivesChart','lblSearchStratigicObjective','الاهداف الاستراتيجية',1,'ar'),(523,'#/stratigicobjectivesChart','lblSearchUnit','الوحدة',1,'ar'),(524,'#/stratigicobjectivesChart','btnSearch','بحث',1,'ar'),(525,'#/stratigicobjectivesChart','lblProjectsAssessmentList','قائمة تقييم المشاريع',1,'ar'),(526,'#/stratigicobjectivesChart','thUnit','الوحدة',1,'ar'),(527,'#/stratigicobjectivesChart','thWeight','الوزن',1,'ar'),(528,'#/stratigicobjectivesChart','thKPI','KPI',1,'ar'),(529,'#/stratigicobjectivesChart','thQ1','Q1',1,'ar'),(530,'#/stratigicobjectivesChart','thQ2','Q2',1,'ar'),(531,'#/stratigicobjectivesChart','thQ3','Q3',1,'ar'),(532,'#/stratigicobjectivesChart','thQ4','Q4',1,'ar'),(533,'#/stratigicobjectivesChart','thResult','النتيجة',1,'ar'),(534,'#/stratigicobjectivesChart','thWObj','WObj',1,'ar'),(535,'#/stratigicobjectivesChart','thPerce','%',1,'ar'),(536,'#/stratigicobjectivesChart','thPrjPerce','Prj %',1,'ar'),(537,'#/stratigicobjectivesChart','thPercObj','Obj %',1,'ar'),(539,'#/stratigicobjectivesChart','thProject','Project',1,'en'),(540,'#/stratigicobjectivesChart','thProject','المشروع',1,'ar'),(541,'#/stratigicobjectivesChart','thActualCost','Actual Cost',1,'en'),(542,'#/stratigicobjectivesChart','thActualCost','التكلفة الفعلية',1,'ar'),(543,'#/stratigicobjectivesChart','thPlannedCost','Planned Cost',1,'en'),(544,'#/stratigicobjectivesChart','thPlannedCost','التكلفة المخططة',1,'ar'),(545,'#/stratigicobjectivesChart','thEvidencesFiles','Evidences Files',1,'en'),(546,'#/stratigicobjectivesChart','thEvidencesFiles','الملفات',1,'ar'),(547,'#/stratigicobjectivesChart','thTarget','Annual Target',1,'en'),(548,'#/stratigicobjectivesChart','thTarget','المستهدف',1,'ar'),(549,'#/stratigicobjectivesChart','lblUploadFile','Upload file...',1,'en'),(550,'#/stratigicobjectivesChart','lblUploadFile','رفع ملف...',1,'ar'),(551,'#/stratigicobjectivesChart','lblUpload','Upload',1,'en'),(552,'#/stratigicobjectivesChart','lblUpload','رفع',1,'ar'),(553,'#/stratigicobjectivesChart','lblCancel','Cancel',1,'en'),(554,'#/stratigicobjectivesChart','lblCancel','الغاء',1,'ar'),(555,'#/stratigicobjectivesChart','lblOneFileAtLeastMsg','Please select at least 1 files to upload!',1,'en'),(556,'#/stratigicobjectivesChart','lblOneFileAtLeastMsg','الرجاء اختيار ملف واحد على الاقل لرفعه',1,'ar'),(557,'#/stratigicobjectivesChart','lblThreeFileMaxMsg','Please select a maximum of 3 files to upload!',1,'en'),(558,'#/stratigicobjectivesChart','lblThreeFileMaxMsg','الرجاء اختيار ثلاث ملفات على الاكثر لرفعهم',1,'ar'),(559,'#/stratigicobjectivesChart','lblSavedSuccessfully','Saved Successfully.',1,'en'),(560,'#/stratigicobjectivesChart','lblSavedSuccessfully','تم الحفظ بنجاح',1,'ar'),(561,'#/stratigicobjectivesChart','lblChooseFile','Choose File',1,'en'),(562,'#/stratigicobjectivesChart','lblChooseFile','اختر ملف',1,'ar'),(565,'master','Setup','Setup',1,'en'),(566,'master','SkillsTypes','Skills Types',1,'en'),(567,'master','Positions','Positions',1,'en'),(568,'master','Competencies','Competencies',1,'en'),(569,'master','Employees','Employees',1,'en'),(570,'master','Planning','Planning',1,'en'),(571,'master','StrategicObjectives','Strategic Objectives',1,'en'),(572,'master','Projects','Projects',1,'en'),(573,'master','ProjectsPlanner','Projects Planner',1,'en'),(574,'master','EmployeeStructure','Employee Structure',1,'en'),(575,'master','EmployeePerformancePlan','Employee Performance Plan',1,'en'),(576,'master','OperationAssessment','Operation & Assessment',1,'en'),(577,'master','ProjectsAssessment','Projects Assessment',1,'en'),(578,'master','ProjectsNavigation','Projects Navigation',1,'en'),(579,'master','EmployeesPerformanceAssessment','Employees Performance Assessment',1,'en'),(580,'master','EmployeeNavigation','Employee Navigation',1,'en'),(581,'master','DashboardAnalysis','Dashboard & Analysis',1,'en'),(582,'master','OrganizationDashboard','Organization Dashboard',1,'en'),(583,'master','EmployeDashboard','Employee Dashboard',1,'en'),(584,'master','Setup','الاعدادات',1,'ar'),(585,'master','SkillsTypes','المهارات',1,'ar'),(586,'master','Positions','المناصب',1,'ar'),(587,'master','Competencies','الكفاءات',1,'ar'),(588,'master','Employees','الموظفون',1,'ar'),(589,'master','Planning','تخطيط',1,'ar'),(590,'master','StrategicObjectives','الأهداف الإستراتيجية',1,'ar'),(591,'master','Projects','مشاريع',1,'ar'),(592,'master','ProjectsPlanner','مخطط المشاريع',1,'ar'),(593,'master','EmployeeStructure','هيكل الموظف',1,'ar'),(594,'master','EmployeePerformancePlan','خطة أداء الموظف',1,'ar'),(595,'master','OperationAssessment','التشغيل والتقييم',1,'ar'),(596,'master','ProjectsAssessment','تقييم المشروعات',1,'ar'),(597,'master','ProjectsNavigation','التنقل في المشروعات',1,'ar'),(598,'master','EmployeesPerformanceAssessment','تقييم أداء الموظفين',1,'ar'),(599,'master','EmployeeNavigation','تنقل الموظف',1,'ar'),(600,'master','DashboardAnalysis','لوحة القيادة والتحليل',1,'ar'),(601,'master','OrganizationDashboard','لوحة معلومات المنظمة',1,'ar'),(602,'master','EmployeDashboard','لوحة معلومات الموظف',1,'ar'),(603,'#/Projects','lblPlannedCost','التكلفة المخططة',1,'ar'),(604,'#/Projects','lblPlannedCost','Planned Cost',1,'en'),(605,'#/stratigicobjectivesChart','lblPlannedCost','التكلفة المخططة',1,'ar'),(606,'#/stratigicobjectivesChart','lblPlannedCost','Planned Cost',1,'en'),(609,'#/Employees','Name','Name',1,'en'),(611,'#/Employees','Position','Position',1,'en'),(612,'#/Employees','Status','Status',1,'en'),(613,'#/Employees','All','All',1,'en'),(614,'#/Employees','InActive','InActive',1,'en'),(615,'#/Employees','Active','Active',1,'en'),(616,'#/Employees','EmployeeEntry','Employee Entry',1,'en'),(618,'#/Employees','SecondName','Second Name',1,'en'),(619,'#/Employees','ThirdName','Third Name',1,'en'),(620,'#/Employees','FamilyName','Family Name',1,'en'),(622,'#/Employees','Scale','Scale',1,'en'),(623,'#/Employees','Phone1','Phone 1',1,'en'),(624,'#/Employees','Manager','Manager',1,'en'),(625,'#/Employees','Phone2','Phone 2',1,'en'),(626,'#/Employees','Save','Save',1,'en'),(627,'#/Employees','ImportEmployees','Import Employees',1,'en'),(628,'#/Employees','ChooseFile','Choose File',1,'en'),(629,'#/Employees','Import','Import',1,'en'),(630,'#/Employees','Employees','الموظفين',1,'ar'),(631,'#/Employees','Setup','الاعدادات',1,'ar'),(632,'#/Employees','Search','بحث',1,'ar'),(633,'#/Employees','EmployeeNo','رقم الموظف',1,'ar'),(634,'#/Employees','Name','الاسم',1,'ar'),(635,'#/Employees','Unit','القسم',1,'ar'),(636,'#/Employees','Position','الموقع الوظيفي',1,'ar'),(637,'#/Employees','Status','الحالة',1,'ar'),(638,'#/Employees','All','الكل',1,'ar'),(639,'#/Employees','InActive','غير فعال',1,'ar'),(640,'#/Employees','Active','فعال',1,'ar'),(641,'#/Employees','EmployeeEntry','ادخال الموظف',1,'ar'),(642,'#/Employees','FirstName','الاسم الاول',1,'ar'),(643,'#/Employees','SecondName','الاسم الثاني',1,'ar'),(644,'#/Employees','ThirdName','الاسم الثالث',1,'ar'),(645,'#/Employees','FamilyName','اسم العائلة',1,'ar'),(646,'#/Employees','Address','العنوان',1,'ar'),(647,'#/Employees','Scale','الدرجة',1,'ar'),(648,'#/Employees','Phone1','الهاتف 1',1,'ar'),(649,'#/Employees','Manager','المدير المباشر',1,'ar'),(650,'#/Employees','Phone2','الهاتف 2',1,'ar'),(651,'#/Employees','Save','حفظ',1,'ar'),(652,'#/Employees','ImportEmployees','ادخال الموظفيين',1,'ar'),(653,'#/Employees','ChooseFile','اختيار الملف',1,'ar'),(654,'#/Employees','Import','ادخال',1,'ar'),(655,'#/positions','CompetenceLevel','Competency Level',1,'en'),(656,'#/positions','CompetenceLevel','درجة الكفاءة',1,'ar'),(657,'#/Employees','Employees','Employees',1,'en'),(658,'#/Employees','Setup','Setup',1,'en'),(659,'#/Employees','Search','Search',1,'en'),(660,'#/Employees','EmployeeNo','Employee No',1,'en'),(662,'#/Employees','Unit','Unit',1,'en'),(669,'#/Employees','FirstName','First Name',1,'en'),(673,'#/Employees','Address','Address',1,'en'),(674,'#/employeeAssessment','EmployeesperfomanceAssessment','Employees performance Assessment',1,'en'),(675,'#/employeeAssessment','OperationAssessment','Operation Assessment',1,'en'),(676,'#/employeeAssessment','Search','Search',1,'en'),(677,'#/employeeAssessment','Year','Year',1,'en'),(678,'#/employeeAssessment','Employee','Employee',1,'en'),(679,'#/employeeAssessment','AssesmentDetails','Assesment Details',1,'en'),(680,'#/employeeAssessment','KPICycle','KPI Cycle',1,'en'),(681,'#/employeeAssessment','Reviewer','Reviewer',1,'en'),(682,'#/employeeAssessment','AgreementDate','Agreement Date',1,'en'),(683,'#/employeeAssessment','Result','Result',1,'en'),(684,'#/employeeAssessment','Target','Target',1,'en'),(685,'#/employeeAssessment','Competencies','Competencies',1,'en'),(686,'#/employeeAssessment','ObjectivesAssessment','Objectives Assessment',1,'en'),(687,'#/employeeAssessment','Objective','Objective',1,'en'),(688,'#/employeeAssessment','Q1','Q1',1,'en'),(689,'#/employeeAssessment','Q2','Q2',1,'en'),(690,'#/employeeAssessment','Q3','Q3',1,'en'),(691,'#/employeeAssessment','Q4','Q4',1,'en'),(692,'#/employeeAssessment','KPI','KPI',1,'en'),(693,'#/employeeAssessment','Save','Save',1,'en'),(694,'#/employeeAssessment','CompetenciesAssessment','Competencies Assessment',1,'en'),(695,'#/employeeAssessment','Competency','Competency',1,'en'),(696,'#/employeeAssessment','EmployeesperfomanceAssessment','تقييم كفاءة الموظفيين',1,'ar'),(697,'#/employeeAssessment','OperationAssessment','التشغيل والتقييم',1,'ar'),(698,'#/employeeAssessment','Search','بحث',1,'ar'),(699,'#/employeeAssessment','Year','السنة',1,'ar'),(700,'#/employeeAssessment','Employee','الموظف',1,'ar'),(701,'#/employeeAssessment','AssesmentDetails','تفاصيل التقييم',1,'ar'),(702,'#/employeeAssessment','KPICycle','تردد المعايير',1,'ar'),(703,'#/employeeAssessment','Reviewer','المدير المباشر',1,'ar'),(704,'#/employeeAssessment','AgreementDate','تاريخ الاتفاقية',1,'ar'),(705,'#/employeeAssessment','Result','النتيجة',1,'ar'),(706,'#/employeeAssessment','Target','الهدف',1,'ar'),(707,'#/employeeAssessment','Competencies','الكفاءات',1,'ar'),(708,'#/employeeAssessment','ObjectivesAssessment','تقييم الاهداف',1,'ar'),(709,'#/employeeAssessment','Objective','الهدف',1,'ar'),(710,'#/employeeAssessment','Q1','الربع 1',1,'ar'),(711,'#/employeeAssessment','Q2','الربع 2',1,'ar'),(712,'#/employeeAssessment','Q3','الربع 3',1,'ar'),(713,'#/employeeAssessment','Q4','الربع 4',1,'ar'),(714,'#/employeeAssessment','KPI','المعيار',1,'ar'),(715,'#/employeeAssessment','Save','حفظ',1,'ar'),(716,'#/employeeAssessment','CompetenciesAssessment','تقييم الكفاءات',1,'ar'),(717,'#/employeeAssessment','Competency','الكفاءة',1,'ar'),(718,'#/Units','HeaderUnits','Units',1,'en'),(719,'#/Units','HeaderSetup','Setup',1,'en'),(720,'#/Units','Search','Search',1,'en'),(721,'#/Units','Units','Units',1,'en'),(722,'#/Units','UnitsList','Units List',1,'en'),(723,'#/Units','UnitEntry','Unit Entry',1,'en'),(724,'#/Units','Name','Name',1,'en'),(725,'#/Units','Fax','Fax',1,'en'),(726,'#/Units','Address','Address',1,'en'),(727,'#/Units','PHONE1','Phone1',1,'en'),(728,'#/Units','PHONE2','Phone2',1,'en'),(729,'#/Units','Type','Type',1,'en'),(730,'#/Units','Save','Save',1,'en'),(731,'#/Units','HeaderUnits','الاقسام',1,'ar'),(732,'#/Units','HeaderSetup','الاعدادات',1,'ar'),(733,'#/Units','Search','بحث',1,'ar'),(734,'#/Units','Units','الاقسام',1,'ar'),(735,'#/Units','UnitsList','قائمة الاقسام',1,'ar'),(736,'#/Units','UnitEntry','ادخال قسم',1,'ar'),(737,'#/Units','Name','الاسم',1,'ar'),(738,'#/Units','Address','العنوان',1,'ar'),(739,'#/Units','Fax','فاكس',1,'ar'),(740,'#/Units','PHONE1','الهاتف 1',1,'ar'),(741,'#/Units','PHONE2','الهاتف 2',1,'ar'),(742,'#/Units','Type','النوع',1,'ar'),(743,'#/Units','Save','حفظ',1,'ar'),(744,'#/Units','UnitDeleted','Unit Deleted',1,'en'),(745,'#/Units','UnitDeleted','تم حذف القسم',1,'ar'),(746,'#/Units','Areyousure','Are you sure!',1,'en'),(747,'#/Units','Areyousure','هل انت متاكد !',1,'ar'),(748,'#/ProjectPlanningChart','lblStratigicObjectives','Strategic Objectives',1,'en'),(749,'#/ProjectPlanningChart','lblStratigicObjectives','الأهداف الاستراتيجية',1,'ar'),(750,'#/ProjectPlanningChart','lblOrganization','Organization',1,'en'),(751,'#/ProjectPlanningChart','lblOrganization','المؤسسة',1,'ar'),(752,'#/ProjectPlanningChart','lblSearch','Search',1,'en'),(753,'#/ProjectPlanningChart','lblYear','Year',1,'en'),(754,'#/ProjectPlanningChart','lblPleaseSelect','Please Select',1,'en'),(755,'#/ProjectPlanningChart','lblPleaseSelect','الرجاء الاختيار',1,'ar'),(756,'#/ProjectPlanningChart','lblDetails','Details',1,'en'),(757,'#/ProjectPlanningChart','lblDetails','التفاصيل',1,'ar'),(758,'#/ProjectPlanningChart','lblObjectiveName','Objective',1,'en'),(759,'#/ProjectPlanningChart','lblObjectiveName','الهدف',1,'ar'),(760,'#/ProjectPlanningChart','lblObjectivesWeight','Weight',1,'en'),(761,'#/ProjectPlanningChart','lblObjectivesWeight','الوزن',1,'ar'),(762,'#/ProjectPlanningChart','lblEdit','Edit',1,'en'),(763,'#/ProjectPlanningChart','lblEdit','تعديل',1,'ar'),(764,'#/ProjectPlanningChart','lblDelete','Delete',1,'en'),(765,'#/ProjectPlanningChart','lblDelete','حذف',1,'ar'),(766,'#/ProjectPlanningChart','SuccessOperationMsg','Operation done successfully',1,'en'),(767,'#/ProjectPlanningChart','SuccessOperationMsg','تمت العملية بنجاح',1,'ar'),(768,'#/ProjectPlanningChart','lblSave','Save',1,'en'),(769,'#/ProjectPlanningChart','lblSave','حفظ',1,'ar'),(770,'#/ProjectPlanningChart','lblQ4Target','المستهدف للربع 4',1,'ar'),(771,'#/ProjectPlanningChart','lblQ4Target','Q4 Target',1,'en'),(772,'#/ProjectPlanningChart','lblQ3Target','المستهدف للربع 3',1,'ar'),(773,'#/ProjectPlanningChart','lblQ3Target','Q3 Target',1,'en'),(774,'#/ProjectPlanningChart','lblQ2Target','المستهدف للربع 2',1,'ar'),(775,'#/ProjectPlanningChart','lblQ2Target','Q2 Target',1,'en'),(776,'#/ProjectPlanningChart','lblQ1Target','المستهدف للربع 1',1,'ar'),(777,'#/ProjectPlanningChart','lblQ1Target','Q1 Target',1,'en'),(778,'#/ProjectPlanningChart','lblDescription','الوصف',1,'ar'),(779,'#/ProjectPlanningChart','lblDescription','Description',1,'en'),(780,'#/ProjectPlanningChart','lblResultUnit','وحدة النتيجة',1,'ar'),(781,'#/ProjectPlanningChart','lblResultUnit','Result Unit',1,'en'),(782,'#/ProjectPlanningChart','lblKPIType','طريقة الاحتساب',1,'ar'),(783,'#/ProjectPlanningChart','lblKPIType','KPI Type',1,'en'),(784,'#/ProjectPlanningChart','lblKPICycle','Review Cycle',1,'en'),(785,'#/ProjectPlanningChart','lblKPICycle','دورية المراجعة',1,'ar'),(786,'#/ProjectPlanningChart','lblTarget','المستهدف',1,'ar'),(787,'#/ProjectPlanningChart','lblKPIs','المؤشرات',1,'ar'),(788,'#/ProjectPlanningChart','lblTarget','Target',1,'en'),(789,'#/ProjectPlanningChart','lblKPIs','KPIs',1,'en'),(790,'#/ProjectPlanningChart','lblProjectName','المشروع',1,'ar'),(791,'#/ProjectPlanningChart','lblProjectName','Project',1,'en'),(792,'#/ProjectPlanningChart','lblProject','المشروع',1,'ar'),(793,'#/ProjectPlanningChart','lblProject','Project',1,'en'),(794,'#/ProjectPlanningChart','lblQ4T','المستهدف ربع 3',1,'ar'),(795,'#/ProjectPlanningChart','lblQ4T','Q3 T',1,'en'),(796,'#/ProjectPlanningChart','lblQ3T','المستهدف ربع 3',1,'ar'),(797,'#/ProjectPlanningChart','lblQ3T','Q3 T',1,'en'),(798,'#/ProjectPlanningChart','lblQ2T','Q2 T',1,'en'),(799,'#/ProjectPlanningChart','lblQ1T','المستهدف ربع 1',1,'ar'),(800,'#/ProjectPlanningChart','lblQ1T','Q1 T',1,'en'),(801,'#/ProjectPlanningChart','lblWeight','الوزن',1,'ar'),(802,'#/ProjectPlanningChart','lblQ2T','المستهدف ربع 2',1,'ar'),(803,'#/ProjectPlanningChart','lblWeight','Weight',1,'en'),(804,'#/ProjectPlanningChart','lblProjectDetails','تفاصيل المشروع',1,'ar'),(805,'#/ProjectPlanningChart','lblProjectDetails','Project Details',1,'en'),(806,'#/ProjectPlanningChart','lblUnit','القسم',1,'ar'),(807,'#/ProjectPlanningChart','lblUnit','Unit',1,'en'),(808,'#/ProjectPlanningChart','lblAll','--- الكل ---',1,'ar'),(809,'#/ProjectPlanningChart','lblAll','--- All ---',1,'en'),(810,'#/ProjectPlanningChart','lblStratigicObjective','الهدف الاستراتيجي',1,'ar'),(811,'#/ProjectPlanningChart','lblStratigicObjective','Strategic Objective',1,'en'),(812,'#/ProjectPlanningChart','lblYear','السنة',1,'ar'),(813,'#/ProjectPlanningChart','lblSearch','بحث',1,'ar'),(814,'#/ProjectPlanningChart','lblProjects','المشاريع',1,'ar'),(815,'#/ProjectPlanningChart','lblProjects','Projects',1,'en'),(816,'#/ProjectPlanningChart','lblSettings','الاعدادات',1,'ar'),(817,'#/ProjectPlanningChart','lblSettings','Settings',1,'en'),(818,'#/ProjectPlanningChart','lblOrganizationProjects','مشاريع المؤسسة',1,'ar'),(819,'#/ProjectPlanningChart','lblOrganizationProjects','Organization Projects',1,'en'),(820,'#/ProjectPlanningChart','lblHeaderProjectsAssessment','Projects Assessment',1,'en'),(821,'#/ProjectPlanningChart','lblHeaderOrganizationObjectives','Organization Objectives',1,'en'),(822,'#/ProjectPlanningChart','lblSearchYear','Year',1,'en'),(823,'#/ProjectPlanningChart','All','All',1,'en'),(824,'#/ProjectPlanningChart','lblSearchStratigicObjective','Strategic Objective',1,'en'),(825,'#/ProjectPlanningChart','lblSearchUnit','Unit',1,'en'),(826,'#/ProjectPlanningChart','btnSearch','Search',1,'en'),(827,'#/ProjectPlanningChart','lblProjectsAssessmentList','Projects Assessment List',1,'en'),(828,'#/ProjectPlanningChart','thUnit','Unit',1,'en'),(829,'#/ProjectPlanningChart','thWeight','Weight',1,'en'),(830,'#/ProjectPlanningChart','thKPI','KPI',1,'en'),(831,'#/ProjectPlanningChart','thQ1','Q1',1,'en'),(832,'#/ProjectPlanningChart','thQ2','Q2',1,'en'),(833,'#/ProjectPlanningChart','thQ3','Q3',1,'en'),(834,'#/ProjectPlanningChart','thQ4','Q4',1,'en'),(835,'#/ProjectPlanningChart','thResult','Result',1,'en'),(836,'#/ProjectPlanningChart','thWObj','WObj',1,'en'),(837,'#/ProjectPlanningChart','thPerce','%',1,'en'),(838,'#/ProjectPlanningChart','thPrjPerce','Prj %',1,'en'),(839,'#/ProjectPlanningChart','thPercObj','Obj %',1,'en'),(840,'#/ProjectPlanningChart','lblHeaderProjectsAssessment','تقييم المشاريع',1,'ar'),(841,'#/ProjectPlanningChart','lblHeaderOrganizationObjectives','اهداف المنظمة',1,'ar'),(842,'#/ProjectPlanningChart','lblSearchYear','السنة',1,'ar'),(843,'#/ProjectPlanningChart','All','الكل',1,'ar'),(844,'#/ProjectPlanningChart','lblSearchStratigicObjective','الاهداف الاستراتيجية',1,'ar'),(845,'#/ProjectPlanningChart','lblSearchUnit','الوحدة',1,'ar'),(846,'#/ProjectPlanningChart','btnSearch','بحث',1,'ar'),(847,'#/ProjectPlanningChart','lblProjectsAssessmentList','قائمة تقييم المشاريع',1,'ar'),(848,'#/ProjectPlanningChart','thUnit','الوحدة',1,'ar'),(849,'#/ProjectPlanningChart','thWeight','الوزن',1,'ar'),(850,'#/ProjectPlanningChart','thKPI','KPI',1,'ar'),(851,'#/ProjectPlanningChart','thQ1','Q1',1,'ar'),(852,'#/ProjectPlanningChart','thQ2','Q2',1,'ar'),(853,'#/ProjectPlanningChart','thQ3','Q3',1,'ar'),(854,'#/ProjectPlanningChart','thQ4','Q4',1,'ar'),(855,'#/ProjectPlanningChart','thResult','النتيجة',1,'ar'),(856,'#/ProjectPlanningChart','thWObj','WObj',1,'ar'),(857,'#/ProjectPlanningChart','thPerce','%',1,'ar'),(858,'#/ProjectPlanningChart','thPrjPerce','Prj %',1,'ar'),(859,'#/ProjectPlanningChart','thPercObj','Obj %',1,'ar'),(860,'#/ProjectPlanningChart','thProject','Project',1,'en'),(861,'#/ProjectPlanningChart','thProject','المشروع',1,'ar'),(862,'#/ProjectPlanningChart','thActualCost','Actual Cost',1,'en'),(863,'#/ProjectPlanningChart','thActualCost','التكلفة الفعلية',1,'ar'),(864,'#/ProjectPlanningChart','thPlannedCost','Planned Cost',1,'en'),(865,'#/ProjectPlanningChart','thPlannedCost','التكلفة المخططة',1,'ar'),(866,'#/ProjectPlanningChart','thEvidencesFiles','Evidences Files',1,'en'),(867,'#/ProjectPlanningChart','thEvidencesFiles','الملفات',1,'ar'),(868,'#/ProjectPlanningChart','thTarget','Annual Target',1,'en'),(869,'#/ProjectPlanningChart','thTarget','المستهدف',1,'ar'),(870,'#/ProjectPlanningChart','lblUploadFile','Upload file...',1,'en'),(871,'#/ProjectPlanningChart','lblUploadFile','رفع ملف...',1,'ar'),(872,'#/ProjectPlanningChart','lblUpload','Upload',1,'en'),(873,'#/ProjectPlanningChart','lblUpload','رفع',1,'ar'),(874,'#/ProjectPlanningChart','lblCancel','Cancel',1,'en'),(875,'#/ProjectPlanningChart','lblCancel','الغاء',1,'ar'),(876,'#/ProjectPlanningChart','lblOneFileAtLeastMsg','Please select at least 1 files to upload!',1,'en'),(877,'#/ProjectPlanningChart','lblOneFileAtLeastMsg','الرجاء اختيار ملف واحد على الاقل لرفعه',1,'ar'),(878,'#/ProjectPlanningChart','lblThreeFileMaxMsg','Please select a maximum of 3 files to upload!',1,'en'),(879,'#/ProjectPlanningChart','lblThreeFileMaxMsg','الرجاء اختيار ثلاث ملفات على الاكثر لرفعهم',1,'ar'),(880,'#/ProjectPlanningChart','lblSavedSuccessfully','Saved Successfully.',1,'en'),(881,'#/ProjectPlanningChart','lblSavedSuccessfully','تم الحفظ بنجاح',1,'ar'),(882,'#/ProjectPlanningChart','lblChooseFile','Choose File',1,'en'),(883,'#/ProjectPlanningChart','lblChooseFile','اختر ملف',1,'ar'),(884,'#/ProjectPlanningChart','lblPlannedCost','التكلفة المخططة',1,'ar'),(885,'#/ProjectPlanningChart','lblPlannedCost','Planned Cost',1,'en'),(886,'#/Projects','lblIsProcess','Process/Project',1,'en'),(887,'#/Projects','optProject','Project/Initiative/Activity',1,'en'),(888,'#/Projects','optProcess','Process/Service',1,'en'),(889,'#/Projects','lblIsProcess','نوع المدخل',1,'ar'),(890,'#/Projects','optProject','مشروع',1,'ar'),(891,'#/Projects','optProcess','عملية',1,'ar'),(892,'#/ProjectPlanningChart','lblIsProcess','Is Process',1,'en'),(893,'#/ProjectPlanningChart','optProject','Project',1,'en'),(894,'#/ProjectPlanningChart','optProcess','Process/Service',1,'en'),(895,'#/ProjectPlanningChart','lblIsProcess','نوع المدخل',1,'ar'),(896,'#/ProjectPlanningChart','optProject','مشروع',1,'ar'),(897,'#/ProjectPlanningChart','optProcess','عملية',1,'ar'),(898,'#/stratigicobjectivesChart','lblIsProcess','Is Process',1,'en'),(899,'#/stratigicobjectivesChart','optProject','Project',1,'en'),(900,'#/stratigicobjectivesChart','optProcess','Process/Service',1,'en'),(901,'#/stratigicobjectivesChart','lblIsProcess','نوع المدخل',1,'ar'),(902,'#/stratigicobjectivesChart','optProject','مشروع',1,'ar'),(903,'#/stratigicobjectivesChart','optProcess','عملية',1,'ar'),(904,'#/stratigicobjectives','lblQ4Target','المستهدف للربع 4',1,'ar'),(905,'#/stratigicobjectives','lblQ4Target','Q4 Target',1,'en'),(906,'#/stratigicobjectives','lblQ3Target','المستهدف للربع 3',1,'ar'),(907,'#/stratigicobjectives','lblQ3Target','Q3 Target',1,'en'),(908,'#/stratigicobjectives','lblQ2Target','المستهدف للربع 2',1,'ar'),(909,'#/stratigicobjectives','lblQ2Target','Q2 Target',1,'en'),(910,'#/stratigicobjectives','lblQ1Target','المستهدف للربع 1',1,'ar'),(911,'#/stratigicobjectives','lblQ1Target','Q1 Target',1,'en'),(912,'#/stratigicobjectives','lblDescription','الوصف',1,'ar'),(913,'#/stratigicobjectives','lblDescription','Description',1,'en'),(914,'#/stratigicobjectives','lblResultUnit','وحدة النتيجة',1,'ar'),(915,'#/stratigicobjectives','lblResultUnit','Result Unit',1,'en'),(916,'#/stratigicobjectives','lblKPIType','طريقة الاحتساب',1,'ar'),(917,'#/stratigicobjectives','lblKPIType','KPI Type',1,'en'),(918,'#/stratigicobjectives','lblKPICycle','Review Cycle',1,'en'),(919,'#/stratigicobjectives','lblKPICycle','دورية المراجعة',1,'ar'),(920,'#/stratigicobjectives','lblTarget','المستهدف',1,'ar'),(921,'#/stratigicobjectives','lblKPIs','المؤشرات',1,'ar'),(922,'#/stratigicobjectives','lblTarget','Target',1,'en'),(923,'#/stratigicobjectives','lblKPIs','KPIs',1,'en'),(924,'#/stratigicobjectives','lblPleaseSelect','الرجاء الاختيار',1,'ar'),(925,'#/stratigicobjectives','lblPleaseSelect','lblPleaseSelect',1,'en'),(926,'#/stratigicobjectives','lblProjectName','المشروع',1,'ar'),(927,'#/stratigicobjectives','lblProjectName','Project',1,'en'),(928,'#/stratigicobjectives','lblProject','المشروع',1,'ar'),(929,'#/stratigicobjectives','lblProject','Project',1,'en'),(930,'#/stratigicobjectives','lblQ4T','المستهدف ربع 4',1,'ar'),(931,'#/stratigicobjectives','lblQ4T','Q4 T',1,'en'),(932,'#/stratigicobjectives','lblQ3T','المستهدف ربع 3',1,'ar'),(933,'#/stratigicobjectives','lblQ3T','Q3 T',1,'en'),(934,'#/stratigicobjectives','lblQ2T','المستهدف ربع 2',1,'ar'),(935,'#/stratigicobjectives','lblQ2T','Q2 T',1,'en'),(936,'#/stratigicobjectives','lblQ1T','المستهدف ربع 1',1,'ar'),(937,'#/stratigicobjectives','lblQ1T','Q1 T',1,'en'),(938,'#/stratigicobjectives','lblWeight','الوزن %',1,'ar'),(939,'#/stratigicobjectives','lblWeight','Weight %',1,'en'),(940,'#/stratigicobjectives','lblProjectDetails','تفاصيل المشروع',1,'ar'),(941,'#/stratigicobjectives','lblProjectDetails','Project Details',1,'en'),(942,'#/stratigicobjectives','lblUnit','القسم',1,'ar'),(943,'#/stratigicobjectives','lblUnit','Unit',1,'en'),(944,'#/stratigicobjectives','lblAll','--- الكل ---',1,'ar'),(945,'#/stratigicobjectives','lblAll','--- All ---',1,'en'),(946,'#/stratigicobjectives','lblStratigicObjective','الهدف الاستراتيجي',1,'ar'),(947,'#/stratigicobjectives','lblStratigicObjective','Strategic Objective',1,'en'),(948,'#/stratigicobjectives','lblYear','السنة',1,'ar'),(949,'#/stratigicobjectives','lblYear','Year',1,'en'),(950,'#/stratigicobjectives','lblSearch','بحث',1,'ar'),(951,'#/stratigicobjectives','lblSearch','Search',1,'en'),(952,'#/stratigicobjectives','lblProjects','المشاريع',1,'ar'),(953,'#/stratigicobjectives','lblProjects','Projects',1,'en'),(954,'#/stratigicobjectives','lblSettings','الاعدادات',1,'ar'),(955,'#/stratigicobjectives','lblSettings','Settings',1,'en'),(956,'#/stratigicobjectives','lblOrganizationProjects','مشاريع المؤسسة',1,'ar'),(957,'#/stratigicobjectives','lblOrganizationProjects','Organization Projects',1,'en'),(958,'#/stratigicobjectives','lblPlannedCost','التكلفة المخططة',1,'ar'),(959,'#/stratigicobjectives','lblPlannedCost','Planned Cost',1,'en'),(960,'#/stratigicobjectives','lblIsProcess','Is Process',1,'en'),(961,'#/stratigicobjectives','optProject','Project',1,'en'),(962,'#/stratigicobjectives','optProcess','Process/Service',1,'en'),(963,'#/stratigicobjectives','lblIsProcess','نوع المدخل',1,'ar'),(964,'#/stratigicobjectives','optProject','مشروع',1,'ar'),(965,'#/stratigicobjectives','optProcess','عملية',1,'ar'),(966,'#/stratigicobjectives','lblQ4Target','المستهدف للربع 4',1,'ar'),(967,'#/stratigicobjectives','lblQ4Target','Q4 Target',1,'en'),(968,'#/stratigicobjectives','lblQ3Target','المستهدف للربع 3',1,'ar'),(969,'#/stratigicobjectives','lblQ3Target','Q3 Target',1,'en'),(970,'#/stratigicobjectives','lblQ2Target','المستهدف للربع 2',1,'ar'),(971,'#/stratigicobjectives','lblQ2Target','Q2 Target',1,'en'),(972,'#/stratigicobjectives','lblQ1Target','المستهدف للربع 1',1,'ar'),(973,'#/stratigicobjectives','lblQ1Target','Q1 Target',1,'en'),(974,'#/stratigicobjectives','lblDescription','الوصف',1,'ar'),(975,'#/stratigicobjectives','lblDescription','Description',1,'en'),(976,'#/stratigicobjectives','lblResultUnit','وحدة النتيجة',1,'ar'),(977,'#/stratigicobjectives','lblResultUnit','Result Unit',1,'en'),(978,'#/stratigicobjectives','lblKPIType','طريقة الاحتساب',1,'ar'),(979,'#/stratigicobjectives','lblKPIType','KPI Type',1,'en'),(980,'#/stratigicobjectives','lblKPICycle','Review Cycle',1,'en'),(981,'#/stratigicobjectives','lblKPICycle','دورية المراجعة',1,'ar'),(982,'#/stratigicobjectives','lblTarget','المستهدف',1,'ar'),(983,'#/stratigicobjectives','lblKPIs','المؤشرات',1,'ar'),(984,'#/stratigicobjectives','lblTarget','Target',1,'en'),(985,'#/stratigicobjectives','lblKPIs','KPIs',1,'en'),(986,'#/stratigicobjectives','lblPleaseSelect','الرجاء الاختيار',1,'ar'),(987,'#/stratigicobjectives','lblPleaseSelect','lblPleaseSelect',1,'en'),(988,'#/stratigicobjectives','lblProjectName','المشروع',1,'ar'),(989,'#/stratigicobjectives','lblProjectName','Project',1,'en'),(990,'#/stratigicobjectives','lblProject','المشروع',1,'ar'),(991,'#/stratigicobjectives','lblProject','Project',1,'en'),(992,'#/stratigicobjectives','lblQ4T','المستهدف ربع 4',1,'ar'),(993,'#/stratigicobjectives','lblQ4T','Q4 T',1,'en'),(994,'#/stratigicobjectives','lblQ3T','المستهدف ربع 3',1,'ar'),(995,'#/stratigicobjectives','lblQ3T','Q3 T',1,'en'),(996,'#/stratigicobjectives','lblQ2T','المستهدف ربع 2',1,'ar'),(997,'#/stratigicobjectives','lblQ2T','Q2 T',1,'en'),(998,'#/stratigicobjectives','lblQ1T','المستهدف ربع 1',1,'ar'),(999,'#/stratigicobjectives','lblQ1T','Q1 T',1,'en'),(1000,'#/stratigicobjectives','lblWeight','الوزن %',1,'ar'),(1001,'#/stratigicobjectives','lblWeight','Weight %',1,'en'),(1002,'#/stratigicobjectives','lblProjectDetails','تفاصيل المشروع',1,'ar'),(1003,'#/stratigicobjectives','lblProjectDetails','Project Details',1,'en'),(1004,'#/stratigicobjectives','lblUnit','القسم',1,'ar'),(1005,'#/stratigicobjectives','lblUnit','Unit',1,'en'),(1006,'#/stratigicobjectives','lblAll','--- الكل ---',1,'ar'),(1007,'#/stratigicobjectives','lblAll','--- All ---',1,'en'),(1008,'#/stratigicobjectives','lblStratigicObjective','الهدف الاستراتيجي',1,'ar'),(1009,'#/stratigicobjectives','lblStratigicObjective','Strategic Objective',1,'en'),(1010,'#/stratigicobjectives','lblYear','السنة',1,'ar'),(1011,'#/stratigicobjectives','lblYear','Year',1,'en'),(1012,'#/stratigicobjectives','lblSearch','بحث',1,'ar'),(1013,'#/stratigicobjectives','lblSearch','Search',1,'en'),(1014,'#/stratigicobjectives','lblProjects','المشاريع',1,'ar'),(1015,'#/stratigicobjectives','lblProjects','Projects',1,'en'),(1016,'#/stratigicobjectives','lblSettings','الاعدادات',1,'ar'),(1017,'#/stratigicobjectives','lblSettings','Settings',1,'en'),(1018,'#/stratigicobjectives','lblOrganizationProjects','مشاريع المؤسسة',1,'ar'),(1019,'#/stratigicobjectives','lblOrganizationProjects','Organization Projects',1,'en'),(1020,'#/stratigicobjectives','lblPlannedCost','التكلفة المخططة',1,'ar'),(1021,'#/stratigicobjectives','lblPlannedCost','Planned Cost',1,'en'),(1022,'#/stratigicobjectives','lblIsProcess','Is Process',1,'en'),(1023,'#/stratigicobjectives','optProject','Project',1,'en'),(1024,'#/stratigicobjectives','optProcess','Process/Service',1,'en'),(1025,'#/stratigicobjectives','lblIsProcess','نوع المدخل',1,'ar'),(1026,'#/stratigicobjectives','optProject','مشروع',1,'ar'),(1027,'#/stratigicobjectives','optProcess','عملية',1,'ar'),(1028,'#/stratigicobjectives','lblKpiName','Measurment',1,'en'),(1029,'#/stratigicobjectives','lblKpiName','الاسم',1,'ar'),(1030,'#/stratigicobjectives','lblKpiDesc','Description',1,'en'),(1031,'#/stratigicobjectives','lblKpiDesc','الوصف',1,'ar'),(1032,'#/stratigicobjectives','lblKpiWeight','Weight %',1,'en'),(1033,'#/stratigicobjectives','lblKpiWeight','الوزن %',1,'ar'),(1034,'#/stratigicobjectives','lblKpiTarget','Target',1,'en'),(1035,'#/stratigicobjectives','lblKpiTarget','الهدف',1,'ar'),(1036,'#/stratigicobjectives','lblBSC','BSC',1,'en'),(1037,'#/stratigicobjectives','lblBSC','نتيجة التوازن',1,'ar'),(1038,'#/stratigicobjectives','lblKpiMeasure','Target Type',1,'en'),(1039,'#/stratigicobjectives','lblKpiMeasure','القياس',1,'ar'),(1040,'#/stratigicobjectives','lblKpiBSCFinancialOption','Financial',1,'en'),(1041,'#/stratigicobjectives','lblKpiBSCFinancialOption','المالية',1,'ar'),(1042,'#/stratigicobjectives','lblKpiBSCCustomersOption','Customers',1,'en'),(1043,'#/stratigicobjectives','lblKpiBSCCustomersOption','العملاء',1,'ar'),(1044,'#/stratigicobjectives','lblKpiBSCInternalProcessOption','Internal Process',1,'en'),(1045,'#/stratigicobjectives','lblKpiBSCInternalProcessOption','عمليات داخلية',1,'ar'),(1046,'#/stratigicobjectives','lblKpiBSCLearninggrowthOption','Learning growth',1,'en'),(1047,'#/stratigicobjectives','lblKpiBSCLearninggrowthOption','النمو التعليمي',1,'ar'),(1048,'#/stratigicobjectives','lblKpiMeasurePercentageOption','Percentage',1,'en'),(1049,'#/stratigicobjectives','lblKpiMeasurePercentageOption','نسبة مئوية',1,'ar'),(1050,'#/stratigicobjectives','lblKpiMeasureValueOption','Value',1,'en'),(1051,'#/stratigicobjectives','lblKpiMeasureValueOption','قيمة',1,'ar'),(1052,'#/stratigicobjectives','btnKpiSave','Save',1,'en'),(1053,'#/stratigicobjectives','btnKpiSave','حفظ',1,'ar'),(1054,'#/stratigicobjectives','lblObjKpiEntry','Objective KPIs Entry',1,'en'),(1055,'#/stratigicobjectives','lblObjKpiEntry','معاير اداء الاهداف الاستراتيجية',1,'ar'),(1056,'#/stratigicobjectives','lblObjKpiList','Objective KPIs List',1,'en'),(1057,'#/stratigicobjectives','lblObjKpiList','قائمة معاير اداء الاهداف الاستراتيجية',1,'ar'),(1058,'#/stratigicobjectives','colKpiName','Name',1,'en'),(1059,'#/stratigicobjectives','colKpiName','الاسم',1,'ar'),(1060,'#/stratigicobjectives','colKpiTarget','Target',1,'en'),(1061,'#/stratigicobjectives','colKpiTarget','الهدف',1,'ar'),(1062,'#/stratigicobjectives','colKpiWeight','Weight %',1,'en'),(1063,'#/stratigicobjectives','colKpiWeight','الوزن %',1,'ar'),(1064,'#/stratigicobjectives','lblTabRelatedProjets','Related Projects',1,'en'),(1065,'#/stratigicobjectives','lblTabRelatedProjets','المشاريع ',1,'ar'),(1066,'#/stratigicobjectives','lblTabRelatedKpis','Related Objectives KPIs',1,'en'),(1067,'#/stratigicobjectives','lblTabRelatedKpis','معايير الاداء',1,'ar'),(1068,'#/ProjectPlanningChart','lblProjectPlannerHeader','Projects Planner',1,'en'),(1069,'#/ProjectPlanningChart','lblProjectPlannerHeader','مخطط المشاريع',1,'ar'),(1070,'#/ProjectPlanningChart','lblStratigicHeader','Strategic Planning',1,'en'),(1071,'#/ProjectPlanningChart','lblStratigicHeader','مخطط استراتيجي',1,'ar'),(1072,'#/ProjectPlanningChart','lblSearchHeader','Search',1,'en'),(1073,'#/ProjectPlanningChart','lblSearchHeader','بحث',1,'ar'),(1074,'#/ProjectPlanningChart','lblYear','Year',1,'en'),(1075,'#/ProjectPlanningChart','lblYear','السنة',1,'ar'),(1076,'#/ProjectPlanningChart','lblOrgObjStrucHeader','Organization Strategic Objectives Structure',1,'en'),(1077,'#/ProjectPlanningChart','lblOrgObjStrucHeader','الاهداف الستراتيجية للمؤسسة',1,'ar'),(1078,'#/ProjectPlanningChart','lblModalStrucObj','Strategic Objectives',1,'en'),(1079,'#/ProjectPlanningChart','lblModalStrucObj','الاهداف الاستراتيجية',1,'ar'),(1080,'#/ProjectPlanningChart','lblModalProjects','Projects',1,'en'),(1081,'#/ProjectPlanningChart','lblModalProjects','المشاريع',1,'ar'),(1082,'#/ProjectPlanningChart','lblModalProjAssessment','Project Assessment',1,'en'),(1083,'#/ProjectPlanningChart','lblModalProjAssessment','تقييم المشاريع',1,'ar'),(1084,'#/stratigicobjectivesChart','lblHeaderProjectNavigation','Project Navigation ',1,'en'),(1085,'#/stratigicobjectivesChart','lblHeaderProjectNavigation','التنقل بين المشاريع',1,'ar'),(1086,'#/stratigicobjectivesChart','lblHeaderOperationAssessment','Operation Assessment',1,'en'),(1087,'#/stratigicobjectivesChart','lblHeaderOperationAssessment','تقييم العمليات',1,'ar'),(1088,'#/stratigicobjectivesChart','lblSearchHead','Search',1,'en'),(1089,'#/stratigicobjectivesChart','lblSearchHead','بحث',1,'ar'),(1090,'#/stratigicobjectivesChart','lblYear','Year',1,'en'),(1091,'#/stratigicobjectivesChart','lblYear','السنة',1,'ar'),(1092,'#/stratigicobjectivesChart','lblMainPanelHeader','Organization Strategic Objectives Structure',1,'en'),(1093,'#/stratigicobjectivesChart','lblMainPanelHeader','الهيكل التنظيمي للاهداف الاستراتيجية',1,'ar'),(1094,'#/stratigicobjectivesChart','lblModalObjectiveHeader','Strategic Objectives',1,'en'),(1095,'#/stratigicobjectivesChart','lblModalObjectiveHeader','الاهداف الاستراتيجية',1,'ar'),(1096,'#/stratigicobjectivesChart','lblModalProjects','Projects',1,'en'),(1097,'#/stratigicobjectivesChart','lblModalProjects','المشاريع',1,'ar'),(1098,'#/stratigicobjectivesChart','lblModalProjectsAssessment','Project Assessment',1,'en'),(1099,'#/stratigicobjectivesChart','lblModalProjectsAssessment','تقييم المشاريع',1,'ar'),(1100,'#/stratigicobjectives','InvalidWeight','Invalid Weight',1,'en'),(1101,'#/stratigicobjectives','InvalidWeight','الوزن غير صحيح',1,'ar'),(1102,'#/stratigicobjectives','AreYouSure','Are You Sure',1,'en'),(1103,'#/stratigicobjectives','AreYouSure','هل انت متأكد؟',1,'ar'),(1104,'#/stratigicobjectives','SuccessMsg','Operation Done successfully.',1,'en'),(1105,'#/stratigicobjectives','SuccessMsg','العملية تمت بنجاح',1,'ar'),(1106,'#/competence','NegativeIndicator','Negative Indicator',1,'en'),(1107,'#/competence','NegativeIndicator','مؤشر سلبي',1,'ar'),(1108,'#/Landing','Home','Home',1,'en'),(1109,'#/Landing','Landing','Landing',1,'en'),(1110,'#/Landing','OurVision','Our Vision',1,'en'),(1111,'#/Landing','OurMission','Our Mission',1,'en'),(1112,'#/Landing','Home','الصفحة الرئيسية',1,'ar'),(1113,'#/Landing','Landing','Landing',1,'ar'),(1114,'#/Landing','OurVision','رؤية الشركة',1,'ar'),(1115,'#/Landing','OurMission','مهمة الشركة',1,'ar'),(1116,'#/Company','HeaderOrganization','Organization',1,'en'),(1117,'#/Company','HeaderSetup','Setup',1,'en'),(1118,'#/Company','OrganizationInfo','Organization Info',1,'en'),(1119,'#/Company','Name','Name',1,'en'),(1120,'#/Company','Address','Address',1,'en'),(1121,'#/Company','Email','Email',1,'en'),(1122,'#/Company','WebSite','WebSite',1,'en'),(1123,'#/Company','Fax','Fax',1,'en'),(1124,'#/Company','PHONE1','Phone1',1,'en'),(1125,'#/Company','PHONE2','Phone2',1,'en'),(1126,'#/Company','Currency','Currency',1,'en'),(1127,'#/Company','Mission','Mission',1,'en'),(1128,'#/Company','Vision','Vision',1,'en'),(1129,'#/Company','Save','Save',1,'en'),(1130,'#/Company','SetPlanEmployee','SetPlanEmployee',1,'en'),(1131,'#/Company','SetPlan','SetPlan',1,'en'),(1132,'#/Company','ProjectLinkKPI','ProjectLinkKPI',1,'en'),(1133,'#/Company','ProjectLinkPlan','ProjectLinkPlan',1,'en'),(1134,'#/Company','HeaderOrganization','المؤسسة',1,'ar'),(1135,'#/Company','HeaderSetup','الاعدادات',1,'ar'),(1136,'#/Company','OrganizationInfo','معلومات المؤسسة',1,'ar'),(1137,'#/Company','Name','الاسم',1,'ar'),(1138,'#/Company','Address','العنوان',1,'ar'),(1139,'#/Company','Email','البريد الالكتروني',1,'ar'),(1140,'#/Company','WebSite','الموقع الالكترزني',1,'ar'),(1141,'#/Company','Fax','الفاكس',1,'ar'),(1142,'#/Company','PHONE1','الهاتف 1',1,'ar'),(1143,'#/Company','PHONE2','الهاتف 2',1,'ar'),(1144,'#/Company','Currency','العملة',1,'ar'),(1145,'#/Company','Mission','المهام',1,'ar'),(1146,'#/Company','Vision','الرؤية',1,'ar'),(1147,'#/Company','Save','حفظ',1,'ar'),(1148,'#/Company','SetPlanEmployee','SetPlanEmployee',1,'ar'),(1149,'#/Company','SetPlan','SetPlan',1,'ar'),(1150,'#/Company','ProjectLinkKPI','ProjectLinkKPI',1,'ar'),(1151,'#/Company','ProjectLinkPlan','ProjectLinkPlan',1,'ar'),(1152,'#/competence','NegativeIndicator','Negative Indicator',1,'en'),(1153,'#/competence','NegativeIndicator','مؤشر سلبي',1,'ar'),(1154,'#/Landing','Home','Home',1,'en'),(1155,'#/Landing','Landing','Landing',1,'en'),(1156,'#/Landing','OurVision','Our Vision',1,'en'),(1157,'#/Landing','OurMission','Our Mission',1,'en'),(1158,'#/Landing','Home','الصفحة الرئيسية',1,'ar'),(1159,'#/Landing','Landing','Landing',1,'ar'),(1160,'#/Landing','OurVision','رؤية الشركة',1,'ar'),(1161,'#/Landing','OurMission','مهمة الشركة',1,'ar'),(1162,'#/EmpDashBoards','lblTitle','Employees DashBoards',1,'en'),(1163,'#/EmpDashBoards','lblTitle','لوحة الموظفين',1,'ar'),(1164,'#/EmpDashBoards','lblSettings','Settings',1,'en'),(1165,'#/EmpDashBoards','lblSettings','الاعدادات',1,'ar'),(1166,'#/EmpDashBoards','lblDashBoards','DashBoards',1,'en'),(1167,'#/EmpDashBoards','lblDashBoards','لوحات',1,'ar'),(1168,'#/EmpDashBoards','lblSearch','Search',1,'en'),(1169,'#/EmpDashBoards','lblSearch','بحث',1,'ar'),(1170,'#/EmpDashBoards','lblYear','Year',1,'en'),(1171,'#/EmpDashBoards','lblYear','السنة',1,'ar'),(1172,'#/EmpDashBoards','lblChart1Title','Target for each employee and results',1,'en'),(1173,'#/EmpDashBoards','lblChart1Title','مقارنة المستهدف و النتيجة للموظفين',1,'ar'),(1174,'#/EmpDashBoards','lblChart2Title','Employees in department vs needed number',1,'en'),(1175,'#/EmpDashBoards','lblChart2Title','مقارنة موظفي الاقسام مع العدد المطلوب',1,'ar'),(1176,'#/EmpDashBoards','lblChart3Title','Employee Rank',1,'en'),(1177,'#/EmpDashBoards','lblChart3Title','ترتيب الموظفين',1,'ar'),(1178,'#/EmpDashBoards','lblDataNotFound','No Data Found',1,'en'),(1179,'#/EmpDashBoards','lblDataNotFound','البيانات غير موجودة',1,'ar'),(1180,'#/EmpDashBoards','lblResult/Target','Result/Target',1,'en'),(1181,'#/EmpDashBoards','lblResult/Target','النتيجة/المستهدف',1,'ar'),(1182,'#/EmpDashBoards','lblTarget','Target',1,'en'),(1183,'#/EmpDashBoards','lblTarget','المستهدف',1,'ar'),(1184,'#/EmpDashBoards','lblResult','Result',1,'en'),(1185,'#/EmpDashBoards','lblResult','النتيجة',1,'ar'),(1186,'#/EmpDashBoards','lblPercentage','Percentage',1,'en'),(1187,'#/EmpDashBoards','lblPercentage','Percentage',1,'ar'),(1188,'#/EmpDashBoards','lblNoOfEmployee','No. Of Employee',1,'en'),(1189,'#/EmpDashBoards','lblNoOfEmployee','عدد الموظفين',1,'ar'),(1190,'#/EmpDashBoards','lblNeeded','Needed',1,'en'),(1191,'#/EmpDashBoards','lblNeeded','المطلوب',1,'ar'),(1192,'#/EmpDashBoards','lblUnit','Unit',1,'en'),(1193,'#/EmpDashBoards','lblUnit','القسم',1,'ar'),(1194,'#/EmpDashBoards','lblTarget','Target',1,'en'),(1195,'#/EmpDashBoards','lblTarget','المستهدف',1,'ar'),(1196,'#/EmpDashBoards','lblResult','Result',1,'en'),(1197,'#/EmpDashBoards','lblResult','النتيجة',1,'ar'),(1198,'#/DashBoards','lblCost','Cost',1,'en'),(1199,'#/DashBoards','lblCost','التكلفة',1,'ar'),(1200,'#/DashBoards','lblActualCost','Actual Cost',1,'en'),(1201,'#/DashBoards','lblActualCost','التكلفة الفعلية',1,'ar'),(1202,'#/DashBoards','lblWeight','Weight',1,'en'),(1203,'#/DashBoards','lblWeight','الوزن',1,'ar'),(1204,'#/DashBoards','lblSterategicWeight','Sterategic Weight',1,'en'),(1205,'#/DashBoards','lblSterategicWeight','الوزن الاستراتيجي',1,'ar'),(1206,'#/DashBoards','lblResultWeightPercentage','Result Weight Percentage',1,'en'),(1207,'#/DashBoards','lblResultWeightPercentage','الوزن النسبي للنتيجة',1,'ar'),(1208,'#/DashBoards','lblSterategicResultWeight','Sterategic Result weight',1,'en'),(1209,'#/DashBoards','lblSterategicResultWeight','وزن النتيجة الاستراتيجي',1,'ar'),(1210,'#/DashBoards','lblUnitsResultsPercentage','Units Results Percentage',1,'en'),(1211,'#/DashBoards','lblUnitsResultsPercentage','الوزن النسبي للاقسام',1,'ar'),(1212,'#/DashBoards','lblUnits','Units',1,'en'),(1213,'#/DashBoards','lblUnits','الأقسام',1,'ar'),(1214,'#/DashBoards','lblDashBoards','Organization DashBoard',1,'en'),(1215,'#/DashBoards','lblDashBoards','لوحة المؤسسة',1,'ar'),(1216,'#/DashBoards','lblSearch','Search',1,'en'),(1217,'#/DashBoards','lblSearch','بحث',1,'ar'),(1218,'#/DashBoards','lblYear','Year',1,'en'),(1219,'#/DashBoards','lblYear','السنة',1,'ar'),(1220,'#/DashBoards','lblContainer2','Strategies percentage from over all organization',1,'en'),(1221,'#/DashBoards','lblContainer2','وزن الاهداف الاستراتيجية من مجمل الاهداف',1,'ar'),(1222,'#/DashBoards','lblContainer3','Units percentage from over all organization',1,'en'),(1223,'#/DashBoards','lblContainer3','وزن القسم من مجمل الاهداف الاستراتيجية',1,'ar'),(1224,'#/DashBoards','lblNoDataFound','No Data Found',1,'en'),(1225,'#/DashBoards','lblNoDataFound','لا توجد بيانات',1,'ar'),(1226,'#/DashBoards','lblContainer','Strategies and percentage of completion',1,'en'),(1227,'#/DashBoards','lblContainer','الاهداف الاستراتيجية ونسبة الانجاز',1,'ar'),(1228,'#/DashBoards','lblContainer5','Projects Actual Cost Statistic',1,'en'),(1229,'#/DashBoards','lblContainer5','احصائيات التكلفة الفعلية للمشاريع',1,'ar'),(1230,'#/DashBoards','lblContainer6','Projects Actual Cost verse Planned Statistic',1,'en'),(1231,'#/DashBoards','lblContainer6','احصائيات تكلفة المشاريع الخطط لها والفعلية',1,'ar'),(1232,'#/DashBoards','lblContainer4','Units Target vs Actual Results',1,'en'),(1233,'#/DashBoards','lblContainer4','نتائج الاقسام والمستهدف',1,'ar'),(1234,'#/performanceLevels','lblPerformanceLevels','Performance Levels',1,'en'),(1235,'#/performanceLevels','lblPerformanceLevels','مستويات الكفاءة',1,'ar'),(1236,'#/performanceLevels','lblSearch','Search',1,'en'),(1237,'#/performanceLevels','lblSearch','بحث',1,'ar'),(1238,'#/performanceLevels','lblYear','Year',1,'en'),(1239,'#/performanceLevels','lblYear','السنة',1,'ar'),(1240,'#/performanceLevels','lblPerformanceLevelsQuota','Performance Levels Quota',1,'en'),(1241,'#/performanceLevels','lblPerformanceLevelsQuota','حصص مستويات الكفاءة',1,'ar'),(1242,'#/performanceLevels','lblDetails','Details',1,'en'),(1243,'#/performanceLevels','lblDetails','التفاصيل',1,'ar'),(1244,'#/performanceLevels','lblLevelName','Level Name',1,'en'),(1245,'#/performanceLevels','lblLevelName','وصف المستوى',1,'ar'),(1246,'#/performanceLevels','lblLevelNumber','Level Number',1,'en'),(1247,'#/performanceLevels','lblLevelNumber','رقم المستوى',1,'ar'),(1248,'#/performanceLevels','lblLevelPercentage','Level Percentage',1,'en'),(1249,'#/performanceLevels','lblLevelPercentage','نسبة المستوى',1,'ar'),(1250,'#/performanceLevels','lblSave','Save',1,'en'),(1251,'#/performanceLevels','lblSave','حفظ',1,'ar'),(1252,'#/performanceLevels','lblAreYouSure','Are you sure?',1,'en'),(1253,'#/performanceLevels','lblAreYouSure','هل انت متأكد ؟',1,'ar'),(1254,'#/performanceLevels','PerformanceLevelDeleted','Performance Level Deleted',1,'en'),(1255,'#/performanceLevels','PerformanceLevelDeleted','تم حذف مستوى الكفائة بنجاح',1,'ar'),(1256,'#/performanceLevels','lblPercentage','Percentage %',1,'en'),(1257,'#/performanceLevels','lblPercentage','النسبة %',1,'ar'),(1258,'#/performanceLevels','lblProjectsResultsRanges','Projects Results Ranges',1,'en'),(1259,'#/performanceLevels','lblProjectsResultsRanges','نطاقات نتائج المشاريع',1,'ar'),(1260,'#/performanceLevels','lblActions','Actions',1,'en'),(1261,'#/performanceLevels','lblActions','الاجراءات',1,'ar'),(1262,'#/performanceLevels','lblDelete','Delete',1,'en'),(1263,'#/performanceLevels','lblDelete','حذف',1,'ar'),(1264,'#/performanceLevels','lblEdit','Edit',1,'en'),(1265,'#/performanceLevels','lblEdit','تعديل',1,'ar'),(1266,'#/performanceLevels','lblFromPercentage','From Percentage',1,'en'),(1267,'#/performanceLevels','lblFromPercentage','من نسبة',1,'ar'),(1268,'#/performanceLevels','lblToPercentage','To Percentage',1,'en'),(1269,'#/performanceLevels','lblToPercentage','الى نسبة',1,'ar'),(1270,'#/performanceLevels','lblLevel','Level',1,'en'),(1271,'#/performanceLevels','lblLevel','مستوى',1,'ar'),(1272,'#/performanceLevels','lblQuota','Quota',1,'en'),(1273,'#/performanceLevels','lblQuota','حصة',1,'ar'),(1274,'#/performanceLevels','lblRemaining','Remaining Employee',1,'en'),(1275,'#/performanceLevels','lblRemaining','باقي الموظفين',1,'ar'),(1276,'#/performanceLevels','lblِAllQuota','All Quota',1,'en'),(1277,'#/performanceLevels','lblِAllQuota','كامل الحصة',1,'ar'),(1278,'#/','lblCost','Cost',1,'en'),(1279,'#/','lblCost','التكلفة',1,'ar'),(1280,'#/','lblActualCost','Actual Cost',1,'en'),(1281,'#/','lblActualCost','التكلفة الفعلية',1,'ar'),(1282,'#/','lblWeight','Weight',1,'en'),(1283,'#/','lblWeight','الوزن',1,'ar'),(1284,'#/','lblSterategicWeight','Sterategic Weight',1,'en'),(1285,'#/','lblSterategicWeight','الوزن الاستراتيجي',1,'ar'),(1286,'#/','lblResultWeightPercentage','Result Weight Percentage',1,'en'),(1287,'#/','lblResultWeightPercentage','الوزن النسبي للنتيجة',1,'ar'),(1288,'#/','lblSterategicResultWeight','Sterategic Result weight',1,'en'),(1289,'#/','lblSterategicResultWeight','وزن النتيجة الاستراتيجي',1,'ar'),(1290,'#/','lblUnitsResultsPercentage','Units Results Percentage',1,'en'),(1291,'#/','lblUnitsResultsPercentage','الوزن النسبي للاقسام',1,'ar'),(1292,'#/','lblUnits','Units',1,'en'),(1293,'#/','lblUnits','الأقسام',1,'ar'),(1294,'#/','lblDashBoards','Organization DashBoard',1,'en'),(1295,'#/','lblDashBoards','لوحة المؤسسة',1,'ar'),(1296,'#/','lblSearch','Search',1,'en'),(1297,'#/','lblSearch','بحث',1,'ar'),(1298,'#/','lblYear','Year',1,'en'),(1299,'#/','lblYear','السنة',1,'ar'),(1300,'#/','lblContainer2','Strategies percentage from over all organization',1,'en'),(1301,'#/','lblContainer2','وزن الاهداف الاستراتيجية من مجمل الاهداف',1,'ar'),(1302,'#/','lblContainer3','Units percentage from over all organization',1,'en'),(1303,'#/','lblContainer3','وزن القسم من مجمل الاهداف الاستراتيجية',1,'ar'),(1304,'#/','lblNoDataFound','No Data Found',1,'en'),(1305,'#/','lblNoDataFound','لا توجد بيانات',1,'ar'),(1306,'#/','lblContainer','Strategies and percentage of completion',1,'en'),(1307,'#/','lblContainer','الاهداف الاستراتيجية ونسبة الانجاز',1,'ar'),(1308,'#/','lblContainer5','Projects Actual Cost Statistic',1,'en'),(1309,'#/','lblContainer5','احصائيات التكلفة الفعلية للمشاريع',1,'ar'),(1310,'#/','lblContainer6','Projects Actual Cost verse Planned Statistic',1,'en'),(1311,'#/','lblContainer6','احصائيات تكلفة المشاريع الخطط لها والفعلية',1,'ar'),(1312,'#/','lblContainer4','Units Target vs Actual Results',1,'en'),(1313,'#/','lblContainer4','نتائج الاقسام والمستهدف',1,'ar'),(1314,'global','lblLevel','Level',1,'en'),(1315,'global','lblNeeded','Needed',1,'en'),(1316,'global','lblNoOfEmployee','No of Employees',1,'en'),(1317,'global','lblPlanned','Planned (standard)',1,'en'),(1318,'global','lblProjectResult','Project Result',1,'en'),(1319,'global','lblProjectResultPercentage','Project Result Percentage',1,'en'),(1320,'global','lblRealEmployeeRank','Real Employee Rank',1,'en'),(1321,'global','lblResult','Result',1,'en'),(1322,'global','lblTarget','Target',1,'en'),(1323,'global','lblUnitWeight','Unit Weight',1,'en'),(1324,'global','PlannedQuota','Planned Quota',1,'en'),(1325,'global','RemainingEmployee','Remaining Employees',1,'en'),(1326,'global','lblLevel','درجة',1,'ar'),(1327,'global','lblLevel','درجة',1,'ar'),(1328,'global','lblNeeded','المطلوب',1,'ar'),(1329,'global','lblNoOfEmployee','عدد الموظفين',1,'ar'),(1330,'global','lblPlanned','المخطط له',1,'ar'),(1331,'global','lblProjectResult','انجاز القسم',1,'ar'),(1332,'global','lblProjectResultPercentage','نسبة انجاز القسم',1,'ar'),(1333,'global','lblRealEmployeeRank','ترتيب الموظفين الحقيقي',1,'ar'),(1334,'global','lblResult','النتيجة',1,'ar'),(1335,'global','lblTarget','المستهدف',1,'ar'),(1336,'global','lblUnitWeight','وزن القسم',1,'ar'),(1337,'global','PlannedQuota','الحصة المقررة',1,'ar'),(1338,'global','RemainingEmployee','باقي الموظفين',1,'ar'),(1339,'#/ProjectPlanningChart','lblAddObjective','Add Objective',1,'en'),(1340,'#/ProjectPlanningChart','lblAddObjective','اضافة هدف استراتيجي',1,'ar'),(1341,'#/ProjectPlanningChart','lblPlannedCost','Planned Cost',1,'en'),(1342,'#/ProjectPlanningChart','lblPlannedCost','التكلفة المخطط لها',1,'ar'),(1343,'#/ProjectPlanningChart','lblWeight','Weight',1,'en'),(1344,'#/ProjectPlanningChart','lblWeight','الوزن',1,'ar'),(1345,'#/ProjectPlanningChart','lblEditObjective','Edit Objective',1,'en'),(1346,'#/ProjectPlanningChart','lblEditObjective','تعديل الهدف',1,'ar'),(1347,'#/ProjectPlanningChart','lblAddProject','Add Project',1,'en'),(1348,'#/ProjectPlanningChart','lblAddProject','اضافة مشروع',1,'ar'),(1349,'#/ProjectPlanningChart','lblProjectsList','Projects List',1,'en'),(1350,'#/ProjectPlanningChart','lblProjectsList','قائمة المشاريع',1,'ar'),(1351,'#/ProjectPlanningChart','lblObjectiveKPIsAssessment','Objective KPIs Assessment',1,'en'),(1352,'#/ProjectPlanningChart','lblObjectiveKPIsAssessment','تقييم مؤشرات الهدف',1,'ar'),(1353,'#/ProjectPlanningChart','lblEditProject','Edit Project',1,'en'),(1354,'#/ProjectPlanningChart','lblEditProject','تعديل مشروع',1,'ar'),(1355,'#/Projects','btnCancel','الغاء',1,'ar'),(1356,'#/Projects','btnSave','حفظ',1,'ar'),(1357,'#/Projects','btnCancel','Cacel',1,'en'),(1358,'#/Projects','btnSave','Save',1,'en'),(1359,'#/Projects','projEvidList','Project Evidences Documents',1,'en'),(1360,'#/Projects','projEvidList','الوثائق المطلوبة للتقييم',1,'ar'),(1361,'#/Projects','projEvidEntry','Project Evidences Document Entry',1,'en'),(1362,'#/Projects','projEvidEntry','ادخال الوثائق المطلوبة للتقييم',1,'ar'),(1363,'#/Projects','docName','Document Name',1,'en'),(1364,'#/Projects','docName','اسم الوثيقة',1,'ar'),(1365,'#/Projects','fileName','File Name',1,'en'),(1366,'#/Projects','fileName','اسم المرفق',1,'ar'),(1367,'#/Projects','confirm','Are you sure ?',1,'en'),(1368,'#/Projects','confirm','هل انت متأكد ؟',1,'ar'),(1369,'#/stratigicobjectivesChart','btnCancel','الغاء',1,'ar'),(1370,'#/stratigicobjectivesChart','btnSave','حفظ',1,'ar'),(1371,'#/stratigicobjectivesChart','btnCancel','Cacel',1,'en'),(1372,'#/stratigicobjectivesChart','btnSave','Save',1,'en'),(1373,'#/stratigicobjectivesChart','projEvidList','Project Evidences Documents',1,'en'),(1374,'#/stratigicobjectivesChart','projEvidList','الوثائق المطلوبة للتقييم',1,'ar'),(1375,'#/stratigicobjectivesChart','projEvidEntry','Project Evidences Document Entry',1,'en'),(1376,'#/stratigicobjectivesChart','projEvidEntry','ادخال الوثائق المطلوبة للتقييم',1,'ar'),(1377,'#/stratigicobjectivesChart','docName','Document Name',1,'en'),(1378,'#/stratigicobjectivesChart','docName','اسم الوثيقة',1,'ar'),(1379,'#/stratigicobjectivesChart','fileName','File Name',1,'en'),(1380,'#/stratigicobjectivesChart','fileName','اسم المرفق',1,'ar'),(1381,'#/stratigicobjectivesChart','confirm','Are you sure ?',1,'en'),(1382,'#/stratigicobjectivesChart','confirm','هل انت متأكد ؟',1,'ar'),(1383,'#/ProjectPlanningChart','btnCancel','الغاء',1,'ar'),(1384,'#/ProjectPlanningChart','btnSave','حفظ',1,'ar'),(1385,'#/ProjectPlanningChart','btnCancel','Cacel',1,'en'),(1386,'#/ProjectPlanningChart','btnSave','Save',1,'en'),(1387,'#/ProjectPlanningChart','projEvidList','Project Evidences Documents',1,'en'),(1388,'#/ProjectPlanningChart','projEvidList','الوثائق المطلوبة للتقييم',1,'ar'),(1389,'#/ProjectPlanningChart','projEvidEntry','Project Evidences Document Entry',1,'en'),(1390,'#/ProjectPlanningChart','projEvidEntry','ادخال الوثائق المطلوبة للتقييم',1,'ar'),(1391,'#/ProjectPlanningChart','docName','Document Name',1,'en'),(1392,'#/ProjectPlanningChart','docName','اسم الوثيقة',1,'ar'),(1393,'#/ProjectPlanningChart','fileName','File Name',1,'en'),(1394,'#/ProjectPlanningChart','fileName','اسم المرفق',1,'ar'),(1395,'#/ProjectPlanningChart','confirm','Are you sure ?',1,'en'),(1396,'#/ProjectPlanningChart','confirm','هل انت متأكد ؟',1,'ar'),(1397,'#/projectsAssessment','confirmUploadEvident','Are you sure, you want to upload this evident ?',1,'en'),(1398,'#/projectsAssessment','confirmUploadEvident','هل انت متأكد من رفع ملف التقييم ?',1,'ar'),(1399,'#/projectsAssessment','confirmDeleteEvident','Are you sure, you want to remove this evident ?',1,'en'),(1400,'#/projectsAssessment','confirmDeleteEvident','هل انت متأكد من حذف ملف التقييم ؟',1,'ar'),(1401,'#/stratigicobjectivesChart','confirmUploadEvident','Are you sure, you want to upload this evident ?',1,'en'),(1402,'#/stratigicobjectivesChart','confirmUploadEvident','هل انت متأكد من رفع ملف التقييم ?',1,'ar'),(1403,'#/stratigicobjectivesChart','confirmDeleteEvident','Are you sure, you want to remove this evident ?',1,'en'),(1404,'#/stratigicobjectivesChart','confirmDeleteEvident','هل انت متأكد من حذف ملف التقييم ؟',1,'ar'),(1405,'BackEnd','projAssessmentNeededDocs','One or more project assessment cannot be saved due to need evidenece documents needed.',1,'en'),(1406,'BackEnd','projAssessmentNeededDocs','واحد او اكثر من تقييم المشاريع لا يمكن حفظه بسبب الملفات المطلوبة.',1,'ar'),(1407,'#/employeeObjectve','Level','Level',1,'en'),(1408,'#/employeeObjectve','Level','المستوى',1,'ar'),(1409,'#/employeeObjectve','CompetenceNature','Type',1,'en'),(1410,'#/employeeObjectve','CompetenceNature','النوع',1,'ar');
/*!40000 ALTER TABLE `tbl_resources` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tbl_skills_types`
--

DROP TABLE IF EXISTS `tbl_skills_types`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_skills_types` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `code` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `name` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tbl_skills_types`
--

LOCK TABLES `tbl_skills_types` WRITE;
/*!40000 ALTER TABLE `tbl_skills_types` DISABLE KEYS */;
/*!40000 ALTER TABLE `tbl_skills_types` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2019-12-06 15:04:17
