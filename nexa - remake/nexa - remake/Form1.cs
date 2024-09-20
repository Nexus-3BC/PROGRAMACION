namespace nexa___remake
{

    using System;
    using System.Data;
    using System.Windows.Forms;
    using MySql.Data.MySqlClient;

    public partial class Form1 : Form
    {
        private string connectionString = "Server=localhost;Database=Nexa;Uid=root;Pwd=;";

        private Button returnButton;
        private Button friendsButton;
        private Button requestsButton;
        private Button communitiesButton;
        private Button callButton;
        private Button videoCallButton;
        private Button pinnedMessagesButton;
        private ListBox infoListBox;
        private TextBox searchTextBox;
        private Panel leftPanel;
        private Panel rightPanel;
        private Panel centerPanel;
        private Panel messagesPanel;
        private PictureBox pictureBox2;
        private ListBox communityPostsListBox; 
        private TextBox postTextBox; 
        private Button postButton; 
        private Button uploadImageButton;
        private OpenFileDialog openFileDialog;
        private string postImagePath;
        private PictureBox postImagePictureBox;
        public Form1()
        {
            InitializeComponent();
            InitializeReturnButton();
            InitializePictureBox2();
            InitializeOpenFileDialog();
            CargarUsuarios();


            postImagePictureBox = new PictureBox
            {
                Size = new Size(100, 100),
                Location = new Point(0, 0),
                SizeMode = PictureBoxSizeMode.StretchImage,
                Visible = false
            };
        }


        public class Usuario
        {
            public int ID_Usuario { get; set; }
            public string Nombre { get; set; }
            public string Correo { get; set; }
            public string Password { get; set; }
        }

        public class ConfiguracionUsuario
        {
            public int ID_Usuario { get; set; }
            public string Configuracion { get; set; }
        }

        public class Comunidad
        {
            public int ID_Comunidad { get; set; }
            public string Nombre_Comunidad { get; set; }
        }

        public class Publicacion
        {
            public int ID_Publicacion { get; set; }
            public string Contenido { get; set; }
            public DateTime Fecha { get; set; }
            public int ID_Comunidad { get; set; }
        }

        public class MensajePrivado
        {
            public int ID_Mensaje { get; set; }
            public int ID_Usuario_Emisor { get; set; }
            public int ID_Usuario_Receptor { get; set; }
            public string Contenido { get; set; }
            public DateTime Fecha { get; set; }
        }
        public class Evento
        {
            public int ID_Evento { get; set; }
            public string Nombre_Evento { get; set; }
            public DateTime Fecha_Evento { get; set; }
            public string Descripcion { get; set; }
            public int ID_Comunidad { get; set; }
        }
        public class Asistencia
        {
            public int ID_Usuario { get; set; }
            public int ID_Evento { get; set; }
        }

        private void InitializeOpenFileDialog()
        {
            openFileDialog = new OpenFileDialog
            {
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp",
                Title = "Select an Image"
            };
        }

        public DataTable ObtenerUsuarios()
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT * FROM Usuario";
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }

        private void CargarUsuarios()
        {
            DataTable dtUsuarios = ObtenerUsuarios();
        }

        public void InsertarUsuario(string nombre, string correo, string password)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "INSERT INTO Usuario (Nombre, Correo, Password) VALUES (@nombre, @correo, @password)";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@nombre", nombre);
                command.Parameters.AddWithValue("@correo", correo);
                command.Parameters.AddWithValue("@password", password);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        private void DisplayCommunityPage(string communityName)
        {
            ClearPanel();
            returnButton.Visible = false;
            pictureBox2.Visible = true;
            pictureBox2.BringToFront();

            int panelWidth = 600;
            int panelHeight = 400;

            int panelX = (this.ClientSize.Width - panelWidth) / 2;
            int panelY = (this.ClientSize.Height - panelHeight) / 2;

            Panel communityPanel = new Panel
            {
                Size = new Size(panelWidth, panelHeight),
                Location = new Point(panelX, panelY),
                BackColor = Color.LightGray
            };
            panel1.Controls.Add(communityPanel);

            Button backToMainPageButton = new Button
            {
                Size = new Size(40, 40),
                Location = new Point(10, 10),
                BackgroundImage = Properties.Resources.Icono, 
                BackgroundImageLayout = ImageLayout.Zoom,
                FlatStyle = FlatStyle.Flat,
                FlatAppearance = { BorderSize = 0 }
            };
            backToMainPageButton.Click += (s, e) => DisplayMainPage();
            panel1.Controls.Add(backToMainPageButton);

            Panel postsContainerPanel = new Panel
            {
                Size = new Size(communityPanel.Width - 20, communityPanel.Height - 80),
                Location = new Point(10, 10),
                AutoScroll = true,
                BackColor = Color.White
            };
            communityPanel.Controls.Add(postsContainerPanel);

            postTextBox = new TextBox
            {
                PlaceholderText = "Escribe un post...",
                Size = new Size(340, 30),
                Location = new Point(10, communityPanel.Height - 60)
            };
            communityPanel.Controls.Add(postTextBox);

            postButton = new Button
            {
                Text = "Publicar",
                Size = new Size(100, 30),
                Location = new Point(360, communityPanel.Height - 60)
            };
            postButton.Click += (sender, e) => PostButton_Click(postsContainerPanel);
            communityPanel.Controls.Add(postButton);

            uploadImageButton = new Button
            {
                Text = "Adjuntar Imagen",
                Size = new Size(120, 30),
                Location = new Point(470, communityPanel.Height - 60)
            };
            uploadImageButton.Click += (sender, e) => UploadImageButton_Click();
            communityPanel.Controls.Add(uploadImageButton);

            postImagePictureBox = new PictureBox
            {
                Size = new Size(100, 100),
                Location = new Point(0, 0),
                SizeMode = PictureBoxSizeMode.StretchImage,
                Visible = false
            };
            communityPanel.Controls.Add(postImagePictureBox);

            Label communityLabel = new Label
            {
                Text = "Comunidad: " + communityName,
                Location = new Point(10, communityPanel.Height - 30),
                AutoSize = true
            };
            communityPanel.Controls.Add(communityLabel);
        }
        private void LoadCommunityPosts()
        {
            centerPanel.Controls.Clear();

            var posts = new List<string>
    {
        "Post 1: Bien venido a nuestra comunidad!",
        "Post 2: Mira nuestros ultimos eventos.",
        "Post 3: Comparte tus ideas."
    };

            ListBox postsListBox = new ListBox
            {
                Size = new System.Drawing.Size(centerPanel.Width - 20, centerPanel.Height - 60),
                Location = new System.Drawing.Point(313, 40)
            };
            postsListBox.Items.AddRange(posts.ToArray());
            centerPanel.Controls.Add(postsListBox);
            
            TextBox newPostTextBox = new TextBox
            {
                PlaceholderText = "Escribe un post...",
                Size = new System.Drawing.Size(centerPanel.Width - 120, 30),
                Location = new System.Drawing.Point(10, centerPanel.Height - 80)
            };
            centerPanel.Controls.Add(newPostTextBox);

            Button postButton = new Button
            {
                Text = "Postea",
                Size = new System.Drawing.Size(100, 30),
                Location = new System.Drawing.Point(centerPanel.Width - 100, centerPanel.Height - 80)
            };
            postButton.Click += (s, e) => SubmitPost(newPostTextBox, postsListBox);
            centerPanel.Controls.Add(postButton);

            Button uploadImageButton = new Button
            {
                Text = "Subir Imagen",
                Size = new System.Drawing.Size(100, 30),
                Location = new System.Drawing.Point(centerPanel.Width - 200, centerPanel.Height - 80)
            };
            uploadImageButton.Click += (s, e) => UploadImage(postsListBox);
            centerPanel.Controls.Add(uploadImageButton);
        }

        private void SubmitPost(TextBox newPostTextBox, ListBox postsListBox)
        {
            if (!string.IsNullOrEmpty(newPostTextBox.Text))
            {
                postsListBox.Items.Add("Nuevo post: " + newPostTextBox.Text);
                newPostTextBox.Clear();
                postsListBox.SelectedIndex = postsListBox.Items.Count - 1;
            }
        }

        private void UploadImage(ListBox postsListBox)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;
                    postsListBox.Items.Add("Nueva Imagen: " + filePath);
                }
            }
        }

        private void UploadImageButton_Click()
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string imagePath = openFileDialog.FileName;
                postImagePictureBox.Image = System.Drawing.Image.FromFile(imagePath);
                postImagePictureBox.Visible = false;
            }
        }


        private void PostButton_Click(Panel postsContainerPanel)
        {
            postImagePictureBox.Visible = true;
            if (postTextBox != null && !string.IsNullOrEmpty(postTextBox.Text))
            {
                string postContent = postTextBox.Text;

                Panel postPanel = new Panel
                {
                    Size = new Size(postsContainerPanel.Width - 20, 120),
                    BackColor = Color.White,
                    BorderStyle = BorderStyle.FixedSingle
                };

                Label postLabel = new Label
                {
                    Text = "Publicación: " + postContent,
                    AutoSize = true
                };
                postPanel.Controls.Add(postLabel);

                if (postImagePictureBox != null && postImagePictureBox.Visible)
                {
                    PictureBox postPictureBox = new PictureBox
                    {
                        Image = postImagePictureBox.Image,
                        Size = new Size(100, 100),
                        Location = new Point(0, postLabel.Bottom + 10),
                        SizeMode = PictureBoxSizeMode.StretchImage
                    };
                    postPanel.Controls.Add(postPictureBox);

                    postImagePictureBox.Visible = false;
                }

                postsContainerPanel.Controls.Add(postPanel);

                postTextBox.Clear();

                postsContainerPanel.AutoScrollPosition = new Point(0, postsContainerPanel.VerticalScroll.Maximum - postsContainerPanel.Height);
            }
        }


        private void InitializePictureBox2()
        {
            pictureBox2 = new PictureBox();
            pictureBox2.Location = new System.Drawing.Point(10, 10);
            pictureBox2.Size = new System.Drawing.Size(50, 50);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.Image = Properties.Resources.Icono;
            pictureBox2.Click += new EventHandler(pictureBox2_Click);
            pictureBox2.BringToFront();
            pictureBox2.Visible = false;
            panel1.Controls.Add(pictureBox2);
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            panel1.Location = new System.Drawing.Point(10, 10);

            progressBar1.Minimum = 0;
            progressBar1.Maximum = 100;
            progressBar1.Step = 1;

            Timer timer = new Timer();
            timer.Interval = 50;
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
            pictureBox1.Visible = true;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (progressBar1.Value < progressBar1.Maximum)
            {
                progressBar1.PerformStep();
            }
            else
            {
                Timer timer = (Timer)sender;
                timer.Stop();

                progressBar1.Visible = false;

                ShowButtons();
            }
        }

        private void ShowButtons()
        {

            Button loginButton = new Button();
            loginButton.Text = "Iniciar Sesión";
            loginButton.Location = new System.Drawing.Point(410, 410);
            loginButton.Size = new System.Drawing.Size(110, 30);
            loginButton.Click += new EventHandler(LoginButton_Click);
            panel1.Controls.Add(loginButton);

            Button registerButton = new Button();
            registerButton.Text = "Registrarse";
            registerButton.Location = new System.Drawing.Point(410, 440);
            registerButton.Size = new System.Drawing.Size(110, 30);
            registerButton.Click += new EventHandler(RegisterButton_Click);
            panel1.Controls.Add(registerButton);
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            DisplayLoginScreen();
        }

        private void RegisterButton_Click(object sender, EventArgs e)
        {
            DisplayRegisterScreen();
        }

        private void DisplayLoginScreen()
        {
            ClearPanel();
            pictureBox1.Visible = false;

            Label loginLabel = new Label();
            loginLabel.Text = "Iniciar Sesión";
            loginLabel.Location = new System.Drawing.Point(370, 50);
            loginLabel.AutoSize = true;
            panel1.Controls.Add(loginLabel);

            TextBox usernameTextBox = new TextBox();
            usernameTextBox.PlaceholderText = "Nombre de usuario";
            usernameTextBox.Location = new System.Drawing.Point(370, 100);
            usernameTextBox.Size = new System.Drawing.Size(200, 30);
            panel1.Controls.Add(usernameTextBox);

            TextBox passwordTextBox = new TextBox();
            passwordTextBox.PlaceholderText = "Contraseña";
            passwordTextBox.PasswordChar = '*';
            passwordTextBox.Location = new System.Drawing.Point(370, 140);
            passwordTextBox.Size = new System.Drawing.Size(200, 30);
            panel1.Controls.Add(passwordTextBox);

            Button submitLoginButton = new Button();
            submitLoginButton.Text = "Iniciar Sesión";
            submitLoginButton.Location = new System.Drawing.Point(370, 180);
            submitLoginButton.Size = new System.Drawing.Size(110, 30);
            submitLoginButton.Click += new EventHandler(SubmitLoginButton_Click);
            panel1.Controls.Add(submitLoginButton);

            panel1.Controls.Add(returnButton);
            returnButton.Visible = true;
        }

        private void DisplayRegisterScreen()
        {
            ClearPanel();
            pictureBox1.Visible = false;

            Label registerLabel = new Label();
            registerLabel.Text = "Registrarse";
            registerLabel.Location = new System.Drawing.Point(370, 50);
            registerLabel.AutoSize = true;
            panel1.Controls.Add(registerLabel);

            TextBox usernameTextBox = new TextBox();
            usernameTextBox.PlaceholderText = "Nombre de usuario";
            usernameTextBox.Location = new System.Drawing.Point(370, 100);
            usernameTextBox.Size = new System.Drawing.Size(200, 30);
            panel1.Controls.Add(usernameTextBox);

            TextBox emailTextBox = new TextBox();
            emailTextBox.PlaceholderText = "Correo electrónico";
            emailTextBox.Location = new System.Drawing.Point(370, 140);
            emailTextBox.Size = new System.Drawing.Size(200, 30);
            panel1.Controls.Add(emailTextBox);

            TextBox passwordTextBox = new TextBox();
            passwordTextBox.PlaceholderText = "Contraseña";
            passwordTextBox.PasswordChar = '*';
            passwordTextBox.Location = new System.Drawing.Point(370, 180);
            passwordTextBox.Size = new System.Drawing.Size(200, 30);
            panel1.Controls.Add(passwordTextBox);

            Button submitRegisterButton = new Button();
            submitRegisterButton.Text = "Registrarse";
            submitRegisterButton.Location = new System.Drawing.Point(370, 220);
            submitRegisterButton.Size = new System.Drawing.Size(110, 30);
            submitRegisterButton.Click += new EventHandler(SubmitRegisterButton_Click);
            panel1.Controls.Add(submitRegisterButton);

            panel1.Controls.Add(returnButton);
            returnButton.Visible = true;
        }

        private void SubmitLoginButton_Click(object sender, EventArgs e)
        {
            DisplayMainPage();
            pictureBox2.Visible = true;
            returnButton.Visible = false;
        }

        private void SubmitRegisterButton_Click(object sender, EventArgs e)
        {
            DisplayMainPage();
            pictureBox2.Visible = true;
            returnButton.Visible = false;
        }

        private void DisplayMainPage()
        {
            ClearPanel();
            returnButton.Visible = false;
            pictureBox2.BringToFront();
            pictureBox2.Visible = true;

            Button backToMainPageButton = new Button
            {
                Size = new System.Drawing.Size(40, 40),
                Location = new System.Drawing.Point(0, 0),
                BackgroundImage = Properties.Resources.Icono,
                BackgroundImageLayout = ImageLayout.Zoom,
                FlatStyle = FlatStyle.Flat,
                FlatAppearance = { BorderSize = 0 }
            };
            backToMainPageButton.Click += (s, e) => DisplayMainPage();
            panel1.Controls.Add(backToMainPageButton);

            leftPanel = new Panel();
            leftPanel.Size = new System.Drawing.Size(200, this.ClientSize.Height - 40);
            leftPanel.Location = new System.Drawing.Point(0, 40);
            leftPanel.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(leftPanel);

            centerPanel = new Panel();
            centerPanel.Size = new System.Drawing.Size(480, this.ClientSize.Height - 40);
            centerPanel.Location = new System.Drawing.Point(220, 40);
            panel1.Controls.Add(centerPanel);

            rightPanel = new Panel();
            rightPanel.Size = new System.Drawing.Size(200, this.ClientSize.Height - 40);
            rightPanel.Location = new System.Drawing.Point(700, 40);
            rightPanel.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(rightPanel);

            searchTextBox = new TextBox();
            searchTextBox.PlaceholderText = "Buscar";
            searchTextBox.Size = new System.Drawing.Size(220, 30);
            searchTextBox.Location = new System.Drawing.Point(680, 10);
            panel1.Controls.Add(searchTextBox);

            friendsButton = new Button();
            friendsButton.Text = "Amigos";
            friendsButton.Size = new System.Drawing.Size(180, 30);
            friendsButton.Location = new System.Drawing.Point(10, 10);
            friendsButton.Click += new EventHandler(FriendsButton_Click);
            leftPanel.Controls.Add(friendsButton);

            requestsButton = new Button();
            requestsButton.Text = "Solicitudes";
            requestsButton.Size = new System.Drawing.Size(180, 30);
            requestsButton.Location = new System.Drawing.Point(10, 50);
            requestsButton.Click += new EventHandler(RequestsButton_Click);
            leftPanel.Controls.Add(requestsButton);

            communitiesButton = new Button();
            communitiesButton.Text = "Comunidades";
            communitiesButton.Size = new System.Drawing.Size(180, 30);
            communitiesButton.Location = new System.Drawing.Point(10, 90);
            communitiesButton.Click += new EventHandler(CommunitiesButton_Click);
            leftPanel.Controls.Add(communitiesButton);

            infoListBox = new ListBox();
            infoListBox.Size = new System.Drawing.Size(180, this.ClientSize.Height - 120);
            infoListBox.Location = new System.Drawing.Point(10, 130);
            infoListBox.Click += new EventHandler(InfoListBox_Click);
            leftPanel.Controls.Add(infoListBox);

            LoadMainPageContent();
        }

        private void LoadMainPageContent()
        {
            Label centralContentLabel = new Label();
            centralContentLabel.Text = "Contenido Actual";
            centralContentLabel.Location = new System.Drawing.Point(10, 10);
            centralContentLabel.AutoSize = true;
            centerPanel.Controls.Add(centralContentLabel);

            Label rightContentLabel = new Label();
            rightContentLabel.Text = "Noticias Actuales";
            rightContentLabel.Location = new System.Drawing.Point(10, 10);
            rightContentLabel.AutoSize = true;
            rightPanel.Controls.Add(rightContentLabel);
        }

        private void FriendsButton_Click(object sender, EventArgs e)
        {
            infoListBox.Items.Clear();
            infoListBox.Items.Add("Amigo 1");
            infoListBox.Items.Add("Amigo 2");
            infoListBox.Items.Add("Amigo 3");
        }

        private void RequestsButton_Click(object sender, EventArgs e)
        {
            
        }

        private void CommunitiesButton_Click(object sender, EventArgs e)
        {
            infoListBox.Items.Clear();
            infoListBox.Items.Add("Comunidad 1");
            infoListBox.Items.Add("Comunidad 2");
            infoListBox.Items.Add("Comunidad 3");
        }

        private void InitializeInfoListBox()
        {
            infoListBox = new ListBox
            {
                Size = new System.Drawing.Size(180, this.ClientSize.Height - 120),
                Location = new System.Drawing.Point(10, 130)
            };
            infoListBox.Click += new EventHandler(InfoListBox_Click);
            leftPanel.Controls.Add(infoListBox);
        }

        private void InfoListBox_Click(object sender, EventArgs e)
{
    if (infoListBox.SelectedItem != null)
    {
        string selectedItem = infoListBox.SelectedItem.ToString();

        if (selectedItem.StartsWith("Amigo"))
        {
            DisplayPrivateMessages(selectedItem);
        }
        else if (selectedItem.StartsWith("Comunidad"))
        {
            DisplayCommunityPage(selectedItem);
        }
    }
}
        private void InitializeReturnButton()
        {
            returnButton = new Button();
            returnButton.Text = "Regresar";
            returnButton.Size = new System.Drawing.Size(110, 30);
            returnButton.Location = new System.Drawing.Point(10, 10);
            returnButton.Click += new EventHandler(ReturnButton_Click);
            returnButton.Visible = false;
            panel1.Controls.Add(returnButton);
        }

        private void ReturnButton_Click(object sender, EventArgs e)
        {
            ClearPanel();
            returnButton.Visible = false;    
            ShowButtons(); 
            pictureBox1.Visible = true;
            pictureBox1.BringToFront();    
        }

        private void ClearPanel()
        {
            panel1.Controls.Clear();
            panel1.Controls.Add(returnButton);
        }

        private void DisplayPrivateMessages(string userName)
        {
            ClearPanel();
            returnButton.Visible = false;
            pictureBox2.Visible = true;
            pictureBox2.BringToFront();

            searchTextBox = new TextBox
            {
                PlaceholderText = "Buscar",
                Size = new System.Drawing.Size(220, 30),
                Location = new System.Drawing.Point(700, 10)
            };
            panel1.Controls.Add(searchTextBox);

            callButton = new Button
            {
                Text = "Llamada",
                Size = new System.Drawing.Size(80, 30),
                Location = new System.Drawing.Point(130, 10)
            };
            panel1.Controls.Add(callButton);

            videoCallButton = new Button
            {
                Text = "Videollamada",
                Size = new System.Drawing.Size(100, 30),
                Location = new System.Drawing.Point(220, 10)
            };
            panel1.Controls.Add(videoCallButton);

            pinnedMessagesButton = new Button
            {
                Text = "Mensajes Fijados",
                Size = new System.Drawing.Size(120, 30),
                Location = new System.Drawing.Point(330, 10)
            };
            panel1.Controls.Add(pinnedMessagesButton);

            Button backToMainPageButton = new Button
            {
                Size = new System.Drawing.Size(40, 40),
                Location = new System.Drawing.Point(0, 0),
                BackgroundImage = Properties.Resources.Icono,
                BackgroundImageLayout = ImageLayout.Zoom,
                FlatStyle = FlatStyle.Flat,
                FlatAppearance = { BorderSize = 0 }
            };
            backToMainPageButton.Click += (s, e) => DisplayMainPage();
            panel1.Controls.Add(backToMainPageButton);

            if (leftPanel == null)
            {
                leftPanel = new Panel
                {
                    Size = new System.Drawing.Size(200, this.ClientSize.Height - 40),
                    Location = new System.Drawing.Point(0, 40),
                    BorderStyle = BorderStyle.FixedSingle
                };
                panel1.Controls.Add(leftPanel);

                friendsButton = new Button
                {
                    Text = "Amigos",
                    Size = new System.Drawing.Size(180, 30),
                    Location = new System.Drawing.Point(10, 10)
                };
                leftPanel.Controls.Add(friendsButton);

                requestsButton = new Button
                {
                    Text = "Solicitudes",
                    Size = new System.Drawing.Size(180, 30),
                    Location = new System.Drawing.Point(10, 50)
                };
                leftPanel.Controls.Add(requestsButton);

                communitiesButton = new Button
                {
                    Text = "Comunidades",
                    Size = new System.Drawing.Size(180, 30),
                    Location = new System.Drawing.Point(10, 90)
                };
                leftPanel.Controls.Add(communitiesButton);

                infoListBox = new ListBox
                {
                    Size = new System.Drawing.Size(180, this.ClientSize.Height - 140),
                    Location = new System.Drawing.Point(10, 130)
                };
                leftPanel.Controls.Add(infoListBox);
            }
            else
            {
                leftPanel.Visible = true;
            }

            if (rightPanel == null)
            {
                rightPanel = new Panel
                {
                    Size = new System.Drawing.Size(200, this.ClientSize.Height - 40),
                    Location = new System.Drawing.Point(this.ClientSize.Width - 200, 40),
                    BorderStyle = BorderStyle.FixedSingle
                };
                panel1.Controls.Add(rightPanel);

                Label profileLabel = new Label
                {
                    Text = "Perfil de " + userName,
                    Location = new System.Drawing.Point(10, 10),
                    AutoSize = true
                };
                rightPanel.Controls.Add(profileLabel);

                PictureBox profilePicture = new PictureBox
                {
                    Size = new System.Drawing.Size(100, 100),
                    Location = new System.Drawing.Point(10, 40),
                    BorderStyle = BorderStyle.FixedSingle
                };
                rightPanel.Controls.Add(profilePicture);

                Label bioLabel = new Label
                {
                    Text = "Bio del usuario\nFecha de unión\nServidores/Amigos en común",
                    Location = new System.Drawing.Point(10, 150),
                    AutoSize = true
                };
                rightPanel.Controls.Add(bioLabel);
            }
            else
            {
                rightPanel.Visible = true;
            }

            if (messagesPanel == null)
            {
                messagesPanel = new Panel
                {
                    Size = new System.Drawing.Size(480, this.ClientSize.Height - 40),
                    Location = new System.Drawing.Point(220, 40),
                    BorderStyle = BorderStyle.FixedSingle
                };
                panel1.Controls.Add(messagesPanel);
            }
            else
            {
                messagesPanel.Visible = true;
            }

            messagesPanel.Controls.Clear();
            Label userLabel = new Label
            {
                Text = "Chat con " + userName,
                Location = new System.Drawing.Point(10, 10),
                AutoSize = true
            };
            messagesPanel.Controls.Add(userLabel);

            ListBox messagesListBox = new ListBox
            {
                Size = new System.Drawing.Size(460, messagesPanel.Height - 100),
                Location = new System.Drawing.Point(10, 40)
            };
            messagesPanel.Controls.Add(messagesListBox);

            TextBox messageTextBox = new TextBox
            {
                PlaceholderText = "Escribe un mensaje...",
                Size = new System.Drawing.Size(340, 30),
                Location = new System.Drawing.Point(10, messagesPanel.Height - 70)
            };
            messagesPanel.Controls.Add(messageTextBox);

            Button sendButton = new Button
            {
                Text = "Enviar",
                Size = new System.Drawing.Size(100, 30),
                Location = new System.Drawing.Point(360, messagesPanel.Height - 70)
            };
            sendButton.Click += (sender, e) => SendButton_Click(messageTextBox, messagesListBox);
            messagesPanel.Controls.Add(sendButton);
        }
        private void BackButton_Click(object sender, EventArgs e)
        {
            ClearPanel();
            DisplayMainPage();
        }
        private void SendButton_Click(TextBox messageTextBox, ListBox messagesListBox)
        {
            if (messageTextBox != null && !string.IsNullOrEmpty(messageTextBox.Text))
            {
                messagesListBox.Items.Add("Yo: " + messageTextBox.Text);
                messageTextBox.Clear();
                messagesListBox.SelectedIndex = messagesListBox.Items.Count - 1;
            }
        }

        private void DisplayCommunities()
        {
            ClearPanel();
            returnButton.Visible = false;
            pictureBox2.BringToFront();
            pictureBox2.Visible = true; 
        }


            private void pictureBox1_Click(object sender, EventArgs e)
        {

        }


        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }


        private void pictureBox2_Click(object sender, EventArgs e)
        {
            ClearPanel();
            DisplayMainPage();
        }
    }
}