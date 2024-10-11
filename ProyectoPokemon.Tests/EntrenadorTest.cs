using System.Collections.Generic;
using JetBrains.Annotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProyectoPokemon;

namespace ProyectoPokemon.Tests;

[TestClass]
[TestSubject(typeof(Entrenador))]
public class EntrenadorTest
{

    [TestMethod]
    public void ElegirEquipo()
    {
        var entrenador = new Entrenador("Jessie");
        var wobbuffet = new Pokemon("Wobbuffet", 100,
            new Ataque("SombraTrampa", 10, new List<Tipos> { new Tipos("Psiquico") }, false), new List<Ataque>(),
            new List<Tipos>());
        Pokedex.agregarPokemon(wobbuffet);

        int indice = 0;
        
        //entrenador.elegirEquipo();
        //Aqui no podemos validar el test ya que esta esperando todo el tiempo a que eligamos el equipo modiante un console readline
        //Dejamos la logica que seguiria el test.
        //Para comprobar su funcionamiento tambien se puede verificar dandole run al program
        
    }

    public void CambiarActivo()
    {
        var entrenador = new Entrenador("Jessie");
        var wobbuffet = new Pokemon("Wobbuffet", 100,
            new Ataque("SombraTrampa", 10, new List<Tipos> { new Tipos("Psiquico") }, false), new List<Ataque>(),
            new List<Tipos>());
        var pikachu = new Pokemon("Pikachu", 100,
            new Ataque("Impactrueno", 50, new List<Tipos> { new Tipos("Electrico") }, false), new List<Ataque>(),
            new List<Tipos>());
        
        entrenador.Equipo.Add(wobbuffet);
        entrenador.Equipo.Add(pikachu);
        entrenador.Activo = pikachu;
        
        entrenador.cambiarActivo(1);

        Assert.Equals(pikachu, entrenador.Activo);
    }
}