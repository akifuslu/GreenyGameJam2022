using UnityEngine;

namespace Audio
{
    public class SfxPlayer : MonoBehaviour
    {
        [SerializeField] private SfxClips clip;

        public void PlaySfx()
        {
            if (!AudioManager.Instance) return;
            
            AudioManager.Instance.PlaySfx(AudioManager.Instance.GetClip(clip));
        }
        
    }
}