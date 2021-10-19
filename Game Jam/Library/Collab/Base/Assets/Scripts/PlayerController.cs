using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
   public float speed = 200.0f;
   public float jumpForce = 13.0f;
   public float airborneJumpForce = 22f;
   private int extraJumps;
   public int extraJumpsValue = 1;

   private bool _alive;

   private Rigidbody2D _body;
   private BoxCollider2D _box;

   public float SlideTime = 0.5f;
   private float box_x = 1.036291f;
   private float box_y = 1.915028f;

   private float jumpTimeCounter;
   public float jumpTime = 0.25f;
   bool isJumping;

    // Start is called before the first frame update
    void Start()
    {
      extraJumps = extraJumpsValue;
      _alive = true;
      _body = GetComponent<Rigidbody2D>();
      _box = GetComponent<BoxCollider2D>();
    }

     IEnumerator Slide()
    {
        _box.size = new Vector2(box_x, 0.3f * box_y);
        yield return new WaitForSeconds(0.5f * PowerupManager.getSlideTimeMultiplier());
        _box.size = new Vector2(box_x, box_y);
    }

    //Update is called once per frame
    void Update()
    {
        if(_alive) {

            float positionX = Mathf.Lerp(-6f, 0, SanityController.GetSanity()/100);
            transform.localPosition = new Vector2(positionX, transform.localPosition.y);

            Vector3 max = _box.bounds.max;
            Vector3 min = _box.bounds.min;
            Vector2 corner1 = new Vector2(max.x, min.y - .05f);
            Vector2 corner2 = new Vector2(min.x, min.y - .1f);
            Collider2D hit = Physics2D.OverlapArea(corner1, corner2);
            bool grounded = false;

            if (hit != null) {
                grounded = true;
            }

            if (grounded && Input.GetKeyDown(KeyCode.Space)) {
                //_body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                extraJumps = extraJumpsValue;
               _body.velocity = new Vector2(0, jumpForce);
               jumpTimeCounter = jumpTime;
               isJumping = true;
            }

            if (Input.GetKey(KeyCode.Space) && isJumping == true) {
               if(jumpTimeCounter > 0){
                  _body.velocity = new Vector2(0, jumpForce);
                  jumpTimeCounter -= Time.deltaTime;
               } else {
                  isJumping = false;
               }
            }

            if (Input.GetKeyDown(KeyCode.Space) && !grounded && extraJumps > 0)
            {
                // Debug.Log("test");
                _body.velocity = new Vector2(0, airborneJumpForce);
                extraJumps--;
            }

            if(Input.GetKeyUp(KeyCode.Space)){
               isJumping = false;
            }

            if (grounded && Input.GetKeyDown(KeyCode.S)) {
                StartCoroutine("Slide");
            }
        }
    }

     public void SetAlive(bool alive) {
        _alive = alive;
     }

}
