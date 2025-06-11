# 🎮 Division Galaxy - Jeu mobile éducatif

## 🎯 Objectif du projet  
Ce jeu mobile a pour objectif d’évaluer les compétences des enfants en **division mathématique** tout en leur offrant une expérience ludique et engageante.  
L’idée est de proposer une interface de type *gaming* afin que les élèves ne s’ennuient pas et qu’ils ne réalisent pas explicitement qu’ils sont en train d’être évalués.  
Le but est de **favoriser l’apprentissage de manière naturelle et motivante**.

---

## 🛬 Page d'accueil  
Cette page est conçue pour permettre l’**authentification des élèves**.  
L’authentification s’effectue après que l’élève a **scanné un QR code** généré lors de sa création sur la plateforme web.  
Une fois le QR code scanné depuis la page d’accueil de l’application, les informations de l’élève sont **récupérées depuis Firebase** puis stockées localement dans le jeu pour personnaliser l’expérience utilisateur.

📷 *[]*

---

## 🌌 Sélection du Mini-jeu  
Cette section agit comme une **landing page interactive**, permettant à l’élève de choisir le mini-jeu auquel il souhaite jouer.  
L’élève navigue à bord de son **vaisseau spatial** à travers un univers imaginaire, où chaque mini-jeu est représenté par une **planète** ou une **galaxie**.  
Cette approche visuelle renforce l’engagement tout en rendant la navigation intuitive et amusante.

📷 *[Insérer une image ici]*

---

## 📘 Guides  
Chaque mini-jeu est accompagné d’un **guide interactif** pour aider l’élève à comprendre les règles du jeu.  
Ces guides sont simples, illustrés, et adaptés à un jeune public.  
Ils permettent aux enfants de jouer de manière **autonome**, tout en garantissant la compréhension des objectifs pédagogiques.

📷 *[Insérer une image ici]*

---

## 🕹️ Mini-jeux

### 🚀 Mini-jeu 1 : Space Shooter  
Ce mini-jeu permet à l’élève d’**identifier les expressions de division** qui donnent un résultat égal à une **valeur cible**.

Le joueur contrôle un **vaisseau spatial** dans un champ d’**astéroïdes**.  
Chaque astéroïde contient une expression de division (correcte ou incorrecte).  
- ✅ Si le joueur touche une bonne réponse → **le score augmente**  
- ❌ Si c’est une mauvaise réponse → **le score diminue**  
L’élève peut aussi **tirer** pour éliminer les mauvaises réponses avant de les percuter.

📷 *[Insérer une image ici]*

---

### 👨‍🚀 Mini-jeu 2 : Astronaute  
Dans ce mini-jeu, l’élève incarne un astronaute qui **saute** lorsqu'on appuie sur la barre d’espace (ou touche l’écran).  
L’objectif est de **sélectionner la bonne réponse** parmi deux propositions attachées à des **pierres flottantes**.

- Les opérations de division sont générées dynamiquement.
- Une seule bonne réponse est proposée à chaque fois.
- Le joueur dispose de **5 cœurs**.  
  Chaque erreur coûte un cœur.  
  Quand les 5 sont perdus, la partie se termine et le meilleur score est enregistré.

📷 *[Insérer une image ici]*

---

### 🌍 Mini-jeu 3 : Save The World  
Ce mini-jeu propose des exercices de **division verticale** à compléter avec un système de **glisser-déposer (drag & drop)**.

- Composé de **3 niveaux de difficulté** croissante.
- À chaque niveau, l’élève doit remplir des champs manquants (quotient, soustractions, restes).
- Plus le niveau est élevé, plus il y a de champs vides et d’options (fausses et vraies).
- Un système de **feedback visuel et sonore** aide l’élève à se corriger.

📷 *[Insérer une image ici]*

---

## 🔧 Technologies utilisées  
- **Unity** (moteur de jeu)  
- **C#** (langage de programmation)  
- **Firebase** (authentification et base de données)  


