using System.Collections.Generic;
using System;
using Newtonsoft.Json;

public class PlayerData
{
    private SkinType _selectedBallSkin;

    private List<SkinType> _openBallsSkins;

    private int _money;

    public PlayerData()
    {
        _money = 5;

        _selectedBallSkin = SkinType.Blue;

        _openBallsSkins = new List<SkinType>() { _selectedBallSkin };
    }

    [JsonConstructor]
    public PlayerData(int money, SkinType selectedBallSkin, List<SkinType> OpenBallsSkins)
    {
        Money = money;
        _selectedBallSkin = selectedBallSkin;
        _openBallsSkins = new List<SkinType>(OpenBallsSkins);
    }

    public int Money
    {
        get => _money;

        set
        {
            if (value < 0)
                throw new ArgumentException(nameof(value));

            _money = value;
        }
    }

    public SkinType SelectedBallSkin
    {
        get => _selectedBallSkin;
        set
        {
            if (_openBallsSkins.Contains(value) == false)
                throw new ArgumentException(nameof(value));

            _selectedBallSkin = value;
        }
    }

    public IEnumerable<SkinType> OpenBallsSkins => _openBallsSkins;

    public void OpenBallSkin(SkinType skin)
    {
        if (_openBallsSkins.Contains(skin))
            throw new ArgumentException(nameof(skin));

        _openBallsSkins.Add(skin);
    }
}
