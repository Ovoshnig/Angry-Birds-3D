using Cysharp.Threading.Tasks;
using R3;
using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class TextPrinterView : MonoBehaviour
{
    [SerializeField, Min(0.1f)] private float _speed = 5f;
    [SerializeField] private bool _playOnAwake = false;

    private readonly ReactiveProperty<bool> _isPrinting = new(false);

    private TMP_Text _tmpText;
    private int _operationId;

    public ReadOnlyReactiveProperty<bool> IsPrinting => _isPrinting;
    public Observable<Unit> Completed { get; private set; }

    protected TMP_Text TmpText => _tmpText;

    protected virtual void Awake()
    {
        Completed = _isPrinting
           .Pairwise()
           .Where(isPrinting => isPrinting.Previous && !isPrinting.Current)
           .Select(_ => Unit.Default)
           .Share();

        _tmpText = GetComponent<TMP_Text>();

        if (_playOnAwake)
        {
            string initialText = _tmpText.text;
            PrintAsync(initialText).Forget();
        }
    }

    protected virtual void OnDestroy()
    {
        CancelPrinting();

        _isPrinting.Dispose();
    }

    public async UniTask PrintAsync(string fullText)
    {
        CancelPrinting();
        int currentOperationId = ++_operationId;

        _isPrinting.Value = true;

        _tmpText.text = fullText;
        _tmpText.maxVisibleCharacters = 0;

        _tmpText.ForceMeshUpdate();

        int totalVisibleCharacters = _tmpText.textInfo.characterCount;
        float delay = 1f / _speed;

        try
        {
            for (int i = 0; i < totalVisibleCharacters; i++)
            {
                if (currentOperationId != _operationId)
                    throw new OperationCanceledException();

                _tmpText.maxVisibleCharacters = i + 1;
                await UniTask.WaitForSeconds(delay, cancellationToken: destroyCancellationToken);
            }
        }
        catch (OperationCanceledException)
        {
            _tmpText.maxVisibleCharacters = totalVisibleCharacters;
        }
        finally
        {
            if (currentOperationId == _operationId)
                _isPrinting.Value = false;
        }
    }

    public void CancelPrinting()
    {
        _operationId++;
        _isPrinting.Value = false;
    }
}
