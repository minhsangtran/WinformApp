CREATE DATABASE QuanLyGaraOtoFinal
GO

--DROP DATABASE QuanLyGaraOto

USE QuanLyGaraOtoFinal
GO

--KHACHHANG
CREATE TABLE TableKhachHang
(
	MaKhachHang		INT IDENTITY PRIMARY KEY,
	TenKhachHang	NVARCHAR(100) NOT NULL,
	NamSinh			NVARCHAR(100) NOT NULL,
	DiaChi			NVARCHAR(100) NOT NULL,
	SoDienThoai		NVARCHAR(100) NOT NULL,
	Email			NVARCHAR(100) NOT NULL,
	TienNo		INT NOT NULL DEFAULT 0,
	UNIQUE (TenKhachHang) 
)
GO

--XE
CREATE TABLE TableXe
(
	BienSoXe	NVARCHAR(100) PRIMARY KEY,
	ChuXe		INT NOT NULL,
	MaHieuXe	INT NOT NULL,
)
GO

--HIEUXE
CREATE TABLE TableHieuXe
(
	MaHieuXe	INT IDENTITY PRIMARY KEY,
	TenHieuXe	NVARCHAR(100) NOT NULL,
	UNIQUE(TenHieuXe)
)
GO


--TIENCONG
CREATE TABLE TableTienCong
(
	MaTienCong		INT IDENTITY PRIMARY KEY,
	LoaiTienCong	NVARCHAR(100) NOT NULL,
	SoTienCong		INT NOT NULL,
	UNIQUE(LoaiTienCong)
)
GO


--PHUTUNG
CREATE TABLE TablePhuTung
(
	MaPhuTung	INT IDENTITY PRIMARY KEY,
	TenPhuTung	NVARCHAR(100) NOT NULL,
	DonGia		INT NOT NULL,
	SoLuong		INT NOT NULL,
	UNIQUE(TenPhuTung)
)	
GO


--PHIEUTIEPNHANSUACHUA
CREATE TABLE TablePhieuTiepNhan
(
	MaPhieuTiepNhan	INT IDENTITY PRIMARY KEY,
	MaKhachHang		INT NOT NULL,
	BienSoXe		NVARCHAR(100) NOT NULL,
	NgayTiepNhan	DATE NOT NULL,
	FOREIGN KEY (MaKhachHang) REFERENCES dbo.TableKhachHang(MaKhachHang),
	FOREIGN KEY (BienSoXe) REFERENCES dbo.TableXe(BienSoXe),
)
GO


--PHIEUSUACHUA
CREATE TABLE TablePhieuSuaChua
(
	MaPhieuSuaChua		INT IDENTITY PRIMARY KEY,
	MaPhieuTiepNhan		INT NOT NULL,
	NgaySuaChua			DATE NOT NULL,
	ThanhToan			BIT NOT NULL DEFAULT 0,
	FOREIGN KEY (MaPhieuTiepNhan) REFERENCES dbo.TablePhieuTiepNhan(MaPhieuTiepNhan),
	UNIQUE(MaPhieuTiepNhan)
)
GO

-- CHITIETPHIEUSUACHUA
CREATE TABLE TableCTPhieuSuaChua
(
	MaCT				INT IDENTITY PRIMARY KEY,
	MaPhieuSuaChua		INT NOT NULL,
	NoiDung				NVARCHAR(100),
	MaPhuTung			INT NOT NULL,
	SoLuong				INT NOT NULL,
	MaTienCong			INT NOT NULL,
	ThanhTien			INT NOT NULL,
	FOREIGN KEY (MaPhieuSuaChua) REFERENCES dbo.TablePhieuSuaChua(MaPhieuSuaChua),
	FOREIGN KEY (MaPhuTung) REFERENCES dbo.TablePhuTung(MaPhuTung),
	FOREIGN KEY (MaTienCong) REFERENCES dbo.TableTienCong(MaTienCong)
)
GO





--PHIEUPHATSINH
CREATE TABLE TablePhieuPhatSinh
(
	--Xoa					BIT	NOT NULL DEFAULT FALSE,
	MaPhieuPhatSinh		INT IDENTITY PRIMARY KEY,
	MaPhuTung			INT NOT NULL,
	DonGia				INT NOT NULL,
	SoLuong				INT NOT NULL,
	NgayNhap			DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
	FOREIGN KEY (MaPhuTung) REFERENCES dbo.TablePhuTung(MaPhuTung)
)
GO

