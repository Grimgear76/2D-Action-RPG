using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneChanger : MonoBehaviour
{

    public string sceneToLoad;
    public Animator fadeAnim;
    public float fadeTime = .5f;
    public Vector2 newPlayerPosition;
    private Transform player;

    public string requiredKeyItemID; 
    private InventoryManager inventoryManager;

    private void Start()
    {
        inventoryManager = FindFirstObjectByType<InventoryManager>();
        if (inventoryManager == null)
            Debug.LogError("SceneChanger: Could not find InventoryManager");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (string.IsNullOrEmpty(requiredKeyItemID) || inventoryManager.HasKeyItem(requiredKeyItemID))
            {
                player = collision.transform;
                fadeAnim.Play("FadeToWhite");

                //removes key item
                if (!string.IsNullOrEmpty(requiredKeyItemID))
                    inventoryManager.RemoveKeyItem(requiredKeyItemID);

                StartCoroutine(DelayFade());
            }
            else
            {
                Debug.Log("Need Key Item");
            }
                
        }
    }

    IEnumerator DelayFade()
    {
        yield return new WaitForSeconds(fadeTime);
        player.position = newPlayerPosition;
        SceneManager.LoadScene(sceneToLoad);
    }
}
