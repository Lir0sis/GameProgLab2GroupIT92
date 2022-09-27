using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodItem : MonoBehaviour
{
    private float _randOffset;
    private Vector3 _origin;

    void Start()
    {
        _origin = transform.position;
        _randOffset = Random.Range(0, 20);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        transform.position = new Vector3(_origin.x, _origin.y + Mathf.Sin(Time.realtimeSinceStartup + _randOffset) * 0.1f, _origin.z);
        transform.localEulerAngles += new Vector3(0, 40 * Time.fixedDeltaTime, 0);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Character")
        {
            var script = other.gameObject.GetComponent(typeof(PlayerControl)) as PlayerControl;
            script.CurrentScore++;
            Destroy(gameObject);
        }
    }


}
