' NOTA: puede usar el comando "Cambiar nombre" del menú contextual para cambiar el nombre de clase "Service1" en el código, en svc y en el archivo de configuración.
' NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione Service1.svc o Service1.svc.vb en el Explorador de soluciones e inicie la depuración.
Imports System.Web.Script.Serialization
Imports EXO_WS_GP
Imports SAPbobsCOM

<ServiceBehavior(ConcurrencyMode:=ConcurrencyMode.Single, InstanceContextMode:=InstanceContextMode.Single)>
Public Class Service1
    Implements IEXO_WS_GP

    Private log As EXO_Log.EXO_Log
    Public AlmacenPrincipal As String = ""
    Public conexionesB1 As Collections.Hashtable

    Private semaforo As System.Threading.Semaphore = New System.Threading.Semaphore(1, 1)

    Public Sub New()

        'Dim log As EXO_Log.EXO_Log

        'Dim conexionesB1 As Collections.Hashtable

        log = New EXO_Log.EXO_Log(System.Configuration.ConfigurationManager.AppSettings("rutaLog"), 10)
        'conexiones = New EXO_Conexiones

        conexionesB1 = New Collections.Hashtable(20)
        AlmacenPrincipal = System.Configuration.ConfigurationManager.AppSettings("AlmacenPrincipal")
        'oCompany = conexiones.conectaDI("manager", "chan7012", "SBOExproZ")

    End Sub

    'Protected Overrides Sub finalize()
    '    For i As Integer = 0 To conexionesB1.Values.Count - 1
    '        desconectaDI(CType(conexionesB1.Values(i), SAPbobsCOM.Company))
    '    Next
    'End Sub

#Region "definiciones"

    Public Function ping() As Boolean Implements IEXO_WS_GP.ping
        log.escribeMensaje("Ping recibido", EXO_Log.EXO_Log.Tipo.informacion)
        Return True
    End Function

    Function LoginUsuario(BaseDatos As String, Usuario As String, Password As String) As String Implements IEXO_WS_GP.LoginUsuario
        Return LoginUsuario(BaseDatos, Usuario, Password, log)
    End Function

    Function BasesDeDatos() As String Implements IEXO_WS_GP.BasesDeDatos
        Return BasesDeDatos(log)
    End Function

    Function UbicacionesDelAlmacen(BaseDatos As String, Usuario As String, Password As String) As String Implements IEXO_WS_GP.UbicacionesDelAlmacen
        Return UbicacionesDelAlmacen(BaseDatos, Usuario, Password, AlmacenPrincipal.ToString, log)
    End Function

    Function RecepcionMaterialesBuscador(BaseDatos As String, Usuario As String, Password As String, NomProv As String, NumContenedor As String, CodEan As String, DescArt As String) As String Implements IEXO_WS_GP.RecepcionMaterialesBuscador
        Return RecepcionMaterialesBuscador(BaseDatos, Usuario, Password, NomProv, NumContenedor, CodEan, DescArt, log)
    End Function

    Function PedidoCompraRegistrarLinea(BaseDatos As String, Usuario As String, Password As String, JSON As String) As String Implements IEXO_WS_GP.PedidoCompraRegistrarLinea
        Return PedidoCompraRegistrarLinea(BaseDatos, Usuario, Password, JSON, log)
    End Function

    Function PedidoCompraRegistrarLinea2(BaseDatos As String, Usuario As String, Password As String, JSON As String) As String Implements IEXO_WS_GP.PedidoCompraRegistrarLinea2
        Return PedidoCompraRegistrarLinea2(BaseDatos, Usuario, Password, JSON, log)
    End Function

    Function PedioCompraResumenFinalizar(BaseDatos As String, Usuario As String, Password As String) As String Implements IEXO_WS_GP.PedioCompraResumenFinalizar
        Return PedioCompraResumenFinalizar(BaseDatos, Usuario, Password, log)
    End Function

    Function PedidoCompraGenerar(BaseDatos As String, Usuario As String, Password As String) As String Implements IEXO_WS_GP.PedidoCompraGenerar
        Return PedidoCompraGenerar(BaseDatos, Usuario, Password, log)
    End Function

    Function ListasPicking(BaseDatos As String, Usuario As String, Password As String) As String Implements IEXO_WS_GP.ListasPicking
        Return ListasPicking(BaseDatos, Usuario, Password, log)
    End Function

    Function UbicacionesDelAlmacenBahias(BaseDatos As String, Usuario As String, Password As String) As String Implements IEXO_WS_GP.UbicacionesDelAlmacenBahias
        Return UbicacionesDelAlmacenBahias(BaseDatos, Usuario, Password, log)
    End Function

    Function DesglosePicking(BaseDatos As String, Usuario As String, Password As String, NumeroPicking As String) As String Implements IEXO_WS_GP.DesglosePicking
        Return DesglosePicking(BaseDatos, Usuario, Password, NumeroPicking, log)
    End Function

    Function OperacionesTraslado(JSON As String, BaseDatos As String, Usuario As String, Password As String) As String Implements IEXO_WS_GP.OperacionesTraslado
        Return OperacionesTraslado(JSON, BaseDatos, Usuario, Password, log)
    End Function

    Function GenerarPicking(JSON As String, BaseDatos As String, Usuario As String, Password As String) As String Implements IEXO_WS_GP.GenerarPicking
        Return GenerarPicking(JSON, BaseDatos, Usuario, Password, log)
    End Function

    Function GenerarPicking2(JSON As String, BaseDatos As String, Usuario As String, Password As String) As String Implements IEXO_WS_GP.GenerarPicking2
        Return GenerarPicking2(JSON, BaseDatos, Usuario, Password, log)
    End Function

    Function ComprobarExisteArticulo(BaseDatos As String, Usuario As String, Password As String, CodEan As String) As String Implements IEXO_WS_GP.ComprobarExisteArticulo
        Return ComprobarExisteArticulo(BaseDatos, Usuario, Password, CodEan, log)
    End Function

    Function ComprobarArticuloSalida(BaseDatos As String, Usuario As String, Password As String, Articulo As String, Lote As String, Cantidad As Double, Ubicacion As String) As String Implements IEXO_WS_GP.ComprobarArticuloSalida
        Return ComprobarArticuloSalida(BaseDatos, Usuario, Password, Articulo, Lote, Cantidad, Ubicacion, log)
    End Function

    Function GenerarDocumentoEntradaManual(JSON As String, BaseDatos As String, Usuario As String, Password As String) As String Implements IEXO_WS_GP.GenerarDocumentoEntradaManual
        Return GenerarDocumentoEntradaSalidaManual(JSON, BaseDatos, Usuario, Password, "Entrada", log)
    End Function

    Function GenerarDocumentoSalidaManual(JSON As String, BaseDatos As String, Usuario As String, Password As String) As String Implements IEXO_WS_GP.GenerarDocumentoSalidaManual
        Return GenerarDocumentoEntradaSalidaManual(JSON, BaseDatos, Usuario, Password, "Salida", log)
    End Function

    Function ListasSolicitudTraslado(BaseDatos As String, Usuario As String, Password As String) As String Implements IEXO_WS_GP.ListasSolicitudTraslado
        Return ListasSolicitudTraslado(BaseDatos, Usuario, Password, log)
    End Function

    Function DesgloseSolicitudesTraslado(BaseDatos As String, Usuario As String, Password As String, NumeroTraslado As String) As String Implements IEXO_WS_GP.DesgloseSolicitudesTraslado
        Return DesgloseSolicitudesTraslado(BaseDatos, Usuario, Password, NumeroTraslado, log)
    End Function

    Function GenerarOperacionTraslado(JSON As String, BaseDatos As String, Usuario As String, Password As String) As String Implements IEXO_WS_GP.GenerarOperacionTraslado
        Return GenerarOperacionTraslado(JSON, BaseDatos, Usuario, Password, log)
    End Function

    Function ComPruebaArticulo(BaseDatos As String, Usuario As String, Password As String, CodArticulo As String, CodEan As String, EsLote As String) As String Implements IEXO_WS_GP.ComPruebaArticulo
        Return ComPruebaArticulo(BaseDatos, Usuario, Password, CodArticulo, CodEan, EsLote, log)
    End Function

    Function GenerarDraftEntrega(BaseDatos As String, Usuario As String, Password As String) As String Implements IEXO_WS_GP.GenerarDraftEntrega
        Return GenerarDraftEntrega(BaseDatos, Usuario, Password, log)
    End Function

    Function ConsultaStock(BaseDatos As String, Usuario As String, Password As String, Filtro As String) As String Implements IEXO_WS_GP.ConsultaStock
        Return ConsultaStock(BaseDatos, Usuario, Password, Filtro, log)
    End Function

    Function DesglosePickingMultiple(BaseDatos As String, Usuario As String, Password As String, NumeroPicking As String) As String Implements IEXO_WS_GP.DesglosePickingMultiple
        Return DesglosePickingMultiple(BaseDatos, Usuario, Password, NumeroPicking, log)
    End Function

    Function ListasPickingMultiple(BaseDatos As String, Usuario As String, Password As String) As String Implements IEXO_WS_GP.ListasPickingMultiple
        Return ListasPickingMultiple(BaseDatos, Usuario, Password, log)
    End Function

    Function ListasRecuentoInventario(BaseDatos As String, Usuario As String, Password As String) As String Implements IEXO_WS_GP.ListasRecuentoInventario
        Return ListasRecuentoInventario(BaseDatos, Usuario, Password, log)
    End Function

    Function DesgloseRecuentoInventario(BaseDatos As String, Usuario As String, Password As String, NumRecuento As String) As String Implements IEXO_WS_GP.DesgloseRecuentoInventario
        Return DesgloseRecuentoInventario(BaseDatos, Usuario, Password, NumRecuento, log)
    End Function

    Function GenerarRecuentoInventario(JSON As String, BaseDatos As String, Usuario As String, Password As String) As String Implements IEXO_WS_GP.GenerarRecuentoInventario
        Return GenerarRecuentoInventario(JSON, BaseDatos, Usuario, Password, log)
    End Function

    Function RecuentoInventarioMarcarFinalizado(BaseDatos As String, Usuario As String, Password As String, NumRecuento As String) As String Implements IEXO_WS_GP.RecuentoInventarioMarcarFinalizado
        Return RecuentoInventarioMarcarFinalizado(BaseDatos, Usuario, Password, NumRecuento, log)
    End Function

    Function CompruebaLote(BaseDatos As String, Usuario As String, Password As String, CodArticulo As String, Lote As String, Ubicacion As String) As String Implements IEXO_WS_GP.CompruebaLote
        Return CompruebaLote(BaseDatos, Usuario, Password, CodArticulo, Lote, Ubicacion, log)
    End Function

    Function OperacionesTrasladoUbicacion(JSON As String, BaseDatos As String, Usuario As String, Password As String) As String Implements IEXO_WS_GP.OperacionesTrasladoUbicacion
        Return OperacionesTrasladoUbicacion(JSON, BaseDatos, Usuario, Password, log)
    End Function

    Function CompruebaUbicacion(BaseDatos As String, Usuario As String, Password As String, CodArticulo As String, Ubicacion As String, EsLote As String) As String Implements IEXO_WS_GP.CompruebaUbicacion
        Return CompruebaUbicacion(BaseDatos, Usuario, Password, CodArticulo, Ubicacion, EsLote, log)
    End Function

#End Region

#Region "Inicializaciones"

    Function zIniClassPedidoCompraRegistrarLinea() As PedidoCompraRegistrarLinea Implements IEXO_WS_GP.zIniClassPedidoCompraRegistrarLinea
        Dim a As PedidoCompraRegistrarLinea = New PedidoCompraRegistrarLinea
        Return a
    End Function

    Function zIniClassTraslados() As Traslado Implements IEXO_WS_GP.zIniClassTraslados
        Dim a As Traslado = New Traslado
        Return a
    End Function

    Function zIniGenerarPicking() As GenerarPicking Implements IEXO_WS_GP.zIniGenerarPicking
        Dim a As GenerarPicking = New GenerarPicking
        Return a
    End Function

    Function zOperacionEntradaSalida() As OperacionEntradaSalida Implements IEXO_WS_GP.zOperacionEntradaSalida
        Dim a As OperacionEntradaSalida = New OperacionEntradaSalida
        Return a
    End Function

    Function zOperacionTraslado() As OperacionTraslado Implements IEXO_WS_GP.zOperacionTraslado
        Dim a As OperacionTraslado = New OperacionTraslado
        Return a
    End Function

    Function zListasRecuentoInventarioCabecera() As ListasRecuentoInventarioCabecera Implements IEXO_WS_GP.zListasRecuentoInventarioCabecera
        Dim a As ListasRecuentoInventarioCabecera = New ListasRecuentoInventarioCabecera
        Return a
    End Function

    Function zListasRecuentoInventarioDetalle() As ListasRecuentoInventarioDetalle Implements IEXO_WS_GP.zListasRecuentoInventarioDetalle
        Dim a As ListasRecuentoInventarioDetalle = New ListasRecuentoInventarioDetalle
        Return a
    End Function



#End Region

#Region "Conectar y Loguin"

    Public Function EstablecerAlmacen(oCompany As SAPbobsCOM.Company)

        Dim SQL As String = "SELECT COALESCE(""U_EXO_INFV"",'01') ""ALMACEN"" FROM ""@EXO_OGEN1"" WHERE ""U_EXO_NOMV""='AlmacenLogOne'"

        Dim rs As SAPbobsCOM.Recordset

        rs = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)

        rs.DoQuery(SQL)

        If rs.RecordCount > 0 Then
            rs.MoveFirst()
            AlmacenPrincipal = rs.Fields.Item("ALMACEN").Value.ToString
        Else
            AlmacenPrincipal = "01"
        End If

        EXO_CleanCOM.CLiberaCOM.liberaCOM(CType(rs, Object))

        Return True

    End Function

    Public Function conectaDI(BaseDatos As String, Usuario As String, Password As String) As SAPbobsCOM.Company

        Dim oComp As SAPbobsCOM.Company
        oComp = New SAPbobsCOM.Company
        Dim ValorUsuarioBase As String = Usuario + "_" + BaseDatos

        'oComp = New EXO_DIAPI.EXO_DIAPI()
        Try
            If conexionesB1.Contains(ValorUsuarioBase) Then
                oComp = CType(conexionesB1(ValorUsuarioBase), SAPbobsCOM.Company)
                Try
                    'If oComp.compañia.InTransaction Then
                    '    oComp.compañia.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack)
                    'End If
                Catch ex As Exception

                    conexionesB1.Remove(ValorUsuarioBase)
                    oComp.Disconnect()
                    oComp = conectaDI(BaseDatos, Usuario, Password)
                End Try
            Else
                Dim servidorSBO As String = System.Configuration.ConfigurationManager.AppSettings("servidorSBO")
                Dim servidorLicencias As String = System.Configuration.ConfigurationManager.AppSettings("servidorLicencias")
                Dim BDSBO As String = System.Configuration.ConfigurationManager.AppSettings("BDSBO")
                Dim usuarioSBO As String = System.Configuration.ConfigurationManager.AppSettings("usuarioSBO")
                Dim pwdSBO As String = System.Configuration.ConfigurationManager.AppSettings("pwdSBO")
                Dim usuarioHANA As String = System.Configuration.ConfigurationManager.AppSettings("usuarioHANA")
                Dim pwdHANA As String = System.Configuration.ConfigurationManager.AppSettings("pwdHANA")

                oComp.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_HANADB
                oComp.UseTrusted = False
                oComp.CompanyDB = BaseDatos
                'oComp.UserName = usuarioSBO
                'oComp.Password = pwdSBO
                oComp.UserName = Usuario
                oComp.Password = Password
                oComp.Server = servidorSBO
                oComp.LicenseServer = servidorLicencias
                oComp.DbUserName = usuarioHANA
                oComp.DbPassword = pwdHANA

                log.escribeMensaje("datos conexion" + servidorSBO + " " + servidorLicencias + " " + BaseDatos + " " + Usuario + " " + Password + " " + usuarioHANA + " " + pwdHANA)

                If oComp.Connect() <> 0 Then
                    Dim algo As String = oComp.GetLastErrorDescription()
                    log.escribeMensaje("error conectando: " + algo)
                    Try
                        conexionesB1.Remove(ValorUsuarioBase)
                    Catch ex As Exception

                    End Try

                Else
                    conexionesB1.Add(ValorUsuarioBase, oComp)
                End If


            End If

        Finally

        End Try
        Return oComp

    End Function

    Public Function LoginUsuario(BaseDatos As String, Usuario As String, Password As String, log As EXO_Log.EXO_Log) As String

        Dim jRes As Resultado = New Resultado
        Dim res As String = ""
        'Dim oCompany As SAPbobsCOM.Company

        'oCompany = conexiones.conectaDI(BaseDatos,Usuario, Password, baseDeDatos)

        'conexiones.ConnectSQLServer(baseDeDatos)
        Dim oCompany As SAPbobsCOM.Company
        oCompany = New SAPbobsCOM.Company
        oCompany = conectaDI(BaseDatos, Usuario, Password)
        EstablecerAlmacen(oCompany)

        'Dim conexiones As EXO_DIAPI.EXO_DIAPI
        'conexiones = New EXO_DIAPI.EXO_DIAPI(oCompany)
        Dim rs As SAPbobsCOM.Recordset
        rs = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)

        Try

            'hacer consulta al sql y y rellenar el listado
            'CONSULTA EN HANA
            Dim query As String = "SELECT ""USER_CODE"" FROM ""OUSR"" WHERE ""USER_CODE""='" + Usuario.Replace("'", "") + "' and ""U_EXO_PASS""='" + Password.Replace("'", "") + "' and LENGTH(""U_EXO_PASS"")>0 "

            'Dim query As String = "SELECT ""firstName"" FROM ""OHEM"" "

            'Dim tabla As DataTable = New System.Data.DataTable()
            'tabla = conexiones.SQL.sqlComoDataTable(query)

            'If tabla.Rows.Count > 0 Then

            '    For Each fila As DataRow In tabla.Rows

            '    Next

            'End If

            'recorro y voy rellenando listado 




            rs.DoQuery(query)
            'Dim tabla As DataTable = New System.Data.DataTable()

            If rs.RecordCount > 0 Then
                ''tabla = conexiones.SQL.sqlComoDataTable(query)

                'If tabla.Rows.Count > 0 Then

                jRes.Resultado = "Ok"

            Else
                jRes.Resultado = "Usuario o contraseña incorrectos"
            End If

        Catch ex As Exception
            log.escribeMensaje(ex.Message, EXO_Log.EXO_Log.Tipo.error)
            jRes.Resultado = ex.Message
        Finally
            EXO_CleanCOM.CLiberaCOM.liberaCOM(CType(rs, Object))
            '   EXO_CleanCOM.CLiberaCOM.liberaCOM(CType(oCompany, Object))
        End Try

        'liberaCompañia(compañia)
        'conexiones.DisconnectSQLServer()
        ' conexiones.liberaCompañia(oCompany)


        Dim js As New JavaScriptSerializer()
        res = js.Serialize(jRes)

        Return res

    End Function

    Public Function BasesDeDatos(log As EXO_Log.EXO_Log) As String

        Dim listado As List(Of BasesDatos) = New List(Of BasesDatos)
        Dim res As String = ""
        Dim oAlm As BasesDatos = New BasesDatos

        Dim Esprimero As Boolean = True

        Try
            log.escribeMensaje("accedo a base datos")
            'hacer consulta al sql y y rellenar el listado

            Dim schemas As String = System.Configuration.ConfigurationManager.AppSettings("EmpresasGP")

            Dim SplitSchemas() As String = Split(schemas, ";")

            For i As Integer = 0 To SplitSchemas.Length - 1
                Dim SplitSub() As String = Split(SplitSchemas(i), "#")

                oAlm = New BasesDatos
                oAlm.Resultado = "Ok"
                oAlm.Almacen = SplitSub(1)
                oAlm.BD = SplitSub(0)
                listado.Add(oAlm)

            Next

        Catch ex As Exception
            log.escribeMensaje(ex.Message, EXO_Log.EXO_Log.Tipo.error)
            oAlm.Resultado = "Error"
            listado.Add(oAlm)
        End Try

        'liberaCompañia(compañia)


        Dim js As New JavaScriptSerializer()
        res = js.Serialize(listado)

        Return res

    End Function

#End Region

