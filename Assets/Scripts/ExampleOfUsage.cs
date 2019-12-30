using UnityEngine;

public class ExampleOfUsage : MonoBehaviour
{
    public WorldGenerator WG;
    public Animator LoadScreen;

    //Start generation
    void Start()
    {
        WG.Generate();
    }

    //Event
    public void HideLoadScreen()
    {
        LoadScreen.SetTrigger("FadeOut");
    }
}
