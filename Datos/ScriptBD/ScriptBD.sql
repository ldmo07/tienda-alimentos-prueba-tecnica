--CREO LA BD SI NO EXISTE
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'TiendaAlimentosBD')
BEGIN
	CREATE DATABASE TiendaAlimentosBD;
END
GO

--USO LA BD 
USE TiendaAlimentosBD;
GO

--CREO LA TABLA DE CATEGORIAS
CREATE TABLE Categoria
(
	idCategoria UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
	nombreCategoria NVARCHAR(250) NOT NULL,
	descripcionCategoria NVARCHAR(250) DEFAULT NULL,
	isEliminado BIT NOT NULL DEFAULT 0,
	fechaInsercion DATETIME NOT NULL DEFAULT GETDATE()

);


--CREO LA TABLA DE PRODUCTOS
CREATE TABLE Producto
(
	idProducto UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
	idCategoria UNIQUEIDENTIFIER NOT NULL,
	nombreProducto NVARCHAR(250) NOT NULL,
	precioProducto MONEY NOT NULL DEFAULT 0,
	unidadesDisponibles INT NOT NULL DEFAULT 1,
	isDisponible BIT DEFAULT 1,
	isEliminado BIT NOT NULL DEFAULT 0,
	fechaInsercion DATETIME NOT NULL DEFAULT GETDATE(),
	CONSTRAINT fk_Producto_Categoria FOREIGN KEY (idCategoria) REFERENCES Categoria(idCategoria)
);

--CREO LA TABLA DE USUARIOS
CREATE TABLE Usuario
(
	idUsuario UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
	nombreUsuario NVARCHAR(250) NOT NULL,
	apellidoUsuario NVARCHAR(250)NOT NULL,
	nidUsuario NVARCHAR(15) NOT NULL UNIQUE,
	userName NVARCHAR(250) NOT NULL UNIQUE,
    [password] NVARCHAR(250) NOT NULL,
	correoUsuario NVARCHAR(250) NOT NULL UNIQUE,
	rolUsuario NVARCHAR(50) NOT NULL,
	isEliminado BIT NOT NULL DEFAULT 0,
	fechaInsercion DATETIME NOT NULL DEFAULT GETDATE()
);

--CREO LA TABLA DE PEDIDOS
CREATE TABLE Pedido
(
	idPedido UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
	idUsuario UNIQUEIDENTIFIER NOT NULL,
	direccionEnvio NVARCHAR(500)NOT NULL,
	isPagado BIT NOT NULL DEFAULT 0,
	isEliminado BIT NOT NULL DEFAULT 0,
	fechaInsercion DATETIME NOT NULL DEFAULT GETDATE(),
	CONSTRAINT fk_Pedido_Usuario FOREIGN KEY (idUsuario) REFERENCES Usuario(idUsuario),
);

--CREO LA TABLA DE DETALLES DEL PEDIDO
CREATE TABLE DetallePedido
(
	idDetallePedido UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
	idPedido UNIQUEIDENTIFIER NOT NULL,
	idProducto UNIQUEIDENTIFIER NOT NULL,
	unidadesPedidas INT NOT NULL,
	fechaInsercion DATETIME NOT NULL DEFAULT GETDATE(),
	CONSTRAINT fk_DetallePedido_Pedido FOREIGN KEY (idPedido) REFERENCES Pedido(idPedido),
	CONSTRAINT fk_DetallePedido_Producto FOREIGN KEY (idProducto) REFERENCES Producto(idProducto)
);


--***************************************************************TIPOS*****************************************************

--CREO EL TIPO DE CATEGORIAS
CREATE TYPE CategoriaType AS TABLE
(
	idCategoria UNIQUEIDENTIFIER DEFAULT NEWID(),
	nombreCategoria NVARCHAR(250) NOT NULL,
	descripcionCategoria NVARCHAR(250) DEFAULT NULL,
	isEliminado BIT NOT NULL DEFAULT 0,
	fechaInsercion DATETIME NOT NULL DEFAULT GETDATE()
);

--CREO EL TIPO DE PRODUCTOS
CREATE TYPE ProductoType AS TABLE
(
	idProducto UNIQUEIDENTIFIER  DEFAULT NEWID(),
	idCategoria UNIQUEIDENTIFIER NOT NULL,
	nombreProducto NVARCHAR(250) NOT NULL,
	precioProducto MONEY NOT NULL DEFAULT 0,
	unidadesDisponibles INT NOT NULL DEFAULT 1,
	isDisponible BIT DEFAULT 1,
	isEliminado BIT NOT NULL DEFAULT 0,
	fechaInsercion DATETIME NOT NULL DEFAULT GETDATE()
);

