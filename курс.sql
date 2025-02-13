CREATE DATABASE Kurs777;
GO
USE Kurs777;
GO

-- Таблица "Поставщики"
CREATE TABLE Suppliers (
SupplierID INT IDENTITY (1,1) PRIMARY KEY NOT NULL, -- номер поставщика
NameSupplier VARCHAR(100) NOT NULL, -- имя поставщика
PhoneNumber VARCHAR(15) NOT NULL, -- номер телефона
SupplierAdress VARCHAR(200) NOT NULL, -- адрес поставщика
);

-- Таблица "Товары"
CREATE TABLE Products (
ProductID INT IDENTITY (1,1) PRIMARY KEY NOT NULL, -- номер товара
NameProduct VARCHAR(100) NOT NULL, -- название товара
TypeProduct VARCHAR(50) NOT NULL, -- тип товара
Price DECIMAL (10,2) NOT NULL, -- цена товара
Stock INT NOT NULL, -- кол-во товара
SupplierID INT FOREIGN KEY REFERENCES Suppliers(SupplierID)
);

-- Таблица "Заказы"
CREATE TABLE Orders (
OrderID INT IDENTITY (1,1) PRIMARY KEY NOT NULL, -- уникальный номер заказа
OrderDate DATE NOT NULL, -- дата заказа
ProductID INT FOREIGN KEY REFERENCES Products(ProductID),
Quantyti INT NOT NULL, -- кол-во заказанного товара
PriceOrder DECIMAL (10,2) NOT NULL, -- цена за единицу продукта
TotalPrice DECIMAL (10,2) NOT NULL, -- общая цена заказа
);

CREATE TABLE Users (
    UserID INT PRIMARY KEY IDENTITY(1,1),      
    Username NVARCHAR(50) NOT NULL UNIQUE,     
    Pass NVARCHAR(255) NOT NULL,				
);
GO

SET IDENTITY_INSERT [Suppliers] ON;
INSERT INTO Suppliers(SupplierID,NameSupplier,PhoneNumber,SupplierAdress)
VALUES
	(1, 'ООО "Веселый чай"','89105773836','г.Рязань, ул. Ленина, д.15'),
	(2, 'ООО "Грустный чай"','89156006768','г.Рязань, ул. Строителей, д.3'),
	(3, 'ООО "Чайная империя"','89305553232','г.Москва, ул. Маяковского, д.52'),
	(4, 'ООО "ЧайХаНа"','89526005898','г.Санкт-Петербург, ул.Невская, д.17'),
	(5, 'ООО "Чайник"','89202223344','г.Рязань, ул. Шабулина, д.1'),
	(6, 'ООО "Отличный чай"','89658114545','г.Липецк, ул. Промышленная, д.28'),
	(7, 'ООО "Чайназес"','89106248839','г.Новосибирск, ул. Солнечная, д.8'),
	(8, 'ООО "Made in China"','89402567777','г.Сочи, ул. Жукова, д.77'),
	(9, 'ООО "Крутой чай"','89309127239','г.Рязань, ул. Гагарина, д.23'),
	(10, 'ООО "Чаепитие"','89156663020','г.Москва, ул. Машинистов, д.5');

SET IDENTITY_INSERT [Suppliers] OFF;

SET IDENTITY_INSERT [Products] ON;
INSERT INTO Products(ProductID,NameProduct,TypeProduct,Price,Stock,SupplierID)
VALUES
	(1, 'Майский чай','Черный','80','10', '1'),
	(2, 'Русский чай','Зеленый','60', '12', '2'),
	(3, 'Принцесса Нури','Улун','95','5', '3'),
	(4, 'Ахмад','Черный','56','20', '4'),
	(5, 'Индийский чай','Черный','115','15', '5'),
	(6, 'Гринфилд','Улун','110','16', '6'),
	(7, 'Король Англии','Пуэр','95','8', '7'),
	(8, 'Липтон','Зеленый','70','14', '8'),
	(9, 'Беседа','Черный','65','11', '9'),
	(10, 'Гринвич','Пуэр','140','3', '10');

SET IDENTITY_INSERT [Products] OFF;

SET IDENTITY_INSERT [Orders] ON;
INSERT INTO Orders(OrderID,OrderDate,Quantyti,PriceOrder,TotalPrice,ProductID)
VALUES
	(1, '2024-12-04','20','50','1000','1'),
	(2, '2024-12-15','25','40', '1000','2'),
	(3, '2024-12-23','15','150','2250','3'),
	(4, '2024-12-24','40','70','2800','4'),
	(5, '2024-11-25','30','160','4800','5'),
	(6, '2025-01-11','20','130','2600','6'),
	(7, '2025-01-13','14','110','1540','7'),
	(8, '2025-01-15','28','100','2800','8'),
	(9, '2025-01-17','19','85','1615','9'),
	(10, '2025-01-23','5','200','1000','10');

SET IDENTITY_INSERT [Products] OFF;

INSERT INTO Users (Username, Pass)
VALUES
('admin', 'iyyy321i'),
('user', '1234567890');
GO