CREATE PROCEDURE [dbo].[uspSolicitudPrestamoEliminar]
	@Id int
AS
	DELETE SolicitudPrestamos
	WHERE Id = @Id;
