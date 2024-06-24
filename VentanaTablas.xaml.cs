using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Data;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace Login
{
    /// <summary>
    /// Lógica de interacción para VentanaTablas.xaml
    /// </summary>
    public partial class VentanaTablas : Window
    {
        private AnadirTripulantes anadirTripulantes;
        private AnadirPasajeros anadirPasajeros;
        private ModificarPasajeros modificarPasajeros;

        public VentanaTablas()
        {
            InitializeComponent();
            InsertarDatosAvionesDesdeJSON();
            MostrarDatosAvionesEnTabla();
            MostrarDatosTripulantesEnTabla(); // Mostrar los datos de los tripulantes al cargar la ventana
            MostrarDatosPasajerosEnTabla();
            MostrarDatosVuelosEnTabla();

        }
        private void InsertarDatosAvionesDesdeJSON()
        {
            try
            {
                string jsonFilePath = "C:\\Users\\dicor\\OneDrive\\Escritorio\\CURSOS DE OPENWEBINAR\\Curso de C#\\Login\\Aviones.json"; // Especifica la ruta de tu archivo JSON
                string jsonText = File.ReadAllText(jsonFilePath);

                dynamic data = JsonConvert.DeserializeObject(jsonText);

                foreach (var avion in data.aviones.avion)
                {
                    int idAvion = avion.idAvion;
                    string matricula = avion.matricula;
                    string modelo = avion.modelo;
                    int asientos = avion.asientos;
                    int estado = avion.estado;

                    // Insertar datos en la base de datos
                    InsertarOActualizarAvionEnBD(idAvion, matricula, modelo, asientos, estado);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al insertar datos de aviones desde el JSON: " + ex.Message);
            }
        }
        private void InsertarOActualizarAvionEnBD(int idAvion, string matricula, string modelo, int asientos, int estado)
        {
            try
            {
                string connectionString = "Server=127.0.0.1;Port=3307;Database=AeropuertoDos;Uid=root;";
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    // Verifica si el avión ya existe en la base de datos
                    string checkQuery = "SELECT COUNT(*) FROM Aviones WHERE idAvion = @idAvion";
                    MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection);
                    checkCommand.Parameters.AddWithValue("@idAvion", idAvion);
                    int count = Convert.ToInt32(checkCommand.ExecuteScalar());

                    if (count > 0)
                    {
                        // El avión ya existe, realiza una actualización
                        string updateQuery = "UPDATE Aviones SET matricula = @matricula, modelo = @modelo, asientos = @asientos, estado = @estado WHERE idAvion = @idAvion";
                        MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection);
                        updateCommand.Parameters.AddWithValue("@matricula", matricula);
                        updateCommand.Parameters.AddWithValue("@modelo", modelo);
                        updateCommand.Parameters.AddWithValue("@asientos", asientos);
                        updateCommand.Parameters.AddWithValue("@estado", estado);
                        updateCommand.Parameters.AddWithValue("@idAvion", idAvion);
                        updateCommand.ExecuteNonQuery();
                    }
                    else
                    {
                        // El avión no existe, realiza una inserción
                        string insertQuery = "INSERT INTO Aviones (idAvion, matricula, modelo, asientos, estado) VALUES (@idAvion, @matricula, @modelo, @asientos, @estado)";
                        MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection);
                        insertCommand.Parameters.AddWithValue("@idAvion", idAvion);
                        insertCommand.Parameters.AddWithValue("@matricula", matricula);
                        insertCommand.Parameters.AddWithValue("@modelo", modelo);
                        insertCommand.Parameters.AddWithValue("@asientos", asientos);
                        insertCommand.Parameters.AddWithValue("@estado", estado);
                        insertCommand.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al insertar o actualizar avión en la base de datos: " + ex.Message);
            }
        }


        private void MostrarDatosAvionesEnTabla()
        {
            try
            {
                string connectionString = "Server=127.0.0.1;Port=3307;Database=AeropuertoDos;Uid=root;";
                string query = "SELECT idAvion, matricula, modelo, asientos, estado FROM Aviones";

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand(query, connection);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                    System.Data.DataTable dataTable = new System.Data.DataTable();
                    adapter.Fill(dataTable);

                    tablaAviones.ItemsSource = dataTable.DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al mostrar datos de aviones en la tabla: " + ex.Message);
            }
        }
        private void MostrarDatosTripulantesEnTabla()
        {
            try
            {
                string connectionString = "Server=127.0.0.1;Port=3307;Database=AeropuertoDos;Uid=root;";
                string query = "SELECT id_Tripulante, Categoria, Nombre, Apellido1, Apellido2, DNI, Edad, Telefono, Correo, Direccion, InformacionAdicional FROM Tripulantes";

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand(query, connection);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    tablaTripulacion.ItemsSource = dataTable.DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al mostrar datos de tripulantes en la tabla: " + ex.Message);
            }
        }
        private void MostrarDatosVuelosEnTabla()
        {
            try
            {
                string connectionString = "Server=127.0.0.1;Port=3307;Database=AeropuertoDos;Uid=root;";
                string query = "SELECT idVuelo, idAvion, Origen, Destino, Fecha, HoraSalida, HoraLlegada FROM Vuelos";

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand(query, connection);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    // Asignar la tabla como origen de datos del DataGrid
                    tablaVuelos.ItemsSource = dataTable.DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al mostrar datos de vuelos en la tabla: " + ex.Message);
            }
        }




        private void MostrarDatosPasajerosEnTabla()
        {
            try
            {
                string connectionString = "Server=127.0.0.1;Port=3307;Database=AeropuertoDos;Uid=root;";
                string query = "SELECT idPasajero, Nombre, Apellido1, Apellido2, DNI, Edad, Telefono, Correo, Direccion FROM Pasajeros";

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand(query, connection);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    tablaPasajeros.ItemsSource = dataTable.DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al mostrar datos de pasajeros en la tabla: " + ex.Message);
            }
        }



        private void btnAnadirTripulacion_Click(object sender, RoutedEventArgs e)
        {
            // Crear instancia de AnadirTripulantes y suscribirse al evento TripulanteAgregado
            anadirTripulantes = new AnadirTripulantes();
            anadirTripulantes.TripulanteAgregado += AnadirTripulantes_TripulanteAgregado;
            anadirTripulantes.Show();
        }
        private void AnadirTripulantes_TripulanteAgregado(object sender, EventArgs e)
        {
            // Actualizar la tabla de tripulantes
            MostrarDatosTripulantesEnTabla();
        }
        private void btnModificarTripulacion_Click(object sender, RoutedEventArgs e)
        {
            DataRowView selectedRow = (DataRowView)tablaTripulacion.SelectedItem;

            if (selectedRow != null)
            {
                int idTripulante = Convert.ToInt32(selectedRow["id_Tripulante"]);
                ModificarTripulantes modificarTripulantes = new ModificarTripulantes(idTripulante);

                modificarTripulantes.TripulanteModificado += ModificarTripulantes_TripulanteModificado;

                modificarTripulantes.fieldNombre.Text = selectedRow["Nombre"].ToString();
                modificarTripulantes.fieldApe1.Text = selectedRow["Apellido1"].ToString();
                modificarTripulantes.fieldApe2.Text = selectedRow["Apellido2"].ToString();
                modificarTripulantes.fieldDni.Text = selectedRow["DNI"].ToString();
                modificarTripulantes.fieldEdad.Text = selectedRow["Edad"].ToString();
                modificarTripulantes.fieldTelefono.Text = selectedRow["Telefono"].ToString();
                modificarTripulantes.fieldCorreo.Text = selectedRow["Correo"].ToString();
                modificarTripulantes.fieldDireccion.Text = selectedRow["Direccion"].ToString();
                modificarTripulantes.fieldInformacionAdicional.Text = selectedRow["InformacionAdicional"].ToString();

                modificarTripulantes.Show();
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un tripulante para modificar.");
            }
        }

        // Manejador de eventos para el evento TripulanteModificado
        private void ModificarTripulantes_TripulanteModificado(object sender, EventArgs e)
        {
            // Vuelve a cargar los datos de los tripulantes en la tabla
            MostrarDatosTripulantesEnTabla();
        }

        private void btnBorrarTripulacion_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Obtener el ID del tripulante seleccionado en la tabla
                DataRowView selectedRow = (DataRowView)tablaTripulacion.SelectedItem;
                int idTripulante = Convert.ToInt32(selectedRow["id_Tripulante"]);

                // Conexión a la base de datos
                string connectionString = "Server=127.0.0.1;Port=3307;Database=AeropuertoDos;Uid=root;";
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    // Comando SQL para eliminar el tripulante
                    string deleteQuery = "DELETE FROM Tripulantes WHERE id_Tripulante = @idTripulante";

                    // Configurar y ejecutar el comando SQL
                    using (MySqlCommand command = new MySqlCommand(deleteQuery, connection))
                    {
                        command.Parameters.AddWithValue("@idTripulante", idTripulante);
                        command.ExecuteNonQuery();
                    }
                }

                // Mostrar mensaje de éxito
                MessageBox.Show("Tripulante eliminado correctamente.");

                // Actualizar la tabla de tripulantes
                MostrarDatosTripulantesEnTabla();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar tripulante: " + ex.Message);
            }
        }

        private void btnModificarVuelos_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Obtener el ID del vuelo seleccionado en la tabla
                DataRowView selectedRow = (DataRowView)tablaVuelos.SelectedItem;
                int idVuelo = Convert.ToInt32(selectedRow["idVuelo"]);

                // Crear instancia de ModificarVuelos y pasar el ID del vuelo seleccionado
                ModificarVuelos modificarVuelos = new ModificarVuelos(idVuelo);
                var result = modificarVuelos.ShowDialog();

                // Verificar si se agregó un vuelo
                if (modificarVuelos.VueloModificado)
                {
                    // Actualizar la tabla de vuelos
                    MostrarDatosVuelosEnTabla();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al abrir la ventana de modificación de vuelos: " + ex.Message);
            }
        }

        private void btnModificarPasajeros_Click(object sender, RoutedEventArgs e)
        {
            DataRowView selectedRow = (DataRowView)tablaPasajeros.SelectedItem;

            if (selectedRow != null)
            {
                string dni = selectedRow["DNI"].ToString();
                string nombre = selectedRow["Nombre"].ToString();
                string apellido1 = selectedRow["Apellido1"].ToString();
                string apellido2 = selectedRow["Apellido2"].ToString();
                int edad = Convert.ToInt32(selectedRow["Edad"]);
                string correo = selectedRow["Correo"].ToString();
                string direccion = selectedRow["Direccion"].ToString();
                string telefono = selectedRow["Telefono"].ToString();

                modificarPasajeros = new ModificarPasajeros(dni, nombre, apellido1, apellido2, edad, correo, direccion, telefono);
                modificarPasajeros.PasajeroActualizado += ModificarPasajeros_PasajeroActualizado;
                modificarPasajeros.Show();
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un pasajero para modificar.");
            }
        }

        // Controlador de eventos para el evento PasajeroActualizado de la ventana ModificarPasajeros
        private void ModificarPasajeros_PasajeroActualizado(object sender, EventArgs e)
        {
            // Actualizar la tabla de pasajeros después de que se haya modificado un pasajero
            MostrarDatosPasajerosEnTabla();
        }

        private void btnBorrarVuelos_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Verificar si se ha seleccionado un vuelo en la tabla
                DataRowView selectedRow = (DataRowView)tablaVuelos.SelectedItem;
                if (selectedRow != null)
                {
                    int idVuelo = Convert.ToInt32(selectedRow["idVuelo"]);

                    // Establecer la conexión con la base de datos
                    string connectionString = "Server=127.0.0.1;Port=3307;Database=AeropuertoDos;Uid=root;";
                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        connection.Open();

                        // Comando SQL para eliminar el vuelo
                        string deleteQuery = "DELETE FROM Vuelos WHERE idVuelo = @idVuelo";

                        // Configurar y ejecutar el comando SQL
                        using (MySqlCommand command = new MySqlCommand(deleteQuery, connection))
                        {
                            command.Parameters.AddWithValue("@idVuelo", idVuelo);
                            command.ExecuteNonQuery();
                        }
                    }

                    // Mostrar mensaje de éxito
                    MessageBox.Show("Vuelo eliminado correctamente.");

                    // Actualizar la tabla de vuelos
                    MostrarDatosVuelosEnTabla();
                }
                else
                {
                    MessageBox.Show("Por favor, seleccione un vuelo para eliminar.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar vuelo: " + ex.Message);
            }
        }


        private void btnBorrarPasajeros_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Obtener el ID del pasajero seleccionado en la tabla
                DataRowView selectedRow = (DataRowView)tablaPasajeros.SelectedItem;
                int idPasajero = Convert.ToInt32(selectedRow["idPasajero"]);

                // Conexión a la base de datos
                string connectionString = "Server=127.0.0.1;Port=3307;Database=AeropuertoDos;Uid=root;";
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    // Comando SQL para eliminar el pasajero
                    string deleteQuery = "DELETE FROM Pasajeros WHERE idPasajero = @idPasajero";

                    // Configurar y ejecutar el comando SQL
                    using (MySqlCommand command = new MySqlCommand(deleteQuery, connection))
                    {
                        command.Parameters.AddWithValue("@idPasajero", idPasajero);
                        command.ExecuteNonQuery();
                    }
                }

                // Mostrar mensaje de éxito
                MessageBox.Show("Pasajero eliminado correctamente.");

                // Actualizar la tabla de pasajeros
                MostrarDatosPasajerosEnTabla();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar pasajero: " + ex.Message);
            }
        }


        private void btnAnadirVuelos_Click(object sender, RoutedEventArgs e)
        {
            AnadirVuelos anadirVuelos = new AnadirVuelos();
            anadirVuelos.VueloAgregado += AnadirVuelos_VueloAgregado;
            anadirVuelos.Show();
        }
        private void AnadirVuelos_VueloAgregado(object sender, EventArgs e)
        {
            // Actualizar la tabla de vuelos
            MostrarDatosVuelosEnTabla();
        }


        private void btnAnadirPasajeros_Click(object sender, RoutedEventArgs e)
        {
            AnadirPasajeros anadirPasajeros = new AnadirPasajeros();
            anadirPasajeros.PasajeroAgregado += AnadirPasajeros_PasajeroAgregado;
            anadirPasajeros.Show();
        }
        private void AnadirPasajeros_PasajeroAgregado(object sender, EventArgs e)
        {
            // Actualizar la tabla de pasajeros
            MostrarDatosPasajerosEnTabla();
        }

        private void tablaTripulacion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void tablaAviones_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
