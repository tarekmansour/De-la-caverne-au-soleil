# De-la-caverne-au-soleil
**Kata** "De la caverne au soleil": passer d'un code crade à un code facile à faire évoluer.

l'API de restervation de bar: trouver la meilleure date et le meilleur bar pour rassembler les développeurs et faire la fête.

Le projet est fait en `architecture n-tiers` et n'a pas évolué depuis longtemps. Essayer d'adopter une approche plus proche de celle de `DDD` avec une `architecture hexagonale`.

## Use cases:

**1. Les bateaux:**
   
  Ajouter une nouvelle source de données pour les bars à bateaux (_les péniches_). Si possible, les bateaux sont toujours notre premier choix et doivent être réservés à la place d'un bar intérieur. Essayez d'implémenter ceci sans refactoring. Ensuite, faites autant de refactoring que nécessaire.

**2. Capacité du bar:**
  
  Les bars se plaignent que nous remplissons trop d'espace. Nous aimerions réserver uniquement des bars où nous remplissons moins de 80% de leur capacité.

**3. rooftops:**

  Nous sommes heureux d'avoir ajouté les bateaux, mais nous avons oublié les rooftops ! il s'agit d'une autre source de données.