using NaughtyAttributes;
using UnityEngine;

[RequireComponent( typeof( BoxCollider ) )]
public class SmoothCamTrigger : MonoBehaviour
{

	[Foldout( "Changes on Trigger" )] [SerializeField] private bool changeTarget = false;
	[Foldout( "Changes on Trigger" )] [SerializeField] private bool changeOffset = false;
	[Foldout( "Changes on Trigger" )] [SerializeField] private bool changeSmoothing = false;
	[Foldout( "Changes on Trigger" )] [SerializeField] private bool changeMinClampVector = false;
	[Foldout( "Changes on Trigger" )] [SerializeField] private bool changeMaxClampVector = false;
	[Foldout( "Changes on Trigger" )] [SerializeField] private bool changeClampX = false;
	[Foldout( "Changes on Trigger" )] [SerializeField] private bool changeClampY = false;
	[Space]
	[Foldout( "New Values" )] [ShowIf( "changeTarget" )] [SerializeField] private Transform target;
	[Foldout( "New Values" )] [ShowIf( "changeOffset" )] [SerializeField] private Vector3 offset;
	[Space]
	[Foldout( "New Values" )] [ShowIf( "changeSmoothing" )] [SerializeField] private float smoothing;
	[Space]
	[Foldout( "New Values" )] [ShowIf( "changeMinClampVector" )] [SerializeField] private Vector2 minClampVector;
	[Foldout( "New Values" )] [ShowIf( "changeMaxClampVector" )] [SerializeField] private Vector2 maxClampVector;
	[Foldout( "New Values" )] [ShowIf( "changeClampX" )] [SerializeField] private bool clampX;
	[Foldout( "New Values" )] [ShowIf( "changeClampY" )] [SerializeField] private bool clampY;

	private SmoothCam smoothCam;

	private void Start()
	{
		smoothCam = FindObjectOfType<SmoothCam>();
		GetComponent<BoxCollider>().isTrigger = true;
	}

	private void OnTriggerEnter( Collider other )
	{
		if( other.GetComponent<IPlayer>() != null )
		{
			if( changeTarget ) smoothCam.Target = target;
			if( changeOffset ) smoothCam.Offset = offset;
			if( changeSmoothing ) smoothCam.Smoothing = smoothing;
			if( changeMinClampVector ) smoothCam.MinClampVector = minClampVector;
			if( changeMaxClampVector ) smoothCam.MaxClampVector = maxClampVector;
			if( changeClampX ) smoothCam.ClampX = clampX;
			if( changeClampY ) smoothCam.ClampY = clampY;
		}
	}
}