#Region "Compras"

    Private Function UbicacionesDelAlmacen(BaseDatos As String, Usuario As String, Password As String, Almacen As String, log As EXO_Log.EXO_Log) As String
        Dim listado As List(Of Ubicaciones) = New List(Of Ubicaciones)
        Dim res As String = ""
        Dim oUbi As Ubicaciones = New Ubicaciones


        Dim oCompany As SAPbobsCOM.Company
        oCompany = New SAPbobsCOM.Company
        oCompany = conectaDI(BaseDatos, Usuario, Password)
        EstablecerAlmacen(oCompany)
        Dim rs As SAPbobsCOM.Recordset

        rs = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)

        Try

            'hacer consulta al sql y y rellenar el listado

            Dim query As String = "SELECT ""BinCode"" FROM ""OBIN"" WHERE ""WhsCode""='" + Almacen + "'"
            'recorro y voy rellenando listado 


            rs.DoQuery(query)

            If rs.RecordCount > 0 Then

                rs.MoveFirst()

                While (Not rs.EoF)

                    oUbi = New Ubicaciones

                    oUbi.Resultado = "Ok"
                    oUbi.Codigo = rs.Fields.Item("BinCode").Value.ToString

                    listado.Add(oUbi)

                    rs.MoveNext()
                End While

            Else

                oUbi.Resultado = "Error no hay datos coincidentes"
                listado.Add(oUbi)

            End If

        Catch ex As Exception
            log.escribeMensaje(ex.Message, EXO_Log.EXO_Log.Tipo.error)
            oUbi.Resultado = "Error " + ex.Message
            listado.Add(oUbi)
        Finally
            EXO_CleanCOM.CLiberaCOM.liberaCOM(CType(rs, Object))
            '   EXO_CleanCOM.CLiberaCOM.liberaCOM(CType(oCompany, Object))
        End Try

        'liberaCompañia(compañia)

        Dim js As New JavaScriptSerializer()
        res = js.Serialize(listado)

        Return res

    End Function

    Public Function RecepcionMaterialesBuscador(BaseDatos As String, Usuario As String, Password As String, NomProv As String, NumContenedor As String, CodEan As String, DescArt As String, log As EXO_Log.EXO_Log) As String

        Dim listado As List(Of PedidosCompra) = New List(Of PedidosCompra)
        Dim res As String = ""
        Dim oPed As PedidosCompra = New PedidosCompra

        Dim Esprimero As Boolean = True

        'conexiones.ConnectSQLServer(BaseDatos)
        Dim oCompany As SAPbobsCOM.Company
        oCompany = New SAPbobsCOM.Company
        oCompany = conectaDI(BaseDatos, Usuario, Password)
        EstablecerAlmacen(oCompany)
        Dim rs As SAPbobsCOM.Recordset

        rs = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)

        Try
            Dim CodEanConversion As String = ""

            If CodEan = "" Then
            Else
                CodEanConversion = CodEan
                'If Len(CodEan) < 15 Then
                '    CodEanConversion = CodEan
                'ElseIf Len(CodEan) = 15 Then
                '    'If Len(CodEan) < 16 Then
                '    CodEanConversion = CodEan.Substring(2, 13)
                'Else

                '    CodEanConversion = CodEan.Substring(2, 14)
                '    'si es ean 128 hay que desglosar el código y luego generar la consulta
                'End If
            End If

            'CONSULTA EN HANA
            Dim query As String = " SELECT * FROM ( SELECT T0.""DocEntry"", T0.""DocNum"",T1.""LineNum"",T0.""CardCode"",T0.""CardName"",T1.""ItemCode"",T2.""ItemName"",max(T1.""OpenQty"")- sum(COALESCE(T3.""U_EXO_CANT"",0)) as ""OpenQty"",  " +
                                " Case WHEN COALESCE(T2.""ManBtchNum"",'N') = 'N' THEN 'N' ELSE 'Y' END as ""EsLote"", " +
                                " T2.""BHeight1"" As ""Alto"", T2.""BWidth1"" As ""Ancho"",T2.""BLength1"" As ""Largo"",T2.""BWeight1"" As ""Peso"",T1.""unitMsr"" " +
                                " , COALESCE(T4.""BcdCode"",'') as ""Ean14"" " +
                                " FROM ""OPOR"" T0 INNER JOIN ""POR1"" T1 ON T0.""DocEntry""=T1.""DocEntry"" " +
                                " INNER Join ""OITM"" T2 ON T1.""ItemCode""=T2.""ItemCode"" " +
                                " LEFT JOIN ""@EXO_GP_PEDCOM"" T3 ON T1.""DocEntry""=T3.""U_EXO_DOCE"" and T1.""LineNum""=T3.""U_EXO_LINENUM"" " +
                                " LEFT JOIN ""OBCD"" T4 ON T2.""PUoMEntry""=T4.""UomEntry"" AND T2.""ItemCode""=T4.""ItemCode"" " +
                                " WHERE 1 = 1 "

            'MANU -> FALTA LEFT JOIN A LA OSPP Y SPP1 PARA EL EAN14

            If NomProv <> "" Then
                query = query + " and UPPER(T0.""CardName"") like '%" + NomProv.ToUpper() + "%' "
            End If

            If NumContenedor <> "" Then
                query = query + " and T1.""U_EXO_CODEOCONTE"" = '" + NumContenedor + "' "
            End If

            If DescArt <> "" Then
                query = query + " and UPPER(T2.""ItemName"") like '%" + DescArt.ToUpper() + "%' "
            End If

            If CodEan <> "" Then
                query = query + " and ((T2.""CodeBars"" = '" + CodEanConversion + "' AND T2.""UgpEntry""='-1') OR COALESCE(T4.""BcdCode"",'')='" + CodEanConversion + "') "
            End If

            query = query + " group by T0.""DocEntry"", T0.""DocNum"",T1.""LineNum"",T0.""CardCode"",T0.""CardName"",T1.""ItemCode"",T2.""ItemName"",T2.""ManBtchNum"", " +
                     " T2.""BHeight1"", T2.""BWidth1"",T2.""BLength1"",T2.""BWeight1"",T1.""unitMsr"" ,T4.""BcdCode"" " +
                     " ORDER BY T0.""DocEntry"", T1.""LineNum"" " +
                    " ) as A0 " +
                    " WHERE A0.""OpenQty"" > 0 "


            rs.DoQuery(query)

            If rs.RecordCount > 0 Then

                rs.MoveFirst()

                While (Not rs.EoF)

                    oPed = New PedidosCompra

                    oPed.Resultado = "Ok"
                    oPed.NumInterno = rs.Fields.Item("DocEntry").Value.ToString
                    oPed.NumDocumento = rs.Fields.Item("DocNum").Value.ToString
                    oPed.NumLinea = rs.Fields.Item("LineNum").Value.ToString
                    oPed.Proveedor = rs.Fields.Item("CardCode").Value.ToString
                    oPed.ProveedorNombre = rs.Fields.Item("CardName").Value.ToString
                    oPed.Codigo = rs.Fields.Item("ItemCode").Value.ToString
                    oPed.Descripcion = rs.Fields.Item("ItemName").Value.ToString
                    oPed.Cantidad = rs.Fields.Item("OpenQty").Value.ToString
                    oPed.EsLote = rs.Fields.Item("EsLote").Value.ToString
                    oPed.Largo = rs.Fields.Item("Largo").Value.ToString
                    oPed.Peso = rs.Fields.Item("Peso").Value.ToString
                    oPed.Alto = rs.Fields.Item("Alto").Value.ToString
                    oPed.Ancho = rs.Fields.Item("Ancho").Value.ToString
                    oPed.UnidadMedida = rs.Fields.Item("unitMsr").Value.ToString

                    listado.Add(oPed)

                    rs.MoveNext()
                End While

            Else

                oPed.Resultado = "Error no hay datos coincidentes"
                listado.Add(oPed)

            End If



        Catch ex As Exception
            log.escribeMensaje(ex.Message, EXO_Log.EXO_Log.Tipo.error)
            oPed.Resultado = "Error: " + ex.Message
            listado.Add(oPed)
        Finally
            EXO_CleanCOM.CLiberaCOM.liberaCOM(CType(rs, Object))
            '   EXO_CleanCOM.CLiberaCOM.liberaCOM(CType(oCompany, Object))
        End Try

        'liberaCompañia(compañia)
        'conexiones.DisconnectSQLServer()

        Dim js As New JavaScriptSerializer()
        res = js.Serialize(listado)



        Return res

    End Function

    Public Function PedidoCompraRegistrarLinea2(BaseDatos As String, Usuario As String, Password As String, JSON As String, log As EXO_Log.EXO_Log) As String

        Dim ListOp As PedidoCompraRegistrarLinea = New PedidoCompraRegistrarLinea
        Dim jRes As Resultado = New Resultado
        Dim res As String = ""
        Dim bPrimero As Boolean = True
        Dim js As New JavaScriptSerializer()
        ListOp = js.Deserialize(Of PedidoCompraRegistrarLinea)(JSON)

        Dim oCompany As SAPbobsCOM.Company
        oCompany = New SAPbobsCOM.Company
        oCompany = conectaDI(BaseDatos, Usuario, Password)
        EstablecerAlmacen(oCompany)
        Dim rs As SAPbobsCOM.Recordset = Nothing
        Dim oOITM As SAPbobsCOM.Items = Nothing

        Try

            Dim query As String = ""


            'COMPRUEBO PEDIDOS
            If ListOp.CantidadSeleccionada > ListOp.CantidadReal Then
                'comprobar que no hay mas pedidos o mas lineas abiertas
                query = "SELECT COUNT(CONCAT(T1.""DocEntry"",T1.""LineNum"")) AS ""TotalPedidos"" FROM ""OPOR"" T0 INNER JOIN ""POR1"" T1 On T0.""DocEntry""=T1.""DocEntry"" " +
                        "WHERE T1.""ItemCode"" = '" + ListOp.Codigo + "' and T0.""CardCode""='" + ListOp.Proveedor + "' and T1.""LineStatus""='O'"

                rs = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)

                rs.DoQuery(query)

                If rs.RecordCount > 0 Then

                    rs.MoveFirst()

                    If rs.Fields.Item("TotalPedidos").Value > 1 Then

                        jRes.Resultado = "Hay mas lineas abiertas de este artículo. Imposible superar la cantidad permitida."
                        res = js.Serialize(jRes)
                        Return res
                    End If
                End If
            End If

            'ACTUALIZO DATOS ARTICULO
            query = "select CASE WHEN  COALESCE(""BHeight1"",0)=0 OR  COALESCE(""BWidth1"",0)=0 OR COALESCE(""BLength1"",0)=0 OR COALESCE(""BWeight1"",0)=0 THEN 'Y' ELSE 'N' END AS ""Actualizar"",""UgpEntry"" " +
                " FROM ""OITM"" " +
                " WHERE ""ItemCode""='" + ListOp.Codigo + "' "

            rs = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)

            rs.DoQuery(query)

            If rs.RecordCount > 0 Then
                If rs.Fields.Item("Actualizar").Value = "Y" Then

                    'MIRAR COMO MONTAR ESTO POR CULPA DE LOS GRUPOS DE MEDIDAS


                    oOITM = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oItems)

                    oOITM.GetByKey(ListOp.Codigo)

                    'oOITM.PurchaseUnitWeight = 6
                    'oOITM.PurchaseWeightUnit = 3
                    Dim DefaultPurchaseUomEntry As Integer = oOITM.DefaultPurchasingUoMEntry
                    If rs.Fields.Item("UgpEntry").Value.ToString <> -1 Then

                        For i = 0 To oOITM.UnitOfMeasurements.Count - 1
                            oOITM.UnitOfMeasurements.SetCurrentLine(i)

                            If oOITM.UnitOfMeasurements.UoMType = ItemUoMTypeEnum.iutPurchasing And oOITM.UnitOfMeasurements.UoMEntry = DefaultPurchaseUomEntry Then

                                oOITM.UnitOfMeasurements.Weight1 = ListOp.Peso
                                oOITM.UnitOfMeasurements.Weight1Unit = 3

                                oOITM.UnitOfMeasurements.Width1 = ListOp.Ancho
                                oOITM.UnitOfMeasurements.Width1Unit = 2

                                oOITM.UnitOfMeasurements.Height1 = ListOp.Alto
                                oOITM.UnitOfMeasurements.Height1Unit = 2

                                oOITM.UnitOfMeasurements.Length1 = ListOp.Largo
                                oOITM.UnitOfMeasurements.Length1Unit = 2

                                oOITM.UnitOfMeasurements.Volume = ListOp.Largo * ListOp.Alto * ListOp.Ancho
                                oOITM.UnitOfMeasurements.VolumeUnit = 2

                            End If
                        Next
                    Else

                        oOITM.PurchaseUnitWeight = ListOp.Peso
                        oOITM.PurchaseWeightUnit = 3
                        oOITM.PurchaseUnitWidth = ListOp.Ancho
                        oOITM.PurchaseWidthUnit = 2
                        oOITM.PurchaseUnitHeight = ListOp.Alto
                        oOITM.PurchaseHeightUnit = 2
                        oOITM.PurchaseUnitLength = ListOp.Largo
                        oOITM.PurchaseLengthUnit = 2

                    End If

                    If oOITM.Update() <> 0 Then
                        Dim err As String = "error" + oCompany.GetLastErrorDescription
                    End If

                    '    Dim Volumen As Double = ListOp.Alto * ListOp.Largo * ListOp.Ancho
                    '    'actualizar oitm
                    '    query = "UPDATE ""OITM"" T2 " +
                    '" SET T2.""BHeight1""=" + ListOp.Alto.ToString + ", T2.""BHght1Unit""=2, " +
                    '    " T2.""BWidth1""=" + ListOp.Ancho.ToString + ", T2.""BWdth1Unit""=2, " +
                    '    " T2.""BLength1""=" + ListOp.Largo.ToString + ", T2.""BLen1Unit""=2, " +
                    '    " T2.""BWeight1""=" + ListOp.Peso.ToString + ",  T2.""BWght1Unit""=3, " +
                    '    " T2.""BVolume""=" + Volumen.ToString + ",T2.""BVolUnit""=2 " +
                    '" WHERE T2.""ItemCode""='" + ListOp.Codigo + "' "


                    '    'actualizar itm12, habría que multiplicar las medidas para obtener el volumen
                    '    If rs.Fields.Item("UgpEntry").Value.ToString <> -1 Then

                    '        query = "UPDATE T2 " +
                    '    " SET T2.""Height1""=" + ListOp.Alto.ToString + ",T2.""Hght1Unit""=2," +
                    '    " T2.""Width1""=" + ListOp.Ancho.ToString + ",T2.""Wdth1Unit""=2, " +
                    '    " T2.""Length1""=" + ListOp.Largo.ToString + ", T2.""Len1Unit""=2, " +
                    '    " T2.""Weight1""=" + ListOp.Peso.ToString + ", T2.""Wght1Unit""=3, " +
                    '    " T2.""Volume""=" + Volumen.ToString + ",T2.""VolUnit""=2 " +
                    '" from ""OITM"" T0 INNER JOIN ""OUOM"" T1 ON T0.""BuyUnitMsr""=T1.""UomCode"" " +
                    '" INNER Join ""ITM12"" T2 ON T0.""ItemCode""=T2.""ItemCode"" And T1.""UomEntry""=T2.""UomEntry"" " +
                    '" WHERE T0.""ItemCode""='" + ListOp.Codigo + "' AND T2.""UomType""='P' "



                End If
            Else
                jRes.Resultado = "Error. El articulo no existe"
                res = js.Serialize(jRes)
                Return res
            End If

            'INSERTO TABLA TEMPORAL
            query = "SELECT MAX(""Code"")+1 AS ""Code"" FROM ""@EXO_GP_PEDCOM"" "

            rs = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)

            rs.DoQuery(query)
            Dim sCode As String = ""
            If rs.RecordCount > 0 Then
                sCode = Right("000000000" + rs.Fields.Item("Code").Value.ToString, 9)
            Else
                sCode = "000000001"
            End If

            query = "INSERT INTO ""@EXO_GP_PEDCOM"" VALUES ('" + sCode + "', '" + sCode + "','" + Usuario + "','" + ListOp.NumInterno + "','" + ListOp.NumLinea + "' " +
            " , '" + ListOp.CantidadSeleccionada + "','" + ListOp.Lote + "','" + ListOp.Ubicacion + "')"
            rs.DoQuery(query)

            jRes.Resultado = "OK"

        Catch ex As Exception
            log.escribeMensaje(ex.Message, EXO_Log.EXO_Log.Tipo.error)
            jRes.Resultado = "Error " + ex.Message
        Finally
            EXO_CleanCOM.CLiberaCOM.liberaCOM(CType(rs, Object))
            '   EXO_CleanCOM.CLiberaCOM.liberaCOM(CType(oCompany, Object))
            EXO_CleanCOM.CLiberaCOM.liberaCOM(CType(oOITM, Object))

        End Try


        res = js.Serialize(jRes)

        Return res

    End Function

    Public Function PedioCompraResumenFinalizar(BaseDatos As String, Usuario As String, Password As String, log As EXO_Log.EXO_Log) As String

        Dim listado As List(Of PedidoCompraResumenFinalizar) = New List(Of PedidoCompraResumenFinalizar)
        Dim res As String = ""
        Dim oPed As PedidoCompraResumenFinalizar = New PedidoCompraResumenFinalizar


        Dim oCompany As SAPbobsCOM.Company
        oCompany = New SAPbobsCOM.Company
        oCompany = conectaDI(BaseDatos, Usuario, Password)
        EstablecerAlmacen(oCompany)
        Dim rs As SAPbobsCOM.Recordset = Nothing

        Try

            'hacer consulta al sql y y rellenar el listado

            Dim query As String = " SELECT SUM(T0.""U_EXO_CANT"") as ""U_EXO_CANT"", COALESCE(T0.""U_EXO_LOTE"",'') AS ""U_EXO_LOTE"",T0.""U_EXO_UBICA"",T1.""CardName"",T4.""ItemCode"",T4.""ItemName"",T3.""UomCode"" " +
                         " FROM ""@EXO_GP_PEDCOM"" T0 INNER JOIN ""OPOR"" T1 On T0.""U_EXO_DOCE""=T1.""DocEntry""  " +
                         " INNER Join ""POR1"" T3 On T0.""U_EXO_DOCE""=T3.""DocEntry"" And T0.""U_EXO_LINENUM""=T3.""LineNum""  " +
                         " INNER Join ""OITM"" T4 ON T3.""ItemCode""=T4.""ItemCode""  " +
                         " WHERE ""U_EXO_USUARIO"" ='" + Usuario + "'  " +
                         " Group by  T0.""U_EXO_CANT"", T0.""U_EXO_LOTE"", T0.""U_EXO_UBICA"", T1.""CardName"", T4.""ItemCode"", T4.""ItemName"", T3.""UomCode"" "




            rs = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)

            rs.DoQuery(query)

            If rs.RecordCount > 0 Then

                rs.MoveFirst()

                While (Not rs.EoF)

                    oPed = New PedidoCompraResumenFinalizar

                    oPed.Resultado = "Ok"
                    oPed.Codigo = rs.Fields.Item("ItemCode").Value.ToString
                    oPed.Proveedor = rs.Fields.Item("CardName").Value.ToString
                    oPed.Descripcion = rs.Fields.Item("ItemName").Value.ToString
                    oPed.Cantidad = CType(rs.Fields.Item("U_EXO_CANT").Value.ToString, Double)
                    oPed.Lote = rs.Fields.Item("U_EXO_LOTE").Value.ToString
                    oPed.Ubicacion = rs.Fields.Item("U_EXO_UBICA").Value.ToString
                    oPed.UnidadMedida = rs.Fields.Item("UomCode").Value.ToString

                    listado.Add(oPed)

                    rs.MoveNext()

                End While

            Else

                oPed.Resultado = "Error no hay nada para finalizar"
                listado.Add(oPed)

            End If

        Catch ex As Exception
            log.escribeMensaje(ex.Message, EXO_Log.EXO_Log.Tipo.error)
            oPed.Resultado = "Error " + ex.Message
            listado.Add(oPed)
        Finally
            EXO_CleanCOM.CLiberaCOM.liberaCOM(CType(rs, Object))
            '   EXO_CleanCOM.CLiberaCOM.liberaCOM(CType(oCompany, Object))
        End Try

        'liberaCompañia(compañia)

        Dim js As New JavaScriptSerializer()
        res = js.Serialize(listado)

        Return res

    End Function

    Public Function PedidoCompraGenerar(BaseDatos As String, Usuario As String, Password As String, log As EXO_Log.EXO_Log) As String

        Dim jRes As Resultado = New Resultado
        Dim res As String = ""
        Dim bPrimero As Boolean = True
        Dim js As New JavaScriptSerializer()
        Dim query As String = ""
        Dim Subquery As String = ""
        Dim rs As SAPbobsCOM.Recordset = Nothing
        Dim rsLin As SAPbobsCOM.Recordset = Nothing
        Dim rsPorte As SAPbobsCOM.Recordset = Nothing
        Dim rscONS As SAPbobsCOM.Recordset = Nothing

        Dim oCompany As SAPbobsCOM.Company
        oCompany = New SAPbobsCOM.Company
        oCompany = conectaDI(BaseDatos, Usuario, Password)
        EstablecerAlmacen(oCompany)

        Dim oDoc As SAPbobsCOM.Documents = Nothing

        jRes.Resultado = "OK"

        Try
            'BUSCAR LAS LINEAS QUE SEAN DEL USUARIO

            query = "Select  T1.""CardCode"",T0.""U_EXO_DOCE"",COALESCE(T0.""U_EXO_LINENUM"",0) ""U_EXO_LINENUM"" FROM ""@EXO_GP_PEDCOM"" T0 INNER JOIN ""OPOR"" T1 On T0.""U_EXO_DOCE""=T1.""DocEntry"" " +
                    " WHERE ""U_EXO_USUARIO""='" + Usuario + "' " +
                    " GROUP BY T1.""CardCode"",T0.""U_EXO_DOCE"",T0.""U_EXO_LINENUM"" " +
                    " ORDER BY T1.""CardCode"",T0.""U_EXO_DOCE"",T0.""U_EXO_LINENUM"" "
            rs = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
            rs.DoQuery(query)

            If rs.RecordCount > 0 Then

                rs.MoveFirst()

                oDoc = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oPurchaseDeliveryNotes)

                Dim EsNuevo As Boolean = True
                Dim clienteActual As String = ""


                Dim esPrimeraLinea As Boolean = True
                'transaction

                If oCompany.InTransaction = True Then
                    oCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack)
                End If

                oCompany.StartTransaction()

                While (Not rs.EoF)

                    'Generar documentos de compra, tener en cuenta ubicaciones y lotes
                    If clienteActual = "" Then
                        EsNuevo = True
                        esPrimeraLinea = True
                    ElseIf clienteActual <> rs.Fields.Item("CardCode").Value.ToString() Then 'si es distinto cliente al anterior o distinta direccion
                        'generamos el albaran

                        'ARTICULOS NO INVENTARIABLES
                        Subquery = "Select DISTINCT( T0.""U_EXO_DOCE"") FROM ""@EXO_GP_PEDCOM"" T0 INNER JOIN ""OPOR"" T1 On T0.""U_EXO_DOCE""=T1.""DocEntry"" " +
                        " WHERE ""U_EXO_USUARIO""='" + Usuario + "' and T1.""CardCode""='" + clienteActual + "' "

                        query = "Select  T1.""DocEntry"",T1.""LineNum"" " +
                        " from ""POR1"" T1 inner join ""OITM"" T2 ON T1.""ItemCode""=T2.""ItemCode"" " +
                        " WHERE coalesce(T2.""InvntItem"",'N')='N' and T1.""LineStatus""='O' and T1.""DocEntry"" in  ( " + Subquery + ")"

                        rscONS = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                        rscONS.DoQuery(query)

                        If rscONS.RecordCount > 0 Then
                            While Not (rscONS.EoF)

                                If esPrimeraLinea = False Then
                                    oDoc.Lines.Add()
                                Else
                                    esPrimeraLinea = False
                                End If

                                oDoc.Lines.BaseEntry = CType(rscONS.Fields.Item("DocEntry").Value.ToString(), Integer)
                                oDoc.Lines.BaseLine = CType(rscONS.Fields.Item("LineNum").Value.ToString(), Integer)
                                oDoc.Lines.BaseType = 22

                                rscONS.MoveNext()
                            End While
                        End If

                        ''COMPROBAMOS LOS PORTES DE LOS DOCUMENTOS PROCESADOS
                        Subquery = "Select DISTINCT( T0.""U_EXO_DOCE"") FROM ""@EXO_GP_PEDCOM"" T0 INNER JOIN ""OPOR"" T1 On T0.""U_EXO_DOCE""=T1.""DocEntry"" " +
                        " WHERE ""U_EXO_USUARIO""='" + Usuario + "' and T1.""CardCode""='" + clienteActual + "'"

                        'añadimos los portes
                        query = "Select T10.""DocEntry"",T10.""LineNum"",T10.""LineTotal"" from POR3 T10 WHERE ""Status""='O' and T10.""DocEntry"" in ( " + Subquery + ") "

                        rsPorte = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                        rsPorte.DoQuery(query)

                        'Dim PrimerPorte As Boolean = False

                        While (Not rsPorte.EoF)

                            'If PrimerPorte = True Then

                            'Else
                            '    PrimerPorte = True
                            'End If
                            oDoc.Expenses.LineTotal = CType(rsPorte.Fields.Item(2).Value.ToString(), Double)
                            oDoc.Expenses.BaseDocEntry = CType(rsPorte.Fields.Item(0).Value.ToString(), Integer)
                            oDoc.Expenses.BaseDocLine = CType(rsPorte.Fields.Item(1).Value.ToString(), Integer)
                            oDoc.Expenses.BaseDocType = 22
                            oDoc.Expenses.Add()

                            rsPorte.MoveNext()

                        End While


                        If oDoc.Add() = 0 Then
                            'si no se puede por el objeto, hacer un update
                        Else
                            jRes.Resultado = oCompany.GetLastErrorDescription

                            If oCompany.InTransaction = True Then
                                oCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack)
                            End If
                            ' conexiones.liberaCompañia(oCompany)
                            res = js.Serialize(jRes)
                            Return res
                        End If

                        esPrimeraLinea = True
                        EsNuevo = True
                    Else
                        EsNuevo = False
                    End If

                    'si es nuevo rellenamos cabecera
                    If EsNuevo = True Then

                        oDoc = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oPurchaseDeliveryNotes)

                        clienteActual = rs.Fields.Item("Cardcode").Value.ToString()

                        oDoc.CardCode = rs.Fields.Item("Cardcode").Value.ToString()

                    End If

                    If esPrimeraLinea = False Then
                        oDoc.Lines.Add()
                    Else
                        esPrimeraLinea = False
                    End If

                    Dim baselinenumber As Integer = 0
                    Dim noesloteprimero As Boolean = True
                    Dim cantidadTotal As Double = 0
                    Dim UbiActual As String = ""

                    'MINI BUCLE PARA LAS LINEAS
                    query = "SELECT SUM(T0.""U_EXO_CANT"") as ""U_EXO_CANT"",T0.""U_EXO_LOTE"",T2.""AbsEntry"", MIN(""NumPerMsr"")*SUM(T0.""U_EXO_CANT"") as ""TotalBin"",  " +
                        " T4.""BHeight1"",T4.""BWidth1"",T4.""BLength1"",T4.""BWeight1"", T4.""BVolume"" " +
                    " FROM ""@EXO_GP_PEDCOM"" T0 INNER JOIN ""OPOR"" T1 On T0.""U_EXO_DOCE""=T1.""DocEntry"" " +
                    " INNER JOIN ""OBIN"" T2 On T0.""U_EXO_UBICA""=T2.""BinCode"" " +
                    " INNER JOIN ""POR1"" T3 On T0.""U_EXO_DOCE""=T3.""DocEntry"" And COALESCE(T0.""U_EXO_LINENUM"",0)=T3.""LineNum"" " +
                    " INNER JOIN ""OITM"" T4 ON T3.""ItemCode""=T4.""ItemCode"" " +
                    " WHERE ""U_EXO_USUARIO""='" + Usuario + "' and  T0.""U_EXO_DOCE""='" + rs.Fields.Item("U_EXO_DOCE").Value.ToString() + "' and COALESCE(T0.""U_EXO_LINENUM"",0)='" + rs.Fields.Item("U_EXO_LINENUM").Value.ToString() + "' " +
                    " group by T2.""AbsEntry"" ,T0.""U_EXO_LOTE"", T4.""BHeight1"",T4.""BWidth1"",T4.""BLength1"",T4.""BWeight1"", T4.""BVolume"" "

                    rsLin = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)

                    rsLin.DoQuery(query)
                    Dim primerLote As String = ""
                    Dim PrimerBucle As Boolean = True

                    While (Not rsLin.EoF)

                        oDoc.Lines.BaseEntry = rs.Fields.Item("U_EXO_DOCE").Value.ToString()
                        oDoc.Lines.BaseLine = rs.Fields.Item("U_EXO_LINENUM").Value.ToString()
                        oDoc.Lines.BaseType = 22

                        If oDoc.Lines.Weight1 = 0 Then
                            oDoc.Lines.Weight1 = CType(rsLin.Fields.Item("BHeight1").Value.ToString(), Double)
                            oDoc.Lines.Weight1Unit = 3

                            oDoc.Lines.Width1 = CType(rsLin.Fields.Item("BWidth1").Value.ToString(), Double)
                            oDoc.Lines.Width1Unit = 2

                            oDoc.Lines.Height1 = CType(rsLin.Fields.Item("BWeight1").Value.ToString(), Double)
                            oDoc.Lines.Hight1Unit = 2
                            'oDoc.Lines. = 2

                            oDoc.Lines.Lengh1 = CType(rsLin.Fields.Item("BLength1").Value.ToString(), Double)
                            oDoc.Lines.Lengh1Unit = 2

                            oDoc.Lines.Volume = CType(rsLin.Fields.Item("BVolume").Value.ToString(), Double)
                            oDoc.Lines.VolumeUnit = 2
                        End If

                        oDoc.Lines.WarehouseCode = AlmacenPrincipal.ToString
                        cantidadTotal = cantidadTotal + CType(rsLin.Fields.Item("U_EXO_CANT").Value, Double)

                        If PrimerBucle = True Then
                            primerLote = rsLin.Fields.Item("U_EXO_LOTE").Value.ToString()
                            baselinenumber = 0
                            PrimerBucle = False
                        Else
                            If primerLote <> rsLin.Fields.Item("U_EXO_LOTE").Value.ToString() Then
                                baselinenumber += 1
                            End If

                        End If
                        primerLote = rsLin.Fields.Item("U_EXO_LOTE").Value.ToString()


                        If rsLin.Fields.Item("U_EXO_LOTE").Value.ToString() <> "" Then
                            'oDoc.Lines.BatchNumbers.BaseLineNumber = 0
                            oDoc.Lines.BatchNumbers.BatchNumber = rsLin.Fields.Item("U_EXO_LOTE").Value.ToString()
                            oDoc.Lines.BatchNumbers.Quantity = CType(rsLin.Fields.Item("TotalBin").Value.ToString(), Double)
                            oDoc.Lines.BatchNumbers.Add()

                            'oDoc.Lines.BinAllocations.SetCurrentLine(0)
                            oDoc.Lines.BinAllocations.SerialAndBatchNumbersBaseLine = baselinenumber
                            oDoc.Lines.BinAllocations.BinAbsEntry = rsLin.Fields.Item("AbsEntry").Value.ToString()
                            oDoc.Lines.BinAllocations.Quantity = CType(rsLin.Fields.Item("TotalBin").Value.ToString(), Double)
                            oDoc.Lines.BinAllocations.Add()
                        Else
                            noesloteprimero = False
                        End If

                        If noesloteprimero = False Then
                            'oDoc.Lines.BinAllocations.SerialAndBatchNumbersBaseLine = 0
                            oDoc.Lines.BinAllocations.BinAbsEntry = rsLin.Fields.Item("AbsEntry").Value.ToString()
                            oDoc.Lines.BinAllocations.Quantity = CType(rsLin.Fields.Item("TotalBin").Value.ToString(), Double)
                            oDoc.Lines.BinAllocations.Add()
                        End If

                        oDoc.Lines.Quantity = cantidadTotal

                        rsLin.MoveNext()
                    End While

                    rs.MoveNext()
                End While


                'ARTICULOS NO INVENTARIABLES
                Subquery = "Select DISTINCT( T0.""U_EXO_DOCE"") FROM ""@EXO_GP_PEDCOM"" T0 INNER JOIN ""OPOR"" T1 On T0.""U_EXO_DOCE""=T1.""DocEntry"" " +
                        " WHERE ""U_EXO_USUARIO""='" + Usuario + "' and T1.""CardCode""='" + clienteActual + "' "

                query = "Select  T1.""DocEntry"",T1.""LineNum"" " +
                        " from ""POR1"" T1 inner join ""OITM"" T2 ON T1.""ItemCode""=T2.""ItemCode"" " +
                        " WHERE coalesce(T2.""InvntItem"",'N')='N' and T1.""LineStatus""='O' and T1.""DocEntry"" in  ( " + Subquery + ")"


                rscONS = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                rscONS.DoQuery(query)

                If rscONS.RecordCount > 0 Then
                    While Not (rscONS.EoF)

                        If esPrimeraLinea = False Then
                            oDoc.Lines.Add()
                        Else
                            esPrimeraLinea = False
                        End If


                        oDoc.Lines.BaseEntry = CType(rscONS.Fields.Item("DocEntry").Value.ToString(), Integer)
                        oDoc.Lines.BaseLine = CType(rscONS.Fields.Item("LineNum").Value.ToString(), Integer)
                        oDoc.Lines.BaseType = 22

                        rscONS.MoveNext()
                    End While
                End If

                'portes
                Subquery = "Select DISTINCT( T0.""U_EXO_DOCE"") FROM ""@EXO_GP_PEDCOM"" T0 INNER JOIN ""OPOR"" T1 On T0.""U_EXO_DOCE""=T1.""DocEntry"" " +
                        " WHERE ""U_EXO_USUARIO""='" + Usuario + "' and T1.""CardCode""='" + clienteActual + "'"

                'añadimos los portes
                query = "Select T10.""DocEntry"",T10.""LineNum"",T10.""LineTotal"" from POR3 T10 WHERE ""Status""='O' and T10.""DocEntry"" in ( " + Subquery + ") "

                rsPorte = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                rsPorte.DoQuery(query)

                While (Not rsPorte.EoF)

                    oDoc.Expenses.LineTotal = CType(rsPorte.Fields.Item(2).Value.ToString(), Double)
                    oDoc.Expenses.BaseDocEntry = CType(rsPorte.Fields.Item(0).Value.ToString(), Integer)
                    oDoc.Expenses.BaseDocLine = CType(rsPorte.Fields.Item(1).Value.ToString(), Integer)
                    oDoc.Expenses.BaseDocType = 22
                    oDoc.Expenses.Add()

                    rsPorte.MoveNext()

                End While

                If oDoc.Add() = 0 Then
                    'si no se puede por el objeto, hacer un update


                    'Eliminamos los documentos
                    query = "DELETE from ""@EXO_GP_PEDCOM"" where ""U_EXO_USUARIO""='" + Usuario + "'"
                    rs.DoQuery(query)


                    If oCompany.InTransaction = True Then
                        oCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_Commit)
                    End If

                Else
                    jRes.Resultado = oCompany.GetLastErrorDescription

                    If oCompany.InTransaction = True Then
                        oCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack)
                    End If

                    ' conexiones.liberaCompañia(oCompany)
                    res = js.Serialize(jRes)
                    Return res

                End If

            Else
                jRes.Resultado = "Error no hay datos coincidentes"
            End If

        Catch ex As Exception
            log.escribeMensaje(ex.Message, EXO_Log.EXO_Log.Tipo.error)
            jRes.Resultado = "Error " + ex.Message
        Finally
            '   EXO_CleanCOM.CLiberaCOM.liberaCOM(CType(oCompany, Object))
            EXO_CleanCOM.CLiberaCOM.liberaCOM(CType(rs, Object))
            EXO_CleanCOM.CLiberaCOM.liberaCOM(CType(rsLin, Object))
            EXO_CleanCOM.CLiberaCOM.liberaCOM(CType(rsPorte, Object))
            EXO_CleanCOM.CLiberaCOM.liberaCOM(CType(rscONS, Object))

            ' EXO_CleanCOM.CLiberaCOM.liberaCOM(CType(oDoc, Object))
        End Try

        res = js.Serialize(jRes)

        Return res

    End Function

#End Region

