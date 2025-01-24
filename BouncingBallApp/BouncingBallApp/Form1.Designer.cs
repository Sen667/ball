using System.Drawing;
using System.Windows.Forms;

namespace BouncingBallApp
{
    public partial class Form1
    {
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(800, 600); // Taille par défaut
            this.FormBorderStyle = FormBorderStyle.None; // Pas de bordures
            this.WindowState = FormWindowState.Maximized; // Fenêtre plein écran
            this.BackColor = Color.Black; // Fond noir (utilisé pour la transparence avec TransparencyKey)
            this.Name = "Form1";
            this.ResumeLayout(false);
        }
    }
}
