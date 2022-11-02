using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Arkanoid.UI
{
    public class UIComponent : MonoBehaviour
    {

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            gameObject.GetComponent<Text>().text = $"{Managers.GameManager.Self.GetHealth()}";
        }
    }
}
