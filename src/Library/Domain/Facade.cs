
using Library;

namespace Ucu.Poo.DiscordBot.Domain;

/// <summary>
/// Esta clase recibe las acciones y devuelve los resultados que permiten
/// implementar las historias de usuario. Otras clases que implementan el bot
/// usan esta <see cref="Facade"/> pero no conocen el resto de las clases del
/// dominio. Esta clase es un singleton.
/// </summary>
public class Facade
{
    private static Facade ? _instance;

    // Este constructor privado impide que otras clases puedan crear instancias
    // de esta.
    private Facade()
    {
        WaitingList = new WaitingList();
        BattlesList = new BattlesList();
    }

    /// <summary>
    /// Obtiene la única instancia de la clase <see cref="Facade"/>.
    /// </summary>
    public static Facade Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new Facade();
            }

            return _instance;
        }
    }

    /// <summary>
    /// Inicializa este singleton. Es necesario solo en los tests.
    /// </summary>
    public static void Reset()
    {
        _instance = null;
    }
    
    private WaitingList WaitingList { get; }
    
    private BattlesList BattlesList { get; }

    /// <summary>
    /// Agrega un jugador a la lista de espera.
    /// </summary>
    /// <param name="displayName">El nombre del jugador.</param>
    /// <returns>Un mensaje con el resultado.</returns>
    public string AddTrainerToWaitingList(string displayName)
    {
        Trainer? player = this.BattlesList.FindTrainerByDisplayName(displayName);
        Battle? battle = this.BattlesList.FindBattleByDisplayName(displayName);
        
        if (battle == null)
        {
            if (this.WaitingList.AddTrainer(displayName))
            {
                return $"{displayName} agregado a la lista de espera";
            }
        
            return $"{displayName} ya está en la lista de espera";   
        }

        return $"{displayName} ya esta en una batalla";
    }

    /// <summary>
    /// Remueve un jugador de la lista de espera.
    /// </summary>
    /// <param name="displayName">El jugador a remover.</param>
    /// <returns>Un mensaje con el resultado.</returns>
    public string RemoveTrainerFromWaitingList(string displayName)
    {
        if (WaitingList.RemoveTrainer(displayName))
        {
            return $"{displayName} removido de la lista de espera";
        }

        return $"{displayName} no está en la lista de espera";
    }

    /// <summary>
    /// Obtiene la lista de jugadores esperando.
    /// </summary>
    /// <returns>Un mensaje con el resultado.</returns>
    public string GetAllTrainersWaiting()
    {
        if (WaitingList.Count == 0)
        {
            return "No hay nadie esperando";
        }

        string result = "Esperan: ";
        foreach (Trainer trainer in WaitingList.GetAllWaiting())
        {
            result = result + trainer.Name + "; ";
        }
        
        return result;
    }

    /// <summary>
    /// Determina si un jugador está esperando para jugar.
    /// </summary>
    /// <param name="displayName">El jugador.</param>
    /// <returns>Un mensaje con el resultado.</returns>
    public string TrainerIsWaiting(string displayName)
    {
        Trainer? trainer = WaitingList.FindTrainerByDisplayName(displayName);
        if (trainer == null)
        {
            return $"{displayName} no está esperando";
        }
        
        return $"{displayName} está esperando";
    }


    private string CreateBattle(string playerDisplayName, string opponentDisplayName)
    {
        // Aunque playerDisplayName y opponentDisplayName no estén en la lista
        // esperando para jugar los removemos igual para evitar preguntar si
        // están para luego removerlos.
        Trainer? player = WaitingList.FindTrainerByDisplayName(playerDisplayName);
        Trainer? opponent = WaitingList.FindTrainerByDisplayName(opponentDisplayName);

        if (player == null || opponent == null)
        {
            return $"{(player == null ? playerDisplayName : opponentDisplayName)} no está en la lista de espera";
        }

        // Remover jugadores de la lista de espera
        WaitingList.RemoveTrainer(playerDisplayName);
        WaitingList.RemoveTrainer(opponentDisplayName);

        string actual = null;
        int turnoRandom = new Random().Next(0, 2);
        
        switch (turnoRandom)
        {
            case 0:
                BattlesList.AddBattle(player, opponent);
                actual = $"Empieza {player.Name}";
                break;
            case 1:
                BattlesList.AddBattle(opponent, player);
                actual = $"Empieza {opponent.Name}";
                break;
        }

        return $"Comienza {player.Name} vs {opponent.Name}\n" +
               $"{actual}";
    }

    /// <summary>
    /// Crea una batalla entre dos jugadores.
    /// </summary>
    /// <param name="playerDisplayName">El primer jugador.</param>
    /// <param name="opponentDisplayName">El oponente.</param>
    /// <returns>Un mensaje con el resultado.</returns>
    public string StartBattle(string playerDisplayName, string? opponentDisplayName)
    {
        // El símbolo ? luego de Trainer indica que la variable opponent puede
        // referenciar una instancia de Trainer o ser null.
        Trainer? opponent;
        
        if (!OpponentProvided() && !SomebodyIsWaiting())
        {
            return "No hay nadie esperando";
        }
        
        if (!OpponentProvided()) // && SomebodyIsWaiting
        {
            opponent = WaitingList.GetAnyoneWaiting();
            
            // El símbolo ! luego de opponent indica que sabemos que esa
            // variable no es null. Estamos seguros porque SomebodyIsWaiting
            // retorna true si y solo si hay usuarios esperando y en tal caso
            // GetAnyoneWaiting nunca retorna null.
            return CreateBattle(playerDisplayName, opponentDisplayName);
        }

        // El símbolo ! luego de opponentDisplayName indica que sabemos que esa
        // variable no es null. Estamos seguros porque OpponentProvided hubiera
        // retorna false antes y no habríamos llegado hasta aquí.
        opponent = WaitingList.FindTrainerByDisplayName(opponentDisplayName!);
        
        if (!OpponentFound())
        {
            return $"{opponentDisplayName} no está esperando";
        }
        
        return CreateBattle(playerDisplayName, opponentDisplayName);
        
        // Funciones locales a continuación para mejorar la legibilidad

        bool OpponentProvided()
        {
            return !string.IsNullOrEmpty(opponentDisplayName);
        }

        bool SomebodyIsWaiting()
        {
            return WaitingList.Count != 0;
        }

        bool OpponentFound()
        {
            return opponent != null;
        }
    }

    /// <summary>
    /// Muestra todos los Pokémon disponibles en la Pokédex del juego.
    /// </summary>
    /// <returns>Una cadena con la lista de Pokémon disponibles.</returns>
    public string ShowPokémonAvailable()
    {
        List<string> pokedexLists = Pokedex.ShowPokedex();
        string value = string.Join("\n", pokedexLists);
        return $"Pokemones Disponibles: \n{value}";
    }

    /// <summary>
    /// Muestra los Pokémon del equipo del jugador especificado.
    /// </summary>
    /// <param name="playerDisplayName">El nombre del jugador.</param>
    /// <returns>Una cadena con la lista de Pokémon del jugador.</returns>
    public string ShowEnemiesPokemon(string playerDisplayName)
    {
        string value = "Pokemon:\n";
        Trainer? player = BattlesList.FindTrainerByDisplayName(playerDisplayName);
        List<Pokemon> pokemones = player.Team;
        foreach (var VARIABLE in pokemones)
        {
            value += "\n" + VARIABLE.Name + "Vida: " + VARIABLE.Health + "/100";
        }

        return value;
    }

    /// <summary>
    /// Permite al jugador elegir un equipo de Pokémon para una batalla.
    /// </summary>
    /// <param name="playerDisplayName">El nombre del jugador.</param>
    /// <param name="number">El índice del Pokémon en la Pokédex.</param>
    /// <returns>Un mensaje indicando el Pokémon elegido.</returns>
    public string ChooseTeam(string playerDisplayName, int number)
    {
        Trainer? player = BattlesList.FindTrainerByDisplayName(playerDisplayName);
        return player.ChooseTeam(number);
    }

    /// <summary>
    /// Permite al jugador usar un ítem durante una batalla.
    /// </summary>
    /// <param name="playerDisplayName">El nombre del jugador.</param>
    /// <param name="opcionPokemon">La opción del Pokémon en el equipo.</param>
    /// <param name="item">El ítem a usar.</param>
    /// <returns>El resultado de usar el ítem.</returns>
    public string UseItem(string playerDisplayName, int opcionPokemon, string item)
    {
        Battle? battle = BattlesList.FindBattleByDisplayName(playerDisplayName);
        
        if (ValidationTurn(playerDisplayName, battle))
        {
            return "No es tu turno ESPERA!";
        }
        
        return battle.IntermediaryUseItem(opcionPokemon, item);
    }

    /// <summary>
    /// Permite al jugador atacar con un Pokémon durante una batalla.
    /// </summary>
    /// <param name="playerDisplayName">El nombre del jugador.</param>
    /// <param name="opcionAtaque">El ataque a realizar.</param>
    /// <returns>El resultado del ataque.</returns>
    public string AttackPokemon(string playerDisplayName, string opcionAtaque)
    {
        Battle? battle = BattlesList.FindBattleByDisplayName(playerDisplayName);
        
        if (ValidationTurn(playerDisplayName, battle))
        {
            return "No es tu turno ESPERA!";
        }
        
        return battle.IntermediaryAttack(opcionAtaque);
    }

    /// <summary>
    /// Permite al jugador cambiar de Pokémon activo durante una batalla.
    /// </summary>
    /// <param name="playerDisplayName">El nombre del jugador.</param>
    /// <param name="opcion">La opción del Pokémon en el equipo.</param>
    /// <returns>El resultado del cambio de Pokémon.</returns>
    public string ChangePokemon(string playerDisplayName, int opcion)
    {
        Battle? battle = BattlesList.FindBattleByDisplayName(playerDisplayName);
        
        if (ValidationTurn(playerDisplayName, battle))
        {
            return "No es tu turno ESPERA!";
        }
        
        return battle.IntermediaryChangeActivePokemon(opcion);
    }

    /// <summary>
    /// Obtiene los ataques disponibles del Pokémon activo del jugador.
    /// </summary>
    /// <param name="playerDisplayName">El nombre del jugador.</param>
    /// <returns>Una cadena con la lista de ataques del Pokémon activo.</returns>
    public string GetPokemonAtacks(string playerDisplayName)
    {
        Trainer? player = BattlesList.FindTrainerByDisplayName(playerDisplayName);
        if (player == null)
        {
            return "Entrenador no encontrado.";
        }
        
        Pokemon activo = player.Active;

        string result = "Ataques:\n";

        foreach (var ataque in activo.Attacks)
        {
            var (dañoAtaque, tipoAtaque) = Attack.ObtainAttack(ataque);

            result += $"{ataque}: Tipo = {tipoAtaque}, Daño = {dañoAtaque}\n";
        }

        return result;
    }

    public string Surrender(string playerDisplayName)
    {
        Battle? battle = this.BattlesList.FindBattleByDisplayName(playerDisplayName);
        this.BattlesList.removeBatlle(battle);
        return $"{playerDisplayName} se a rendido. Termino la Batalla";
        
    }
    
    public string Win(string playerDisplayName)
    {
        Battle? battle = this.BattlesList.FindBattleByDisplayName(playerDisplayName);
        this.BattlesList.removeBatlle(battle);
        return $"{playerDisplayName} a ganado!. Termino la Batalla!";
        
    }

    /// <summary>
    /// Valida si es el turno del jugador durante una batalla.
    /// </summary>
    /// <param name="playerDisplayName">El nombre del jugador.</param>
    /// <param name="batt">La batalla en curso.</param>
    /// <returns>True si es el turno del jugador, False de lo contrario.</returns>
    public bool ValidationTurn(string playerDisplayName, Battle batt)
    {
        Trainer? player = BattlesList.FindTrainerByDisplayName(playerDisplayName);
        if (player.Name != batt.ActualTurn.Name)
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// Cambia el turno en caso del que el jugador lo decida.
    /// </summary>
    /// <returns>Turno Cambiado. Es el Turno de X, en caso contrario No es tu turno</returns>
    public string ChangeTurn(string playerDisplayName)
    {
        Battle? battle = this.BattlesList.FindBattleByDisplayName(playerDisplayName);
        if (!(ValidationTurn(playerDisplayName, battle)))
        {
            battle.CambiarTurno();
            return $"Turno cambiado. Es el turno de {battle.ActualTurn.Name}";
        }

        return "No es tu turno";
    }
}
