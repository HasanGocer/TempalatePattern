using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoSingleton<PlayerManager>
{
    private int _playerItemCount;
    private int _playerMoneyCount;

    public void PlusPlayerItemCount(int plus)
    {
        _playerItemCount += plus;
    }
    public void PlusPlayerMoneyCount(int plus)
    {
        _playerMoneyCount += plus;
    }

    public int ReturnPlayerItemCount()
    {
        return _playerItemCount;
    }
    public int ReturnPlayerMoneyCount()
    {
        return _playerMoneyCount;
    }
}
