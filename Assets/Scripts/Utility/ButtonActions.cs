using UnityEngine;
using UnityEngine.SceneManagement;

namespace Utility
{
    public class ButtonActions : MonoBehaviour
    {
        public void LoadGame()
        {
            Fader.Instance.FadeScene(2);
        }

        public void LoadMainMenu()
        {
            Fader.Instance.FadeScene(1);
        }
        
        public void ExitGame()
        {
            Application.Quit();
        }
        
    }
}