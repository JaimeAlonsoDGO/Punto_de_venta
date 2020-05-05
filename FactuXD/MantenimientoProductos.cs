using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MiLibreria; //Iportamos nuestra galeria de funciones

namespace FactuXD
{
    public partial class MantenimientoProductos : Mantenimiento
    {
        public MantenimientoProductos()
        {
            InitializeComponent();
        }

        public override Boolean Guardar()
        {
            if(Utilidades.ValidarFormulario(this, errorProvider1) == false)
            {
                try
                {
                    //CREAMOS LA CADENA PARA EL PROCEDIMIENTO ALMACENADO EN NUESTRA BD
                    string cmd = string.Format("EXEC ActualizaArtiulos '{0}','{1}','{2}'", txtIDPro.Text.Trim(), txtNomPro.Text.Trim(), txtPrecio.Text.Trim());
                    Utilidades.Ejecutar(cmd); //Le pasamos nuestra cadena a la función EJECUTAR
                    MessageBox.Show("Se ha guardado correctamente.");
                    return true;

                } //try
                catch (Exception error)
                {
                    MessageBox.Show("HA OCURRIDO UN ERROR: " + error);
                    return false;
                }//Cath
            }//IF
            else
            {
                return false;
            }//ELSE
        } //GUARDAR

        public override void Eliminar()
        {
            try
            {
                string cmd = string.Format("EXEC EliminarArticulo '{0}'", txtIDPro.Text.Trim());
                Utilidades.Ejecutar(cmd);
                MessageBox.Show("SE ELIMINÓ CORRECTAMENTE");
            }
            catch(Exception error)
            {
                MessageBox.Show("HA OCURRIDO UN ERROR: " + error);
            }//catch
            
        } //ELIMINAR

        private void txtIDPro_TextChanged(object sender, EventArgs e)
        {
            errorProvider1.Clear();
        }
    }
}
