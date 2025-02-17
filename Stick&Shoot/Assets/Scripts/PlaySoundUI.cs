using UnityEngine;

public class PlaySoundUI : MonoBehaviour
{
    [SerializeField] private Sounds _soundType;

    public void PlaySound()
    {
        SoundManager.Instance.PlaySound(_soundType);
    }
}