--CREO EL TIPO DE USUARIOS
CREATE TYPE UsuarioType AS TABLE
(
	idUsuario UNIQUEIDENTIFIER  DEFAULT NEWID(),
	nombreUsuario NVARCHAR(250) NOT NULL,
	apellidoUsuario NVARCHAR(250)NOT NULL,
	nidUsuario NVARCHAR(15) NOT NULL UNIQUE,
	userName NVARCHAR(250) NOT NULL UNIQUE,
    [password] NVARCHAR(250) NOT NULL,
	correoUsuario NVARCHAR(250) NOT NULL UNIQUE,
	rolUsuario NVARCHAR(50) NOT NULL,
	isEliminado BIT NOT NULL DEFAULT 0,
	fechaInsercion DATETIME NOT NULL DEFAULT GETDATE()
);

--CREO EL TIPO DE PEDIDOS
CREATE TYPE PedidoType AS TABLE
(
	idPedido UNIQUEIDENTIFIER  DEFAULT NEWID(),
	idUsuario UNIQUEIDENTIFIER NOT NULL,
	direccionEnvio NVARCHAR(500)NOT NULL,
	isPagado BIT NOT NULL DEFAULT 0,
	isEliminado BIT NOT NULL DEFAULT 0,
	fechaInsercion DATETIME NOT NULL DEFAULT GETDATE()
);

--CREO EL TIPO DE DETALLES DEL PEDIDO
CREATE TYPE DetallePedidoType AS TABLE
(
	idDetallePedido UNIQUEIDENTIFIER  DEFAULT NEWID(),
	idPedido UNIQUEIDENTIFIER NOT NULL,
	idProducto UNIQUEIDENTIFIER NOT NULL,
	unidadesPedidas INT NOT NULL,
	fechaInsercion DATETIME NOT NULL DEFAULT GETDATE()
);

--******************************************* STORE PROCEDURE ****************************************

GO
-- =============================================
-- Author:		LUIS DAVID MERCADO 
-- Create date: 16/04/2024
-- Description:	INSERTA UNA CATEGORIA
-- =============================================
CREATE PROCEDURE SP_INSERTAR_CATEGORIA 
	@nombreCategoria NVARCHAR(250),
	@descripcionCategoria NVARCHAR(250)=NULL
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO Categoria(nombreCategoria,descripcionCategoria) VALUES (@nombreCategoria,@descripcionCategoria);
END
GO

-- =============================================
-- Author:		LUIS DAVID MERCADO ORTEGA
-- Create date: 16/04/2024
-- Description:	OBTIENE UNA CATEGORIA O UNA LISTA DE CATEGORIAS
-- =============================================
CREATE PROCEDURE SP_LISTAR_CATEGORIA
	@idCategoria UNIQUEIDENTIFIER = NULL 
AS
BEGIN
	
	SET NOCOUNT ON;

	IF @idCategoria IS NOT NULL
	BEGIN
		SELECT * FROM Categoria WHERE idCategoria=@idCategoria AND isEliminado=0;
	END
	ELSE
	BEGIN
		SELECT * FROM Categoria Where isEliminado=0;
	END
    
END
GO

-- =============================================
-- Author:		LUIS DAVID MERCADO ORTEGA
-- Create date: 17/04/2024
-- Description:	ELIMINA UNA CATEGORIA
-- =============================================
CREATE PROCEDURE SP_ELIMINAR_CATEGORIA
	@idCategoria UNIQUEIDENTIFIER
AS
BEGIN
	
	SET NOCOUNT ON;
	IF NOT EXISTS (  SELECT TOP 1 * FROM Categoria WHERE idCategoria = @idCategoria )
	BEGIN
		RAISERROR ('No Existe una categoria con ese ID', 16, 1);
	END
	UPDATE Categoria SET isEliminado = 1 WHERE idCategoria=@idCategoria;        
END
GO

-- =============================================
-- Author:		LUIS DAVID MERCADO
-- Create date: 17/04/2024
-- Description:	ACTUALIZA UNA CATEGORIA
-- =============================================
CREATE PROCEDURE SP_ACTUALIZAR_CATEGORIA 
	@idCategoria UNIQUEIDENTIFIER,
	@nombreCategoria NVARCHAR(250),
	@descripcionCategoria NVARCHAR(250)
