namespace Nexa___Proyecto
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitializeLoginMenu();
        }

        private void InitializeLoginMenu()
        {
            // Crear controles para el menú de inicio de sesión
            Label lblUsername = new Label() { Text = "Usuario:", Location = new System.Drawing.Point(10, 10) };
            TextBox txtUsername = new TextBox() { Location = new System.Drawing.Point(100, 10) };

            Label lblPassword = new Label() { Text = "Contraseña:", Location = new System.Drawing.Point(10, 40) };
            TextBox txtPassword = new TextBox() { Location = new System.Drawing.Point(100, 40), PasswordChar = '*' };

            Button btnLogin = new Button() { Text = "Login", Location = new System.Drawing.Point(10, 70) };
            btnLogin.Click += (s, e) => ShowMainWindow();

            Button btnRegister = new Button() { Text = "Registrar", Location = new System.Drawing.Point(100, 70) };
            btnRegister.Click += (s, e) => ShowRegisterMenu();

            this.Controls.Add(lblUsername);
            this.Controls.Add(txtUsername);
            this.Controls.Add(lblPassword);
            this.Controls.Add(txtPassword);
            this.Controls.Add(btnLogin);
            this.Controls.Add(btnRegister);
        }

        private void ShowRegisterMenu()
        {
            // Limpiar controles anteriores
            this.Controls.Clear();

            // Crear controles para el menú de registro
            Label lblUsername = new Label() { Text = "Usuario:", Location = new System.Drawing.Point(10, 10) };
            TextBox txtUsername = new TextBox() { Location = new System.Drawing.Point(100, 10) };

            Label lblPassword = new Label() { Text = "Contraseña:", Location = new System.Drawing.Point(10, 40) };
            TextBox txtPassword = new TextBox() { Location = new System.Drawing.Point(100, 40), PasswordChar = '*' };

            Label lblConfirmPassword = new Label() { Text = "Confirmar Contraseña:", Location = new System.Drawing.Point(10, 70) };
            TextBox txtConfirmPassword = new TextBox() { Location = new System.Drawing.Point(150, 70), PasswordChar = '*' };

            Button btnRegister = new Button() { Text = "Registrar", Location = new System.Drawing.Point(10, 100) };
            btnRegister.Click += (s, e) => ShowMainWindow();

            this.Controls.Add(lblUsername);
            this.Controls.Add(txtUsername);
            this.Controls.Add(lblPassword);
            this.Controls.Add(txtPassword);
            this.Controls.Add(lblConfirmPassword);
            this.Controls.Add(txtConfirmPassword);
            this.Controls.Add(btnRegister);
        }

        private void ShowMainWindow()
        {
            // Limpiar controles anteriores
            this.Controls.Clear();

            // Panel de búsqueda
            TextBox txtSearch = new TextBox() { Location = new System.Drawing.Point(10, 10), Width = 200 };
            Button btnSearch = new Button() { Text = "Buscar", Location = new System.Drawing.Point(220, 10) };
            btnSearch.Click += (s, e) => MessageBox.Show("Buscar: " + txtSearch.Text);

            // Panel de amigos, solicitudes y grupos
            Button btnFriends = new Button() { Text = "Amigos", Location = new System.Drawing.Point(10, 50), Width = 100 };
            Button btnRequests = new Button() { Text = "Solicitudes", Location = new System.Drawing.Point(10, 80), Width = 100 };
            Button btnGroups = new Button() { Text = "Grupos", Location = new System.Drawing.Point(10, 110), Width = 100 };
            Button btnCommunity = new Button() { Text = "Comunidades", Location = new System.Drawing.Point(10, 140), Width = 100 };

            btnFriends.Click += (s, e) => MessageBox.Show("Amigos");
            btnRequests.Click += (s, e) => MessageBox.Show("Solicitudes");
            btnGroups.Click += (s, e) => MessageBox.Show("Grupos");
            btnCommunity.Click += (s, e) => ShowCommunityWindow();

            // Panel de mensajes
            Label lblMessages = new Label() { Text = "Mensajes", Location = new System.Drawing.Point(10, 180) };
            ListBox lstMessages = new ListBox() { Location = new System.Drawing.Point(10, 210), Width = 100, Height = 200 };

            // Panel de usuario
            Label lblUserStatus = new Label() { Text = "Usuario/Estado", Location = new System.Drawing.Point(10, 420) };
            ComboBox cmbStatus = new ComboBox() { Location = new System.Drawing.Point(10, 450), Width = 100 };
            cmbStatus.Items.AddRange(new string[] { "Online", "Offline", "Busy", "Away" });

            // Panel de contenido principal
            Label lblContentFilter = new Label() { Text = "Nuevo / Popular / Más votado", Location = new System.Drawing.Point(150, 50) };
            ListBox lstContent = new ListBox() { Location = new System.Drawing.Point(150, 80), Width = 400, Height = 300 };

            // Panel de noticias
            Label lblNews = new Label() { Text = "Noticias", Location = new System.Drawing.Point(600, 50) };
            ListBox lstNews = new ListBox() { Location = new System.Drawing.Point(600, 80), Width = 200, Height = 300 };

            // Añadir todos los controles al formulario
            this.Controls.Add(txtSearch);
            this.Controls.Add(btnSearch);
            this.Controls.Add(btnFriends);
            this.Controls.Add(btnRequests);
            this.Controls.Add(btnGroups);
            this.Controls.Add(btnCommunity);
            this.Controls.Add(lblMessages);
            this.Controls.Add(lstMessages);
            this.Controls.Add(lblUserStatus);
            this.Controls.Add(cmbStatus);
            this.Controls.Add(lblContentFilter);
            this.Controls.Add(lstContent);
            this.Controls.Add(lblNews);
            this.Controls.Add(lstNews);
        }

        private void ShowCommunityWindow()
        {
            this.Controls.Clear();

            // Panel de búsqueda
            TextBox txtSearch = new TextBox() { Location = new System.Drawing.Point(10, 10), Width = 200 };
            Button btnSearch = new Button() { Text = "Buscar", Location = new System.Drawing.Point(220, 10) };
            btnSearch.Click += (s, e) => MessageBox.Show("Buscar: " + txtSearch.Text);

            // Panel de links, reglas y categorías
            Label lblLinks = new Label() { Text = "Links", Location = new System.Drawing.Point(10, 50) };
            Button btnRules = new Button() { Text = "Reglas/Actualizaciones", Location = new System.Drawing.Point(10, 80), Width = 150 };
            Button btnCategory1 = new Button() { Text = "Categoría 1", Location = new System.Drawing.Point(10, 110), Width = 150 };
            Button btnCategory2 = new Button() { Text = "Categoría 2", Location = new System.Drawing.Point(10, 140), Width = 150 };
            Button btnCategory3 = new Button() { Text = "Categoría 3", Location = new System.Drawing.Point(10, 170), Width = 150 };

            btnRules.Click += (s, e) => MessageBox.Show("Reglas/Actualizaciones");
            btnCategory1.Click += (s, e) => MessageBox.Show("Categoría 1");
            btnCategory2.Click += (s, e) => MessageBox.Show("Categoría 2");
            btnCategory3.Click += (s, e) => MessageBox.Show("Categoría 3");

            // Panel de posts
            Label lblPosts = new Label() { Text = "Posts", Location = new System.Drawing.Point(200, 50) };
            ListBox lstPosts = new ListBox() { Location = new System.Drawing.Point(200, 80), Width = 400, Height = 300 };

            // Panel de usuarios en línea y desconectados
            Label lblOnlineUsers = new Label() { Text = "Usuarios en línea", Location = new System.Drawing.Point(650, 50) };
            ListBox lstOnlineUsers = new ListBox() { Location = new System.Drawing.Point(650, 80), Width = 200, Height = 100 };
            Label lblOfflineUsers = new Label() { Text = "Usuarios desconectados", Location = new System.Drawing.Point(650, 200) };
            ListBox lstOfflineUsers = new ListBox() { Location = new System.Drawing.Point(650, 230), Width = 200, Height = 100 };

            // Panel de comentarios
            TextBox txtComment = new TextBox() { Location = new System.Drawing.Point(200, 400), Width = 400 };
            Button btnComment = new Button() { Text = "Comentar", Location = new System.Drawing.Point(620, 400) };
            btnComment.Click += (s, e) => MessageBox.Show("Comentario: " + txtComment.Text);

            // Añadir todos los controles al formulario
            this.Controls.Add(txtSearch);
            this.Controls.Add(btnSearch);
            this.Controls.Add(lblLinks);
            this.Controls.Add(btnRules);
            this.Controls.Add(btnCategory1);
            this.Controls.Add(btnCategory2);
            this.Controls.Add(btnCategory3);
            this.Controls.Add(lblPosts);
            this.Controls.Add(lstPosts);
            this.Controls.Add(lblOnlineUsers);
            this.Controls.Add(lstOnlineUsers);
            this.Controls.Add(lblOfflineUsers);
            this.Controls.Add(lstOfflineUsers);
            this.Controls.Add(txtComment);
            this.Controls.Add(btnComment);
        }
    }
}