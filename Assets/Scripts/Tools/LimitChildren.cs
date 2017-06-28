//Limits the ammount of objects this gameobject can have as children
//
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class LimitChildren : MonoBehaviour {
        [Range(0, 20)]
        public int Limit;

        //Can Be called by anyone, will destory this whole object
        public void DestroyThis()
        {
            Destroy(this.gameObject);
        }

        //Update the list and if something new is added, delete the first child on the list(Which will be the OLDEST gameobject in the set) 
        //--------------To call this function:  gameObject.GetComponent<LimitChildren>().UpdateList();
        //Every time your script instances something into a gameobject "Folder" You should be calling this to check if its overflowing
        public void UpdateList() {
            if(this.transform.childCount > Limit) 
				Destroy(this.transform.GetChild(0));
        }


}