#Region "Picking"

    Private Function CompruebaLote(BaseDatos As String, Usuario As String, Password As String, CodArticulo As String, Lote As String, Ubicacion As String, log As EXO_Log.EXO_Log) As String

        Dim res As String = ""
        Dim oPic As CompruebaLote = New CompruebaLote

        Dim Esprimero As Boolean = True

        Dim oCompany As SAPbobsCOM.Company
        oCompany = New SAPbobsCOM.Company
        oCompany = conectaDI(BaseDatos, Usuario, Password)


        Dim rs As SAPbobsCOM.Recordset = Nothing

        Try

            'hacer consulta al sql y y rellenar el listado
            Dim query As String = ""

            query = " Select  COALESCE(T6.""BinCode"",'') BinCode,T4.""DistNumber"",T4.""ItemCode"",t5.""OnHandQty"",COALESCE(T7.""NumInSale"",1) NUMPERMSR,T7.""ItemName"",T7.""SalUnitMsr"" " +
        " FROM ""OBTN"" T4 " +
        " INNER Join ""OBBQ"" T5 ON T4.""AbsEntry""=T5.""SnBMDAbs"" " +
        " INNER Join ""OBIN"" T6 ON T5.""BinAbs""=T6.""AbsEntry"" " +
         " INNER Join ""OITM"" T7 ON T7.""ItemCode""=T4.""ItemCode"" " +
            " WHERE T4.""DistNumber""='" + Lote + "' and t5.""OnHandQty"">0 "

            If CodArticulo <> "" Then
                query = query + " and t4.""ItemCode""='" + CodArticulo + "'"
            End If

            If Ubicacion <> "" Then
                query = query + " and  T6.""BinCode""='" + Ubicacion + "'"
            End If


            rs = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)

            rs.DoQuery(query)

            If rs.RecordCount = 1 Then

                rs.MoveFirst()

                While (Not rs.EoF)

                    oPic = New CompruebaLote

                    oPic.Resultado = "Ok"
                    oPic.Cantidad = rs.Fields.Item("OnHandQty").Value
                    oPic.Lote = rs.Fields.Item("DistNumber").Value
                    oPic.Articulo = rs.Fields.Item("ItemCode").Value
                    oPic.Ubicacion = rs.Fields.Item("BinCode").Value
                    oPic.CantidadUDM = CType(rs.Fields.Item("NUMPERMSR").Value.ToString, Double)
                    oPic.Descripcion = rs.Fields.Item("ItemName").Value.ToString
                    oPic.UnidadMedida = rs.Fields.Item("SalUnitMsr").Value.ToString
                    Exit While
                End While
            ElseIf rs.RecordCount > 1 Then
                oPic.Resultado = "Error: mas de una coincidencia para la busqueda"

            Else

                oPic.Resultado = "Error: El lote no corresponde con el articulo/ubicacion"

            End If

        Catch ex As Exception
            log.escribeMensaje(ex.Message, EXO_Log.EXO_Log.Tipo.error)
            oPic.Resultado = "Error: " + ex.Message
        Finally
            '   EXO_CleanCOM.CLiberaCOM.liberaCOM(CType(oCompany, Object))
            EXO_CleanCOM.CLiberaCOM.liberaCOM(CType(rs, Object))
        End Try

        'liberaCompañia(compañia)
        'conexiones.DisconnectSQLServer()

        Dim js As New JavaScriptSerializer()
        res = js.Serialize(oPic)

        Return res

    End Function


    Private Function ComPruebaArticulo(BaseDatos As String, Usuario As String, Password As String, CodArticulo As String, CodEan As String, EsLote As String, log As EXO_Log.EXO_Log) As String

        Dim res As String = ""
        Dim oPic As CompruebaArticulo = New CompruebaArticulo

        Dim Esprimero As Boolean = True

        Dim oCompany As SAPbobsCOM.Company
        oCompany = New SAPbobsCOM.Company
        oCompany = conectaDI(BaseDatos, Usuario, Password)
        EstablecerAlmacen(oCompany)
        Dim CodEanConversion As String = ""
        Dim NumLote As String = ""
        Dim rs As SAPbobsCOM.Recordset = Nothing

        Try

            'hacer consulta al sql y y rellenar el listado
            Dim query As String = ""

            If EsLote = "N" Then
                CodEanConversion = CodEan

                'If Len(CodEan) < 15 Then
                '    CodEanConversion = CodEan
                'ElseIf Len(CodEan) = 15 Then
                '    '  'If Len(CodEan) < 16 Then
                '    CodEanConversion = CodEan.Substring(2, 13)
                'Else
                '    CodEanConversion = CodEan.Substring(2, 14)
                'End If

                NumLote = ""
            Else

                CodEanConversion = CodEan
                NumLote = ""
                'If Len(CodEan) < 15 Then
                '    CodEanConversion = CodEan
                'ElseIf Len(CodEan) = 15 Then
                '    'If Len(CodEan) < 16 Then
                '    CodEanConversion = CodEan.Substring(2, 13)
                'Else

                '    CodEanConversion = CodEan.Substring(2, 14)
                '    'si es ean 128 hay que desglosar el código y luego generar la consulta
                '    If Len(CodEan) > 18 Then
                '        NumLote = CodEan.Substring(18, CodEan.Length - 18)
                '    End If

                'End If
            End If

            query = " SELECT T2.""ItemCode"", '0' as ""Cantidad"" " +
                              " FROM  ""OITM"" T2 " +
                              " LEFT JOIN ""OBCD"" T4 ON T2.""SUoMEntry""=T4.""UomEntry"" AND T2.""ItemCode""=T4.""ItemCode"" " +
                              " WHERE  ((T2.""CodeBars"" = '" + CodEanConversion + "' AND T2.""UgpEntry""='-1') OR COALESCE(T4.""BcdCode"",'')='" + CodEanConversion + "') and T2.""ItemCode""='" + CodArticulo + "'"


            rs = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)

            rs.DoQuery(query)

            If rs.RecordCount > 0 Then

                rs.MoveFirst()

                While (Not rs.EoF)

                    oPic = New CompruebaArticulo

                    oPic.Resultado = "Ok"
                    oPic.Cantidad = 0
                    oPic.Lote = NumLote

                    rs.MoveNext()
                End While

            Else

                oPic.Resultado = "El código de barras no se corresponde al articulo"

            End If

        Catch ex As Exception
            log.escribeMensaje(ex.Message, EXO_Log.EXO_Log.Tipo.error)
            oPic.Resultado = "Error: " + ex.Message
        Finally
            '   EXO_CleanCOM.CLiberaCOM.liberaCOM(CType(oCompany, Object))
            EXO_CleanCOM.CLiberaCOM.liberaCOM(CType(rs, Object))
        End Try

        'liberaCompañia(compañia)
        'conexiones.DisconnectSQLServer()

        Dim js As New JavaScriptSerializer()
        res = js.Serialize(oPic)

        Return res

    End Function

    Private Function ListasPicking(BaseDatos As String, Usuario As String, Password As String, log As EXO_Log.EXO_Log) As String
        Dim listado As List(Of ListasPicking) = New List(Of ListasPicking)
        Dim res As String = ""
        Dim oPic As ListasPicking = New ListasPicking

        Dim Esprimero As Boolean = True

        Dim oCompany As SAPbobsCOM.Company
        oCompany = New SAPbobsCOM.Company
        oCompany = conectaDI(BaseDatos, Usuario, Password)
        EstablecerAlmacen(oCompany)
        Dim rs As SAPbobsCOM.Recordset = Nothing
        Dim rs2 As SAPbobsCOM.Recordset = Nothing

        Try

            ''hacer consulta al sql y y rellenar el listado
            Dim query As String = "SELECT ""AbsEntry"",""PickDate"",""Remarks"" FROM ""OPKL""  T0 " +
                  " WHERE ""Status"" not in ('Y','C')  and ""Canceled""='N' and COALESCE(""U_EXO_PPIST"",'N')='N' " +
            " and 'Y' = COALESCE((SELECT MAX('Y') from ""PKL1"" AS T1 INNER JOIN ""RDR1"" T2 ON T1.""OrderEntry""=T2.""DocEntry"" and T1.""OrderLine""=T2.""LineNum"" " +
            " WHERE T0.""AbsEntry""=T1.""AbsEntry""  " +
            " ),'N') "
            'And T2.""WhsCode""='" + AlmacenPrincipal + "'

            'Dim query As String = "SELECT ""AbsEntry"",""PickDate"",""Remarks"" FROM ""OPKL""  T0 " +
            '  " WHERE ""Status"" not in ('Y','C')  and ""Canceled""='N' and COALESCE(""U_EXO_PPIST"",'N')='N' " +
            '"  And (SELECT COUNT(""AbsEntry"") FROM ""PKL1"" T10 WHERE T0.""AbsEntry""=T10.""AbsEntry"")= " +
            '"    (SELECT COUNT(T2.""LineNum"") from ""PKL1"" AS T1 INNER JOIN ""RDR1"" T2 ON T1.""OrderEntry""=T2.""DocEntry"" and T1.""OrderLine""=T2.""LineNum""  " +
            '" WHERE T0.""AbsEntry""=T1.""AbsEntry""   " +
            '" And T2.""WhsCode""='" + AlmacenPrincipal + "')"

            'recorro y voy rellenando listado 



            rs = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)

            rs.DoQuery(query)

            If rs.RecordCount > 0 Then

                rs.MoveFirst()

                While (Not rs.EoF)

                    oPic = New ListasPicking

                    oPic.Resultado = "Ok"
                    oPic.Numero = rs.Fields.Item("AbsEntry").Value.ToString
                    oPic.Fecha = rs.Fields.Item("PickDate").Value.ToString

                    'oPic.Comentario = rs.Fields.Item("Remarks").Value.ToString

                    rs2 = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)

                    query = "select top 1 COALESCE(T3.""TrnspName"",'') TrnspName, " +
                        " concat(concat(concat(concat(t2.""DocNum"",'-'),COALESCE(T4.""CardName"",'')),'-'),Coalesce(T2.""NumAtCard"",'')) CardFName , " +
                  " CONCAT(CONCAT(COALESCE(T4.""U_EXO_OBSP"",''), '/'),COALESCE(T2.""PickRmrk"",''))  U_EXO_OBSP " +
                  " FROM ""PKL1"" T0 INNER JOIN ""RDR1"" T1 On T0.""OrderEntry""=T1.""DocEntry""  " +
                  " INNER JOIN ""ORDR"" T2 ON T1.""DocEntry""=T2.""DocEntry""  " +
                  " Left Join ""OSHP"" T3 ON T2.""TrnspCode""=T3.""TrnspCode"" " +
                  " INNER JOIN ""OCRD"" T4 ON T2.""CardCode""=T4.""CardCode""  " +
                  " WHERE T0.""AbsEntry""='" + rs.Fields.Item("AbsEntry").Value.ToString + "' "

                    rs2.DoQuery(query)

                    If rs2.RecordCount > 0 Then
                        While (Not rs2.EoF)

                            oPic.Comentario = rs2.Fields.Item("CardFName").Value.ToString
                            oPic.Transportista = rs2.Fields.Item("TrnspName").Value.ToString
                            oPic.Observaciones = rs2.Fields.Item("U_EXO_OBSP").Value.ToString
                            rs2.MoveNext()

                        End While

                    End If

                    listado.Add(oPic)

                    rs.MoveNext()
                End While

            Else

                oPic.Resultado = "No hay Pickings disponibles"
                listado.Add(oPic)

            End If

        Catch ex As Exception
            log.escribeMensaje(ex.Message, EXO_Log.EXO_Log.Tipo.error)
            oPic.Resultado = "Error: " + ex.Message
            listado.Add(oPic)
        Finally
            '   EXO_CleanCOM.CLiberaCOM.liberaCOM(CType(oCompany, Object))
            EXO_CleanCOM.CLiberaCOM.liberaCOM(CType(rs, Object))
            EXO_CleanCOM.CLiberaCOM.liberaCOM(CType(rs2, Object))
        End Try

        'liberaCompañia(compañia)
        'conexiones.DisconnectSQLServer()

        Dim js As New JavaScriptSerializer()
        res = js.Serialize(listado)

        Return res

    End Function

    Private Function UbicacionesDelAlmacenBahias(BaseDatos As String, Usuario As String, Password As String, log As EXO_Log.EXO_Log) As String

        Dim listado As List(Of Ubicaciones) = New List(Of Ubicaciones)
        Dim res As String = ""
        Dim oPlayas As Ubicaciones = New Ubicaciones

        Dim Esprimero As Boolean = True

        Dim oCompany As SAPbobsCOM.Company
        oCompany = New SAPbobsCOM.Company
        oCompany = conectaDI(BaseDatos, Usuario, Password)
        EstablecerAlmacen(oCompany)
        Dim rs As SAPbobsCOM.Recordset = Nothing
        Try

            'hacer consulta al sql y y rellenar el listado

            Dim query As String = "select ""BinCode"" FROM ""OBIN"" WHERE ""U_EXO_ESBAHIA""='Y'"
            'recorro y voy rellenando listado 


            rs = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)

            rs.DoQuery(query)

            If rs.RecordCount > 0 Then

                rs.MoveFirst()

                While (Not rs.EoF)

                    oPlayas = New Ubicaciones

                    oPlayas.Resultado = "Ok"
                    oPlayas.Codigo = rs.Fields.Item("BinCode").Value.ToString
                    listado.Add(oPlayas)


                    rs.MoveNext()
                End While

            Else

                oPlayas.Resultado = "Error no hay datos coincidentes"
                listado.Add(oPlayas)

            End If

        Catch ex As Exception
            log.escribeMensaje(ex.Message, EXO_Log.EXO_Log.Tipo.error)
            oPlayas.Resultado = "Error: " + ex.Message
            listado.Add(oPlayas)
        Finally
            '   EXO_CleanCOM.CLiberaCOM.liberaCOM(CType(oCompany, Object))
            EXO_CleanCOM.CLiberaCOM.liberaCOM(CType(rs, Object))
        End Try

        'liberaCompañia(compañia)
        'conexiones.DisconnectSQLServer()

        Dim js As New JavaScriptSerializer()
        res = js.Serialize(listado)

        Return res

    End Function

    Private Function DesglosePicking(BaseDatos As String, Usuario As String, Password As String, NumeroPicking As String, log As EXO_Log.EXO_Log) As String

        Dim listado As List(Of Picking) = New List(Of Picking)
        Dim res As String = ""
        Dim oPic As Picking = New Picking

        Dim Esprimero As Boolean = True

        Dim oCompany As SAPbobsCOM.Company
        oCompany = New SAPbobsCOM.Company


        oCompany = conectaDI(BaseDatos, Usuario, Password)

        EstablecerAlmacen(oCompany)
        Dim rs As SAPbobsCOM.Recordset = Nothing

        Try

            'LLAMAR AL PROCEDIMIENTO ALMACENADO


            Dim query As String = "CALL EXO_GP_TRABAJO_LISTA_PICKING(" + NumeroPicking + ", NULL) "



            rs = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)

            rs.DoQuery(query)

            If rs.RecordCount > 0 Then

                rs.MoveFirst()

                While (Not rs.EoF)

                    oPic = New Picking

                    oPic.Resultado = "Ok"
                    oPic.PickingLinea = rs.Fields.Item("PICKENTRY").Value.ToString
                    oPic.Articulo = rs.Fields.Item("ITEMCODE").Value.ToString
                    oPic.Descripcion = rs.Fields.Item("ITEMNAME").Value.ToString

                    oPic.CantidadTotal = CType(rs.Fields.Item("CANTIDADTOTAL").Value.ToString, Double) / CType(rs.Fields.Item("NUMPERMSR").Value.ToString, Double)
                    oPic.Cantidad = CType(rs.Fields.Item("CANTIDAD").Value.ToString, Double) / CType(rs.Fields.Item("NUMPERMSR").Value.ToString, Double)

                    oPic.CantidadUDM = CType(rs.Fields.Item("NUMPERMSR").Value.ToString, Double)
                    oPic.UnidadMedida = rs.Fields.Item("UDM").Value.ToString
                    oPic.Lote = rs.Fields.Item("BATCHNUM").Value.ToString
                    oPic.Ubicacion = rs.Fields.Item("BINCODE").Value.ToString()
                    oPic.UbicacionPropuesta = rs.Fields.Item("PROPUESTO").Value.ToString()
                    oPic.EsLote = rs.Fields.Item("ESLOTE").Value.ToString
                    oPic.Procesado = rs.Fields.Item("SEPUEDEGESTIONAR").Value.ToString()
                    oPic.CantidadPicking = CType(rs.Fields.Item("CANTIDADPICK").Value.ToString, Double) / CType(rs.Fields.Item("NUMPERMSR").Value.ToString, Double)

                    listado.Add(oPic)


                    rs.MoveNext()
                End While

            Else

                oPic.Resultado = "Error no hay datos coincidentes"
                listado.Add(oPic)

            End If

        Catch ex As Exception
            log.escribeMensaje(ex.Message, EXO_Log.EXO_Log.Tipo.error)
            oPic.Resultado = "Error: " + ex.Message
            listado.Add(oPic)
        Finally
            '   EXO_CleanCOM.CLiberaCOM.liberaCOM(CType(oCompany, Object))
            EXO_CleanCOM.CLiberaCOM.liberaCOM(CType(rs, Object))
        End Try

        'liberaCompañia(compañia)
        'conexiones.DisconnectSQLServer()

        Dim js As New JavaScriptSerializer()
        res = js.Serialize(listado)

        Return res

    End Function

    'FUNCION para generar el traslado del picking
    Private Function OperacionesTraslado(JSON As String, BaseDatos As String, Usuario As String, Password As String, log As EXO_Log.EXO_Log) As String

        Dim ListOp As Traslado = New Traslado
        Dim jRes As Resultado = New Resultado
        Dim res As String = ""
        Dim bPrimero As Boolean = True
        Dim js As New JavaScriptSerializer()
        ListOp = js.Deserialize(Of Traslado)(JSON)

        Dim oCompany As SAPbobsCOM.Company
        oCompany = New SAPbobsCOM.Company
        oCompany = conectaDI(BaseDatos, Usuario, Password)
        EstablecerAlmacen(oCompany)
        Dim rs As SAPbobsCOM.Recordset = Nothing
        Dim oDoc As SAPbobsCOM.StockTransfer = Nothing

        log.escribeMensaje("traslado 1", EXO_Log.EXO_Log.Tipo.error)
        log.escribeMensaje("traslado json " + JSON, EXO_Log.EXO_Log.Tipo.error)

        Try

            Dim UbicacionOrigen As String = ""
            Dim UbicacionDestino As String = ""
            Dim AlmacenOrigen As String = ""
            Dim AlmacenDestino As String = ""
            log.escribeMensaje("traslado 2", EXO_Log.EXO_Log.Tipo.error)
            Dim query As String = "SELECT ""AbsEntry"",""WhsCode"" FROM ""OBIN"" WHERE ""BinCode""='" + ListOp.UbicacionOrigen + "'"

            rs = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
            rs.DoQuery(query)
            log.escribeMensaje("traslado 3", EXO_Log.EXO_Log.Tipo.error)

            If rs.RecordCount > 0 Then
                rs.MoveFirst()
                UbicacionOrigen = rs.Fields.Item("AbsEntry").Value.ToString()
                AlmacenOrigen = rs.Fields.Item("WhsCode").Value.ToString()
            Else
                'log.escribeMensaje(ex.Message, EXO_Log.EXO_Log.Tipo.error)
                jRes.Resultado = "Error: La ubicacion origen no existe " + ListOp.UbicacionOrigen

                res = js.Serialize(jRes)

                Return res
            End If

            log.escribeMensaje("ubicacion origen " + UbicacionOrigen, EXO_Log.EXO_Log.Tipo.error)

            query = "SELECT ""AbsEntry"",""WhsCode"" FROM ""OBIN"" WHERE ""BinCode""='" + ListOp.UbicacionDestino + "'"
            rs = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
            rs.DoQuery(query)
            log.escribeMensaje("traslado 4", EXO_Log.EXO_Log.Tipo.error)
            If rs.RecordCount > 0 Then
                rs.MoveFirst()
                UbicacionDestino = rs.Fields.Item("AbsEntry").Value.ToString()
                AlmacenDestino = rs.Fields.Item("WhsCode").Value.ToString()
            Else
                'log.escribeMensaje(ex.Message, EXO_Log.EXO_Log.Tipo.error)
                jRes.Resultado = "Error: La ubicacion destino no existe " + ListOp.UbicacionDestino
                res = js.Serialize(jRes)

                Return res
            End If
            log.escribeMensaje("ubicacion destino " + UbicacionDestino, EXO_Log.EXO_Log.Tipo.error)


            oDoc = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oStockTransfer)
            oDoc.DocObjectCode = SAPbobsCOM.BoObjectTypes.oStockTransfer

            oDoc.FromWarehouse = AlmacenOrigen
            oDoc.ToWarehouse = AlmacenDestino
            oDoc.Comments = "Creado desde lectores planta"


            If IsNothing(ListOp.NumeroPicking) Then
            Else
                oDoc.UserFields.Fields.Item("U_EXO_NUMPIC").Value = ListOp.NumeroPicking
                oDoc.UserFields.Fields.Item("U_EXO_LINPIC").Value = ListOp.PickingLinea


            End If


            'SE TRABAJA CON LA UNIDAD DEL ARTICULO POR TANTO HAY QUE BUSCAR LA DEL ARTICULO Y REALIZAR LA CONVERSION

            oDoc.Lines.ItemCode = ListOp.CodigoArticulo
            oDoc.Lines.Quantity = ListOp.Cantidad
            oDoc.Lines.FromWarehouseCode = AlmacenOrigen
            oDoc.Lines.WarehouseCode = AlmacenDestino

            'oDoc.Lines.BinAllocations.SerialAndBatchNumbersBaseLine = 0
            If ListOp.Lote <> "" Then
                'oDoc.Lines.BatchNumbers.BaseLineNumber = 0
                oDoc.Lines.BatchNumbers.BatchNumber = ListOp.Lote
                oDoc.Lines.BatchNumbers.Quantity = ListOp.Cantidad
                oDoc.Lines.BatchNumbers.Add()
            End If

            'oDoc.Lines.BinAllocations.SetCurrentLine(0)
            oDoc.Lines.BinAllocations.SerialAndBatchNumbersBaseLine = 0
            oDoc.Lines.BinAllocations.BinActionType = SAPbobsCOM.BinActionTypeEnum.batFromWarehouse
            oDoc.Lines.BinAllocations.BinAbsEntry = UbicacionOrigen
            oDoc.Lines.BinAllocations.Quantity = ListOp.Cantidad
            oDoc.Lines.BinAllocations.Add()


            'oDoc.Lines.BinAllocations.SetCurrentLine(1)
            oDoc.Lines.BinAllocations.SerialAndBatchNumbersBaseLine = 0
            oDoc.Lines.BinAllocations.BinActionType = SAPbobsCOM.BinActionTypeEnum.batToWarehouse
            oDoc.Lines.BinAllocations.BinAbsEntry = UbicacionDestino
            oDoc.Lines.BinAllocations.Quantity = ListOp.Cantidad
            oDoc.Lines.BinAllocations.Add()
            log.escribeMensaje("traslado 7", EXO_Log.EXO_Log.Tipo.error)

            If oDoc.Add() = 0 Then
                log.escribeMensaje("traslado 8", EXO_Log.EXO_Log.Tipo.error)

                jRes.Resultado = "Ok"

                If IsNothing(ListOp.NumeroPicking) Then
                Else
                    'actualizamos la lista de picking
                    ' conexiones.ExecuteNonQuery("update pkl1 set u_exo_traslado='Y' where absentry='" + ListOp.NumeroPicking + "' and pickentry='" + ListOp.PickingLinea + "'")
                End If
            Else
                log.escribeMensaje("traslado 9", EXO_Log.EXO_Log.Tipo.error)

                jRes.Resultado = oCompany.GetLastErrorDescription
            End If

            'conexiones.liberaDocumento(oDoc)
            System.Runtime.InteropServices.Marshal.ReleaseComObject(oDoc)
        Catch ex As Exception
            log.escribeMensaje(ex.Message, EXO_Log.EXO_Log.Tipo.error)
            jRes.Resultado = "Error : " + ex.Message
        Finally
            '   EXO_CleanCOM.CLiberaCOM.liberaCOM(CType(oCompany, Object))
            EXO_CleanCOM.CLiberaCOM.liberaCOM(CType(rs, Object))
            ' EXO_CleanCOM.CLiberaCOM.liberaCOM(CType(oDoc, Object))
        End Try

        'conexiones.liberaCompañia(oCompany)

        res = js.Serialize(jRes)

        Return res

    End Function

    Private Function GenerarPicking2(JSON As String, BaseDatos As String, Usuario As String, Password As String, log As EXO_Log.EXO_Log) As String

        'falta poner el transaction
        'log.escribeMensaje(JSON, EXO_Log.EXO_Log.Tipo.informacion)
        'log.escribeMensaje(Usuario, EXO_Log.EXO_Log.Tipo.informacion)
        'log.escribeMensaje(Password, EXO_Log.EXO_Log.Tipo.informacion)

        Dim ListOp As GenerarPicking = New GenerarPicking

        Dim jRes As Resultado = New Resultado

        Dim res As String = ""
        Dim bPrimero As Boolean = True

        Dim js As New JavaScriptSerializer()

        Dim EntregasGeneradas As String = ""

        Dim oCompany As SAPbobsCOM.Company
        oCompany = New SAPbobsCOM.Company
        oCompany = conectaDI(BaseDatos, Usuario, Password)

        EstablecerAlmacen(oCompany)
        Dim rs As SAPbobsCOM.Recordset = Nothing
        Dim odraftODLN As SAPbobsCOM.Documents = Nothing

        Try
            log.escribeMensaje(JSON, log.Tipo.informacion)
            ListOp = js.Deserialize(Of GenerarPicking)(JSON)

            Dim sdocnum As String = ""



            'If oCompany.InTransaction = True Then
            '    oCompany.EndTransaction(BoWfTransOpt.wf_RollBack)
            'End If

            'Comprobamos si ya tenemos el número de picking generado, de ser así se termina el proceso
            Dim query As String = "SELECT ""U_EXO_PICK"" FROM ""@EXO_OGPPA"" WHERE ""U_EXO_PICK""='" + ListOp.NumeroPicking + "'"

            rs = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
            rs.DoQuery(query)

            If rs.RecordCount = 0 Then

                'oCompany.StartTransaction()
                jRes.Resultado = ""
                '------------LOS LOTES HAY QUE ASIGNARLOS PRIMERO EN EL PEDIDO-------------------
                query = "SELECT ""AbsEntry"",""WhsCode"" FROM ""OBIN"" WHERE ""BinCode""='" + ListOp.Ubicacion + "'"

                rs = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                rs.DoQuery(query)

                Dim UbicacionBahia As String = ""
                Dim sAlmacen As String = ""

                If rs.RecordCount > 0 Then
                    rs.MoveFirst()
                    UbicacionBahia = rs.Fields.Item("AbsEntry").Value.ToString()
                    sAlmacen = rs.Fields.Item("WhsCode").Value.ToString()
                End If

                'hay que asignar a los pedidos primero. 
                Dim sql As String = "SELECT T0.""AbsEntry"",T0.""PickEntry"",T0.""OrderEntry"",T0.""OrderLine"",T3.""CardCode"",T1.""NumPerMsr"", " +
                    " T4.""StreetS"", T4.""StreetNoS"", T4.""BlockS"", T4.""CityS"", T4.""ZipCodeS"", T4.""CountyS"", T4.""StateS"", T4.""CountryS""," +
                    " T4.""Address2S"",T4.""Address3S"", " +
                    " T4.""StreetB"", T4.""StreetNoB"", T4.""BlockB"", T4.""CityB"", T4.""ZipCodeB"", T4.""CountyB"", T4.""StateB"", T4.""CountryB""," +
                    " T4.""Address2B"",T4.""Address3B"", " +
                    "  COALESCE(t4.""U_B1SYS_DIR3_01"",'') ""DIR3_01"", COALESCE(t4.""U_B1SYS_DIR3_02"",'') ""DIR3_02"", COALESCE(t4.""U_B1SYS_DIR3_03"",'') ""DIR3_03"" " +
                    " , T3.""NumAtCard"", T3.""ShipToCode"",T3.""PayToCode"" " +
                    " FROM ""PKL1"" T0  INNER JOIN ""RDR1"" T1 On  T1.""DocEntry""=T0.""OrderEntry"" And   T1.""LineNum""=T0.""OrderLine"" " +
                    " INNER JOIN ""OITM"" T2 On T1.""ItemCode""=T2.""ItemCode"" " +
                    " INNER JOIN ""ORDR"" T3 On T1.""DocEntry""=T3.""DocEntry"" " +
                    " INNER JOIN ""RDR12"" T4 ON T3.""DocEntry""=T4.""DocEntry"" " +
                    " WHERE T0.""AbsEntry"" = " + ListOp.NumeroPicking + " " +
                    " ORDER BY T0.""OrderEntry"",T0.""OrderLine"" "

                'poner en demo bankinter y quitar la anterior
                'Dim sql As String = "SELECT T0.""AbsEntry"",T0.""PickEntry"",T0.""OrderEntry"",T0.""OrderLine"",T3.""CardCode"",T1.""NumPerMsr"" " +
                '    " ,T3.""ShipToCode"", T3.""PayToCode"",t3.""NumAtCard""  " +
                '    ", T4.""StreetS"", T4.""StreetNoS"", T4.""BlockS"", T4.""CityS"", T4.""ZipCodeS"", T4.""CountyS"", T4.""StateS"", T4.""CountryS""," +
                '    "T4.""Address2S"",T4.""Address3S"", " +
                '    "T4.""StreetB"", T4.""StreetNoB"", T4.""BlockB"", T4.""CityB"", T4.""ZipCodeB"", T4.""CountyB"", T4.""StateB"", T4.""CountryB""," +
                '    "T4.""Address2B"",T4.""Address3B"" " +
                '    "   " +
                '    " , COALESCE(t4.""U_B1SYS_DIR3_01"",'') ""DIR3_01"", COALESCE(t4.""U_B1SYS_DIR3_02"",'') ""DIR3_02"", COALESCE(t4.""U_B1SYS_DIR3_03"",'') ""DIR3_03"" " +
                '    " FROM ""PKL1"" T0  INNER JOIN ""RDR1"" T1 On  T1.""DocEntry""=T0.""OrderEntry"" And   T1.""LineNum""=T0.""OrderLine"" " +
                '    " INNER JOIN ""OITM"" T2 On T1.""ItemCode""=T2.""ItemCode"" " +
                '    " INNER JOIN ""ORDR"" T3 On T1.""DocEntry""=T3.""DocEntry"" " +
                '    " INNER JOIN ""RDR12"" T4 ON T3.""DocEntry""=T4.""DocEntry"" " +
                '    " WHERE T0.""AbsEntry"" = " + ListOp.NumeroPicking + " " +
                '    " ORDER BY T0.""OrderEntry"",T0.""OrderLine"" "

                Dim DIR1 As String = ""
                Dim DIR2 As String = ""
                Dim DIR3 As String = ""

                rs.DoQuery(sql)


                DIR1 = rs.Fields.Item("DIR3_01").Value.ToString()
                DIR2 = rs.Fields.Item("DIR3_02").Value.ToString()
                DIR3 = rs.Fields.Item("DIR3_03").Value.ToString()

                odraftODLN = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oDrafts)

                odraftODLN.DocType = SAPbobsCOM.BoDocumentTypes.dDocument_Items

                odraftODLN.DocObjectCode = SAPbobsCOM.BoObjectTypes.oDeliveryNotes

                odraftODLN.CardCode = rs.Fields.Item("CardCode").Value.ToString()
                odraftODLN.NumAtCard = rs.Fields.Item("NumAtCard").Value.ToString()

                If rs.Fields.Item("ShipToCode").Value.ToString() = "" Then
                    odraftODLN.AddressExtension.ShipToStreet = rs.Fields.Item("StreetS").Value.ToString()
                    odraftODLN.AddressExtension.ShipToStreetNo = rs.Fields.Item("StreetNoS").Value.ToString()
                    odraftODLN.AddressExtension.ShipToBlock = rs.Fields.Item("BlockS").Value.ToString()
                    odraftODLN.AddressExtension.ShipToCity = rs.Fields.Item("CityS").Value.ToString()
                    odraftODLN.AddressExtension.ShipToZipCode = rs.Fields.Item("ZipCodeS").Value.ToString()
                    odraftODLN.AddressExtension.ShipToCounty = rs.Fields.Item("CountyS").Value.ToString()
                    odraftODLN.AddressExtension.ShipToState = rs.Fields.Item("StateS").Value.ToString()
                    odraftODLN.AddressExtension.ShipToCountry = rs.Fields.Item("CountryS").Value.ToString()
                    'silvia añado
                    odraftODLN.AddressExtension.ShipToAddress2 = rs.Fields.Item("Address2S").Value.ToString()
                    odraftODLN.AddressExtension.ShipToAddress3 = rs.Fields.Item("Address3S").Value.ToString()
                Else
                    odraftODLN.ShipToCode = rs.Fields.Item("ShipToCode").Value.ToString()
                End If

                If rs.Fields.Item("PayToCode").Value.ToString() = "" Then
                    odraftODLN.AddressExtension.BillToStreet = rs.Fields.Item("StreetB").Value.ToString()
                    odraftODLN.AddressExtension.BillToStreetNo = rs.Fields.Item("StreetNoB").Value.ToString()
                    odraftODLN.AddressExtension.BillToBlock = rs.Fields.Item("BlockB").Value.ToString()
                    odraftODLN.AddressExtension.BillToCity = rs.Fields.Item("CityB").Value.ToString()
                    odraftODLN.AddressExtension.BillToZipCode = rs.Fields.Item("ZipCodeB").Value.ToString()
                    odraftODLN.AddressExtension.BillToCounty = rs.Fields.Item("CountyB").Value.ToString()
                    odraftODLN.AddressExtension.BillToState = rs.Fields.Item("StateB").Value.ToString()
                    odraftODLN.AddressExtension.BillToCountry = rs.Fields.Item("CountryB").Value.ToString()
                    odraftODLN.AddressExtension.BillToAddress2 = rs.Fields.Item("Address2B").Value.ToString()
                    odraftODLN.AddressExtension.BillToAddress3 = rs.Fields.Item("Address3B").Value.ToString()
                Else
                    odraftODLN.PayToCode = rs.Fields.Item("PayToCode").Value.ToString()

                End If

                odraftODLN.NumAtCard = rs.Fields.Item("NumAtCard").Value.ToString()

                'quitar para demo bankinter
                'campos usuario

                'odraftODLN.UserFields.Fields.Item("U_EXO_FMENSUAL").Value = rs.Fields.Item("U_EXO_FMENSUAL").Value.ToString()

                Dim DocPedido As String = ""
                Dim EsNuevo As Boolean = True
                Dim setCurrent2 As Boolean = True

                Dim baselinenumber2 As Integer = 0
                Dim noesloteprimero2 As Boolean = True
                Dim cantidadTotal2 As Double = 0

                Dim AnyadeRegistros2 As Boolean = True
                Dim esPrimeraLinea As Boolean = True

                Dim contadorLineas As Integer = 1

                If rs.RecordCount > 0 Then
                    rs.MoveFirst()

                    While (Not rs.EoF)

                        setCurrent2 = True
                        AnyadeRegistros2 = True

                        Dim baselinenumber As Integer = 0
                        Dim noesloteprimero As Boolean = True
                        Dim cantidadTotal As Double = 0
                        Dim MasdeUnLote As Boolean = True


                        If esPrimeraLinea = False Then
                            odraftODLN.Lines.Add()
                        Else
                            esPrimeraLinea = False
                        End If

                        'recorremos ListOp.Palets para calcular palets bultos
                        'recorremos bultos
                        Dim totalBultos As Integer = 0
                        For Each bultos In ListOp.Bultos
                            If bultos.Bulto > totalBultos Then
                                totalBultos = bultos.Bulto
                            End If

                        Next

                        odraftODLN.UserFields.Fields.Item("U_EXO_QTYBULTOS").Value = totalBultos

                        Dim totalPalets As Integer = 0
                        Dim PesototalPalet As Double = 0
                        Dim VolTotalPalet As Double = 0

                        Dim esprimero As Boolean = True

                        For Each palets In ListOp.Palets

                            PesototalPalet = PesototalPalet + palets.Peso
                            VolTotalPalet = VolTotalPalet + palets.Volumen
                            'If palets.Tipo = "medio" Then
                            '    totalPalets = totalPalets + 0.5
                            'Else
                            '    totalPalets = totalPalets + 1
                            'End If
                        Next

                        totalPalets = ListOp.Palets.Count

                        log.escribeMensaje("TOTAL PALETS " + totalPalets.ToString)

                        odraftODLN.UserFields.Fields.Item("U_EXO_NUMPALETS").Value = totalPalets

                        log.escribeMensaje("TOTAL peso " + PesototalPalet.ToString)

                        odraftODLN.UserFields.Fields.Item("U_EXO_PESOTOTALPALET").Value = PesototalPalet

                        odraftODLN.UserFields.Fields.Item("U_EXO_VOLTOTALPALET").Value = VolTotalPalet

                        odraftODLN.UserFields.Fields.Item("U_EXO_NUMPIC").Value = ListOp.NumeroPicking

                        'buscamos el pickentry en el json, para poder hacer los baseentry
                        For Each Linea As LineasPicking In ListOp.Lineas
                            'encontramos la linea y trabajamos con listop

                            If rs.Fields.Item("PickEntry").Value.ToString = Linea.PickingLinea Then

                                odraftODLN.Lines.BaseEntry = CType(rs.Fields.Item("OrderEntry").Value.ToString(), Integer)
                                odraftODLN.Lines.BaseLine = CType(rs.Fields.Item("OrderLine").Value.ToString(), Integer)

                                odraftODLN.Lines.BaseType = 17
                                cantidadTotal = cantidadTotal + Linea.Cantidad
                                odraftODLN.Lines.BinAllocations.BinAbsEntry = CType(UbicacionBahia, Integer)
                                odraftODLN.Lines.WarehouseCode = sAlmacen

                                If Linea.Lote <> "" Then
                                    'odraftODLN.Lines.BatchNumbers.BaseLineNumber = CType(rs.Fields.Item("OrderLine").Value.ToString(), Integer)

                                    If MasdeUnLote = False Then
                                        odraftODLN.Lines.BatchNumbers.Add()
                                        odraftODLN.Lines.BinAllocations.Add()
                                    End If
                                    MasdeUnLote = False

                                    odraftODLN.Lines.BatchNumbers.BatchNumber = Linea.Lote
                                    odraftODLN.Lines.BatchNumbers.Quantity = Linea.Cantidad



                                    odraftODLN.Lines.BinAllocations.SerialAndBatchNumbersBaseLine = baselinenumber
                                    odraftODLN.Lines.BinAllocations.BinAbsEntry = CType(UbicacionBahia, Integer)
                                    odraftODLN.Lines.BinAllocations.Quantity = Linea.Cantidad

                                    baselinenumber += 1
                                Else
                                    noesloteprimero = False
                                End If


                            End If
                        Next

                        If noesloteprimero = False Then

                            'odraftODLN.Lines.BinAllocations.SerialAndBatchNumbersBaseLine = 0

                            odraftODLN.Lines.BinAllocations.BinAbsEntry = CType(UbicacionBahia, Integer)
                            odraftODLN.Lines.BinAllocations.Quantity = cantidadTotal
                            odraftODLN.Lines.BinAllocations.Add()
                        End If

                        odraftODLN.Lines.Quantity = cantidadTotal / CType(rs.Fields.Item("NumPerMsr").Value.ToString, Double)
                        'ACUERDOS GLOBALES AÑADO silvia
                        'odraftODLN.Lines.UserFields.Fields.Item("U_EXO_AGRLNNUMGRUPO").Value = rs.Fields.Item("U_EXO_AGRLNNUMGRUPO").Value.ToString
                        'odraftODLN.Lines.UserFields.Fields.Item("U_EXO_AGRNOGRUPO").Value = rs.Fields.Item("U_EXO_AGRNOGRUPO").Value.ToString
                        contadorLineas = contadorLineas + 1
                        rs.MoveNext()
                    End While

                    'ARTICULOS NO INVENTARIABLES
                    sql = "Select  T1.""DocEntry"",T1.""LineNum"" " +
                        " from ""RDR1"" T1 inner join ""OITM"" T2 On T1.""ItemCode""=T2.""ItemCode"" " +
                        " WHERE coalesce(T2.""InvntItem"",'N')='N' and T1.""LineStatus""='O' and T1.""DocEntry"" in ( " +
                        " Select  distinct(T0.""OrderEntry"") " +
                        " FROM ""PKL1"" T0   " +
                        " WHERE T0.""AbsEntry"" =  " + ListOp.NumeroPicking + ") "

                    Dim rscONS As SAPbobsCOM.Recordset
                    rscONS = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                    rscONS.DoQuery(sql)

                    If rscONS.RecordCount > 0 Then
                        While Not (rscONS.EoF)

                            If esPrimeraLinea = False Then
                                odraftODLN.Lines.Add()
                            Else
                                esPrimeraLinea = False
                            End If


                            odraftODLN.Lines.BaseEntry = CType(rscONS.Fields.Item("DocEntry").Value.ToString(), Integer)
                            odraftODLN.Lines.BaseLine = CType(rscONS.Fields.Item("LineNum").Value.ToString(), Integer)
                            odraftODLN.Lines.BaseType = 17

                            rscONS.MoveNext()
                        End While
                    End If

                    'PORTES
                    sql = "Select  T10.""DocEntry"",T10.""LineNum"",T10.""LineTotal"" " +
                        " from ""RDR3"" T10 " +
                        " WHERE  T10.""DocEntry"" in ( " +
                        " Select  distinct(T0.""OrderEntry"") " +
                        " FROM ""PKL1"" T0   " +
                        " WHERE T0.""AbsEntry"" =  " + ListOp.NumeroPicking + ") "

                    rscONS = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                    rscONS.DoQuery(sql)

                    While (Not rscONS.EoF)

                        odraftODLN.Expenses.LineTotal = CType(rscONS.Fields.Item("LineTotal").Value.ToString(), Double)
                        odraftODLN.Expenses.BaseDocEntry = CType(rscONS.Fields.Item("DocEntry").Value.ToString(), Integer)
                        odraftODLN.Expenses.BaseDocLine = CType(rscONS.Fields.Item("LineNum").Value.ToString(), Integer)
                        odraftODLN.Expenses.BaseDocType = 17
                        odraftODLN.Expenses.Add()

                        rscONS.MoveNext()
                    End While

                    If odraftODLN.Add() = 0 Then
                        jRes.Resultado = "Ok"
                        Dim DraftKey As String = oCompany.GetNewObjectKey

                        Dim sql3 As String = "UPDATE ""DRF12"" set ""U_B1SYS_DIR3_01""='" + DIR1 + "',""U_B1SYS_DIR3_02""='" + DIR2 + "',""U_B1SYS_DIR3_03""='" + DIR3 + "' " +
                                        " WHERE ""DocEntry""='" + DraftKey + "' "
                        rs.DoQuery(sql3)

                        If ListOp.Bultos.Count > 0 Then
                            'llamo a generar Udo de picking
                            If GenerarBultosPacking(oCompany, ListOp.Bultos, ListOp.NumeroPicking, ListOp.Palets, DraftKey) Then
                                rs.DoQuery(" update ""OPKL"" SET ""U_EXO_PPIST""='Y' WHERE ""AbsEntry""='" + ListOp.NumeroPicking + "'")
                            Else
                                jRes.Resultado = "Error Generando el packing"
                            End If

                            'marcar el picking como efectuado con el campo de usuario

                        End If

                    Else
                        jRes.Resultado = oCompany.GetLastErrorDescription
                        log.escribeMensaje(oCompany.GetLastErrorDescription, EXO_Log.EXO_Log.Tipo.informacion)
                        'If oCompany.InTransaction = True Then
                        '    oCompany.EndTransaction(BoWfTransOpt.wf_RollBack)
                        'End If

                        ' conexiones.liberaCompañia(oCompany)
                        res = js.Serialize(jRes)
                        Return res

                    End If
                End If
            Else
                jRes.Resultado = "Ok"
            End If


        Catch ex As Exception
            log.escribeMensaje(ex.Message, EXO_Log.EXO_Log.Tipo.error)
            jRes.Resultado = "Error. " + ex.Message

            'If oCompany.InTransaction = True Then
            '    oCompany.EndTransaction(BoWfTransOpt.wf_RollBack)
            'End If
        Finally
            '   EXO_CleanCOM.CLiberaCOM.liberaCOM(CType(oCompany, Object))
            EXO_CleanCOM.CLiberaCOM.liberaCOM(CType(rs, Object))
            EXO_CleanCOM.CLiberaCOM.liberaCOM(CType(odraftODLN, Object))
        End Try


        'conexiones.liberaCompañia(oCompany)

        res = js.Serialize(jRes)

        Return res

    End Function

    Private Function GenerarBultosPacking(oCompany As SAPbobsCOM.Company, bultos As List(Of BultosPicking), numeroPicking As String, palets As List(Of PaletsPicking), DraftKey As String) As Boolean

        Dim oGeneralService As SAPbobsCOM.GeneralService = Nothing
        Dim oGeneralData As SAPbobsCOM.GeneralData = Nothing
        Dim oChild As SAPbobsCOM.GeneralData = Nothing
        Dim oChildren As SAPbobsCOM.GeneralDataCollection = Nothing
        Dim oGeneralDataParams As SAPbobsCOM.GeneralDataParams = Nothing

        Try
            oGeneralService = oCompany.GetCompanyService.GetGeneralService("EXO_OGPPA")
            oGeneralData = oGeneralService.GetDataInterface(SAPbobsCOM.GeneralServiceDataInterfaces.gsGeneralData)

            oGeneralData.SetProperty("U_EXO_PICK", numeroPicking)
            oGeneralData.SetProperty("U_EXO_DRAFT", DraftKey)

            oChildren = oGeneralData.Child("EXO_OGPPA1")
            ' oChild = oChildren.Add

            For Each FilaBulto In bultos

                oChild = oChildren.Add
                oChild.SetProperty("U_EXO_NBULTO", FilaBulto.Bulto)
                oChild.SetProperty("U_EXO_CODART", FilaBulto.Articulo.ToString)
                oChild.SetProperty("U_EXO_CANT", FilaBulto.Cantidad)
                oChild.SetProperty("U_EXO_LOTE", FilaBulto.Lote)
                oChild.SetProperty("U_EXO_LPICK", FilaBulto.LineaPicking)

                'consulta sql para sacar docentry y linenum

                Dim rs2 As SAPbobsCOM.Recordset
                rs2 = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                Dim sql As String = "Select ""OrderEntry"", ""OrderLine"" from ""PKL1"" WHERE ""AbsEntry""='" + numeroPicking + "' and ""PickEntry""='" + FilaBulto.LineaPicking + "'"
                rs2.DoQuery(sql)

                oChild.SetProperty("U_EXO_DOCE", rs2.Fields.Item("OrderEntry").Value.ToString)
                oChild.SetProperty("U_EXO_DOCL", rs2.Fields.Item("OrderLine").Value.ToString)

            Next

            If palets.Count > 0 Then

                oChildren = oGeneralData.Child("EXO_OGPPA2")
                ' oChild = oChildren.Add

                For Each FilaPalet In palets
                    oChild = oChildren.Add
                    oChild.SetProperty("U_EXO_PAL", FilaPalet.Palet.ToString)

                    oChild.SetProperty("U_EXO_TIPO", FilaPalet.Tipo.ToString)

                    oChild.SetProperty("U_EXO_PESO", FilaPalet.Peso)

                    oChild.SetProperty("U_EXO_VOL", FilaPalet.Volumen)

                    oChild.SetProperty("U_EXO_ALT", FilaPalet.Altura)

                Next
            End If

            oGeneralDataParams = oGeneralService.Add(oGeneralData)

            Dim sDocEntry As String = oGeneralDataParams.GetProperty("DocEntry")

            If oGeneralDataParams Is Nothing OrElse sDocEntry = "" Then
                log.escribeMensaje(oCompany.GetLastErrorDescription, EXO_Log.EXO_Log.Tipo.error)
                Return False
            End If


        Catch ex As Exception

            log.escribeMensaje(ex.Message, EXO_Log.EXO_Log.Tipo.error)
            Return False
        Finally
            If oGeneralService IsNot Nothing Then System.Runtime.InteropServices.Marshal.FinalReleaseComObject(oGeneralService)
            If oGeneralData IsNot Nothing Then System.Runtime.InteropServices.Marshal.FinalReleaseComObject(oGeneralData)
            If oChild IsNot Nothing Then System.Runtime.InteropServices.Marshal.FinalReleaseComObject(oChild)
            If oChildren IsNot Nothing Then System.Runtime.InteropServices.Marshal.FinalReleaseComObject(oChildren)
            If oGeneralDataParams IsNot Nothing Then System.Runtime.InteropServices.Marshal.FinalReleaseComObject(oGeneralDataParams)

        End Try


        Return True

    End Function

    Private Function CompruebaUbicacion(BaseDatos As String, Usuario As String, Password As String, CodArticulo As String, Ubicacion As String, EsLote As String, log As EXO_Log.EXO_Log) As String

        Dim res As String = ""
        Dim oPic As CompruebaLote = New CompruebaLote

        Dim Esprimero As Boolean = True

        Dim oCompany As SAPbobsCOM.Company
        oCompany = New SAPbobsCOM.Company
        oCompany = conectaDI(BaseDatos, Usuario, Password)
        EstablecerAlmacen(oCompany)
        Dim CodEanConversion As String = ""

        Dim rs As SAPbobsCOM.Recordset = Nothing

        Try

            'hacer consulta al sql y y rellenar el listado
            Dim query As String = ""


            'recibo articulo y ubicacion
            'tengo que buscar todas las unidades que hayan en esta ubicación de este articulo, independientemente del número de lotes.

            query = " Select  COALESCE(T6.""BinCode"",'') BinCode,T4.""ItemCode"",SUM(t5.""OnHandQty"") ""Cant"",COALESCE(T7.""NumInSale"",1) NUMPERMSR,T7.""ItemName"",T7.""SalUnitMsr"" " +
            " FROM ""OBTN"" T4 " +
            " INNER Join ""OBBQ"" T5 ON T4.""AbsEntry""=T5.""SnBMDAbs"" " +
            " INNER Join ""OBIN"" T6 ON T5.""BinAbs""=T6.""AbsEntry"" " +
            " INNER Join ""OITM"" T7 ON T7.""ItemCode""=T4.""ItemCode"" " +
            " WHERE  t5.""OnHandQty"">0  and t4.""ItemCode""='" + CodArticulo + "'  and  T6.""BinCode""='" + Ubicacion + "' " +
            " group by T7.""NumInSale"",T7.""ItemName"",T7.""SalUnitMsr"",T6.""BinCode"",T4.""ItemCode"" "
            rs = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)

            rs.DoQuery(query)

            If rs.RecordCount > 0 Then

                rs.MoveFirst()

                While (Not rs.EoF)

                    oPic = New CompruebaLote

                    oPic.Resultado = "Ok"
                    oPic.Cantidad = CType(rs.Fields.Item("Cant").Value, Double)
                    'oPic.Lote = NumLote
                    oPic.Lote = ""


                    oPic.Articulo = rs.Fields.Item("ItemCode").Value
                    oPic.Ubicacion = rs.Fields.Item("BinCode").Value
                    oPic.CantidadUDM = CType(rs.Fields.Item("NUMPERMSR").Value.ToString, Double)
                    oPic.Descripcion = rs.Fields.Item("ItemName").Value.ToString
                    oPic.UnidadMedida = rs.Fields.Item("SalUnitMsr").Value.ToString

                    rs.MoveNext()
                End While

            Else

                oPic.Resultado = "El código de barras no se corresponde al articulo"

            End If

        Catch ex As Exception
            log.escribeMensaje(ex.Message, EXO_Log.EXO_Log.Tipo.error)
            oPic.Resultado = "Error: " + ex.Message
        Finally
            '   EXO_CleanCOM.CLiberaCOM.liberaCOM(CType(oCompany, Object))
            EXO_CleanCOM.CLiberaCOM.liberaCOM(CType(rs, Object))
        End Try

        'liberaCompañia(compañia)
        'conexiones.DisconnectSQLServer()

        Dim js As New JavaScriptSerializer()
        res = js.Serialize(oPic)

        Return res

    End Function

    Private Function OperacionesTrasladoUbicacion(JSON As String, BaseDatos As String, Usuario As String, Password As String, log As EXO_Log.EXO_Log) As String

        Dim ListOp As Traslado = New Traslado
        Dim jRes As Resultado = New Resultado
        Dim res As String = ""
        Dim bPrimero As Boolean = True
        Dim js As New JavaScriptSerializer()
        ListOp = js.Deserialize(Of Traslado)(JSON)

        Dim oCompany As SAPbobsCOM.Company
        oCompany = New SAPbobsCOM.Company
        oCompany = conectaDI(BaseDatos, Usuario, Password)
        EstablecerAlmacen(oCompany)
        Dim rs As SAPbobsCOM.Recordset = Nothing
        Dim oDoc As SAPbobsCOM.StockTransfer = Nothing


        Try

            Dim UbicacionOrigen As String = ""
            Dim UbicacionDestino As String = ""
            Dim AlmacenOrigen As String = ""
            Dim AlmacenDestino As String = ""

            Dim query As String = "SELECT ""AbsEntry"",""WhsCode"" FROM ""OBIN"" WHERE ""BinCode""='" + ListOp.UbicacionOrigen + "'"

            rs = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
            rs.DoQuery(query)


            If rs.RecordCount > 0 Then
                rs.MoveFirst()
                UbicacionOrigen = rs.Fields.Item("AbsEntry").Value.ToString()
                AlmacenOrigen = rs.Fields.Item("WhsCode").Value.ToString()
            Else
                'log.escribeMensaje(ex.Message, EXO_Log.EXO_Log.Tipo.error)
                jRes.Resultado = "Error: La ubicacion origen no existe " + ListOp.UbicacionOrigen

                res = js.Serialize(jRes)

                Return res
            End If

            query = "SELECT ""AbsEntry"",""WhsCode"" FROM ""OBIN"" WHERE ""BinCode""='" + ListOp.UbicacionDestino + "'"
            rs = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
            rs.DoQuery(query)

            If rs.RecordCount > 0 Then
                rs.MoveFirst()
                UbicacionDestino = rs.Fields.Item("AbsEntry").Value.ToString()
                AlmacenDestino = rs.Fields.Item("WhsCode").Value.ToString()
            Else
                'log.escribeMensaje(ex.Message, EXO_Log.EXO_Log.Tipo.error)
                jRes.Resultado = "Error: La ubicacion destino no existe " + ListOp.UbicacionDestino
                res = js.Serialize(jRes)

                Return res
            End If

            oDoc = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oStockTransfer)
            oDoc.DocObjectCode = SAPbobsCOM.BoObjectTypes.oStockTransfer

            oDoc.FromWarehouse = AlmacenOrigen
            oDoc.ToWarehouse = AlmacenDestino
            oDoc.Comments = "Creado desde lectores planta Ubicacion completa"


            If IsNothing(ListOp.NumeroPicking) Then
            Else
                oDoc.UserFields.Fields.Item("U_EXO_NUMPIC").Value = ListOp.NumeroPicking
                oDoc.UserFields.Fields.Item("U_EXO_LINPIC").Value = ListOp.PickingLinea


            End If


            'SE TRABAJA CON LA UNIDAD DEL ARTICULO POR TANTO HAY QUE BUSCAR LA DEL ARTICULO Y REALIZAR LA CONVERSION

            oDoc.Lines.ItemCode = ListOp.CodigoArticulo
            oDoc.Lines.Quantity = ListOp.Cantidad
            oDoc.Lines.FromWarehouseCode = AlmacenOrigen
            oDoc.Lines.WarehouseCode = AlmacenDestino

            'hacemos una query para buscar todos los lotes y las cantidades, se hace un bucle para rellenarlo.
            query = " Select  COALESCE(T6.""BinCode"",'') BinCode,T4.""DistNumber"",T4.""ItemCode"",t5.""OnHandQty"",COALESCE(T7.""NumInSale"",1) NUMPERMSR,T7.""ItemName"",T7.""SalUnitMsr"" " +
           " FROM ""OBTN"" T4 " +
           " INNER Join ""OBBQ"" T5 ON T4.""AbsEntry""=T5.""SnBMDAbs"" " +
           " INNER Join ""OBIN"" T6 ON T5.""BinAbs""=T6.""AbsEntry"" " +
           " INNER Join ""OITM"" T7 ON T7.""ItemCode""=T4.""ItemCode"" " +
           " WHERE  t5.""OnHandQty"">0  and t4.""ItemCode""='" + ListOp.CodigoArticulo + "'  and  T6.""BinCode""='" + ListOp.UbicacionOrigen + "'"

            rs = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)

            rs.DoQuery(query)

            If rs.RecordCount > 0 Then

                rs.MoveFirst()
                Dim MasdeUnLote As Boolean = True
                Dim BaselineNumber As Integer = 0
                While (Not rs.EoF)

                    If MasdeUnLote = False Then
                        oDoc.Lines.BatchNumbers.Add()

                    End If
                    MasdeUnLote = False


                    log.escribeMensaje(rs.Fields.Item("DistNumber").Value.ToString + " " + rs.Fields.Item("OnHandQty").Value.ToString)

                    oDoc.Lines.BatchNumbers.BatchNumber = rs.Fields.Item("DistNumber").Value
                    oDoc.Lines.BatchNumbers.Quantity = CType(rs.Fields.Item("OnHandQty").Value, Double)

                    'oDoc.Lines.BinAllocations.SetCurrentLine(0)
                    oDoc.Lines.BinAllocations.SerialAndBatchNumbersBaseLine = BaselineNumber
                    oDoc.Lines.BinAllocations.BinActionType = SAPbobsCOM.BinActionTypeEnum.batFromWarehouse
                    oDoc.Lines.BinAllocations.BinAbsEntry = UbicacionOrigen
                    oDoc.Lines.BinAllocations.Quantity = CType(rs.Fields.Item("OnHandQty").Value, Double)

                    oDoc.Lines.BinAllocations.Add()

                    'oDoc.Lines.BinAllocations.SetCurrentLine(1)
                    oDoc.Lines.BinAllocations.SerialAndBatchNumbersBaseLine = BaselineNumber
                    oDoc.Lines.BinAllocations.BinActionType = SAPbobsCOM.BinActionTypeEnum.batToWarehouse
                    oDoc.Lines.BinAllocations.BinAbsEntry = UbicacionDestino
                    oDoc.Lines.BinAllocations.Quantity = CType(rs.Fields.Item("OnHandQty").Value, Double)

                    oDoc.Lines.BinAllocations.Add()
                    BaselineNumber += 1



                    rs.MoveNext()

                End While


            End If


            If oDoc.Add() = 0 Then

                jRes.Resultado = "Ok"

                If IsNothing(ListOp.NumeroPicking) Then
                Else
                    'actualizamos la lista de picking
                    ' conexiones.ExecuteNonQuery("update pkl1 set u_exo_traslado='Y' where absentry='" + ListOp.NumeroPicking + "' and pickentry='" + ListOp.PickingLinea + "'")
                End If
            Else

                jRes.Resultado = oCompany.GetLastErrorDescription
            End If

            'conexiones.liberaDocumento(oDoc)
            System.Runtime.InteropServices.Marshal.ReleaseComObject(oDoc)
        Catch ex As Exception
            log.escribeMensaje(ex.Message, EXO_Log.EXO_Log.Tipo.error)
            jRes.Resultado = "Error : " + ex.Message
        Finally
            '   EXO_CleanCOM.CLiberaCOM.liberaCOM(CType(oCompany, Object))
            EXO_CleanCOM.CLiberaCOM.liberaCOM(CType(rs, Object))
            ' EXO_CleanCOM.CLiberaCOM.liberaCOM(CType(oDoc, Object))
        End Try

        'conexiones.liberaCompañia(oCompany)

        res = js.Serialize(jRes)

        Return res

    End Function


