using MySql.Data.MySqlClient;
using System;
using System.Windows;

namespace Login
{
    public partial class MainWindow : Window
    {
        private string connectionString = "Server=127.0.0.1;Port=3307;Database=AeropuertoDos;Uid=root;";
        public MainWindow()
        {
            InitializeComponent();
        }

        private void botonLogin_Click(object sender, RoutedEventArgs e)
        {
            string usuario = cuadroUsuario.Text;
            string contrasenia = cuadroContra.Password;

            // Realizar la consulta para verificar las credenciales en la base de datos
            string query = $"SELECT * FROM Login WHERE Usuario = '{usuario}' AND Contrasenia = '{contrasenia}'";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    MySqlCommand command = new MySqlCommand(query, connection);
                    MySqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        MessageBox.Show("Usuario correcto");
                        VentanaTablas ventanaTablas = new VentanaTablas();
                        ventanaTablas.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Usuario o contraseña incorrectos");

                        cuadroUsuario.Text = "";
                        cuadroContra.Password = "";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al iniciar sesión: {ex.Message}");
            }
        }


        private void botonRegistrarse_Click(object sender, RoutedEventArgs e)
        {
            VentanaAlta ventanaAlta = new VentanaAlta();

            ventanaAlta.Show();

        }

    }
}
