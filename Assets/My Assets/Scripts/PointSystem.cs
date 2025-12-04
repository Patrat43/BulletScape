using UnityEngine;

public class PointSystem : PlayerSystem
{

    private int points = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     
    }

    public void AddPoints(int amount)
    {
        points += amount;
        Debug.Log("Points added! Total points: " + points);
        player.ID.events.OnPointsChange?.Invoke();
    }

    public int GetPoints()
    {
        return points;
    }

}
