namespace Assets.MobileOptimizedWater.Scripts
{
    using UnityEngine;

    public class AnimationStarter : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private Motion animation1;

        public void Awake()
        {
            animator.Play(animation1.name);
        }
    }
}
