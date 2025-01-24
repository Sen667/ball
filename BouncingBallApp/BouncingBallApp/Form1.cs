using System;
using System.Drawing;
using System.Windows.Forms;

namespace BouncingBallApp
{
    public partial class Form1 : Form
    {
        private float x = 100; // Position initiale de la balle (X)
        private float y = 100; // Position initiale de la balle (Y)
        private float dx = 4;  // Vitesse horizontale
        private float dy = 2;  // Vitesse verticale
        private const float gravity = 0.5f; // Gravit� simul�e

        private const float bounceDampening = 0.6f; // R�duction d'�nergie apr�s chaque rebond
        private const float friction = 0.98f; // R�duction de la vitesse horizontale apr�s un rebond au sol

        private const int ballSize = 150; // Taille de la balle
        private Image ballImage; // Image personnalis�e pour la balle

        private bool mouseOnClick = false; // Indique si la balle est d�plac�e manuellement
        private float offsetX = 0; // D�calage X entre la souris et la balle
        private float offsetY = 0; // D�calage Y entre la souris et la balle

        public Form1()
        {
            InitializeComponent();
            InitializeCustomSettings();

            // Charger l'image de la balle
            try
            {
                ballImage = Image.FromFile("Ball.png"); // Nom du fichier PNG
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du chargement de l'image : {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ballImage = null; // D�finit null si l'image ne peut pas �tre charg�e
            }
        }

        // Param�tres personnalis�s du formulaire
        private void InitializeCustomSettings()
        {
            this.DoubleBuffered = true; // Activer le double-buffering pour des graphismes fluides
            this.TransparencyKey = this.BackColor; // Rendre le fond transparent
            this.TopMost = true; // L'application reste toujours au premier plan
            this.FormBorderStyle = FormBorderStyle.None; // Supprime les bordures
            this.WindowState = FormWindowState.Maximized; // Fen�tre plein �cran

            // Cr�ation d'un timer pour g�rer les animations
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer
            {
                Interval = 10 // Environ 60 FPS
            };
            timer.Tick += (sender, e) => MoveBall();
            timer.Start();
        }

        // G�rer l'affichage de la balle
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Activer l'anti-aliasing pour des graphismes plus fluides
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Dessiner la balle si l'image est disponible
            if (ballImage != null)
            {
                e.Graphics.DrawImage(ballImage, x, y, ballSize, ballSize);
            }
            else
            {
                // En cas d'erreur de chargement, dessiner une balle rouge
                e.Graphics.FillEllipse(Brushes.Red, x, y, ballSize, ballSize);
            }
        }

        // D�placement automatique de la balle
        private void MoveBall()
        {
            if (mouseOnClick) return; // Ne pas d�placer automatiquement si la balle est contr�l�e par la souris

            // Mise � jour des positions
            x += dx;
            y += dy;

            // Appliquer la gravit�
            dy += gravity;

            // Collision avec le bord gauche/droit
            if (x + ballSize >= this.ClientSize.Width || x <= 0)
            {
                dx = -dx * bounceDampening; // Inverse et r�duit la vitesse horizontale
                x = Math.Max(0, Math.Min(x, this.ClientSize.Width - ballSize));
            }

            // Collision avec le bas (sol)
            if (y + ballSize >= this.ClientSize.Height)
            {
                dy = -dy * bounceDampening; // Inverse et r�duit la vitesse verticale
                y = this.ClientSize.Height - ballSize;

                dx *= friction; // R�duction progressive de la vitesse horizontale
                if (Math.Abs(dy) < 0.1f) dy = 0; // Stoppe les rebonds verticaux infimes
                if (Math.Abs(dx) < 0.1f) dx = 0; // Stoppe les mouvements horizontaux infimes
            }

            // Collision avec le plafond
            if (y <= 0)
            {
                dy = -dy * bounceDampening; // Inverse et r�duit la vitesse verticale
                y = 0;
            }

            // Forcer le rafra�chissement de l'affichage
            Invalidate();
        }

        // D�but du clic souris pour attraper la balle
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            // V�rifier si le clic est sur la balle
            if (e.X >= x && e.X <= x + ballSize && e.Y >= y && e.Y <= y + ballSize)
            {
                mouseOnClick = true;
                offsetX = e.X - x;
                offsetY = e.Y - y;
            }
        }

        // D�placement de la balle avec la souris
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (mouseOnClick)
            {
                // Mise � jour de la position de la balle
                x = e.X - offsetX;
                y = e.Y - offsetY;

                // Emp�che la balle de sortir des bords
                x = Math.Max(0, Math.Min(x, this.ClientSize.Width - ballSize));
                y = Math.Max(0, Math.Min(y, this.ClientSize.Height - ballSize));

                Invalidate(); // Redessiner la balle
            }
        }

        // Fin du clic souris
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            mouseOnClick = false; // Lib�re la balle
        }
    }
}
