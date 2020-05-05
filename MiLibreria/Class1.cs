using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data; //NAMESPACE PARA UTILIZAR EL DATASET
using System.Data.SqlClient; //NAMESPACE PARA UTILIZAR LA CONEXION A SQL
using System.Windows.Forms; //Importamos para usarlo en la validacion de datos ingresados

namespace MiLibreria
{
    public class Utilidades
    {
        public static DataSet Ejecutar (string cmd)
        {
            //CREACION DE LA CONEXION
            SqlConnection Con = new SqlConnection("Data Source=.;Initial Catalog=Administracion;Integrated Security=True");
            Con.Open();

            DataSet DS = new DataSet(); //CREACION DEL CONTENEDOR DATASET
            SqlDataAdapter DP = new SqlDataAdapter(cmd, Con); //CREACION DEL ADAPTADOR SQL

            DP.Fill(DS); //peticiòn para rellenar el DATASET

            Con.Close(); //cierra la conexion para evitar errores

            return DS; // Devuelve el DataSet ya llenado
        }

        //METODO PARA VALIDAR LOS DATOOS INGRESADOS EN EL FORMULARIO
        public static Boolean ValidarFormulario (Control Objeto, ErrorProvider ErrorProvider)
        {
            Boolean HayErrores = false;

            //Al ser diversos textbox se usara una estructura foreach para validar uno por uno
            foreach(Control Item in Objeto.Controls) //Por cada objeto (parámetro del método)
            {
                if (Item is ErrorTxtBox) //Si el objeto analizado es un ErrorTxtBox
                {
                    ErrorTxtBox Obj = (ErrorTxtBox)Item; //Instanciamos un ErrorTxtBox

                    if(Obj.Validar == true) //Validamos el estatus de la propiedad VALIDAR de nuestro ErrorTxtBox
                    {
                        if(string.IsNullOrEmpty(Obj.Text.Trim())) //Validamos si el ErrorTxtBox está vacio
                        {
                            ErrorProvider.SetError(Obj, "Campo Obligatorio"); //Configuramos el mensaje de nuestro ErrorProvider (Objeto, mensaje)
                            HayErrores = true; //Activamos nuestra variable de control
                        }
                    }

                    if (Obj.SoloNumeros == true) //Validamos si el objeto tiene activada la comprobación para solo números
                    {
                        int cont = 0, letrasEncontradas = 0; //Variables de control

                        foreach(char letra in Obj.Text.Trim()) //Recorrer cada caracter de la cadena ingresada
                        {
                            if(char.IsLetter(Obj.Text.Trim(), cont) == true) //Valida si el caracter actual es una letra
                            {
                                letrasEncontradas++; //Aumenta el contador de letras
                                break;
                            }//If
                            cont++; //Aumentamos contador para avanzar la posición
                        }//ForEach
                        if(letrasEncontradas != 0) //Valida si se encontraron letras
                        {
                            HayErrores = true; //Significa que hubo una letra en la cadena
                            ErrorProvider.SetError(Obj, "Solo ingresar números"); //Error en el ErrorProvider
                        }//IF
                    }//IF
                }//IF
            }//FOREACH

            return HayErrores;

        }//VALIDAR FORMULARIO

    }
}
