using MySql.Data.MySqlClient;
using System;
using System.Windows;

namespace Login
{
    public partial class VentanaAlta : Window
    {
        // Cadena de conexión a la base de datos MySQL
        private string connectionString = "Server=127.0.0.1;Port=3307;Database=AeropuertoDos;Uid=root;";

        public VentanaAlta()
        {
            InitializeComponent();
        }


        private void botonGuardarDatos_Click(object sender, RoutedEventArgs e)
        {
            // Obtener la información de los TextBox
            string usuario = cuadroPonerUsuario.Text;
            string contrasenia = cuadroPonerContra.Password;
            string nombre = cuadroPonerNombre.Text;
            string apellidos = cuadroPonerApellidos.Text;
            string correo = cuadroPonerCorreo.Text;

            // Crear la consulta SQL para insertar los datos en la tabla Login
            string query = "INSERT INTO Login (Usuario, Contrasenia, Nombre, Apellidos, Correo) " +
                           $"VALUES ('{usuario}', '{contrasenia}', '{nombre}', '{apellidos}', '{correo}')";

            try
            {
                // Establecer la conexión con la base de datos
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    // Abrir la conexión
                    connection.Open();

                    // Ejecutar la consulta SQL
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.ExecuteNonQuery();

                    // Mostrar un mensaje de éxito
                    MessageBox.Show("Los datos se han guardado correctamente.");

                    // Limpiar los TextBox
                    cuadroPonerUsuario.Clear();
                    cuadroPonerContra.Clear();
                    cuadroPonerNombre.Clear();
                    cuadroPonerApellidos.Clear();
                    cuadroPonerCorreo.Clear();

                    // Cerrar la ventana
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                // Mostrar un mensaje de error si ocurre alguna excepción
                MessageBox.Show($"Error al guardar los datos: {ex.Message}");
            }
        }
    }
}