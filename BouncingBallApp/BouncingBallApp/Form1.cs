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
        private const float gravity = 0.5f; // Gravité simulée

        private const float bounceDampening = 0.6f; // Réduction d'énergie après chaque rebond
        private const float friction = 0.98f; // Réduction de la vitesse horizontale après un rebond au sol

        private const int ballSize = 150; // Taille de la balle
        private Image ballImage; // Image personnalisée pour la balle

        private bool mouseOnClick = false; // Indique si la balle est déplacée manuellement
        private float offsetX = 0; // Décalage X entre la souris et la balle
        private float offsetY = 0; // Décalage Y entre la souris et la balle

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
                ballImage = null; // Définit null si l'image ne peut pas être chargée
            }
        }

        // Paramètres personnalisés du formulaire
        private void InitializeCustomSettings()
        {
            this.DoubleBuffered = true; // Activer le double-buffering pour des graphismes fluides
            this.TransparencyKey = this.BackColor; // Rendre le fond transparent
            this.TopMost = true; // L'application reste toujours au premier plan
            this.FormBorderStyle = FormBorderStyle.None; // Supprime les bordures
            this.WindowState = FormWindowState.Maximized; // Fenêtre plein écran

            // Création d'un timer pour gérer les animations
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer
            {
                Interval = 10 // Environ 60 FPS
            };
            timer.Tick += (sender, e) => MoveBall();
            timer.Start();
        }

        // Gérer l'affichage de la balle
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

        // Déplacement automatique de la balle
        private void MoveBall()
        {
            if (mouseOnClick) return; // Ne pas déplacer automatiquement si la balle est contrôlée par la souris

            // Mise à jour des positions
            x += dx;
            y += dy;

            // Appliquer la gravité
            dy += gravity;

            // Collision avec le bord gauche/droit
            if (x + ballSize >= this.ClientSize.Width || x <= 0)
            {
                dx = -dx * bounceDampening; // Inverse et réduit la vitesse horizontale
                x = Math.Max(0, Math.Min(x, this.ClientSize.Width - ballSize));
            }

            // Collision avec le bas (sol)
            if (y + ballSize >= this.ClientSize.Height)
            {
                dy = -dy * bounceDampening; // Inverse et réduit la vitesse verticale
                y = this.ClientSize.Height - ballSize;

                dx *= friction; // Réduction progressive de la vitesse horizontale
                if (Math.Abs(dy) < 0.1f) dy = 0; // Stoppe les rebonds verticaux infimes
                if (Math.Abs(dx) < 0.1f) dx = 0; // Stoppe les mouvements horizontaux infimes
            }

            // Collision avec le plafond
            if (y <= 0)
            {
                dy = -dy * bounceDampening; // Inverse et réduit la vitesse verticale
                y = 0;
            }

            // Forcer le rafraîchissement de l'affichage
            Invalidate();
        }

        // Début du clic souris pour attraper la balle
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            // Vérifier si le clic est sur la balle
            if (e.X >= x && e.X <= x + ballSize && e.Y >= y && e.Y <= y + ballSize)
            {
                mouseOnClick = true;
                offsetX = e.X - x;
                offsetY = e.Y - y;
            }
        }

        // Déplacement de la balle avec la souris
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (mouseOnClick)
            {
                // Mise à jour de la position de la balle
                x = e.X - offsetX;
                y = e.Y - offsetY;

                // Empêche la balle de sortir des bords
                x = Math.Max(0, Math.Min(x, this.ClientSize.Width - ballSize));
                y = Math.Max(0, Math.Min(y, this.ClientSize.Height - ballSize));

                Invalidate(); // Redessiner la balle
            }
        }

        // Fin du clic souris
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            mouseOnClick = false; // Libère la balle
        }
    }
}
