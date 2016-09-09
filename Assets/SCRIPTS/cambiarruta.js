#pragma strict
var siguienteway : Transform;
function Start () {

}

function Update () {

}
function OnTriggerStay (otro : Collider){
    otro.GetComponent(patrullar).objetivo = siguienteway;
}