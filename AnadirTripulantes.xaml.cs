using MySql.Data.MySqlClient;
using System;
using System.Windows;

namespace Login
{
    public partial class AnadirTripulantes : Window
    {
        public event EventHandler TripulanteAgregado;

        // Cadena de conexión a la base de datos MySQL
        private string connectionString = "Server=127.0.0.1;Port=3307;Database=AeropuertoDos;Uid=root;";

        public AnadirTripulantes()
        {
            InitializeComponent();
        }

        private void agregarTripulante(object sender, RoutedEventArgs e)
        {
            try
            {
                // Abrir conexión a la base de datos MySQL
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    // Comando SQL para insertar un nuevo tripulante
                    string sqlQuery = "INSERT INTO Tripulantes (Categoria, Nombre, Apellido1, Apellido2, DNI, Edad, Telefono, Correo, Direccion, InformacionAdicional) " +
                                      "VALUES (@Categoria, @Nombre, @Apellido1, @Apellido2, @DNI, @Edad, @Telefono, @Correo, @Direccion, @InformacionAdicional)";

                    // Crear y configurar el comando SQL
                    using (MySqlCommand command = new MySqlCommand(sqlQuery, connection))
                    {
                        // Asignar parámetros con los valores de los campos de la interfaz de usuario
                        command.Parameters.AddWithValue("@Categoria", (RadioButtonPiloto.IsChecked == true) ? "Piloto" : (RadioButtonCopiloto.IsChecked == true) ? "Copiloto" : "Azafato");
                        command.Parameters.AddWithValue("@Nombre", fieldNombre.Text);
                        command.Parameters.AddWithValue("@Apellido1", fieldApellido1.Text);
                        command.Parameters.AddWithValue("@Apellido2", fieldApellido2.Text);
                        command.Parameters.AddWithValue("@DNI", fieldDni.Text);
                        command.Parameters.AddWithValue("@Edad", Convert.ToInt32(fieldEdad.Text));
                        command.Parameters.AddWithValue("@Telefono", fieldTlf.Text);
                        command.Parameters.AddWithValue("@Correo", fieldCorreo.Text);
                        command.Parameters.AddWithValue("@Direccion", fieldDireccion.Text);
                        command.Parameters.AddWithValue("@InformacionAdicional", fieldInformacionAdicional.Text);

                        // Ejecutar la consulta SQL
                        command.ExecuteNonQuery();
                    }

                    MessageBox.Show("Tripulante agregado correctamente.");
                    // Cerrar la ventana
                    Close();

                    // Dispara el evento de tripulante agregado
                    TripulanteAgregado?.Invoke(this, EventArgs.Empty);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar tripulante: " + ex.Message);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Limpiar los campos de la interfaz de usuario
            fieldNombre.Text = "";
            fieldApellido1.Text = "";
            fieldApellido2.Text = "";
            fieldDni.Text = "";
            fieldEdad.Text = "";
            fieldTlf.Text = "";
            fieldCorreo.Text = "";
            fieldDireccion.Text = "";
            fieldInformacionAdicional.Text = "";
        }
    }
}
