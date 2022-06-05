using System.Windows.Forms;
using System.Data;

public class combos
{
    public static bool Categoriaproducto(ComboBox Combo)
    {
        string Query = "";
        conectorSql conecta = new conectorSql();
        Query = "select idcategoria,descripcion from Cat_Categorias order by descripcion asc";
        DataTable conten = conecta.Lectura(Query);
        Combo.DataSource = conten;
        Combo.ValueMember = conten.Columns[0].ToString().Trim();
        Combo.DisplayMember = conten.Columns[1].ToString().Trim();
        Combo.Text = "";
        return true;
    }

    //AGREGADO POR JOSE 04-12-2019
    public static bool CatalogoTipos(ComboBox Combo)
    {
        string Query = "";
        conectorSql conecta = new conectorSql();
        Query = "select idtipo,descripcion from Cat_tipos order by descripcion asc";
        DataTable conten = conecta.Lectura(Query);
        Combo.DataSource = conten;
        Combo.ValueMember = conten.Columns[0].ToString().Trim();
        Combo.DisplayMember = conten.Columns[1].ToString().Trim();
        Combo.Text = "";
        return true;
   }


public static bool Unidadproducto(ComboBox Combo)
    {
        string Query = "";
        conectorSql conecta = new conectorSql();
        Query = "select distinct(unidad) from productos order by unidad asc";
        DataTable conten = conecta.Lectura(Query);
        Combo.DataSource = conten;
        Combo.ValueMember = conten.Columns[0].ToString().Trim();
        Combo.DisplayMember = conten.Columns[0].ToString().Trim();
        Combo.Text = "";
        return true;
    }

    public static bool ComboBancos(ComboBox Combo)
    {
        string Query = "";
        conectorSql conecta = new conectorSql();
        Query = "select cvbanco, nombre from Bancos order by nombre";
        DataTable conten = conecta.Lectura(Query);
        Combo.DataSource = conten;
        Combo.ValueMember = conten.Columns[0].ToString().Trim();
        Combo.DisplayMember = conten.Columns[1].ToString().Trim();
        Combo.Text = "";
        return true;
    }

    public static bool ComboFormadePago(ComboBox Combo)
    {
        string Query = "";
        conectorSql conecta = new conectorSql();
        Query = "select  nombre from formadepago order by nombre";
        DataTable conten = conecta.Lectura(Query);
        Combo.DataSource = conten;
        Combo.ValueMember = conten.Columns[0].ToString().ToUpper().Trim();
        Combo.DisplayMember = conten.Columns[0].ToString().ToUpper().Trim();
        Combo.Text = "";
        return true;
    }

    public static bool ComboVendedores(ComboBox Combo)
    {
        string Query = "";
        conectorSql conecta = new conectorSql();
        Query = "select cvvendedor, nombre from vendedores order by cvvendedor asc";
        DataTable conten = conecta.Lectura(Query);
        Combo.DataSource = conten;
        Combo.ValueMember = conten.Columns[0].ToString().Trim();
        Combo.DisplayMember = conten.Columns[1].ToString().Trim();
        Combo.Text = "";
        return true;
    }

    public static bool ComboDoctores(ComboBox Combo)
    {
        string Query = "";
        conectorSql conecta = new conectorSql();
        Query = "select cvdoctor, nombre from doctores order by nombre asc";
        DataTable conten = conecta.Lectura(Query);
        Combo.DataSource = conten;
        Combo.ValueMember = conten.Columns[0].ToString().Trim();
        Combo.DisplayMember = conten.Columns[1].ToString().Trim();
        Combo.Text = "";
        return true;
    }

    public static bool ComboDoctoresConFiltros(ComboBox Combo, string cvdoctor)
    {
        string Query = "";
        conectorSql conecta = new conectorSql();
        Query = "select cvdoctor, nombre from doctores where cvdoctor='" +  cvdoctor+ "' or  cvdoctor='15' order by nombre asc";
        DataTable conten = conecta.Lectura(Query);
        Combo.DataSource = conten;
        Combo.ValueMember = conten.Columns[0].ToString().Trim();
        Combo.DisplayMember = conten.Columns[1].ToString().Trim();
        Combo.Text = "";
        return true;
    }

   
    public static bool ComboServicio(ComboBox Combo)
    {
        string Query = "";
        conectorSql conecta = new conectorSql();
        Query = "select cvservicio, nombre from CatServicios order by nombre asc";
        DataTable conten = conecta.Lectura(Query);
        Combo.DataSource = conten;
        Combo.ValueMember = conten.Columns[0].ToString().Trim();
        Combo.DisplayMember = conten.Columns[1].ToString().Trim();
        Combo.Text = "";
        return true;
    }

