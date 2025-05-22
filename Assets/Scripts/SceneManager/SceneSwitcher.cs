using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SceneSwitcher : MonoBehaviour
{
    [SerializeField] private Scenes _scene;
    
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() => SceneManager.LoadScene(_scene.ToString()));
    }
}
