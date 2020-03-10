SET @OLD_FOREIGN_KEY_CHECKS = @@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS =1;

CREATE SCHEMA IF NOT EXISTS carShowroom DEFAULT CHARACTER SET utf8;
USE  carShowroom;

drop table if exists user;
drop table if exists car;
drop table if exists warehouse;
drop schema if exists carShowroom;

CREATE TABLE IF NOT EXISTS carShowroom.user (
    idUser INT PRIMARY KEY NOT NULL AUTO_INCREMENT,
    role ENUM('client', 'admin', 'driver') NOT NULL,
    name VARCHAR(30) NOT NULL UNIQUE,
    login VARCHAR(30) NOT NULL UNIQUE,
    password VARCHAR(30) NOT NULL,
    birthday DATE NOT NULL,
    phoneNumber VARCHAR(30) NOT NULL,
    email VARCHAR(30) NOT NULL
);

CREATE TABLE IF NOT EXISTS carShowroom.car (
    idCar INT PRIMARY KEY NOT NULL AUTO_INCREMENT,
    country VARCHAR(30) NOT NULL,
    color VARCHAR(20) NOT NULL,
    mark VARCHAR(20) NOT NULL,
    model VARCHAR(30) NOT NULL,
    engine VARCHAR(40) NOT NULL,
    price FLOAT NOT NULL,
    built VARCHAR(100) NOT NULL
    warehouseId int not NULL,
    FOREIGN KEY (warehouseId)  REFERENCES carShowroom.warehouse(idWarehouse)
);

CREATE TABLE IF NOT EXISTS carShowroom.warehouse (
    idWarehouse INT PRIMARY KEY NOT NULL AUTO_INCREMENT,
    address VARCHAR(100) NOT NULL,
    amountOfCar INT NOT NULL
);