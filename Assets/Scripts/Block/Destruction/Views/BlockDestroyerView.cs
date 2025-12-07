using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(CollisionView))]
[RequireComponent(typeof(BlockSFXPlayerView))]
public abstract class BlockDestroyerView : MonoBehaviour
{
    [SerializeField] private GameSettings _gameSettings;


    private Material _material;
    private CollisionView _collisionView = null;
    private BlockSFXPlayerView _sfxPlayerView = null;

    public CollisionView CollisionView
    {
        get
        {
            if (_collisionView == null)
                _collisionView = GetComponent<CollisionView>();

            return _collisionView;
        }
    }

    public BlockSFXPlayerView SFXPlayerView
    {
        get
        {
            if (_sfxPlayerView == null)
                _sfxPlayerView = GetComponent<BlockSFXPlayerView>();

            return _sfxPlayerView;
        }
    }

    public BlockHealthModel HealthModel { get; private set; }

    protected BlockSettings BlockSettings => _gameSettings.BlockSettings;
    protected abstract float MaxHealth { get; }

    private void Awake()
    {
        _material = GetComponent<MeshRenderer>().material;

        HealthModel = new BlockHealthModel(MaxHealth);
    }

    public void Damage(float _)
    {
        float crackAmount = 1f - (HealthModel.Health / MaxHealth);
        _material.SetFloat(BlockDestructionConstants.CrackAmountName, crackAmount);
    }

    public void Destroy() => Destroy(gameObject);
}
