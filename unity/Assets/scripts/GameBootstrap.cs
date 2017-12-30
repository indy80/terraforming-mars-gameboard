using UnityEngine;

public class GameBootstrap : MonoBehaviour
{
    [SerializeField] private GameView gameView;

    public void Start()
    {
        var game = new Game(this.gameView);
    }
}

