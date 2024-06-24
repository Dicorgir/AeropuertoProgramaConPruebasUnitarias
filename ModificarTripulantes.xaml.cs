using MySql.Data.MySqlClient;
using System;
using System.Windows;

namespace Login
{
    /// <summary>
    /// Lógica de interacción para ModificarTripulantes.xaml
    /// </summary>
    public partial class ModificarTripulantes : Window
    {

        public event EventHandler TripulanteModificado;

        // Define una variable para almacenar el ID del tripulante que se está modificando
        private int idTripulante;

        public ModificarTripulantes(int idTripulante)
        {
            InitializeComponent();

            // Almacena el ID del tripulante que se está modificando
            this.idTripulante = idTripulante;
        }

        private void AceptarMod(object sender, RoutedEventArgs e)
        {
            try
            {
                // Establece la conexión a la base de datos
                string connectionString = "Server=127.0.0.1;Port=3307;Database=AeropuertoDos;Uid=root;";
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    // Define la consulta SQL para actualizar los datos del tripulante
                    string updateQuery = "UPDATE Tripulantes SET Nombre = @nombre, Apellido1 = @apellido1, Apellido2 = @apellido2, DNI = @dni, Edad = @edad, Telefono = @telefono, Correo = @correo, Direccion = @direccion, InformacionAdicional = @infoAdicional WHERE id_Tripulante = @idTripulante";

                    // Crea y configura el comando SQL
                    using (MySqlCommand command = new MySqlCommand(updateQuery, connection))
                    {
                        // Asigna los parámetros de la consulta con los valores de los campos modificados
                        command.Parameters.AddWithValue("@nombre", fieldNombre.Text);
                        command.Parameters.AddWithValue("@apellido1", fieldApe1.Text);
                        command.Parameters.AddWithValue("@apellido2", fieldApe2.Text);
                        command.Parameters.AddWithValue("@dni", fieldDni.Text);
                        command.Parameters.AddWithValue("@edad", fieldEdad.Text);
                        command.Parameters.AddWithValue("@telefono", fieldTelefono.Text);
                        command.Parameters.AddWithValue("@correo", fieldCorreo.Text);
                        command.Parameters.AddWithValue("@direccion", fieldDireccion.Text);
                        command.Parameters.AddWithValue("@infoAdicional", fieldInformacionAdicional.Text);
                        command.Parameters.AddWithValue("@idTripulante", idTripulante);

                        // Ejecuta la consulta SQL
                        int rowsAffected = command.ExecuteNonQuery();

                        // Verifica si se realizó la actualización correctamente
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Datos del tripulante actualizados correctamente.");
                            TripulanteModificado?.Invoke(this, EventArgs.Empty);
                            // Cierra la ventana de modificación
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("No se pudo actualizar los datos del tripulante.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar los datos del tripulante: " + ex.Message);
            }
        }

        private void LimpiarMod(object sender, RoutedEventArgs e)
        {
            // Limpiar los campos de texto
            fieldNombre.Text = "";
            fieldApe1.Text = "";
            fieldApe2.Text = "";
            fieldDni.Text = "";
            fieldEdad.Text = "";
            fieldTelefono.Text = "";
            fieldCorreo.Text = "";
            fieldDireccion.Text = "";
            fieldInformacionAdicional.Text = "";
        }
    }
}
