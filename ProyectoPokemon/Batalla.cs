namespace ProyectoPokemon;

public class Batalla
{
    private Entrenador jugador;
    private Entrenador jugadorOponente;
    private bool turno;
    public Entrenador Jugador
    {
        get { return jugador; }
        set { jugador = value; }
    }

    public Entrenador JugadorOponente
    {
        get { return jugadorOponente; }
        set { jugadorOponente = value; }
    }

    public bool Turno
    {
        get { return turno; }
        set { turno = value; }
    }

    public Batalla(Entrenador jugador, Entrenador oponente)
    {
        this.Jugador = jugador;
        this.JugadorOponente = oponente;
        this.Turno = true;
    }
    
    public void iniciarBatalla() 
    { 
        Console.WriteLine("¡Comienza la batalla!"); 
        this.Jugador.comenzarTurno(); 
        this.JugadorOponente.comenzarTurno();
    } 
    
    public void terminarBatalla() 
    { 
        Console.WriteLine("¡La batalla ha terminado!"); 
    }

    public void procesarTurno()
    {
        if (this.Turno) // Turno del jugador
        {
            if (!this.Jugador.Activo.EstaDerrotado)
            {
                this.Jugador.Activo.mostrarAtaques();
                Console.WriteLine("Elige un ataque o presiona -1 para cambiar de Pokémon:");
                int ataqueIndex = int.Parse(Console.ReadLine());
            
                if (ataqueIndex == -1)
                {
                    Console.WriteLine("Selecciona el índice del Pokémon que deseas activar:");
                    int indexPokemon = int.Parse(Console.ReadLine());
                    this.Jugador.cambiarActivo(indexPokemon);
                    Console.WriteLine($"{this.Jugador.Nombre} ha cambiado a {this.Jugador.Activo.Nombre} como Pokémon activo.");
                }
                else
                {
                    this.Jugador.elegirAtaque(ataqueIndex, this.JugadorOponente.Activo);
                }
            }
        }
        else // Turno del oponente
        {
            if (!this.JugadorOponente.Activo.EstaDerrotado)
            {
                this.JugadorOponente.Activo.mostrarAtaques();
                Console.WriteLine("Elige un ataque o presiona -1 para cambiar de Pokémon:");
                int ataqueIndex = int.Parse(Console.ReadLine());
            
                if (ataqueIndex == -1)
                {
                    Console.WriteLine("Selecciona el índice del Pokémon que deseas activar:");
                    int indexPokemon = int.Parse(Console.ReadLine());
                    this.JugadorOponente.cambiarActivo(indexPokemon);
                    Console.WriteLine($"{this.JugadorOponente.Nombre} ha cambiado a {this.JugadorOponente.Activo.Nombre} como Pokémon activo.");
                }
                else
                {
                    this.JugadorOponente.elegirAtaque(ataqueIndex, this.Jugador.Activo);
                }
            }
        }

        // Cambiar el turno
        this.Turno = !this.Turno; // Alterna el turno
    }

}