using System.Collections;

public class AMLoader : MonoBehaviour

    public AudioManager theAM;

    private void Awake()

            AudioManager newAM = Instantiate(theAM);


        }
    }
}