--BAOCAOTON
CREATE TABLE TableBaoCaoTon
(
	MaBaoCaoTon	INT	IDENTITY PRIMARY KEY,
	Thang		INT NOT NULL DEFAULT MONTH(CURRENT_TIMESTAMP),
	Nam			INT NOT NULL DEFAULT YEAR(CURRENT_TIMESTAMP),
	MaPhuTung	INT NOT NULL,
	
)
GO

--DROP TABLE dbo.TableBaoCaoTon

CREATE TABLE TableCTBaoCaoTon
(
	MaBaoCaoTon	INT NOT NULL,
	TonDau		INT NOT NULL,
	PhatSinh	INT NOT NULL DEFAULT 0,
	TonCuoi		INT NOT NULL,
	PRIMARY KEY (MaBaoCaoTon),
	FOREIGN KEY (MaBaoCaoTon) REFERENCES dbo.TableBaoCaoTon(MaBaoCaoTon)
)
GO

--PHIEUTHUTIEN
CREATE TABLE TablePhieuThuTien
(
	MaPhieuThuTien		INT	IDENTITY PRIMARY KEY,
	MaPhieuSuaChua		INT NOT NULL,
	MaKhachHang			INT NOT NULL,
	BienSoXe			NVARCHAR(100) NOT NULL,
	NgayThuTien			DATE NOT NULL,
	SoTienThu			INT NOT NULL,
	FOREIGN KEY (MaPhieuSuaChua) REFERENCES dbo.TablePhieuSuaChua(MaPhieuSuaChua),
	FOREIGN KEY (MaKhachHang) REFERENCES dbo.TableKhachHang(MaKhachHang),
	FOREIGN KEY (BienSoXe) REFERENCES dbo.TableXe(BienSoXe)
)
GO

--BAOCAODOANHSO
CREATE TABLE TableBaoCaoDoanhSo
(
	Thang		INT NOT NULL,
	Nam			INT NOT NULL,
	MaHieuXe	INT NOT NULL,
	SoLuotSua	INT NOT NULL,
	ThanhTien	INT NOT NULL,
	TiLe		FLOAT NOT NULL,
	PRIMARY KEY (Thang, Nam),
	FOREIGN KEY (MaHieuXe) REFERENCES dbo.TableHieuXe(MaHieuXe)
)
GO
--TAIKHOAN
CREATE TABLE TableTaiKhoan
(
	UserName		NVARCHAR(100) PRIMARY KEY,
	PassWord		NVARCHAR(100) NOT NULL,
	Type			BINARY NOT NULL
)
GO

CREATE PROC USP_LayDanhSachTaiKhoanTuUserName
@userName1 NVARCHAR(100), @userName2 NVARCHAR(100)
AS
BEGIN
    SELECT * FROM dbo.TableTaiKhoan WHERE UserName = @userName1 OR UserName = @userName2
END
GO

CREATE PROC USP_Login
@userName NVARCHAR(100), @passWord NVARCHAR(100)
AS
BEGIN
    SELECT * FROM dbo.TableTaiKhoan WHERE UserName = @userName AND PassWord = @passWord
END
GO

CREATE PROC USP_Login
@userName NVARCHAR(100), @passWord NVARCHAR(100)
AS
BEGIN
    SELECT * FROM dbo.TableTaiKhoan WHERE UserName = @userName AND PassWord = @passWord
END
GO

CREATE PROC USP_SearchCustomerByID
@customerID INT
AS
BEGIN
	SELECT	MaKhachHang AS [Mã khách hàng], 
		TenKhachHang AS [Tên khách hàng],
		NamSinh AS [Năm sinh],
		DiaChi AS [Địa chỉ],
		SoDienThoai AS [Số điện thoại],
		Email
	FROM dbo.TableKhachHang 
	WHERE @customerID = MaKhachHang
END
GO


CREATE PROC USP_SearchCustomerByName
@customerName NVARCHAR(100)
AS
BEGIN
	SELECT	MaKhachHang AS [Mã khách hàng], 
			TenKhachHang AS [Tên khách hàng],
			NamSinh AS [Năm sinh],
			DiaChi AS [Địa chỉ],
			SoDienThoai AS [Số điện thoại],
			Email
	FROM dbo.TableKhachHang
	WHERE TenKhachHang LIKE '%'+@customerName+'%'
