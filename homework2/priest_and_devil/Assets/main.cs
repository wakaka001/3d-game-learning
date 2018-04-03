using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.Mygame;

namespace Com.Mygame
{
    public class Director : System.Object
    {
        private static Director _instance;
        public SceneController currentSceneController { get; set; }

        public static Director getInstance()
        {
            if (_instance == null)
            {
                _instance = new Director();
            }
            return _instance;
        }
    }

    public interface SceneController
    {
        void loadResources();
    }
    public interface UserAction
    {
        void moveBoat();
        void characterIsClicked(MyCharactorController characterCtrl);
        void restart();
    }

    public class move : MonoBehaviour
    {
        float speed = 20;
        int moving_status;  // 0->not moving, 1->moving to middle, 2->moving to dest
        Vector3 dest;
        Vector3 middle;

        void Update()
        {
            if (moving_status == 1)
            {
                transform.position = Vector3.MoveTowards(transform.position, middle, speed * Time.deltaTime);
                if (transform.position == middle)
                {
                    moving_status = 2;
                }
            }
            else if (moving_status == 2)
            {
                transform.position = Vector3.MoveTowards(transform.position, dest, speed * Time.deltaTime);
                if (transform.position == dest)
                {
                    moving_status = 0;
                }
            }
        }
        public void setDest(Vector3 _dest)
        {
            dest = _dest;
            middle = _dest;
            if (_dest.y == transform.position.y)
            {   // boat moving
                moving_status = 2;
                
            }
            else if (_dest.y < transform.position.y)
            {   // character from coast to boat
                middle.y = transform.position.y;
                
            }
            else
            {                               // character from boat to coast
                middle.x = transform.position.x;
               
            }
            
            moving_status = 1;
        }

        public void Reset()
        {
            moving_status = 0;
        }
    }
    public class MyCharactorController
    {
        GameObject character;
        move moveScript;
        ClickGUI clickGUI;
        int charactorType; // 0 means priest, 1 means devil
        bool _isOnBoat;
        shoreController shoreController;

        public MyCharactorController(string which_character)
        {
            if (which_character == "priest")
            {
                character = Object.Instantiate(Resources.Load("Perfabs/priest", typeof(GameObject)), Vector3.zero, Quaternion.identity, null) as GameObject;
                charactorType = 0;
            }
            else
            {
                character = Object.Instantiate(Resources.Load("Perfabs/devil", typeof(GameObject)), Vector3.zero, Quaternion.identity, null) as GameObject;
                charactorType = 1;
            }
            moveScript = character.AddComponent(typeof(move)) as move;
            clickGUI = character.AddComponent(typeof(ClickGUI)) as ClickGUI;
            clickGUI.setController(this);

        }

        public void setName(string name)
        {
            character.name = name;
        }

        public void setPosition(Vector3 pos)
        {
            character.transform.position = pos;
        }

        public void moveToPosition(Vector3 destination)
        {

            moveScript.setDest(destination);

        }

        public int getType()
        {
            return charactorType;
        }

        public string getName()
        {
            return character.name;
        }

        public void getOnBoat(BoatController boatCtrl)
        {
            shoreController = null;
            character.transform.parent = boatCtrl.getGameobj().transform;
            _isOnBoat = true;
        }

        public void getOnShore(shoreController coastCtrl)
        {
            shoreController = coastCtrl;
            character.transform.parent = null;
            _isOnBoat = false;
        }

        public bool isOnBoat()
        {
            return _isOnBoat;
        }
        public shoreController getCoastController()
        {
            return shoreController;
        }

        public void reset()
        {
            moveScript.Reset();
            shoreController = (Director.getInstance().currentSceneController as firstController).fromCoast;
            getOnShore(shoreController);
            setPosition(shoreController.getEmptyPosition());
            shoreController.getOnShore(this);
        }
    }
    public class shoreController
    {
        GameObject shore;
        Vector3 from_pos = new Vector3(9, 0, 0);
        Vector3 to_pos = new Vector3(-9, 0, 0);
        Vector3[] positions;
        int to_or_from; // -1 means to, 1 means from
        MyCharactorController[] passengerPlaner;

