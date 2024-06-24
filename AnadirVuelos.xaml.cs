using MySql.Data.MySqlClient;
using System;
using System.Windows;

namespace Login
{
    /// <summary>
    /// Lógica de interacción para AnadirVuelos.xaml
    /// </summary>
    public partial class AnadirVuelos : Window
    {

        public EventHandler VueloAgregado;
        public AnadirVuelos()
        {
            InitializeComponent();
            CargarIdAviones(); // Cargar los ID de los aviones en el ComboBox al inicializar la ventana
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            // Obtener los valores de los campos de la ventana
            int idAvion = Convert.ToInt32(ComboIdAvion.SelectedValue);
            string origen = txtOrigen.Text;
            string destino = txtDestino.Text;
            string horaSalida = txtSalida.Text;
            string horaLlegada = txtLlegada.Text;
            DateTime fecha = DatePickerFecha.SelectedDate ?? DateTime.Now; // Obtener la fecha del DatePicker

            // Insertar los datos en la base de datos
            InsertarVueloEnBD(idAvion, origen, destino, fecha, horaSalida, horaLlegada);
        }
        private void InsertarVueloEnBD(int idAvion, string origen, string destino, DateTime fecha, string horaSalida, string horaLlegada)
        {
            try
            {
                string connectionString = "Server=127.0.0.1;Port=3307;Database=AeropuertoDos;Uid=root;";
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO Vuelos (idAvion, Origen, Destino, Fecha, HoraSalida, HoraLlegada) " +
                                   "VALUES (@idAvion, @origen, @destino, @fecha, @horaSalida, @horaLlegada)";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@idAvion", idAvion);
                    command.Parameters.AddWithValue("@origen", origen);
                    command.Parameters.AddWithValue("@destino", destino);
                    command.Parameters.AddWithValue("@fecha", fecha.ToString("yyyy-MM-dd")); // Asegúrate de formatear la fecha correctamente
                    command.Parameters.AddWithValue("@horaSalida", horaSalida);
                    command.Parameters.AddWithValue("@horaLlegada", horaLlegada);
                    command.ExecuteNonQuery();
                }
                MessageBox.Show("Vuelo insertado correctamente.");
                // Cerrar la ventana
                this.Close();

                VueloAgregado?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al insertar vuelo en la base de datos: " + ex.Message);
            }
        }


        private void CargarIdAviones()
        {
            try
            {
                string connectionString = "Server=127.0.0.1;Port=3307;Database=AeropuertoDos;Uid=root;";
                string query = "SELECT idAvion FROM Aviones";

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand(query, connection);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ComboIdAvion.Items.Add(reader["idAvion"]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los ID de aviones: " + ex.Message);
            }
        }

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCampos();
        }

        private void LimpiarCampos()
        {
            txtOrigen.Text = string.Empty;
            txtDestino.Text = string.Empty;
            txtSalida.Text = string.Empty;
            txtLlegada.Text = string.Empty;
            ComboIdAvion.SelectedIndex = -1;
        }
    }
}
