using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public GameObject click;
    public Point point = null;
    [Header("Jde o speci�ln� sousedstv� [Zastaral� nepou��vat, ale m?lo by st�le fungovat]")]
    public int id = 0;
    public float speed = 0;
    public GameObject direction = null;
    public SpecialConnection Sc;

    void clicked()
    {
        // Pokud je t�eba, zjisti, kdo jsi
        if (point == null)
        {
            for (int k = 0; k < Player.mapa.Count; k++)
            {
                if (((Point)Player.mapa[k]).button == this.gameObject)
                {
                    point = (Point)Player.mapa[k];
                    break;
                }
            }
        }
        // Try to play a sound
        SoundManager sm = SoundManager.getManager();
        if(sm != null){
            sm.playClick();
        }

        // Spawn a circle
        Instantiate(click, transform.position + transform.up.normalized * 0.01f, transform.rotation);
        //Debug.Log("click");
        if(point != null){
            Player.najdiCestu(point);
        }else{
            Debug.Log("point nenalezen");
        }        
    }


    
    void Start()
    {
        
    }


    void Update()
    {
       
    }
}
