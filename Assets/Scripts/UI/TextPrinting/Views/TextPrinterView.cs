using Cysharp.Threading.Tasks;
using LitMotion;
using LitMotion.Extensions;
using R3;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class TextPrinterView : UIView
{
    [SerializeField, Min(0.1f)] private float _speed = 5f;
    [SerializeField] private bool _autoPlay = false;

    private readonly ReactiveProperty<bool> _isPrinting = new(false);
    private readonly Subject<Unit> _completed = new();

    private TMP_Text _tmpText;
    private MotionHandle _handle;

    public ReadOnlyReactiveProperty<bool> IsPrinting => _isPrinting;
    public Observable<Unit> Completed => _completed;

    protected TMP_Text TmpText => _tmpText;

    protected virtual void Awake() => _tmpText = GetComponent<TMP_Text>();

    protected virtual void Start()
    {
        if (_autoPlay)
            PrintAsync(_tmpText.text).Forget();
    }

    protected virtual void OnDestroy()
    {
        _isPrinting.Dispose();
        _completed.Dispose();
    }

    public async UniTask PrintAsync(string fullText)
    {
        _handle.TryCancel();

        _isPrinting.Value = true;

        _tmpText.text = fullText;
        _tmpText.ForceMeshUpdate();

        int totalVisibleCharacters = _tmpText.textInfo.characterCount;
        float duration = totalVisibleCharacters / _speed;

        try
        {
            _handle = LMotion.Create(0, totalVisibleCharacters, duration)
                .BindToMaxVisibleCharacters(_tmpText);

            await _handle.ToUniTask(destroyCancellationToken);
            _completed.OnNext(Unit.Default);
        }
        finally
        {
            if (!_isPrinting.IsDisposed)
                _isPrinting.Value = false;
        }
    }

    public bool TryCompletePrinting() => _handle.TryComplete();
}
