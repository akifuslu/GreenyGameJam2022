namespace Audio
{
    public static class PlaySfx
    {
        public static void PlayButtonClick()
        {
            var au = AudioManager.Instance;
            if (!au) return;

            au.PlaySfx(au.GetClip(SfxClips.ButtonClick));
        }
        
        public static void PlayTileClip(SfxClips target)
        {
            var au = AudioManager.Instance;
            if (!au) return;

            au.PlaySfxTile(au.GetClip(target));
        }

        public static void PlayNegativeButtonClick()
        {
            var au = AudioManager.Instance;
            if (!au) return;

            au.PlaySfx(au.GetClip(SfxClips.NegativeButtonClick));
        }
        
        public static void PlayMove()
        {
            var au = AudioManager.Instance;
            if (!au) return;

            au.PlaySfx(au.GetClip(SfxClips.Move));
        }
        
        public static void PlayPopupShown()
        {
            return;
            var au = AudioManager.Instance;
            if (!au) return;

            au.PlaySfx(au.GetClip(SfxClips.PopupShown));
        }
        
        public static void PlayChoiceSelected()
        {
            var au = AudioManager.Instance;
            if (!au) return;

            au.PlaySfx(au.GetClip(SfxClips.ChoiceSelected));
        }
        
        public static void PlayWin()
        {
            var au = AudioManager.Instance;
            if (!au) return;

            au.PlaySfx(au.GetClip(SfxClips.Win));
        }
        
        public static void PlayLose()
        {
            var au = AudioManager.Instance;
            if (!au) return;

            au.PlaySfx(au.GetClip(SfxClips.Lose));
        }
        
    }
}