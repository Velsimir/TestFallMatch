using UnityEngine;

public class FpsTargeter : MonoBehaviour
{
    private void Awake()
    {
        Application.targetFrameRate = 60;
    }
}
