# BouncingBallApp - README

## Description
BouncingBallApp est une application Windows Forms simple qui simule une balle rebondissante. La balle se déplace automatiquement, rebondissant sur les bords de la fenêtre, et réagit à la gravité et à la friction. Les utilisateurs peuvent également interagir avec la balle en cliquant et en la déplaçant sur l'écran.

## Fonctionnalités
- **Simulation de la balle rebondissante** : La balle se déplace en permanence et rebondit sur les bords de la fenêtre et le sol, simulant des phénomènes physiques comme la gravité et la friction.
- **Interaction avec la souris** : L'utilisateur peut cliquer sur la balle pour la déplacer, et la balle suivra les mouvements de la souris.
- **Balle personnalisable** : L'application permet d'utiliser une image personnalisée pour la balle (si disponible), sinon une balle rouge sera dessinée par défaut.
- **Animation fluide** : Le double buffering et l'anti-aliasing sont activés pour assurer une animation fluide de la balle.

## Prérequis
- .NET Framework (Application Windows Forms)
- L'application utilise les namespaces `System.Drawing` et `System.Windows.Forms`.
- Un fichier image personnalisé (`Ball.png`) pour la balle est attendu dans le même répertoire que l'exécutable. Si l'image n'est pas trouvée, une balle rouge sera dessinée à la place.

## Détails des fonctionnalités
1. **Mouvement automatique** :
   - La balle se déplace avec une vitesse initiale (`dx` et `dy`) et est affectée par la gravité simulée.
   - La balle rebondit sur les bords de la fenêtre et le sol avec une diminution progressive de la vitesse, simulant une perte d'énergie (`bounceDampening`).
   - La vitesse horizontale diminue après chaque rebond au sol en raison de la friction.

2. **Interaction avec la souris** :
   - Cliquer sur la balle permet de la déplacer manuellement.
   - Le mouvement de la balle est limité à l'intérieur de la fenêtre, empêchant la balle de sortir de l'écran.

3. **Paramètres personnalisés** :
   - Fenêtre en plein écran sans bordures.
   - L'application reste toujours au premier plan par rapport aux autres fenêtres.
   - Double buffering pour une animation plus fluide.
   - Paramètres de gravité et de réduction de rebond ajustables pour obtenir des effets physiques différents.

## Contrôles
- **Clic gauche** : Attraper et déplacer la balle.
- **Relâcher le clic gauche** : Libérer la balle du contrôle manuel ; la balle reprendra son mouvement automatique.

## Installation
1. Téléchargez les fichiers du projet.
2. Ouvrez le projet dans Visual Studio ou tout autre IDE compatible avec .NET.
3. Compilez et lancez le projet.

### Optionnel :
- Placez une image personnalisée de la balle (`Ball.png`) dans le répertoire de l'application pour utiliser un design personnalisé pour la balle. Si l'image n'est pas trouvée, une balle rouge par défaut sera affichée.

## Explication du code

### Variables principales :
- `x`, `y` : Position actuelle de la balle.
- `dx`, `dy` : Vitesse de la balle sur les axes horizontal et vertical.
- `gravity` : Gravité simulée affectant la vitesse verticale.
- `bounceDampening` : Réduction de la vitesse après chaque rebond.
- `friction` : Réduction de la vitesse horizontale lorsqu'elle touche le sol.

### Méthodes principales :
- `OnPaint` : Responsabilité de dessiner la balle sur le formulaire.
- `MoveBall` : Gère le mouvement automatique de la balle et les phénomènes physiques (gravité, rebonds).
- `OnMouseDown`, `OnMouseMove`, `OnMouseUp` : Gèrent les interactions avec la souris pour déplacer la balle manuellement.
- `InitializeCustomSettings` : Personnalise l'apparence et les paramètres du formulaire, y compris l'activation du timer pour l'animation.

## Licence
Ce projet est en open source. Vous pouvez le modifier et le distribuer sous les termes de la licence MIT.

---

Amusez-vous à faire rebondir la balle !
![image](https://github.com/user-attachments/assets/8dd9374d-ef5f-4b14-92f7-f343a7e16502)

