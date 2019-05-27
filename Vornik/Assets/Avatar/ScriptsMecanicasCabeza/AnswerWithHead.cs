using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerWithHead : MonoBehaviour
{
    [SerializeField] private List<GameObject> collisions = new List<GameObject>();
    [SerializeField] private List<float> timeCollisions = new List<float>();
    [SerializeField] private float timeToAnswer = 3f;
    [SerializeField] private bool _isCollidin = false;
    [SerializeField] private SubtitleControl subtitleControl;

    private string _buttDer = "Oculus_CrossPlatform_Button2";
    private string _buttIzq = "Oculus_CrossPlatform_Button4";
    enum Answer { Yes, No, None }
    private Answer _answer = Answer.None;

    private void Update()
    {
        if (_answer != Answer.None) enabled = false;

        if (Input.GetButtonDown(_buttDer))
        {
            _answer = Answer.No;
            return;
        }

        if (Input.GetButtonDown(_buttIzq))
        {
            _answer = Answer.Yes;
            return;
        }

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            if (hit.transform.CompareTag("AskCollider") && !_isCollidin)
            {
                collisions.Add(hit.transform.gameObject);
                timeCollisions.Add(Time.time);
                _isCollidin = true;
            }
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
        }
        else
        {
            _isCollidin = false;
            if (collisions.Count > 2)
                _answer = CompareAnswers();
        }

    }

    private Answer CompareAnswers()
    {
        var last0 = collisions[collisions.Count - 1];
        var last1 = collisions[collisions.Count - 2];
        var last2 = collisions[collisions.Count - 3];
        var lastTime0 = timeCollisions[collisions.Count -1];
        var lastTime2 = timeCollisions[collisions.Count - 3];

        if (lastTime0 - lastTime2 > timeToAnswer)
            return Answer.None;
        if ((last0.name == "Left" && last1.name == "Right" && last2.name == "Left") || (last0.name == "Right" && last1.name == "Left" && last2.name == "Right"))
            return Answer.No;
        if ((last0.name == "Up" && last1.name == "Down" && last2.name == "Up") || (last0.name == "Down" && last1.name == "Up" && last2.name == "Down"))
            return Answer.Yes;

        return Answer.None;
    }

    private void OnDisable()
    {
        var aux = _answer == Answer.Yes ? true : false;
        subtitleControl.Answer(aux);
    }

}