#End Region

#Region "Picking Multiple"

    Private Function ListasPickingMultiple(BaseDatos As String, Usuario As String, Password As String, log As EXO_Log.EXO_Log) As String

        Dim listado As List(Of ListasPickingMultiple) = New List(Of ListasPickingMultiple)
        Dim res As String = ""
        Dim oPic As ListasPickingMultiple = New ListasPickingMultiple

        Dim oListasPickingDetalle As List(Of ListasPickingDetalle) = New List(Of ListasPickingDetalle)
        Dim oPickingDetalle As ListasPickingDetalle = New ListasPickingDetalle

        Dim Esprimero As Boolean = True

        Dim oCompany As SAPbobsCOM.Company
        oCompany = New SAPbobsCOM.Company
        oCompany = conectaDI(BaseDatos, Usuario, Password)
        EstablecerAlmacen(oCompany)

        Try

            ''hacer consulta al sql y y rellenar el listado
            Dim query As String = "SELECT ""AbsEntry"",""PickDate"",""Remarks"" FROM ""OPKL""  T0 " +
                  " WHERE ""Status"" not in ('C')  and ""Canceled""='N' and COALESCE(""U_EXO_PPIST"",'N')='N' " +
            " and 'Y' = COALESCE((SELECT MAX('Y') from ""PKL1"" AS T1 INNER JOIN ""RDR1"" T2 ON T1.""OrderEntry""=T2.""DocEntry"" and T1.""OrderLine""=T2.""LineNum"" " +
            " WHERE T0.""AbsEntry""=T1.""AbsEntry""  " +
            " And T2.""WhsCode""='" + AlmacenPrincipal + "'),'N') "

            'Dim query As String = "SELECT ""AbsEntry"",""PickDate"",""Remarks"" FROM ""OPKL""  T0 " +
            '  " WHERE ""Status"" not in ('Y','C')  and ""Canceled""='N' and COALESCE(""U_EXO_PPIST"",'N')='N' " +
            '"  And (SELECT COUNT(""AbsEntry"") FROM ""PKL1"" T10 WHERE T0.""AbsEntry""=T10.""AbsEntry"")= " +
            '"    (SELECT COUNT(T2.""LineNum"") from ""PKL1"" AS T1 INNER JOIN ""RDR1"" T2 ON T1.""OrderEntry""=T2.""DocEntry"" and T1.""OrderLine""=T2.""LineNum""  " +
            '" WHERE T0.""AbsEntry""=T1.""AbsEntry""   " +
            '" And T2.""WhsCode""='" + AlmacenPrincipal + "')"

            'recorro y voy rellenando listado 

            Dim rs As SAPbobsCOM.Recordset
            Dim rs2 As SAPbobsCOM.Recordset

            rs = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)

            rs.DoQuery(query)

            If rs.RecordCount > 0 Then

                rs.MoveFirst()

                While (Not rs.EoF)

                    oPic = New ListasPickingMultiple
                    oListasPickingDetalle = New List(Of ListasPickingDetalle)

                    oPic.Resultado = "Ok"
                    oPic.Numero = rs.Fields.Item("AbsEntry").Value.ToString
                    oPic.Fecha = rs.Fields.Item("PickDate").Value.ToString

                    'oPic.Comentario = rs.Fields.Item("Remarks").Value.ToString

                    rs2 = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)

                    query = "select COALESCE(T3.""TrnspName"",'') TrnspName,COALESCE(T4.""CardName"",'') CardName , " +
                        " COALESCE(T2.""PickRmrk"",'')  U_EXO_OBSP, T0.""RelQtty"",T1.""ItemCode"",T1.""Dscription"" " +
                        " , T6.""CardName"" Proveedor, T7.""OnHand"" " +
                  " FROM ""PKL1"" T0 INNER JOIN ""RDR1"" T1 On T0.""OrderEntry""=T1.""DocEntry""   and T1.""LineNum""=T0.""OrderLine"" " +
                  " INNER JOIN ""ORDR"" T2 ON T1.""DocEntry""=T2.""DocEntry""  " +
                  " Left Join ""OSHP"" T3 ON T2.""TrnspCode""=T3.""TrnspCode"" " +
                  " INNER JOIN ""OCRD"" T4 ON T2.""CardCode""=T4.""CardCode""  " +
                  " INNER JOIN ""OITM"" T5 ON T1.""ItemCode""=T5.""ItemCode"" " +
                  " LEFT JOIN ""OCRD"" T6 ON T5.""CardCode""=T6.""CardCode"" " +
                  " INNER JOIN ""OITW"" T7 ON T1.""WhsCode""=T7.""WhsCode"" and T1.""ItemCode""=T7.""ItemCode"" " +
                  " WHERE T0.""AbsEntry""='" + rs.Fields.Item("AbsEntry").Value.ToString + "' "

                    rs2.DoQuery(query)

                    If rs2.RecordCount > 0 Then

                        oPic.Comentario = rs2.Fields.Item("CardName").Value.ToString
                        oPic.Transportista = rs2.Fields.Item("TrnspName").Value.ToString
                        oPic.Observaciones = rs2.Fields.Item("U_EXO_OBSP").Value.ToString
                        oPic.CentroCoste = ""
                        oPic.NumLineas = 0

                        While (Not rs2.EoF)

                            oPickingDetalle = New ListasPickingDetalle

                            oPic.NumLineas = oPic.NumLineas + 1
                            oPickingDetalle.Articulo = rs2.Fields.Item("ItemCode").Value.ToString
                            oPickingDetalle.Descripcion = rs2.Fields.Item("Dscription").Value.ToString
                            oPickingDetalle.Cantidad = Convert.ToDouble(rs2.Fields.Item("RelQtty").Value.ToString)
                            oPickingDetalle.Proveedor = rs2.Fields.Item("Proveedor").Value.ToString
                            oPickingDetalle.Stock = Convert.ToDouble(rs2.Fields.Item("OnHand").Value.ToString)

                            oListasPickingDetalle.Add(oPickingDetalle)

                            rs2.MoveNext()

                        End While

                    End If

                    oPic.Lineas = oListasPickingDetalle


                    listado.Add(oPic)

                    rs.MoveNext()
                End While

            Else

                oPic.Resultado = "No hay Pickings disponibles"
                listado.Add(oPic)

            End If

        Catch ex As Exception
            log.escribeMensaje(ex.Message, EXO_Log.EXO_Log.Tipo.error)
            oPic.Resultado = "Error: " + ex.Message
            listado.Add(oPic)
        End Try

        'liberaCompañia(compañia)
        'conexiones.DisconnectSQLServer()

        Dim js As New JavaScriptSerializer()
        res = js.Serialize(listado)

        Return res

    End Function

    Private Function DesglosePickingMultiple(BaseDatos As String, Usuario As String, Password As String, NumeroPicking As String, log As EXO_Log.EXO_Log) As String

        Dim listado As List(Of PickingMultiple) = New List(Of PickingMultiple)
        Dim res As String = ""
        Dim oPic As PickingMultiple = New PickingMultiple

        Dim Esprimero As Boolean = True

        Dim oCompany As SAPbobsCOM.Company
        oCompany = New SAPbobsCOM.Company
        oCompany = conectaDI(BaseDatos, Usuario, Password)
        EstablecerAlmacen(oCompany)
        Dim ubicacionBahia As String = ""
        Dim js As New JavaScriptSerializer()

        Try

            Dim rs As SAPbobsCOM.Recordset

            rs = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)

            'como la bahia va automatica se la paso a Jaime a traves del desglose picking

            Dim query As String = "SELECT T4.""BinCode"" " +
                " from ""PKL1"" AS T1 INNER JOIN ""RDR1"" T2 ON T1.""OrderEntry""=T2.""DocEntry"" and T1.""OrderLine""=T2.""LineNum"" " +
                " inner join ""OBIN"" T4 ON T2.""WhsCode""=T4.""WhsCode"" " +
                " And T1.""AbsEntry""='" + NumeroPicking + "' AND ""U_EXO_ESBAHIA""='Y' "

            rs.DoQuery(query)

            If rs.RecordCount > 0 Then

                rs.MoveFirst()

                If rs.Fields.Item("BinCode").Value.ToString <> "" Then

                    ubicacionBahia = rs.Fields.Item("BinCode").Value.ToString

                Else
                    oPic.Resultado = "Error: El almacen del picking No tiene bahia asignada "
                    listado.Add(oPic)
                    res = js.Serialize(listado)
                    Return res
                End If
            Else

                oPic.Resultado = "Error: El almacen del picking No tiene bahia asignada "
                listado.Add(oPic)
                res = js.Serialize(listado)
                Return res

            End If

            query = "CALL EXO_GP_TRABAJO_LISTA_PICKING_MULTIPLE(" + NumeroPicking + ", NULL) "



            rs.DoQuery(query)

            If rs.RecordCount > 0 Then

                rs.MoveFirst()

                While (Not rs.EoF)

                    oPic = New PickingMultiple

                    oPic.Resultado = "Ok"
                    oPic.PickingLinea = rs.Fields.Item("PICKENTRY").Value.ToString
                    oPic.Articulo = rs.Fields.Item("ITEMCODE").Value.ToString
                    oPic.Descripcion = rs.Fields.Item("ITEMNAME").Value.ToString

                    oPic.CantidadTotal = CType(rs.Fields.Item("CANTIDADTOTAL").Value.ToString, Double) / CType(rs.Fields.Item("NUMPERMSR").Value.ToString, Double)
                    oPic.Cantidad = CType(rs.Fields.Item("CANTIDAD").Value.ToString, Double) / CType(rs.Fields.Item("NUMPERMSR").Value.ToString, Double)

                    oPic.CantidadUDM = CType(rs.Fields.Item("NUMPERMSR").Value.ToString, Double)
                    oPic.UnidadMedida = rs.Fields.Item("UDM").Value.ToString
                    oPic.Lote = rs.Fields.Item("BATCHNUM").Value.ToString
                    oPic.Ubicacion = rs.Fields.Item("BINCODE").Value.ToString()
                    oPic.UbicacionPropuesta = rs.Fields.Item("PROPUESTO").Value.ToString()
                    oPic.EsLote = rs.Fields.Item("ESLOTE").Value.ToString
                    oPic.Procesado = rs.Fields.Item("SEPUEDEGESTIONAR").Value.ToString()

                    oPic.UbicacionBahia = ubicacionBahia
                    oPic.Orden = rs.Fields.Item("ALTSORTCOD").Value.ToString()
                    oPic.CantidadPicking = CType(rs.Fields.Item("CANTIDADPICK").Value.ToString, Double) / CType(rs.Fields.Item("NUMPERMSR").Value.ToString, Double)
                    listado.Add(oPic)


                    rs.MoveNext()
                End While

            Else

                oPic.Resultado = "Error no hay datos coincidentes"
                listado.Add(oPic)

            End If

        Catch ex As Exception
            log.escribeMensaje(ex.Message, EXO_Log.EXO_Log.Tipo.error)
            oPic.Resultado = "Error: " + ex.Message
            listado.Add(oPic)
        End Try

        'liberaCompañia(compañia)
        'conexiones.DisconnectSQLServer()


        res = js.Serialize(listado)

        Return res

    End Function