END
GO


CREATE PROC USP_SearchCarByCarNumber
@carNumber NVARCHAR(100)
AS
BEGIN
    SELECT	BienSoXe AS [Biển số xe],
			hieuXe.TenHieuXe AS [Tên hiệu xe]
	FROM dbo.TableXe AS xe, dbo.TableHieuXe AS hieuXe
	WHERE xe.MaHieuXe = hieuXe.MaHieuXe AND xe.BienSoXe LIKE '%' + @carNumber + '%'
END
GO

CREATE PROC USP_SearchCarListByCustomerName
@customerName NVARCHAR(100)
AS
BEGIN
	SELECT Xe.BienSoXe AS [Biển số xe],
		   HieuXe.TenHieuXe AS [Tên hiệu xe]
	FROM	dbo.TableKhachHang AS KhachHang, 
			dbo.TableXe AS Xe, 
			dbo.TableHieuXe AS HieuXe
	WHERE KhachHang.MaKhachHang = Xe.ChuXe AND Xe.MaHieuXe = HieuXe.MaHieuXe AND KhachHang.TenKhachHang = @customerName
END
GO


CREATE PROC USP_SearchCustomerByCarNumber
@carNumber NVARCHAR(100)
AS
BEGIN
	SELECT	KhachHang.MaKhachHang AS [Mã khách hàng],
			KhachHang.TenKhachHang AS [Tên khách hàng],
			KhachHang.NamSinh AS [Năm sinh],
			KhachHang.DiaChi AS [Địa chỉ],
			KhachHang.SoDienThoai AS [Số điện thoại],
			KhachHang.Email
	FROM	dbo.TableKhachHang AS KhachHang, 
			dbo.TableXe AS Xe
	WHERE KhachHang.MaKhachHang = Xe.ChuXe AND Xe.BienSoXe = @carNumber
END
GO

CREATE PROC USP_CreateNewAcceptForm
@customerID		INT,
@carNumber		NVARCHAR(100)
AS
BEGIN
	INSERT dbo.TablePhieuTiepNhan
	(
	    MaKhachHang,
	    BienSoXe,
		NgayTiepNhan
	)
	VALUES
	(   @customerID,   -- MaKhachHang - int
	    @carNumber, -- BienSoXe - nvarchar(100)
		GETDATE()
	    )
END
GO

CREATE PROC USP_AddNewCustomer
@customerName		NVARCHAR(100),
@customerBirthDay	NVARCHAR(100),
@customerAddress	NVARCHAR(100),
@customerPhone		NVARCHAR(100),
@customerEmail		NVARCHAR(100)
AS
BEGIN
    INSERT dbo.TableKhachHang
    (
        TenKhachHang,
        NamSinh,
        DiaChi,
        SoDienThoai,
        Email,
		TienNo
    )
    VALUES
    (   @customerName, -- TenKhachHang - nvarchar(100)
        @customerBirthDay, -- NamSinh - nvarchar(100)
        @customerAddress, -- DiaChi - nvarchar(100)
        @customerPhone, -- SoDienThoai - nvarchar(100)
        @customerEmail,  -- Email - nvarchar(100)
		0
        )
END
GO

CREATE PROC USP_AddNewCar
@carNumber	NVARCHAR(100),
@carOwner	INT,
@carType	INT
AS
BEGIN
	INSERT dbo.TableXe
	(
	    BienSoXe,
	    ChuXe,
	    MaHieuXe

	)
	VALUES
	(   @carNumber, -- BienSoXe - nvarchar(100)
	    @carOwner,   -- ChuXe - int
	    @carType   -- MaHieuXe - int
	    
	    )
END
GO

CREATE PROC USP_SearchWageByWageName
@wageName NVARCHAR(100)
AS
BEGIN
	SELECT MaTienCong AS [Mã tiền công], LoaiTienCong AS [Tên loại tiền công], SoTienCong AS [Trị giá]
	FROM dbo.TableTienCong 
	WHERE LoaiTienCong = @wageName
END
GO

CREATE PROC USP_SearchWageByWageValue
@wageValue INT
AS
BEGIN
	SELECT MaTienCong AS [Mã tiền công], LoaiTienCong AS [Tên loại tiền công], SoTienCong AS [Trị giá]
	FROM dbo.TableTienCong 
	WHERE SoTienCong = @wageValue
END
GO

