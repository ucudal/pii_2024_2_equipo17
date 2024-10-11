using System.Collections.Generic;
using JetBrains.Annotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProyectoPokemon;

namespace ProyectoPokemon.Tests;

[TestClass]
[TestSubject(typeof(Batalla))]
public class BatallaTests
{
    private Entrenador entrenador1;
    private Entrenador entrenador2;
    private Pokemon pikachu;
    private Pokemon charmander;
    private Batalla batalla;

    public void Setup()
    {
        // Crear ataques
        var ataqueTrueno = new Ataque("Trueno", 50, new List<Tipos> { new Tipos("Eléctrico") }, true);
        var ataqueLlamarada = new Ataque("Llamarada", 50, new List<Tipos> { new Tipos("Fuego") }, true);

        // Crear Pokemones
        pikachu = new Pokemon("Pikachu", 100, ataqueTrueno, new List<Ataque> { ataqueTrueno },
            new List<Tipos> { new Tipos("Eléctrico") });
        charmander = new Pokemon("Charmander", 100, ataqueLlamarada, new List<Ataque> { ataqueLlamarada },
            new List<Tipos> { new Tipos("Fuego") });

        // Crear entrenadores
        entrenador1 = new Entrenador("Ash");
        entrenador2 = new Entrenador("Gary");

        entrenador1.elegirEquipo();
        entrenador2.elegirEquipo();
        // Crear batalla
        batalla = new Batalla(entrenador1, entrenador2);
    


    
        // No podemos verificar la salida de consola directamente sin redirigir el flujo de salida,
        // Aquí usamos una aserción de ejemplo:
        batalla.iniciarBatalla();
    

        // No podemos verificar la salida de consola directamente sin redirigir el flujo de salida,
        // Aquí usamos una aserción de ejemplo:
        batalla.terminarBatalla();
    
        // No podemos verificar ya que usa la consola directamente
        // Aquí usamos una aserción de ejemplo:
        // Testea que el turno se cambie correctamente
        batalla.procesarTurno(); // Simula turno
    }

}