#End Region

#Region "Consulta Stock"

    Private Function ConsultaStock(BaseDatos As String, Usuario As String, Password As String, Filtro As String, log As EXO_Log.EXO_Log) As String


        Dim js As New JavaScriptSerializer()
        Dim res As String = ""

        Dim listado As List(Of Stock) = New List(Of Stock)

        Dim oAlb As Stock = New Stock
        Dim strOrdenacion As String = ""
        Dim strComprobacion As String = ""

        Dim rs As SAPbobsCOM.Recordset

        Dim oCompany As SAPbobsCOM.Company
        oCompany = New SAPbobsCOM.Company
        oCompany = conectaDI(BaseDatos, Usuario, Password)
        EstablecerAlmacen(oCompany)

        Try

            'reviso query
            Dim CodEanConversion = Filtro
            'If Len(Filtro) < 15 Then
            '    CodEanConversion = Filtro
            'ElseIf Len(Filtro) = 15 Then
            '    '  'If Len(CodEan) < 16 Then
            '    CodEanConversion = Filtro.Substring(2, 13)
            'Else
            '    CodEanConversion = Filtro.Substring(2, 14)
            'End If
            'query con el detalle de todo lo que tiene el borrador, cantidades y lotes
            Dim query As String = "Select t5.""ItemCode"", t5.""ItemName"", T1.""DistNumber"", T0.""OnHandQty"", T2.""BinCode"", cast(t1.""Notes"" As nvarchar(1000))  ""Notes"" " +
                        " , Case When T1.""Status""=0 Then 'Liberado' when T1.""Status""=1 then 'Denegado' else 'Bloqueado' end  ""Estatus"",T1.""InDate"",t5.""InvntryUom"" " +
                        " from """ + BaseDatos + """.""OBBQ"" T0 " +
                        " INNER JOIN """ + BaseDatos + """.""OBTN"" T1 ON T0.""SnBMDAbs"" = T1.""AbsEntry"" AND T0.""ItemCode"" = T1.""ItemCode"" " +
                        " INNER JOIN """ + BaseDatos + """.""OBIN"" T2 ON T2.""AbsEntry"" = T0.""BinAbs"" " +
                        " INNER JOIN """ + BaseDatos + """.""OITM"" T5 ON T5.""ItemCode""=T1.""ItemCode"" " +
                        " WHERE (T0.""ItemCode"" = '" + Filtro + "' or t1.""DistNumber"" = '" + Filtro + "' or  t5.""CodeBars""='" + CodEanConversion + "' or t2.""BinCode""='" + Filtro + "') " +
                        " and coalesce(T0.""OnHandQty"", 0) >  0 " +
              " GROUP BY t5.""ItemCode"",t5.""ItemName"",T1.""DistNumber"", T0.""OnHandQty"", T2.""BinCode"",t1.""Status"" ,t1.""InDate"" ,cast(t1.""Notes"" as nvarchar(1000)),t5.""InvntryUom"" "
            '" order by isnull(T0.OnHandQty, 0), T1.InDate DESC, T1.DistNumber DESC "

            'union all y la mismsa consulta de comprobar lote que no tienen lote con la oibq SILVIA
            query = query & " UNION ALL " +
                " SELECT t5.""ItemCode"", t5.""ItemName"",'' ""DistNumber"", IFNULL(T0.""OnHandQty"",0)  ""OnHandQty"", T2.""BinCode"",  '' ""Notes""  , " +
                " 'Liberado' ""Estatus""  ,T2.""CreateDate"" ""InDate"",t5.""InvntryUom"" " +
                " From """ + BaseDatos + """.""OIBQ"" T0  " +
                " INNER JOIN """ + BaseDatos + """.""OBIN"" T2 ON T2.""AbsEntry"" = T0.""BinAbs"" And T2.""WhsCode"" = T0.""WhsCode""   " +
                " INNER JOIN """ + BaseDatos + """.""OITM"" T5 ON T5.""ItemCode""=T0.""ItemCode"" " +
                "  WHERE  " +
                " (T0.""ItemCode"" = '" + Filtro + "'  or  t5.""CodeBars""='" + CodEanConversion + "' or t2.""BinCode""='" + Filtro + "')   " +
                "  And IFNULL(T0.""OnHandQty"", 0) >  0  and T5.""ManBtchNum""='N' " +
                " group by t5.""ItemCode"",t5.""ItemName"", T0.""OnHandQty"", T2.""BinCode"",T2.""CreateDate"",t5.""InvntryUom"" "


            strOrdenacion = "ORDER BY t5.""ItemCode"" asc, ""InDate"" asc, ""DistNumber"" ASC "

            query = query & strOrdenacion
            '" ORDER BY iNDATE asc,t2.bincode asc,t5.itemcode asc,DistNumber ASC "


            'COMENTADO POR MANU
            '" And " +
            '" T0.ItemCode + '#' + T1.DistNumber NOT IN (SELECT TLote.ItemCode + '#' + TLote.BatchNum FROM OIBT TLote WHERE TLote.ItemCode = T0.ItemCode AND ISNULL(TLote.IsCommited, 0) <> 0) " +
            rs = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)

            rs.DoQuery(query)

            If rs.RecordCount > 0 Then
                rs.MoveFirst()

                While (Not rs.EoF)

                    oAlb = New Stock

                    oAlb.Resultado = "Ok"

                    oAlb.Codigo = rs.Fields.Item("ItemCode").Value.ToString()
                    oAlb.Descripcion = rs.Fields.Item("ItemName").Value.ToString()

                    oAlb.Cantidad = CType(rs.Fields.Item("OnHandQty").Value.ToString(), Double)
                    oAlb.Lote = rs.Fields.Item("DistNumber").Value.ToString()
                    oAlb.Ubicacion = rs.Fields.Item("BinCode").Value.ToString()
                    oAlb.Estatus = rs.Fields.Item("Estatus").Value.ToString()
                    oAlb.InfoDetallada = rs.Fields.Item("Notes").Value.ToString()
                    'añadir ubicacion, UniMedida
                    oAlb.UnidadMedida = rs.Fields.Item("InvntryUom").Value.ToString()
                    listado.Add(oAlb)

                    rs.MoveNext()
                End While
            End If

        Catch ex As Exception
            log.escribeMensaje(ex.Message, EXO_Log.EXO_Log.Tipo.error)
            oAlb.Resultado = "Error. " + ex.Message
            listado.Add(oAlb)
        End Try

        res = js.Serialize(listado)

        Return res

    End Function

#End Region

#Region "Regularizacion de stock"

    'ESTA LLAMADA SE UTILIZA, EN ENTRADAS, SALIDAS Y TRASLADOS
    Public Function ComprobarExisteArticulo(BaseDatos As String, Usuario As String, Password As String, CodEan As String, log As EXO_Log.EXO_Log) As String

        Dim listado As List(Of Articulo) = New List(Of Articulo)
        Dim res As String = ""
        Dim oPed As Articulo = New Articulo

        Dim Esprimero As Boolean = True

        'conexiones.ConnectSQLServer(BaseDatos)
        Dim oCompany As SAPbobsCOM.Company
        oCompany = New SAPbobsCOM.Company

        'log.escribeMensaje(BaseDatos, EXO_Log.EXO_Log.Tipo.informacion)
        'log.escribeMensaje(Usuario, EXO_Log.EXO_Log.Tipo.informacion)
        'log.escribeMensaje(Password, EXO_Log.EXO_Log.Tipo.informacion)
        'log.escribeMensaje(CodEan, EXO_Log.EXO_Log.Tipo.informacion)

        oCompany = conectaDI(BaseDatos, Usuario, Password)


        EstablecerAlmacen(oCompany)

        Try

            Dim CodEanConversion As String = ""
            Dim NumLote As String = ""

            CodEanConversion = CodEan
            NumLote = ""
            'If Len(CodEan) < 15 Then
            '    CodEanConversion = CodEan
            'ElseIf Len(CodEan) = 15 Then
            '    'If Len(CodEan) < 16 Then
            '    CodEanConversion = CodEan.Substring(2, 13)
            '    NumLote = ""
            'Else
            '    CodEanConversion = CodEan.Substring(2, 14)
            '    If Len(CodEan) > 18 Then
            '        'si es ean 128 hay que desglosar el código y luego generar la consulta
            '        NumLote = CodEan.Substring(18, CodEan.Length - 18)
            '    End If
            'End If


            'CONSULTA EN HANA UDM COMPRAS
            'Dim query As String = " SELECT T2.""ItemCode"",T2.""ItemName"", " +
            '                    " Case WHEN COALESCE(T2.""ManBtchNum"",'N') = 'N' THEN 'N' ELSE 'Y' END as ""EsLote"", " +
            '                    " T2.""BHeight1"" As ""Alto"", T2.""BWidth1"" As ""Ancho"",T2.""BLength1"" As ""Largo"",T2.""BWeight1"" As ""Peso"",T2.""BuyUnitMsr"" " +
            '                    " , COALESCE(T4.""BcdCode"",'') as ""Ean14"",COALESCE(T2.""NumInBuy"",1) as ""CantidadUDM"" " +
            '                    " FROM ""OITM"" T2  LEFT JOIN ""OBCD"" T4 ON T2.""PUoMEntry""=T4.""UomEntry"" AND T2.""ItemCode""=T4.""ItemCode"" " +
            '                    " WHERE 1=1 "

            'UDM VENTAS
            Dim query As String = " SELECT T2.""ItemCode"",T2.""ItemName"", " +
                                " Case WHEN COALESCE(T2.""ManBtchNum"",'N') = 'N' THEN 'N' ELSE 'Y' END as ""EsLote"", " +
                                " T2.""SHeight1"" As ""Alto"", T2.""SWidth1"" As ""Ancho"",T2.""SLength1"" As ""Largo"",T2.""SWeight1"" As ""Peso"",T2.""SalUnitMsr"" " +
                                " , COALESCE(T4.""BcdCode"",'') as ""Ean14"",COALESCE(T2.""NumInSale"",1) as ""CantidadUDM"" " +
                                " FROM ""OITM"" T2  LEFT JOIN ""OBCD"" T4 ON T2.""PUoMEntry""=T4.""UomEntry"" AND T2.""ItemCode""=T4.""ItemCode"" " +
                                " WHERE 1=1 "


            If CodEan <> "" Then
                query = query + " and ((T2.""CodeBars"" = '" + CodEanConversion + "' AND T2.""UgpEntry""='-1') OR COALESCE(T4.""BcdCode"",'')='" + CodEanConversion + "') "
            End If

            Dim rs As SAPbobsCOM.Recordset

            rs = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)

            rs.DoQuery(query)

            If rs.RecordCount > 0 Then

                rs.MoveFirst()

                listado = New List(Of Articulo)
                While (Not rs.EoF)

                    oPed = New Articulo


                    oPed.Resultado = "Ok"

                    If rs.Fields.Item("EsLote").Value.ToString = "N" And NumLote <> "" Then

                        oPed.Resultado = "Error: Articulo " + rs.Fields.Item("ItemCode").Value.ToString + " configurado sin lote y etiqueta leida con lote. "

                        listado.Add(oPed)
                        Exit While

                    ElseIf rs.Fields.Item("EsLote").Value.ToString = "Y" And NumLote = "" Then

                    End If

                    oPed.Articulo = rs.Fields.Item("ItemCode").Value.ToString
                    oPed.Descripcion = rs.Fields.Item("ItemName").Value.ToString
                    oPed.EsLote = rs.Fields.Item("EsLote").Value.ToString
                    oPed.Largo = rs.Fields.Item("Largo").Value.ToString
                    oPed.Peso = rs.Fields.Item("Peso").Value.ToString
                    oPed.Alto = rs.Fields.Item("Alto").Value.ToString
                    oPed.Ancho = rs.Fields.Item("Ancho").Value.ToString
                    oPed.UnidadMedida = rs.Fields.Item("SalUnitMsr").Value.ToString
                    oPed.Lote = NumLote
                    oPed.CantidadUDM = CType(rs.Fields.Item("CantidadUDM").Value.ToString, Double)
                    oPed.Ubicacion = ""
                    oPed.Cantidad = 0

                    listado.Add(oPed)

                    rs.MoveNext()
                End While

            Else

                oPed.Resultado = "Error: no hay datos coincidentes"

                listado.Add(oPed)

            End If

        Catch ex As Exception

            log.escribeMensaje(ex.Message, EXO_Log.EXO_Log.Tipo.error)
            oPed.Resultado = "Error: " + ex.Message

            listado.Add(oPed)
        End Try

        'liberaCompañia(compañia)
        'conexiones.DisconnectSQLServer()

        Dim js As New JavaScriptSerializer()
        res = js.Serialize(listado)

        Return res

    End Function

    Public Function ComprobarArticuloSalida(BaseDatos As String, Usuario As String, Password As String, Articulo As String, Lote As String, Cantidad As Double, Ubicacion As String, log As EXO_Log.EXO_Log) As String

        Dim listado As List(Of Resultado) = New List(Of Resultado)
        Dim res As String = ""
        Dim oPed As Resultado = New Resultado

        Dim Esprimero As Boolean = True


        'conexiones.ConnectSQLServer(BaseDatos)
        Dim rs As SAPbobsCOM.Recordset
        Dim oCompany As SAPbobsCOM.Company
        oCompany = New SAPbobsCOM.Company
        oCompany = conectaDI(BaseDatos, Usuario, Password)
        EstablecerAlmacen(oCompany)
        Dim almacen As String = ""
        Dim js As New JavaScriptSerializer()
        Try

            Dim query As String = ""


            query = "SELECT ""AbsEntry"",""WhsCode"" FROM ""OBIN"" WHERE ""BinCode""='" + Ubicacion + "'"
            rs = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
            rs.DoQuery(query)

            If rs.RecordCount > 0 Then
                rs.MoveFirst()

                almacen = rs.Fields.Item("WhsCode").Value.ToString()
            Else
                'log.escribeMensaje(ex.Message, EXO_Log.EXO_Log.Tipo.error)
                oPed.Resultado = "Error: La ubicacion destino no existe " + Ubicacion
                listado.Add(oPed)
                res = js.Serialize(listado)

                Return res
            End If



            If Lote = "" Or Lote = "0" Then

                query = "SELECT '' DistNumber, T0.""OnHandQty"" OnHandQty, T2.""BinCode"" BinCode " +
                        " FROM ""OIBQ"" T0 " +
                        " INNER JOIN ""OBIN"" T2 ON T2.""AbsEntry"" = T0.""BinAbs"" " +
                        " WHERE T0.""ItemCode"" = '" + Articulo + "' AND T0.""WhsCode"" = '" + almacen + "'  and T2.""BinCode"" ='" + Ubicacion + "'"

            Else

                query = "SELECT T1.""DistNumber"" DistNumber, T0.""OnHandQty"" OnHandQty, T2.""BinCode"" BinCode " +
                        " FROM ""OBBQ"" T0 " +
                        " INNER JOIN ""OBTN"" T1 ON T0.""SnBMDAbs"" = T1.""AbsEntry"" AND T0.""ItemCode"" = T1.""ItemCode"" " +
                        " INNER JOIN ""OBIN"" T2 ON T2.""AbsEntry"" = T0.""BinAbs""  " +
                        " WHERE T0.""ItemCode"" = '" + Articulo + "' AND T0.""WhsCode"" = '" + almacen + "'  and T2.""BinCode"" ='" + Ubicacion + "' " +
                        " AND T1.""DistNumber"" = '" + Lote + "'"
            End If

            rs = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)



            rs.DoQuery(query)

            If rs.RecordCount > 0 Then


                rs.MoveFirst()

                While (Not rs.EoF)

                    oPed = New Resultado

                    oPed.Resultado = "Ok"

                    If CType(rs.Fields.Item("OnHandQty").Value, Double) < Cantidad Then
                        oPed.Resultado = "Error No hay stock suficiente para realizar la salida, cantidad maxima " + rs.Fields.Item("OnHandQty").Value.ToString
                    End If

                    listado.Add(oPed)

                    rs.MoveNext()
                End While

            Else

                oPed.Resultado = "Error: No se ha encontrado el artículo en la ubicación seleccionada."
                listado.Add(oPed)

            End If

        Catch ex As Exception
            log.escribeMensaje(ex.Message, EXO_Log.EXO_Log.Tipo.error)
            oPed.Resultado = "Error: " + ex.Message
            listado.Add(oPed)
        End Try

        'liberaCompañia(compañia)
        'conexiones.DisconnectSQLServer()

        res = js.Serialize(listado)

        Return res

    End Function

    Public Function GenerarDocumentoEntradaSalidaManual(JSON As String, BaseDatos As String, Usuario As String, Password As String, TipoDoc As String, log As EXO_Log.EXO_Log) As String

        Dim ListOp As OperacionEntradaSalida = New OperacionEntradaSalida

        Dim jRes As Resultado = New Resultado
        Dim res As String = ""
        Dim bPrimero As Boolean = True
        Dim js As New JavaScriptSerializer()
        Dim EntregasGeneradas As String = ""
        ListOp = js.Deserialize(Of OperacionEntradaSalida)(JSON)
        Dim sdocnum As String = ""

        Dim oCompany As SAPbobsCOM.Company
        oCompany = New SAPbobsCOM.Company
        oCompany = conectaDI(BaseDatos, Usuario, Password)
        EstablecerAlmacen(oCompany)

        Try

            Dim oDoc As SAPbobsCOM.Documents

            If TipoDoc = "Entrada" Then
                oDoc = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oInventoryGenEntry)
            Else
                oDoc = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oInventoryGenExit)
            End If


            Dim esPrimeraLinea As Boolean = True

            For Each Linea As Articulo In ListOp.Lineas

                If esPrimeraLinea = False Then
                    oDoc.Lines.Add()
                Else
                    esPrimeraLinea = False
                End If

                oDoc.Lines.Quantity = Linea.Cantidad
                oDoc.Lines.ItemCode = Linea.Articulo

                log.escribeMensaje(AlmacenPrincipal, EXO_Log.EXO_Log.Tipo.informacion)
                'Con este comando activamos si queremos usar la unidad de inventario o la del articulo
                'si es la del articulo hay que poner en la ubicacion y en los lotes la cantidad total, buscar en el articulo la conversión. NumInBuy or NumInSale
                'oDoc.Lines.UseBaseUnits = BoYesNoEnum.tNO


                Dim query As String = "SELECT ""AbsEntry"",""WhsCode"" FROM ""OBIN"" WHERE ""BinCode""='" + Linea.Ubicacion + "'"
                Dim rs As SAPbobsCOM.Recordset
                Dim sUbicacion As String = ""
                rs = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)

                rs.DoQuery(query)


                If rs.RecordCount > 0 Then
                    sUbicacion = rs.Fields.Item("AbsEntry").Value.ToString()
                    log.escribeMensaje(sUbicacion, EXO_Log.EXO_Log.Tipo.informacion)
                Else

                    jRes.Resultado = "Error: No existe la ubicación " + Linea.Ubicacion.ToString

                    res = js.Serialize(jRes)

                    Return res
                End If

                oDoc.Lines.WarehouseCode = rs.Fields.Item("WhsCode").Value.ToString()

                'MANU PARA CADA ARTICULO HAY QUE BUSCAR JUNTO CON SU UNIDAD DE MEDIDA, LA CANTIDAD A MULTIPLICAR EN EL LOTES


                If Linea.Lote <> "" Then
                    'oDoc.Lines.BatchNumbers.BaseLineNumber = 0
                    oDoc.Lines.BatchNumbers.BatchNumber = Linea.Lote
                    oDoc.Lines.BatchNumbers.Quantity = Linea.Cantidad
                    oDoc.Lines.BatchNumbers.Add()

                    'oDoc.Lines.BinAllocations.SetCurrentLine(0)
                    oDoc.Lines.BinAllocations.SerialAndBatchNumbersBaseLine = 0
                    oDoc.Lines.BinAllocations.BinAbsEntry = sUbicacion
                    oDoc.Lines.BinAllocations.Quantity = Linea.Cantidad
                    oDoc.Lines.BinAllocations.Add()
                Else
                    oDoc.Lines.BinAllocations.BinAbsEntry = sUbicacion
                    oDoc.Lines.BinAllocations.Quantity = Linea.Cantidad
                    oDoc.Lines.BinAllocations.Add()
                End If
            Next

            If oDoc.Add() = 0 Then

                Dim documento As String = oCompany.GetNewObjectKey()
                jRes.Resultado = "Ok: Documento " + documento.ToString + " de " + TipoDoc + " generado correctamente."
            Else
                jRes.Resultado = "Error: " + oCompany.GetLastErrorDescription

                res = js.Serialize(jRes)
                Return res

            End If

        Catch ex As Exception

            log.escribeMensaje(ex.Message, EXO_Log.EXO_Log.Tipo.error)
            jRes.Resultado = "Error: " + ex.Message

        End Try

        res = js.Serialize(jRes)

        Return res

    End Function

#End Region

#Region "No usar"

    ''' NO USAR GENERARPICKING, ERROR EN EL WEBSERVICE DE LA WEB, SE UTILIZA GENERAR PICKING2
    Private Function GenerarPicking(JSON As String, BaseDatos As String, Usuario As String, Password As String, log As EXO_Log.EXO_Log) As String

        'falta poner el transaction
        log.escribeMensaje("1", EXO_Log.EXO_Log.Tipo.informacion)


        Dim ListOp As GenerarPicking = New GenerarPicking

        Dim jRes As Resultado = New Resultado

        Dim res As String = ""
        Dim bPrimero As Boolean = True

        Dim js As New JavaScriptSerializer()
        log.escribeMensaje("2", EXO_Log.EXO_Log.Tipo.informacion)
        Dim EntregasGeneradas As String = ""

        'ListOp = js.Deserialize(Of GenerarPicking)(JSON)

        'Dim sdocnum As String = ""

        'Dim oCompany As SAPbobsCOM.Company
        'oCompany = New SAPbobsCOM.Company
        'oCompany = conectaDI(BaseDatos,Usuario, Password)

        Try


            ListOp = js.Deserialize(Of GenerarPicking)(JSON)
            log.escribeMensaje("3", EXO_Log.EXO_Log.Tipo.informacion)
            Dim sdocnum As String = ""

            Dim oCompany As SAPbobsCOM.Company
            oCompany = New SAPbobsCOM.Company
            oCompany = conectaDI(BaseDatos, Usuario, Password)
            EstablecerAlmacen(oCompany)
            log.escribeMensaje("4", EXO_Log.EXO_Log.Tipo.informacion)
            'If oCompany.InTransaction = True Then
            '    oCompany.EndTransaction(BoWfTransOpt.wf_RollBack)
            'End If

            'oCompany.StartTransaction()
            jRes.Resultado = ""
            '------------LOS LOTES HAY QUE ASIGNARLOS PRIMERO EN EL PEDIDO-------------------
            Dim query As String = "SELECT ""AbsEntry"" FROM ""OBIN"" WHERE ""BinCode""='" + ListOp.Ubicacion + "'"
            Dim rs As SAPbobsCOM.Recordset
            rs = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
            rs.DoQuery(query)

            Dim UbicacionBahia As String = ""

            If rs.RecordCount > 0 Then
                rs.MoveFirst()
                UbicacionBahia = rs.Fields.Item("AbsEntry").Value.ToString()
            End If

            'hay que asignar a los pedidos primero.
            Dim sql As String = "SELECT T0.""AbsEntry"",T0.""PickEntry"",T0.""OrderEntry"",T0.""OrderLine"" " +
                    " FROM ""PKL1"" T0  INNER JOIN ""RDR1"" T1 ON  T1.""DocEntry""=T0.""OrderEntry"" and   T1.""LineNum""=T0.""OrderLine"" " +
                    " INNER JOIN ""OITM"" T2 ON T1.""ItemCode""=T2.""ItemCode"" " +
                    " WHERE T0.""AbsEntry"" = " + ListOp.NumeroPicking + " and T2.""ManBtchNum""='Y' and T0.""PickStatus""<>'Y'" +
                    " ORDER BY T0.""OrderEntry"",T0.""OrderLine"" "

            rs.DoQuery(sql)

            Dim order As SAPbobsCOM.Documents = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oOrders)
            Dim DocPedido As String = ""
            Dim EsNuevo As Boolean = True
            Dim setCurrent2 As Boolean = True

            Dim baselinenumber2 As Integer = 0
            Dim noesloteprimero2 As Boolean = True
            Dim cantidadTotal2 As Double = 0

            Dim AnyadeRegistros2 As Boolean = True

            If rs.RecordCount > 0 Then
                rs.MoveFirst()
                While (Not rs.EoF)

                    If DocPedido = "" Then

                        EsNuevo = True
                        DocPedido = rs.Fields.Item("OrderEntry").Value.ToString()
                        order.GetByKey(rs.Fields.Item("OrderEntry").Value.ToString())

                    ElseIf DocPedido <> rs.Fields.Item("OrderEntry").Value.ToString() Then
                        'Actualizamos el pedido

                        DocPedido = rs.Fields.Item("OrderEntry").Value.ToString()
                        If order.Update() = 0 Then

                        Else
                            jRes.Resultado = oCompany.GetLastErrorDescription

                            If oCompany.InTransaction = True Then
                                oCompany.EndTransaction(BoWfTransOpt.wf_RollBack)
                            End If
                            ' conexiones.liberaCompañia(oCompany)
                            res = js.Serialize(jRes)
                            Return res
                        End If
                        order.GetByKey(rs.Fields.Item("OrderEntry").Value.ToString())
                        EsNuevo = True
                    Else
                        EsNuevo = False
                    End If

                    For Each Linea As LineasPicking In ListOp.Lineas
                        'encontramos la linea y trabajamos con listop

                        If CType(rs.Fields.Item("PickEntry").Value.ToString, Integer) = Linea.PickingLinea Then

                            If setCurrent2 = True Then
                                order.Lines.SetCurrentLine(CType(rs.Fields.Item("OrderLine").Value.ToString, Integer))
                                setCurrent2 = False
                            End If

                            cantidadTotal2 = cantidadTotal2 + Linea.Cantidad

                            'oPick.Lines.BinAllocations.BinAbsEntry = UbicacionBahia

                            If Not AnyadeRegistros2 Then
                                order.Lines.BatchNumbers.Add()
                                order.Lines.BinAllocations.Add()
                            End If

                            order.Lines.BatchNumbers.BaseLineNumber = CType(rs.Fields.Item("OrderLine").Value.ToString, Integer)
                            order.Lines.BatchNumbers.BatchNumber = Linea.Lote
                            order.Lines.BatchNumbers.Quantity = Linea.Cantidad

                            AnyadeRegistros2 = False
                            order.Lines.BinAllocations.AllowNegativeQuantity = BoYesNoEnum.tYES
                            order.Lines.BinAllocations.BaseLineNumber = CType(rs.Fields.Item("OrderLine").Value.ToString, Integer)
                            order.Lines.BinAllocations.SerialAndBatchNumbersBaseLine = baselinenumber2
                            order.Lines.BinAllocations.BinAbsEntry = UbicacionBahia
                            order.Lines.BinAllocations.Quantity = Linea.Cantidad

                            noesloteprimero2 = False

                            baselinenumber2 += 1

                        End If
                    Next

                    rs.MoveNext()
                End While

                If order.Update() = 0 Then
                    jRes.Resultado = "Ok"
                Else
                    jRes.Resultado = oCompany.GetLastErrorDescription

                    'If oCompany.InTransaction = True Then
                    '    oCompany.EndTransaction(BoWfTransOpt.wf_RollBack)
                    'End If

                    ' conexiones.liberaCompañia(oCompany)
                    res = js.Serialize(jRes)
                    Return res

                End If
            End If


            '------------YA SE PUEDE HACER EL PICKING DE TODO-------------------
            Dim oPick As SAPbobsCOM.PickLists = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oPickLists)

            'consulta de lineas y documentos del picking
            sql = "SELECT T0.""AbsEntry"",T0.""PickEntry"",T0.""OrderEntry"",T0.""OrderLine"" " +
                    " FROM ""PKL1"" T0  " +
                    " WHERE T0.""AbsEntry"" = " + ListOp.NumeroPicking + " and ""PickStatus""<>'Y'" +
                    " ORDER BY t0.""PickEntry"" "

            rs.DoQuery(sql)

            'If oCompany.InTransaction = True Then
            '    oCompany.EndTransaction(BoWfTransOpt.wf_RollBack)
            'End If

            oPick.GetByKey(ListOp.NumeroPicking)
            If rs.RecordCount > 0 Then
                rs.MoveFirst()
                While (Not rs.EoF)

                    Dim baselinenumber As Integer = 0
                    Dim noesloteprimero As Boolean = True
                    Dim cantidadTotal As Double = 0
                    Dim setCurrent As Boolean = True
                    Dim AnyadeRegistros As Boolean = True

                    For Each Linea As LineasPicking In ListOp.Lineas
                        'encontramos la linea y trabajamos con listop

                        If CType(rs.Fields.Item("PickEntry").Value.ToString, Integer) = Linea.PickingLinea Then


                            If setCurrent = True Then
                                oPick.Lines.SetCurrentLine(Linea.PickingLinea)
                                setCurrent = False
                            End If

                            cantidadTotal = cantidadTotal + Linea.Cantidad

                            'oPick.Lines.BinAllocations.BinAbsEntry = UbicacionBahia

                            If Not AnyadeRegistros Then

                                oPick.Lines.BatchNumbers.Add()
                                oPick.Lines.BinAllocations.Add()
                            End If

                            If Linea.Lote <> "" Then
                                oPick.Lines.BatchNumbers.BaseLineNumber = Linea.PickingLinea
                                oPick.Lines.BatchNumbers.BatchNumber = Linea.Lote
                                oPick.Lines.BatchNumbers.Quantity = Linea.Cantidad

                                AnyadeRegistros = False
                                oPick.Lines.BinAllocations.AllowNegativeQuantity = BoYesNoEnum.tYES
                                oPick.Lines.BinAllocations.BaseLineNumber = Linea.PickingLinea
                                oPick.Lines.BinAllocations.SerialAndBatchNumbersBaseLine = baselinenumber
                                oPick.Lines.BinAllocations.BinAbsEntry = UbicacionBahia
                                oPick.Lines.BinAllocations.Quantity = Linea.Cantidad


                            Else
                                noesloteprimero = False
                            End If

                            baselinenumber += 1

                        End If
                    Next

                    If noesloteprimero = False Then
                        'oDoc.Lines.BinAllocations.SerialAndBatchNumbersBaseLine = 0
                        oPick.Lines.BinAllocations.BinAbsEntry = UbicacionBahia
                        oPick.Lines.BinAllocations.Quantity = cantidadTotal
                        'oPick.Lines.BinAllocations.Add()
                    Else

                    End If

                    'oPick.Lines.PickedQuantity = cantidadTotal

                    rs.MoveNext()
                End While
            End If


            If oPick.Update() = 0 Then
                jRes.Resultado = "Ok"
                Try
                    If ListOp.Bultos.Count > 0 Then
                        'LLAMO A generar Udo de picking
                        If GenerarBultosPacking(oCompany, ListOp.Bultos, ListOp.NumeroPicking, ListOp.Palets, "0") Then
                        Else
                            jRes.Resultado = "Error Generando el packing"
                        End If
                    End If
                Catch ex As Exception
                    log.escribeMensaje(ex.Message, EXO_Log.EXO_Log.Tipo.error)
                End Try

            Else
                jRes.Resultado = oCompany.GetLastErrorDescription

                'If oCompany.InTransaction = True Then
                '    oCompany.EndTransaction(BoWfTransOpt.wf_RollBack)
                'End If

                ' conexiones.liberaCompañia(oCompany)
                res = js.Serialize(jRes)
                Return res

            End If

        Catch ex As Exception
            log.escribeMensaje(ex.Message, EXO_Log.EXO_Log.Tipo.error)
            jRes.Resultado = "Error. " + ex.Message

            'If oCompany.InTransaction = True Then
            '    oCompany.EndTransaction(BoWfTransOpt.wf_RollBack)
            'End If

        End Try


        'conexiones.liberaCompañia(oCompany)

        res = js.Serialize(jRes)

        Return res

    End Function

    '' No usar, se utiliza pedido compra resgistrarlinea2
    Public Function PedidoCompraRegistrarLinea(BaseDatos As String, Usuario As String, Password As String, JSON As String, log As EXO_Log.EXO_Log) As String

        Dim ListOp As PedidoCompraRegistrarLinea = New PedidoCompraRegistrarLinea
        Dim jRes As Resultado = New Resultado
        Dim res As String = ""
        Dim bPrimero As Boolean = True
        Dim js As New JavaScriptSerializer()
        ListOp = js.Deserialize(Of PedidoCompraRegistrarLinea)(JSON)

        Dim oCompany As SAPbobsCOM.Company
        oCompany = New SAPbobsCOM.Company
        oCompany = conectaDI(BaseDatos, Usuario, Password)
        EstablecerAlmacen(oCompany)

        Try

            Dim query As String = ""
            Dim rs As SAPbobsCOM.Recordset

            'COMPRUEBO PEDIDOS
            If ListOp.CantidadSeleccionada > ListOp.CantidadReal Then
                'comprobar que no hay mas pedidos o mas lineas abiertas
                query = "SELECT COUNT(CONCAT(T1.""DocEntry"",T1.""LineNum"")) AS ""TotalPedidos"" FROM ""OPOR"" T0 INNER JOIN ""POR1"" T1 On T0.""DocEntry""=T1.""DocEntry"" " +
                        "WHERE T1.""ItemCode"" = '" + ListOp.Codigo + "' and T0.""CardCode""='" + ListOp.Proveedor + "' and T1.""LineStatus""='O'"

                rs = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)

                rs.DoQuery(query)

                If rs.RecordCount > 0 Then

                    rs.MoveFirst()

                    If rs.Fields.Item("TotalPedidos").Value > 1 Then

                        jRes.Resultado = "Hay mas lineas abiertas de este artículo. Imposible superar la cantidad permitida."
                        res = js.Serialize(jRes)
                        Return res
                    End If
                End If
            End If

            'ACTUALIZO DATOS ARTICULO
            query = "select CASE WHEN  COALESCE(""BHeight1"",0)=0 OR  COALESCE(""BWidth1"",0)=0 OR COALESCE(""BLength1"",0)=0 OR COALESCE(""BWeight1"",0)=0 THEN 'Y' ELSE 'N' END AS ""Actualizar"",""UgpEntry"" " +
                " FROM ""OITM"" " +
                " WHERE ""ItemCode""='" + ListOp.Codigo + "' "

            rs = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)

            rs.DoQuery(query)

            If rs.RecordCount > 0 Then
                If rs.Fields.Item("Actualizar").Value = "Y" Then

                    'MIRAR COMO MONTAR ESTO POR CULPA DE LOS GRUPOS DE MEDIDAS
                    Dim oOITM As SAPbobsCOM.Items

                    oOITM = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oItems)

                    oOITM.GetByKey(ListOp.Codigo)

                    'oOITM.PurchaseUnitWeight = 6
                    'oOITM.PurchaseWeightUnit = 3
                    Dim DefaultPurchaseUomEntry As Integer = oOITM.DefaultPurchasingUoMEntry
                    If rs.Fields.Item("UgpEntry").Value.ToString <> -1 Then

                        For i = 0 To oOITM.UnitOfMeasurements.Count - 1
                            oOITM.UnitOfMeasurements.SetCurrentLine(i)

                            If oOITM.UnitOfMeasurements.UoMType = ItemUoMTypeEnum.iutPurchasing And oOITM.UnitOfMeasurements.UoMEntry = DefaultPurchaseUomEntry Then

                                oOITM.UnitOfMeasurements.Weight1 = ListOp.Peso
                                oOITM.UnitOfMeasurements.Weight1Unit = 3

                                oOITM.UnitOfMeasurements.Width1 = ListOp.Ancho
                                oOITM.UnitOfMeasurements.Width1Unit = 2

                                oOITM.UnitOfMeasurements.Height1 = ListOp.Alto
                                oOITM.UnitOfMeasurements.Height1Unit = 2

                                oOITM.UnitOfMeasurements.Length1 = ListOp.Largo
                                oOITM.UnitOfMeasurements.Length1Unit = 2

                                oOITM.UnitOfMeasurements.Volume = ListOp.Largo * ListOp.Alto * ListOp.Ancho
                                oOITM.UnitOfMeasurements.VolumeUnit = 2

                            End If
                        Next
                    Else

                        oOITM.PurchaseUnitWeight = ListOp.Peso
                        oOITM.PurchaseWeightUnit = 3
                        oOITM.PurchaseUnitWidth = ListOp.Ancho
                        oOITM.PurchaseWidthUnit = 2
                        oOITM.PurchaseUnitHeight = ListOp.Alto
                        oOITM.PurchaseHeightUnit = 2
                        oOITM.PurchaseUnitLength = ListOp.Largo
                        oOITM.PurchaseLengthUnit = 2

                    End If

                    If oOITM.Update() <> 0 Then
                        Dim err As String = "error" + oCompany.GetLastErrorDescription
                    End If

                    '    Dim Volumen As Double = ListOp.Alto * ListOp.Largo * ListOp.Ancho
                    '    'actualizar oitm
                    '    query = "UPDATE ""OITM"" T2 " +
                    '" SET T2.""BHeight1""=" + ListOp.Alto.ToString + ", T2.""BHght1Unit""=2, " +
                    '    " T2.""BWidth1""=" + ListOp.Ancho.ToString + ", T2.""BWdth1Unit""=2, " +
                    '    " T2.""BLength1""=" + ListOp.Largo.ToString + ", T2.""BLen1Unit""=2, " +
                    '    " T2.""BWeight1""=" + ListOp.Peso.ToString + ",  T2.""BWght1Unit""=3, " +
                    '    " T2.""BVolume""=" + Volumen.ToString + ",T2.""BVolUnit""=2 " +
                    '" WHERE T2.""ItemCode""='" + ListOp.Codigo + "' "


                    '    'actualizar itm12, habría que multiplicar las medidas para obtener el volumen
                    '    If rs.Fields.Item("UgpEntry").Value.ToString <> -1 Then

                    '        query = "UPDATE T2 " +
                    '    " SET T2.""Height1""=" + ListOp.Alto.ToString + ",T2.""Hght1Unit""=2," +
                    '    " T2.""Width1""=" + ListOp.Ancho.ToString + ",T2.""Wdth1Unit""=2, " +
                    '    " T2.""Length1""=" + ListOp.Largo.ToString + ", T2.""Len1Unit""=2, " +
                    '    " T2.""Weight1""=" + ListOp.Peso.ToString + ", T2.""Wght1Unit""=3, " +
                    '    " T2.""Volume""=" + Volumen.ToString + ",T2.""VolUnit""=2 " +
                    '" from ""OITM"" T0 INNER JOIN ""OUOM"" T1 ON T0.""BuyUnitMsr""=T1.""UomCode"" " +
                    '" INNER Join ""ITM12"" T2 ON T0.""ItemCode""=T2.""ItemCode"" And T1.""UomEntry""=T2.""UomEntry"" " +
                    '" WHERE T0.""ItemCode""='" + ListOp.Codigo + "' AND T2.""UomType""='P' "



                End If
            Else
                jRes.Resultado = "Error. El articulo no existe"
                res = js.Serialize(jRes)
                Return res
            End If

            'INSERTO TABLA TEMPORAL
            query = "SELECT MAX(""Code"")+1 AS ""Code"" FROM ""@EXO_GP_PEDCOM"" "

            rs = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)

            rs.DoQuery(query)
            Dim sCode As String = ""
            If rs.RecordCount > 0 Then
                sCode = Right("000000000" + rs.Fields.Item("Code").Value.ToString, 9)
            Else
                sCode = "000000001"
            End If

            query = "INSERT INTO ""@EXO_GP_PEDCOM"" VALUES ('" + sCode + "', '" + sCode + "','" + Usuario + "','" + ListOp.NumInterno + "','" + ListOp.NumLinea + "' " +
            " , '" + ListOp.CantidadSeleccionada + "','" + ListOp.Lote + "','" + ListOp.Ubicacion + "')"
            rs.DoQuery(query)

            jRes.Resultado = "OK"

        Catch ex As Exception
            log.escribeMensaje(ex.Message, EXO_Log.EXO_Log.Tipo.error)
            jRes.Resultado = "Error " + ex.Message
        End Try


        res = js.Serialize(jRes)

        Return res

    End Function

    Public Function GenerarDraftEntrega(BaseDAtos As String, Usuario As String, Password As String, log As EXO_Log.EXO_Log) As String

        Dim oCompany As SAPbobsCOM.Company
        oCompany = New SAPbobsCOM.Company
        oCompany = conectaDI(BaseDAtos, Usuario, Password)
        EstablecerAlmacen(oCompany)


        Dim odraftODLN As SAPbobsCOM.Documents = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oDrafts)

        odraftODLN.DocType = SAPbobsCOM.BoDocumentTypes.dDocument_Items
        odraftODLN.DocObjectCode = SAPbobsCOM.BoObjectTypes.oDeliveryNotes

        odraftODLN.CardCode = "C000001"

        odraftODLN.Lines.BaseEntry = 113
        odraftODLN.Lines.BaseLine = 0
        odraftODLN.Lines.BaseType = SAPbobsCOM.BoObjectTypes.oOrders


        odraftODLN.Lines.BatchNumbers.BatchNumber = "03012018002"
        odraftODLN.Lines.BatchNumbers.Quantity = 500
        odraftODLN.Lines.BatchNumbers.Add()


        'oDoc.Lines.BinAllocations.SetCurrentLine(0)
        odraftODLN.Lines.BinAllocations.SerialAndBatchNumbersBaseLine = 0

        odraftODLN.Lines.BinAllocations.BinAbsEntry = 257
        odraftODLN.Lines.BinAllocations.Quantity = 500
        odraftODLN.Lines.BinAllocations.Add()

        If odraftODLN.Add() = 0 Then
            Return "Ok"
        Else
            Return oCompany.GetLastErrorDescription
        End If


    End Function

    'Private Function GenerarPicking2(JSON As String, BaseDatos As String, Usuario As String, Password As String, log As EXO_Log.EXO_Log) As String

    '    'falta poner el transaction
    '    'log.escribeMensaje(JSON, EXO_Log.EXO_Log.Tipo.informacion)
    '    'log.escribeMensaje(Usuario, EXO_Log.EXO_Log.Tipo.informacion)
    '    'log.escribeMensaje(Password, EXO_Log.EXO_Log.Tipo.informacion)

    '    Dim ListOp As GenerarPicking = New GenerarPicking

    '    Dim jRes As Resultado = New Resultado

    '    Dim res As String = ""
    '    Dim bPrimero As Boolean = True

    '    Dim js As New JavaScriptSerializer()

    '    Dim EntregasGeneradas As String = ""

    '    'ListOp = js.Deserialize(Of GenerarPicking)(JSON)

    '    'Dim sdocnum As String = ""

    '    'Dim oCompany As SAPbobsCOM.Company
    '    'oCompany = New SAPbobsCOM.Company
    '    'oCompany = conectaDI(BaseDatos,Usuario, Password)

    '    Try

    '        ListOp = js.Deserialize(Of GenerarPicking)(JSON)

    '        Dim sdocnum As String = ""

    '        Dim oCompany As SAPbobsCOM.Company
    '        oCompany = New SAPbobsCOM.Company
    '        oCompany = conectaDI(BaseDatos, Usuario, Password)

    '        EstablecerAlmacen(oCompany)

    '        'If oCompany.InTransaction = True Then
    '        '    oCompany.EndTransaction(BoWfTransOpt.wf_RollBack)
    '        'End If

    '        'oCompany.StartTransaction()
    '        jRes.Resultado = ""
    '        '------------LOS LOTES HAY QUE ASIGNARLOS PRIMERO EN EL PEDIDO-------------------
    '        Dim query As String = "SELECT ""AbsEntry"" FROM ""OBIN"" WHERE ""BinCode""='" + ListOp.Ubicacion + "'"
    '        Dim rs As SAPbobsCOM.Recordset
    '        rs = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
    '        rs.DoQuery(query)

    '        Dim UbicacionBahia As String = ""

    '        If rs.RecordCount > 0 Then
    '            rs.MoveFirst()
    '            UbicacionBahia = rs.Fields.Item("AbsEntry").Value.ToString()
    '        End If

    '        'hay que asignar a los pedidos primero.
    '        Dim sql As String = "SELECT T0.""AbsEntry"",T0.""PickEntry"",T0.""OrderEntry"",T0.""OrderLine"" as LineNum,T1.""VisOrder"" as OrderLine " +
    '                " FROM ""PKL1"" T0  INNER JOIN ""RDR1"" T1 ON  T1.""DocEntry""=T0.""OrderEntry"" and   T1.""LineNum""=T0.""OrderLine"" " +
    '                " INNER JOIN ""OITM"" T2 ON T1.""ItemCode""=T2.""ItemCode"" " +
    '                " WHERE T0.""AbsEntry"" = " + ListOp.NumeroPicking + " and T2.""ManBtchNum""='Y' and T0.""PickStatus""<>'Y'" +
    '                " ORDER BY T0.""OrderEntry"",T0.""OrderLine"" "

    '        rs.DoQuery(sql)

    '        Dim order As SAPbobsCOM.Documents = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oOrders)
    '        Dim DocPedido As String = ""
    '        Dim EsNuevo As Boolean = True
    '        Dim setCurrent2 As Boolean = True

    '        Dim baselinenumber2 As Integer = 0
    '        Dim noesloteprimero2 As Boolean = True
    '        Dim cantidadTotal2 As Double = 0

    '        Dim AnyadeRegistros2 As Boolean = True

    '        If rs.RecordCount > 0 Then
    '            rs.MoveFirst()
    '            While (Not rs.EoF)

    '                If DocPedido = "" Then

    '                    EsNuevo = True
    '                    DocPedido = rs.Fields.Item("OrderEntry").Value.ToString()
    '                    order.GetByKey(rs.Fields.Item("OrderEntry").Value.ToString())

    '                ElseIf DocPedido <> rs.Fields.Item("OrderEntry").Value.ToString() Then
    '                    'Actualizamos el pedido

    '                    DocPedido = rs.Fields.Item("OrderEntry").Value.ToString()
    '                    If order.Update() = 0 Then

    '                    Else
    '                        jRes.Resultado = oCompany.GetLastErrorDescription

    '                        If oCompany.InTransaction = True Then
    '                            oCompany.EndTransaction(BoWfTransOpt.wf_RollBack)
    '                        End If
    '                        ' conexiones.liberaCompañia(oCompany)
    '                        res = js.Serialize(jRes)
    '                        Return res
    '                    End If
    '                    order.GetByKey(rs.Fields.Item("OrderEntry").Value.ToString())
    '                    EsNuevo = True
    '                Else
    '                    EsNuevo = False
    '                End If

    '                setCurrent2 = True
    '                AnyadeRegistros2 = True

    '                For Each Linea As LineasPicking In ListOp.Lineas
    '                    'encontramos la linea y trabajamos con listop

    '                    If CType(rs.Fields.Item("PickEntry").Value.ToString, Integer) = Linea.PickingLinea Then

    '                        If setCurrent2 = True Then

    '                            order.Lines.SetCurrentLine(CType(rs.Fields.Item("OrderLine").Value.ToString, Integer))

    '                            setCurrent2 = False
    '                        End If

    '                        log.escribeMensaje("Linea pick " + Linea.PickingLinea.ToString + " lote" + Linea.Lote.ToString + " cantidad " + Linea.Cantidad.ToString, EXO_Log.EXO_Log.Tipo.informacion)

    '                        cantidadTotal2 = cantidadTotal2 + Linea.Cantidad

    '                        'oPick.Lines.BinAllocations.BinAbsEntry = UbicacionBahia

    '                        If Not AnyadeRegistros2 Then
    '                            order.Lines.BatchNumbers.Add()
    '                            order.Lines.BinAllocations.Add()
    '                        End If

    '                        order.Lines.BatchNumbers.BaseLineNumber = CType(rs.Fields.Item("OrderLine").Value.ToString, Integer)
    '                        order.Lines.BatchNumbers.BatchNumber = Linea.Lote
    '                        order.Lines.BatchNumbers.Quantity = Linea.Cantidad

    '                        AnyadeRegistros2 = False
    '                        order.Lines.BinAllocations.AllowNegativeQuantity = BoYesNoEnum.tYES
    '                        order.Lines.BinAllocations.BaseLineNumber = CType(rs.Fields.Item("OrderLine").Value.ToString, Integer)
    '                        order.Lines.BinAllocations.SerialAndBatchNumbersBaseLine = baselinenumber2
    '                        order.Lines.BinAllocations.BinAbsEntry = UbicacionBahia
    '                        order.Lines.BinAllocations.Quantity = Linea.Cantidad

    '                        noesloteprimero2 = False

    '                        baselinenumber2 += 1

    '                    End If
    '                Next

    '                rs.MoveNext()
    '            End While

    '            If order.Update() = 0 Then
    '                jRes.Resultado = "Ok"
    '            Else
    '                jRes.Resultado = oCompany.GetLastErrorDescription
    '                log.escribeMensaje(oCompany.GetLastErrorDescription, EXO_Log.EXO_Log.Tipo.informacion)
    '                'If oCompany.InTransaction = True Then
    '                '    oCompany.EndTransaction(BoWfTransOpt.wf_RollBack)
    '                'End If

    '                ' conexiones.liberaCompañia(oCompany)
    '                res = js.Serialize(jRes)
    '                Return res

    '            End If
    '        End If


    '        '------------YA SE PUEDE HACER EL PICKING DE TODO-------------------
    '        Dim oPick As SAPbobsCOM.PickLists = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oPickLists)

    '        'consulta de lineas y documentos del picking
    '        sql = "SELECT T0.""AbsEntry"",T0.""PickEntry"",T0.""OrderEntry"",T0.""OrderLine"" " +
    '                " FROM ""PKL1"" T0  " +
    '                " WHERE T0.""AbsEntry"" = " + ListOp.NumeroPicking + " and ""PickStatus""<>'Y'" +
    '                " ORDER BY t0.""PickEntry"" "

    '        rs.DoQuery(sql)

    '        'If oCompany.InTransaction = True Then
    '        '    oCompany.EndTransaction(BoWfTransOpt.wf_RollBack)
    '        'End If

    '        oPick.GetByKey(ListOp.NumeroPicking)
    '        If rs.RecordCount > 0 Then
    '            rs.MoveFirst()
    '            While (Not rs.EoF)

    '                Dim baselinenumber As Integer = 0
    '                Dim noesloteprimero As Boolean = True
    '                Dim cantidadTotal As Double = 0
    '                Dim setCurrent As Boolean = True
    '                Dim AnyadeRegistros As Boolean = True

    '                For Each Linea As LineasPicking In ListOp.Lineas
    '                    'encontramos la linea y trabajamos con listop

    '                    If CType(rs.Fields.Item("PickEntry").Value.ToString, Integer) = Linea.PickingLinea Then


    '                        If setCurrent = True Then
    '                            oPick.Lines.SetCurrentLine(Linea.PickingLinea)
    '                            setCurrent = False
    '                        End If

    '                        cantidadTotal = cantidadTotal + Linea.Cantidad

    '                        'oPick.Lines.BinAllocations.BinAbsEntry = UbicacionBahia

    '                        If Not AnyadeRegistros Then

    '                            oPick.Lines.BatchNumbers.Add()
    '                            oPick.Lines.BinAllocations.Add()
    '                        End If

    '                        If Linea.Lote <> "" Then
    '                            oPick.Lines.BatchNumbers.BaseLineNumber = Linea.PickingLinea
    '                            oPick.Lines.BatchNumbers.BatchNumber = Linea.Lote
    '                            oPick.Lines.BatchNumbers.Quantity = Linea.Cantidad

    '                            AnyadeRegistros = False
    '                            oPick.Lines.BinAllocations.AllowNegativeQuantity = BoYesNoEnum.tYES
    '                            oPick.Lines.BinAllocations.BaseLineNumber = Linea.PickingLinea
    '                            oPick.Lines.BinAllocations.SerialAndBatchNumbersBaseLine = baselinenumber
    '                            oPick.Lines.BinAllocations.BinAbsEntry = UbicacionBahia
    '                            oPick.Lines.BinAllocations.Quantity = Linea.Cantidad


    '                        Else
    '                            noesloteprimero = False
    '                        End If

    '                        baselinenumber += 1

    '                    End If
    '                Next

    '                If noesloteprimero = False Then
    '                    'oDoc.Lines.BinAllocations.SerialAndBatchNumbersBaseLine = 0
    '                    oPick.Lines.BinAllocations.BinAbsEntry = UbicacionBahia
    '                    oPick.Lines.BinAllocations.Quantity = cantidadTotal
    '                    'oPick.Lines.BinAllocations.Add()
    '                Else


    '                End If

    '                'oPick.Lines.PickedQuantity = cantidadTotal

    '                rs.MoveNext()
    '            End While
    '        End If


    '        If oPick.Update() = 0 Then
    '            log.escribeMensaje("8", EXO_Log.EXO_Log.Tipo.informacion)
    '            'llamar a funcion para generar el udo de los bultos y asignarselo al pedido.

    '            jRes.Resultado = "Ok"
    '            Try
    '                If ListOp.Bultos.Count > 0 Then
    '                    'LLAMO A generar Udo de picking
    '                    If GenerarBultosPacking(oCompany, ListOp.Bultos, ListOp.NumeroPicking) Then
    '                    Else
    '                        jRes.Resultado = "Error Generando el packing"
    '                    End If
    '                End If
    '            Catch ex As Exception
    '                log.escribeMensaje(ex.Message, EXO_Log.EXO_Log.Tipo.error)
    '            End Try


    '        Else
    '            jRes.Resultado = oCompany.GetLastErrorDescription

    '            'If oCompany.InTransaction = True Then
    '            '    oCompany.EndTransaction(BoWfTransOpt.wf_RollBack)
    '            'End If

    '            ' conexiones.liberaCompañia(oCompany)
    '            res = js.Serialize(jRes)
    '            Return res

    '        End If

    '    Catch ex As Exception
    '        log.escribeMensaje(ex.Message, EXO_Log.EXO_Log.Tipo.error)
    '        jRes.Resultado = "Error. " + ex.Message

    '        'If oCompany.InTransaction = True Then
    '        '    oCompany.EndTransaction(BoWfTransOpt.wf_RollBack)
    '        'End If

    '    End Try


    '    'conexiones.liberaCompañia(oCompany)

    '    res = js.Serialize(jRes)

    '    Return res

    'End Function

    '''''FUTURAS MEJORAS, YA DESARROLLADAS CUANDO FUE MADRIFERR

#End Region


#Region "Reubicaciones de material, solicitudes de traslado, se iba almacennado en la web, hasta finalizar"

    Private Function ListasSolicitudTraslado(BaseDatos As String, Usuario As String, Password As String, log As EXO_Log.EXO_Log) As String
        Dim listado As List(Of ListasPicking) = New List(Of ListasPicking)
        Dim res As String = ""
        Dim oPic As ListasPicking = New ListasPicking

        Dim Esprimero As Boolean = True

        Dim oCompany As SAPbobsCOM.Company
        oCompany = New SAPbobsCOM.Company
        oCompany = conectaDI(BaseDatos, Usuario, Password)
        EstablecerAlmacen(oCompany)

        Try

            'hacer consulta al sql y y rellenar el listado

            Dim query As String = " Select T0.""DocEntry"",T0.""DocNum"",T0.""DocDate"",T0.""Comments"" " +
                                " FROM ""OWTQ"" T0  " +
                                " WHERE  T0.""DocStatus""='O'"


            query = query + "ORDER BY T0.""DocEntry"""
            'recorro y voy rellenando listado 

            Dim rs As SAPbobsCOM.Recordset

            rs = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)

            rs.DoQuery(query)

            If rs.RecordCount > 0 Then

                rs.MoveFirst()

                While (Not rs.EoF)

                    oPic = New ListasPicking

                    oPic.Resultado = "Ok"
                    oPic.Numero = rs.Fields.Item("DocNum").Value.ToString
                    oPic.NumeroInternoTraslado = rs.Fields.Item("DocEntry").Value.ToString
                    oPic.Fecha = rs.Fields.Item("DocDate").Value.ToString
                    oPic.Comentario = rs.Fields.Item("Comments").Value.ToString
                    listado.Add(oPic)

                    rs.MoveNext()
                End While

            Else

                oPic.Resultado = "Error no hay datos coincidentes"
                listado.Add(oPic)

            End If

        Catch ex As Exception
            log.escribeMensaje(ex.Message, EXO_Log.EXO_Log.Tipo.error)
            oPic.Resultado = "Error: " + ex.Message
            listado.Add(oPic)
        End Try

        'liberaCompañia(compañia)
        'conexiones.DisconnectSQLServer()

        Dim js As New JavaScriptSerializer()
        res = js.Serialize(listado)

        Return res

    End Function

    Public Function DesgloseSolicitudesTraslado(BaseDatos As String, Usuario As String, Password As String, NumeroTraslado As String, log As EXO_Log.EXO_Log) As String

        Dim listado As List(Of PedidosCompra) = New List(Of PedidosCompra)
        Dim res As String = ""
        Dim oPed As PedidosCompra = New PedidosCompra

        Dim Esprimero As Boolean = True

        'conexiones.ConnectSQLServer(BaseDatos)
        Dim oCompany As SAPbobsCOM.Company
        oCompany = New SAPbobsCOM.Company
        oCompany = conectaDI(BaseDatos, Usuario, Password)
        EstablecerAlmacen(oCompany)

        Try

            'CONSULTA EN HANA
            Dim query As String = " SELECT T0.""DocEntry"", T0.""DocNum"",T1.""LineNum"",T0.""CardCode"",T0.""CardName"",T1.""ItemCode"",T2.""ItemName"",T1.""OpenQty"" as ""OpenQty"", " +
                                " Case WHEN COALESCE(T2.""ManBtchNum"",'N') = 'N' THEN 'N' ELSE 'Y' END as ""EsLote"", " +
                                " T2.""BHeight1"" As ""Alto"", T2.""BWidth1"" As ""Ancho"",T2.""BLength1"" As ""Largo"",T2.""BWeight1"" As ""Peso"",T1.""unitMsr"" " +
                                " , COALESCE(T4.""BcdCode"",'') as ""Ean14"" " +
                                " FROM ""OWTQ"" T0 INNER JOIN ""WTQ1"" T1 ON T0.""DocEntry""=T1.""DocEntry"" " +
                                " INNER JOIN ""OITM"" T2 ON T1.""ItemCode""=T2.""ItemCode"" " +
                                  " LEFT JOIN ""OBCD"" T4 ON T2.""PUoMEntry""=T4.""UomEntry"" AND T2.""ItemCode""=T4.""ItemCode"" " +
                                " WHERE ""OpenQty"" > 0 AND T0.""DocStatus""='O' and T0.""DocNum""='" + NumeroTraslado + "'"


            query = query + "ORDER BY T0.""DocEntry"", T1.""LineNum"" "

            Dim rs As SAPbobsCOM.Recordset

            rs = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)

            rs.DoQuery(query)

            If rs.RecordCount > 0 Then

                rs.MoveFirst()

                While (Not rs.EoF)

                    oPed = New PedidosCompra

                    oPed.Resultado = "Ok"
                    oPed.NumInterno = rs.Fields.Item("DocEntry").Value.ToString
                    oPed.NumDocumento = rs.Fields.Item("DocNum").Value.ToString
                    oPed.NumLinea = rs.Fields.Item("LineNum").Value.ToString
                    oPed.Proveedor = rs.Fields.Item("CardCode").Value.ToString
                    oPed.ProveedorNombre = rs.Fields.Item("CardName").Value.ToString
                    oPed.Codigo = rs.Fields.Item("ItemCode").Value.ToString
                    oPed.Descripcion = rs.Fields.Item("ItemName").Value.ToString
                    oPed.Cantidad = rs.Fields.Item("OpenQty").Value.ToString
                    oPed.EsLote = rs.Fields.Item("EsLote").Value.ToString
                    oPed.Largo = rs.Fields.Item("Largo").Value.ToString
                    oPed.Peso = rs.Fields.Item("Peso").Value.ToString
                    oPed.Alto = rs.Fields.Item("Alto").Value.ToString
                    oPed.Ancho = rs.Fields.Item("Ancho").Value.ToString
                    oPed.UnidadMedida = rs.Fields.Item("unitMsr").Value.ToString

                    listado.Add(oPed)

                    rs.MoveNext()
                End While

            Else

                oPed.Resultado = "Error no hay datos coincidentes"
                listado.Add(oPed)

            End If

        Catch ex As Exception
            log.escribeMensaje(ex.Message, EXO_Log.EXO_Log.Tipo.error)
            oPed.Resultado = "Error: " + ex.Message
            listado.Add(oPed)
        End Try

        'liberaCompañia(compañia)
        'conexiones.DisconnectSQLServer()

        Dim js As New JavaScriptSerializer()
        res = js.Serialize(listado)

        Return res

    End Function

    Private Function GenerarOperacionTraslado(JSON As String, BaseDatos As String, Usuario As String, Password As String, log As EXO_Log.EXO_Log) As String

        'falta poner el transaction

        Dim ListOp As OperacionTraslado = New OperacionTraslado

        Dim jRes As Resultado = New Resultado
        Dim res As String = ""
        Dim bPrimero As Boolean = True
        Dim js As New JavaScriptSerializer()
        Dim EntregasGeneradas As String = ""
        ListOp = js.Deserialize(Of OperacionTraslado)(JSON)
        Dim sdocnum As String = ""


        Dim oCompany As SAPbobsCOM.Company
        oCompany = New SAPbobsCOM.Company
        oCompany = conectaDI(BaseDatos, Usuario, Password)
        EstablecerAlmacen(oCompany)

        Try

            Dim oDoc As SAPbobsCOM.StockTransfer = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oStockTransfer)
            oDoc.DocObjectCode = SAPbobsCOM.BoObjectTypes.oStockTransfer

            'consulta de lineas y documentos del picking
            Dim sql As String = "select t0.""DocEntry"",t0.""LineNum"",t0.""ItemCode"" " +
                    " from ""WTQ1"" t0 INNER JOIN ""OWTQ"" T1 ON T0.""DocEntry""=T1.""DocEntry""  " +
                    " where t0.""DocEntry"" = " + ListOp.NumeroSolTraslado + " " +
                    " and t0.""OpenQty"" > 0 and T1.""DocStatus""='O'" +
                    " order by t0.""LineNum"" "

            Dim rs As SAPbobsCOM.Recordset

            rs = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)

            rs.DoQuery(sql)

            Dim esPrimeraLinea As Boolean = True

            If rs.RecordCount > 0 Then

                rs.MoveFirst()

                oDoc.FromWarehouse = AlmacenPrincipal.ToString
                oDoc.ToWarehouse = AlmacenPrincipal.ToString
                oDoc.Comments = "Creado desde lectores planta"

                While (Not rs.EoF)


                    If esPrimeraLinea = False Then
                        oDoc.Lines.Add()
                    Else
                        esPrimeraLinea = False
                    End If

                    Dim baselinenumber As Integer = 0
                    Dim noesloteprimero As Boolean = True
                    Dim cantidadTotal As Double = 0
                    Dim UbicacionOrigen As String = ""
                    Dim UbicacionDestino As String = ""

                    'buscamos el pickentry en el json, para poder hacer los baseentry
                    For Each Linea As LineasTraslado In ListOp.Lineas
                        'encontramos la linea y trabajamos con listop
                        If CType(rs.Fields.Item("LineNum").Value.ToString, Integer) = Linea.NumeroLinea Then

                            oDoc.Lines.BaseEntry = ListOp.NumeroSolTraslado
                            oDoc.Lines.BaseLine = Linea.NumeroLinea
                            oDoc.Lines.BaseType = InvBaseDocTypeEnum.InventoryTransferRequest

                            oDoc.Lines.FromWarehouseCode = AlmacenPrincipal.ToString
                            oDoc.Lines.WarehouseCode = AlmacenPrincipal.ToString

                            'Consulta para la ubicacion bahia origen y destino

                            Dim query As String = "SELECT ""AbsEntry"" FROM ""OBIN"" WHERE ""BinCode""='" + Linea.UbicacionOrigen + "'"
                            Dim rs2 As SAPbobsCOM.Recordset
                            rs2 = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                            rs2.DoQuery(query)

                            If rs2.RecordCount > 0 Then
                                rs2.MoveFirst()
                                UbicacionOrigen = rs2.Fields.Item("AbsEntry").Value.ToString()
                            End If

                            query = "SELECT ""AbsEntry"" FROM ""OBIN"" WHERE ""BinCode""='" + Linea.UbicacionDestino + "'"
                            rs2 = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                            rs2.DoQuery(query)

                            If rs2.RecordCount > 0 Then
                                rs2.MoveFirst()
                                UbicacionDestino = rs2.Fields.Item("AbsEntry").Value.ToString()
                            End If

                            cantidadTotal = cantidadTotal + Linea.Cantidad

                            If Linea.Lote <> "" Then
                                'oDoc.Lines.BatchNumbers.BaseLineNumber = 0
                                oDoc.Lines.BatchNumbers.BatchNumber = Linea.Lote
                                oDoc.Lines.BatchNumbers.Quantity = Linea.Cantidad
                                oDoc.Lines.BatchNumbers.Add()

                                'oDoc.Lines.BinAllocations.SetCurrentLine(0)
                                oDoc.Lines.BinAllocations.SerialAndBatchNumbersBaseLine = baselinenumber
                                oDoc.Lines.BinAllocations.BinActionType = SAPbobsCOM.BinActionTypeEnum.batFromWarehouse
                                oDoc.Lines.BinAllocations.BinAbsEntry = UbicacionOrigen
                                oDoc.Lines.BinAllocations.Quantity = Linea.Cantidad
                                oDoc.Lines.BinAllocations.Add()

                                'oDoc.Lines.BinAllocations.SetCurrentLine(1)
                                oDoc.Lines.BinAllocations.SerialAndBatchNumbersBaseLine = baselinenumber
                                oDoc.Lines.BinAllocations.BinActionType = SAPbobsCOM.BinActionTypeEnum.batToWarehouse
                                oDoc.Lines.BinAllocations.BinAbsEntry = UbicacionDestino
                                oDoc.Lines.BinAllocations.Quantity = Linea.Cantidad
                                oDoc.Lines.BinAllocations.Add()

                            Else
                                noesloteprimero = False
                            End If

                            baselinenumber += 1

                        End If
                    Next

                    If noesloteprimero = False Then
                        'oDoc.Lines.BinAllocations.SerialAndBatchNumbersBaseLine = 0
                        oDoc.Lines.BinAllocations.BinActionType = SAPbobsCOM.BinActionTypeEnum.batFromWarehouse
                        oDoc.Lines.BinAllocations.BinAbsEntry = UbicacionOrigen
                        oDoc.Lines.BinAllocations.Quantity = cantidadTotal
                        oDoc.Lines.BinAllocations.Add()

                        oDoc.Lines.BinAllocations.BinActionType = SAPbobsCOM.BinActionTypeEnum.batToWarehouse
                        oDoc.Lines.BinAllocations.BinAbsEntry = UbicacionDestino
                        oDoc.Lines.BinAllocations.Quantity = cantidadTotal
                        oDoc.Lines.BinAllocations.Add()

                    End If

                    oDoc.Lines.Quantity = cantidadTotal

                    rs.MoveNext()
                End While


                If oDoc.Add() = 0 Then
                    jRes.Resultado = "Ok"
                Else
                    jRes.Resultado = oCompany.GetLastErrorDescription

                    res = js.Serialize(jRes)
                    Return res

                End If

            End If

        Catch ex As Exception
            log.escribeMensaje(ex.Message, EXO_Log.EXO_Log.Tipo.error)
            jRes.Resultado = "Error. " + ex.Message

            If oCompany.InTransaction = True Then
                oCompany.EndTransaction(BoWfTransOpt.wf_RollBack)
            End If

        End Try


        'conexiones.liberaCompañia(oCompany)

        res = js.Serialize(jRes)

        Return res

    End Function

