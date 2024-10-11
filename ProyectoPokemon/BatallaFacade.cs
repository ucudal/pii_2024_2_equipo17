namespace ProyectoPokemon
{
    public class BatallaFacade
    {
        private Tipos tipos;

        public Tipos Tipos(string nombre)
        {
            tipos = Tipos(nombre);
        }
        private Entrenador entrenador;

        public Entrenador Entrenador(string nombre)
        {
            entrenador = Entrenador(nombre);
        }

        private Ataque ataque;

        public Ataque Ataque(string nombre, int daño, List<Tipos> tipo, bool especial)
        {
            ataque = new Ataque(nombre, daño, tipo, especial);
        }
        private Batalla batalla;

        public Batalla Batalla(Entrenador jugador, Entrenador oponente)
        {
            batalla = new Batalla(jugador, oponente);
        }

        public void iniciarBatalla()
        {
            batalla.iniciarBatalla();
        }
        public void procesarTurno()
        {
            batalla.procesarTurno;
        }
        public void finalizarBatalla()
        {
            batalla.terminarBatalla();
        }
        public void agregarTipo(Tipos tipo)
        {
            ataque.agregarTipo();
        }

        public void elegirEquipo()
        {
            entrenador.elegirEquipo;
        }

        public void cambiarActivo(int indexPokemonList)
        {
            entrenador.cambiarActivo(indexPokemonList);
        }

        public void elegirAtaque(int indexAtaque, Pokemon oponente)
        {
            entrenador.elegirAtaque(indexAtaque, oponente);
        }

        public void comenzarTurno()
        {
            entrenador.comenzarTurno();
        }

        public void finalizarTurno()
        {
            entrenador.finalizarTurno();
        }

        public void agregarDebilidades(Tipos tipo)
        {
            tipos.agregarDebilidades(tipo);
        }

        public void agregarFortalezas(Tipos tipo)
        {
            tipos.agregarFortalezas(tipo);
        }

        public void eliminarDebilidades(Tipos tipo)
        {
            tipos.eliminarDebilidades(tipo);
        }

        public void eliminarFortalezas(Tipos tipo)
        {
            tipos.eliminarFortalezas(tipo);
        }

        public double multiplicadorDaño(Tipos tipoAtaque)
        {
            tipos.multiplicadorDaño(tipoAtaque);
        }

















    }
}