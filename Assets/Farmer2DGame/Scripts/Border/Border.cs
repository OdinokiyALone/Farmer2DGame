using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Border : MonoBehaviour
{

    //public Vector2 cornerTL;
    //public Vector2 cornerDR;
    public Vector2 size;
    public List<GameObject> objects;
    //private float minX, maxX, minY, maxY;
    private Rect rect;

    private void Start()
    {
        rect = new Rect(this.transform.position, size);
        //StartCoroutine("Check");
    }

    public bool IsInside(Vector2 pos)
    {
        return true;
        //return rect.Contains(pos);
    }

    private void Update()
    {
        
    }

    public IEnumerable Check()
    {
        while (true)
        {
            foreach (GameObject obj in objects)
            {
                Debug.Log(obj.name + ": " + IsInside(obj.transform.position));
                yield return new WaitForSeconds(1);
            }
        }
    }
            /*
    void Update()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            GameObject child = this.transform.GetChild(i)._gameObject;
            if (child.GetComponent() != null)
            {
            }





            float verticalExtent = Camera.main.orthographicSize;
            float horizontalExtent = verticalExtent * Screen.width / Screen.height;

            // ¬ычисл€ем минимальные и максимальные координаты
            minX = Camera.main.transform.position.x - horizontalExtent;
            maxX = Camera.main.transform.position.x + horizontalExtent;
            minY = Camera.main.transform.position.y - verticalExtent;
            maxY = Camera.main.transform.position.y + verticalExtent;

            float offsetX = 0;
            float offsetY = 0;

            minX += offsetX;
            maxX -= offsetX;
            minY += offsetY;
            maxY -= offsetY;

            // ќграничиваем позицию игрока
            float clampedX = Mathf.Clamp(transform.position.x, minX, maxX);
            float clampedY = Mathf.Clamp(transform.position.y, minY, maxY);

            transform.position = new Vector3(clampedX, clampedY, transform.position.z);
        }
    }
            */
        void OnDrawGizmos()
        {
            Vector2 pos = this.transform.position;
            Gizmos.DrawLine(pos, new Vector2(pos.x, pos.y + size.y));
            Gizmos.DrawLine(new Vector2(pos.x, pos.y + size.y), new Vector2(pos.x + size.x, pos.y + size.y));
            Gizmos.DrawLine(new Vector2(pos.x + size.x, pos.y + size.y), new Vector2(pos.x + size.x, pos.y));
            Gizmos.DrawLine(new Vector2(pos.x + size.x, pos.y), pos);
        
            //Gizmos.DrawLine(cornerTL,new Vector2(cornerTL.x,cornerDR.y));
            //Gizmos.DrawLine(new Vector2(cornerTL.x,cornerDR.y), cornerDR);
            //Gizmos.DrawLine(cornerDR, new Vector2(cornerDR.x, cornerTL.y));
            //Gizmos.DrawLine(new Vector2(cornerDR.x, cornerTL.y), cornerTL);

        }
}

