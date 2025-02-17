using UnityEngine;

[CreateAssetMenu(fileName ="SkinConfig", menuName ="ScriptableObjects/SkinConfig")]
public class BallsSkinsConfigs : ScriptableObject
{
    [field: SerializeField] public GameObject BallPrefab {  get; private set; }
    [field: SerializeField] public Sprite SkinPicture {  get; private set; }
    [field: SerializeField] public int SkinPrice { get; private set; }
    [field: SerializeField] public SkinType SkinType { get; private set; }
}
