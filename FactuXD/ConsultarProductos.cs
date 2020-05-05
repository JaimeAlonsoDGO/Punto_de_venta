using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MiLibreria;

namespace FactuXD
{
    public partial class ConsultarProductos : Consultas
    {
        public ConsultarProductos()
        {
            InitializeComponent();
        }

        private void ConsultarProductos_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = LlenarDataGV("Articulo").Tables[0];
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtNombre.Text.Trim()) == false) //Validamos que el txtBox no esté vacio
            {
                try
                {
                    DataSet DS; //Creamos DataSet
                    string cmd = "SELECT * FROM Articulo WHERE Nom_pro LIKE ('%" + txtNombre.Text.Trim() + "%')"; //Creamos Cadena de consulta
                    DS = Utilidades.Ejecutar(cmd); //Rellenamos nuetro DataSet

                    dataGridView1.DataSource = DS.Tables[0]; //Rellenamos nuestro DataGridView
                }//TRY
                catch (Exception error)
                {
                    MessageBox.Show("ERROR: " + error.Message);
                }//CATCH
            }//if
        }
    }
}
