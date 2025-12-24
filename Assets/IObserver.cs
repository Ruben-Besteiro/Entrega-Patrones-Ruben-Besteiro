using UnityEngine;

public interface IObserver
{
    // Cada vez que el contador de arriba a la izquierda llega a 0, los observadores alternan entre mirar al jugador y a la pared
    void OnNotify(bool seeking);
}
