using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cinemachine;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Pool;
using VContainer;
using VContainer.Unity;

public class GameManager : MonoBehaviour
{
    [Inject] IObjectResolver _resolver;

    [SerializeField]
    private Player _player;

    public CinemachineVirtualCamera virtualCamera;

    void Start()
    {
        Debug.Log("Start[GameManager] start");
        Debug.Log("Start[GameManager] end");
        OnGameStart();
    }

    public void OnGameStart()
    {
        Debug.Log("OnGameStart[GameManager] start");
        _player = _resolver.Instantiate(_player);
        virtualCamera.Follow = _player.gameObject.transform;
        Debug.Log("OnGameStart[GameManager] end");
    }

    [Inject]
    public void Construct(IObjectResolver resolver)
    {
        Debug.Log($"Construction[GameManager] start");
        _resolver = resolver;
        Debug.Log("Construction[GameManager] ended");
    }

}
