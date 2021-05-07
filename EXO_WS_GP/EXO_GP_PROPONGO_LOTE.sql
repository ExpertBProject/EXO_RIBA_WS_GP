CREATE PROCEDURE EXO_GP_PROPONGO_LOTE
(
	IN pAlmacen NVARCHAR(30),
	IN pArticulo NVARCHAR(50),
	IN pCantidadSolicitada DECIMAL(19,6),
	OUT oLote NVARCHAR(36),
  	OUT oCantidad DECIMAL(19,6),
  	OUT oUbicacion NVARCHAR(228)
)
LANGUAGE SQLSCRIPT
AS
-- Return values
BEGIN


	DECLARE CURSOR c_0 FOR
	 	SELECT T1."DistNumber" DistNumber, T0."OnHandQty" OnHandQty, T2."BinCode" BinCode
		FROM "OBBQ" T0
			INNER JOIN "OBTN" T1 ON T0."SnBMDAbs" = T1."AbsEntry" AND T0."ItemCode" = T1."ItemCode"
			INNER JOIN "OBIN" T2 ON T2."AbsEntry" = T0."BinAbs"   
	WHERE T0."ItemCode" = :pArticulo AND T0."WhsCode" = :pAlmacen AND COALESCE(T0."OnHandQty", 0) >= :pCantidadSolicitada AND T1."Status"=0
		AND COALESCE(T2."U_EXO_ESBAHIA",'N') = 'N' AND  COALESCE(T0."OnHandQty", 0) > 0 AND
		CONCAT(CONCAT(T0."ItemCode", '#') , T1."DistNumber") NOT IN (SELECT CONCAT(CONCAT(TLote."ItemCode",'#'), TLote."BatchNum") FROM "OIBT" TLote WHERE TLote."ItemCode" = T0."ItemCode" AND COALESCE(TLote."IsCommited", 0) <> 0)
	ORDER BY    T1."InDate" ASC, T1."SysNumber" ASC,COALESCE(T0."OnHandQty", 0),T1."DistNumber" ASC
	LIMIT 1;
		
	oLote := '';
	oCantidad := 0;
	oUbicacion := '';
	
	FOR c_row_0 AS c_0 DO
		oLote := c_row_0.DistNumber;
		oCantidad := c_row_0.OnHandQty;
		oUbicacion := c_row_0.BinCode;
	END FOR;	
	
END;







