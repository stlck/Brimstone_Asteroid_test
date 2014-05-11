using UnityEngine;
using System.Collections;


public class StateMachine
{
	protected static StateMachine _instance = new StateMachine();
	
	public static StateMachine Instance
	{
		get
		{
			return _instance;
		}
	}

	public GameState Current {
		get;
		private set;
	}

	protected StateMenu Menu {
		get;
		private set;
	}
	protected StatePause Pause {
		get;
		private set;
	}
	protected StateLobby Lobby {
		get;
		private set;
	}
	protected StatePlay Play {
		get;
		private set;
	}
	protected StateLoading Loading {
		get;
		private set;
	}
	
	public void Init()
	{
		this.Menu = StateMenu.Instance;
		this.Pause = StatePause.Instance;
		this.Lobby = StateLobby.Instance;
		this.Play = StatePlay.Instance;
		this.Loading = StateLoading.Instance;

		Instance.Current = this.Menu;
	}

	public void UpdateCurrent()
	{
		Current.UpdateState ();
	}

	public void OnGUICurrent()
	{
		Current.OnGUIState ();
	}

	public static void SetPlay()
	{
		Instance.Current.EndState ();
		Instance.Current = Instance.Play;
		Instance.Current.StartState ();
	}
	public static void SetPause()
	{
		Instance.Current.EndState ();
		Instance.Current = Instance.Pause;
		Instance.Current.StartState ();
	}
	public static void SetMenu()
	{
		Instance.Current.EndState ();
		Instance.Current = Instance.Menu;
		Instance.Current.StartState ();
	}
	public static void SetLobby()
	{
		Instance.Current.EndState ();
		Instance.Current = Instance.Lobby;
		Instance.Current.StartState ();
	}
	public static void SetLoading()
	{
		Instance.Current.EndState ();
		Instance.Current = Instance.Loading;
		Instance.Current.StartState ();
	}
}

public abstract class GameState
{
	public abstract void UpdateState ();

	public abstract void OnGUIState ();

	public abstract void StartState();
	public abstract void EndState();
}
