using ProyectoPokemon;
using System;
using System.Collections.Generic;

// Crear tipos de Pokémon
Tipos fuego = new Tipos("Fuego");
Tipos agua = new Tipos("Agua");
Tipos planta = new Tipos("Planta");

// Crear ataques
Ataque ataqueFuego = new Ataque("Lanzallamas", 40, new List<Tipos> { fuego }, true);
Ataque ataqueAgua = new Ataque("Rayo Hielo", 30, new List<Tipos> { agua }, true);
Ataque ataquePlanta = new Ataque("Hoja Afilada", 35, new List<Tipos> { planta }, true);

// Crear Pokémon
Pokemon charmander = new Pokemon("Charmander", 100, ataqueFuego, new List<Ataque> { ataqueFuego }, new List<Tipos> { fuego });
Pokemon squirtle = new Pokemon("Squirtle", 100, ataqueAgua, new List<Ataque> { ataqueAgua }, new List<Tipos> { agua });

// Crear entrenadores
Entrenador ash = new Entrenador("Ash");
Entrenador gary = new Entrenador("Gary");


// Finalizar la batalla
Batalla batalla = new Batalla(ash, gary);

ash.elegirEquipo();
gary.elegirEquipo();
batalla.procesarTurno();
