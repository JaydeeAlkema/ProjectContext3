// https://amalgamatelabs.com/Blog/1/data_persistence

using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class DataAccess
{
	//[DllImport( "__Internal" )]
	//private static extern void SyncFiles();

	//[DllImport( "__Internal" )]
	//private static extern void WindowAlert( string message );


	//public static void Save( PlayerData playerData )
	//{
	//	string dataPath = string.Format( "{0}/Playerdata.dat", Application.persistentDataPath );
	//	BinaryFormatter formatter = new BinaryFormatter();
	//	FileStream fileStream;

	//	try
	//	{
	//		if( File.Exists( dataPath ) )
	//		{
	//			File.WriteAllText( dataPath, string.Empty );
	//			fileStream = File.Open( dataPath, FileMode.Open );
	//		}
	//		else
	//		{
	//			fileStream = File.Create( dataPath );
	//		}

	//		formatter.Serialize( fileStream, playerData );
	//		fileStream.Close();

	//		if( Application.platform == RuntimePlatform.WebGLPlayer )
	//		{
	//			SyncFiles();
	//		}
	//	}
	//	catch( Exception e )
	//	{
	//		PlatformSafeMessage( "Failed to save: " + e.Message );
	//	}
	//}

	//public static PlayerData Load()
	//{
	//	PlayerData playerData = null;
	//	string dataPath = string.Format( "{0}/Playerdata.dat", Application.persistentDataPath );

	//	try
	//	{
	//		if( File.Exists( dataPath ) )
	//		{
	//			BinaryFormatter formatter = new BinaryFormatter();
	//			FileStream fileStream = File.Open( dataPath, FileMode.Open );

	//			playerData = ( PlayerData )formatter.Deserialize( fileStream );
	//			fileStream.Close();
	//		}
	//	}
	//	catch( Exception e )
	//	{
	//		PlatformSafeMessage( "Failed to save: " + e.Message );
	//	}

	//	return playerData;
	//}

	//private static void PlatformSafeMessage( string message )
	//{
	//	if( Application.platform == RuntimePlatform.WebGLPlayer )
	//	{
	//		WindowAlert( message );
	//	}
	//	else
	//	{
	//		Debug.Log( message );
	//	}
	//}
}
