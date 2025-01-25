using UnityEngine;

public class BubblePoppingController : MonoBehaviour
{
    public GameController gameController;

    private int rightPoppedBubblesCount = 0;
    private int wrongPoppedBubblesCount = 0;

    private int rightPopsCountInInstruction = 0;

    public int currentInstructionsIndex;
    
    public void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }

    public void SetRightPopsCount(int count)
    {
        rightPopsCountInInstruction = count;
    }

    public void OnBubbleFinishLent()
    {
        if (rightPopsCountInInstruction - 5 > rightPoppedBubblesCount || wrongPoppedBubblesCount > 5)
        {
            gameController.OnFailedPopsFinish();
        }
        else
        {
            gameController.OnSuccessPopsFinish(currentInstructionsIndex);
        }

        gameController.AddTotalScore(rightPoppedBubblesCount);
    }

    public void OnPoppedRight()
    {
        rightPoppedBubblesCount++;
    }

    public void OnPoppedWrong()
    {
        wrongPoppedBubblesCount++;
    }

    public void SetPopSchemeIndex(int currentInstructionsIndex)
    {
        this.currentInstructionsIndex = currentInstructionsIndex;
    }
}