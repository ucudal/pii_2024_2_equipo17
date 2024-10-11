using JetBrains.Annotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProyectoPokemon;

namespace ProyectoPokemon.Tests;

[TestClass]
[TestSubject(typeof(Tipos))]
public class TiposTest
{

[TestMethod]
    public void CreateTiposTests()
    {
        string nombre = "Fuego";

        Tipos fuego = new Tipos(nombre);
        
        Assert.AreEqual(fuego.Nombre, nombre);
    }

    [TestMethod]
    public void AgregarDebilidadesTest()
    {
        Tipos fuego = new Tipos("Fuego");
        Tipos agua = new Tipos("Agua");
        
        fuego.agregarDebilidades(agua);
        
        Assert.IsNotNull(fuego.Debilidades);
    }
    
    [TestMethod]
    public void AgregarFortalezasTest()
    {
        Tipos fuego = new Tipos("Fuego");
        Tipos agua = new Tipos("Agua");
        
        agua.agregarFortalezas(fuego);
        
        Assert.IsNotNull(agua.Fortalezas);
    }
    
    [TestMethod]
    public void EliminarFortalezasTest()
    {
        Tipos fuego = new Tipos("Fuego");
        Tipos agua = new Tipos("Agua");
        
        agua.agregarFortalezas(fuego);
        agua.eliminarFortalezas(fuego);
        Assert.AreEqual(fuego.Fortalezas.Count, 0);
    }
    
    [TestMethod]
    public void EliminarDebilidadesTest()
    {
        Tipos fuego = new Tipos("Fuego");
        Tipos agua = new Tipos("Agua");
        
        fuego.agregarDebilidades(agua);
        fuego.eliminarDebilidades(agua);
        Assert.AreEqual(fuego.Debilidades.Count, 0);
    }

    [TestMethod]
    public void multiplicadorDañoDebilidad()
    {
        Tipos fuego = new Tipos("Fuego");
        Tipos agua = new Tipos("Agua");
        
        fuego.agregarDebilidades(agua);
        
        Assert.AreEqual(fuego.multiplicadorDaño(agua), 2.0);
    }
    
    [TestMethod]
    public void multiplicadorDañoFortaleza()
    {
        Tipos fuego = new Tipos("Fuego");
        Tipos agua = new Tipos("Agua");

        fuego.agregarFortalezas(agua);
        
        Assert.AreEqual(fuego.multiplicadorDaño(agua), 0.5);
    }
    
    [TestMethod]
    public void multiplicadorDañoNoraml()
    {
        Tipos fuego = new Tipos("Fuego");
        Tipos agua = new Tipos("Agua");

        Assert.AreEqual(fuego.multiplicadorDaño(agua), 1.0);
    }
}