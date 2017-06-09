using UnityEngine;
using UnityEngine.Networking;

public class TwoPlayerNetworkManager : NetworkManager
{
	[SerializeField] private GameObject playerModel;
	[SerializeField] private Material material1;
	[SerializeField] private Material material2;
	
	public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
	{
		var startPosition = GetStartPosition();
		var player = Instantiate(playerModel, startPosition.position, startPosition.rotation);
		var material = numPlayers % 2 == 0 ? material1 : material2;
		player.GetComponent<Renderer>().material = material;
		NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
	}
}