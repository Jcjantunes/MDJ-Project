using UnityEngine;

public class AnimationDestroyWhenDone : MonoBehaviour {
    public void destroy() {
        Destroy(gameObject);
    }
}