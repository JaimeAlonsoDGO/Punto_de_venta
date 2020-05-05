using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiLibreria
{
    public partial class ErrorTxtBox : TextBox
    {
        public ErrorTxtBox()
        {
            InitializeComponent();
        }

        //CREAMOS LA PROPIEDAD DE CONTROL DE USUARIO

        public Boolean Validar
        {
            set;
            get;
        } //VALIDAR

        public Boolean SoloNumeros
        {
            set;
            get;
        }//SOLONUMEROS
    }


}