#End Region

#Region "Solicitud de traslado, funcionalidad igual que el picking"
    Private Function ListasTraslado(BaseDatos As String, Usuario As String, Password As String, Almacen As String, log As EXO_Log.EXO_Log) As String
        Dim listado As List(Of ListasPickingMultiple) = New List(Of ListasPickingMultiple)
        Dim res As String = ""
        Dim oPic As ListasPickingMultiple = New ListasPickingMultiple

        Dim Esprimero As Boolean = True

        Dim oCompany As SAPbobsCOM.Company
        oCompany = New SAPbobsCOM.Company
        oCompany = conectaDI(BaseDatos, Usuario, Password)

        Try

            ''hacer consulta al sql y y rellenar el listado
            Dim query As String = "SELECT ""AbsEntry"",""PickDate"",""Remarks"" FROM ""OPKL""  T0 " +
                  " WHERE ""Status"" not in ('Y','C')  and ""Canceled""='N' and COALESCE(""U_EXO_PPIST"",'N')='N' " +
            " and 'Y' = COALESCE((SELECT MAX('Y') from ""PKL1"" AS T1 INNER JOIN ""WTQ1"" T2 ON T1.""OrderEntry""=T2.""DocEntry"" and T1.""OrderLine""=T2.""LineNum"" " +
            " INNER JOIN ""OWTQ"" T3 ON T2.""DocEntry""=T3.""DocEntry"" " +
            " WHERE T0.""AbsEntry""=T1.""AbsEntry""  " +
            " And T3.""Filler""='" + Almacen + "'),'N') "

            'Dim query As String = "SELECT ""AbsEntry"",""PickDate"",""Remarks"" FROM ""OPKL""  T0 " +
            '  " WHERE ""Status"" not in ('Y','C')  and ""Canceled""='N' and COALESCE(""U_EXO_PPIST"",'N')='N' " +
            '"  And (SELECT COUNT(""AbsEntry"") FROM ""PKL1"" T10 WHERE T0.""AbsEntry""=T10.""AbsEntry"")= " +
            '"    (SELECT COUNT(T2.""LineNum"") from ""PKL1"" AS T1 INNER JOIN ""RDR1"" T2 ON T1.""OrderEntry""=T2.""DocEntry"" and T1.""OrderLine""=T2.""LineNum""  " +
            '" WHERE T0.""AbsEntry""=T1.""AbsEntry""   " +
            '" And T2.""WhsCode""='" + AlmacenPrincipal + "')"

            'recorro y voy rellenando listado 

            Dim rs As SAPbobsCOM.Recordset
            Dim rs2 As SAPbobsCOM.Recordset

            rs = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)

            rs.DoQuery(query)

            If rs.RecordCount > 0 Then

                rs.MoveFirst()

                While (Not rs.EoF)

                    oPic = New ListasPickingMultiple

                    oPic.Resultado = "Ok"
                    oPic.Numero = rs.Fields.Item("AbsEntry").Value.ToString
                    oPic.Fecha = rs.Fields.Item("PickDate").Value.ToString

                    'oPic.Comentario = rs.Fields.Item("Remarks").Value.ToString

                    rs2 = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)

                    query = "select top 1 COALESCE(T3.""TrnspName"",'') TrnspName,COALESCE(T4.""CardName"",'') CardFName , " +
                        " COALESCE(T2.""PickRmrk"",'')  U_EXO_OBSP " +
                  " FROM ""PKL1"" T0 INNER JOIN ""WTQ1"" T1 On T0.""OrderEntry""=T1.""DocEntry""  " +
                  " INNER JOIN ""OWTQ"" T2 ON T1.""DocEntry""=T2.""DocEntry""  " +
                  " Left Join ""OSHP"" T3 ON T2.""TrnspCode""=T3.""TrnspCode"" " +
                  " LEFT JOIN ""OCRD"" T4 ON T2.""CardCode""=T4.""CardCode""  " +
                  " WHERE T0.""AbsEntry""='" + rs.Fields.Item("AbsEntry").Value.ToString + "' "

                    rs2.DoQuery(query)

                    If rs2.RecordCount > 0 Then
                        While (Not rs2.EoF)

                            oPic.Comentario = rs2.Fields.Item("CardFName").Value.ToString
                            oPic.Transportista = rs2.Fields.Item("TrnspName").Value.ToString
                            oPic.Observaciones = rs2.Fields.Item("U_EXO_OBSP").Value.ToString
                            rs2.MoveNext()

                        End While

                    End If

                    listado.Add(oPic)

                    rs.MoveNext()
                End While

            Else

                oPic.Resultado = "No hay Pickings disponibles"
                listado.Add(oPic)

            End If

        Catch ex As Exception
            log.escribeMensaje(ex.Message, EXO_Log.EXO_Log.Tipo.error)
            oPic.Resultado = "Error: " + ex.Message
            listado.Add(oPic)
        End Try

        'liberaCompañia(compañia)
        'conexiones.DisconnectSQLServer()

        Dim js As New JavaScriptSerializer()
        res = js.Serialize(listado)

        Return res

    End Function

    Private Function DesgloseTraslado(BaseDatos As String, Usuario As String, Password As String, NumeroPicking As String, log As EXO_Log.EXO_Log) As String

        Dim listado As List(Of SolicitudTraslado) = New List(Of SolicitudTraslado)
        Dim res As String = ""
        Dim oPic As SolicitudTraslado = New SolicitudTraslado

        Dim Esprimero As Boolean = True

        Dim oCompany As SAPbobsCOM.Company
        oCompany = New SAPbobsCOM.Company
        oCompany = conectaDI(BaseDatos, Usuario, Password)

        Dim js As New JavaScriptSerializer()

        Try

            'LLAMAR AL PROCEDIMIENTO ALMACENADO

            Dim UbicacionDestinoPropuesta As String = ""
            Dim GestionaUbicacionDestino As String = ""

            Dim query As String = "CALL EXO_GP_TRABAJO_LISTA_TRASLADO(" + NumeroPicking + ", NULL) "

            Dim rs As SAPbobsCOM.Recordset

            rs = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)

            rs.DoQuery(query)

            If rs.RecordCount > 0 Then

                rs.MoveFirst()

                'CONSULTA PARA SABER SI UBICACIONES SON DEL MISMOS EMPLAZAMIENTO, SI LO SON UBACIONDESTINOPROPUESTA -> LA DEL TRASLADO
                'SI SON DIFERENTES EMPLAZAMIENTOS BUSCAR LA UBICACION DE RECIBO TRASLADO DEL ALMACEN DESTINO

                Dim rs2 As SAPbobsCOM.Recordset

                rs2 = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)

                query = "Select T0.""Filler"",T0.""ToWhsCode"",Case When COALESCE(T1.""Location"",'-1')=COALESCE(T2.""Location"",'-1') Then 'LOCALIZACION' ELSE COALESCE(T3.""U_EXO_RECSOLTRA"",'NOHAY') END AS UBIDEST,T3.""BinCode"" " +
                        " FROM ""OWTQ"" T0 " +
                            " INNER Join ""OWHS"" T1 ON T0.""Filler""=T1.""WhsCode"" " +
                            " INNER Join ""OWHS"" T2 On T0.""ToWhsCode""=T2.""WhsCode"" " +
                            " Left Join ""OBIN"" T3 ON T2.""WhsCode""=T3.""WhsCode"" And T3.""U_EXO_RECSOLTRA""='Y' " +
                        "  WHERE T0.""DocEntry"" in (select ""OrderEntry"" from ""PKL1"" WHERE ""AbsEntry""=" + NumeroPicking.ToString + " limit 1) "

                rs2.DoQuery(query)

                If rs2.RecordCount > 0 Then

                    rs2.MoveFirst()

                    If rs2.Fields.Item("UBIDEST").Value.ToString = "LOCALIZACION" Then

                        UbicacionDestinoPropuesta = ""
                        GestionaUbicacionDestino = "Y"
                    ElseIf rs2.Fields.Item("UBIDEST").Value.ToString = "NOHAY" Then

                        oPic.Resultado = "Falta ubicacion traslados destino en almacen destino, configurar en SAP."
                        listado.Add(oPic)

                        res = js.Serialize(listado)
                        Return res
                    Else
                        UbicacionDestinoPropuesta = rs2.Fields.Item("BinCode").Value.ToString
                        GestionaUbicacionDestino = "N"
                    End If
                End If

                While (Not rs.EoF)

                    oPic = New SolicitudTraslado

                    oPic.Resultado = "Ok"
                    oPic.PickingLinea = rs.Fields.Item("PICKENTRY").Value.ToString
                    oPic.Articulo = rs.Fields.Item("ITEMCODE").Value.ToString
                    oPic.Descripcion = rs.Fields.Item("ITEMNAME").Value.ToString

                    oPic.CantidadTotal = CType(rs.Fields.Item("CANTIDADTOTAL").Value.ToString, Double) / CType(rs.Fields.Item("NUMPERMSR").Value.ToString, Double)
                    oPic.Cantidad = CType(rs.Fields.Item("CANTIDAD").Value.ToString, Double) / CType(rs.Fields.Item("NUMPERMSR").Value.ToString, Double)

                    oPic.CantidadUDM = CType(rs.Fields.Item("NUMPERMSR").Value.ToString, Double)
                    oPic.UnidadMedida = rs.Fields.Item("UDM").Value.ToString
                    oPic.Lote = rs.Fields.Item("BATCHNUM").Value.ToString
                    oPic.Ubicacion = rs.Fields.Item("BINCODE").Value.ToString()
                    oPic.UbicacionPropuesta = rs.Fields.Item("PROPUESTO").Value.ToString()
                    oPic.EsLote = rs.Fields.Item("ESLOTE").Value.ToString
                    oPic.Procesado = rs.Fields.Item("SEPUEDEGESTIONAR").Value.ToString()

                    oPic.GestionaUbicacionDestino = GestionaUbicacionDestino

                    If GestionaUbicacionDestino = "Y" Then
                        oPic.UbicacionDestinoPropuesta = rs.Fields.Item("BINCODEDESTINO").Value.ToString()
                    Else
                        oPic.UbicacionDestinoPropuesta = UbicacionDestinoPropuesta
                    End If

                    listado.Add(oPic)


                    rs.MoveNext()
                End While

            Else

                oPic.Resultado = "Error no hay datos coincidentes"
                listado.Add(oPic)

            End If

        Catch ex As Exception
            log.escribeMensaje(ex.Message, EXO_Log.EXO_Log.Tipo.error)
            oPic.Resultado = "Error: " + ex.Message
            listado.Add(oPic)
        End Try

        'liberaCompañia(compañia)
        'conexiones.DisconnectSQLServer()


        res = js.Serialize(listado)

        Return res

    End Function

    Private Function GenerarTraslado(JSON As String, BaseDatos As String, Usuario As String, Password As String, log As EXO_Log.EXO_Log) As String

        Dim ListOp As GenerarTraslado = New GenerarTraslado

        Dim jRes As Resultado = New Resultado

        Dim res As String = ""
        Dim bPrimero As Boolean = True

        Dim js As New JavaScriptSerializer()

        Dim EntregasGeneradas As String = ""

        Dim UbicacionOrigen As String = ""
        Dim UbicacionDestino As String = ""
        'ListOp = js.Deserialize(Of GenerarPicking)(JSON)

        'Dim sdocnum As String = ""

        'Dim oCompany As SAPbobsCOM.Company
        'oCompany = New SAPbobsCOM.Company
        'oCompany = conectaDI(BaseDatos,Usuario, Password)

        Try

            ListOp = js.Deserialize(Of GenerarTraslado)(JSON)

            Dim sdocnum As String = ""

            Dim oCompany As SAPbobsCOM.Company
            oCompany = New SAPbobsCOM.Company
            oCompany = conectaDI(BaseDatos, Usuario, Password)

            'Comprobamos si ya tenemos el número de picking generado, de ser así se termina el proceso
            Dim query As String = ""
            Dim rs As SAPbobsCOM.Recordset

            'oCompany.StartTransaction()
            jRes.Resultado = ""

            rs = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)

            'la ubicacion origen es la ubicacion destino del traslado
            Dim sql As String = "SELECT T0.""AbsEntry"",T0.""PickEntry"",T0.""OrderEntry"",T0.""OrderLine"",T3.""CardCode"",T1.""NumPerMsr"", " +
                    "T3.""Filler"", T3.""ToWhsCode"",T4.""AbsEntry"" ""UBIORIGEN"" " +
                    " FROM ""PKL1"" T0  INNER JOIN ""WTQ1"" T1 On  T1.""DocEntry""=T0.""OrderEntry"" And   T1.""LineNum""=T0.""OrderLine"" " +
                    " INNER JOIN ""OITM"" T2 On T1.""ItemCode""=T2.""ItemCode"" " +
                    " INNER JOIN ""OWTQ"" T3 On T1.""DocEntry""=T3.""DocEntry"" " +
                    " INNER JOIN ""OBIN"" T4 ON T3.""ToWhsCode""=T4.""WhsCode"" and T4.""U_EXO_ESSOLTRAS""='Y' " +
                    " WHERE T0.""AbsEntry"" = " + ListOp.NumeroTraslado + " " +
                    " ORDER BY T0.""OrderEntry"",T0.""OrderLine"" "

            rs.DoQuery(sql)

            Dim oTransfer As SAPbobsCOM.StockTransfer = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oStockTransfer)

            oTransfer.DocObjectCode = SAPbobsCOM.BoObjectTypes.oStockTransfer

            Dim DocPedido As String = ""
            Dim EsNuevo As Boolean = True
            Dim setCurrent2 As Boolean = True

            Dim baselinenumber2 As Integer = 0
            Dim noesloteprimero2 As Boolean = True
            Dim cantidadTotal2 As Double = 0

            Dim AnyadeRegistros2 As Boolean = True
            Dim esPrimeraLinea As Boolean = True

            Dim contadorLineas As Integer = 1

            If rs.RecordCount > 0 Then
                rs.MoveFirst()

                UbicacionOrigen = rs.Fields.Item("UBIORIGEN").Value.ToString

                oTransfer.FromWarehouse = rs.Fields.Item("Filler").Value.ToString
                oTransfer.ToWarehouse = rs.Fields.Item("ToWhsCode").Value.ToString
                oTransfer.Comments = "Creado desde lectores planta"

                While (Not rs.EoF)

                    setCurrent2 = True
                    AnyadeRegistros2 = True

                    Dim baselinenumber As Integer = 0
                    Dim noesloteprimero As Boolean = True
                    Dim cantidadTotal As Double = 0
                    Dim MasdeUnLote As Boolean = True
                    Dim esprimero As Boolean = True

                    If esPrimeraLinea = False Then
                        oTransfer.Lines.Add()
                    Else
                        esPrimeraLinea = False
                    End If

                    'valores de la solicitud de traslado
                    oTransfer.Lines.BaseEntry = rs.Fields.Item("OrderEntry").Value.ToString
                    oTransfer.Lines.BaseLine = rs.Fields.Item("OrderLine").Value.ToString
                    oTransfer.Lines.BaseType = InvBaseDocTypeEnum.InventoryTransferRequest

                    oTransfer.Lines.FromWarehouseCode = rs.Fields.Item("Filler").Value.ToString
                    oTransfer.Lines.WarehouseCode = rs.Fields.Item("ToWhsCode").Value.ToString

                    'para cada linea, hacemos una consulta para buscar los lotes
                    Dim rs2 As SAPbobsCOM.Recordset

                    rs2 = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)

                    'la ubicacion destino, es la que haya en el campo de usuario

                    sql = "SELECT T2.""AbsEntry"", sum(T0.""Quantity"")  ""Quantity"", '' ""DistNumber"" " +
                    " FROM  ""WTR1"" T0 INNER JOIN ""OWTR"" T1 ON T0.""DocEntry""=T1.""DocEntry"" " +
                    "  INNER JOIN ""OBIN"" T2 ON T1.""U_EXO_UBIPIC""=T2.""BinCode"" " +
                    " INNER JOIN ""OITM"" T3 ON T0.""ItemCode""=T3.""ItemCode"" and T3.""ManBtchNum""='N' " +
                    " WHERE T1.""U_EXO_NUMPIC"" = " + rs.Fields.Item("AbsEntry").Value.ToString + " And t1.""U_EXO_LINPIC""='" + rs.Fields.Item("PickEntry").Value.ToString + "' " +
                    " group by T2.""AbsEntry"" "

                    sql = sql + "UNION ALL "

                    sql = sql + "SELECT T2.""AbsEntry"", sum(T0.""Quantity"") ""Quantity"", T6.""DistNumber"" " +
                        " FROM  ""WTR1"" T0 INNER JOIN ""OWTR"" T1 On T0.""DocEntry""=T1.""DocEntry"" " +
                        "  INNER JOIN ""OBIN"" T2 ON T1.""U_EXO_UBIPIC""=T2.""BinCode"" " +
                        " INNER JOIN ""OITM"" T3 ON T0.""ItemCode""=T3.""ItemCode"" and T3.""ManBtchNum""='Y' " +
                        " INNER Join ""OITL"" T4 On T4.""DocEntry""=T0.""DocEntry"" And T4.""DocLine""=T0.""LineNum"" And T4.""DocType""=67 " +
                        " INNER JOIN ""ITL1"" T5 ON T5.""LogEntry"" = T4.""LogEntry"" " +
                        " INNER JOIN ""OBTN"" T6 ON  T6.""SysNumber"" = T5.""SysNumber"" AND T6.""ItemCode"" = T5.""ItemCode"" And T6.""AbsEntry""=T5.""MdAbsEntry"" " +
                        " WHERE T1.""U_EXO_NUMPIC"" = " + rs.Fields.Item("AbsEntry").Value.ToString + " And t1.""U_EXO_LINPIC""='" + rs.Fields.Item("PickEntry").Value.ToString + "' " +
                        " group by T2.""AbsEntry"",T6.""DistNumber"" "

                    rs2.DoQuery(sql)

                    While (Not rs2.EoF)

                        'UbicacionOrigen
                        UbicacionDestino = rs2.Fields.Item("AbsEntry").Value.ToString

                        oTransfer.Lines.BaseEntry = CType(rs.Fields.Item("OrderEntry").Value.ToString(), Integer)
                        oTransfer.Lines.BaseLine = CType(rs.Fields.Item("OrderLine").Value.ToString(), Integer)

                        oTransfer.Lines.BaseType = InvBaseDocTypeEnum.InventoryTransferRequest

                        cantidadTotal = cantidadTotal + CType(rs2.Fields.Item("Quantity").Value.ToString, Double)
                        oTransfer.Lines.BinAllocations.BinAbsEntry = CType(UbicacionOrigen, Integer)
                        oTransfer.Lines.WarehouseCode = rs.Fields.Item("Filler").Value.ToString
                        oTransfer.Lines.WarehouseCode = rs.Fields.Item("ToWhsCode").Value.ToString


                        If rs2.Fields.Item("DistNumber").Value.ToString <> "" Then
                            'oTransfer.Lines.BatchNumbers.BaseLineNumber = CType(rs.Fields.Item("OrderLine").Value.ToString(), Integer)

                            If MasdeUnLote = False Then
                                oTransfer.Lines.BatchNumbers.Add()
                                oTransfer.Lines.BinAllocations.Add()
                            End If
                            MasdeUnLote = False

                            oTransfer.Lines.BatchNumbers.BatchNumber = rs2.Fields.Item("DistNumber").Value.ToString
                            oTransfer.Lines.BatchNumbers.Quantity = CType(rs2.Fields.Item("Quantity").Value.ToString, Double)


                            oTransfer.Lines.BinAllocations.SerialAndBatchNumbersBaseLine = baselinenumber
                            oTransfer.Lines.BinAllocations.BinActionType = SAPbobsCOM.BinActionTypeEnum.batFromWarehouse
                            oTransfer.Lines.BinAllocations.BinAbsEntry = UbicacionOrigen
                            oTransfer.Lines.BinAllocations.Quantity = CType(rs2.Fields.Item("Quantity").Value.ToString, Double)
                            oTransfer.Lines.BinAllocations.Add()


                            ''oDoc.Lines.BinAllocations.SetCurrentLine(1)
                            oTransfer.Lines.BinAllocations.SerialAndBatchNumbersBaseLine = baselinenumber
                            oTransfer.Lines.BinAllocations.BinActionType = SAPbobsCOM.BinActionTypeEnum.batToWarehouse
                            oTransfer.Lines.BinAllocations.BinAbsEntry = UbicacionDestino
                            oTransfer.Lines.BinAllocations.Quantity = CType(rs2.Fields.Item("Quantity").Value.ToString, Double)
                            oTransfer.Lines.BinAllocations.Add()

                            baselinenumber += 1
                        Else
                            noesloteprimero = False
                        End If


                        rs2.MoveNext()
                    End While

                    If noesloteprimero = False Then

                        oTransfer.Lines.BinAllocations.BinActionType = SAPbobsCOM.BinActionTypeEnum.batFromWarehouse
                        oTransfer.Lines.BinAllocations.BinAbsEntry = UbicacionOrigen
                        oTransfer.Lines.BinAllocations.Quantity = cantidadTotal
                        oTransfer.Lines.BinAllocations.Add()

                        oTransfer.Lines.BinAllocations.BinActionType = SAPbobsCOM.BinActionTypeEnum.batToWarehouse
                        oTransfer.Lines.BinAllocations.BinAbsEntry = UbicacionDestino
                        oTransfer.Lines.BinAllocations.Quantity = cantidadTotal
                        oTransfer.Lines.BinAllocations.Add()

                    End If

                    oTransfer.Lines.Quantity = cantidadTotal

                    rs.MoveNext()
                End While


                If oTransfer.Add() = 0 Then

                    Dim DocEntry As String = oCompany.GetNewObjectKey
                    jRes.Resultado = "OK:" + DocEntry


                    rs.DoQuery(" update ""OPKL"" SET ""U_EXO_PPIST""='Y' WHERE ""AbsEntry""='" + ListOp.NumeroTraslado + "'")



                Else
                    jRes.Resultado = oCompany.GetLastErrorDescription
                    log.escribeMensaje(oCompany.GetLastErrorDescription, EXO_Log.EXO_Log.Tipo.informacion)
                    'If oCompany.InTransaction = True Then
                    '    oCompany.EndTransaction(BoWfTransOpt.wf_RollBack)
                    'End If

                    ' conexiones.liberaCompañia(oCompany)
                    res = js.Serialize(jRes)
                    Return res

                End If
            End If


        Catch ex As Exception
            log.escribeMensaje(ex.Message, EXO_Log.EXO_Log.Tipo.error)
            jRes.Resultado = "Error. " + ex.Message

            'If oCompany.InTransaction = True Then
            '    oCompany.EndTransaction(BoWfTransOpt.wf_RollBack)
            'End If

        End Try


        'conexiones.liberaCompañia(oCompany)

        res = js.Serialize(jRes)

        Return res

    End Function

