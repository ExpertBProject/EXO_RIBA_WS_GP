CREATE FUNCTION EXO_GP_Gestionar_En_Pick
(
 	IN pClavePicking int,
    IN pLineaPicking int,
	IN pCantAGestionar DECIMAL(19,6),
	IN pLoteAsignado nvarchar(36)

) RETURNS pResultado NVARCHAR(1)
LANGUAGE SQLSCRIPT
AS
	-- Declare the return variable here
	vCantEnBahia NUMERIC(19,6);
		
BEGIN

 IF :pLoteAsignado != '' THEN
 
 	SELECT  SUM(T1."Quantity") INTO vCantEnBahia FROM "OWTR" T0
	INNER JOIN "WTR1" T1 ON T0."DocEntry" = T1."DocEntry"
	INNER JOIN "IBT1" T2 ON T2."BaseType" = T0."ObjType" AND T2."BaseEntry" = T1."DocEntry" 
	WHERE T0."U_EXO_NUMPIC" = :pClavePicking AND T0."U_EXO_LINPIC" = :pLineaPicking AND T2."BatchNum" = :pLoteAsignado;    
 
 
 ELSE

  	SELECT SUM(T1."Quantity")  INTO vCantEnBahia FROM "OWTR" T0
	INNER JOIN "WTR1" T1 ON T0."DocEntry" = T1."DocEntry"
	WHERE T0."U_EXO_NUMPIC" = :pClavePicking AND T0."U_EXO_LINPIC" = :pLineaPicking;
 
 END IF;

  IF COALESCE(:vCantEnBahia, 0) >= :pCantAGestionar THEN
    pResultado :=  'Y';
  ELSE 
    pResultado :=  'N';
  END IF;

END;







