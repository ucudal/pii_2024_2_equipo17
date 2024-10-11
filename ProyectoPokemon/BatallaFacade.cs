namespace ProyectoPokemon
{
    public class BatallaFacade
    {
        private Batalla batalla;

        public BatallaFacade(Entrenador jugador, Entrenador oponente)
        {
            batalla = new Batalla(jugador, oponente);
        }

        public void iniciarBatalla()
        {
            batalla.iniciarBatalla();
        }

        public void ataqueJugador(int indiceAtaque)
        {
            batalla.procesarTurno();
        }

        public void mostrarEstadoPokemones()
        {
            Console.WriteLine($"{batalla.Jugador.Activo.Nombre} tiene {batalla.Jugador.Activo.Vida} de vida restante.");
            Console.WriteLine($"{batalla.JugadorOponente.Activo.Nombre} tiene {batalla.JugadorOponente.Activo.Vida} de vida restante.");
        }

        public void finalizarBatalla()
        {
            batalla.terminarBatalla();
        }
        public void cambiarPokemonActivo(int indexPokemon)
        {
            if (indexPokemon >= 0 && indexPokemon < batalla.Jugador.Equipo.Count)
            {
                batalla.Jugador.cambiarActivo(indexPokemon);
                Console.WriteLine($"{batalla.Jugador.Nombre} ha cambiado a {batalla.Jugador.Activo.Nombre} como Pokémon activo.");
            }
            else
            {
                Console.WriteLine("¡Índice de Pokémon no válido!");
            }
        }
        public void procesarTurno(int ataqueIndex)
        {
            if (batalla.Turno)
            {
                if (!batalla.Jugador.Activo.EstaDerrotado)
                {
                    Console.WriteLine($"{batalla.Jugador.Nombre}, es tu turno.");
                    batalla.Jugador.Activo.mostrarAtaques();
                    ataqueIndex = int.Parse(Console.ReadLine());
                    batalla.Jugador.elegirAtaque(ataqueIndex, batalla.JugadorOponente.Activo);
                }
            }
            else
            {
                if (!batalla.JugadorOponente.Activo.EstaDerrotado)
                {
                    Console.WriteLine($"{batalla.JugadorOponente.Nombre}, es tu turno.");
                    batalla.JugadorOponente.Activo.mostrarAtaques();
                    ataqueIndex = int.Parse(Console.ReadLine());
                    batalla.JugadorOponente.elegirAtaque(ataqueIndex, batalla.Jugador.Activo);
                }
            }
    
            // Alternar turno
            batalla.Turno = !batalla.Turno;
        }



    }
}