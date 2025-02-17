using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum Sounds
{
	ButtonClick,
	ButtonAppear,
	HideButton,
	Win,
	Lose,
	BallShoot,
	StickBall,
	KickOff,
	StarCollect,
	DestroyObstacle,
	BallDestroy,
	MenuMusic,
	GameMusic,
	BuySound
}

public class SoundManager : MonoBehaviour
{
	public static SoundManager Instance { get; private set; }

	private const string MusicVolumeKey = "MusicVolume";  
	private const string SFXVolumeKey = "SFXVolume";      

	public float MusicVolume { get; private set; } = 0.5f; 
	public float SFXVolume { get; private set; } = 0.5f;   

	[System.Serializable]
	public class SoundEntry
	{
		public Sounds soundType;      
		public AudioClip clip;        
		public AudioSource source;    
		public bool isMusic;          
	}

	[SerializeField]
	private List<SoundEntry> sounds = new List<SoundEntry>();

	private Dictionary<Sounds, SoundEntry> soundDictionary = new Dictionary<Sounds, SoundEntry>();

	private Slider MusicSlider;     
	private Slider SFXSlider;       

	private void Awake()
	{
		if (Instance != null && Instance != this)
		{
			Destroy(gameObject);
			return;
		}
		Instance = this;
		DontDestroyOnLoad(gameObject);

		InitializeSoundDictionary();

		MusicVolume = PlayerPrefs.GetFloat(MusicVolumeKey, 0.5f); 
		SFXVolume = PlayerPrefs.GetFloat(SFXVolumeKey, 0.5f);     
	}

	private void Start()
	{
		InitializeSliders();

		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		InitializeSliders();
	}

	private void InitializeSliders()
	{
		MusicSlider = GameObject.FindGameObjectWithTag("MusicSlider")?.GetComponent<Slider>();     
		SFXSlider = GameObject.FindGameObjectWithTag("SFXSlider")?.GetComponent<Slider>();

		if (MusicSlider != null)
		{
			MusicSlider.value = MusicVolume;  
			MusicSlider.onValueChanged.AddListener(value =>
			{
				SetMusicVolume(value);
			});
		}

		if (SFXSlider != null)
		{
			SFXSlider.value = SFXVolume; 
			SFXSlider.onValueChanged.AddListener(value =>
			{
				SetSFXVolume(value);
			});
		}
	}

	private void InitializeSoundDictionary()
	{
		foreach (var sound in sounds)
		{
			if (!soundDictionary.ContainsKey(sound.soundType))
				soundDictionary[sound.soundType] = sound;

			if (sound.source == null)
			{
				sound.source = gameObject.AddComponent<AudioSource>();
				sound.source.clip = sound.clip;
				sound.source.loop = sound.isMusic; 
			}
		}
	}

	public void SetMusicVolume(float volume)
	{
		MusicVolume = volume;

		foreach (var sound in sounds)
		{
			if (sound.isMusic && sound.source != null)
				sound.source.volume = MusicVolume;
		}

		PlayerPrefs.SetFloat(MusicVolumeKey, MusicVolume);
		PlayerPrefs.Save();
	}

	public void SetSFXVolume(float volume)
	{
		SFXVolume = volume;

		foreach (var sound in sounds)
		{
			if (!sound.isMusic && sound.source != null)
				sound.source.volume = SFXVolume;
		}

		PlayerPrefs.SetFloat(SFXVolumeKey, SFXVolume);
		PlayerPrefs.Save();
	}

	public void PlaySound(Sounds soundType)
	{
		if (soundDictionary.TryGetValue(soundType, out var sound))
		{
			sound.source.volume = sound.isMusic ? MusicVolume : SFXVolume;
			sound.source.Play();
		}
		else
		{
			Debug.LogWarning($"Sound of type '{soundType}' not found.");
		}
	}

	public void StopSound(Sounds soundType)
	{
		if (soundDictionary.TryGetValue(soundType, out var sound))
		{
			sound.source.Stop();
		}
		else
		{
			Debug.LogWarning($"Sound of type '{soundType}' not found.");
		}
	}
}