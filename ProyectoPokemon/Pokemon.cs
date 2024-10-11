using System.Collections;
using System.Runtime.CompilerServices;

namespace ProyectoPokemon;

public class Pokemon : IPorTurnos
{
    private string nombre;
    private int vida;
    private Ataque ataqueEspecial;
    private List<Ataque> ataques;
    private List<Tipos> tipos;
    private bool estaDerrotado;

    public string Nombre
    {
        get { return nombre; }
        set { nombre = value; }
    }

    public int Vida
    {
        get { return vida; }
        set { vida = value; }
    }

    public Ataque AtaqueEspecial
    {
        get { return ataqueEspecial; }
        set { ataqueEspecial = value; }
    }

    public List<Ataque> Ataques
    {
        get { return ataques; }
        set { ataques = value; }
    }

    public List<Tipos> Tipos
    {
        get { return tipos; }
        set { tipos = value; }
    }

    public bool EstaDerrotado
    {
        get { return estaDerrotado; }
        set { estaDerrotado = value; }
    }

    public Pokemon(string nombre, int vida, Ataque especial, List<Ataque> ataques, List<Tipos> tipos)
    {
        this.Nombre = nombre;
        this.Vida = vida;
        this.ataqueEspecial = especial;
        this.Ataques = ataques;
        this.Tipos = tipos;
        this.EstaDerrotado = false;
        
        Pokedex.agregarPokemon(this);
    }

    public void recibirDaño(int daño)
    {
        if (!this.EstaDerrotado)
        {
            this.Vida -= daño;
            if (this.Vida <= 0)
            {
                this.EstaDerrotado = true;
                this.Vida = 0;
                Console.WriteLine($"{this.Nombre} a sido derrotado");
            }
        }
        else
        {
            Console.WriteLine($"{this.Nombre} no puede recibir daño por que ya a sido derrotado");
        }
    }

    public void atacar(Pokemon oponente, Ataque ataque)
    {
        int dañoTotal;
        double multiplicador = 1.0;
        
        if (!EstaDerrotado)
        {
            foreach (var tipoAtaque in ataque.Tipos)
            {
                foreach (var tipoOponente in oponente.Tipos)
                {
                    multiplicador = tipoOponente.multiplicadorDaño(tipoAtaque);
                }
            }

            dañoTotal = (int)(ataque.Daño * multiplicador);
            oponente.recibirDaño(dañoTotal);
            Console.WriteLine($"{this.Nombre} ataca a {oponente.nombre} usando {ataque.Nombre}, causando {dañoTotal} de daño");
        }
        else
        {
            Console.WriteLine($"{this.Nombre} no puede atacar por que esta derrotado");
        }
    }

    public void comenzarTurno()
    {
        if (!EstaDerrotado)
        {
            Console.WriteLine($"{this.Nombre} comienza su turno.");
        }
        else
        {
            Console.WriteLine($"{this.Nombre} está derrotado y no puede comenzar su turno.");
        }
    }
    
    public void finalizarTurno()
    {
        if (!EstaDerrotado)
        {
            Console.WriteLine($"{this.Nombre} finaliza su turno.");
        }
    }
    
    public void mostrarAtaques()
    {
        int numero = 0;
        Console.WriteLine("Lista de ataques del Pokemon");
        foreach (var ataque in this.Ataques)
        {
            Console.WriteLine($"{numero} - {ataque.Nombre}");
            numero += 1;
        }
        Console.WriteLine($"{numero} - {this.AtaqueEspecial.Nombre}");
    }

    public void mostrarVida()
    {
        Console.WriteLine($"{this.Nombre} : { this.Vida }HP");
    }
}