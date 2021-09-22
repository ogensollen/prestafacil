CREATE PROCEDURE [dbo].[uspSolicitudPrestamoActualizar]
	@Id int,
	@FechaRegistro datetime,
	@Cliente varchar(500),
	@MontoSolicitado decimal(7,2)
AS
	UPDATE SolicitudPrestamos
	SET FechaRegistro = @FechaRegistro,
		Cliente = @Cliente,
		MontoSolicitado = @MontoSolicitado
	WHERE Id = @Id;
RETURN 0
