Imports System.IO
Imports System.Runtime.Serialization

Imports System.Runtime.Serialization.Json


Public Class Form1

    Dim cliente As WS_GP.EXO_WS_GPClient = New WS_GP.EXO_WS_GPClient

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        System.Net.ServicePointManager.ServerCertificateValidationCallback = New System.Net.Security.RemoteCertificateValidationCallback(AddressOf ValidarCertificado)
        Dim cliente As WS_GP.EXO_WS_GPClient = New WS_GP.EXO_WS_GPClient

    End Sub

    Private Function ValidarCertificado(ByVal sender As Object, ByVal certificate As System.Security.Cryptography.X509Certificates.X509Certificate, ByVal chain As System.Security.Cryptography.X509Certificates.X509Chain, ByVal sslPolicyErrors As System.Net.Security.SslPolicyErrors) As Boolean
        Return True
    End Function

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        System.Net.ServicePointManager.ServerCertificateValidationCallback = New System.Net.Security.RemoteCertificateValidationCallback(AddressOf ValidarCertificado)
        Dim cliente As WS_GP.EXO_WS_GPClient = New WS_GP.EXO_WS_GPClient
        Dim respuestas As String = ""

        respuestas = cliente.ping()
        MessageBox.Show(respuestas)



    End Sub


    Private Sub Button23_Click(sender As Object, e As EventArgs) Handles Button23.Click
        System.Net.ServicePointManager.ServerCertificateValidationCallback = New System.Net.Security.RemoteCertificateValidationCallback(AddressOf ValidarCertificado)
        Dim cliente As WS_GP.EXO_WS_GPClient = New WS_GP.EXO_WS_GPClient
        Dim respuestas As String = ""

        respuestas = cliente.BasesDeDatos()
        MessageBox.Show(respuestas)

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        System.Net.ServicePointManager.ServerCertificateValidationCallback = New System.Net.Security.RemoteCertificateValidationCallback(AddressOf ValidarCertificado)
        Dim cliente As WS_GP.EXO_WS_GPClient = New WS_GP.EXO_WS_GPClient
        Dim respuestas As String = ""

        respuestas = cliente.LoginUsuario("SBORIBAWOODSL", "mperiz", "M@rt1nN1c0")
        MessageBox.Show(respuestas)

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        System.Net.ServicePointManager.ServerCertificateValidationCallback = New System.Net.Security.RemoteCertificateValidationCallback(AddressOf ValidarCertificado)
        Dim cliente As WS_GP.EXO_WS_GPClient = New WS_GP.EXO_WS_GPClient
        Dim respuestas As String = ""

        respuestas = cliente.UbicacionesDelAlmacen("ALEX_BANKINTER", "mperiz", "M@rt1nN1c0")
        MessageBox.Show(respuestas)
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        System.Net.ServicePointManager.ServerCertificateValidationCallback = New System.Net.Security.RemoteCertificateValidationCallback(AddressOf ValidarCertificado)
        Dim cliente As WS_GP.EXO_WS_GPClient = New WS_GP.EXO_WS_GPClient
        Dim respuestas As String = ""

        respuestas = cliente.RecepcionMaterialesBuscador("DEMO_SBO", "mperiz", "M@rt1nN1c0", "", "", "", "")
        MessageBox.Show(respuestas)
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        System.Net.ServicePointManager.ServerCertificateValidationCallback = New System.Net.Security.RemoteCertificateValidationCallback(AddressOf ValidarCertificado)
        Dim cliente As WS_GP.EXO_WS_GPClient = New WS_GP.EXO_WS_GPClient
        Dim respuestas As String = ""


        Dim oRegLinea As WS_GP.PedidoCompraRegistrarLinea = New WS_GP.PedidoCompraRegistrarLinea

        oRegLinea.NumInterno = "22"
        oRegLinea.NumLinea = 0
        oRegLinea.Proveedor = "P000002"
        oRegLinea.Codigo = "104001010100001"

        oRegLinea.Lote = ""
        oRegLinea.CantidadReal = 1
        oRegLinea.CantidadSeleccionada = 1

        oRegLinea.Ubicacion = "01_LANDE-A-A-2"

        oRegLinea.Peso = 10
        oRegLinea.Alto = 11
        oRegLinea.Ancho = 12
        oRegLinea.Largo = 13

        Dim str As New MemoryStream()
        Dim js As New System.Runtime.Serialization.Json.DataContractJsonSerializer(oRegLinea.GetType)
        js.WriteObject(str, oRegLinea)
        str.Position = 0
        Dim sr As New StreamReader(str)
        Dim JSON As String = sr.ReadToEnd()

        respuestas = cliente.PedidoCompraRegistrarLinea("ALEX_BANKINTER", "mperiz", "M@rt1nN1c0", JSON)
        MessageBox.Show(respuestas)
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click

        System.Net.ServicePointManager.ServerCertificateValidationCallback = New System.Net.Security.RemoteCertificateValidationCallback(AddressOf ValidarCertificado)
        Dim cliente As WS_GP.EXO_WS_GPClient = New WS_GP.EXO_WS_GPClient
        Dim respuestas As String = ""

        respuestas = cliente.PedidoCompraGenerar("DEMO_SBO", "mperiz", "M@rt1nN1c0")
        MessageBox.Show(respuestas)
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        System.Net.ServicePointManager.ServerCertificateValidationCallback = New System.Net.Security.RemoteCertificateValidationCallback(AddressOf ValidarCertificado)
        Dim cliente As WS_GP.EXO_WS_GPClient = New WS_GP.EXO_WS_GPClient
        Dim respuestas As String = ""

        respuestas = cliente.ListasPicking("ALEX_BANKINTER", "mperiz", "M@rt1nN1c0")
        MessageBox.Show(respuestas)
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        System.Net.ServicePointManager.ServerCertificateValidationCallback = New System.Net.Security.RemoteCertificateValidationCallback(AddressOf ValidarCertificado)
        Dim cliente As WS_GP.EXO_WS_GPClient = New WS_GP.EXO_WS_GPClient
        Dim respuestas As String = ""

        respuestas = cliente.UbicacionesDelAlmacenBahias("ALEX_BANKINTER", "mperiz", "M@rt1nN1c0")
        MessageBox.Show(respuestas)
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        System.Net.ServicePointManager.ServerCertificateValidationCallback = New System.Net.Security.RemoteCertificateValidationCallback(AddressOf ValidarCertificado)
        Dim cliente As WS_GP.EXO_WS_GPClient = New WS_GP.EXO_WS_GPClient
        Dim respuestas As String = ""

        respuestas = cliente.DesglosePicking("DEMO_SBO", "mperiz", "M@rt1nN1c0", 18)
        MessageBox.Show(respuestas)
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click

        System.Net.ServicePointManager.ServerCertificateValidationCallback = New System.Net.Security.RemoteCertificateValidationCallback(AddressOf ValidarCertificado)
        Dim cliente As WS_GP.EXO_WS_GPClient = New WS_GP.EXO_WS_GPClient
        Dim respuestas As String = ""

        Dim oTraslado As WS_GP.Traslado = New WS_GP.Traslado

        'oTraslado.CodigoArticulo = "500703530601"
        'oTraslado.Cantidad = 14
        'oTraslado.Lote = "C0002E17080002-0008"
        'oTraslado.Almacen = "02"
        'oTraslado.UbicacionOrigen = "02.15.PLAYA"
        'oTraslado.UbicacionDestino = "02.12.10.23.C"

        oTraslado.CodigoArticulo = "111001089900007"
        oTraslado.Cantidad = 500
        oTraslado.Lote = "19122017004"
        oTraslado.Almacen = "01LANDE"
        oTraslado.UbicacionOrigen = "01LANDEA010C"
        oTraslado.UbicacionDestino = "01LANDEA001B"
        oTraslado.NumeroPicking = 31
        oTraslado.PickingLinea = 0

        Dim str As New MemoryStream()
        Dim js As New System.Runtime.Serialization.Json.DataContractJsonSerializer(oTraslado.GetType)
        js.WriteObject(str, oTraslado)
        str.Position = 0
        Dim sr As New StreamReader(str)
        Dim JSON As String = sr.ReadToEnd()

        respuestas = cliente.OperacionesTraslado(JSON, "ALEX_BANKINTER", "mperiz", "M@rt1nN1c0")
        MessageBox.Show(respuestas)
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click

        System.Net.ServicePointManager.ServerCertificateValidationCallback = New System.Net.Security.RemoteCertificateValidationCallback(AddressOf ValidarCertificado)
        Dim cliente As WS_GP.EXO_WS_GPClient = New WS_GP.EXO_WS_GPClient
        Dim respuestas As String = ""

        Dim oPicking As WS_GP.GenerarPicking = New WS_GP.GenerarPicking

        Dim oLinea As WS_GP.LineasPicking = New WS_GP.LineasPicking
        Dim oLineas As List(Of WS_GP.LineasPicking) = New List(Of WS_GP.LineasPicking)

        Dim oBulto As WS_GP.BultosPicking = New WS_GP.BultosPicking
        Dim oBultos As List(Of WS_GP.BultosPicking) = New List(Of WS_GP.BultosPicking)

        Dim oPalet As WS_GP.PaletsPicking = New WS_GP.PaletsPicking
        Dim oPalets As List(Of WS_GP.PaletsPicking) = New List(Of WS_GP.PaletsPicking)



        oPicking.NumeroPicking = 3
        oPicking.Ubicacion = "02BAHIA"
        oPicking.Resultado = ""

        oLinea = New WS_GP.LineasPicking
        oLinea.Articulo = "000003"
        oLinea.Cantidad = 1
        oLinea.Lote = ""
        oLinea.PickingLinea = 1
        oLineas.Add(oLinea)

        oLinea = New WS_GP.LineasPicking
        oLinea.Articulo = "000002"
        oLinea.Cantidad = 1
        oLinea.Lote = "L02"
        oLinea.PickingLinea = 0
        oLineas.Add(oLinea)

        oPicking.Lineas = oLineas.ToArray

        oBulto = New WS_GP.BultosPicking
        oBulto.Articulo = "110001110100001"
        oBulto.Cantidad = 2000
        oBulto.Lote = ""
        oBulto.Bulto = 1
        oBulto.LineaPicking = 1
        oBultos.Add(oBulto)


        oPicking.Bultos = oBultos.ToArray

        oPalet = New WS_GP.PaletsPicking
        oPalet.Tipo = "europalet"
        oPalet.Palet = 1
        oPalet.Peso = 1
        oPalet.Volumen = 0.96
        oPalet.Altura = 1
        oPalets.Add(oPalet)

        oPicking.Palets = oPalets.ToArray

        Dim str As New MemoryStream()
        Dim js As New System.Runtime.Serialization.Json.DataContractJsonSerializer(oPicking.GetType)
        js.WriteObject(str, oPicking)
        str.Position = 0
        Dim sr As New StreamReader(str)
        Dim JSON As String = sr.ReadToEnd()

        'Dim path As String = "E:\Desarrollo\Usuarios\mperiz\picking.txt"
        'Dim readText As String = File.ReadAllText(path)
        'JSON = readText

        respuestas = cliente.GenerarPicking2(JSON, "DEMO_SBO", "mperiz", "M@rt1nN1c0")


        MessageBox.Show(respuestas)
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        System.Net.ServicePointManager.ServerCertificateValidationCallback = New System.Net.Security.RemoteCertificateValidationCallback(AddressOf ValidarCertificado)
        Dim cliente As WS_GP.EXO_WS_GPClient = New WS_GP.EXO_WS_GPClient
        Dim respuestas As String = ""

        respuestas = cliente.ComprobarExisteArticulo("ALEX_BANKINTER", "mperiz", "M@rt1nN1c0", "018435043130414")
        MessageBox.Show(respuestas)
    End Sub

    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click
        System.Net.ServicePointManager.ServerCertificateValidationCallback = New System.Net.Security.RemoteCertificateValidationCallback(AddressOf ValidarCertificado)
        Dim cliente As WS_GP.EXO_WS_GPClient = New WS_GP.EXO_WS_GPClient
        Dim respuestas As String = ""

        respuestas = cliente.ComprobarArticuloSalida("ALEX_BANKINTER", "mperiz", "M@rt1nN1c0", "111001089900001", "", 500, "01_LANDE-A-A-1")
        MessageBox.Show(respuestas)
    End Sub

    Private Sub Button14_Click(sender As Object, e As EventArgs) Handles Button14.Click
        Dim cliente As WS_GP.EXO_WS_GPClient = New WS_GP.EXO_WS_GPClient
        Dim respuestas As String = ""
        Dim oDoc As WS_GP.OperacionEntradaSalida = New WS_GP.OperacionEntradaSalida

        Dim oLinea As WS_GP.Articulo = New WS_GP.Articulo
        Dim oLineas As List(Of WS_GP.Articulo) = New List(Of WS_GP.Articulo)

        oLinea = New WS_GP.Articulo
        oLinea.ArticuloMember = "111001089900007"
        oLinea.Cantidad = 500
        oLinea.Lote = "19122017004"
        oLinea.Ubicacion = "01LANDEA010C"

        oLineas.Add(oLinea)

        oLinea = New WS_GP.Articulo
        oLinea.ArticuloMember = "111001089900007"
        oLinea.Cantidad = 1
        oLinea.Lote = "11122017_1"
        oLinea.Ubicacion = "01LANDEA010C"
        oLineas.Add(oLinea)

        oLinea = New WS_GP.Articulo
        oLinea.ArticuloMember = "111001089900007"
        oLinea.Cantidad = 1
        oLinea.Lote = "11122017_2"
        oLinea.Ubicacion = "01LANDEA010C"
        oLineas.Add(oLinea)

        oDoc.Lineas = oLineas.ToArray

        Dim str As New MemoryStream()
        Dim js As New System.Runtime.Serialization.Json.DataContractJsonSerializer(oDoc.GetType)
        js.WriteObject(str, oDoc)
        str.Position = 0
        Dim sr As New StreamReader(str)
        Dim JSON As String = sr.ReadToEnd()

        respuestas = cliente.GenerarDocumentoEntradaManual(JSON, "ALEX_BANKINTER", "mperiz", "M@rt1nN1c0")
        MessageBox.Show(respuestas)
    End Sub

    Private Sub Button15_Click(sender As Object, e As EventArgs) Handles Button15.Click
        Dim cliente As WS_GP.EXO_WS_GPClient = New WS_GP.EXO_WS_GPClient
        Dim respuestas As String = ""
        Dim oDoc As WS_GP.OperacionEntradaSalida = New WS_GP.OperacionEntradaSalida

        Dim oLinea As WS_GP.Articulo = New WS_GP.Articulo
        Dim oLineas As List(Of WS_GP.Articulo) = New List(Of WS_GP.Articulo)

        oLinea = New WS_GP.Articulo
        oLinea.ArticuloMember = "111001089900001"
        oLinea.Cantidad = 10
        oLinea.Lote = ""
        oLinea.Ubicacion = "01_LANDE-A-A-2"
        oLineas.Add(oLinea)

        oLinea = New WS_GP.Articulo
        oLinea.ArticuloMember = "111001089900007"
        oLinea.Cantidad = 1
        oLinea.Lote = "11122017_1"
        oLinea.Ubicacion = "01_LANDE-A-A-2"
        oLineas.Add(oLinea)

        oLinea = New WS_GP.Articulo
        oLinea.ArticuloMember = "111001089900007"
        oLinea.Cantidad = 1
        oLinea.Lote = "11122017_2"
        oLinea.Ubicacion = "01_LANDE-A-A-2"
        oLineas.Add(oLinea)

        oDoc.Lineas = oLineas.ToArray

        Dim str As New MemoryStream()
        Dim js As New System.Runtime.Serialization.Json.DataContractJsonSerializer(oDoc.GetType)
        js.WriteObject(str, oDoc)
        str.Position = 0
        Dim sr As New StreamReader(str)
        Dim JSON As String = sr.ReadToEnd()

        respuestas = cliente.GenerarDocumentoSalidaManual(JSON, "ALEX_BANKINTER", "mperiz", "M@rt1nN1c0")
        MessageBox.Show(respuestas)
    End Sub

    Private Sub Button17_Click(sender As Object, e As EventArgs) Handles Button17.Click
        Dim cliente As WS_GP.EXO_WS_GPClient = New WS_GP.EXO_WS_GPClient
        Dim respuestas As String = ""

        respuestas = cliente.DesgloseSolicitudesTraslado("ALEX_BANKINTER", "mperiz", "M@rt1nN1c0", "1")
        MessageBox.Show(respuestas)

    End Sub

    Private Sub Button18_Click(sender As Object, e As EventArgs) Handles Button18.Click
        Dim cliente As WS_GP.EXO_WS_GPClient = New WS_GP.EXO_WS_GPClient
        Dim respuestas As String = ""

        respuestas = cliente.ListasSolicitudTraslado("ALEX_BANKINTER", "mperiz", "M@rt1nN1c0")
        MessageBox.Show(respuestas)
    End Sub

    Private Sub Button16_Click(sender As Object, e As EventArgs) Handles Button16.Click
        Dim cliente As WS_GP.EXO_WS_GPClient = New WS_GP.EXO_WS_GPClient
        Dim respuestas As String = ""
        Dim oDoc As WS_GP.OperacionTraslado = New WS_GP.OperacionTraslado

        Dim oLinea As WS_GP.LineasTraslado = New WS_GP.LineasTraslado
        Dim oLineas As List(Of WS_GP.LineasTraslado) = New List(Of WS_GP.LineasTraslado)

        oDoc.NumeroSolTraslado = 3


        oLinea = New WS_GP.LineasTraslado
        oLinea.Articulo = "104001020100001"
        oLinea.Cantidad = 35
        oLinea.Lote = ""
        oLinea.UbicacionOrigen = "01_LANDE-A-A-2"
        oLinea.UbicacionDestino = "01_LANDE-A-A-3"
        oLinea.NumeroLinea = 0
        oLineas.Add(oLinea)

        oLinea = New WS_GP.LineasTraslado
        oLinea.Articulo = "111001089900001"
        oLinea.Cantidad = 390
        oLinea.Lote = ""
        oLinea.UbicacionOrigen = "01_LANDE-A-A-5"
        oLinea.UbicacionDestino = "01_LANDE-A-A-3"
        oLinea.NumeroLinea = 1
        oLineas.Add(oLinea)

        oDoc.Lineas = oLineas.ToArray

        Dim str As New MemoryStream()
        Dim js As New System.Runtime.Serialization.Json.DataContractJsonSerializer(oDoc.GetType)
        js.WriteObject(str, oDoc)
        str.Position = 0
        Dim sr As New StreamReader(str)
        Dim JSON As String = sr.ReadToEnd()

        respuestas = cliente.GenerarOperacionTraslado(JSON, "ALEX_BANKINTER", "mperiz", "M@rt1nN1c0")
        MessageBox.Show(respuestas)
    End Sub

    Private Sub Button19_Click(sender As Object, e As EventArgs) Handles Button19.Click



        Dim cliente As WS_GP.EXO_WS_GPClient = New WS_GP.EXO_WS_GPClient
        Dim respuestas As String = ""

        respuestas = cliente.ComprobarExisteArticulo("ALEX_BANKINTER", "mperiz", "M@rt1nN1c0", "0118426017016879102427")
        'respuestas = cliente.ComPruebaArticulo("ALEX_BANKINTER", "mperiz", "M@rt1nN1c0", "110001180200001", "]C1011843504310096410123", "Y")
        MessageBox.Show(respuestas)
    End Sub

    Private Sub Button20_Click(sender As Object, e As EventArgs) Handles Button20.Click

        Dim cliente As WS_GP.EXO_WS_GPClient = New WS_GP.EXO_WS_GPClient
        Dim respuestas As String = ""

        respuestas = cliente.GenerarDraftEntrega("ALEX_BANKINTER", "mperiz", "M@rt1nN1c0")
        'respuestas = cliente.ComPruebaArticulo("ALEX_BANKINTER", "mperiz", "M@rt1nN1c0", "110001180200001", "]C1011843504310096410123", "Y")
        MessageBox.Show(respuestas)
    End Sub

    Private Sub Button21_Click(sender As Object, e As EventArgs) Handles Button21.Click

        Dim cliente As WS_GP.EXO_WS_GPClient = New WS_GP.EXO_WS_GPClient
        Dim respuestas As String = ""

        respuestas = cliente.PedidoCompraGenerar("ALEX_BANKINTER", "mperiz", "M@rt1nN1c0")
    End Sub

    Private Sub Button22_Click(sender As Object, e As EventArgs) Handles Button22.Click
        Dim cliente As WS_GP.EXO_WS_GPClient = New WS_GP.EXO_WS_GPClient
        Dim respuestas As String = ""

        'respuestas = cliente.ConsultaStock("ALEX_BANKINTER", "mperiz", "M@rt1nN1c0", "01LANDEC002A")
        respuestas = cliente.ConsultaStock("DEMO_SBO", "mperiz", "M@rt1nN1c0", "0125896314785241")

        MessageBox.Show(respuestas)
    End Sub

    Private Sub Button24_Click(sender As Object, e As EventArgs) Handles Button24.Click
        Dim cliente As WS_GP.EXO_WS_GPClient = New WS_GP.EXO_WS_GPClient
        Dim respuestas As String = ""

        'respuestas = cliente.ConsultaStock("ALEX_BANKINTER", "mperiz", "M@rt1nN1c0", "01LANDEC002A")
        respuestas = cliente.ListasRecuentoInventario("DEMO_SBO", "mperiz", "M@rt1nN1c0")

        MessageBox.Show(respuestas)

        '
    End Sub

    Private Sub Button25_Click(sender As Object, e As EventArgs) Handles Button25.Click
        Dim cliente As WS_GP.EXO_WS_GPClient = New WS_GP.EXO_WS_GPClient
        Dim respuestas As String = ""

        'respuestas = cliente.ConsultaStock("ALEX_BANKINTER", "mperiz", "M@rt1nN1c0", "01LANDEC002A")
        respuestas = cliente.DesgloseRecuentoInventario("DEMO_SBO", "mperiz", "M@rt1nN1c0", "21")

        MessageBox.Show(respuestas)
    End Sub

    Private Sub Button26_Click(sender As Object, e As EventArgs) Handles Button26.Click

        Dim cliente As WS_GP.EXO_WS_GPClient = New WS_GP.EXO_WS_GPClient
        Dim respuestas As String = ""
        Dim oDoc As WS_GP.ListasRecuentoInventarioCabecera = New WS_GP.ListasRecuentoInventarioCabecera

        Dim oLinea As WS_GP.ListasRecuentoInventarioDetalle = New WS_GP.ListasRecuentoInventarioDetalle
        Dim oLineas As List(Of WS_GP.ListasRecuentoInventarioDetalle) = New List(Of WS_GP.ListasRecuentoInventarioDetalle)

        oLinea = New WS_GP.ListasRecuentoInventarioDetalle
        oLinea.CantidadContada = "80"
        oLinea.Articulo = "000003"
        oLinea.CodUbicacion = "1"
        oLineas.Add(oLinea)

        'oLinea = New WS_GP.ListasRecuentoInventarioDetalle
        'oLinea.ArticuloMember = "111001089900007"
        'oLinea.Cantidad = 1
        'oLinea.Lote = "11122017_1"
        'oLinea.Ubicacion = "01_LANDE-A-A-2"
        'oLineas.Add(oLinea)

        oDoc.NumeroInterno = "5"

        oDoc.Lineas = oLineas.ToArray

        Dim str As New MemoryStream()
        Dim js As New System.Runtime.Serialization.Json.DataContractJsonSerializer(oDoc.GetType)
        js.WriteObject(str, oDoc)
        str.Position = 0
        Dim sr As New StreamReader(str)
        Dim JSON As String = sr.ReadToEnd()

        respuestas = cliente.GenerarRecuentoInventario(JSON, "DEMO_SBO", "mperiz", "M@rt1nN1c0")
        MessageBox.Show(respuestas)
    End Sub

    Private Sub Button27_Click(sender As Object, e As EventArgs) Handles Button27.Click
        Dim cliente As WS_GP.EXO_WS_GPClient = New WS_GP.EXO_WS_GPClient
        Dim respuestas As String = ""

        'respuestas = cliente.ConsultaStock("ALEX_BANKINTER", "mperiz", "M@rt1nN1c0", "01LANDEC002A")
        respuestas = cliente.RecuentoInventarioMarcarFinalizado("DEMO_SBO", "mperiz", "M@rt1nN1c0", "5")

        MessageBox.Show(respuestas)
    End Sub

    Private Sub Button28_Click(sender As Object, e As EventArgs) Handles Button28.Click

        Dim cliente As WS_GP.EXO_WS_GPClient = New WS_GP.EXO_WS_GPClient
        Dim respuestas As String = ""


        'respuestas = cliente.CompruebaLote("DEMO_SBO", "mperiz", "M@rt1nN1c0", "000002", "AS1", "")

        '{"Resultado":"Ok","Cantidad":103,"Lote":"AS1","Articulo":"000002","Ubicacion":"02P01E02"}
        'respuestas = cliente.CompruebaLote("DEMO_SBO", "mperiz", "M@rt1nN1c0", "", "AS1", "02P01E02")
        respuestas = cliente.CompruebaLoteReubicacion("SBORIBAWOODSL", "mperiz", "M@rt1nN1c0", "", "510266-6", "")

        MessageBox.Show(respuestas)
    End Sub

    Private Sub Button30_Click(sender As Object, e As EventArgs) Handles Button30.Click
        Dim cliente As WS_GP.EXO_WS_GPClient = New WS_GP.EXO_WS_GPClient
        Dim respuestas As String = ""


        'respuestas = cliente.CompruebaLote("DEMO_SBO", "mperiz", "M@rt1nN1c0", "000002", "AS1", "")

        '{"Resultado":"Ok","Cantidad":103,"Lote":"AS1","Articulo":"000002","Ubicacion":"02P01E02"}
        respuestas = cliente.CompruebaUbicacion("DEMO_SBO", "mperiz", "M@rt1nN1c0", "000002", "02P01E02", "Y")



        MessageBox.Show(respuestas)
    End Sub

    Private Sub Button29_Click(sender As Object, e As EventArgs) Handles Button29.Click

        System.Net.ServicePointManager.ServerCertificateValidationCallback = New System.Net.Security.RemoteCertificateValidationCallback(AddressOf ValidarCertificado)
        Dim cliente As WS_GP.EXO_WS_GPClient = New WS_GP.EXO_WS_GPClient
        Dim respuestas As String = ""

        Dim oTraslado As WS_GP.Traslado = New WS_GP.Traslado


        oTraslado.CodigoArticulo = "000002"
        oTraslado.Cantidad = 4
        oTraslado.Lote = ""
        oTraslado.Almacen = "02"
        oTraslado.UbicacionOrigen = "02P01E01"
        oTraslado.UbicacionDestino = "02P01E02"
        oTraslado.NumeroPicking = 31
        oTraslado.PickingLinea = 0


        Dim str As New MemoryStream()
        Dim js As New System.Runtime.Serialization.Json.DataContractJsonSerializer(oTraslado.GetType)
        js.WriteObject(str, oTraslado)
        str.Position = 0
        Dim sr As New StreamReader(str)
        Dim JSON As String = sr.ReadToEnd()

        respuestas = cliente.OperacionesTrasladoUbicacion(JSON, "DEMO_SBO", "mperiz", "M@rt1nN1c0")
        MessageBox.Show(respuestas)
    End Sub

    Private Sub Button31_Click(sender As Object, e As EventArgs) Handles Button31.Click
        Dim cliente As WS_GP.EXO_WS_GPClient = New WS_GP.EXO_WS_GPClient
        Dim respuestas As String = ""


        'respuestas = cliente.CompruebaLote("DEMO_SBO", "mperiz", "M@rt1nN1c0", "000002", "AS1", "")

        '{"Resultado":"Ok","Cantidad":103,"Lote":"AS1","Articulo":"000002","Ubicacion":"02P01E02"}
        respuestas = cliente.DetalleLoteLineaPicking("DEMO_SBO", "mperiz", "M@rt1nN1c0", "48", "0")



        MessageBox.Show(respuestas)
    End Sub
End Class