AS
BEGIN

	SET NOCOUNT ON;
	IF NOT EXISTS (  SELECT TOP 1 * FROM Categoria WHERE idCategoria = @idCategoria )
	BEGIN
		RAISERROR ('No Existe una categoria con ese ID', 16, 1);
	END
	UPDATE Categoria SET nombreCategoria =@nombreCategoria , descripcionCategoria=@descripcionCategoria WHERE idCategoria=@idCategoria;
    
END
GO

-- =============================================
-- Author:		LUIS DAVID MERCADO 
-- Create date: 16/04/2024
-- Description:	INSERTA UN PRODUCTO
-- =============================================
CREATE PROCEDURE SP_INSERTAR_PRODUCTO 
	@idCategoria UNIQUEIDENTIFIER,
	@nombreProducto NVARCHAR(250),
	@precioProducto MONEY,
	@unidadesDisponibles INT = 1,
	@idUltimoProductoInsertado UNIQUEIDENTIFIER OUTPUT

AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO Producto (idCategoria,nombreProducto,precioProducto,unidadesDisponibles)
	VALUES (@idCategoria,@nombreProducto,@precioProducto,@unidadesDisponibles)
	
	--SETEO EL ID DEL ULTIMO PEDIDO DEL USUARIO
	SET @idUltimoProductoInsertado = (SELECT TOP 1 idProducto FROM Producto ORDER BY fechaInsercion DESC)

END
GO

-- =============================================
-- Author:		LUIS DAVID MERCADO ORTEGA
-- Create date: 16/04/2024
-- Description:	OBTIENE UN PRODUCTO O UNA LISTA DE PRODUCTOS
-- =============================================
CREATE PROCEDURE SP_LISTAR_PRODUCTO
	@idProducto UNIQUEIDENTIFIER = NULL 
AS
BEGIN
	
	SET NOCOUNT ON;

	IF @idProducto IS NOT NULL
	BEGIN
		SELECT * FROM Producto WHERE idProducto=@idProducto AND isEliminado=0;
	END
	ELSE
	BEGIN
		SELECT * FROM Producto WHERE isEliminado=0;;
	END
    
END
GO

-- =============================================
-- Author:		LUIS DAVID MERCADO ORTEGA
-- Create date: 16/04/2024
-- Description:	ELIMINA UN PRODUCTO
-- =============================================
CREATE PROCEDURE SP_ELIMINAR_PRODUCTO
	@idProducto UNIQUEIDENTIFIER
AS
BEGIN
	
	SET NOCOUNT ON;
	IF NOT EXISTS (  SELECT TOP 1 * FROM Producto WHERE idProducto = @idProducto )
	BEGIN
		RAISERROR ('No Existe un producto con ese ID', 16, 1);
	END
	UPDATE Producto SET isEliminado = 1, isDisponible=0 WHERE idProducto=@idProducto;    
END
GO

-- =============================================
-- Author:		LUIS DAVID MERCADO
-- Create date: 17/04/2024
-- Description:	ACTUALIZA UN PRODUCTO
-- =============================================
CREATE PROCEDURE SP_ACTUALIZAR_PRODUCTO 
	@idProducto UNIQUEIDENTIFIER,
	@idCategoria UNIQUEIDENTIFIER,
	@nombreProducto NVARCHAR(250),
	@precioProducto MONEY,
	@unidadesDisponibles INT,
	@isDisponible BIT
AS
BEGIN

	SET NOCOUNT ON;
	IF NOT EXISTS (  SELECT TOP 1 * FROM Producto WHERE idProducto = @idProducto )
	BEGIN
		RAISERROR ('No Existe un producto con ese ID', 16, 1);
	END
	UPDATE Producto SET idCategoria =@idCategoria, nombreProducto=@nombreProducto, 
	precioProducto=@precioProducto, unidadesDisponibles=@unidadesDisponibles, isDisponible=@isDisponible
	WHERE idProducto=@idProducto;
END
GO

-- =============================================
-- Author:		LUIS DAVID MERCADO ORTEGA
-- Create date: 16/04/2024
-- Description:	ACTUALZIAR UNIDADES DISPONIBLES PRODUCTO
-- =============================================
CREATE PROCEDURE [dbo].[SP_ACTUALIZAR_UNIDADES_DISPONIBLE_PRODUCTO]
	@idProducto UNIQUEIDENTIFIER,
	@unidadesRestar INT
