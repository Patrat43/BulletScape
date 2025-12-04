using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerID ID = new PlayerID();

    private void Awake()
    {
        if (ID.events == null)
            ID.events = new PlayerEvents();
    }
}
