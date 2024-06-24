using MySql.Data.MySqlClient;
using System;
using System.Windows;

namespace Login
{
    /// <summary>
    /// Lógica de interacción para ModificarPasajeros.xaml
    /// </summary>
    public partial class ModificarPasajeros : Window
    {
        public event EventHandler PasajeroActualizado;
        // Método para invocar el evento cuando se actualiza un pasajero
        public ModificarPasajeros(string dni, string nombre, string apellido1, string apellido2, int edad, string correo, string direccion, string telefono)
        {
            InitializeComponent();

            // Establecer los valores de los campos de texto con los datos del pasajero
            txtDniPs.Text = dni;
            txtNombrePs.Text = nombre;
            txtPrApellPs.Text = apellido1;
            txtSnApellPs.Text = apellido2;
            txtEda.Text = edad.ToString();
            txtEmailPs.Text = correo;
            txtDireccPs.Text = direccion;
            txtTelefono.Text = telefono;
        }

        private void bttAceptarPS_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Obtener los nuevos valores de los campos de texto
                string nuevoDni = txtDniPs.Text;
                string nuevoNombre = txtNombrePs.Text;
                string nuevoApellido1 = txtPrApellPs.Text;
                string nuevoApellido2 = txtSnApellPs.Text;
                int nuevaEdad = Convert.ToInt32(txtEda.Text);
                string nuevoCorreo = txtEmailPs.Text;
                string nuevaDireccion = txtDireccPs.Text;
                string nuevoTelefono = txtTelefono.Text;

                // Conexión a la base de datos
                string connectionString = "Server=127.0.0.1;Port=3307;Database=AeropuertoDos;Uid=root;";
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    // Comando SQL para actualizar los datos del pasajero
                    string updateQuery = "UPDATE Pasajeros SET Nombre = @Nombre, Apellido1 = @Apellido1, Apellido2 = @Apellido2, " +
                                         "DNI = @DNI, Edad = @Edad, Correo = @Correo, Direccion = @Direccion, Telefono = @Telefono " +
                                         "WHERE DNI = @OldDNI";

                    // Configurar y ejecutar el comando SQL
                    using (MySqlCommand command = new MySqlCommand(updateQuery, connection))
                    {
                        command.Parameters.AddWithValue("@Nombre", nuevoNombre);
                        command.Parameters.AddWithValue("@Apellido1", nuevoApellido1);
                        command.Parameters.AddWithValue("@Apellido2", nuevoApellido2);
                        command.Parameters.AddWithValue("@DNI", nuevoDni);
                        command.Parameters.AddWithValue("@Edad", nuevaEdad);
                        command.Parameters.AddWithValue("@Correo", nuevoCorreo);
                        command.Parameters.AddWithValue("@Direccion", nuevaDireccion);
                        command.Parameters.AddWithValue("@Telefono", nuevoTelefono);
                        command.Parameters.AddWithValue("@OldDNI", nuevoDni); // Suponiendo que "dniOriginal" es una variable que almacena el DNI original del pasajero
                        command.ExecuteNonQuery();
                    }
                }
                PasajeroActualizado?.Invoke(this, e);

                MessageBox.Show("Pasajero actualizado correctamente.");

                // Cerrar la ventana después de la actualización
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar pasajero: " + ex.Message);
            }
        }
    }
}
