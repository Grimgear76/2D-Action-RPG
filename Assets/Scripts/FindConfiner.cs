using UnityEngine;
using Unity.Cinemachine;
using UnityEngine.SceneManagement;
using System.Collections;

public class FindConfiner : MonoBehaviour
{
    private CinemachineConfiner2D confiner;

    private void Awake()
    {
        confiner = GetComponent<CinemachineConfiner2D>();
        if (confiner == null)
            Debug.LogWarning("FindConfiner: No CinemachineConfiner2D found on this GameObject.");
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(AssignConfinerNextFrame());
    }

    private IEnumerator AssignConfinerNextFrame()
    {
        // Wait for the scene to fully initialize
        yield return null;

        if (confiner == null) yield break;

        GameObject confinerObject = GameObject.Find("Confiner");
        if (confinerObject == null)
        {
            Debug.LogWarning("FindConfiner: Could not find 'CamConfiner' in scene: " + SceneManager.GetActiveScene().name);
            yield break;
        }

        PolygonCollider2D poly = confinerObject.GetComponent<PolygonCollider2D>();
        if (poly == null)
        {
            Debug.LogWarning("FindConfiner: 'CamConfiner' has no PolygonCollider2D.");
            yield break;
        }

        confiner.BoundingShape2D = poly;
        confiner.InvalidateBoundingShapeCache();
    }
}