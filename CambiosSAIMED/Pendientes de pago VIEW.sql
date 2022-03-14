/****** Script for SelectTopNRows command from SSMS  ******/
use CEPAMMCDMX
go

ALTER VIEW v_pedidos as
SELECT TOP 20
	[idcliente]
	,clientes.[cvcliente]
	,[nombre]     
      ,[factura]
      ,[activo]
      ,[fechamod]
      ,[metodopago]
      ,[formapago]
      ,[diascredito]
      ,[saldo]
      ,[numpedido]
      ,Pagos.[fecha]
      ,[concepto]
      ,[cvconcepto]
      ,[estatus]
      ,[fechapago]
      ,[pagocon]
      ,Pagos.[tipopago]
      
	  ,detalle.[cvproducto]
      ,detalle.[descripcion]
      ,detalle.[cantidad]
     -- ,detalle.[preunitario]
		,detalle.[precio]
	  ,[total]
      ,[iva]
      ,[totalgeneral]
	  ,[totalgeneral] + ISNULL( [cambio] , 0 ) as Pago_Con
      ,ISNULL([cambio],0) cambio

  FROM  [clientes] left join [Pagos] on clientes.[cvcliente] =  Pagos.[cvcliente]
	left join [DetallesRecibos] detalle on detalle.[cvcliente] = clientes.[cvcliente]
	left join [Recibos] rec on rec.[cvcliente] = clientes.[cvcliente] and rec.numrecibo = Pagos.numpedido


  where [cvconcepto] <>10
		AND [saldo]  IS NOT NULL