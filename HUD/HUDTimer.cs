using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDTimer : HUDElement {
    
    public void EditChrono(float time)
    {
        if (time < 0)
            time = 0;
        

        //float minutes = Mathf.Floor((float)time / 60);
        float seconds = Mathf.Floor((float)time % 60);
        float milliSeconds = Mathf.Floor(((float)time*1000) % 1000);

        if (milliSeconds >= 100)
            milliSeconds /= 10;

        GetComponent<Text>().text = string.Format("{00:00}:{1:00}",seconds, milliSeconds);
    }

}
