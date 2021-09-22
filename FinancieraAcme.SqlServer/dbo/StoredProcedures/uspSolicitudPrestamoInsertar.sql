CREATE PROCEDURE [dbo].[uspSolicitudPrestamoInsertar]
	@Id int output,
	@FechaRegistro datetime,
	@Cliente varchar(500),
	@MontoSolicitado decimal(7,2)
AS
	INSERT SolicitudPrestamos
	VALUES (@FechaRegistro,@Cliente,@MontoSolicitado);
	SELECT @Id = SCOPE_IDENTITY();
RETURN 0
