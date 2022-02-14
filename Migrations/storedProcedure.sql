USE [AgroPrimeAPI]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetWorkersFromDate]    Script Date: 2/14/2022 4:30:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SP_GetWorkersFromDate]
@DateToCompare as Date
as
SELECT * from Worker
WHERE Worker.FechaNacimiento > @DateToCompare
GO
