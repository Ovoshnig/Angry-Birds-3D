using Cysharp.Threading.Tasks;
using R3;
using System;
using System.Text;
using System.Threading;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class TextPrinterView : MonoBehaviour
{
    [SerializeField, Min(0.1f)] private float _speed = 5f;
    [SerializeField] private bool _playOnAwake = false;

    private readonly ReactiveProperty<bool> _isPrinting = new(false);
    private readonly Subject<Unit> _completed = new();

    private TMP_Text _tmpText;
    private CancellationTokenSource _cts;

    public ReadOnlyReactiveProperty<bool> IsPrinting => _isPrinting;
    public Observable<Unit> Completed => _completed;

    protected TMP_Text TmpText => _tmpText;

    protected virtual void Awake()
    {
        IsPrinting
           .Pairwise()
           .Where(isPrinting => isPrinting.Previous && !isPrinting.Current)
           .Subscribe(_ => _completed.OnNext(Unit.Default))
           .AddTo(this);

        _tmpText = GetComponent<TMP_Text>();

        if (_playOnAwake)
        {
            string initialText = _tmpText.text;
            PrintAsync(initialText).Forget();
        }
    }

    protected virtual void OnDestroy() => CancelPrinting();

    public async UniTask PrintAsync(string fullText)
    {
        CancelPrinting();
        _cts = new CancellationTokenSource();

        _isPrinting.Value = true;
        _tmpText.text = string.Empty;

        StringBuilder stringBuilder = new();
        float delay = 1 / _speed;

        try
        {
            foreach (var symbol in fullText)
            {
                stringBuilder.Append(symbol);
                _tmpText.text = stringBuilder.ToString();

                await UniTask.WaitForSeconds(delay, cancellationToken: _cts.Token);
            }
        }
        catch (OperationCanceledException)
        {
            _tmpText.text = fullText;
        }
        finally
        {
            _isPrinting.Value = false;
        }
    }

    public void CancelPrinting()
    {
        if (_cts != null)
        {
            _cts.Cancel();
            _cts.Dispose();
            _cts = null;
        }
    }
}
