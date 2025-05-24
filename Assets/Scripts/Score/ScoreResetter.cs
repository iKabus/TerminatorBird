using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ScoreResetter : MonoBehaviour
{
    [SerializeField] private ScoreData _score;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() => _score.Reset());
    }
}
