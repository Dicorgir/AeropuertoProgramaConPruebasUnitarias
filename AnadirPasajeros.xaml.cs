using MySql.Data.MySqlClient;
using System;
using System.Windows;

namespace Login
{
    /// <summary>
    /// Lógica de interacción para AnadirPasajeros.xaml
    /// </summary>
    public partial class AnadirPasajeros : Window
    {
        public event EventHandler PasajeroAgregado;

        private string connectionString = "Server=127.0.0.1;Port=3307;Database=AeropuertoDos;Uid=root;";
        public AnadirPasajeros()
        {
            InitializeComponent();
        }

        private void bttAceptarPS_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                // Abrir conexión a la base de datos MySQL
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    // Comando SQL para insertar un nuevo tripulante
                    string sqlQuery = "INSERT INTO Pasajeros (Nombre, Apellido1, Apellido2, DNI, Edad, Telefono, Correo, Direccion) " +
                                      "VALUES (@Nombre, @Apellido1, @Apellido2, @DNI, @Edad, @Telefono, @Correo, @Direccion)";

                    // Crear y configurar el comando SQL
                    using (MySqlCommand command = new MySqlCommand(sqlQuery, connection))
                    {
                        // Asignar parámetros con los valores de los campos de la interfaz de usuario
                        command.Parameters.AddWithValue("@Nombre", txtNombrePs.Text);
                        command.Parameters.AddWithValue("@Apellido1", txtPrApellPs.Text);
                        command.Parameters.AddWithValue("@Apellido2", txtSnApellPs.Text);
                        command.Parameters.AddWithValue("@DNI", txtDniPs.Text);
                        command.Parameters.AddWithValue("@Edad", Convert.ToInt32(txtEda.Text));
                        command.Parameters.AddWithValue("@Telefono", txtTelefono.Text);
                        command.Parameters.AddWithValue("@Correo", txtEmailPs.Text);
                        command.Parameters.AddWithValue("@Direccion", txtDireccPs.Text);

                        // Ejecutar la consulta SQL
                        command.ExecuteNonQuery();
                    }

                    MessageBox.Show("Pasajero agregado correctamente.");
                    // Cerrar la ventana
                    Close();

                    // Dispara el evento de tripulante agregado
                    PasajeroAgregado?.Invoke(this, EventArgs.Empty);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar Pasajero: " + ex.Message);
            }
        }

        private void bttLimpiarPs_Click(object sender, RoutedEventArgs e)
        {
            // Limpiar campos
            txtNombrePs.Text = "";
            txtPrApellPs.Text = "";
            txtSnApellPs.Text = "";
            txtDniPs.Text = "";
            txtEda.Text = "";
            txtTelefono.Text = "";
            txtEmailPs.Text = "";
            txtDireccPs.Text = "";
        }
    }
}
