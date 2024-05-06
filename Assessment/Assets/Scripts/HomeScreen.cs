using UnityEngine;

public class HomeScreen : MonoBehaviour
{
    public void ButtonProject(int scene)
    {
        Loading.instance.LoadLevel(scene, 2);
    }
}