        public shoreController(string _to_or_from)
        {
            positions = new Vector3[] { new Vector3(7F,1,0), new Vector3(8F, 1, 0), new Vector3(9F,1, 0),
                                        new Vector3(10F,1,0),new Vector3(11F,1,0),new Vector3(12F,1,0)};

            passengerPlaner = new MyCharactorController[6];

            if (_to_or_from == "from")
            {
                shore = Object.Instantiate(Resources.Load("Perfabs/shore", typeof(GameObject)), from_pos, Quaternion.identity, null) as GameObject;
                shore.name = "from";
                to_or_from = 1;
            }
            else
            {
                shore = Object.Instantiate(Resources.Load("Perfabs/shore", typeof(GameObject)), to_pos, Quaternion.identity, null) as GameObject;
                shore.name = "to";
                to_or_from = -1;
            }
        }
        public int getEmptyIndex()
        {
            for (int i = 0; i < passengerPlaner.Length; i++)
            {
                if (passengerPlaner[i] == null) return i;

            }
            return -1;
        }
        public Vector3 getEmptyPosition()
        {
            Vector3 pos = positions[getEmptyIndex()];
            pos.x *= to_or_from;
            return pos;
        }
        public void getOnShore(MyCharactorController charactoectrl)
        {
            int index = getEmptyIndex();
            passengerPlaner[index] = charactoectrl;
        }
        public MyCharactorController getoffShore(string passenger_name)
        {
            for (int i = 0; i < passengerPlaner.Length; i++)
            {
                if (passengerPlaner[i] != null && passengerPlaner[i].getName() == passenger_name)
                {
                    MyCharactorController charactorCtrl = passengerPlaner[i];
                    passengerPlaner[i] = null;
                    return charactorCtrl;
                }
            }
            Debug.Log("cant find passenger on coast: " + passenger_name);
            return null;
        }
        public int get_to_or_from()
        {
            return to_or_from;
        }
        public int[] getCharacterNum()
        {
            int[] count = { 0, 0 };
            for (int i = 0; i < passengerPlaner.Length; i++)
            {
                if (passengerPlaner[i] == null)
                    continue;
                if (passengerPlaner[i].getType() == 0)
                {
                    count[0]++;
                }
                else
                {
                    count[1]++;
                }
            }
            return count;
        }
        public void reset()
        {
            passengerPlaner = new MyCharactorController[6];
        }

    }

    public class BoatController
    {
        GameObject boat;
        move moveScript;
        Vector3 fromPos = new Vector3(4.5f, 0, 0);
        Vector3 toPos = new Vector3(-4.5f, 0, 0);
        Vector3[] from_pos;
        Vector3[] to_pos;

        int to_or_from; // -1 means to, 1 means from
        MyCharactorController[] passenger = new MyCharactorController[2];


        public BoatController()
        {
            to_or_from = 1;

            from_pos = new Vector3[] { new Vector3(5, 0.8f, 0), new Vector3(4, 0.8f, 0) };
            to_pos = new Vector3[] { new Vector3(-4, 0.8f, 0), new Vector3(-5, 0.8f, 0) };
            boat = Object.Instantiate(Resources.Load("Perfabs/ship", typeof(GameObject)), fromPos, Quaternion.identity, null) as GameObject;
            boat.name = "boat";

            moveScript = boat.AddComponent(typeof(move)) as move;
            boat.AddComponent(typeof(ClickGUI));
        }
        public void Move()
        {
            if (to_or_from == -1)
            {
                moveScript.setDest(fromPos);
                to_or_from = 1;
            }
            else
            {
                moveScript.setDest(toPos);
                to_or_from = -1;
            }
        }
        public int getEmptyIndex()
        {
            for (int i = 0; i < passenger.Length; i++)
            {
                if (passenger[i] == null)
                {
                    return i;
                }
            }
            return -1;
        }
        public bool isEmpty()
        {
            for (int i = 0; i < passenger.Length; i++)
            {
                if (passenger[i] != null)
                {
                    return false;
                }
            }
            return true;
        }
        public Vector3 getEmptyPosition()
        {
            Vector3 pos;
            int emptyIndex = getEmptyIndex();
            if (to_or_from == -1)
            {
                pos = to_pos[emptyIndex];
            }
            else
            {
                pos = from_pos[emptyIndex];
            }
            return pos;
        }
        public void GetOnBoat(MyCharactorController characterCtrl)
        {
            int index = getEmptyIndex();
            passenger[index] = characterCtrl;
        }
        public MyCharactorController GetOffBoat(string passenger_name)
        {
            for (int i = 0; i < passenger.Length; i++)
            {
                if (passenger[i] != null && passenger[i].getName() == passenger_name)
                {
                    MyCharactorController charactorCtrl = passenger[i];
                    passenger[i] = null;
                    return charactorCtrl;
                }
            }
            Debug.Log("Cant find passenger in boat: " + passenger_name);
            return null;
        }
        public GameObject getGameobj()
        {
            return boat;
        }
        public int get_to_or_from()
        {
            return to_or_from;
        }
        public int[] getCharacterNum()
        {
            int[] count = { 0, 0 };
            for (int i = 0; i < passenger.Length; i++)
            {
                if (passenger[i] == null)
                    continue;
                if (passenger[i].getType() == 0)
                {
                    count[0]++;
                }
                else
                {
                    count[1]++;
                }
            }
            return count;
        }
        public void reset()
        {
            moveScript.Reset();
            if (to_or_from == -1)
            {
                Move();
            }
            passenger = new MyCharactorController[2];
        }
    }
}


