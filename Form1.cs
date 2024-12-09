using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace petrolproje
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Giriş butonuna tıklama olayı
        private void btnLogin_Click(object sender, EventArgs e)
        {
            // Kullanıcı adı ve şifreyi alalım
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            // Kullanıcı türünü seçelim
            string userType = cmbUserType.SelectedItem.ToString();

            // Veritabanı bağlantı dizesi
            string connectionString = "Server=LEGEND; Database=PetrolProje; Integrated Security=True;";

            // Kullanıcı doğrulama sorgusu
            string query = "SELECT COUNT(*) FROM users WHERE username = @username AND password = @password AND userType = @userType";

            // Veritabanı bağlantısını açalım
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    // Bağlantıyı açalım
                    connection.Open();

                    // SQL komutunu hazırlayalım
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Parametreleri ekleyelim
                        command.Parameters.AddWithValue("@username", username);
                        command.Parameters.AddWithValue("@password", password);
                        command.Parameters.AddWithValue("@userType", userType);

                        // Sonucu alalım
                        int result = (int)command.ExecuteScalar();

                        // Giriş başarılı ise
                        if (result > 0)
                        {
                            MessageBox.Show("Giriş başarılı! Hoşgeldiniz, " + userType + ".", "Giriş Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Kullanıcı türüne göre yönlendirme yapalım
                            if (userType == "Yönetici")
                            {
                                // Yönetici ekranına yönlendirme
                                // AdminPanelForm adminPanel = new AdminPanelForm();
                                // adminPanel.Show();
                            }
                            else if (userType == "Personel")
                            {
                                // Personel ekranına yönlendirme
                                // PersonnelDashboardForm personnelForm = new PersonnelDashboardForm();
                                // personnelForm.Show();
                            }

                            // Giriş formunu kapat
                            this.Hide();  // Giriş formunu gizler, ancak form hala açık kalır.
                        }
                        else
                        {
                            // Giriş başarısız
                            MessageBox.Show("Kullanıcı adı veya şifre hatalı! Lütfen tekrar deneyin.", "Giriş Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Veritabanı bağlantı hatası
                    MessageBox.Show("Veritabanı bağlantı hatası: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Şifremi Unuttum linkine tıklama olayı
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Şifre sıfırlama işlemi başlatılabilir
            MessageBox.Show("Şifre sıfırlama işlemi başlatıldı.");
            // Veya yeni bir şifre sıfırlama formu açılabilir
            // ResetPasswordForm resetForm = new ResetPasswordForm();
            // resetForm.Show();
        }
    }
}
