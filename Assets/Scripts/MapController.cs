using UnityEngine;

public class MapScreenFollow : MonoBehaviour
{
    private bool isOpen;
    [SerializeField] GameObject map;
    public float maxTime = 5f;
    public float cooldown = 10f;
    //mientras el mapa este en uso, parar al jugador
    //Limitar el uso del mapa a 5secs
    //anadir cooldown para sacar el mapa (10s)

    void Start()
    {
        map.SetActive(false);
    }
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            isOpen = !isOpen;
            if (map != null) 
            {
                map.SetActive(isOpen);
            }      
        }
    }
}