using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class InteractableManager : MonoBehaviour
{
    public static InteractableManager Instance;
    public VideoPlayer videoPlayer; // 綁定你的 VideoPlayer 組件
    public Renderer tvScreenRenderer; // 電視螢幕的 Renderer，用於控制材質
    public Material offScreenMaterial; // 關閉時的螢幕材質
    public Material onScreenMaterial;  // 打開時的螢幕材質

    public Animator tvAnimator;
    private bool isVideoPlaying = false; // 記錄影片是否正在播放
    public float snapThreshold = 2f;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        // 初始化時讓電視處於關閉狀態
        if (tvScreenRenderer != null && offScreenMaterial != null)
        {
            tvScreenRenderer.material = offScreenMaterial;
        }
    }
    public void PlayVideo(GameObject interactableObject)
    {
        if (isVideoPlaying) return;
        Animator animator = interactableObject.GetComponent<Animator>();
        if (animator != null)
        {
            animator.SetTrigger("FlickerOn");  // 觸發 flicker 動畫
    

        }
        
        if (!isVideoPlaying && videoPlayer != null && tvScreenRenderer != null)
        {tvAnimator.SetBool("IsTVOn", true);
            // 確保動畫完成後再播放影片
            StartCoroutine(WaitForAnimationThenPlayVideo());
        }
    }

    private IEnumerator WaitForAnimationThenPlayVideo()
    {
        // 等待動畫播放完成
        while (tvAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            yield return null; // 每一幀檢查動畫進度
        }

        // 動畫結束，切換材質並播放影片
        Material newOnScreenMaterial = new Material(onScreenMaterial);
        tvScreenRenderer.material = newOnScreenMaterial;

        videoPlayer.Play();
        isVideoPlaying = true;
    }
    public void HandleInteraction(Item item, GameObject interactableObject)
    {
        InteractableManager tv = interactableObject.GetComponent<InteractableManager>();
        if (tv != null)
        {
            float distance = Vector3.Distance(interactableObject.transform.position, this.transform.position);
        
            if (distance <= snapThreshold)
            {
                tv.PlayVideo(interactableObject); // Trigger TV interaction if within range
            }
        }
    }
}
