using UnityEngine;

public class GameController: MonoBehaviour
{
    public int score = 0;

    public LampState lampState;

    public void AddTotalScore(int score)
    {
        this.score += score;
    }

    public void OnFailedPopsFinish()
    {
        lampState.BlinkRed();
    }

    public void OnSuccessPopsFinish()
    {
        lampState.BlinkGreen();
    }
}