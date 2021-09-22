CREATE TABLE [dbo].[SolicitudPrestamos] (
    [Id]              INT             IDENTITY (1, 1) NOT NULL,
    [FechaRegistro]   DATETIME2 (7)   NOT NULL,
    [Cliente]         NVARCHAR (MAX)  NULL,
    [MontoSolicitado] DECIMAL (18, 2) NOT NULL,
    CONSTRAINT [PK_SolicitudPrestamos] PRIMARY KEY CLUSTERED ([Id] ASC)
);

