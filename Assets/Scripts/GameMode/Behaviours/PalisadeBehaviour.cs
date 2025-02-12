using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalisadeBehaviour : MonoBehaviour
{
    [SerializeField] private int _health = 3;
    [SerializeField] private List<string> _ignorePostNames = new List<string>();
    [SerializeField] private GameObject _scoreboard;

    private List<GameObject> _posts = new List<GameObject>();
    private Dictionary<string,int> _postHealth = new Dictionary<string, int>();
    private bool _isWrecked = false;

    private void Start()
    {
        foreach (Transform child in transform)
        {
            if (child.name.StartsWith("Cylinder") && !_ignorePostNames.Contains(child.name))
            {
                _posts.Add(child.gameObject);
                _postHealth.Add(child.name, _health);
            }
        }
    }

    public void EnableScoreboard()
    {
        _scoreboard.SetActive(true);
    }

    public void Damage()
    {
        if(_posts.Count <= 0) return;

        // pick a random post
        int index = Random.Range(0, _posts.Count);

        // get the post name
        string postName = _posts[index].name;

        // reduce the health of the post
        _postHealth[postName]--;

        // if the post health is zero, remove the post
        if (_postHealth[postName] <= 0)
        {
            // remove the post
            Destroy(_posts[index]);
            _posts.RemoveAt(index);
        }

        // if all posts are destroyed
        if (_posts.Count <= 0)
        {
            Debug.Log($"{name} is wrecked!");
            _isWrecked = true;
        }
    }

    public bool IsWrecked()
    {
        return _isWrecked;
    }
}
