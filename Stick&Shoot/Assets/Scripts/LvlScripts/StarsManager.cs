using UnityEngine;
using UnityEngine.UI;

public class StarsManager : MonoBehaviour
{
    [SerializeField] private Text _starsCountText;
    [SerializeField] private ParticleSystem _starParticles;

    private int _stars = 0;

    private Wallet _wallet;

    private IDataProvider _dataProvider;

    public void Inialize(Wallet wallet, IDataProvider dataProvider)
    {
        _wallet = wallet;
        _dataProvider = dataProvider;
    }

    public void AddStars(int count)
    {
        _stars += count;
    }

    public void SummingStars()
    {
        _starsCountText.text = _stars.ToString();
		//add in JSON file.
		_wallet.AddCoins(_stars);
        _dataProvider.Save();
    }

    public void SpawnParticles(Transform spawnPoint)
    {
        if (!_starParticles.isPlaying)
        {
            PlayParticles(spawnPoint);
        }
        else
        {
            _starParticles.Stop();
			PlayParticles(spawnPoint);
		}
    }

    private void PlayParticles(Transform spawnPoint)
    {
		_starParticles.transform.position = spawnPoint.position;
		_starParticles.Play();
	}
}
