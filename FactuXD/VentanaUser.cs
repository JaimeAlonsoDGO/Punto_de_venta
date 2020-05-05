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
    public partial class VentanaUser : FormBase
    {
        public VentanaUser()
        {
            InitializeComponent();
        }

        private void VentanaUser_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void VentanaUser_Load(object sender, EventArgs e)
        {
            //Creamos cadena de consulta para el DataSet
            string cmd = "SELECT * FROM Usuarios WHERE id_usuario=" + VentanaLogin.Codigo;

            DataSet DS = Utilidades.Ejecutar(cmd);//Ejecutamos nuestra cadena de instrucciones en el DataSet

            //Rellenamos cada elemetno
            lblNomUs.Text = DS.Tables[0].Rows[0]["Nom_usu"].ToString().Trim();
            lblUs.Text = DS.Tables[0].Rows[0]["account"].ToString().Trim();
            lblCodigo.Text = DS.Tables[0].Rows[0]["id_usuario"].ToString().Trim();

            String URL = DS.Tables[0].Rows[0]["Foto"].ToString().Trim();
            pictureBox1.Image = Image.FromFile(URL);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ContenedorPrincipal ConP = new ContenedorPrincipal();
            this.Hide();
            ConP.Show();
        }
    }
}
