using Cysharp.Threading.Tasks;
using LitMotion;
using LitMotion.Extensions;
using Ovoshnig.UI.Basic;
using R3;
using TMPro;
using UnityEngine;

namespace Ovoshnig.UI.TextPrinting;

[RequireComponent(typeof(TMP_Text))]
public class TextPrinterView : UIView
{
    [SerializeField, Min(0.1f)] private float _speed = 5f;
    [SerializeField] private bool _autoPlay = false;

    private readonly ReactiveProperty<bool> _isPrinting = new(false);
    private readonly Subject<Unit> _completed = new();
    private MotionHandle _handle;

    public ReadOnlyReactiveProperty<bool> IsPrinting => _isPrinting;
    public Observable<Unit> Completed => _completed;

    protected TMP_Text TmpText { get; private set; }

    protected virtual void Awake() => TmpText = GetComponent<TMP_Text>();

    protected virtual void Start()
    {
        if (_autoPlay)
            PrintAsync(TmpText.text).Forget();
    }

    protected override void OnDestroy()
    {
        _isPrinting.Dispose();
        _completed.Dispose();
        base.OnDestroy();
    }

    public async UniTask PrintAsync(string fullText)
    {
        _handle.TryCancel();

        _isPrinting.Value = true;

        TmpText.text = fullText;
        TmpText.ForceMeshUpdate();

        int totalVisibleCharacters = TmpText.textInfo.characterCount;
        float duration = totalVisibleCharacters / _speed;

        try
        {
            _handle = LMotion.Create(0, totalVisibleCharacters, duration).BindToMaxVisibleCharacters(TmpText);

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
