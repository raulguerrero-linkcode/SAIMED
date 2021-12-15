using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;

 [XmlRootAttribute(Namespace = "http://www.sat.gob.mx/cfd/3")]
 public partial class Comprobante2
    {
        [XmlAttributeAttribute()]
        public string version;

        [XmlAttributeAttribute()]
        public string folio;

        [XmlAttributeAttribute()]
        public string fecha;

        [XmlAttributeAttribute()]
        public string sello;

        [XmlAttributeAttribute()]
        public string formaDePago;

        [XmlAttributeAttribute()]
        public string noCertificado;

        [XmlAttributeAttribute()]
        public string certificado;

        [XmlAttributeAttribute()]
        public string subTotal;

        [XmlAttributeAttribute()]
        public string Moneda;

        [XmlAttributeAttribute()]
        public string total;

        [XmlAttributeAttribute()]
        public string tipoDeComprobante;

        [XmlAttributeAttribute()]
        public string metodoDePago;

        [XmlAttributeAttribute()]
        public string LugarExpedicion;

        [XmlElementAttribute()]
        public TEmisor Emisor;

        [XmlElementAttribute()]
        public TReceptor Receptor;

        [XmlArrayItemAttribute("Concepto")]
        public TConcepto[] Conceptos;

        [XmlElementAttribute()]
        public TImpuestos Impuestos;

    }

    public partial class TEmisor
    {
        [XmlAttributeAttribute()]
        public string rfc;

        [XmlAttributeAttribute()]
        public string nombre;

        [XmlElementAttribute()]
        public TDomicilioFiscal DomicilioFiscal;

        [XmlElementAttribute()]
        public TRegimenFiscal[] RegimenFiscal;

    }

    public partial class TDomicilioFiscal
    {
        [XmlAttributeAttribute()]
        public string calle;

        [XmlAttributeAttribute()]
        public string municipio;

        [XmlAttributeAttribute()]
        public string estado;

        [XmlAttributeAttribute()]
        public string pais;

        [XmlAttributeAttribute()]
        public string codigoPostal;

    }

    public partial class TRegimenFiscal
    {
        [XmlAttributeAttribute()]
        public string Regimen;

    }

    public partial class TReceptor
    {
        [XmlAttributeAttribute()]
        public string rfc;

        [XmlAttributeAttribute()]
        public string nombre;

        [XmlElementAttribute()]
        public TDomicilio Domicilio;
    }

    public partial class TDomicilio
    {
        [XmlAttributeAttribute()]
        public string calle;

        [XmlAttributeAttribute()]
        public string noExterior;

        [XmlAttributeAttribute()]
        public string colonia;

        [XmlAttributeAttribute()]
        public string municipio;

        [XmlAttributeAttribute()]
        public string estado;

        [XmlAttributeAttribute()]
        public string pais;

        [XmlAttributeAttribute()]
        public string codigoPostal;
    }

    public partial class TConcepto
    {
        [XmlAttributeAttribute()]
        public string cantidad;

        [XmlAttributeAttribute()]
        public string unidad;

        [XmlAttributeAttribute()]
        public string descripcion;

        [XmlAttributeAttribute()]
        public string valorUnitario;

        [XmlAttributeAttribute()]
        public string importe;
    }

    public partial class TImpuestos
    {
        [XmlArrayItemAttribute("Traslado")]
        public TTraslado[] Traslados;
    }

    public partial class TTraslado
    {
        [XmlAttributeAttribute()]
        public string impuesto;

        [XmlAttributeAttribute()]
        public string tasa;

        [XmlAttributeAttribute()]
        public string importe;

    }


