using UnityEngine;
public class NPCInteraction : MonoBehaviour
{
    public PuzzleUIManager puzzleManager;
    public int puzzleIndex; // 1, 2, or 3
    private bool hasInteracted = false;

    private bool isPlayerInside = false;
    private bool InteractTextShowing = false;
    private bool completedTextShowing = false;
    private bool ReturnHomeTextShowing = false;
    private bool IntroTextShowing = false;
    private bool introDone = false;

    // private bool hasInteracted = false;

    void Start()
    {
        if (puzzleIndex != 1)
        {
            hasInteracted = SaveManager.IsNPCCompleted(puzzleIndex);
        }
    }

    void Update()
    {
        if (isPlayerInside)
        {
            if (!hasInteracted)
            {
                if (!puzzleManager.isActive && !InteractTextShowing && !completedTextShowing)
                {
                    if (Player.returnHomeCompleted)
                    {
                        puzzleManager.goHomePromptToggle(true);
                    }
                    else if (puzzleIndex == 1 && !introDone)
                    {
                        puzzleManager.IntroTextToggle(true);
                        IntroTextShowing = true;
                    }
                    else if (puzzleIndex == 1 && introDone)
                    {
                        puzzleManager.ReturnHomeTextToggle(true);
                        completedTextShowing = true;
                    }
                    else
                    {
                        puzzleManager.InteractTextToggle(true);
                        InteractTextShowing = true;
                    }
                }

                if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown("/"))
                {
                    if (Player.returnHomeCompleted)
                    {
                        puzzleManager.CompleteReturnHome();
                        return;
                    }

                    puzzleManager.IntroTextToggle(false);
                    puzzleManager.InteractTextToggle(false);
                    puzzleManager.ReturnHomeTextToggle(false);
                    IntroTextShowing = false;
                    InteractTextShowing = false;
                    completedTextShowing = false;
                    introDone = true;
                    Interact();
                }
            }
            else
            {
                if (!completedTextShowing)
                {
                    if (puzzleIndex == 1)
                        puzzleManager.ReturnHomeTextToggle(true);
                    else
                        puzzleManager.PuzzleCompleteTextToggle(true);
                    completedTextShowing = true;
                }
            }
        }
        else
        {
            if (IntroTextShowing)
            {
                puzzleManager.IntroTextToggle(false);
                IntroTextShowing = false;
            }
            if (InteractTextShowing)
            {
                puzzleManager.InteractTextToggle(false);
                InteractTextShowing = false;
            }
            if (completedTextShowing)
            {
                if (puzzleIndex == 1)
                    puzzleManager.ReturnHomeTextToggle(false);
                else
                    puzzleManager.PuzzleCompleteTextToggle(false);
                completedTextShowing = false;
            }
            if (ReturnHomeTextShowing)
            {
                puzzleManager.ReturnHomeTextToggle(false);
                ReturnHomeTextShowing = false;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    { 
        if (other.CompareTag("Player"))
        {
            isPlayerInside = true;
            //Debug.Log("EEEE")
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = false;
        }
    }

    void Interact()
    {
        //Debug.Log("PressE worked");

        if (puzzleIndex == 1)
        {
            puzzleManager.ReturnHomeTextToggle(false);
            puzzleManager.ShowReturnHome();
        }
        else if (puzzleIndex == 2)
        {
            hasInteracted = true;
            puzzleManager.ShowPuzzle2();
        }
        else if (puzzleIndex == 3)
        {
            hasInteracted = true;
            puzzleManager.ShowPuzzle3();
        }
        else if (puzzleIndex == 4)
        {
            hasInteracted = true;
            puzzleManager.ShowPuzzle4();
        }
    }

}