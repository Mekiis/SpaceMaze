@MenuItem ("SGM/Batch Rename")
static function BatchRename()
{ 
	// Renames all selected items in the Hierarchy to the first item selected, with numbers.
	var iname : String;
    ispacer = "0";
    icount = 0;
    iname = Selection.activeGameObject.name;   // The item in the inspector
       
    istuff = Selection.gameObjects.length;  // if I wanted this to support renaming of > 99 objects correctly, I'd use this.
       
    for (igo in Selection.gameObjects)
    {
        icount ++;
        if (icount > 9) ispacer = "";
        igo.name = iname + "_" + ispacer + icount;
    }
}