CREATE PROC USP_AddNewWage
@wageName NVARCHAR(100),
@wageValue INT
AS
BEGIN
    INSERT dbo.TableTienCong
    (
        LoaiTienCong,
        SoTienCong
    )
    VALUES
    (   @wageName, -- LoaiTienCong - nvarchar(100)
        @wageValue    -- SoTienCong - int
        )
END
GO

CREATE PROC USP_UpdateWage
@wageID		INT,
@wageName	NVARCHAR(100),
@wageValue	INT
AS
BEGIN
    UPDATE dbo.TableTienCong
	SET LoaiTienCong = @wageName, SoTienCong = @wageValue
	WHERE MaTienCong = @wageID
END
GO

CREATE PROC USP_DeleteWage
@wageID INT
AS
BEGIN
    DELETE dbo.TableTienCong WHERE MaTienCong = @wageID
END
GO

-------- SEARCH ACCESSARY
CREATE PROC USP_SearchAccessaryByName
@accessaryName NVARCHAR(100)
AS
BEGIN
    SELECT	MaPhuTung AS [Mã phụ tùng],
			TenPhuTung AS [Tên phụ tùng],
			DonGia AS [Đơn giá],
			SoLuong AS [Số lượng]
	FROM	dbo.TablePhuTung
	WHERE TenPhuTung LIKE @accessaryName+'%'
END
GO

-------- SEARCH ACCESSARY BY ID
CREATE PROC USP_SearchAccessaryByID
@accessaryID NVARCHAR(100)
AS
BEGIN
    SELECT	MaPhuTung AS [Mã phụ tùng],
			TenPhuTung AS [Tên phụ tùng],
			DonGia AS [Đơn giá],
			SoLuong AS [Số lượng]
	FROM	dbo.TablePhuTung
	WHERE	MaPhuTung = @accessaryID
END
GO

-------- SEARCH ACCESSARY IMPORT BY NAME
CREATE PROC USP_SearchAccessaryImportByName
@accessaryName NVARCHAR(100)
AS
BEGIN
    SELECT	DISTINCT
			pps.MaPhieuPhatSinh AS	[Mã phiếu],
			pps.MaPhuTung		AS	[Mã phụ tùng],
			pps.DonGia			AS	[Đơn giá],
			pps.SoLuong			AS	[Số Lượng],
			DAY(pps.NgayNhap)	AS	[Ngày],
			MONTH(pps.NgayNhap)	AS	[Tháng],
			YEAR(pps.NgayNhap)	AS	[Năm]
	FROM	dbo.TablePhieuPhatSinh pps, dbo.TablePhuTung pt
	WHERE	pt.TenPhuTung LIKE @accessaryName+'%' AND pt.MaPhuTung = pps.MaPhuTung
END
GO

-------- SEARCH ACCESSARY IMPORT BY DATE
CREATE PROC USP_SearchAccessaryImportByDate
@accessaryDay INT,
@accessaryMonth INT,
@accessaryYear INT
AS
BEGIN
    SELECT	DISTINCT
			MaPhieuPhatSinh AS	[Mã phiếu],
			MaPhuTung		AS	[Mã phụ tùng],
			DonGia			AS	[Đơn giá],
			SoLuong			AS	[Số Lượng],
			DAY(NgayNhap)	AS	[Ngày],
			MONTH(NgayNhap)	AS	[Tháng],
			YEAR(NgayNhap)	AS	[Năm]
	FROM	TablePhieuPhatSinh
	WHERE	DAY(NgayNhap) = @accessaryDay AND MONTH(NgayNhap) = @accessaryMonth AND YEAR(NgayNhap) = @accessaryYear
END
GO

-------- SEARCH ACCESSARY IMPORT
CREATE PROC USP_SearchAccessaryImportByID
@accessaryID NVARCHAR(100)
AS
BEGIN
    SELECT	MaPhieuPhatSinh AS	[Mã phiếu],
			MaPhuTung		AS	[Mã phụ tùng],
			DonGia			AS	[Đơn giá],
			SoLuong			AS	[Số Lượng],
			DAY(NgayNhap)	AS	[Ngày],
			MONTH(NgayNhap)	AS	[Tháng],
			YEAR(NgayNhap)	AS	[Năm]
	FROM dbo.TablePhieuPhatSinh
	WHERE	MaPhuTung = @accessaryID
END
GO

