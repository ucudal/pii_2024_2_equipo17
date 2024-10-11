using System.Collections.Generic;
using JetBrains.Annotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProyectoPokemon;

namespace ProyectoPokemon.Tests;

[TestClass]
[TestSubject(typeof(Ataque))]
public class AtaqueTest
{

    [TestMethod]
    public void CreateAtaque()
    {
        string nombre = "LanzaLlamas";
        int daño = 40;
        List<Tipos> tipo = new List<Tipos>();
        bool especial = false;

        Ataque lanzaLlamas = new Ataque(nombre, daño, tipo, especial);
        
        Assert.AreEqual(lanzaLlamas.Nombre, nombre);
        Assert.AreEqual(lanzaLlamas.Daño, daño);
    }

    [TestMethod]
    public void AgregarTipoAtaque()
    {
        string nombre = "LanzaLlamas";
        int daño = 40;
        List<Tipos> tipo = new List<Tipos>();
        bool especial = false;

        Ataque lanzaLlamas = new Ataque(nombre, daño, tipo, especial);
        Tipos tipos = null;
        
        lanzaLlamas.agregarTipo(tipos);
        
        Assert.AreEqual(lanzaLlamas.Tipos.Count, 1);
    }
}