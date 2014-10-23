using UnityEngine;
using System.Collections;

public class PlatformGenerator : MonoBehaviour {

    public GameObject player;
    public Transform platforms;
    public SpriteRenderer platformPrefab, coinPrefab, obstaclePrefab;
    public Transform dirt;
    public float minPlatformHeight = 0.5F, maxPlatformHeight = 1.5F, maxYDifference = 5, distanceFactor = 2.3F, minSizeForObstacle = 3;

    private PlatformerCharacter2D playerController;
    private int minY;
    private Transform latestPlatform;

	// Use this for initialization
	void Start () {
        playerController = player.GetComponent<PlatformerCharacter2D>();
        Vector3 spriteSize = dirt.GetComponent<SpriteRenderer>().bounds.max;
        minY = ((int) (spriteSize.y)) - 1;
	}
	
	// Update is called once per frame
	void Update () {    
        Transform currentPlatform = playerController.currentPlatform;
        if(latestPlatform != currentPlatform) {
            latestPlatform = currentPlatform;
            Transform lastPlatform = platforms.GetChild(platforms.childCount - 1);
            float xScale = Random.Range(3.5F + playerController.speed / 3, 6 + playerController.speed / 2);
            Vector3 pos = lastPlatform.position, scale = new Vector3(xScale, Random.Range(minPlatformHeight, maxPlatformHeight), 1);
            pos.y += Random.Range(-maxYDifference + scale.y + 1, maxYDifference - scale.y);
            if(pos.y <= minY) pos.y = minY;
            BoxCollider2D lastPlatformCollider = lastPlatform.GetComponent<BoxCollider2D>();
            pos.x = lastPlatformCollider.bounds.max.x + scale.x * platformPrefab.sprite.bounds.max.x + playerController.speed * (distanceFactor + Random.value);
            SpriteRenderer newPlatform = (SpriteRenderer) Instantiate(platformPrefab, pos, lastPlatform.rotation);
            newPlatform.transform.localScale = scale;
            newPlatform.transform.parent = platforms;
            for(int i = 0; i < 8; ++i) {
                float offset = Random.value;  
                if(Random.value < offset) {
                    Vector3 position = newPlatform.transform.position;
                    Vector3 extents = newPlatform.GetComponent<BoxCollider2D>().bounds.extents;
                    position.y += extents.y + 2.5F + Random.Range(0, 5);
                    position.x += (1 - 2 * (i % 2)) * extents.x * offset;
                    Instantiate(coinPrefab, position, new Quaternion(0, 0, 0, 0));
                }
            }
            if(xScale >= minSizeForObstacle && Random.value < 0.5) {
                Vector3 position = newPlatform.transform.position;
                Vector3 extents = newPlatform.GetComponent<BoxCollider2D>().bounds.extents;
                extents.x = xScale;
                position.y += extents.y + (obstaclePrefab.renderer.bounds.size.y / 2);
                position.x += Random.Range(-extents.x / 2, extents.x / 2);
                Instantiate(obstaclePrefab, position, new Quaternion(0, 0, 0, 0));
            }
        }
	}
}