-------- UPDATE ACCESSARY
CREATE PROC USP_UpdateAccessary
@accessaryName NVARCHAR(100),
@accessaryNumberInStock INT,
@accessaryPrice INT,
@accessaryNumberGet INT
AS
BEGIN
    UPDATE dbo.TablePhuTung
	SET SoLuong = @accessaryNumberInStock + @accessaryNumberGet, DonGia = @accessaryPrice, TenPhuTung = @accessaryName
	WHERE TenPhuTung = @accessaryName
END
GO

-------- EDIT ACCESSARY
CREATE PROC USP_EditAccessary
@accessaryID INT,
@accessaryName NVARCHAR(100),
@accessaryNumberInStock INT,
@accessaryPrice INT
AS
BEGIN
    UPDATE dbo.TablePhuTung
	SET SoLuong = @accessaryNumberInStock, DonGia = @accessaryPrice, TenPhuTung = @accessaryName
	WHERE MaPhuTung = @accessaryID
END
GO

-------- INSERT ACCESSARY
CREATE PROC USP_InsertAccessary
@accessaryName NVARCHAR(100),
@accessaryPrice INT,
@accessaryNumberGet INT
AS
BEGIN
    INSERT dbo.TablePhuTung
    (
        TenPhuTung,
        DonGia,
        SoLuong
    )
    VALUES
    (   @accessaryName, -- TenPhuTung - nvarchar(100)
        @accessaryPrice,   -- DonGia - int
        @accessaryNumberGet    -- SoLuong - int
    )
END
GO

-------- DELETE ACCESSARY
CREATE PROC USP_DeleteAccessary
@accessaryID INT
AS
BEGIN
	DELETE FROM dbo.TablePhuTung
	WHERE MaPhuTung = @accessaryID
END
GO

CREATE PROC USP_InsertAccessaryImportForm
		@accessaryID INT, -- MaPhuTung - int
	    @accessaryNumberGet	INT, -- SoLuong - int
		@accessaryPrice INT
AS
BEGIN
	INSERT dbo.TablePhieuPhatSinh
	(
	    MaPhuTung,
	    SoLuong,
		DonGia
	)
	VALUES
	(   @accessaryID, -- MaPhuTung - int
	    @accessaryNumberGet, -- SoLuong - int
		@accessaryPrice -- Don gia nhap - int
	)

END
GO

-------- DELETE ACCESSARY IMPORT FORM PROCEDURE
CREATE PROC USP_DeleteAccessaryImportForm
		@accessaryImportID	INT, -- MaPhuTung - int
		@accessaryID		INT,
		@accessaryNumberGet	INT
AS
BEGIN
		DELETE FROM dbo.TablePhieuPhatSinh
		WHERE MaPhieuPhatSinh = @accessaryImportID
		UPDATE dbo.TablePhuTung
		SET SoLuong = SoLuong - @accessaryNumberGet
		WHERE MaPhuTung = @accessaryID 
END
GO

CREATE PROC USP_UpdateInventoryReportBySupply
@accessaryID INT,
@accessaryNumberGet INT
AS
BEGIN
	IF NOT EXISTS	(SELECT * FROM TableBaoCaoTon AS B
							WHERE	B.Thang = MONTH(CURRENT_TIMESTAMP)
							AND		B.Nam = YEAR(CURRENT_TIMESTAMP)
							AND		B.MaPhuTung = @accessaryID)
	BEGIN				
	INSERT INTO dbo.TableBaoCaoTon
	(
		MaPhuTung
	)
	VALUES
	(
		@accessaryID
	)

	END

	DECLARE	@MaBaoCaoTon INT
	SELECT	@MaBaoCaoTon = MaBaoCaoTon
	FROM	dbo.TableBaoCaoTon
	WHERE	MaPhuTung = @accessaryID

	DECLARE	@SoLuong INT
	SELECT	@SoLuong = SoLuong
	FROM	dbo.TablePhuTung
	WHERE	MaPhuTung = @accessaryID

	IF NOT EXISTS (SELECT * FROM dbo.TableCTBaoCaoTon WHERE MaBaoCaoTon = @MaBaoCaoTon)
	BEGIN
		INSERT INTO dbo.TableCTBaoCaoTon
		(
			MaBaoCaoTon,
			PhatSinh,
			TonDau,
			TonCuoi
		)
		VALUES
		(
			@MaBaoCaoTon,
			@accessaryNumberGet,
			@SoLuong - @accessaryNumberGet,
			@SoLuong
		)
	END
	ELSE
	BEGIN
		DECLARE @tmpPhatSinh INT, @tmpTonCuoi INT
		SELECT @tmpPhatSinh = PhatSinh, @tmpTonCuoi = TonCuoi
		FROM	dbo.TableCTBaoCaoTon
		WHERE	MaBaoCaoTon = @MaBaoCaoTon

		UPDATE	dbo.TableCTBaoCaoTon
		SET		PhatSinh = @tmpPhatSinh + @accessaryNumberGet,
				TonCuoi = @tmpTonCuoi + @accessaryNumberGet
		FROM	dbo.TableCTBaoCaoTon
		WHERE	MaBaoCaoTon = @MaBaoCaoTon
	END

