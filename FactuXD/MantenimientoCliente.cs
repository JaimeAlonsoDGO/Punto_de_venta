using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MiLibreria; //Importamos nuestra libreria

namespace FactuXD
{
    public partial class MantenimientoCliente : Mantenimiento
    {
        public MantenimientoCliente()
        {
            InitializeComponent();
        }

        public override Boolean Guardar()
        {
            if(Utilidades.ValidarFormulario(this, errorProvider1) == false) //En caso de que el método Validar Formulario nos indique que no está vacio
            {
                try
                {
                    /////CREANDO CADENA PARA CONSULTA
                    string cmd = string.Format("EXEC ActualizaCliente '{0}','{1}','{2}'", txtIDCli.Text.Trim(), txtNomCli.Text.Trim(), txtApeCli.Text.Trim());
                    Utilidades.Ejecutar(cmd);
                    MessageBox.Show("GUARDADO EXITOSAMENTE");
                    return true;
                }//TRY
                catch (Exception error)
                {
                    MessageBox.Show("ERROR: " + error);
                    return false;
                }//CATCH
            } //IF
            else
            {
                return false;
            }//else
                
        } //GUARDAR

        public override void Eliminar()
        {
            try
            {
                string cmd = string.Format("EXEC EliminarCliente '{0}'", txtIDCli.Text.Trim());
                Utilidades.Ejecutar(cmd);
                MessageBox.Show("ELIMINADO EXITOSAMENTE");

            }//TRY
            catch(Exception error)
            {
                MessageBox.Show("ERROR ");
            }
        } //ELIMINAR

        private void txtIDCli_TextChanged(object sender, EventArgs e)
        {
            errorProvider1.Clear();
        }
    }
}
