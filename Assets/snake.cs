using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snake : MonoBehaviour
{
    public float step;
    Controls controls;
    bool growingPending;
    public GameObject tailPrefab,foodPrefab,leftSide,rightSide,topSide,bottomSide;
    public List<Transform> tail = new List<Transform>();
    Vector3 lastPos;

    enum Controls
    {
        up,down,left,right
    }

    private void Start(){
        step = GetComponent<SpriteRenderer>().bounds.size.x;
        StartCoroutine(MoveCoroutine());
        CreateFood();
    }

    private void Update(){
        if(Input.GetKeyDown(KeyCode.RightArrow)){
            controls = Controls.right;
        }
        else if(Input.GetKeyDown(KeyCode.LeftArrow)){
            controls = Controls.left;
        }
        else if(Input.GetKeyDown(KeyCode.UpArrow)){
            controls = Controls.up;
        }
        else if(Input.GetKeyDown(KeyCode.DownArrow)){
            controls = Controls.down;
        }
    }

    private void Move(){
        lastPos = transform.position;
        Vector3 nextPos = Vector3.up;
        if(controls == Controls.left){
            nextPos = Vector3.left;
        }
        else if(controls == Controls.right){
            nextPos = Vector3.right;
        }
        else if(controls == Controls.up){
            nextPos = Vector3.up;
        }
        else if(controls == Controls.down){
            nextPos = Vector3.down;
        }
        transform.position += nextPos*step;
        MoveTail();
    }

    void MoveTail(){
        for (int i = 0; i < tail.Count; i++){
            Vector3 temp = tail[i].position;
            tail[i].position = lastPos;
            lastPos = temp;
        }
        if (growingPending) CreateTail();
    }

    IEnumerator MoveCoroutine(){
        while (true){
            yield return new WaitForSeconds(0.5f);
            Move();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.gameObject.tag == "food"){
            print("colision");
            growingPending = true;
            Destroy(collision.gameObject);
            CreateFood();
        }
        else if(collision.gameObject.tag == "block"){
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameOber");
        }
    }

    void CreateTail(){
        GameObject newTail = Instantiate(tailPrefab, lastPos, Quaternion.identity);
        newTail.name = "Tail_" + tail.Count;
        tail.Add(newTail.transform);
        growingPending = false;
    }

    void CreateFood(){
        Vector2 pos = new Vector2(Random.Range(leftSide.transform.position.x,rightSide.transform.position.x),Random.Range(topSide.transform.position.y,bottomSide.transform.position.y));
        Instantiate(foodPrefab, pos, Quaternion.identity);
    }
}
