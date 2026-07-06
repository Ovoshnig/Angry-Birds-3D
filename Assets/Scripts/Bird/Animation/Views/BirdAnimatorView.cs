using UnityEngine;

public class BirdAnimatorView : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        if (TryGetComponent(out Animator animator))
            _animator = animator;
    }

    public void SetFloat(int id, float value) => _animator.SetFloat(id, value);

    public void SetBool(int id, bool value) => _animator.SetBool(id, value);

    public void SetTrigger(int id) => _animator.SetTrigger(id);
}