    public static bool ComboConceptosPago(ComboBox Combo)
    {
        string Query = "";
        conectorSql conecta = new conectorSql();
        Query = "select cvconcepto, nombre from ConceptosPago order by nombre";
        DataTable conten = conecta.Lectura(Query);
        Combo.DataSource = conten;
        Combo.ValueMember = conten.Columns[0].ToString().Trim();
        Combo.DisplayMember = conten.Columns[1].ToString().Trim();
        Combo.Text = "";
        return true;
    }

    public static bool EmitioRemisiones(ComboBox Combo)
    {
        string Query = "";
        conectorSql conecta = new conectorSql();
        Query = "select distinct(emitio) from remisiones order by emitio asc";
        DataTable conten = conecta.Lectura(Query);
        Combo.DataSource = conten;
        Combo.ValueMember = conten.Columns[0].ToString().Trim();
        Combo.DisplayMember = conten.Columns[0].ToString().Trim();
        Combo.Text = "";
        return true;
    }

    public static bool UsuariosSistema(ComboBox Combo)
    {
        string Query = "";
        conectorSql conecta = new conectorSql();
        Query = "select distinct(cvusuario) from usuarios order by cvusuario asc";
        DataTable conten = conecta.Lectura(Query);
        Combo.DataSource = conten;
        Combo.ValueMember = conten.Columns[0].ToString().Trim();
        Combo.DisplayMember = conten.Columns[0].ToString().Trim();
        Combo.Text = "";
        return true;
    }

    public static bool ListaClientes(ComboBox Combo)
    {
        string Query = "";
        conectorSql conecta = new conectorSql();
        Query = "select distinct(cvcliente) from clientes order by cvcliente asc";
        DataTable conten = conecta.Lectura(Query);
        Combo.DataSource = conten;
        Combo.ValueMember = conten.Columns[0].ToString().Trim();
        Combo.DisplayMember = conten.Columns[0].ToString().Trim();
        Combo.Text = "";
        return true;
    }

    public static bool UnidadesProductos(ComboBox Combo)
    {
        string Query = "";
        conectorSql conecta = new conectorSql();
        Query = "select distinct(unidad) from productos order by unidad asc";
        DataTable conten = conecta.Lectura(Query);
        Combo.DataSource = conten;
        Combo.ValueMember = conten.Columns[0].ToString().Trim();
        Combo.DisplayMember = conten.Columns[0].ToString().Trim();
        Combo.Text = "";
        return true;
    }

    public static bool ColoniasRecibosporentregar(ComboBox Combo)
    {
        string Query = "";
        conectorSql conecta = new conectorSql();
        Query = "select distinct(colonia) from recibos where entregado='NO' order by colonia asc";
        DataTable conten = conecta.Lectura(Query);
        Combo.DataSource = conten;
        Combo.ValueMember = conten.Columns[0].ToString().Trim();
        Combo.DisplayMember = conten.Columns[0].ToString().Trim();
        Combo.Text = "";
        return true;
    }

    public static bool MarcasProductos(ComboBox Combo)
    {
        string Query = "";
        conectorSql conecta = new conectorSql();
        Query = "select distinct(marca) from productos order by marca asc";
        DataTable conten = conecta.Lectura(Query);
        Combo.DataSource = conten;
        Combo.ValueMember = conten.Columns[0].ToString().Trim();
        Combo.DisplayMember = conten.Columns[0].ToString().Trim();
        Combo.Text = "";
        return true;
    }




    public static bool ProveedoresProductos(ComboBox Combo)
    {
        string Query = "";
        conectorSql conecta = new conectorSql();
        Query = "select distinct(cvproveedor) as clave, nombre from proveedores order by nombre asc";
        DataTable conten = conecta.Lectura(Query);
        Combo.DataSource = conten;
        Combo.ValueMember = conten.Columns[0].ToString().Trim();
        Combo.DisplayMember = conten.Columns[1].ToString().Trim();
        Combo.Text = "";
        return true;
    }

    
}

