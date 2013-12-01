using UnityEngine;
using System.Collections;

public class BombFocusBeam : MonoBehaviour
{
    private Transform myTrfm;
    public GameObject targetGObj;
    private Transform targetTrfm;

    public  GameObject bombGObj;
    private Bomb       bomb;


    // Use this for initialization
    void Start()
    {
        renderer.sortingLayerName = "bullet";
        bomb        = (Bomb) bombGObj.GetComponent( "Bomb" );
        myTrfm      = transform;
        targetTrfm  = targetGObj.transform;

        SetTransform();
    }

    // Update is called once per frame
    void Update()
    {
        if ( bombGObj == null || targetGObj == null ) {
            CancelAttack();
        }
        SetTransform();


    }

    private void SetTransform()
    {
        if ( targetGObj == null ) {
            return;
        }
        
        Vector2 enemyPos     = targetTrfm.position;
        Vector2 myPos        = myTrfm.position;

        //get original length, must set rotation = identity to get correct bounds
        myTrfm.rotation      = Quaternion.identity;
        float currentLength  = renderer.bounds.size.x;

        //set ratation
        Quaternion rot       = Quaternion.identity;
        rot.eulerAngles      = new Vector3( 0, 0, 57.2958f * Mathf.Atan2( ( enemyPos.y - myPos.y ), ( enemyPos.x - myPos.x ) ) );
        myTrfm.rotation      = rot;

        //set scale
        float   targetLength = Vector2.Distance( myTrfm.position, enemyPos );
        Vector3 scale        = myTrfm.localScale;
        float   times        = targetLength*2f / currentLength;
        scale.x              = scale.x*times + 0.1f;// targetLength + 0.2f;
        scale.y              = 1;
        myTrfm.localScale    = scale;
    }


    void OnTriggerExit2D( Collider2D collider )
    {
        if ( collider.gameObject == targetGObj ) {
            CancelAttack();
        }
    }

    public void CancelAttack()
    {
        if (bomb != null)
            bomb.CancelAttack();

        if (gameObject != null)
            DestroyObject( gameObject );
    }
}
