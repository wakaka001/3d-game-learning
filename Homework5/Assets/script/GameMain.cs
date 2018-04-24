using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.Manager;

namespace Com.Manager
{
    public class GameScenceController : IUserInterface, IQueryStatus, setStatus, IScore
    {
        public int sendDiskCount { get; private set; }
        private static GameScenceController _gameScenceController;
        private GameStatus _gameStatus;
        private ScenceStatus _scenceStatus;
        public DiskThrower _DiskThrower;
        private DiskFactory _diskFactory = DiskFactory.getFactory();
        public string mode;
        public void setmode(string _mode)
        {
            mode = _mode;
        }
        public static GameScenceController getGSController()
        {
            if (_gameScenceController == null)
                _gameScenceController = new GameScenceController();
            return _gameScenceController;
        }

        public void sendDisk()
        {
            int diskCount = _DiskThrower.diskCount;
            var diskList = _diskFactory.prepareDisks(diskCount);
            if(mode == "Physical")
                _DiskThrower.sendDisk(diskList);
            else
                _DiskThrower.sendDisk2(diskList);
        }

        public void destroyDisk(GameObject disk)
        {
            _DiskThrower.destroyDisk(disk);
            _diskFactory.recycleDisk(disk);
        }

        public GameStatus queryGameStatus() { return _gameStatus; }
        public ScenceStatus queryScenceStatus() { return _scenceStatus; }

        public void setGameStatus(GameStatus _gameStatus) { this._gameStatus = _gameStatus; }
        public void setScenceStatus(ScenceStatus _scenceStatus) { this._scenceStatus = _scenceStatus; }

        public void addScore() { GameModel.getGameModel().addScore(); }
        public void subScore() { GameModel.getGameModel().subScore(); }
        public int getScore() { return GameModel.getGameModel().score; }
        public void reset() { GameModel.getGameModel().reset(); }
        public void update() { _DiskThrower.scenceUpdate(); }
    }
    public interface IUserInterface
    {
        void sendDisk();
        void destroyDisk(GameObject disk);
    }

    public enum GameStatus
    {
        ing = 0,
        win = 1,
        fail = 2,
        load = 3
          
    }

    public enum ScenceStatus
    {
        waiting = 0,
        shooting = 1
    }

    public interface IQueryStatus
    {
        GameStatus queryGameStatus();
        ScenceStatus queryScenceStatus();
    }

    public interface setStatus
    {
        void setGameStatus(GameStatus _gameStatus);
        void setScenceStatus(ScenceStatus _scenceStatus);
    }

    public interface IScore
    {
        void addScore();
        void subScore();
        int getScore();
        void reset();
    }
}



public class GameMain : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        GameScenceController.getGSController()._DiskThrower = this.gameObject.AddComponent<DiskThrower>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}

