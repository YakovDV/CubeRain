using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    public void ChangeColor(Renderer renderer)
    {
        if (renderer != null)
            renderer.material.color = Random.ColorHSV();
    }
}
