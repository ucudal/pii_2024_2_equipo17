using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProyectoPokemon;

namespace ProyectoPokemon.Tests;

[TestClass]
[TestSubject(typeof(Pokedex))]
public class PokedexTest
{

    [TestMethod]
    public void AgregarPokemon()
    {
        var wobbuffet = new Pokemon("Wobbuffet", 100,
            new Ataque("SombraTrampa", 10, new List<Tipos> { new Tipos("Psiquico") }, false), new List<Ataque>(),
            new List<Tipos>());
        
        Pokedex.agregarPokemon(wobbuffet);

        Assert.IsTrue(Pokedex.ListaPokemons.Contains(wobbuffet));
    }

    public void ObtenerPokemonPorIndice()
    {
        var pikachu = new Pokemon("Pikachu", 100,
            new Ataque("Impactrueno", 50, new List<Tipos> { new Tipos("Electrico") }, false), new List<Ataque>(),
            new List<Tipos>());
        Pokedex.agregarPokemon(pikachu);

        var resultado = Pokedex.obtenerPokemonPorIndice(0);


        Assert.Equals(pikachu, resultado);
    }

    public void EliminarPokemon()
    {
        var squirtle = new Pokemon("Squirtle", 90,
            new Ataque("Pistola de Agua", 40, new List<Tipos> { new Tipos("Agua") }, false), new List<Ataque>(),
            new List<Tipos>());
        Pokedex.agregarPokemon(squirtle);
        
        Pokedex.eliminarPokemon(squirtle);
        
        Assert.IsFalse(Pokedex.ListaPokemons.Contains(squirtle));
    }
}