-- MySQL dump 10.13  Distrib 5.5.29, for Linux (x86_64)
--
-- Host: localhost    Database: links_old
-- ------------------------------------------------------
-- Server version	5.5.29-log

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `stash_sync`
--

DROP TABLE IF EXISTS `stash_sync`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `stash_sync` (
  `owner` varchar(64) NOT NULL,
  `leagueId` smallint(6) NOT NULL,
  `version` int(11) NOT NULL,
  `user` varchar(32) NOT NULL,
  PRIMARY KEY (`owner`,`leagueId`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `stash_sync`
--

LOCK TABLES `stash_sync` WRITE;
/*!40000 ALTER TABLE `stash_sync` DISABLE KEYS */;
INSERT INTO `stash_sync` VALUES ('Plx',0,1360014859,'Viki'),('lexaraz',0,1361405400,'Lexaras'),('LexarasPOE',0,1361242801,'Lexaras'),('Lexaras',0,1361242791,'Lexaras'),('Karkus',0,1360676511,'Viki'),('Viki',0,1370743396,'Viki'),('iXTrace4',0,1361286969,'Viki'),('iXTrace3',0,1362183577,'Viki'),('iXTrace2',0,1361104253,'Viki'),('riteris13',0,1363429414,'Vova');
/*!40000 ALTER TABLE `stash_sync` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `stash_users`
--

DROP TABLE IF EXISTS `stash_users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `stash_users` (
  `user` varchar(32) NOT NULL,
  `key` varchar(32) NOT NULL,
  PRIMARY KEY (`key`),
  UNIQUE KEY `user` (`user`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;


/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2013-06-17 17:00:24