END
GO

CREATE PROC USP_CreateSaleReport
@month	INT,
@year	INT
AS 
BEGIN
    SELECT  HieuXe.TenHieuXe AS [Hiệu xe],
			(SELECT COUNT(MaPhieuTiepNhan) 
			FROM dbo.TablePhieuTiepNhan 
			WHERE BienSoXe = Xe.BienSoXe AND MONTH(NgayTiepNhan) = @month AND YEAR(NgayTiepNhan) = @year) AS [Số lượt sửa],
			(SELECT SUM(ThanhTien) FROM dbo.TableCTPhieuSuaChua WHERE  MaPhieuSuaChua IN (SELECT MaPhieuSuaChua FROM dbo.TablePhieuSuaChua WHERE MaPhieuTiepNhan IN (SELECT MaPhieuTiepNhan FROM dbo.TablePhieuTiepNhan WHERE BienSoXe = (SELECT BienSoXe FROM dbo.TableXe AS TBXe, dbo.TableHieuXe AS TBHXe WHERE TBXe.MaHieuXe = TBHXe.MaHieuXe AND TBHXe.TenHieuXe = HieuXe.TenHieuXe)) AND ThanhToan = 1)) AS [Thành tiền]
			--((SELECT SUM(ThanhTien) FROM dbo.TableCTPhieuSuaChua WHERE  MaPhieuSuaChua IN (SELECT MaPhieuSuaChua FROM dbo.TablePhieuSuaChua WHERE MaPhieuTiepNhan IN (SELECT MaPhieuTiepNhan FROM dbo.TablePhieuTiepNhan WHERE BienSoXe = Xe.BienSoXe) AND ThanhToan = 1))*100)
			--/(SELECT SUM((SELECT SUM(ThanhTien) FROM dbo.TableCTPhieuSuaChua WHERE  MaPhieuSuaChua IN (SELECT MaPhieuSuaChua FROM dbo.TablePhieuSuaChua WHERE MaPhieuTiepNhan IN (SELECT MaPhieuTiepNhan FROM dbo.TablePhieuTiepNhan WHERE BienSoXe = Xe.BienSoXe) AND ThanhToan = 1)))) AS [Tỉ lệ]
	FROM	dbo.TableXe AS Xe,
			dbo.TableHieuXe AS HieuXe,
			dbo.TablePhieuTiepNhan AS PhieuTiepNhan,
			dbo.TablePhieuSuaChua AS PhieuSuaChua,
			dbo.TableCTPhieuSuaChua AS CTPhieuSuaChua
	WHERE	Xe.BienSoXe = PhieuTiepNhan.BienSoXe AND
			PhieuSuaChua.MaPhieuTiepNhan = PhieuTiepNhan.MaPhieuTiepNhan AND
			PhieuSuaChua.MaPhieuSuaChua = CTPhieuSuaChua.MaPhieuSuaChua AND
			HieuXe.MaHieuXe = Xe.MaHieuXe

END

CREATE PROC USP_SearchInventoryReportByDate
@accessaryMonth INT,
@accessaryYear INT
AS
BEGIN
	SELECT
		bct.Thang AS [Tháng],
		bct.Nam AS [Năm],
		pt.TenPhuTung AS [Tên phụ tùng],
		ctbct.TonDau AS [Tồn đầu],
		ctbct.PhatSinh AS [Phát sinh],
		ctbct.TonCuoi AS [Tồn cuối]
	FROM	dbo.TableCTBaoCaoTon ctbct INNER JOIN (dbo.TableBaoCaoTon bct INNER JOIN dbo.TablePhuTung pt ON pt.MaPhuTung = bct.MaPhuTung)
			ON ctbct.MaBaoCaoTon = bct.MaBaoCaoTon
	WHERE	bct.Thang = @accessaryMonth AND bct.Nam = @accessaryYear
