# 作业内容

## 1.简答题
### （1）解释 游戏对象（GameObjects） 和 资源（Assets）的区别与联系。
	游戏对象是所有在Unity上的基本，包括了物体，摄像机，音频等。
	资源是指在Unity上可使用的资源文件，例如图片以及具体的音乐等。
	区别：对象是抽象的，资源是具体的。
	联系：对象可以调用资源。
### （2）编写一个代码，使用 debug 语句来验证 MonoBehaviour基本行为或事件触发的条件
```
public classNewBehaviourScript : MonoBehaviour {
    void Awake() {
      Debug.Log("Now Awake!");
    }
		
    void Start() {
      Debug.Log("Now Start!");
    }
		
    void Update() {
      Debug.Log("Now Update!");
    }
		
    void FixedUpdate() {
      Debug.Log("Now FixedUpdate!");
    }

    void LateUpdate() {
      Debug.Log("Now LateUpdate!");
    }

    void OnGUI() {
      Debug.Log("Now onGUI!");
    }

    void OnDisable() {
      Debug.Log("Now onDisable!");
    }

    void OnEnable() {
      Debug.Log("Now onDestroy!");
    }
  }
```
### (3）查找脚本手册，了解 GameObject，Transform，Component 对象
    	1.分别翻译官方对三个对象的描述(Description)
        	游戏对象是在Unity中的基础对象，代表了人物，道具和背景。
        	变换组件确定了每个对象的位置，旋转和缩放。
        	组件是物体与行为之间的类似螺母与螺栓的枢纽。
    	2.描述下图中 table 对象（实体）的属性、table 的 Transform 的属性、 table 的部件
        	table 的对象是 GameObject，第一个选择框是 activeSelf 属性。
        	Transform 的属性是Position、Rotation、Scale。
        	table 的部件是 Mesh Filter、Box Collider、Mesh Renderer。
	3. 	//查找对象
		public static GameObject Find(string name)
        	//添加子对象
        	public static GameObect CreatePrimitive(PrimitiveTypetype)
        	//遍历对象树
        	foreach (Transform child in transform) {}
        	//清除所有子对象
        	foreach (Transform child in transform) { Destroy(child.gameObject);}
    	4.资源预设（Prefabs）与 对象克隆 (clone)
        	预设的好处：
        	    预设可以方便地使用多次出现的对象集，从而达到减轻工作量的作用。
        	预设与对象克隆 (clone or copy or Instantiate of Unity Object) 关系：
         	   对象克隆不会因为克隆源的改变而改变，预设则相反。
   	 5.尝试解释组合模式（Composite Pattern / 一种设计模式）。使用 BroadcastMessage() 方法
        	组合模式允许用户将对象组合成树形结构来表现”部分-整体“的层次结构，使得客户以一致的方式处理单个对象以及对象的组合。组合模式实现的最关键的地方是——简单对象和复合对象必须实现相同的接口。这就是组合模式能够将组合对象和简单对象进行一致处理的原因。
          	  子类对象：
           	     void say() {
           	     print("Hello World!");}
           	 父类对象：   
           	     void Start () {
            	    this.BroadcastMessage("say");}
![image](https://github.com/wakaka001/3d-game-learning/tree/master/src/UMLet.png)