AS
BEGIN
	
	SET NOCOUNT ON;
	IF NOT EXISTS (  SELECT TOP 1 * FROM Producto WHERE idProducto = @idProducto )
	BEGIN
		RAISERROR ('No Existe un producto con ese ID', 16, 1);
	END
	ELSE
	BEGIN
		DECLARE @disponible int = (SELECT TOP 1 unidadesDisponibles FROM Producto WHERE idProducto = @idProducto)
		DECLARE @resultado int = @disponible - @unidadesRestar;
		IF(@resultado<0)
		BEGIN
		   RAISERROR ('No hay suficientes unidades', 16, 1);
		END
		ELSE IF (@resultado=0)
		BEGIN
			UPDATE Producto SET unidadesDisponibles =0 , isDisponible=0 WHERE idProducto=@idProducto; 
		END
		ELSE
		BEGIN
			UPDATE Producto SET unidadesDisponibles =@resultado WHERE idProducto=@idProducto; 
		END
	END	
END
GO

-- =============================================
-- Author:		LUIS DAVID MERCADO 
-- Create date: 16/04/2024
-- Description:	INSERTA UN USUARIO
-- =============================================
CREATE PROCEDURE SP_INSERTAR_USUARIO
	@nombreUsuario NVARCHAR(250),
	@apellidoUsuario NVARCHAR(250),
	@nidUsuario NVARCHAR(15),
	@userName NVARCHAR(250),
    @password NVARCHAR(250),
	@correoUsuario NVARCHAR(250),
	@rolUsuario  NVARCHAR(50) = 'comprador'
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO Usuario (nombreUsuario,apellidoUsuario,nidUsuario,userName,[password],correoUsuario,rolUsuario)
	VALUES(@nombreUsuario,@apellidoUsuario,@nidUsuario,@userName,@password,@correoUsuario,@rolUsuario)

END
GO

-- =============================================
-- Author:		LUIS DAVID MERCADO ORTEGA
-- Create date: 16/04/2024
-- Description:	OBTIENE UN USUARIO O UNA LISTA DE USUARIOS
-- =============================================
CREATE PROCEDURE SP_LISTAR_USUARIO
	@idUsuario UNIQUEIDENTIFIER = NULL 
AS
BEGIN
	
	SET NOCOUNT ON;

	IF @idUsuario IS NOT NULL
	BEGIN
		SELECT * FROM Usuario WHERE idUsuario=@idUsuario AND isEliminado=0;
	END
	ELSE
	BEGIN
		SELECT * FROM Usuario WHERE isEliminado=0;
	END
    
END
GO

-- =============================================
-- Author:		LUIS DAVID MERCADO ORTEGA
-- Create date: 16/04/2024
-- Description:	ELIMINA UN USUARIO
-- =============================================
CREATE PROCEDURE SP_ELIMINAR_USUARIO
	@idUsuario UNIQUEIDENTIFIER
AS
BEGIN
	
	SET NOCOUNT ON;
	IF NOT EXISTS (  SELECT TOP 1 * FROM Usuario WHERE idUsuario = @idUsuario )
	BEGIN
		RAISERROR ('No Existe un Usuario con ese ID', 16, 1);
	END
	UPDATE Usuario SET isEliminado=1 WHERE idUsuario=@idUsuario;    
END
GO

-- =============================================
-- Author:		LUIS DAVID MERCADO ORTEGA
-- Create date: 16/04/2024
-- Description:	OBTIENE UN USUARIO POR CORREO O POR USERNAME
-- =============================================
CREATE  PROCEDURE [dbo].[SP_OBTENER_USUARIO_POR_EMAIL_USERNAME]
	@correoOrUserName Nvarchar(250)
AS
BEGIN
	
	SET NOCOUNT ON;

	IF @correoOrUserName IS NOT NULL
	BEGIN
		SELECT top 1 * FROM Usuario WHERE (correoUsuario=@correoOrUserName OR userName=@correoOrUserName) AND isEliminado=0;
	END
END
GO


-- =============================================
-- Author:		LUIS DAVID MERCADO 
-- Create date: 16/04/2024
-- Description:	INSERTA UN PEDIDO
-- =============================================
CREATE PROCEDURE SP_INSERTAR_PEDIDO
	
	@idUsuario UNIQUEIDENTIFIER,
	@direccionEnvio NVARCHAR(500),
	@idUltimoPedidoUsuario UNIQUEIDENTIFIER OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO Pedido(idUsuario,direccionEnvio)
	VALUES(@idUsuario,@direccionEnvio)

	--SETEO EL ID DEL ULTIMO PEDIDO DEL USUARIO
	SET @idUltimoPedidoUsuario = (SELECT TOP 1 idPedido FROM Pedido WHERE @idUsuario = @idUsuario ORDER BY fechaInsercion DESC)

END
GO