END

CREATE PROC USP_SearchInventoryReportByMonthOrYear
@accessaryMonth INT,
@accessaryYear INT
AS
BEGIN
	SELECT
		bct.Thang AS [Tháng],
		bct.Nam AS [Năm],
		pt.TenPhuTung AS [Tên phụ tùng],
		ctbct.TonDau AS [Tồn đầu],
		ctbct.PhatSinh AS [Phát sinh],
		ctbct.TonCuoi AS [Tồn cuối]
	FROM	dbo.TableCTBaoCaoTon ctbct INNER JOIN (dbo.TableBaoCaoTon bct INNER JOIN dbo.TablePhuTung pt ON pt.MaPhuTung = bct.MaPhuTung)
			ON ctbct.MaBaoCaoTon = bct.MaBaoCaoTon
	WHERE	bct.Thang = @accessaryMonth OR bct.Nam = @accessaryYear
END
GO

CREATE PROC USP_UpdateInventoryReportEachMonth
AS
BEGIN
	INSERT INTO dbo.TableBaoCaoTon
	(
		MaPhuTung
	)
	SELECT DISTINCT pt.MaPhuTung
	FROM dbo.TablePhuTung pt

	INSERT INTO dbo.TableCTBaoCaoTon
	(
		MaBaoCaoTon,
		TonDau,
		TonCuoi
	)
	SELECT DISTINCT	bct.MaBaoCaoTon, pt.SoLuong, pt.SoLuong
	FROM	dbo.TableBaoCaoTon bct INNER JOIN dbo.TablePhuTung pt ON pt.MaPhuTung = bct.MaPhuTung
	WHERE	bct.Thang = MONTH(CURRENT_TIMESTAMP)
	AND		bct.Nam = YEAR(CURRENT_TIMESTAMP)
END
GO

CREATE PROC USP_CarSearch
@CarID	NVARCHAR(100),
@CustomerName NVARCHAR(100),
@CustomerPhoneNumber NVARCHAR(100)
AS
BEGIN
	SELECT	xe.BienSoXe		as [Biển số xe],
			xe.MaHieuXe		as [Hiệu xe],
			xe.ChuXe		as [Chủ xe]
	FROM	dbo.TableXe xe INNER JOIN	dbo.TableKhachHang kh ON xe.ChuXe = kh.MaKhachHang
	WHERE	kh.TenKhachHang		LIKE	@CustomerName+'%'
	AND		xe.BienSoXe			LIKE	@CarID+'%'
	AND		kh.SoDienThoai		LIKE	@CustomerPhoneNumber+'%'
END
GO

CREATE PROC USP_CustomerSearch
@CarID	NVARCHAR(100),
@CustomerName NVARCHAR(100),
@CustomerPhoneNumber NVARCHAR(100)
AS
BEGIN
	SELECT		TenKhachHang	AS [Tên khách hàng],
				NamSinh			AS [Năm sinh],
				DiaChi			AS [Địa chỉ],
				SoDienThoai		AS [SĐT],
				Email			AS [Email],
				kh.TienNo       AS [Tiền nợ]
	FROM	dbo.TableXe xe INNER JOIN	dbo.TableKhachHang kh ON xe.ChuXe = kh.MaKhachHang
	WHERE	kh.TenKhachHang		LIKE	@CustomerName+'%'
	AND		xe.BienSoXe			LIKE	@CarID+'%'
	AND		kh.SoDienThoai		LIKE	@CustomerPhoneNumber+'%'
END
GO

CREATE PROC USP_CarBrandSearchByName
@CarBrandName NVARCHAR(100)
AS
BEGIN
	SELECT	MaHieuXe	AS [Mã hiệu xe],
			TenHieuXe	AS [Tên hiệu xe]
			--XuatXu		AS [Xuất xứ]
	FROM	dbo.TableHieuXe
	WHERE	TenHieuXe LIKE @CarBrandName+'%'
END
GO


CREATE PROC USP_InsertCarBrand
@CarBrandName NVARCHAR(100)
AS
BEGIN
	INSERT INTO dbo.TableHieuXe
	(
		TenHieuXe
	)
	VALUES
	(
		@CarBrandName
	)
END
GO