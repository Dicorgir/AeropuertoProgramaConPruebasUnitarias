using MySql.Data.MySqlClient;
using System;
using System.Windows;

namespace Login
{
    public partial class ModificarVuelos : Window
    {
        private string connectionString = "Server=127.0.0.1;Port=3307;Database=AeropuertoDos;Uid=root;";
        private int vueloId; // Necesitas obtener este valor antes de abrir la ventana

        public bool VueloModificado { get; private set; }

        public ModificarVuelos(int idVuelo)
        {
            InitializeComponent();
            vueloId = idVuelo;
            CargarDatosVuelo(); // Llamar a la función para cargar los datos
        }

        private void CargarDatosVuelo()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = $"SELECT idAvion, Origen, Destino, HoraSalida, HoraLlegada FROM Vuelos WHERE idVuelo = {vueloId}";

                    MySqlCommand command = new MySqlCommand(query, connection);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Cargar los datos en los TextBox
                            txtIdAvion.Text = reader["idAvion"].ToString();
                            txtOrigen.Text = reader["Origen"].ToString();
                            txtDestino.Text = reader["Destino"].ToString();
                            txtSalida.Text = reader["HoraSalida"].ToString();
                            txtLlegada.Text = reader["HoraLlegada"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los datos: {ex.Message}");
            }
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string idAvion = txtIdAvion.Text;
                    string origen = txtOrigen.Text;
                    string destino = txtDestino.Text;
                    string salida = txtSalida.Text;
                    string llegada = txtLlegada.Text;

                    string query = $"UPDATE Vuelos SET idAvion = '{idAvion}', Origen = '{origen}', Destino = '{destino}', HoraSalida = '{salida}', HoraLlegada = '{llegada}' WHERE idVuelo = {vueloId}";

                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.ExecuteNonQuery();

                    MessageBox.Show("Los datos se han actualizado correctamente.");


                    // Indicar que se agregó un vuelo
                    VueloModificado = true;

                    // Cerrar la ventana con DialogResult
                    this.DialogResult = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar los datos: {ex.Message}");
            }
        }

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            // Limpiar los TextBox
            txtIdAvion.Clear();
            txtOrigen.Clear();
            txtDestino.Clear();
            txtSalida.Clear();
            txtLlegada.Clear();
        }
    }
}