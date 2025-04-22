
using System.Collections.Generic;
using common.stateMachine;
using DefaultNamespace;
using game_core;
using GameCore;
using ui.frame;
using UnityEngine;

public class GameMain : MonoBehaviour
{

    public GameObject viewRoot;

    public GameObject followUIRoot;
    
    /**
     * 不管是游戏还是登录都有的模块
     */
    public List<IModule> coreModules = new List<IModule>();

    public StateMachine<GameStateEnum> gameStateMachine = new StateMachine<GameStateEnum>();
    
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Locator.register<GameMain>(this);
        coreModules.Add(new ViewManager(viewRoot));
        // coreModules.Add(new NetService());
        coreModules.Add(new UpdateService());
        coreModules.Add(new GameObjectUIService(followUIRoot));
        gameStateMachine.addState( GameStateEnum.GAME ,new GameStartState(gameStateMachine));
        gameStateMachine.addState(GameStateEnum.LOGIN, new GameLoginState(gameStateMachine));
    }

    // Start is called before the first frame update
    private void Start()
    {
        // core module initialize first
        coreModules.ForEach( module => module.initialize());
        coreModules.ForEach( module => module.start());
        gameStateMachine.switchState(GameStateEnum.LOGIN);
    }

    // Update is called once per frame
    private void Update()
    {
        coreModules.ForEach( module => module.update());
        gameStateMachine.update();
    }

    private void OnDestroy()
    {
        Locator.unregister<GameMain>();
        coreModules.ForEach( module => module.destroy());
    }
}
