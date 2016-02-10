using UnityEngine;
using System.Collections;

public class WaterScroller : MonoBehaviour {

    public bool sideWays = false;
    public float scrollSpeed = 0.5F;
    public Material thisMat;
    private float offset = 0f;

	public bool multiChannel = false;
	public float scrollSpeedMulti = 0.11f;
	private float offsetMulti = 0;
	public bool uniqueMaterial = false;
    void Start()
    {
		if (uniqueMaterial)
			thisMat = GetComponent<Renderer>().material;
		else
    		thisMat = GetComponent<Renderer>().sharedMaterial;
    }

    void Update() {
       offset += Time.deltaTime * scrollSpeed;

       if (sideWays)
        thisMat.SetTextureOffset("_MainTex", new Vector2(0, offset));
       else
         thisMat.SetTextureOffset("_MainTex", new Vector2(offset, 0));
        if (offset > 1f)
        	offset -= 1.0f;

		if (multiChannel)
		{
			offsetMulti += Time.deltaTime * scrollSpeedMulti;
			if (sideWays)
				thisMat.SetTextureOffset("_Illum", new Vector2(0, offsetMulti));
			else
				thisMat.SetTextureOffset("_Illum", new Vector2(offsetMulti, 0));
			if (offsetMulti > 1f)
				offsetMulti -= 1.0f;
		}
    }

}