#End Region

#Region "Recuento de inventario, funcionalidad para utilizar los recuentos de SAP"

    Private Function ListasRecuentoInventario(BaseDatos As String, Usuario As String, Password As String, log As EXO_Log.EXO_Log) As String

        Dim listado As List(Of ListasRecuentoInventario) = New List(Of ListasRecuentoInventario)
        Dim res As String = ""
        Dim oRecInv As ListasRecuentoInventario = New ListasRecuentoInventario


        Dim Esprimero As Boolean = True

        Dim oCompany As SAPbobsCOM.Company
        oCompany = New SAPbobsCOM.Company
        oCompany = conectaDI(BaseDatos, Usuario, Password)
        EstablecerAlmacen(oCompany)

        Try

            ''hacer consulta al sql y y rellenar el listado

            'faltaria filtrar por el almacen del usuario
            Dim query As String = "SELECT ""DocEntry"",""DocNum"",""Remarks"" ""Ref2"",""CountDate"" FROM ""OINC"" WHERE COALESCE(""U_EXO_COM"",'N')='N' and ""Status""='O' "

            'recorro y voy rellenando listado 

            Dim rs As SAPbobsCOM.Recordset
            Dim rs2 As SAPbobsCOM.Recordset

            rs = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)

            rs.DoQuery(query)

            If rs.RecordCount > 0 Then

                rs.MoveFirst()

                While (Not rs.EoF)

                    oRecInv = New ListasRecuentoInventario

                    oRecInv.Resultado = "Ok"
                    oRecInv.Numero = rs.Fields.Item("DocNum").Value.ToString
                    oRecInv.NumeroInterno = rs.Fields.Item("DocEntry").Value.ToString
                    oRecInv.Fecha = rs.Fields.Item("CountDate").Value.ToString
                    oRecInv.Comentario = rs.Fields.Item("Ref2").Value.ToString

                    listado.Add(oRecInv)

                    rs.MoveNext()
                End While

            Else

                oRecInv.Resultado = "No hay Recuentos disponibles"
                listado.Add(oRecInv)

            End If

        Catch ex As Exception
            log.escribeMensaje(ex.Message, EXO_Log.EXO_Log.Tipo.error)
            oRecInv.Resultado = "Error: " + ex.Message
            listado.Add(oRecInv)
        End Try

        'liberaCompañia(compañia)
        'conexiones.DisconnectSQLServer()

        Dim js As New JavaScriptSerializer()
        res = js.Serialize(listado)

        Return res

    End Function

    Private Function DesgloseRecuentoInventario(BaseDatos As String, Usuario As String, Password As String, NumeroRecuento As String, log As EXO_Log.EXO_Log) As String

        Dim oRecCab As ListasRecuentoInventarioCabecera = New ListasRecuentoInventarioCabecera

        Dim listado As List(Of ListasRecuentoInventarioDetalle) = New List(Of ListasRecuentoInventarioDetalle)
        Dim res As String = ""
        Dim oRec As ListasRecuentoInventarioDetalle = New ListasRecuentoInventarioDetalle
        Dim listadoEAN As List(Of CodigoEAN) = New List(Of CodigoEAN)
        Dim oEAN As CodigoEAN = New CodigoEAN

        Dim Esprimero As Boolean = True

        Dim oCompany As SAPbobsCOM.Company
        oCompany = New SAPbobsCOM.Company
        oCompany = conectaDI(BaseDatos, Usuario, Password)

        EstablecerAlmacen(oCompany)

        Dim ubicacionBahia As String = ""
        Dim js As New JavaScriptSerializer()

        Try

            Dim rs As SAPbobsCOM.Recordset
            Dim rs2 As SAPbobsCOM.Recordset

            rs = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
            rs2 = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)

            'como la bahia va automatica se la paso a Jaime a traves del desglose picking

            Dim query As String = " SELECT t0.""DocEntry"",t0.""LineNum"",t0.""ItemCode"",t0.""ItemDesc"",t1.""BinCode"",T1.""AbsEntry"", " +
                                " COALESCE(T0.""InWhsQty"",0) ""CANTTEORICA"" ,coalesce(T0.""CountQty"",0) ""CANTCONTADA"" " +
                                " , ""Counted"" " +
                                " FROM ""INC1"" t0 inner join ""OBIN"" t1 on t0.""BinEntry""=t1.""AbsEntry"" " +
                                " inner Join ""OINC"" t2 On t0.""DocEntry""=t2.""DocEntry"" " +
                                " where t2.""Status""='O' AND t2.""U_EXO_COM""='N' AND t0.""DocEntry""=" + NumeroRecuento + " " +
                                " order by t1.""BinCode"" asc "

            rs.DoQuery(query)

            If rs.RecordCount > 0 Then

                rs.MoveFirst()

                oRecCab.Resultado = "Ok"
                oRecCab.NumeroInterno = rs.Fields.Item("DocEntry").Value.ToString

                While (Not rs.EoF)

                    oRec = New ListasRecuentoInventarioDetalle
                    oRec.Resultado = "Ok"
                    oRec.NumeroLinea = rs.Fields.Item("LineNum").Value.ToString
                    oRec.Articulo = rs.Fields.Item("ItemCode").Value.ToString
                    oRec.Descripcion = rs.Fields.Item("ItemDesc").Value.ToString
                    oRec.Ubicacion = rs.Fields.Item("BinCode").Value.ToString
                    oRec.CodUbicacion = rs.Fields.Item("AbsEntry").Value.ToString
                    oRec.CantidadContada = CType(rs.Fields.Item("CANTCONTADA").Value.ToString(), Double)
                    oRec.CantidadTeorica = CType(rs.Fields.Item("CANTTEORICA").Value.ToString(), Double)
                    oRec.Verificado = rs.Fields.Item("Counted").Value.ToString

                    ''tengo que buscar todos los codigos de barras del articulo
                    'query = " Select  T2.""ItemCode"", T2.""CodeBars"" ""EAN"" " +
                    '        " FROM  ""OITM"" T2 where T2.""ItemCode""= '" + rs.Fields.Item("ItemCode").Value.ToString + "' " +
                    '        " union " +
                    '        " Select  T2.""ItemCode"",  T4.""BcdCode"" ""EAN"" " +
                    '        " FROM  ""OITM"" T2 " +
                    '        " Left Join ""OBCD"" T4 On coalesce(T2.""SUoMEntry"", -1)=T4.""UomEntry"" And T2.""ItemCode""=T4.""ItemCode"" " +
                    '        "  where T2.""ItemCode""= '" + rs.Fields.Item("ItemCode").Value.ToString + "' " +
                    '        " order by T2.""ItemCode"" "

                    'rs2.DoQuery(query)

                    'listadoEAN = New List(Of CodigoEAN)

                    'If rs2.RecordCount > 0 Then

                    '    rs2.MoveFirst()

                    '    While (Not rs2.EoF)
                    '        oEAN = New CodigoEAN
                    '        oEAN.EAN = rs2.Fields.Item("EAN").Value.ToString
                    '        listadoEAN.Add(oEAN)

                    '        rs2.MoveNext()
                    '    End While

                    'End If

                    'oRec.EAN = listadoEAN

                    listado.Add(oRec)

                    rs.MoveNext()
                End While

                oRecCab.Lineas = listado
            Else

                oRecCab.Resultado = "Error no hay datos coincidentes"
                listado.Add(oRec)

            End If

        Catch ex As Exception
            log.escribeMensaje(ex.Message, EXO_Log.EXO_Log.Tipo.error)
            oRecCab.Resultado = "Error: " + ex.Message
            listado.Add(oRec)
        End Try

        'liberaCompañia(compañia)
        'conexiones.DisconnectSQLServer()


        res = js.Serialize(listado)

        Return res

    End Function

    Private Function GenerarRecuentoInventario(JSON As String, BaseDatos As String, Usuario As String, Password As String, log As EXO_Log.EXO_Log) As String

        Dim ListOp As ListasRecuentoInventarioCabecera = New ListasRecuentoInventarioCabecera

        Dim jRes As Resultado = New Resultado
        Dim res As String = ""
        Dim bPrimero As Boolean = True
        Dim js As New JavaScriptSerializer()
        Dim EntregasGeneradas As String = ""
        ListOp = js.Deserialize(Of ListasRecuentoInventarioCabecera)(JSON)
        Dim sdocnum As String = ""

        Dim oCompany As SAPbobsCOM.Company
        oCompany = New SAPbobsCOM.Company
        oCompany = conectaDI(BaseDatos, Usuario, Password)
        EstablecerAlmacen(oCompany)

        Try

            Dim oCS As SAPbobsCOM.CompanyService = oCompany.GetCompanyService()
            Dim oICS As SAPbobsCOM.InventoryCountingsService = oCS.GetBusinessService(SAPbobsCOM.ServiceTypes.InventoryCountingsService)
            Dim oICP As SAPbobsCOM.InventoryCountingParams = oICS.GetDataInterface(SAPbobsCOM.InventoryCountingsServiceDataInterfaces.icsInventoryCountingParams)
            '//Counting Document DocEntry
            oICP.DocumentEntry = Convert.ToInt64(ListOp.NumeroInterno)
            '//Get the Counting Document
            Dim oIC As SAPbobsCOM.InventoryCounting = oICS.Get(oICP)
            '//If Document is Multiple Counters Type

            Dim oICLS As SAPbobsCOM.InventoryCountingLines = oIC.InventoryCountingLines
            Dim oICL As SAPbobsCOM.InventoryCountingLine
            Dim iLine As Integer = 0
            Dim iCurrentCounter As Integer
            '//Set the Counter User
            '//oICL.CounterID = 1 for manager // (OUSR.USERID) or oCompany.UserSignature for DI loged user
            iCurrentCounter = oCompany.UserSignature
            '*****************************************************
            'NOTE: When Document is Multiple Counters Type
            '      oICLS.Count = (Count of Lines * Users Counters)
            '*****************************************************
            For i As Integer = 0 To oICLS.Count - 1
                '//Set the Line of Counting
                oICL = oICLS.Item(i)
                '//Evaluate the Counter User for not repeat user line
                Dim algo As Integer = oICL.CounterID
                'If oICL.CounterID = iCurrentCounter Then

                For Each Linea In ListOp.Lineas

                    If oICL.BinEntry = Linea.CodUbicacion And oICL.ItemCode = Linea.Articulo Then
                        oICL.Counted = BoYesNoEnum.tYES
                        oICL.CountedQuantity = Linea.CantidadContada
                    End If

                Next

                'End If
            Next

            Try
                oICS.Update(oIC)
                jRes.Resultado = "Ok"

                'Dim rs As SAPbobsCOM.Recordset

                'rs = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                'rs.DoQuery(" update ""OINC"" SET ""U_EXO_COM""='Y' WHERE ""DocEntry""='" + ListOp.NumeroInterno + "'")

            Catch ex As Exception
                Dim ierr As Integer
                Dim serr As String = ""
                oCompany.GetLastError(ierr, serr)
                jRes.Resultado = "Error: " + serr
            End Try

        Catch ex As Exception

            log.escribeMensaje(ex.Message, EXO_Log.EXO_Log.Tipo.error)
            jRes.Resultado = "Error: " + ex.Message

        End Try

        res = js.Serialize(jRes)

        Return res

    End Function

    Private Function RecuentoInventarioMarcarFinalizado(BaseDatos As String, Usuario As String, Password As String, NumInterno As String, log As EXO_Log.EXO_Log) As String


        Dim js As New JavaScriptSerializer()
        Dim jRes As Resultado = New Resultado
        Dim res As String = ""

        Dim oCompany As SAPbobsCOM.Company
        oCompany = New SAPbobsCOM.Company
        oCompany = conectaDI(BaseDatos, Usuario, Password)
        EstablecerAlmacen(oCompany)

        Try

            jRes.Resultado = "Ok"
            Dim rs As SAPbobsCOM.Recordset

            rs = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
            rs.DoQuery(" update ""OINC"" SET ""U_EXO_COM""='Y' WHERE ""DocEntry""='" + NumInterno + "'")

        Catch ex As Exception

            log.escribeMensaje(ex.Message, EXO_Log.EXO_Log.Tipo.error)
            jRes.Resultado = "Error: " + ex.Message

        End Try

        res = js.Serialize(jRes)

        Return res

    End Function

#End Region

End Class
