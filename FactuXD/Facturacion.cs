using MiLibreria;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FactuXD
{
    public partial class Facturacion : Procesos
    {
        public Facturacion()
        {
            InitializeComponent();
        }

        private void Facturacion_Load(object sender, EventArgs e)
        {
            //CARGAMOS EL NOMBRE DE LA PERONA QUE LE ATIENDE
            string cmd = "SELECT * FROM Usuarios WHERE id_usuario = " + VentanaLogin.Codigo;
            DataSet DS;
            DS = Utilidades.Ejecutar(cmd);

            lblAtiende.Text = DS.Tables[0].Rows[0]["Nom_usu"].ToString().Trim();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtCodigoCli.Text.Trim()) == false)
            {
                try
                {
                    //CREAMOS LA CADENA PARA CONSULTAR NOMBRE DEL CLIENTE
                    string cmd = string.Format("SELECT * FROM Cliente WHERE id_clientes = '{0}'", txtCodigoCli.Text.Trim());
                    DataSet DS;
                    DS = Utilidades.Ejecutar(cmd);
                    txtCliente.Text = DS.Tables[0].Rows[0]["Nom_cli"].ToString().Trim() + " "+ DS.Tables[0].Rows[0]["Ape_cli"].ToString().Trim();
                    txtCodigoPro.Focus();
                }//TRY
                catch(Exception error)
                {
                    MessageBox.Show("ERROR:" + error.Message);
                }
            }//IF
        }

        public static int con_fila = 0;
        public static double total;

        private void btnColocar_Click(object sender, EventArgs e)
        {
            if(Utilidades.ValidarFormulario(this, errorProvider1) == false) //VALIDAMOS QUE LOS CAMPOS REQUERIDOS ESTÉN LLENADOS
            {
                bool existe = false; //Variable que nos indicará si el registro que queremos ingresar existe o no

                int num_fila = 0; //Controla el numero de fila que estamos validando

                if(con_fila == 0) //Validamos si aun no tenemos registros 
                {
                    dataGridView1.Rows.Add(txtCodigoPro.Text, TxtDescripcion.Text, TxtPrecio.Text, TxtCantidad.Text); //Colocamos todos los valores de .os txtBox en orden
                    //OBTENEMOS EL IMPORTE MULTIPLICANDO EL VALOR DE LAS CELDAS CANTIDAD * PRECIO
                    double importe = Convert.ToDouble(dataGridView1.Rows[con_fila].Cells[2].Value) * Convert.ToDouble(dataGridView1.Rows[con_fila].Cells[3].Value);
                    //ASIGNAMOS A LA COLUMNA IMPORTE EL VALOR DE LA VARIABLE IMPORTE
                    dataGridView1.Rows[con_fila].Cells[4].Value = importe;
                    
                    con_fila++; //Aumentamos nuestra variable con_fila
                }//IF

                else
                {
                    foreach(DataGridViewRow fila in dataGridView1.Rows) //RECORRER LOS REGISTRO EN BUSQUEDA DE COINCIDENCIAS
                    {
                        if(fila.Cells[0].Value.ToString() == txtCodigoPro.Text) //Comparar si lo ingresado en el CODIGO PRODUCTO es igual al de la fila seleccionada
                        {
                            //Si es igual tendremos que añadirlo al ya existente
                            existe = true;
                            num_fila = fila.Index;
                            break;
                        }//
                    }//FOREACH

                    if(existe == true) //Si existe, se debe adjuntar lo que estamos agregando a lo ya existente
                    {
                        dataGridView1.Rows[num_fila].Cells[3].Value = (Convert.ToDouble(dataGridView1.Rows[num_fila].Cells[3].Value) + Convert.ToDouble(TxtCantidad.Text)).ToString();
                        double importe = Convert.ToDouble(dataGridView1.Rows[num_fila].Cells[2].Value) * Convert.ToDouble(dataGridView1.Rows[num_fila].Cells[3].Value);
                        dataGridView1.Rows[num_fila].Cells[4].Value = importe;
                    }//IF
                    else
                    {
                        dataGridView1.Rows.Add(txtCodigoPro.Text, TxtDescripcion.Text, TxtPrecio.Text, TxtCantidad.Text); //Colocamos todos los valores de .os txtBox en orden
                                                                                                                        //OBTENEMOS EL IMPORTE MULTIPLICANDO EL VALOR DE LAS CELDAS CANTIDAD * PRECIO
                        double importe = Convert.ToDouble(dataGridView1.Rows[con_fila].Cells[2].Value) * Convert.ToDouble(dataGridView1.Rows[con_fila].Cells[3].Value);
                        //ASIGNAMOS A LA COLUMNA IMPORTE EL VALOR DE LA VARIABLE IMPORTE
                        dataGridView1.Rows[con_fila].Cells[4].Value = importe;
                        
                        con_fila++; //Aumentamos nuestra variable con_fila
                    }//ELSE
                }//ELSE
            }//IF

            //AQUI creamos el total que se mostrata en el LblTotal
            total = 0;
            foreach (DataGridViewRow fila in dataGridView1.Rows)
            {
                total += Convert.ToDouble(fila.Cells[4].Value);
                lblTotal.Text ="$ " + total.ToString();
            }//ForEach

        }//COLOCAR

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (con_fila > 0) //Preguntamos si yya hay registros en el dataGridView
            {
                total = total - (Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[4].Value)); //Se asigna el nuevo valor a total
                lblTotal.Text = "$ " + total.ToString(); //Actualizamos el total que se muesta en nuestro label total

                dataGridView1.Rows.RemoveAt(dataGridView1.CurrentRow.Index); //Removemos el articulo que está seleccionado
                con_fila--; //Reducimos el contador en uno, ya que hay un articulo menos
            }
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            ConsultarClientes ConCli = new ConsultarClientes();
            ConCli.ShowDialog();

            txtCodigoCli.Text = ConCli.dataGridView1.Rows[ConCli.dataGridView1.CurrentRow.Index].Cells[0].Value.ToString();
            txtCliente.Text = ConCli.dataGridView1.Rows[ConCli.dataGridView1.CurrentRow.Index].Cells[1].Value.ToString();

            txtCodigoPro.Focus();
        }

        private void btnProductos_Click(object sender, EventArgs e)
        {
            ConsultarProductos ConPro = new ConsultarProductos();
            ConPro.ShowDialog();

            txtCodigoPro.Text = ConPro.dataGridView1.Rows[ConPro.dataGridView1.CurrentRow.Index].Cells[0].Value.ToString();
            TxtDescripcion.Text = ConPro.dataGridView1.Rows[ConPro.dataGridView1.CurrentRow.Index].Cells[1].Value.ToString();
            TxtPrecio.Text = ConPro.dataGridView1.Rows[ConPro.dataGridView1.CurrentRow.Index].Cells[2].Value.ToString();
            TxtCantidad.Focus();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Nuevo();
        }

        public override void Nuevo()
        {
            txtCodigoCli.Text = "";
            txtCliente.Text = "";
            txtCodigoPro.Text = "";
            TxtDescripcion.Text = "";
            TxtCantidad.Text = "";
            TxtPrecio.Text = "";
            lblTotal.Text = "$ 0";
            dataGridView1.Rows.Clear();
            txtCodigoCli.Focus();

        }
    }
}
