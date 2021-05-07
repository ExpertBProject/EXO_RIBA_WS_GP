CREATE PROCEDURE EXO_GP_TRABAJO_LISTA_PICKING_MULTIPLE
(
	IN ClavePicking NVARCHAR(30),
	
	OUT TablaDatos TABLE
	(
	AbsEntry INT,
	PickEntry INT,
	ItemCode NVARCHAR(50),
	ItemName NVARCHAR(100),
	CantidadTotal DECIMAL(19,6),
	Cantidad DECIMAL(19,6),
	Udm NVARCHAR(20),
	BatchNum NVARCHAR(36),
	BinCode NVARCHAR(228),
	Propuesto NVARCHAR(1),
	EsLote NCHAR(1),    
	SePuedeGestionar NVARCHAR(1),
	NumPerMsr DECIMAL(16,6),
	ALTSORTCOD NVARCHAR(50),
	CantidadPick DECIMAL(19,6)
	)
)
LANGUAGE SQLSCRIPT
AS
-- Return values
BEGIN
	
	DECLARE vCantidadAsignada  DECIMAL(19,6) := 0;
	DECLARE vCantidadFaltante  DECIMAL(19,6) := 0;
	DECLARE vLineaPicking  INT := 0;
	
	DECLARE vLotePropuesto  NVARCHAR(36) :='';
  	DECLARE vCantidadPropuesta DECIMAL(19,6) :=0;
  	DECLARE vUbicProp  NVARCHAR(228) :='';
	
	
  --Los de lote asignados  
	DECLARE CURSOR c_0 FOR
	SELECT T0."AbsEntry" AbsEntry, T1."PickEntry" LinPicket, T2."ItemCode" ItemCode, T2."Dscription" ItemName,T1."RelQtty" PickQtty, T4."Quantity" Cantidad, 
			COALESCE(T3."InvntryUom", '') Udm, T4."BatchNum" BatchNum,
			EXO_GP_PROPONGO_UBICACION(T2."WhsCode"  , T2."ItemCode", T4."BatchNum", T4."Quantity", 'V') BinCode,
			'Y' propuesto, 'Y' EsLote, 
			EXO_GP_GESTIONAR_EN_PICK(T0."AbsEntry", t1."PickEntry", T4."Quantity", T4."BatchNum") SePuedeGestionar,COALESCE(T3."NumInSale",1) NumPerMsr,0 Apilable,T1."PickQtty"+T1."RelQtty" CantidadPick
 	FROM "OPKL" T0	   
		INNER JOIN "PKL1" T1 ON T0."AbsEntry" = T1."AbsEntry"
		INNER JOIN "RDR1" T2 ON T1."BaseObject" = T2."ObjType" AND T2."DocEntry" = T1."OrderEntry" AND T2."LineNum" = T1."OrderLine"
		INNER JOIN "OITM" T3 ON T3."ItemCode" = T2."ItemCode"
		INNER JOIN "IBT1" T4 ON T4."BaseType" = T2."ObjType" AND T4."BaseEntry" = T2."DocEntry" AND T4."BaseLinNum" = T2."LineNum"
	WHERE  T0."AbsEntry" = :ClavePicking and COALESCE(T4."Quantity", 0) != 0 and T1."BaseObject"=17;


    --los articulos con lote que no estaban asignados y ya han pasado
	DECLARE CURSOR c_1 FOR
	SELECT T2."AbsEntry" AbsEntry, T2."PickEntry" LinPicket,
	   		T1."ItemCode" ItemCode, T1."Dscription" ItemName, T2."RelQtty" as PickQtty,T1."Quantity" Cantidad, 
	   		COALESCE(TArt."InvntryUom", '') AS Udm, T3."BatchNum" BatchNum,
	   		'' BinCode,'N'  propuesto, 'Y' EsLote, 'Y' SePuedeGestionar ,COALESCE(TArt."NumInSale",1) NumPerMsr,0 Apilable,T2."PickQtty"+T2."RelQtty" CantidadPick
	FROM "OWTR" T0
		INNER JOIN "WTR1" T1 ON T0."DocEntry" = T1."DocEntry"
		INNER JOIN "OITM" TArt on TArt."ItemCode" = T1."ItemCode"
		INNER JOIN "PKL1" T2 ON T2."AbsEntry" = T0."U_EXO_NUMPIC" AND T2."PickEntry" = T0."U_EXO_LINPIC" and T2."BaseObject"=17
		INNER JOIN "IBT1" T3 ON T3."BaseType" = T1."ObjType" AND T3."BaseEntry" = T1."DocEntry" AND T3."BaseLinNum" = T1."LineNum"
	WHERE T0."U_EXO_NUMPIC" = :ClavePicking AND ( T3."BatchNum" NOT IN (SELECT TAsig."BatchNum"  FROM "IBT1" TAsig WHERE TAsig."BaseType" = T2."BaseObject" AND 
		TAsig."BaseEntry" = T2."OrderEntry" AND TAsig."BaseLinNum" = T2."OrderLine" ) );
		
		
    --los articulos sin lote  y ya han pasado
	DECLARE CURSOR c_2 FOR
 	SELECT T2."AbsEntry" AbsEntry, T2."PickEntry" LinPicket,
	   		T1."ItemCode" ItemCode, T1."Dscription" ItemName, T2."RelQtty" as PickQtty,T1."Quantity" Cantidad, 
	   		COALESCE(TArt."InvntryUom", '') AS Udm, '' BatchNum, '' AS BinCode,
			'N' propuesto, 'N' EsLote, 'Y' SePuedeGestionar  ,COALESCE(TArt."NumInSale",1) NumPerMsr ,0 Apilable,T2."PickQtty"+T2."RelQtty" CantidadPick
	FROM "OWTR" T0 
		INNER JOIN "WTR1" T1 ON T0."DocEntry" = T1."DocEntry"
		INNER JOIN "OITM" TArt on TArt."ItemCode" = T1."ItemCode"
		INNER JOIN "PKL1" T2 ON T2."AbsEntry" = T0."U_EXO_NUMPIC" AND T2."PickEntry" = T0."U_EXO_LINPIC" and T2."BaseObject"=17	 								
	WHERE  TART."ManBtchNum" = 'N' AND T0."U_EXO_NUMPIC" = :ClavePicking;
		
	--los que faltan, picking completo, que le iremos restando lo de arriba	
	DECLARE CURSOR cPicking_3 FOR
	SELECT T1."AbsEntry" AbsEntry, T1."PickEntry" PickEntry, T2."ItemCode" ItemCode, T2."Dscription" ItemName, 
			T1."RelQtty"  CantLinea, COALESCE(T2."unitMsr", '') AS Udm, 
			T2."WhsCode" Almacen, T3."ManBtchNum" ConLote,COALESCE(T3."NumInSale",1) NumPerMsr,0 Apilable,T1."PickQtty"+T1."RelQtty" CantidadPick
	FROM "OPKL" T0
		INNER JOIN "PKL1" T1 ON T0."AbsEntry" = T1."AbsEntry" and T1."BaseObject"=17
		INNER JOIN "RDR1" T2 ON T1."BaseObject" = T2."ObjType" AND T2."DocEntry" = T1."OrderEntry" AND T2."LineNum" = T1."OrderLine"
		INNER JOIN "OITM" T3 ON T3."ItemCode" = T2."ItemCode" 
	WHERE T0."AbsEntry" = :ClavePicking;	
		
		
	CREATE LOCAL TEMPORARY TABLE #tmp (
		AbsEntry INT,
		PickEntry INT,
		ItemCode NVARCHAR(50),
		ItemName NVARCHAR(100),
		CantidadTotal DECIMAL(19,6),
		Cantidad DECIMAL(19,6),
		Udm NVARCHAR(20),
		BatchNum NVARCHAR(36),
		BinCode NVARCHAR(228),
		Propuesto NVARCHAR(1),
		EsLote NVARCHAR(1),    
		SePuedeGestionar NVARCHAR(1),
		NumPerMsr DECIMAL(16,6),
		Apilable INT,
		CantidadPick DECIMAL(19,6)
	);
	
	--Agregamos todos los datos a la tabla temporals	
	FOR c_row_0 AS c_0 DO
		INSERT INTO #tmp VALUES (c_row_0.AbsEntry, c_row_0.LinPicket,c_row_0.ItemCode,c_row_0.ItemName,c_row_0.PickQtty,c_row_0.Cantidad,
		c_row_0.Udm,c_row_0.BatchNum,c_row_0.BinCode,c_row_0.propuesto,c_row_0.EsLote,c_row_0.SePuedeGestionar,c_row_0.NumPerMsr,c_row_0.Apilable,c_row_0.CantidadPick);
	END FOR;	
			
	FOR c_row_1 AS c_1 DO
		INSERT INTO #tmp VALUES (c_row_1.AbsEntry, c_row_1.LinPicket,c_row_1.ItemCode,c_row_1.ItemName,c_row_1.PickQtty,c_row_1.Cantidad,
		c_row_1.Udm,c_row_1.BatchNum,c_row_1.BinCode,c_row_1.propuesto,c_row_1.EsLote,c_row_1.SePuedeGestionar,c_row_1.NumPerMsr,c_row_1.Apilable,c_row_1.CantidadPick);
	END FOR;

	FOR c_row_2 AS c_2 DO
		INSERT INTO #tmp VALUES (c_row_2.AbsEntry, c_row_2.LinPicket,c_row_2.ItemCode,c_row_2.ItemName,c_row_2.PickQtty,c_row_2.Cantidad,
		c_row_2.Udm,c_row_2.BatchNum,c_row_2.BinCode,c_row_2.propuesto,c_row_2.EsLote,c_row_2.SePuedeGestionar,c_row_2.NumPerMsr,c_row_2.Apilable,c_row_2.CantidadPick);
	END FOR;

	FOR c_row_3 AS cPicking_3 DO

 		SELECT coalesce(SUM(Cantidad),0) INTO vCantidadAsignada 
 		FROM #tmp  
 		WHERE AbsEntry = :ClavePicking and PickEntry = c_row_3.PickEntry;
 		
 		IF COALESCE(vCantidadAsignada,0) < c_row_3.CantLinea THEN
			vCantidadFaltante :=  c_row_3.CantLinea - vCantidadAsignada;
			
			IF c_row_3.ConLote = 'Y' THEN
				vCantidadFaltante :=  c_row_3.CantLinea - vCantidadAsignada;
				--llamamos a procedimiento propongo lote, que nos devuelve lote,cantidad, ubicacion
				CALL EXO_GP_PROPONGO_LOTE(c_row_3.Almacen,c_row_3.ItemCode,vCantidadFaltante,vLotePropuesto,vCantidadPropuesta,vUbicProp);
			ELSE
				vLotePropuesto :='';
  				vCantidadPropuesta := vCantidadFaltante;
  	 			vUbicProp :='';
				--llamamos a propongo ubicacion para uqe nos devuelva la ubicacion del articulo
				SELECT EXO_GP_PROPONGO_UBICACION(c_row_3.Almacen, c_row_3.ItemCode,'', vCantidadPropuesta, 'V') INTO vUbicProp FROM DUMMY;
			
			END IF;
			
			--hacer el insert para la tabla
			INSERT INTO #tmp VALUES (:ClavePicking, c_row_3.PickEntry,c_row_3.ItemCode,c_row_3.ItemName,vCantidadFaltante,:vCantidadPropuesta,
			c_row_3.Udm,vLotePropuesto,vUbicProp,'Y',c_row_3.ConLote,'N',c_row_3.NumPerMsr,c_row_3.Apilable,c_row_3.CantidadPick);
			
			
 		END IF;
 		
	END FOR;

	TablaDatos = SELECT t0.AbsEntry,t0.PickEntry,t0.ItemCode,t0.ItemName,MAX(t0.CantidadTotal) CantidadTotal,SUM(t0.Cantidad) Cantidad,t0.Udm,t0.BatchNum,t0.BinCode,t0.Propuesto
	,t0.EsLote, t0.SePuedeGestionar,t0.NumPerMsr, coalesce(cast(t1."AltSortCod"  as nvarchar(50)),'999999') "ALTSORTCOD",t0.CantidadPick
	FROM #tmp t0 left join OBIN t1 on t0.BinCode=t1."BinCode"
	GROUP BY t0.AbsEntry,t0.PickEntry,t0.ItemCode,t0.ItemName,t0.Udm,t0.BatchNum,t0.BinCode,t0.Propuesto,t0.EsLote, t0.SePuedeGestionar,t0.NumPerMsr,t0.Apilable,t1."AltSortCod",t0.CantidadPick
	ORDER BY  t0.SePuedeGestionar,t1."AltSortCod",t0.ItemCode,t0.Apilable;

	DROP TABLE #tmp;

END;