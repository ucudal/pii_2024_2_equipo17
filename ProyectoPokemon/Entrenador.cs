namespace ProyectoPokemon;

public class Entrenador : IPorTurnos
{
    private string nombre;
    private List<Pokemon> equipo;
    private Pokemon activo;

    public string Nombre
    {
        get { return nombre; }
        set { nombre = value; }
    }

    public List<Pokemon> Equipo
    {
        get { return equipo; }
        set { equipo = value; }
    }

    public Pokemon Activo
    {
        get { return activo; }
        set { activo = value; }
    }

    public Entrenador(string nombre)
    {
        this.Nombre = nombre;
        this.Equipo = new List<Pokemon>();
        this.Activo = null;
    }

    public void elegirEquipo()
    {
        while (this.Equipo.Count < 2)
        {
            Pokedex.mostrarPokedex();
            Console.WriteLine("Selecciona el numero del pokemon que deseas agregar a tu equipo");
            int numero = int.Parse(Console.ReadLine());

            Pokemon seleccion = Pokedex.obtenerPokemonPorIndice(numero);

            if (seleccion != null)
            {
                if (!this.Equipo.Contains(seleccion))
                {
                    this.Equipo.Add(seleccion);
                    Console.WriteLine($"{seleccion.Nombre} ha sido añadido a tu equipo.");
                }
                else
                {
                    Console.WriteLine("El pokemon ya esta en tu equipo");
                }
            }

            if (this.Equipo.Count == 1)
            {
                this.Activo = seleccion;
            }

            if (this.Equipo.Count == 6)
            {
                Console.WriteLine("Ya tienes 6 pokemones en tu equipo");
            }
        }
    }

    public void cambiarActivo(int indexPokemonList)
    {
        if (indexPokemonList >= 0 && indexPokemonList < this.Equipo.Count)
        {
            this.Activo = this.Equipo[indexPokemonList];
        }
        else
        {
            Console.WriteLine("Indice no valido. No se pudo cambiar el pokemon");
        }
    }

    public void elegirAtaque(int indexAtaque, Pokemon oponente)
    {
        Ataque ataqueElegido = null;
        if (indexAtaque == 3)
        {
            ataqueElegido = this.activo.AtaqueEspecial;
        }
        else
        {
            ataqueElegido = this.activo.Ataques[indexAtaque];
        }
        
        this.activo.atacar(oponente, ataqueElegido);
        }
    
    public void comenzarTurno()
    {
            Console.WriteLine($"{this.Nombre} comienza su turno.");
    }
    
    public void finalizarTurno()
    {
            Console.WriteLine($"{this.Nombre} finaliza su turno.");
    }
}