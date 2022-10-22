using UnityEngine;

namespace Utility
{
    public class ButtonActions : MonoBehaviour
    {
        public void LoadGame()
        {
            Fader.Instance.FadeScene(1);
        }

        public void LoadMainMenu()
        {
            Fader.Instance.FadeScene(0);
        }
        
        public void ExitGame()
        {
            Application.Quit();
        }
        
    }
}