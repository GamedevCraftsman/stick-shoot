using UnityEngine;

public class NumberOfLvl : MonoBehaviour
{
    [SerializeField] private int _lvlNumber;
    [SerializeField] private GameObject _lvlNumberObj;
    [SerializeField] private GameObject _lockPanel;
    [SerializeField] private GameObject _donePanel;

    public int LvlNumber { get => _lvlNumber; private set => _lvlNumber = value; }
    public GameObject LockPanel { get => _lockPanel; private set => _lockPanel = value; }
    public GameObject DonePanel { get => _donePanel; private set => _donePanel = value; }
    public GameObject LvlNumberObj { get => _lvlNumberObj; private set => _lvlNumberObj = value;}
}
