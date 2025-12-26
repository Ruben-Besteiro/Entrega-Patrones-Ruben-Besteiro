using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    List<IObserver> observers = new List<IObserver>();
    float counter = 2f;
    [SerializeField] TextMeshProUGUI timeText;
    private bool textIsRed = false;
    public bool enemiesChasing = false;
    public static GameManager Instance;

    void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
    }

    void Start()
    {
        timeText.color = Color.green;
        StartCoroutine(IECounter());
    }

    // Los observadores se añaden y se quitan ellos solos
    public void Subscribe(IObserver observer)
    {
        if (!observers.Contains(observer))
        {
            observers.Add(observer);
        }
    }

    public void Unsubscribe(IObserver observer)
    {
        observers.Remove(observer);
    }

    IEnumerator IECounter()
    {
        bool lookAtPlayer = false;

        while (true)
        {
            while (counter > 0)
            {
                if (!enemiesChasing)
                {
                    counter -= Time.deltaTime;
                    timeText.text = counter.ToString();
                }
                yield return null;
            }

            lookAtPlayer = !lookAtPlayer;
            NotifyObservers(lookAtPlayer);

            textIsRed = !textIsRed;
            timeText.color = textIsRed ? Color.red : Color.green;
            counter = 2;
        }
    }

    // Cuando el contador llega a 0 el GameManager notifica a los observadores para que alternen entre mirar a la pared y al jugador
    private void NotifyObservers(bool seeking)
    {
        foreach (IObserver observer in observers)
        {
            observer.OnNotify(seeking);
        }
    }
}
