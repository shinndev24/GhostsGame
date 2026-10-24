using UnityEngine;

public class HaloShaderController : MonoBehaviour
{
    public Transform player;
    public float radius = 4f;

    void Update()
    {
        Shader.SetGlobalVector("_PlayerPos", player.position);
        Shader.SetGlobalFloat("_Radius", radius);
        Shader.SetGlobalFloat("_Softness", 1.5f);
    }
}