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

// Agregar Pokémon a los equipos
ash.Equipo.Add(charmander);
gary.Equipo.Add(squirtle);

// Establecer Pokémon activo
ash.Activo = charmander;
gary.Activo = squirtle;

// Crear la batalla
BatallaFacade batallaFacade = new BatallaFacade(ash, gary);
batallaFacade.iniciarBatalla();
while (!ash.Activo.EstaDerrotado && !gary.Activo.EstaDerrotado)
{
    // Mostrar estado de los Pokémon
    batallaFacade.mostrarEstadoPokemones();
    
    // Turno de Ash
    Console.WriteLine($"{ash.Nombre}, es tu turno.");
    batallaFacade.ataqueJugador(0); // Suponiendo que Ash elige el primer ataque
    batallaFacade.procesarTurno(0); // Procesar el turno de Ash

    // Verificar si el Pokémon oponente ha sido derrotado
    if (gary.Activo.EstaDerrotado)
    {
        Console.WriteLine($"{gary.Nombre} ha sido derrotado. ¡{ash.Nombre} gana!");
        break;
    }

    // Turno de Gary
    Console.WriteLine($"{gary.Nombre}, es tu turno.");
    batallaFacade.procesarTurno(0); // Procesar el turno del oponente (Gary)
    
    // Verificar si el Pokémon oponente ha sido derrotado
    if (ash.Activo.EstaDerrotado)
    {
        Console.WriteLine($"{ash.Nombre} ha sido derrotado. ¡{gary.Nombre} gana!");
        break;
    }
}

// Finalizar la batalla
batallaFacade.finalizarBatalla();