-- =============================================
-- Author:		LUIS DAVID MERCADO 
-- Create date: 16/04/2024
-- Description:	INSERTA UN DETALLE DE PEDIDO
-- =============================================
CREATE PROCEDURE SP_INSERTAR_DETALLE_PEDIDO
	
	@idPedido UNIQUEIDENTIFIER,
	@idProducto UNIQUEIDENTIFIER,
	@unidadesPedidas INT
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO DetallePedido(idPedido,idProducto,unidadesPedidas)
	VALUES(@idPedido,@idProducto,@unidadesPedidas)

END
GO

-- =============================================
-- Author:		LUIS DAVID MERCADO 
-- Create date: 16/04/2024
-- Description:	LISTA TODOS LOS PEDIDOS
-- =============================================
CREATE PROCEDURE SP_LISTAR_DETALLE_PEDIDO
AS
BEGIN
	SET NOCOUNT ON;

	SELECT 
    u.idUsuario AS idUsuario,
	u.nombreUsuario As nombreUsuario,
    p.idPedido AS idPedido, 
    prod.idProducto AS idProducto, 
    prod.nombreProducto AS nombreProducto, 
	Max(prod.precioProducto) As precioProducto,
	Max(dp.unidadesPedidas) As unidadesPedidas,
    SUM(dp.unidadesPedidas * prod.precioProducto) AS subTotalPedido
	FROM 
		Usuario u
	JOIN 
		Pedido p ON u.idUsuario = p.idUsuario
	JOIN 
		DetallePedido dp ON p.idPedido = dp.idPedido
	JOIN 
		Producto prod ON dp.idProducto = prod.idProducto
	GROUP BY 
		u.idUsuario, p.idPedido, prod.idProducto,prod.nombreProducto,nombreUsuario
	ORDER BY 
		u.idUsuario, p.idPedido;

END
GO

-- =============================================
-- Author:		LUIS DAVID MERCADO 
-- Create date: 16/04/2024
-- Description:	LISTA TODOS LOS PEDIDOS POR  ID PEDIO
-- =============================================
CREATE PROCEDURE SP_LISTAR_DETALLE_PEDIDO_POR_ID_PEDIDO
	@idPedido UNIQUEIDENTIFIER 
AS
BEGIN
	SET NOCOUNT ON;

	SELECT 
    u.idUsuario AS idUsuario,
	u.nombreUsuario As nombreUsuario,
    p.idPedido AS idPedido, 
    prod.idProducto AS idProducto, 
    prod.nombreProducto AS nombreProducto, 
	Max(prod.precioProducto) As precioProducto,
	Max(dp.unidadesPedidas) As unidadesPedidas,
    SUM(dp.unidadesPedidas * prod.precioProducto) AS subTotalPedido
	FROM 
		Usuario u
	JOIN 
		Pedido p ON u.idUsuario = p.idUsuario
	JOIN 
		DetallePedido dp ON p.idPedido = dp.idPedido
	JOIN 
		Producto prod ON dp.idProducto = prod.idProducto
	WHERE p.idPedido = @idPedido
	GROUP BY 
		u.idUsuario, p.idPedido, prod.idProducto,prod.nombreProducto,nombreUsuario
	ORDER BY 
		u.idUsuario, p.idPedido;

END
GO


-- =============================================
-- Author:		LUIS DAVID MERCADO 
-- Create date: 16/04/2024
-- Description:	LISTA TODOS LOS PEDIDOS DEL USUARIO
-- =============================================
CREATE PROCEDURE SP_LISTAR_DETALLE_PEDIDO_POR_ID_USUARIO
	@idUsuario UNIQUEIDENTIFIER 
AS
BEGIN
	SET NOCOUNT ON;

	SELECT 
    u.idUsuario AS idUsuario,
	u.nombreUsuario As nombreUsuario,
    p.idPedido AS idPedido, 
    prod.idProducto AS idProducto, 
    prod.nombreProducto AS nombreProducto, 
	Max(prod.precioProducto) As precioProducto,
	Max(dp.unidadesPedidas) As unidadesPedidas,
    SUM(dp.unidadesPedidas * prod.precioProducto) AS subTotalPedido
	FROM 
		Usuario u
	JOIN 
		Pedido p ON u.idUsuario = p.idUsuario
	JOIN 
		DetallePedido dp ON p.idPedido = dp.idPedido
	JOIN 
		Producto prod ON dp.idProducto = prod.idProducto
	WHERE u.idUsuario = @idUsuario
	GROUP BY 
		u.idUsuario, p.idPedido, prod.idProducto,prod.nombreProducto,nombreUsuario
	ORDER BY 
		u.idUsuario, p.idPedido;

END
GO
