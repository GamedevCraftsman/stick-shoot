using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    
    public void LoadScene(int sceneNumber)
    {
        DOTween.KillAll();
        StopAllCoroutines();

		SoundManager.Instance.StopSound(Sounds.MenuMusic);
        SoundManager.Instance.StopSound(Sounds.GameMusic);

		SceneManager.LoadScene(sceneNumber);
    }
}
