using UnityEngine;

public class Player : MonoBehaviour
{

    void Update()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Moved)
            {

            }
        }
    }
}
