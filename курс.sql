CREATE DATABASE Kurs777;
GO
USE Kurs777;
GO

-- ������� "����������"
CREATE TABLE Suppliers (
SupplierID INT IDENTITY (1,1) PRIMARY KEY NOT NULL, -- ����� ����������
NameSupplier VARCHAR(100) NOT NULL, -- ��� ����������
PhoneNumber VARCHAR(15) NOT NULL, -- ����� ��������
SupplierAdress VARCHAR(200) NOT NULL, -- ����� ����������
);

-- ������� "������"
CREATE TABLE Products (
ProductID INT IDENTITY (1,1) PRIMARY KEY NOT NULL, -- ����� ������
NameProduct VARCHAR(100) NOT NULL, -- �������� ������
TypeProduct VARCHAR(50) NOT NULL, -- ��� ������
Price DECIMAL (10,2) NOT NULL, -- ���� ������
Stock INT NOT NULL, -- ���-�� ������
SupplierID INT FOREIGN KEY REFERENCES Suppliers(SupplierID)
);

-- ������� "������"
CREATE TABLE Orders (
OrderID INT IDENTITY (1,1) PRIMARY KEY NOT NULL, -- ���������� ����� ������
OrderDate DATE NOT NULL, -- ���� ������
ProductID INT FOREIGN KEY REFERENCES Products(ProductID),
Quantyti INT NOT NULL, -- ���-�� ����������� ������
PriceOrder DECIMAL (10,2) NOT NULL, -- ���� �� ������� ��������
TotalPrice DECIMAL (10,2) NOT NULL, -- ����� ���� ������
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
	(1, '��� "������� ���"','89105773836','�.������, ��. ������, �.15'),
	(2, '��� "�������� ���"','89156006768','�.������, ��. ����������, �.3'),
	(3, '��� "������ �������"','89305553232','�.������, ��. �����������, �.52'),
	(4, '��� "�������"','89526005898','�.�����-���������, ��.�������, �.17'),
	(5, '��� "������"','89202223344','�.������, ��. ��������, �.1'),
	(6, '��� "�������� ���"','89658114545','�.������, ��. ������������, �.28'),
	(7, '��� "��������"','89106248839','�.�����������, ��. ���������, �.8'),
	(8, '��� "Made in China"','89402567777','�.����, ��. ������, �.77'),
	(9, '��� "������ ���"','89309127239','�.������, ��. ��������, �.23'),
	(10, '��� "��������"','89156663020','�.������, ��. ����������, �.5');

SET IDENTITY_INSERT [Suppliers] OFF;

SET IDENTITY_INSERT [Products] ON;
INSERT INTO Products(ProductID,NameProduct,TypeProduct,Price,Stock,SupplierID)
VALUES
	(1, '������� ���','������','80','10', '1'),
	(2, '������� ���','�������','60', '12', '2'),
	(3, '��������� ����','����','95','5', '3'),
	(4, '�����','������','56','20', '4'),
	(5, '��������� ���','������','115','15', '5'),
	(6, '��������','����','110','16', '6'),
	(7, '������ ������','����','95','8', '7'),
	(8, '������','�������','70','14', '8'),
	(9, '������','������','65','11', '9'),
	(10, '�������','����','140','3', '10');

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