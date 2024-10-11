using System.Collections.Generic;
using JetBrains.Annotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProyectoPokemon;

namespace ProyectoPokemon.Tests;

[TestClass]
[TestSubject(typeof(Pokemon))]
public class PokemonTest
{

    [TestMethod]
    public void TestInicializarPokemon()
    {
        List<Ataque> ataques = new List<Ataque>();
        List<Tipos> tipos = new List<Tipos>();
        Ataque especial = new Ataque("Lanzallamas", 40, tipos, true);

        Pokemon charmander = new Pokemon("Charmander", 100, especial, ataques, tipos);

        Assert.AreEqual("Charmander", charmander.Nombre);
        Assert.AreEqual(100,charmander.Vida);
        Assert.AreEqual(especial,charmander.AtaqueEspecial);
        Assert.IsFalse(charmander.EstaDerrotado);
    }

    [TestMethod]
    public void TestRecibeDaño()
    {
        Pokemon charmander = new Pokemon("Charmander", 100, null, new List<Ataque>(), new List<Tipos>());
        charmander.recibirDaño(30);
        
        Assert.AreEqual(70, charmander.Vida);
        Assert.IsFalse(charmander.EstaDerrotado);
    }

    [TestMethod]

    public void TestMuerte()
    {
        Pokemon charmander = new Pokemon("Charmander", 100, null, new List<Ataque>(), new List<Tipos>());
        charmander.recibirDaño(150);
        
        Assert.IsTrue(charmander.EstaDerrotado);
        Assert.AreEqual(0,charmander.Vida);
        
    }

    [TestMethod]
    public void DañoDerrotado()
    {
        Pokemon charmander = new Pokemon("Charmander", 100, null, new List<Ataque>(), new List<Tipos>());
        charmander.recibirDaño(150);
        charmander.recibirDaño(50);
        
        Assert.AreEqual(0,charmander.Vida);
    }
    
    [TestMethod]
    public void TestAtacarPokemon()
    {
        List<Tipos> tiposAtaque = new List<Tipos>();
        List<Ataque> ataques = new List<Ataque>();
        Ataque especial = new Ataque("Lanzallamas", 50, tiposAtaque, true);

        Pokemon charmander = new Pokemon("Charmander", 100, null, new List<Ataque>(), new List<Tipos>());
        Pokemon squirtle = new Pokemon("Squirtle", 100, null, new List<Ataque>(), new List<Tipos>());

        charmander.atacar(squirtle, especial);
        
        Assert.AreEqual(50, squirtle.Vida);
    }

    [TestMethod]
    public void TestMostrarVida()
    {
        Pokemon charmander = new Pokemon("Charmander", 100, null, new List<Ataque>(), new List<Tipos>());
        charmander.mostrarVida();

    }
    
}