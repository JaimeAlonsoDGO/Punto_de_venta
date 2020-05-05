using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data; //Lo usamos para utilizar el DataSet
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient; //IMPLEMENTAR EL ESPACIO DE NOMBRE PARA LA CONEXION
using MiLibreria; //Agregamos el NAMESPACE de nuestro DLL

namespace FactuXD
{
    public partial class VentanaLogin : FormBase
    {
        public VentanaLogin()
        {
            InitializeComponent();
        }

        public static string Codigo = "";

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            try
            {
                //Se crea la cadena de consulta para nuestro archivo DLL
                string CMD = string.Format("Select * FROM Usuarios WHERE account = '{0}' AND password = '{1}'", txtNomAcc.Text.Trim(), txtPass.Text.Trim());

                DataSet ds = Utilidades.Ejecutar(CMD); // Rellenamos nuestro DataSet con lo obtenido en la consulta

                Codigo = ds.Tables[0].Rows[0]["id_usuario"].ToString().Trim();

                // Asignamos lo obtenido en el DS a dos variables string
                string cuenta = ds.Tables[0].Rows[0]["account"].ToString().Trim();
                string contra = ds.Tables[0].Rows[0]["password"].ToString().Trim();

                //Comparacion para inicio de sesion entre lo obtenido del DS y lo capturado en los TextBo
                if(cuenta == txtNomAcc.Text.Trim() && contra == txtPass.Text.Trim())
                {
                    // F para comparar si el usuario es Administrador o usuario
                    if(Convert.ToBoolean(ds.Tables[0].Rows[0]["Status_admin"]) == true)
                    {
                        VentanaAdmin VenAd = new VentanaAdmin(); //Crea instancia de Ventana Admin
                        this.Hide();// Esconde ventana login
                        VenAd.Show(); //Muestra la instancia creada de la ventana admin
                    }
                    else
                    {
                        VentanaUser VenUs = new VentanaUser();
                        this.Hide();
                        VenUs.Show();
                    }
                }

            }
            catch(Exception error)
            {
                MessageBox.Show("ERROR: Usuario y/o contraseña incorrecto");
                txtPass.Text = "";
                txtNomAcc.Text = "";
                txtNomAcc.Focus();
            }
        }

        //Metodo para cerrar la aplicacion completamente
        private void VtnLogin_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit(); //Para cerrar la app
        }
    }
}
