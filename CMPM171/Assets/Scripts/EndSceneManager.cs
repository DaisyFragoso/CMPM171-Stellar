using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSceneManager : MonoBehaviour
{
    private Animator animator;
    public GameObject endSceneAnimatorObject;
    public GameObject endSceneCard;
    
    void Start()
    {
        animator = endSceneAnimatorObject.GetComponent<Animator>();
    }
    void Update()
    {
        if(this.animator.GetCurrentAnimatorStateInfo(0).IsName("endSceneAnim") )
        {
            bool animationDone = animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f;

            if(animationDone)
            {
                endSceneAnimatorObject.SetActive(false);
                endSceneCard.SetActive(true);
            }
        }
    }

    public void MenuButton()
    {
        SceneManager.LoadSceneAsync(0);
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
