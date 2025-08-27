-- MySQL dump 10.13  Distrib 8.0.42, for Win64 (x86_64)
--
-- Host: localhost    Database: fertilizer
-- ------------------------------------------------------
-- Server version	8.0.42

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
-- Table structure for table `bank_transactions`
--

DROP TABLE IF EXISTS `bank_transactions`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `bank_transactions` (
  `transaction_id` int NOT NULL AUTO_INCREMENT,
  `transaction_type` enum('Deposit','Withdraw') NOT NULL,
  `amount` decimal(10,2) NOT NULL,
  `transaction_date` datetime DEFAULT CURRENT_TIMESTAMP,
  `remarks` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`transaction_id`),
  CONSTRAINT `bank_transactions_chk_1` CHECK ((`amount` > 0))
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `bank_transactions`
--

LOCK TABLES `bank_transactions` WRITE;
/*!40000 ALTER TABLE `bank_transactions` DISABLE KEYS */;
/*!40000 ALTER TABLE `bank_transactions` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `batch_details`
--

DROP TABLE IF EXISTS `batch_details`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `batch_details` (
  `batch_detail_id` int NOT NULL AUTO_INCREMENT,
  `batch_id` int NOT NULL,
  `product_id` int NOT NULL,
  `cost_price` int NOT NULL,
  `quantity_recived` int NOT NULL,
  PRIMARY KEY (`batch_detail_id`),
  KEY `batch_id` (`batch_id`),
  KEY `product_id` (`product_id`),
  CONSTRAINT `batch_details_ibfk_1` FOREIGN KEY (`batch_id`) REFERENCES `batches` (`batch_id`) ON DELETE CASCADE,
  CONSTRAINT `batch_details_ibfk_2` FOREIGN KEY (`product_id`) REFERENCES `products` (`product_id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=59 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `batch_details`
--

LOCK TABLES `batch_details` WRITE;
/*!40000 ALTER TABLE `batch_details` DISABLE KEYS */;
INSERT INTO `batch_details` VALUES (34,1,2,1500,100),(35,1,2,1450,80),(36,1,3,1600,120),(37,1,4,1550,90),(38,2,5,2200,50),(39,2,6,2100,40),(40,2,7,2000,60),(41,2,8,2300,45),(42,3,9,1800,100),(43,3,3,1750,85),(44,3,4,1900,95),(45,3,2,1850,110),(46,4,3,500,30),(47,4,4,600,25),(48,4,5,550,35),(49,4,6,700,20),(50,5,7,800,40),(51,5,8,850,50),(52,5,9,900,30),(53,5,2,950,60),(54,3,5,1200,12),(55,14,3,8000,100),(56,14,11,1900,100),(57,16,12,13700,300),(58,16,5,1200,200);
/*!40000 ALTER TABLE `batch_details` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `batches`
--

DROP TABLE IF EXISTS `batches`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `batches` (
  `batch_id` int NOT NULL AUTO_INCREMENT,
  `batch_name` varchar(100) NOT NULL,
  `supplier_id` int NOT NULL,
  `recieved_date` datetime DEFAULT NULL,
  PRIMARY KEY (`batch_id`),
  KEY `supplier_id` (`supplier_id`),
  CONSTRAINT `batches_ibfk_1` FOREIGN KEY (`supplier_id`) REFERENCES `suppliers` (`supplier_id`)
) ENGINE=InnoDB AUTO_INCREMENT=17 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `batches`
--

LOCK TABLES `batches` WRITE;
/*!40000 ALTER TABLE `batches` DISABLE KEYS */;
INSERT INTO `batches` VALUES (1,'Batch-Urea-July',1,'2025-07-01 00:00:00'),(2,'Batch-DAP-July',2,'2025-07-02 00:00:00'),(3,'Batch-NPK-July',3,'2025-07-03 00:00:00'),(4,'Batch-Pesticides-July',1,'2025-07-04 00:00:00'),(5,'Batch-Micronutrients-July',2,'2025-07-05 00:00:00'),(14,'august-2025-01',5,'2025-08-14 14:40:38'),(15,'august-2025-02',10,'2025-08-14 15:11:16'),(16,'august-14-2025',10,'2025-08-14 15:14:01');
/*!40000 ALTER TABLE `batches` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `customer_bill_details`
--

DROP TABLE IF EXISTS `customer_bill_details`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `customer_bill_details` (
  `Bill_detail_ID` int NOT NULL AUTO_INCREMENT,
  `Bill_id` int NOT NULL,
  `product_id` int DEFAULT NULL,
  `quantity` int DEFAULT NULL,
  `discount` int DEFAULT '0',
  `status` enum('bill','invoice') NOT NULL,
  PRIMARY KEY (`Bill_detail_ID`),
  KEY `customerbilldetail_ibfk_2` (`product_id`),
  KEY `customerbilldetail_ibfk_1` (`Bill_id`),
  CONSTRAINT `customerbilldetail_ibfk_1` FOREIGN KEY (`Bill_id`) REFERENCES `customerbills` (`BillID`) ON DELETE CASCADE,
  CONSTRAINT `customerbilldetail_ibfk_2` FOREIGN KEY (`product_id`) REFERENCES `products` (`product_id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=129 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `customer_bill_details`
--

LOCK TABLES `customer_bill_details` WRITE;
/*!40000 ALTER TABLE `customer_bill_details` DISABLE KEYS */;
INSERT INTO `customer_bill_details` VALUES (101,81,5,12,100,'bill'),(102,81,3,1,1,'bill'),(103,81,8,1,1,'bill'),(104,82,8,1,100,'bill'),(105,82,7,1,100,'bill'),(106,82,5,1,111,'bill'),(107,83,4,2,100,'bill'),(108,83,8,2,0,'bill'),(110,86,4,1,100,'bill'),(111,86,7,1,100,'bill'),(112,87,3,2,100,'bill'),(113,90,3,2,100,'bill'),(114,90,2,1,0,'bill'),(115,91,2,2,0,'bill'),(116,92,2,2,0,'bill'),(117,92,4,1,100,'bill'),(118,94,3,2,0,'bill'),(119,95,3,1,0,'bill'),(120,95,5,2,100,'bill'),(121,95,4,2,0,'bill'),(122,95,2,1,0,'bill'),(123,96,3,2,100,'bill'),(124,97,12,10,0,'bill'),(125,98,2,10,0,'bill'),(126,99,2,18,0,'bill'),(127,99,3,5,0,'bill'),(128,99,5,3,0,'bill');
/*!40000 ALTER TABLE `customer_bill_details` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `customerbills`
--

DROP TABLE IF EXISTS `customerbills`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `customerbills` (
  `BillID` int NOT NULL AUTO_INCREMENT,
  `CustomerID` int NOT NULL,
  `SaleDate` date NOT NULL,
  `total_price` int DEFAULT NULL,
  `paid_amount` int DEFAULT NULL,
  `payment_status` enum('Paid','Due') DEFAULT 'Due',
  PRIMARY KEY (`BillID`),
  KEY `CustomerID` (`CustomerID`),
  CONSTRAINT `customerbills_ibfk_1` FOREIGN KEY (`CustomerID`) REFERENCES `customers` (`customer_id`) ON DELETE RESTRICT ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=100 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `customerbills`
--

LOCK TABLES `customerbills` WRITE;
/*!40000 ALTER TABLE `customerbills` DISABLE KEYS */;
INSERT INTO `customerbills` VALUES (77,27,'2025-07-22',10800,9000,'Due'),(78,32,'2025-07-22',3600,3590,'Due'),(79,29,'2025-07-22',15100,6000,'Due'),(81,26,'2025-07-22',30596,111,'Due'),(82,30,'2025-07-22',6878,6800,'Due'),(83,33,'2025-07-22',8000,2000,'Due'),(86,28,'2025-07-22',6300,6300,'Due'),(87,30,'2025-07-23',11800,12000,'Due'),(90,28,'2025-07-23',2300,0,'Due'),(91,29,'2025-07-23',5000,2000,'Due'),(92,30,'2025-07-23',8100,8100,'Due'),(94,26,'2025-07-23',14000,14000,'Due'),(95,28,'2025-07-25',23700,900,'Due'),(96,30,'2025-08-14',13800,10000,'Due'),(97,26,'2025-08-14',140000,90000,'Due'),(98,26,'2025-08-14',45000,20000,'Due'),(99,28,'2025-08-14',150100,100000,'Due');
/*!40000 ALTER TABLE `customerbills` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `customerpricerecord`
--

DROP TABLE IF EXISTS `customerpricerecord`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `customerpricerecord` (
  `record_id` int NOT NULL AUTO_INCREMENT,
  `customer_id` int NOT NULL,
  `BillID` int NOT NULL,
  `date` date NOT NULL,
  `payment` decimal(10,2) NOT NULL,
  `remarks` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`record_id`),
  KEY `customer_id` (`customer_id`),
  KEY `BillID` (`BillID`),
  CONSTRAINT `customerpricerecord_ibfk_1` FOREIGN KEY (`customer_id`) REFERENCES `customers` (`customer_id`),
  CONSTRAINT `customerpricerecord_ibfk_2` FOREIGN KEY (`BillID`) REFERENCES `customerbills` (`BillID`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=43 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `customerpricerecord`
--

LOCK TABLES `customerpricerecord` WRITE;
/*!40000 ALTER TABLE `customerpricerecord` DISABLE KEYS */;
INSERT INTO `customerpricerecord` VALUES (23,29,79,'2025-07-22',6000.00,NULL),(25,26,81,'2025-07-22',111.00,NULL),(26,30,82,'2025-07-22',6000.00,NULL),(27,33,83,'2025-07-22',2000.00,NULL),(29,28,86,'2025-07-22',6300.00,NULL),(30,32,78,'2025-07-24',90.00,''),(31,30,82,'2025-07-25',800.00,'secon payment'),(32,30,87,'2025-07-23',12000.00,NULL),(33,28,90,'2025-07-23',0.00,NULL),(34,29,91,'2025-07-23',2000.00,NULL),(35,30,92,'2025-07-23',8100.00,NULL),(36,26,94,'2025-07-23',14000.00,NULL),(37,28,95,'2025-07-25',900.00,NULL),(38,30,96,'2025-08-14',10000.00,NULL),(39,26,97,'2025-08-14',50000.00,NULL),(40,26,97,'2025-08-14',40000.00,''),(41,26,98,'2025-08-14',20000.00,NULL),(42,28,99,'2025-08-14',100000.00,NULL);
/*!40000 ALTER TABLE `customerpricerecord` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `customers`
--

DROP TABLE IF EXISTS `customers`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `customers` (
  `customer_id` int NOT NULL AUTO_INCREMENT,
  `first_name` varchar(50) DEFAULT NULL,
  `last_name` varchar(50) DEFAULT NULL,
  `phone` varchar(20) DEFAULT NULL,
  `address` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`customer_id`),
  UNIQUE KEY `first_name` (`first_name`,`last_name`)
) ENGINE=InnoDB AUTO_INCREMENT=36 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `customers`
--

LOCK TABLES `customers` WRITE;
/*!40000 ALTER TABLE `customers` DISABLE KEYS */;
INSERT INTO `customers` VALUES (26,'Ali','Khan','03001234567','Lahore, Punjab'),(27,'Sara','Ahmed','03119876543','Karachi, Sindh'),(28,'Usman','Iqbal','03211223344','Islamabad'),(29,'Ayesha','Malik','03337775555','Multan'),(30,'Hassan','Raza','03005551234','Faisalabad'),(31,'Fatima','Zafar','03449998877','Peshawar'),(32,'Bilal','Hussain','03008889900','Rawalpindi'),(33,'Zainab','Mir','03112223344','Quetta'),(34,'abdu','ahad','09090099090','lahore'),(35,'abdul','ahad','0909090909','faislabad');
/*!40000 ALTER TABLE `customers` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `inventory_log`
--

DROP TABLE IF EXISTS `inventory_log`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `inventory_log` (
  `log_id` int NOT NULL AUTO_INCREMENT,
  `product_id` int NOT NULL,
  `change_type` enum('purchase','sale','return_from_customer','return_to_supplier','manual_adjustment','used_in_service') NOT NULL,
  `quantity_change` int NOT NULL,
  `log_date` datetime DEFAULT CURRENT_TIMESTAMP,
  `remarks` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`log_id`),
  KEY `product_id` (`product_id`),
  CONSTRAINT `inventory_log_ibfk_1` FOREIGN KEY (`product_id`) REFERENCES `products` (`product_id`)
) ENGINE=InnoDB AUTO_INCREMENT=237 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `inventory_log`
--

LOCK TABLES `inventory_log` WRITE;
/*!40000 ALTER TABLE `inventory_log` DISABLE KEYS */;
INSERT INTO `inventory_log` VALUES (210,5,'purchase',12,'2025-07-21 12:38:23','Batch: Batch-NPK-July, Price: 1200'),(211,4,'sale',1,'2025-07-22 11:01:47',NULL),(212,7,'sale',1,'2025-07-22 11:01:47',NULL),(213,3,'sale',2,'2025-07-23 23:39:22',NULL),(214,3,'sale',2,'2025-07-23 23:39:22',NULL),(215,2,'sale',1,'2025-07-23 23:39:22',NULL),(216,2,'sale',2,'2025-07-23 23:39:22',NULL),(217,2,'sale',2,'2025-07-23 23:39:22',NULL),(218,4,'sale',1,'2025-07-23 23:39:22',NULL),(219,3,'sale',2,'2025-07-23 23:39:22',NULL),(220,3,'sale',1,'2025-07-25 22:25:03',NULL),(221,5,'sale',2,'2025-07-25 22:25:03',NULL),(222,4,'sale',2,'2025-07-25 22:25:03',NULL),(223,2,'sale',1,'2025-07-25 22:25:03',NULL),(224,10,'manual_adjustment',11,'2025-08-02 10:34:11','Manual adjustment by the user'),(225,3,'sale',2,'2025-08-14 14:35:47',NULL),(226,11,'manual_adjustment',0,'2025-08-14 14:42:34','Manual adjustment by the user'),(227,3,'purchase',100,'2025-08-14 14:43:16','Batch: august-2025-01, Price: 8000'),(228,11,'purchase',100,'2025-08-14 14:43:16','Batch: august-2025-01, Price: 1900'),(229,12,'manual_adjustment',0,'2025-08-14 15:17:12','Manual adjustment by the user'),(230,12,'purchase',300,'2025-08-14 15:19:22','Batch: august-14-2025, Price: 13700'),(231,5,'purchase',200,'2025-08-14 15:19:23','Batch: august-14-2025, Price: 1200'),(232,12,'sale',10,'2025-08-14 15:20:27',NULL),(233,2,'sale',10,'2025-08-14 15:30:43',NULL),(234,2,'sale',18,'2025-08-14 15:36:13',NULL),(235,3,'sale',5,'2025-08-14 15:36:13',NULL),(236,5,'sale',3,'2025-08-14 15:36:13',NULL);
/*!40000 ALTER TABLE `inventory_log` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `orderdetails`
--

DROP TABLE IF EXISTS `orderdetails`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `orderdetails` (
  `orderdetail_id` int NOT NULL AUTO_INCREMENT,
  `order_id` int DEFAULT NULL,
  `product_id` int DEFAULT NULL,
  `quantity` int DEFAULT NULL,
  PRIMARY KEY (`orderdetail_id`),
  KEY `order_id` (`order_id`),
  KEY `product_id` (`product_id`),
  CONSTRAINT `orderdetails_ibfk_1` FOREIGN KEY (`order_id`) REFERENCES `orders` (`order_id`),
  CONSTRAINT `orderdetails_ibfk_2` FOREIGN KEY (`product_id`) REFERENCES `products` (`product_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `orderdetails`
--

LOCK TABLES `orderdetails` WRITE;
/*!40000 ALTER TABLE `orderdetails` DISABLE KEYS */;
/*!40000 ALTER TABLE `orderdetails` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `orders`
--

DROP TABLE IF EXISTS `orders`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `orders` (
  `order_id` int NOT NULL AUTO_INCREMENT,
  `supplier_id` int DEFAULT NULL,
  `date` datetime DEFAULT NULL,
  PRIMARY KEY (`order_id`),
  KEY `supplier_id` (`supplier_id`),
  CONSTRAINT `orders_ibfk_1` FOREIGN KEY (`supplier_id`) REFERENCES `suppliers` (`supplier_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `orders`
--

LOCK TABLES `orders` WRITE;
/*!40000 ALTER TABLE `orders` DISABLE KEYS */;
/*!40000 ALTER TABLE `orders` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `products`
--

DROP TABLE IF EXISTS `products`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `products` (
  `product_id` int NOT NULL AUTO_INCREMENT,
  `name` varchar(100) NOT NULL,
  `description` varchar(200) DEFAULT NULL,
  `sale_price` int DEFAULT NULL,
  `quantity` int DEFAULT NULL,
  PRIMARY KEY (`product_id`)
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `products`
--

LOCK TABLES `products` WRITE;
/*!40000 ALTER TABLE `products` DISABLE KEYS */;
INSERT INTO `products` VALUES (2,'Urea','Nitrogen-based fertilizer for general crop use',2500,66),(3,'DAP','Di-ammonium phosphate for root development',10000,136),(4,'Potash','Potassium-rich fertilizer for fruiting crops',3200,69),(5,'Zinc Sulfate','Micronutrient for plant growth',1400,247),(6,'Calcium Nitrate','Fertilizer for calcium and nitrogen supply',2300,30),(7,'NPK 20-20-20','Balanced fertilizer for all-round growth',3500,59),(8,'Ammonium Sulfate','Used for nitrogen and sulfur supply',2000,23),(9,'Compost','Organic fertilizer for soil improvement',800,200),(10,'fauji DAP','11',12000,11),(11,'fert','',2000,100),(12,'sona dap','',13800,290);
/*!40000 ALTER TABLE `products` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `supplier_bill_details`
--

DROP TABLE IF EXISTS `supplier_bill_details`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `supplier_bill_details` (
  `s_Bill_detail_ID` int NOT NULL AUTO_INCREMENT,
  `supplier_Bill_ID` int NOT NULL,
  `product_id` int DEFAULT NULL,
  `quantity` int DEFAULT NULL,
  PRIMARY KEY (`s_Bill_detail_ID`),
  KEY `supplierbilldetail_ibfk_2` (`product_id`),
  KEY `supplierbilldetail_ibfk_1` (`supplier_Bill_ID`),
  CONSTRAINT `supplierbilldetail_ibfk_1` FOREIGN KEY (`supplier_Bill_ID`) REFERENCES `supplierbills` (`supplier_Bill_ID`) ON UPDATE CASCADE,
  CONSTRAINT `supplierbilldetail_ibfk_2` FOREIGN KEY (`product_id`) REFERENCES `products` (`product_id`) ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=18 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `supplier_bill_details`
--

LOCK TABLES `supplier_bill_details` WRITE;
/*!40000 ALTER TABLE `supplier_bill_details` DISABLE KEYS */;
/*!40000 ALTER TABLE `supplier_bill_details` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `supplierbills`
--

DROP TABLE IF EXISTS `supplierbills`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `supplierbills` (
  `supplier_Bill_ID` int NOT NULL AUTO_INCREMENT,
  `supplier_id` int NOT NULL,
  `Date` date NOT NULL,
  `total_price` int DEFAULT NULL,
  `batch_id` int DEFAULT NULL,
  `paid_amount` int DEFAULT '0',
  `payment_status` enum('Paid','Due') DEFAULT 'Due',
  PRIMARY KEY (`supplier_Bill_ID`),
  KEY `supplier_id` (`supplier_id`),
  KEY `fk_sb_2` (`batch_id`),
  CONSTRAINT `fk_sb_2` FOREIGN KEY (`batch_id`) REFERENCES `batches` (`batch_id`),
  CONSTRAINT `supplierbills_ibfk_1` FOREIGN KEY (`supplier_id`) REFERENCES `suppliers` (`supplier_id`) ON DELETE RESTRICT ON UPDATE CASCADE,
  CONSTRAINT `chk_paid_vs_total` CHECK ((`paid_amount` <= `total_price`)),
  CONSTRAINT `chk_supplier_paid_amount` CHECK ((`paid_amount` <= `total_price`))
) ENGINE=InnoDB AUTO_INCREMENT=26 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `supplierbills`
--

LOCK TABLES `supplierbills` WRITE;
/*!40000 ALTER TABLE `supplierbills` DISABLE KEYS */;
/*!40000 ALTER TABLE `supplierbills` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `supplierpricerecord`
--

DROP TABLE IF EXISTS `supplierpricerecord`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `supplierpricerecord` (
  `supplier_record_id` int NOT NULL AUTO_INCREMENT,
  `supplier_id` int NOT NULL,
  `supplier_Bill_ID` int NOT NULL,
  `date` date NOT NULL,
  `payment` decimal(10,2) NOT NULL,
  `remarks` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`supplier_record_id`),
  KEY `supplier_id` (`supplier_id`),
  KEY `supplier_Bill_ID` (`supplier_Bill_ID`),
  CONSTRAINT `supplierpricerecord_ibfk_1` FOREIGN KEY (`supplier_id`) REFERENCES `suppliers` (`supplier_id`),
  CONSTRAINT `supplierpricerecord_ibfk_2` FOREIGN KEY (`supplier_Bill_ID`) REFERENCES `supplierbills` (`supplier_Bill_ID`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=29 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `supplierpricerecord`
--

LOCK TABLES `supplierpricerecord` WRITE;
/*!40000 ALTER TABLE `supplierpricerecord` DISABLE KEYS */;
/*!40000 ALTER TABLE `supplierpricerecord` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `suppliers`
--

DROP TABLE IF EXISTS `suppliers`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `suppliers` (
  `supplier_id` int NOT NULL AUTO_INCREMENT,
  `name` varchar(100) NOT NULL,
  `phone` varchar(20) DEFAULT NULL,
  `address` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`supplier_id`),
  UNIQUE KEY `name` (`name`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `suppliers`
--

LOCK TABLES `suppliers` WRITE;
/*!40000 ALTER TABLE `suppliers` DISABLE KEYS */;
INSERT INTO `suppliers` VALUES (1,'Agro Fertilizers Pvt Ltd','0300-1234568','Lahore, Punjab'),(2,'GreenGrow Suppliers','0311-9876543','Faisalabad, Punjab'),(3,'Pak Agri Chemicals','0322-456789','Multan, Punjab'),(4,'Zameen Crop Solutions','0345-6789123','Sahiwal, Punjab'),(5,'Nutrient Agro Tech','0333-1122334','Bahawalpur, Punjab'),(7,'gillani fertilizers','0202020202','bhains colony, karachi'),(8,'khan fertilizer','09099900909','lahore\r\nlahore'),(9,'abdullah fertilizer wale','0303030303','dijkot'),(10,'ali farqan','0909090909','fasisi');
/*!40000 ALTER TABLE `suppliers` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `transaction_history`
--

DROP TABLE IF EXISTS `transaction_history`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `transaction_history` (
  `transaction_id` int NOT NULL AUTO_INCREMENT,
  `transaction_type` enum('Deposit','Withdraw') NOT NULL,
  `amount` decimal(10,2) NOT NULL,
  `transaction_date` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `description` varchar(255) DEFAULT NULL,
  `remaining_balance` decimal(10,2) NOT NULL,
  PRIMARY KEY (`transaction_id`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `transaction_history`
--

LOCK TABLES `transaction_history` WRITE;
/*!40000 ALTER TABLE `transaction_history` DISABLE KEYS */;
INSERT INTO `transaction_history` VALUES (1,'Deposit',10000.00,'2025-07-01 00:00:00','Initial deposit',10000.00),(2,'Withdraw',2500.00,'2025-07-05 00:00:00','Purchase of fertilizers',7500.00),(3,'Deposit',5000.00,'2025-07-10 00:00:00','Farmer payment received',12500.00),(4,'Withdraw',3000.00,'2025-07-12 00:00:00','Maintenance expense',9500.00),(5,'Deposit',8000.00,'2025-07-15 00:00:00','Loan credited',17500.00),(6,'Withdraw',4500.00,'2025-07-20 00:00:00','Salary distribution',13000.00),(7,'Withdraw',1212.00,'2025-08-02 00:00:00','asasa',11788.00);
/*!40000 ALTER TABLE `transaction_history` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `users`
--

DROP TABLE IF EXISTS `users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `users` (
  `user_id` int NOT NULL AUTO_INCREMENT,
  `username` varchar(50) NOT NULL,
  `password_hash` varchar(255) NOT NULL,
  `Email` varchar(100) DEFAULT NULL,
  `role` enum('Admin','Technician','Sales') NOT NULL,
  PRIMARY KEY (`user_id`),
  UNIQUE KEY `username` (`username`),
  KEY `idx_users_username` (`username`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `users`
--

LOCK TABLES `users` WRITE;
/*!40000 ALTER TABLE `users` DISABLE KEYS */;
/*!40000 ALTER TABLE `users` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2025-08-27  9:29:35
