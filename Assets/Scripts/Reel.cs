using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Reel : MonoBehaviour
{
    [SerializeField]
    private Image[] _commandButtons;

    private int _currentSelectedIndex;
    private CancellationTokenSource _cancellationTokenSource;
    private async void Start()
    {
        _cancellationTokenSource = new CancellationTokenSource();
        try
        {
            await Spin(_cancellationTokenSource);
        }
        catch(OperationCanceledException exception)
        {
            Debug.Log("終わった");
        }
        
    }

    private async void Update()
    {
        if (Input.GetButtonDown("Jump") && _cancellationTokenSource.IsCancellationRequested==false)
        {
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();
        }
        else if(Input.GetButtonDown("Jump"))
        {
            _cancellationTokenSource = new CancellationTokenSource();
            try
            {
                await Spin(_cancellationTokenSource);
            }
            catch(OperationCanceledException exception)
            {
                Debug.Log("終わった");
            }
        }
    }

    public async UniTask Spin(CancellationTokenSource source)
    {
        while (source.Token.IsCancellationRequested == false)
        {
            await UniTask.Delay(100,cancellationToken:source.Token);
            _commandButtons[_currentSelectedIndex].color = Color.white;
            _currentSelectedIndex++;
            _currentSelectedIndex %= _commandButtons.Length;
            _commandButtons[_currentSelectedIndex].color = Color.red;
        }
    }
}
